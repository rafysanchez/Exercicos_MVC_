using System.Web;


namespace Sids.Prodesp.Core.Services.WebService.Empenho
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Extension;
    using Model.Interface.Base;
    using Model.Interface.Configuracao;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using Model.Interface.Service.Empenho;
    using Model.ValueObject.Service.Siafem.Base;
    using Model.ValueObject.Service.Siafem.Empenho;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Sids.Prodesp.Infrastructure.Helpers;
    using SIAFDOC = Model.ValueObject.Service.Siafem.Empenho.SIAFDOC;
    using SFCODOC = Model.ValueObject.Service.Siafem.Empenho.SFCODOC;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Base.Empenho;
    using Model.Interface;
    using System.Configuration;

    public class SiafemEmpenhoService : SiafemEmpenhoBaseService
    {
        private ILogError _logError;
        private readonly ISiafemEmpenho _siafemService;

        private readonly ISiafemEmpenho _siafisicoService;

        private readonly ICrudPrograma _programa;
        private readonly ICrudFonte _fonte;
        private readonly ICrudEstrutura _estutura;

        public SiafemEmpenhoService(ILogError logError, ISiafemEmpenho siafemService, ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estutura) : base(logError)
        {
            _estutura = estutura;
            _fonte = fonte;
            _logError = logError;
            _siafemService = siafemService;
            _programa = programa;
        }

        #region Metodos Publicos 

        #region Empenho
        public string InserirEmpenhoSiafem(string login, string senha, ref Model.Entity.Empenho.Empenho objModel, IEnumerable<IMes> mes, IEnumerable<IEmpenhoItem> itens, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new Documento
                {
                    Empenho = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes,
                    EmpenhoItens = itens
                };

                var siafdoc = GerarSiafdoc(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoSiafem(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNe = xm.GetElementsByTagName("NumeroNE");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (numeroNe.Count > 0)
                    objModel.NumeroEmpenhoSiafem = numeroNe[0].InnerText;

                string msgErro = null;

                foreach (XmlElement message in messagemErro)
                {
                    msgErro = string.Concat(msgErro, " \n ", message.InnerText);
                }

                if (!bool.Parse(root))
                {
                    throw new SiafemException(msgErro);
                }
                return numeroNe[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoSiafem(string login, string senha, ref Model.Entity.Empenho.Empenho objModel, IList<EmpenhoItem> itens, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new Documento
                {
                    EmpenhoReforco = new EmpenhoReforco
                    {
                        CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                        CodigoGestao = objModel.CodigoGestao,
                        NumeroEmpenhoSiafem = objModel.NumeroEmpenhoSiafem
                    },
                    EmpenhoItens = itens
                };

                var siafdoc = GerarSiafdocReforcoDescricao(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoReforcoSiafem(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNe = xm.GetElementsByTagName("NumeroNE");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafemException(messagemErro[0].InnerText);

                return numeroNe[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoSiafisico(string login, string senha, Model.Entity.Empenho.Empenho objModel, IEnumerable<IMes> mes, IEnumerable<IEmpenhoItem> itens, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new Documento
                {
                    Empenho = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes,
                    EmpenhoItens = itens
                };

                var siafdoc = GerarSfcodocInclusao(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNe = xm.GetElementsByTagName("NumeroCT");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText);

                return numeroNe[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public int InserirEmpenhoSiafisico(string login, string senha, Model.Entity.Empenho.Empenho objModel, IEmpenhoItem empenhoItem, string unidadeGestora)
        {
            try
            {
                Documento documento = new Documento
                {
                    Empenho = objModel,
                    EmpenhoItem = empenhoItem
                };

                var siafdoc = GerarSiafisicoCtDescricao(documento, EnumAcaoSiaf.Inserir, EnumTipoOperacaoEmpenho.Empenho);

                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText + ", Item: " + empenhoItem.CodigoItemServico);

                return Convert.ToInt32(xm.GetElementsByTagName("NumSeq")[0].InnerText);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public int InserirEmpenhoSiafisicoAlteracao(string login, string senha, Model.Entity.Empenho.Empenho objModel, IEmpenhoItem empenhoItem, string unidadeGestora)
        {
            try
            {
                Documento dtoSfcodoc = new Documento
                {
                    Empenho = objModel,
                    EmpenhoItem = empenhoItem
                };

                var siafdoc = GerarSfcodocAlteracaoItem(dtoSfcodoc);

                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText);

                return Convert.ToInt32(xm.GetElementsByTagName("NumSeq")[0].InnerText);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoSiafisicoAlteracao(string login, string senha, Model.Entity.Empenho.Empenho objModel, IList<IMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new Documento
                {
                    Empenho = objModel,
                    ValorMes = mes,
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura() { Codigo = objModel.NaturezaId }).FirstOrDefault()
                };

                var siafdoc = GerarSfcodocAlteraçao(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");

                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText);

                return objModel.NumeroCT;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public ConsultaPrecoNE ConsultaPrecoNE(string cPF, string v1, string numNE, string uge, string gestao)
        {
            try
            {
                var siafdoc = new SFCODOC
                {
                    cdMsg = "SFCOConPrecoNE",
                    SiafisicoConPrecoNE = new SiafisicoConPrecoNE
                    {
                        documento = new documento
                        {
                            UnidadeGestora = uge,
                            Gestao = gestao,
                            NumNE = numNE
                        }
                    }
                };

                var result = _siafemService.Consultar(cPF, v1, uge, siafdoc);

                var xml = ConverterXml(result);

                var MsgRetorno = xml.GetElementsByTagName("MsgRetorno").Count > 0 ? xml.GetElementsByTagName("MsgRetorno")[0].InnerText : String.Empty;
                var Doc_Retorno = xml.GetElementsByTagName("Doc_Retorno").Count > 0 ? xml.GetElementsByTagName("Doc_Retorno")[0].InnerText : String.Empty;

                if (string.IsNullOrWhiteSpace(Doc_Retorno))
                    throw new SidsException("SIAFISICO - ERRO AO CONSULTAR O SERVIÇO.");

                if (!string.IsNullOrWhiteSpace(MsgRetorno))
                    throw new SiafisicoException(String.Format("ConPrecoNE {0}", MsgRetorno));


                var resultado = xml.GetElementsByTagName("NE").ConvertNodeTo<ConsultaPrecoNE>();

                return resultado;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public static T Deserialize<T>(string xml)
        {
            var xmlReaderSettings = new XmlReaderSettings()
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                ValidationType = ValidationType.None
            };

            XmlReader xmlReader = XmlTextReader.Create(new StringReader(xml), xmlReaderSettings);
            XmlSerializer xs = new XmlSerializer(typeof(T), "");

            return (T)xs.Deserialize(xmlReader);
        }

        #endregion

        #region Reforço Empenho

        public string InserirEmpenhoReforcoSiafem(string login, string senha, ref EmpenhoReforco objModel, IEnumerable<IMes> mes, IEnumerable<IEmpenhoItem> itens, string unidadeGestora)
        {
            try
            {

                var dtoSiafdoc = new Documento
                {
                    EmpenhoReforco = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes,
                    EmpenhoItens = itens
                };

                var siafdoc = GerarSiafdocReforco(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoReforcoSiafem(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNe = xm.GetElementsByTagName("NumeroNE");

                if (numeroNe.Count > 0)
                    objModel.NumeroEmpenhoSiafem = numeroNe[0].InnerText;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafemException(messagemErro[0].InnerText);

                return numeroNe[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoReforcoSiafem(string login, string senha, ref EmpenhoReforco objModel, IEnumerable<IEmpenhoItem> itens, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new Documento
                {
                    EmpenhoReforco = objModel,
                    EmpenhoItens = itens
                };

                var siafdoc = GerarSiafdocReforcoDescricao(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoReforcoSiafem(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNe = xm.GetElementsByTagName("NumeroNE");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafemException(messagemErro[0].InnerText);

                return numeroNe[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoReforcoSiafisico(string login, string senha, EmpenhoReforco objModel, List<IMes> meses, string unidadeGestora)
        {
            try
            {
                Documento dtoSiafdoc = new Documento
                {
                    EmpenhoReforco = objModel,
                    ValorMes = meses
                };

                var siafdoc = GerarSfcodocInclusaoSiafisico(dtoSiafdoc);

                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numCt = xm.GetElementsByTagName("NumeroCT");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;


                if (messagemErro.Count > 0 && messagemErro[0].InnerText != "" && !messagemErro[0].InnerText.Contains("CONTRATO REFORCADO COM SUCESSO TECLE ENTER PARA RETORNAR"))
                    root = false.ToString();
                else if (status.Count > 0)
                    root = status[0].FirstChild.Value;

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText);

                objModel.NumeroCT = numCt.Count > 0 ? numCt[0].InnerText : objModel.NumeroCT;
                return objModel.NumeroCT;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public int InserirEmpenhoReforcoSiafisico(string login, string senha, EmpenhoReforco objModel, IEmpenhoItem empenhoItem, string unidadeGestora)
        {
            try
            {
                Documento documento = new Documento
                {
                    Empenho = new Empenho
                    {
                        CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                        CodigoGestao = objModel.CodigoGestao,
                        NumeroCT = objModel.NumeroCT
                    },
                    EmpenhoItem = empenhoItem
                };
                //var siafdoc = GerarSfcodocDescricao(documento);
                var siafdoc = GerarSiafisicoCtDescricao(documento, EnumAcaoSiaf.Inserir, EnumTipoOperacaoEmpenho.Reforco);

                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException("SIAFISICO - " + messagemErro[0].InnerText + ", Item: " + empenhoItem.CodigoItemServico);

                return Convert.ToInt32(xm.GetElementsByTagName("NumSeq")[0].InnerText);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public int InserirEmpenhoSiafisicoReforcoAlteracao(string login, string senha, EmpenhoReforco objModel, IEmpenhoItem empenhoItem, string unidadeGestora)
        {
            try
            {
                Documento documento = new Documento
                {
                    Empenho = new Empenho
                    {
                        CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                        CodigoGestao = objModel.CodigoGestao,
                        NumeroCT = objModel.NumeroCT
                    },
                    EmpenhoItem = empenhoItem
                };
                var siafdoc = GerarSiafisicoCtDescricao(documento, EnumAcaoSiaf.Alterar, EnumTipoOperacaoEmpenho.Reforco);


                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText);

                return Convert.ToInt32(xm.GetElementsByTagName("NumSeq")[0].InnerText);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }


        #endregion

        #region Cancelamento Empenho
        public string InserirEmpenhoCancelamentoSiafem(string login, string senha, ref EmpenhoCancelamento objModel, IEnumerable<IMes> meses, string unidadeGestora)
        {
            try
            {
                var siafdoc = SiafemDocumentProvider(objModel, new Documento
                {
                    EmpenhoCancelamento = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = meses
                });

                var response = ConverterXml(_siafemService.InserirEmpenhoCancelamentoSiafem(login, senha, unidadeGestora, siafdoc));

                var statusMessage = response.GetElementsByTagName("StatusOperacao");

                var errorMessage = response.GetElementsByTagName("MsgErro");

                errorMessage = errorMessage.Count == 0 ? response.GetElementsByTagName("MsgRetorno") : errorMessage;

                var numeroNE = response.GetElementsByTagName("NumeroNE");

                var isRoot = true;

                if (statusMessage.Count > 0)
                    isRoot = Convert.ToBoolean(statusMessage[0].FirstChild.Value);
                else if (errorMessage.Count > 0 && !string.IsNullOrEmpty(errorMessage[0].InnerText))
                    isRoot = false;

                if (numeroNE.Count > 0)
                    objModel.NumeroEmpenhoSiafem = numeroNE[0].InnerText;

                var msgErro = default(string);
                foreach (XmlElement message in errorMessage)
                    msgErro = $"{msgErro} \n {message.InnerText}";

                if (!isRoot)
                    throw new SidsException($"SIAFEM - {msgErro}");

                return numeroNE[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoCancelamentoSiafem(string login, string senha, ref EmpenhoCancelamento objModel, IEnumerable<IEmpenhoItem> itens, string unidadeGestora)
        {
            try
            {
                var siafdoc = CreateSiafemDocIncDescNE(new Documento
                {
                    EmpenhoCancelamento = objModel,
                    EmpenhoItens = itens
                });
                var response = ConverterXml(_siafemService
                    .InserirEmpenhoCancelamentoSiafem(login, senha, unidadeGestora, siafdoc));
                var statusMessage = response.GetElementsByTagName("StatusOperacao");
                var errorMessage = response.GetElementsByTagName("MsgErro");
                var numeroNE = response.GetElementsByTagName("NumeroNE")[0].InnerText;
                var isRoot = true;

                if (statusMessage.Count > 0)
                    isRoot = Convert.ToBoolean(statusMessage[0].FirstChild.Value);
                else if (errorMessage.Count > 0 && !string.IsNullOrEmpty(errorMessage[0].InnerText))
                    isRoot = false;

                if (!isRoot)
                    throw new SidsException($"SIAFEM - {errorMessage[0].InnerText}");

                return numeroNE;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirEmpenhoCancelamentoSiafisico(string login, string senha, EmpenhoCancelamento objModel, IEnumerable<IMes> mes, string unidadeGestora)
        {
            try
            {
                var siafdoc = SiafisicoDocumentProvider(objModel, new Documento
                {
                    EmpenhoCancelamento = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes
                });
                var response = ConverterXml(_siafemService
                    .InserirEmpenhoCancelamentoSiafisico(login, senha, unidadeGestora, siafdoc));
                var statusMessage = response.GetElementsByTagName("StatusOperacao");
                var errorMessage = response.GetElementsByTagName("MsgErro");
                errorMessage = errorMessage.Count == 0 ? response.GetElementsByTagName("MsgRetorno") : errorMessage;
                var numeroCT = response.GetElementsByTagName("NumeroCT");
                var isRoot = true;

                if (statusMessage.Count > 0)
                    isRoot = Convert.ToBoolean(statusMessage[0].FirstChild.Value);
                else if (errorMessage.Count > 0 && !string.IsNullOrEmpty(errorMessage[0].InnerText))
                    isRoot = false;

                if (!isRoot)
                    throw new SidsException($"SIAFISICO - {errorMessage[0].InnerText}");

                return numeroCT[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public int InserirEmpenhoCancelamentoSiafisico(string login, string senha, EmpenhoCancelamento objModel, IEmpenhoItem empenhoItem, string unidadeGestora)
        {
            try
            {
                var newDoc = new Documento
                {
                    EmpenhoCancelamento = objModel,
                    EmpenhoItem = empenhoItem
                };
                var siafdoc = GerarSiafisicoCtDescricao(newDoc, EnumAcaoSiaf.Inserir, EnumTipoOperacaoEmpenho.Cancelamento);

                var response = ConverterXml(_siafemService
                    .InserirEmpenhoCancelamentoSiafisico(login, senha, unidadeGestora, siafdoc));
                var statusMessage = response.GetElementsByTagName("StatusOperacao");
                var errorMessage = response.GetElementsByTagName("MsgRetorno");
                var isRoot = true;

                if (statusMessage.Count > 0)
                    isRoot = Convert.ToBoolean(statusMessage[0].FirstChild.Value);
                else if (errorMessage.Count > 0 && !string.IsNullOrEmpty(errorMessage[0].InnerText))
                    isRoot = false;

                if (!isRoot)
                    throw new SidsException($"SIAFISICO - {errorMessage[0].InnerText}, Item: {empenhoItem.CodigoItemServico}");

                return Convert.ToInt32(response.GetElementsByTagName("NumSeq")[0].InnerText);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string ContablizarEmpenhoCancelamento(string login, string senha, EmpenhoCancelamento objModel, IEnumerable<IMes> mes, string unidadeGestora)
        {
            try
            {
                var siafdoc = SiafisicoDocumentProvider(objModel, new Documento
                {
                    EmpenhoCancelamento = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes
                });
                var response = ConverterXml(_siafemService
                    .InserirEmpenhoCancelamentoSiafisico(login, senha, unidadeGestora, siafdoc));
                var statusMessage = response.GetElementsByTagName("StatusOperacao");
                var errorMessage = response.GetElementsByTagName("MsgErro");
                errorMessage = errorMessage.Count == 0 ? response.GetElementsByTagName("MsgRetorno") : errorMessage;
                var NumeroNE = response.GetElementsByTagName("NeCancelamento");
                var isRoot = true;

                if (statusMessage.Count > 0)
                    isRoot = Convert.ToBoolean(statusMessage[0].FirstChild.Value);
                else if (errorMessage.Count > 0 && !string.IsNullOrEmpty(errorMessage[0].InnerText))
                    isRoot = false;

                if (!isRoot)
                    throw new SidsException($"SIAFISICO - {errorMessage[0].InnerText}");

                return NumeroNE[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }
        #endregion

        public string ContablizarEmpenho(string login, string senha, IEmpenho objModel, string unidadeGestora)
        {
            try
            {
                SFCODOC siafdoc;

                List<string> licitacoes = new List<string> { "2", "5" };


                if(objModel.ContBec)
                    siafdoc = new SFCODOC
                    {
                        cdMsg = "SFCOContNeBec2",
                        SiafisicoDocContNeBec2 = new SiafisicoDocContNeBec2
                        {
                            documento = new documento
                            {
                                UG = objModel.CodigoUnidadeGestora,
                                Gestao = objModel.CodigoGestao,
                                NumeroCT = objModel.NumeroCT.Substring(6, 5)
                            }
                        }
                    };
                else
                    siafdoc = new SFCODOC
                    {
                        cdMsg = "SFCOContNe2",
                        SiafisicoDocContNe2 = new SiafisicoDocContNe2
                        {
                            documento = new documento
                            {
                                UG = objModel.CodigoUnidadeGestora,
                                Gestao = objModel.CodigoGestao,
                                NumeroCT = objModel.NumeroCT.Substring(6, 5)
                            }
                        }
                    };

                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNe = xm.GetElementsByTagName("NumeroNe");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException(messagemErro[0].InnerText);



                return numeroNe[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        #region Metodos Consultas

        public ConsultaNe ConsultaNe(string login, string senha, string numeroNe, string unidadeGestora)
        {
            var empenho = new Empenho { NumeroEmpenhoSiafem = numeroNe };

            return ConsultaNe(login, senha, empenho, unidadeGestora);
        }
        public ConsultaNe ConsultaNe(string login, string senha, IEmpenho empenho, string unidadeGestora)
        {
            try
            {
                var siafdoc = new SIAFDOC
                {
                    cdMsg = "SIAFConsultaEmpenhos",
                    SiafemDocConsultaEmpenhos = new SiafemDocConsultaEmpenhos
                    {
                        documento = new documento
                        {
                            UnidadeGestora = unidadeGestora,
                            Gestao = "16055",
                            NumeroNe = empenho.NumeroEmpenhoSiafem + empenho.NumeroEmpenhoSiafisico
                        }
                    }
                };

                var result = _siafemService.Consultar(login, (senha), unidadeGestora, siafdoc);

                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgErro");

                if (messagem.Count <= 0)
                    messagem = xm.GetElementsByTagName("MsgRetorno");

                string root = "";
                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new SiafemException(messagem[0].FirstChild.Value);
                }
                var document = xm.GetElementsByTagName("documento");


                ConsultaNe resultingMessage = ConvertNode<ConsultaNe>(document);


                return resultingMessage;
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }

        public ConsultaCt ConsultaCt(string login, string senha, string numCt, string unidadeGestora, string gestao)
        {
            try
            {
                var siafdoc = new SFCODOC
                {
                    cdMsg = "SFCOConsultaCT",
                    SiafisicoConsultaCT = new SiafisicoConsultaCT
                    {
                        documento = new documento
                        {
                            UnidadeGestora = unidadeGestora,
                            Gestao = gestao,
                            PrefixoCT = numCt.Substring(0, 6),
                            NumCT = numCt.Substring(6, 5),
                        }

                    }
                };

                var result = _siafemService.Consultar(login, senha, unidadeGestora, siafdoc);

                var msgErros = result.ToXml("SFCODOC");

                var status = msgErros.GetElementsByTagName("StatusOperacao");
                var messagem = msgErros.GetElementsByTagName("MsgErro");

                if (messagem.Count <= 0)
                    messagem = msgErros.GetElementsByTagName("MsgRetorno");

                string root = "";
                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new SiafemException(messagem[0].FirstChild.Value);
                }


                var ct = msgErros.GetElementsByTagName("CT").ConvertNodeTo<ConsultaCt>();

                return ct;
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }


        public ConsultaEmpenhos ConsultaEmpenhos(string login, string senha, string cgcCpf, string data, string fonte, string gestao, string unidadeGestora, string gestaoCredor, string licitacao, string modalidadeEmpenho, string natureza, string numeroNe, string processo, string uge)
        {
            try
            {
                var siafdoc = new SIAFDOC
                {
                    cdMsg = "SIAFListaEmpenhos",
                    SiafemDocListaEmpenhos = new SiafemDocListaEmpenhos
                    {
                        documento = new documento
                        {
                            CgcCpf = cgcCpf,
                            Data = data == null ? null : Convert.ToDateTime(data).ToString("ddMMMyyyy"),
                            Fonte = fonte,
                            Gestao = gestao,
                            GestaoCredor = gestaoCredor,
                            Licitacao = licitacao,
                            ModalidadeEmpenho = modalidadeEmpenho,
                            Natureza = natureza?.Substring(0, 6),
                            NumeroNe = numeroNe?.Substring(6, 5),
                            Prefixo = numeroNe?.Substring(0, 6),
                            Processo = processo,
                            UnidadeGestora = unidadeGestora
                        }
                    }
                };

                var result = _siafemService.Consultar(login, senha, uge, siafdoc);

                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgErro");

                if (messagem.Count <= 0)
                    messagem = xm.GetElementsByTagName("MsgRetorno");

                string root = "";
                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new SiafemException(messagem[0].FirstChild.Value);
                }
                var document = xm.GetElementsByTagName("documento");


                ConsultaEmpenhos resultingMessage = ConvertNode<ConsultaEmpenhos>(document);


                return resultingMessage;
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }


        public ConsultaPdfEmpenho ObterPdfEmpenho(string login, string senha, IEmpenho empenho, EnumTipoServicoFazenda tipo, string unidadeGestora)
        {
            try
            {
                string msg = string.Empty;
                switch (tipo)
                {
                    case EnumTipoServicoFazenda.Siafisico:
                        msg = "SFCONEPDF001";
                        break;
                    case EnumTipoServicoFazenda.Siafem:
                        msg = "SIAFNEPDF001";
                        break;
                }

                var doc = new documento
                {
                    UG = unidadeGestora,
                    Gestao = "16055",
                    NumeroNE = tipo == EnumTipoServicoFazenda.Siafem ? empenho.NumeroEmpenhoSiafem.Substring(6) : empenho.NumeroEmpenhoSiafisico.Substring(6),
                    cdOrdenador = ConfigurationManager.AppSettings["CdOrdenador"]
                };

         
                var result = string.Empty;

                switch (tipo)
                {
                    case EnumTipoServicoFazenda.Siafisico:
                        var siafdoc = new SFCODOC
                        {
                            cdMsg = msg
                        };

                        siafdoc.SiafisicoDocPdfEmpenho = new SiafemDocObterPdfEmpenho
                        {
                            documento = doc
                        };
                        result = _siafemService.Consultar(login, (senha), unidadeGestora, siafdoc);
                        break;

                    case EnumTipoServicoFazenda.Siafem:

                        var siafdocs = new SIAFDOC
                        {
                            cdMsg = msg
                        };

                        siafdocs.SiafemDocPdfEmpenho = new SiafemDocObterPdfEmpenho
                        {
                            documento = doc
                        };

                        result = _siafemService.Consultar(login, (senha), unidadeGestora, siafdocs);
                        break;
                }


                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgErro");

                if (messagem.Count <= 0)
                    messagem = xm.GetElementsByTagName("MsgRetorno");

                string root = "";
                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();
                else
                    root = false.ToString();


                if (!bool.Parse(root))
                {
                    throw new SiafemException(messagem[0].FirstChild.Value);
                }
                var document = xm.GetElementsByTagName("documento");


                ConsultaPdfEmpenho resultingMessage = ConvertNode<ConsultaPdfEmpenho>(document);


                return resultingMessage;
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }


        #endregion


        #endregion

        #region Metodos Privados

        #region EmpenhoCancelamento
        #region Helpers
        private static documento GenerateValueAndDescriptionForDocumentMonths(documento document, IEnumerable<IMes> months)
        {
            var properties = document.GetType().GetProperties();
            for (var i = 0; i < months.Count(); i++)
            {
                var monthNumber = (i + 1).ToString("D2");
                properties.FirstOrDefault(x => x.Name.Equals(string.Concat("Mes", monthNumber)))
                    .SetValue(document, months.ElementAt(i).Descricao);

                properties.FirstOrDefault(x => x.Name.Equals(string.Concat("Valor", monthNumber)))
                    .SetValue(document, months.ElementAt(i).ValorMes.ToString(CultureInfo.InvariantCulture));
            }

            return document;
        }

        private static documento GenerateSiafemDocument(Documento document)
        {
            var siafdoc = new documento
            {
                EmpenhoOriginal = document.EmpenhoCancelamento.CodigoEmpenhoOriginal?.Substring(6, 5),
                DataEmissao = document.EmpenhoCancelamento.DataEmissao.ToString("ddMMMyyyy").ToUpper(),
                UnidadeGestora = document.EmpenhoCancelamento.CodigoUnidadeGestora,
                Gestao = document.EmpenhoCancelamento.CodigoGestao,
                CgcCpfUgCredor = document.EmpenhoCancelamento.NumeroCNPJCPFUGCredor,
                GestaoCredor = document.EmpenhoCancelamento.CodigoGestaoCredor,
                NaturezaDespesa = document.Estutura.Natureza + document.EmpenhoCancelamento.CodigoNaturezaItem,
                Processo = document.EmpenhoCancelamento.NumeroProcesso,
                Evento = document.EmpenhoCancelamento.CodigoEvento.ZeroParaNulo(),
                Valor = document.ValorMes.Sum(z => z.ValorMes).ToString(CultureInfo.InvariantCulture)
            };

            return GenerateValueAndDescriptionForDocumentMonths(siafdoc, document.ValorMes);
        }

        private static documento GenerateSiafisicoDocument(Documento document)
        {
            var siafdoc = new documento
            {
                Gestao = document.EmpenhoCancelamento.CodigoGestao,
                Contrato = document.EmpenhoCancelamento.NumeroOriginalCT?.Substring(6, 5),

                Valor = document.ValorMes.Sum(z => z.ValorMes).ToString(CultureInfo.InvariantCulture)
            };

            return GenerateValueAndDescriptionForDocumentMonths(siafdoc, document.ValorMes);
        }

        private static SIAFDOC SiafemDocumentProvider(EmpenhoCancelamento objModel, Documento document)
        {
            var siafdoc = new SIAFDOC();

            switch (objModel.EmpenhoCancelamentoTipoId)
            {
                case 1: //Cancela Empenho Fonte Tesouro
                    siafdoc = CreateSiafemDocCanNETes(document); break;
                case 2: //Cancela Empenho Fonte Não Tesouro
                    siafdoc = CreateSiafemDocCanNEVinc(document); break;
                case 3: //Cancela Empenho de Adiantamento de Fonte Tesouro
                    siafdoc = CreateSiafemDocCanNeAdTes(document); break;
                case 4: //Cancela Empenho de Adiantamento de Fonte Vinculado
                    siafdoc = CreateSiafemDocCanNeAdVin(document); break;
                case 5: //Cancela Empenho de Adiantamento Vinculado Não Pago
                    siafdoc = CreateSiafemDocCanNeAdVnp(document); break;
                case 6: //Cancela Empenho de Pessoal 
                    siafdoc = CreateSiafemDocCanNePess(document); break;
                case 7: //Cancelamento de RP sem Limite SIAFCanNeTesBL
                    siafdoc = CreateSiafemDocCanNeTesBL(document); break;
            }

            return siafdoc;
        }

        private static SFCODOC SiafisicoDocumentProvider(EmpenhoCancelamento objModel, Documento document)
        {
            var siafdoc = new SFCODOC();

            switch (objModel.EmpenhoCancelamentoTipoId)
            {
                case 8:  //Cancela CT Fonte Não Tesouro 
                    siafdoc = CreateSiafisicoDocCanCTVinc(document); break;
                case 9:  //Cancela Empenho Fonte Não Tesouro 
                    siafdoc = CreateSiafisicoDocCanNeVin(document); break;
                case 10: //Cancela NE BEC Fonte Tesouro
                    siafdoc = CreateSiafisicoDocCanNeBecTes(document); break;
                case 11: //Cancela NE BEC Fonte Não Tesouro
                    siafdoc = CreateSiafisicoDocCanNeBecVin(document); break;
            }

            return siafdoc;
        }
        #endregion

        #region Siafem
        private static SIAFDOC CreateSiafemDocIncDescNE(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFIncDescNE001",
                SiafemDocIncDescNE = new SiafemDocIncDescNE
                {

                    documento = new documento
                    {
                        UnidadeGestora = document.EmpenhoCancelamento.CodigoUnidadeGestora,
                        Gestao = document.EmpenhoCancelamento.CodigoGestao,
                        NumeroEmpenho = document.EmpenhoCancelamento.NumeroEmpenhoSiafem
                    },
                    descricao = new descricao
                    {
                        Repeticao = new Repeticao
                        {
                            des = document.EmpenhoItens.Select(x => new des
                            {
                                UnidadeMedida = x.DescricaoUnidadeMedida,
                                Quantidade = x.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],
                                PrecoUnitario = x.ValorUnitario.ZeroParaNulo(),
                                PrecoTotal = x.ValorTotal.ZeroParaNulo(),
                                DescricaoParte1 = x.DescricaoItemSiafem?.Replace(";", ""),
                                DescricaoParte2 = " ",
                                DescricaoParte3 = " "
                            }).ToList()
                        }
                    }
                }
            };
        }

        private static SIAFDOC CreateSiafemDocCanNETes(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFCanNeTes",
                SiafemDocCanNeTes = new SiafemDocCanNeTes
                {
                    documento = GenerateSiafemDocument(document)
                }
            };
        }

        private static SIAFDOC CreateSiafemDocCanNEVinc(Documento document)
        {
            var siafidoc = new SIAFDOC
            {
                cdMsg = "SIAFCanNeVin",
                SiafemDocCanNeVin = new SiafemDocCanNeVin
                {
                    documento = GenerateSiafemDocument(document)
                }
            };

            siafidoc.SiafemDocCanNeVin.documento.Evento = Convert.ToString(document.EmpenhoCancelamento.CodigoEvento);

            return siafidoc;
        }

        private static SIAFDOC CreateSiafemDocCanNeAdTes(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFCanNeAdTes",
                SiafemDocCanNeAdTes = new SiafemDocCanNeAdTes
                {
                    documento = GenerateSiafemDocument(document)
                }
            };
        }

        private static SIAFDOC CreateSiafemDocCanNeAdVin(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFCanNeAdVin",
                SiafemDocCanNeAdVin = new SiafemDocCanNeAdVin
                {
                    documento = GenerateSiafemDocument(document)
                }
            };
        }

        private static SIAFDOC CreateSiafemDocCanNeAdVnp(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFCanNeAdVnp",
                SiafemDocCanNeAdVnp = new SiafemDocCanNeAdVnp
                {
                    documento = GenerateSiafemDocument(document)
                }
            };
        }

        private static SIAFDOC CreateSiafemDocCanNePess(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFCanNePess",
                SiafemDocCanNePess = new SiafemDocCanNePess
                {
                    documento = GenerateSiafemDocument(document)
                }
            };
        }

        private static SIAFDOC CreateSiafemDocCanNeTesBL(Documento document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFCanNeTesBL",
                SiafemDocCanNeTesBL = new SiafemDocCanNeTesBL
                {
                    documento = GenerateSiafemDocument(document)
                }
            };
        }
        #endregion

        #region Siafisico
        //private static SFCODOC CreateSiafisicoDocDescCT(Documento document)
        //{
        //    var qtdConvertida = document.EmpenhoItem.QuantidadeMaterialServico.ConverterQuantidade();

        //    string vlrDesc;
        //    string vlrInt;
        //    if (document.EmpenhoItem.ValorUnitario.ToString().Length < 3)
        //    {
        //        vlrDesc = int.Parse(document.EmpenhoItem.ValorUnitario.ToString()).ToString("D2");
        //        vlrInt = "0";
        //    }
        //    else
        //    {
        //        vlrDesc = document.EmpenhoItem.ValorUnitario.ToString().Substring(document.EmpenhoItem.ValorUnitario.ToString().Length - 2, 2);
        //        vlrInt = document.EmpenhoItem.ValorUnitario.ToString().Substring(0, document.EmpenhoItem.ValorUnitario.ToString().Length - 2);
        //    }

        //    var siafdoc = new SFCODOC
        //    {
        //        cdMsg = "SFCOCTDESC001",
        //        SiafisicoDocDescCT = new SiafisicoDocDescCT
        //        {
        //            documento = new documento
        //            {
        //                UG = document.EmpenhoCancelamento.CodigoUnidadeGestora,
        //                Gestao = document.EmpenhoCancelamento.CodigoGestao,
        //                NumeroCT = document.EmpenhoCancelamento.NumeroCT == null? " ": document.EmpenhoCancelamento.NumeroCT,
        //                Item = document.EmpenhoItem.CodigoItemServico,
        //                UnidadeForn = document.EmpenhoItem.CodigoUnidadeFornecimentoItem,
        //                Validademinimadoprodutonaentregade50porcento = "x",
        //                Validademinimadoprodutonaentregade60porcento = " ",
        //                Validademinimadoprodutonaentregade75porcento = " ",
        //                Validademinimadoprodutonaentregade80porcento = " ",
        //                Validademinimadoprodutonaentregavideedital = " ",
        //                Validademinimadoprodutonaentregaconformelegislacaovigentemedicamentomanipulado = " ",
        //                Quantidade = qtdConvertida.Key,
        //                QuantidadeDec = qtdConvertida.Value,
        //                ValorUnitario = vlrInt.Replace(",",""),
        //                ValorUnitarioDec = vlrDesc,
        //                JustificativaPreco1 = ListaString(70, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0],
        //                JustificativaPreco2 = ListaString(70, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1],
        //                JustificativaValorLancado1 = ListaString(70, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0],
        //                JustificativaValorLancado2 = ListaString(70, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1],
        //            }
        //        }
        //    };
        //    return siafdoc;
        //}

        private static SFCODOC CreateSiafisicoDocCanCTVinc(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCOCanCTVinc",
                SiafisicoDocCanCTVinc = new SiafisicoDocCanCTVinc
                {
                    documento = GenerateSiafisicoDocument(document)
                }
            };
        }

        private static SFCODOC CreateSiafisicoDocCanNeVin(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCOCANNEVINC",
                SiafisicoDocCanNeVin = new SiafisicoDocCanNeVin
                {
                    documento = new documento
                    {
                        Gestao = document.EmpenhoCancelamento.CodigoGestao,
                        NumeroCT = document.EmpenhoCancelamento.NumeroCT,
                        UG = document.EmpenhoCancelamento.CodigoUnidadeGestora
                    },
                    cronograma = new cronograma
                    {
                        repeticao = document.ValorMes.Select(x => new repeticao
                        {
                            mes = x.Descricao,
                            valor = x.ValorMes.ToString(CultureInfo.InvariantCulture)
                        }).ToList()

                    }
                }
            };
        }

        private static SFCODOC CreateSiafisicoDocCanNeBecTes(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCOCanNeBecTes",
                SiafisicoDocCanNeBecTes = new SiafisicoDocCanNeBecTes
                {
                    documento = new documento
                    {
                        Gestao = document.EmpenhoCancelamento.CodigoGestao,
                        NumeroCT = document.EmpenhoCancelamento.NumeroCT,
                        UG = document.EmpenhoCancelamento.CodigoUnidadeGestora
                    }
                }
            };
        }

        private static SFCODOC CreateSiafisicoDocCanNeBecVin(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCOCanNeBecVin",
                SiafisicoDocCanNeBecVin = new SiafisicoDocCanNeBecVin
                {
                    documento = new documento
                    {
                        Gestao = document.EmpenhoCancelamento.CodigoGestao,
                        NumeroCT = document.EmpenhoCancelamento.NumeroCT,
                        UG = document.EmpenhoCancelamento.CodigoUnidadeGestora
                    }
                }
            };
        }
        #endregion
        #endregion

        //private static SFCODOC GerarSfcodocDescricao(Documento document)
        //{
        //    var qtdConvertida = document.EmpenhoItem.QuantidadeMaterialServico.ConverterQuantidade();

        //    string vlrDesc;
        //    string vlrInt;
        //    if (document.EmpenhoItem.ValorUnitario.ToString().Length < 3)
        //    {
        //        vlrDesc = int.Parse(document.EmpenhoItem.ValorUnitario.ToString()).ToString("D2");
        //        vlrInt = "0";
        //    }
        //    else
        //    {
        //        vlrDesc = document.EmpenhoItem.ValorUnitario.ToString().Substring(document.EmpenhoItem.ValorUnitario.ToString().Length - 2, 2);
        //        vlrInt = document.EmpenhoItem.ValorUnitario.ToString().Substring(0, document.EmpenhoItem.ValorUnitario.ToString().Length - 2);
        //    }

        //    var siafdoc = new SFCODOC
        //    {
        //        cdMsg = "SFCOCTDESC001",
        //        SiafisicoDocDescCT = new SiafisicoDocDescCT
        //        {
        //            documento = new documento
        //            {
        //                UG = document.Empenho.CodigoUnidadeGestora,
        //                Gestao = document.Empenho.CodigoGestao,
        //                NumeroCT = document.Empenho.NumeroCT,
        //                Item = document.EmpenhoItem.CodigoItemServico,
        //                UnidadeForn = document.EmpenhoItem.CodigoUnidadeFornecimentoItem,
        //                Validademinimadoprodutonaentregade50porcento = "x",
        //                Validademinimadoprodutonaentregade60porcento = " ",
        //                Validademinimadoprodutonaentregade75porcento = " ",
        //                Validademinimadoprodutonaentregade80porcento = " ",
        //                Validademinimadoprodutonaentregavideedital = " ",
        //                Validademinimadoprodutonaentregaconformelegislacaovigentemedicamentomanipulado = " ",
        //                Quantidade = qtdConvertida.Key,
        //                QuantidadeDec = qtdConvertida.Value,
        //                ValorUnitario = vlrInt.Replace(",", ""),
        //                ValorUnitarioDec = vlrDesc,
        //                JustificativaPreco1 = ListaString(50, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0],
        //                JustificativaPreco2 = ListaString(50, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1],
        //                JustificativaValorLancado1 = ListaString(50, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0],
        //                JustificativaValorLancado2 = ListaString(50, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1],
        //            }
        //        }
        //    };
        //    return siafdoc;
        //}

        private static SIAFDOC GerarSiafdoc(Documento dtoSiafdoc)
        {

            var siafdoc = new SIAFDOC
            {
                cdMsg = "SIAFNE001",
                SiafemDocNE = new SiafemDocNE
                {
                    documento = new documento
                    {
                        DataEmissao = dtoSiafdoc.Empenho.DataEmissao.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.Empenho.CodigoUnidadeGestora,
                        Gestao = dtoSiafdoc.Empenho.CodigoGestao,
                        Processo = dtoSiafdoc.Empenho.NumeroProcesso,
                        Evento = dtoSiafdoc.Empenho.CodigoEvento.ZeroParaNulo(),
                        Credor = dtoSiafdoc.Empenho.NumeroCNPJCPFUGCredor,
                        GestaoCredor = dtoSiafdoc.Empenho.CodigoGestaoCredor,
                        PTRes = dtoSiafdoc.Programa.Ptres,
                        UO = dtoSiafdoc.Empenho.CodigoUO.ZeroParaNulo(),
                        PT = dtoSiafdoc.Programa.Cfp + "0000",
                        Fonte = dtoSiafdoc.Fonte.Codigo,
                        NaturezaDespesa = dtoSiafdoc.Estutura.Natureza + dtoSiafdoc.Empenho.CodigoNaturezaItem,
                        UGO = dtoSiafdoc.Empenho.CodigoUGO.ZeroParaNulo(),
                        Acordo = dtoSiafdoc.Empenho.DescricaoAcordo,
                        Municipio = dtoSiafdoc.Empenho.CodigoMunicipio,
                        Modalidade = dtoSiafdoc.Empenho.ModalidadeId.ZeroParaNulo(),
                        Licitacao = dtoSiafdoc.Empenho.LicitacaoId,
                        ReferenciaLegal = removerAcentos(dtoSiafdoc.Empenho.DescricaoReferenciaLegal),
                        OrigemMaterial = dtoSiafdoc.Empenho.OrigemMaterialId.ZeroParaNulo(),
                        Valor = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(CultureInfo.InvariantCulture),
                        LocalEntrega = dtoSiafdoc.Empenho.DescricaoLocalEntregaSiafem,
                        DataEntrega = dtoSiafdoc.Empenho.DataEntregaMaterial == default(DateTime) ? null : dtoSiafdoc.Empenho.DataEntregaMaterial.ToString("ddMMMyyyy").ToUpper(),
                        TipoEmpenho = dtoSiafdoc.Empenho.EmpenhoTipoId.ZeroParaNulo(),
                        TipoObra = dtoSiafdoc.Empenho.TipoObraId.ZeroParaNulo(),
                        UGObra = dtoSiafdoc.Empenho.CodigoUGObra.ZeroParaNulo(),
                        AnoContrato = dtoSiafdoc.Empenho.NumeroAnoContrato.ZeroParaNulo(),
                        MesContrato = dtoSiafdoc.Empenho.NumeroMesContrato != 0 ? dtoSiafdoc.Empenho.NumeroMesContrato.ToString("D2") : null,
                        NumeroObra = dtoSiafdoc.Empenho.NumeroObra
                    },
                    cronograma = new cronograma
                    {
                        Repeticao = new Repeticao
                        {
                            desc = dtoSiafdoc.ValorMes.Select(x => new desc
                            {
                                Mes = x.Descricao,
                                Valor = x.ValorMes.ToString(CultureInfo.InvariantCulture)
                            }).ToList()
                        }
                    },
                    descricao = new descricao
                    {
                        Repeticao = new Repeticao
                        {
                            des = dtoSiafdoc.EmpenhoItens.Select(x => new des
                            {
                                UnidadeMedida = x.DescricaoUnidadeMedida,
                                Quantidade = "1",
                                PrecoUnitario = x.ValorTotal.ZeroParaNulo(),
                                PrecoTotal = x.ValorTotal.ZeroParaNulo(),
                                DescricaoParte1 = x.DescricaoItemSiafem?.Replace(";", ""),
                                DescricaoParte2 = " ",
                                DescricaoParte3 = " "

                            }).ToList()
                        }
                    }
                }
            };
            return siafdoc;
        }


        private static SIAFDOC GerarSiafdocReforco(Documento dtoSiafdoc)
        {
            var descricaoItemSiafem = dtoSiafdoc.EmpenhoItens.FirstOrDefault().DescricaoItemSiafem?.Split(';').ToList();

            while (descricaoItemSiafem != null && descricaoItemSiafem.Count < 3)
            {
                descricaoItemSiafem.Add(string.Empty);
            }

            var siafdoc = new SIAFDOC
            {
                cdMsg = "SIAFRENE001",
                SiafemDocRENE = new SiafemDocRENE
                {

                    documento = new documento
                    {
                        DataEmissao = dtoSiafdoc.EmpenhoReforco.DataEmissao.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.EmpenhoReforco.CodigoUnidadeGestora,
                        Gestao = dtoSiafdoc.EmpenhoReforco.CodigoGestao,
                        Processo = dtoSiafdoc.EmpenhoReforco.NumeroProcesso,
                        Evento = dtoSiafdoc.EmpenhoReforco.CodigoEvento.ZeroParaNulo(),
                        Credor = dtoSiafdoc.EmpenhoReforco.NumeroCNPJCPFUGCredor,
                        GestaoCredor = dtoSiafdoc.EmpenhoReforco.CodigoGestaoCredor,
                        EmpenhoOriginal = dtoSiafdoc.EmpenhoReforco.CodigoEmpenhoOriginal?.Substring(6, 5),
                        Valor = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(CultureInfo.InvariantCulture),
                        NaturezaDespesa = dtoSiafdoc.Estutura.Natureza + dtoSiafdoc.EmpenhoReforco.CodigoNaturezaItem,
                        Municipio = dtoSiafdoc.EmpenhoReforco.CodigoMunicipio,
                        Acordo = dtoSiafdoc.EmpenhoReforco.DescricaoAcordo,
                        LocalEntrega = dtoSiafdoc.EmpenhoReforco.DescricaoLocalEntregaSiafem,

                    },
                    cronograma = new cronograma
                    {
                        Repeticao = new Repeticao
                        {
                            desc = dtoSiafdoc.ValorMes.Select(x => new desc
                            {
                                Mes = x.Descricao,
                                Valor = x.ValorMes.ToString(CultureInfo.InvariantCulture)
                            }).ToList()
                        }
                    },
                    descricao = new descricao
                    {
                        Repeticao = new Repeticao
                        {
                            des = dtoSiafdoc.EmpenhoItens.Select(x => new des
                            {
                                UnidadeMedida = x.DescricaoUnidadeMedida,
                                Quantidade = "1",
                                PrecoUnitario = x.ValorTotal.ZeroParaNulo(),
                                PrecoTotal = x.ValorTotal.ZeroParaNulo(),
                                DescricaoParte1 = x.DescricaoItemSiafem?.Replace(";", ""),
                                DescricaoParte2 = " ",
                                DescricaoParte3 = " "
                            }).ToList()
                        }
                    }
                }
            };
            return siafdoc;
        }


        private static SIAFDOC GerarSiafdocReforcoDescricao(Documento dtoSiafdoc)
        {
            var siafdoc = new SIAFDOC
            {
                cdMsg = "SIAFIncDescNE001",
                SiafemDocIncDescNE = new SiafemDocIncDescNE
                {

                    documento = new documento
                    {
                        UnidadeGestora = dtoSiafdoc.EmpenhoReforco.CodigoUnidadeGestora,
                        Gestao = dtoSiafdoc.EmpenhoReforco.CodigoGestao,
                        NumeroEmpenho = dtoSiafdoc.EmpenhoReforco.NumeroEmpenhoSiafem
                    },
                    descricao = new descricao
                    {
                        Repeticao = new Repeticao
                        {
                            des = dtoSiafdoc.EmpenhoItens.Select(x => new des
                            {
                                UnidadeMedida = x.DescricaoUnidadeMedida,
                                Quantidade = x.QuantidadeMaterialServico.ZeroParaNulo(),
                                PrecoUnitario = x.ValorUnitario.ZeroParaNulo(),
                                PrecoTotal = x.ValorTotal.ZeroParaNulo(),
                                DescricaoParte1 = x.DescricaoItemSiafem?.Replace(";", ""),
                                DescricaoParte2 = " ",
                                DescricaoParte3 = " "

                            }).ToList()
                        }
                    }
                }
            };
            return siafdoc;
        }


        private static SFCODOC GerarSfcodocInclusao(Documento dtoSiafdoc)
        {
            //var listObs = dtoSiafdoc.Model..Split(';').ToList();

            var siafdoc = new SFCODOC
            {
                cdMsg = "SFCOCT001",
                SiafisicoDocCT = new SiafisicoDocCT
                {
                    documento = new documento
                    {
                        UG = dtoSiafdoc.Empenho.CodigoUnidadeGestora,
                        Gestao = dtoSiafdoc.Empenho.CodigoGestao,
                        CnpjCpfFornecedor = dtoSiafdoc.Empenho.NumeroCNPJCPFUGCredor,
                        UGFornecedora = dtoSiafdoc.Empenho.NumeroCNPJCPFUGCredor == null ? dtoSiafdoc.Empenho.CodigoUnidadeGestoraFornecedora : null,
                        GestaoFornecedora = dtoSiafdoc.Empenho.NumeroCNPJCPFUGCredor == null ? dtoSiafdoc.Empenho.CodigoGestaoFornecedora : null,
                        Evento = dtoSiafdoc.Empenho.CodigoEvento.ZeroParaNulo(),
                        DataEmissao = dtoSiafdoc.Empenho.DataEmissao.ToString("ddMMyyyy"),
                        Fonte = dtoSiafdoc.Fonte.Codigo,
                        PTRES = dtoSiafdoc.Programa.Ptres,
                        Natureza = $"{dtoSiafdoc.Estutura.Natureza}{dtoSiafdoc.Empenho.CodigoNaturezaItem}",
                        Municipio = dtoSiafdoc.Empenho.CodigoMunicipio,
                        UGO = dtoSiafdoc.Empenho.CodigoUGO.ZeroParaNulo(),
                        DataEntrega = dtoSiafdoc.Empenho.DataEntregaMaterial == default(DateTime) ? null : dtoSiafdoc.Empenho.DataEntregaMaterial.ToString("ddMMyyyy").ToUpper(),
                        //dtoSiafdoc.Empenho.DataEntregaMaterial.ToString("ddMMyyyy"),
                        //LocalEntrega = dtoSiafdoc.Empenho.DescricaoLocalEntregaSiafem,
                        TipoAquisicao = dtoSiafdoc.Empenho.TipoAquisicaoId.ZeroParaNulo(),
                        OrigemMaterial = dtoSiafdoc.Empenho.OrigemMaterialId.ZeroParaNulo(),
                        Modalidade = dtoSiafdoc.Empenho.ModalidadeId.ZeroParaNulo(),
                        TipoLicitacao = dtoSiafdoc.Empenho.LicitacaoId,
                        RefLegal = dtoSiafdoc.Empenho.DescricaoReferenciaLegal,
                        //UnidadeGestora = dtoSiafdoc.Empenho.CodigoUnidadeGestora,
                        NumeroProcesso = dtoSiafdoc.Empenho.NumeroProcesso,
                        NumeroContratoFornec = dtoSiafdoc.Empenho.NumeroContratoFornecedor,
                        NumeroEdital = dtoSiafdoc.Empenho.NumeroEdital,
                        ValorEmpenhar = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(CultureInfo.InvariantCulture)
                    },
                    LocalEntrega = new LocalEntrega
                    {
                        Logradouro = dtoSiafdoc.Empenho.DescricaoLogradouroEntrega,
                        Bairro = dtoSiafdoc.Empenho.DescricaoBairroEntrega,
                        Cidade = dtoSiafdoc.Empenho.DescricaoCidadeEntrega,
                        CEP = dtoSiafdoc.Empenho.NumeroCEPEntrega,
                        InformacoesAdicionais = ListaString(210, dtoSiafdoc.Empenho.DescricaoInformacoesAdicionaisEntrega, 1)[0]
                    },
                    cronograma = new cronograma
                    {
                        Repeticao = new Repeticao
                        {
                            desc = dtoSiafdoc.ValorMes.Select(x => new desc
                            {
                                Mes = x.Descricao,
                                Valor = x.ValorMes.ToString(CultureInfo.InvariantCulture)
                            }).ToList()
                        }
                    }
                }
            };
            return siafdoc;
        }

        private static SFCODOC GerarSfcodocInclusaoSiafisico(Documento dtoSiafdoc)
        {
            var evento = dtoSiafdoc.EmpenhoReforco.CodigoEvento.ToString();

            string evento52 = string.Empty;
            string evento92 = string.Empty;

            if (!string.IsNullOrEmpty(evento))
            {
                evento52 = evento.Substring(evento.Length - 2, 2) == "52" ? "X" : " ";
                evento92 = evento.Substring(evento.Length - 2, 2) == "92" ? "X" : " ";
            }

            var cnjpUg = dtoSiafdoc.EmpenhoReforco.NumeroCNPJCPFUGCredor;

            var siafdoc = new SFCODOC
            {
                cdMsg = "SFCOIncContRE",
                SiafisicoDocIncContRE = new SiafisicoDocIncContRE
                {
                    documento = new documento
                    {
                        DataEmissao = dtoSiafdoc.EmpenhoReforco.DataEmissao.ToString("ddMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.EmpenhoReforco.CodigoUnidadeGestora,
                        Gestao = dtoSiafdoc.EmpenhoReforco.CodigoGestao,
                        CnpjCpf = cnjpUg.Length > 6 ? cnjpUg : " ",
                        UGFornecedor = cnjpUg.Length <= 6 ? cnjpUg : null,
                        GestaoFornecedor = cnjpUg.Length <= 6 ? dtoSiafdoc.EmpenhoReforco.CodigoGestaoCredor : null,
                        Contrato = dtoSiafdoc.EmpenhoReforco.NumeroOriginalCT?.Substring(6, 5),
                        Evento52 = evento52,
                        Evento92 = evento92,
                        Valor = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(),
                    }
                }
            };

            var propriedades = siafdoc.SiafisicoDocIncContRE.documento.GetType().GetProperties().ToList();

            var valorMes = dtoSiafdoc.ValorMes.ToList();

            for (int i = 0; i < valorMes.Count; i++)
            {
                propriedades.FirstOrDefault(x => x.Name == "Mes" + (i + 1).ToString("D2"))
                    .SetValue(siafdoc.SiafisicoDocIncContRE.documento, valorMes[i].Descricao);

                propriedades.FirstOrDefault(x => x.Name == "Valor" + (i + 1).ToString("D2"))
                    .SetValue(siafdoc.SiafisicoDocIncContRE.documento, valorMes[i].ValorMes.ToString());
            }
            return siafdoc;
        }


        private static SFCODOC GerarSfcodocAlteraçao(Documento dtoSiafdoc)
        {
            //var listObs = dtoSiafdoc.Model..Split(';').ToList();

            var siafdoc = new SFCODOC
            {
                cdMsg = "SFCODocAltContNe",
                SiafisicoDocAltContNe = new SiafisicoDocAltContNe
                {
                    documento = new documento
                    {
                        UnidadeGestora = dtoSiafdoc.Empenho.CodigoUnidadeGestora,
                        Gestao = dtoSiafdoc.Empenho.CodigoGestao,
                        CodigoMunicipio = dtoSiafdoc.Empenho.CodigoMunicipio,
                        CPFCredor = dtoSiafdoc.Empenho.NumeroCNPJCPFUGCredor ?? $"{dtoSiafdoc.Empenho.CodigoUnidadeGestoraFornecedora}{dtoSiafdoc.Empenho.CodigoGestaoFornecedora}",
                        DataPrevista = dtoSiafdoc.Empenho.DataEntregaMaterial == default(DateTime) ? null : dtoSiafdoc.Empenho.DataEntregaMaterial.ToString("ddMMyyyy").ToUpper(),
                        //dtoSiafdoc.Empenho.DataEntregaMaterial.ToString("ddMMyyyy").ToUpper(),
                        Fonte = dtoSiafdoc.Fonte.Codigo,
                        LocalEntrega = dtoSiafdoc.Empenho.DescricaoLogradouroEntrega,
                        Modalidade = dtoSiafdoc.Empenho.ModalidadeId.ZeroParaNulo(),
                        NaturezaDespsesa = dtoSiafdoc.Estutura.Natureza + dtoSiafdoc.Empenho.CodigoNaturezaItem,
                        NumEdital = dtoSiafdoc.Empenho.NumeroEdital,
                        NumeroProcesso = dtoSiafdoc.Empenho.NumeroProcesso,
                        Origem = dtoSiafdoc.Empenho.OrigemMaterialId.ZeroParaNulo(),
                        PTRes = dtoSiafdoc.Programa.Ptres,
                        RefLegal = dtoSiafdoc.Empenho.DescricaoReferenciaLegal,
                        Tipo = dtoSiafdoc.Empenho.TipoAquisicaoId.ZeroParaNulo(),
                        TipoAquisicao = dtoSiafdoc.Empenho.LicitacaoId,
                        UGO = dtoSiafdoc.Empenho.CodigoUGO.ZeroParaNulo(),
                        CEP = dtoSiafdoc.Empenho.NumeroCEPEntrega,
                        Bairro = dtoSiafdoc.Empenho.DescricaoBairroEntrega,
                        Cidade = dtoSiafdoc.Empenho.DescricaoCidadeEntrega,
                        InformacoesAdicion = dtoSiafdoc.Empenho.DescricaoInformacoesAdicionaisEntrega,
                        NumeroCT = dtoSiafdoc.Empenho.NumeroCT?.Substring(6, 5),
                        DataEmissao = dtoSiafdoc.Empenho.DataEmissao.ToString("ddMMyyyy").ToUpper(),
                        Valor = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(),
                    }
                }
            };

            var propriedades = siafdoc.SiafisicoDocAltContNe.documento.GetType().GetProperties().ToList();

            var valorMes = dtoSiafdoc.ValorMes.ToList();

            for (int i = 0; i < valorMes.Count; i++)
            {
                propriedades.FirstOrDefault(x => x.Name == "Mes" + (i + 1).ToString("D2"))
                    .SetValue(siafdoc.SiafisicoDocAltContNe.documento, valorMes[i].Descricao);

                propriedades.FirstOrDefault(x => x.Name == "Valor" + (i + 1).ToString("D2"))
                    .SetValue(siafdoc.SiafisicoDocAltContNe.documento, valorMes[i].ValorMes.ToString());

                propriedades.FirstOrDefault(x => x.Name == "ValorEmpenhar" + (i + 1).ToString())
                    .SetValue(siafdoc.SiafisicoDocAltContNe.documento, valorMes[i].ValorMes.ToString());
            }

            return siafdoc;
        }


        private static SFCODOC GerarSfcodocAlteracaoItem(Documento document)
        {
            var qtdConvertida = document.EmpenhoItem.QuantidadeMaterialServico.ConverterQuantidade();


            string vlrDesc;
            string vlrInt;
            if (document.EmpenhoItem.ValorUnitario.ToString().Length < 3)
            {
                vlrDesc = ((int)document.EmpenhoItem.ValorUnitario).ToString("D2");
                vlrInt = "0";
            }
            else
            {
                vlrDesc = document.EmpenhoItem.ValorUnitario.ToString().Substring(document.EmpenhoItem.ValorUnitario.ToString().Length - 2, 2);
                vlrInt = document.EmpenhoItem.ValorUnitario.ToString().Substring(0, document.EmpenhoItem.ValorUnitario.ToString().Length - 2);
            }

            var JustificativaPreco1 = ListaString(50, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0];
            var JustificativaPreco2 = ListaString(50, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1];

            vlrDesc = vlrDesc == "00" ? " " : vlrDesc;


            var siafdoc = new SFCODOC
            {
                cdMsg = "SFCODocAltDescCT",
                SiafisicoDocAltDescCT = new SiafisicoDocAltDescCT
                {
                    documento = new documento
                    {
                        UG = document.Empenho.CodigoUnidadeGestora,
                        Gestao = document.Empenho.CodigoGestao,
                        NumeroCT = document.Empenho.NumeroCT,
                        Seq = document.EmpenhoItem.SequenciaItem.ToString("D3"),
                        Item = document.EmpenhoItem.CodigoItemServico,
                        UnidadeFornecimento = Convert.ToInt32(document.EmpenhoItem.CodigoUnidadeFornecimentoItem).ToString("D5"),
                        Quantidade = qtdConvertida.Key,
                        QuantidadeDec = qtdConvertida.Value,
                        ValorParteInteira = vlrInt.Replace(",", ""),
                        ValorParteDecimal = vlrDesc,
                        ConfirmaPreco = "S",
                        JustificativaPreco1 = JustificativaPreco1,
                        JustificativaPreco2 = string.IsNullOrWhiteSpace(JustificativaPreco2) ? "." : JustificativaPreco2,
                        //JustificativaItemNao = ListaString(50, dtoSfcodoc.EmpenhoItem.DescricaoJustificativaPreco, 2)[0]
                    }
                }
            };
            return siafdoc;
        }

        private T ConvertNode<T>(XmlNodeList node) where T : class
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node[node.Count - 1].OuterXml);
            stw.Flush();

            stm.Position = 0;


            XmlSerializer ser = new XmlSerializer(typeof(T));
            T result = ser.Deserialize(stm) as T;

            return result;
        }


        private XmlDocument ConverterXml(string xml)
        {
            try
            {
                xml = xml.Replace("&", "&amp;");

                var document = new XmlDocument();

                document.LoadXml(xml);

                //document.LoadXml(xml.Replace("001 <", "001 "));

                return document;
            }
            catch
            {
                throw new SiafemException(xml);
            }
        }


        private static string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç<>";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc  ";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }


        #endregion

        #region Centralização de Empenho

        public string TransmitirEmpenho<T>(EnumTipoServicoFazenda servico, EnumAcaoSiaf acao, string login, string senha, T objModel, IEnumerable<IMes> meses, IEnumerable<IEmpenhoItem> itens, string unidadeGestora) where T : BaseEmpenho
        {
            string msg = string.Empty;
            switch (servico)
            {
                case EnumTipoServicoFazenda.Siafisico:
                    msg = TransmitirEmpenhoSiafisico(acao, login, senha, objModel, meses, itens, unidadeGestora);
                    break;
                case EnumTipoServicoFazenda.Siafem:
                    break;
            }

            return msg;
        }
        public string TransmitirEmpenhoSiafisico<T>(EnumAcaoSiaf acao, string login, string senha, T objModel, IEnumerable<IMes> mes, IEnumerable<IEmpenhoItem> itens, string unidadeGestora) where T : BaseEmpenho
        {
            try
            {
                Documento dtoSiafdoc;

                dtoSiafdoc = GerarDocumentoEmpenho(objModel, mes, itens);

                var siafdoc = GerarSfcodocInclusao(dtoSiafdoc);
                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagemErro = xm.GetElementsByTagName("MsgErro");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;
                var numCt = xm.GetElementsByTagName("NumeroCT");

                // se for reforço e !messagemErro[0].InnerText.Contains("CONTRATO REFORCADO COM SUCESSO TECLE ENTER PARA RETORNAR") deve ser sucesso

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException("SIAFISICO - " + messagemErro[0].InnerText);

                objModel.NumeroCT = numCt.Count > 0 ? numCt[0].InnerText : objModel.NumeroCT;
                return objModel.NumeroCT;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }


        public int TransmitirEmpenhoItem<TEmpenho, TItem>(EnumTipoServicoFazenda servico, EnumAcaoSiaf acao, string login, string senha, TEmpenho objModel, TItem item, string unidadeGestora)
            where TEmpenho : BaseEmpenho
            where TItem : BaseEmpenhoItem
        {
            int seq = 0;

            switch (servico)
            {
                case EnumTipoServicoFazenda.Siafisico:
                    seq = TransmitirEmpenhoItemSiafisico(acao, login, senha, objModel, item, unidadeGestora);
                    break;
                case EnumTipoServicoFazenda.Siafem:
                    break;
            }

            return seq;
        }
        public int TransmitirEmpenhoItemSiafisico<TEmpenho>(EnumAcaoSiaf acao, string login, string senha, TEmpenho objModel, IEmpenhoItem item, string unidadeGestora) where TEmpenho : BaseEmpenho
        {
            try
            {
                Documento dtoSiafdoc;

                dtoSiafdoc = GerarDocumentoEmpenhoItem(objModel, item);
                var siafdoc = GerarSiafisicoCtDescricao(dtoSiafdoc, acao, EnumTipoOperacaoEmpenho.Empenho);

                var result = _siafemService.InserirEmpenhoSiafisico(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagemErro = xm.GetElementsByTagName("MsgErro");
                messagemErro = messagemErro.Count == 0 ? xm.GetElementsByTagName("MsgRetorno") : messagemErro;
                var numCt = xm.GetElementsByTagName("NumeroCT");

                // se for reforço e !messagemErro[0].InnerText.Contains("CONTRATO REFORCADO COM SUCESSO TECLE ENTER PARA RETORNAR") deve ser sucesso


                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new SiafisicoException("SIAFISICO - " + messagemErro[0].InnerText);

                objModel.NumeroCT = numCt.Count > 0 ? numCt[0].InnerText : objModel.NumeroCT;

                return Convert.ToInt32(xm.GetElementsByTagName("NumSeq")[0].InnerText);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }


        private Documento GerarDocumentoEmpenho<TEmpenho>(TEmpenho objModel, IEnumerable<IMes> mes, IEnumerable<IEmpenhoItem> itens) where TEmpenho : BaseEmpenho
        {
            Documento doc = null;
            Type type = typeof(TEmpenho);


            if (type == typeof(Empenho))
            {
                doc = new Documento
                {
                    Empenho = objModel as Empenho,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes,
                    EmpenhoItens = itens
                };

            }
            else if (type == typeof(EmpenhoReforco))
            {
                doc = new Documento
                {
                    EmpenhoReforco = objModel as EmpenhoReforco,
                    ValorMes = mes
                };
            }
            else if (type == typeof(EmpenhoCancelamento))
            {
                doc = new Documento
                {
                    EmpenhoCancelamento = objModel as EmpenhoCancelamento,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = objModel.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.NaturezaId }).FirstOrDefault(),
                    ValorMes = mes
                };
            }

            return doc;
        }
        private Documento GerarDocumentoEmpenhoItem<TEmpenho>(TEmpenho objModel, IEmpenhoItem item) where TEmpenho : BaseEmpenho
        {
            Documento doc = null;
            Type type = typeof(TEmpenho);


            if (type == typeof(Empenho))
            {
                doc = new Documento
                {
                    Empenho = objModel as Empenho,
                    EmpenhoItem = item as EmpenhoItem
                };
            }
            else if (type == typeof(EmpenhoReforco))
            {
                doc = new Documento
                {
                    Empenho = new Empenho
                    {
                        CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                        CodigoGestao = objModel.CodigoGestao,
                        NumeroCT = objModel.NumeroCT
                    },
                    EmpenhoItem = item as EmpenhoReforcoItem
                };
            }
            else if (type == typeof(EmpenhoCancelamento))
            {
                doc = new Documento
                {
                    Empenho = new Empenho
                    {
                        CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                        CodigoGestao = objModel.CodigoGestao,
                        NumeroCT = objModel.NumeroCT
                    },
                    EmpenhoItem = item as EmpenhoCancelamentoItem
                };
            }

            return doc;
        }
        #endregion
    }
}
