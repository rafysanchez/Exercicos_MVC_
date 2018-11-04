using System.Web;

namespace Sids.Prodesp.Core.Services.WebService.Reserva
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Exceptions;
    using Model.Interface.Base;
    using Model.Interface.Configuracao;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using Model.Interface.Service.Reserva;
    using Model.ValueObject.Service.Siafem.Base;
    using Model.ValueObject.Service.Siafem.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;

    public class SiafemReservaService : BaseService
    {
        #region Propriedades

        private ILogError _logError;
        private readonly ISiafemReserva _siafemReservaService;
        private readonly ICrudPrograma _programa;
        private readonly ICrudFonte _fonte;
        private readonly ICrudEstrutura _estutura;

        #endregion

        #region Construtores
        public SiafemReservaService(
            ILogError logError, ISiafemReserva siafemService, ICrudPrograma programa, ICrudFonte fonte,
            ICrudEstrutura estutura) : base(logError)
        {
            _estutura = estutura;
            _fonte = fonte;
            _logError = logError;
            _siafemReservaService = siafemService;
            _programa = programa;
        }
        #endregion

        #region Metodos Publicos
        public string InserirReservaSiafem(string login, string senha, IReserva reserva, List<IMes> mes,string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DtoSiafdoc
                {
                    Reserva = reserva,
                    Programa = _programa.Fetch(new Programa { Codigo = (int)reserva.Programa }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = (int)reserva.Fonte }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = (int)reserva.Estrutura }).FirstOrDefault(),
                    ValorMes = mes
                };

                var siafdoc = GerarSiafdoc(dtoSiafdoc);
                var result = _siafemReservaService.InserirReservaSiafem(login, senha, unidadeGestora, siafdoc);
                this.SaveLog(new Exception(result)); // TODO remover
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
                    throw new SiafemException(messagemErro[0].InnerText);

                return numeroNr[0].InnerText;
            }
            catch (Exception e)
            {
                this.SaveLog(e); // TODO remover
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
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

                var result = _siafemReservaService.InserirReservaSiafisico(login, senha, unidadeGestora, siafdoc);
                this.SaveLog(new Exception(result)); // TODO remover
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
                    throw new SiafisicoException(messagem[0].FirstChild.Value);
                }

                return xm.GetElementsByTagName("NumeroNR")[0].FirstChild.Value;
            }
            catch (Exception e)
            {
                this.SaveLog(e); // TODO remover
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirReservaCancelamento(string login, string senha, IReserva cancelamento,List<ReservaCancelamentoMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DtoSiafdoc
                {
                    Reserva = cancelamento,
                    Programa = _programa.Fetch(new Programa { Codigo = (int)cancelamento.Programa }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = (int)cancelamento.Fonte }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = (int)cancelamento.Estrutura }).FirstOrDefault(),
                    ValorMes = mes.Cast<IMes>()
                };


                var siafdoc = GerarSiafemDocCanNR(dtoSiafdoc);

                var result = _siafemReservaService.InserirCancelamentoReserva(login, senha, unidadeGestora, siafdoc);
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
                    throw new SiafemException(messagemErro[0].InnerText);
                }

                return numeroNr[0].InnerText;
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
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

                var result = _siafemReservaService.ConsultaOC(login, senha, unidadegestora, siafdoc);
                string root = false.ToString();
                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgErro");
                messagem = messagem.Count == 0? xm.GetElementsByTagName("MsgRetorno"): messagem;
                

                if (status.Count > 0)
                    root = status[0].FirstChild.Value;
                else if (messagem.Count > 0)
                    root = false.ToString();

                if (!bool.Parse(root))
                {
                    throw new SiafisicoException(messagem[0].FirstChild.Value);
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
                throw new SidsException(e.Message);
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

                var result = _siafemReservaService.ConsultaNr(login, (senha), unidadeGestora, siafdoc);

                var xm = ConverterXml(result);

                var status = xm.GetElementsByTagName("StatusOperacao");

                var messagem = xm.GetElementsByTagName("MsgRetorno");
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


                ConsultaNr resultingMessage = ConvertNode(document);


                return resultingMessage;
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
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
                propriedades.FirstOrDefault(x => x.Name == "Mes" + (i + 1).ToString()).SetValue(siafdoc.SiafisicoDocNRPregao.documento, valorMes[i].Descricao);
                propriedades.FirstOrDefault(x => x.Name == "Valor" + (i + 1).ToString()).SetValue(siafdoc.SiafisicoDocNRPregao.documento, valorMes[i].ValorMes.ToString());
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
