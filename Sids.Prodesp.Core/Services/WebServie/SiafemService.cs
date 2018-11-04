using System.Web;

namespace Sids.Prodesp.Core.Services.WebServie
{
    using Base;
    using Interface.Interface.Configuracao;
    using Interface.Interface.Log;
    using Interface.Interface.Reserva;
    using Interface.Interface.Service;
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Interface;
    using Model.ValueObject.Service.Reserva.Siafem;
    using Model.ValueObject.Service.Reserva.Siafem.Base;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using login = Model.ValueObject.Service.Login;

    public class SiafemService : BaseService
    {
        #region Propriedades

        private ILogError _logError;
        private readonly ISiafem _siafemService;
        private readonly ICrudPrograma _programa;
        private readonly ICrudFonte _fonte;
        private readonly ICrudEstrutura _estutura;

        #endregion

        #region Construtores

        public SiafemService(
            ILogError logError, ISiafem siafemService, ICrudPrograma programa, ICrudFonte fonte,
            ICrudEstrutura estutura) : base(logError)
        {
            _estutura = estutura;
            _fonte = fonte;
            _logError = logError;
            _siafemService = siafemService;
            _programa = programa;
        }

        #endregion

        #region Metodos Publicos
        public string Login(string login, string senha, string unidadeGestora)
        {
            try
            {
                var siafdoc = new login.SIAFDOC
                {
                    cdMsg = "SIAFLOGIN001",
                    SiafemLogin = new login.SiafemLogin
                    {
                        login = new login.login
                        {
                            Codigo = login,
                            Senha = senha,
                            Ano = DateTime.Now.Year.ToString(),
                            CICSS = "false"
                        }
                    }
                };

                var result = _siafemService.Login(login, (senha), unidadeGestora, siafdoc);
                var xm = ConverterXml(result);

                var mensagem = xm.GetElementsByTagName("StatusOperacao")[0].FirstChild.Value;

                if (!bool.Parse(mensagem))
                    mensagem = xm.GetElementsByTagName("MsgErro")[0].FirstChild.Value;

                return mensagem;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }

        public bool AlterarSenha(string login, string senha, string novaSenha, string unidadeGestora)
        {
            try
            {
                var caract = novaSenha.Substring(novaSenha.Length - 1, 1).ToUpper() == "X" ? "y" : "x";

                var siafdoc = new login.SIAFDOC
                {
                    cdMsg = "SIAFTrocaSenha",
                    SiafemLogin = new login.SiafemLogin
                    {
                        login = new login.login
                        {
                            Codigo = login,
                            Senha = senha,
                            NovaSenha = senha == novaSenha ? novaSenha.Substring(0, novaSenha.Length - 1) + caract : novaSenha,
                            ManterSenha = senha == novaSenha ? "s" : " ",
                            Ano = DateTime.Now.Year.ToString(),
                            CICSS = "false"
                        }
                    }
                };

                var result = _siafemService.AlterarSenha(login, senha, unidadeGestora, siafdoc);
                var xm = ConverterXml(result);

                var root = xm.GetElementsByTagName("StatusOperacao")[0].FirstChild.Value;

                if (!bool.Parse(root))
                    throw new Exception("Retorno SIAFEM: " + xm.GetElementsByTagName("MsgErro")[0].FirstChild.Value);

                return bool.Parse(root);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }

        public string InserirReservaSiafem(string login, string senha, IReserva reserva, List<IMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DtoSiafdoc
                {
                    Reserva = reserva,
                    Programa = _programa.Buscar(new Programa { Codigo = (int)reserva.Programa }).FirstOrDefault(),
                    Fonte = _fonte.Buscar(new Fonte { Id = (int)reserva.Fonte }).FirstOrDefault(),
                    Estutura = _estutura.Buscar(new Estrutura { Codigo = (int)reserva.Estrutura }).FirstOrDefault(),
                    ValorMes = mes
                };

                var siafdoc = GerarSiafdoc(dtoSiafdoc);
                var result = _siafemService.InserirReservaSiafem(login, senha, unidadeGestora, siafdoc);
                var root = true.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");
                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNr = xm.GetElementsByTagName("NumeroNR");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                    throw new Exception("SIAFEM - " + messagemErro[0].InnerText);

                return numeroNr[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }

        public string InserirReservaSiafisico(string login, string senha, IReserva reserva, List<IMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DtoSiafdoc
                {
                    Reserva = reserva,
                    ValorMes = mes
                };

                var siafdoc = GerarSfcodoc(dtoSiafdoc);

                var result = _siafemService.InserirReservaSiafisico(login, senha, unidadeGestora, siafdoc);
                string root = false.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgErro");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new Exception("SIAFISICO - " + messagem[0].FirstChild.Value);
                }

                return xm.GetElementsByTagName("NumeroNR")[0].FirstChild.Value;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }

        public string InserirReservaCancelamento(string login, string senha, IReserva cancelamento, List<ReservaCancelamentoMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DtoSiafdoc
                {
                    Reserva = cancelamento,
                    Programa = _programa.Buscar(new Programa { Codigo = (int)cancelamento.Programa }).FirstOrDefault(),
                    Fonte = _fonte.Buscar(new Fonte { Id = (int)cancelamento.Fonte }).FirstOrDefault(),
                    Estutura = _estutura.Buscar(new Estrutura { Codigo = (int)cancelamento.Estrutura }).FirstOrDefault(),
                    ValorMes = mes.Cast<IMes>()
                };


                var siafdoc = GerarSiafemDocCanNR(dtoSiafdoc);

                var result = _siafemService.InserirCancelamentoReserva(login, senha, unidadeGestora, siafdoc);
                string root = true.ToString();
                var xm = ConverterXml(result);


                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagemErro = xm.GetElementsByTagName("MsgErro");
                var numeroNr = xm.GetElementsByTagName("NumeroNR");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagemErro.Count > 0 && messagemErro[0].InnerText != "")
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new Exception("SIAFEM - " + messagemErro[0].InnerText);
                }

                return numeroNr[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new Exception(e.Message);
            }
        }

        public ConsultaOc ConsultaOC(string login, string senha, string oc, string unidadegestora, string gestao)
        {
            try
            {

                var siafdoc = new SFCODOC
                {
                    cdMsg = "SFCOConsultaOC",
                    SiafisicoConsultaOC = new SiafisicoConsultaOC
                    {
                        documento = new documento
                        {
                            UnidadeGestora = unidadegestora,
                            Gestao = gestao,
                            PrefixoOC = oc.Substring(0, 6),
                            NumOC = oc.Substring(6, 5)
                        }
                    }
                };

                var result = _siafemService.ConsultaOC(login, senha, unidadegestora, siafdoc);
                string root = false.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgErro");

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new Exception("SIAFISICO - " + messagem[0].FirstChild.Value);
                }

                return new ConsultaOc
                {
                    Natureza = xm.GetElementsByTagName("cd_Natureza")[0].FirstChild.Value,
                    Fonte = xm.GetElementsByTagName("cd_Fonte")[0].FirstChild.Value,
                    Processo = xm.GetElementsByTagName("cd_NumeroProcesso")[0].FirstChild.Value,
                    Ptres = xm.GetElementsByTagName("cd_Ptres")[0].FirstChild.Value
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ConsultaNr ConsultaNr(string login, string senha, IReserva reserva, string unidadeGestora)
        {
            try
            {
                var siafdoc = new SIAFDOC
                {
                    cdMsg = "SIAFConsultaNR",
                    SiafemDocConsultaNR = new SiafemDocConsultaNR
                    {
                        documento = new documento
                        {
                            UnidadeGestora = reserva.Ugo,
                            Gestao = reserva.Uo,
                            NumeroNR = reserva.NumSiafemSiafisico
                        }
                    }
                };

                var result = _siafemService.ConsultaNr(login, (senha), unidadeGestora, siafdoc);

                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgRetorno");
                messagem = messagem.Count == 0 ? xm.GetElementsByTagName("MsgErro") : messagem;
                string root = "";
                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new Exception("SIAFEM - " + messagem[0].FirstChild.Value);
                }

                var document = xm.GetElementsByTagName("documento");


                ConsultaNr resultingMessage = ConvertNode(document);


                return resultingMessage;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Metodos Privados
        private static SIAFDOC GerarSiafdoc(DtoSiafdoc dtoSiafdoc)
        {
            var listObs = dtoSiafdoc.Reserva.Observacao.Split(';').ToList();

            var siafdoc = new SIAFDOC
            {
                cdMsg = "SIAFNR001",
                SiafemDocNR = new SiafemDocNR
                {
                    documento = new documento
                    {
                        DataEmissao = dtoSiafdoc.Reserva.DataEmissao.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.Reserva.Ugo,
                        Gestao = dtoSiafdoc.Reserva.Uo,
                        PTRes = dtoSiafdoc.Programa.Ptres.ToString(),
                        Processo = dtoSiafdoc.Reserva.Processo,
                        UO = dtoSiafdoc.Reserva.Uo,
                        PT = dtoSiafdoc.Programa.Cfp + "0000",
                        Fonte = dtoSiafdoc.Fonte.Codigo,
                        NaturezaDespesa = dtoSiafdoc.Estutura.Natureza.ToString(),
                        UGO = dtoSiafdoc.Reserva.Ugo,
                        PlanoInterno = "",
                        Valor = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(CultureInfo.InvariantCulture)
                    },
                    cronograma = new cronograma
                    {
                        Repeticao = new Repeticao
                        {
                            desc = dtoSiafdoc.ValorMes.Select(x => new desc
                            {
                                Mes = x.Descricao,
                                Valor = x.ValorMes.ToString(CultureInfo.InvariantCulture)
                            }).ToList(),
                        }
                    },
                    observacao = new observacao
                    {
                        Repeticao = new Repeticao
                        {
                            obs = listObs.Select(x => new obs
                            {
                                Observacao = removerAcentos(x.ToString())
                            }).ToList()
                        }
                    }
                }
            };
            return siafdoc;
        }

        private static SFCODOC GerarSfcodoc(DtoSiafdoc dtoSiafdoc)
        {
            var siafdoc = new SFCODOC
            {
                cdMsg = "SFCONRPregao",
                SiafisicoDocNRPregao = new SiafisicoDocNRPregao
                {
                    documento = new documento
                    {
                        DataEmissao = dtoSiafdoc.Reserva.DataEmissao.ToString("ddMMMyyyy").ToUpper(),
                        Gestao = dtoSiafdoc.Reserva.Uo,
                        NumOC = dtoSiafdoc.Reserva.Oc.Substring(6, 5),
                        UG = dtoSiafdoc.Reserva.Ugo
                    }
                }
            };

            var listObs = dtoSiafdoc.Reserva.Observacao.Split(';');

            if (listObs.Length > 0)
                siafdoc.SiafisicoDocNRPregao.documento.Observacao1 = removerAcentos(listObs[0]);

            if (listObs.Length > 1)
                siafdoc.SiafisicoDocNRPregao.documento.Observacao2 = removerAcentos(listObs[1]);

            if (listObs.Length > 2)
                siafdoc.SiafisicoDocNRPregao.documento.Observacao3 = removerAcentos(listObs[2]);

            var propriedades = siafdoc.SiafisicoDocNRPregao.documento.GetType().GetProperties().ToList();

            var valorMes = dtoSiafdoc.ValorMes.ToList();
            for (int i = 0; i < valorMes.Count; i++)
            {
                propriedades.FirstOrDefault(x => x.Name == "Mes" + (i + 1).ToString())
                    .SetValue(siafdoc.SiafisicoDocNRPregao.documento, valorMes[i].Descricao);
                propriedades.FirstOrDefault(x => x.Name == "Valor" + (i + 1).ToString())
                    .SetValue(siafdoc.SiafisicoDocNRPregao.documento, valorMes[i].ValorMes.ToString());
            }

            return siafdoc;
        }

        private static SIAFDOC GerarSiafemDocCanNR(DtoSiafdoc dtoSiafdoc)
        {
            var listObs = dtoSiafdoc.Reserva.Observacao.Split(';').ToList();

            var SIAFDOC = new SIAFDOC
            {
                cdMsg = "SIAFCanNR",
                SiafemDocCanNR = new SiafemDocCanNR
                {
                    documento = new documento
                    {
                        DataEmissao = dtoSiafdoc.Reserva.DataEmissao.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.Reserva.Ugo,
                        Gestao = dtoSiafdoc.Reserva.Uo,
                        PTRes = dtoSiafdoc.Programa.Ptres.ToString(),
                        Processo = dtoSiafdoc.Reserva.Processo,
                        UO = dtoSiafdoc.Reserva.Uo,
                        PT = dtoSiafdoc.Programa.Cfp + "0000",
                        Fonte = dtoSiafdoc.Fonte.Codigo,
                        NaturezaDespesa = dtoSiafdoc.Estutura.Natureza.ToString(),
                        UGO = dtoSiafdoc.Reserva.Ugo,
                        PlanoInterno = "",
                        Valor = dtoSiafdoc.ValorMes.Sum(x => x.ValorMes).ToString(CultureInfo.InvariantCulture)
                    },
                    cronograma = new cronograma
                    {
                        Repeticao = new Repeticao
                        {
                            desc = dtoSiafdoc.ValorMes.Select(x => new desc
                            {
                                Mes = x.Descricao,
                                Valor = x.ValorMes.ToString(CultureInfo.InvariantCulture)
                            }).ToList(),
                        }
                    },
                    observacao = new observacao
                    {
                        Repeticao = new Repeticao
                        {
                            obs = listObs.Select(x => new obs
                            {
                                Observacao = removerAcentos(x.ToString())
                            }).ToList()
                        }
                    }
                }
            };

            return SIAFDOC;
        }

        private ConsultaNr ConvertNode(XmlNodeList node)
        {
            MemoryStream stm = new MemoryStream();

            StreamWriter stw = new StreamWriter(stm);
            stw.Write(node[1].OuterXml);
            stw.Flush();

            stm.Position = 0;

            XmlSerializer ser = new XmlSerializer(typeof(ConsultaNr));
            ConsultaNr result = (ser.Deserialize(stm) as ConsultaNr);

            return result;
        }

        private XmlDocument ConverterXml(string xml)
        {
            try
            {
                var xm = new XmlDocument();
                xm.LoadXml(xml.Replace("001 <", "001 "));

                return xm;
            }
            catch (Exception ex)
            {

                throw new Exception("SIAFEM - " + xml);
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
    }

    #region ClassesInternas
    internal class DtoSiafdoc
    {
        public IReserva Reserva { get; set; }
        public Programa Programa { get; set; }
        public Fonte Fonte { get; set; }
        public Estrutura Estutura { get; set; }
        public IEnumerable<IMes> ValorMes { get; set; }
    }

    #endregion
}
