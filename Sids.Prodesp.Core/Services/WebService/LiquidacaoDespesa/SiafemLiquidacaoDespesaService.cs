using System.Web;

namespace Sids.Prodesp.Core.Services.WebService.LiquidacaoDespesa
{
    using Base;
    using Extensions;
    using Infrastructure.Helpers;
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.Configuracao;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using Model.Interface.Service.LiquidacaoDespesa;
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml;
    using Model.Extension;
    using System.Collections.Specialized;
    using Model.Exceptions;

    public class SiafemLiquidacaoDespesaService : BaseService
    {
        private readonly ISiafemLiquidacaoDespesa _siafem;
        private readonly ICrudPrograma _programa;
        private readonly ICrudFonte _fonte;
        private readonly ICrudEstrutura _estutura;

        public SiafemLiquidacaoDespesaService(
            ILogError log, ISiafemLiquidacaoDespesa siafem,
            ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estutura)
            : base(log)
        {
            _estutura = estutura;
            _fonte = fonte;
            _siafem = siafem;
            _programa = programa;
        }

        #region Metodos Publicos 

        #region Rap

        #region Inscricao
        public string InserirRapInscricaoSiafem(string login, string password, string unidadeGestora, IRap entity)
        {
            try
            {
                var siafdoc = CreateSIAFDocInscRNP(entity);
                var result = _siafem.InserirRapInscricaoSiafem(login, password, unidadeGestora, siafdoc).ToXml("SIAFEM");

                return ReturnMessageProviderForSiafemService(result, entity);

            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }
        #endregion

        #region Requisicao
        public string InserirRapRequisicaoApoioSiafem(string login, string password, string unidadeGestora, IRap entity)
        {
            try
            {
                var siafidoc = CreateSIAFDocIncTraRPNP(entity);
                var result =
                    _siafem.InserirRapRequisicaoApoioSiafem(login, password, unidadeGestora, siafidoc).ToXml("SIAFEM");

                return ReturnMessageProviderForSiafemService(result, entity);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }


        #endregion

        #region Anulacao
        public string InserirRapAnulacaoApoioSiafem(string login, string password, string unidadeGestora, RapAnulacao entity)
        {
            try
            {
                return ReturnMessageProviderForSiafemService(
                    _siafem.InserirRapRequisicaoApoioSiafem(
                        login,
                        password,
                        unidadeGestora,
                        CreateSIAFDocIncTraRPNP(entity)
                    ).ToXml("SIAFEM"),
                    entity
                );
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }
        #endregion

        #endregion

        #region Subempenho
        public string InserirSubempenhoSiafem(string login, string password, string unidadeGestora, ILiquidacaoDespesa entity)
        {
            try
            {
                var documento = SiafemDocumentProvider(entity);
                var response =
                    _siafem.InserirSubempenhoSiafem(login, password, unidadeGestora, documento).ToXml("SIAFEM");

                return ReturnMessageProviderForSiafemService(response, entity);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirSubempenhoSiafisico(string login, string password, string unidadeGestora, Subempenho entity)
        {
            try
            {
                var document = new Documento
                {
                    Subempenho = entity,
                    Programa = _programa.Fetch(new Programa { Codigo = entity.ProgramaId }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = entity.FonteId }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = entity.NaturezaId }).FirstOrDefault()
                };

                var sfcodoc = SiafisicoDocumentProvider(entity, document);

                var response =
                    _siafem.InserirSubempenhoSiafisico(login, password, unidadeGestora, sfcodoc, document.TagId).ToXml("SIAFISICO");

                //response = SiafisicoDocumentRepeatIdProvider(entity, response);

                return ReturnMessageProviderForSiafisicoService(response, entity);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        #endregion

        #region SubempenhoCancelamento
        public string InserirSubempenhoCancelamentoSiafisico(string login, string password, string unidadeGestora, SubempenhoCancelamento entity)
        {
            try
            {
                var document = SiafisicoCancelamentoDocumentProvider(entity);
                var response =
                    _siafem.InserirSubempenhoSiafisico(login, password, unidadeGestora, document).ToXml("SIAFISICO");

                return ReturnMessageProviderForSiafisicoServiceCan(response, entity);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }
        #endregion

        #region Metodos Consultas

        public ConsultaNL ConsultaNL(string login, string password, string unidadeGestora, string numeroSiafemSiafisco)
        {
            try
            {
                var document = CreateSiafemConsultaNl(unidadeGestora, numeroSiafemSiafisco);
                var response = _siafem.Consultar(login, password, unidadeGestora, document);
                var xm = ConverterXml(response);

                return ReturnMessageForQueryNumber(xm);
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }

        public ConsultaNL ConsultaNL(string login, string password, string unidadeGestora, ILiquidacaoDespesa entity)
        {
            try
            {
                var document = CreateSiafemConsultaNl(entity);
                var response = _siafem.Consultar(login, password, unidadeGestora, document);
                var xm = ConverterXml(response);

                return ReturnMessageForQueryNumber(xm);
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }
        #endregion

        #endregion

        #region Metodos Privados

        #region Subempenho

        #region Helpers
        private static SIAFDOC SiafemDocumentProvider(ILiquidacaoDespesa entity)
        {
            switch (entity.CenarioSiafemSiafisico)
            {
                case 6: //Nota de Lançamento – NL
                    return CreateSiafemDocNL(entity);
                case 7: //Liquidação de Medição de Obras – NLObras
                    return CreateSiafemDocNLObras(entity);
                case 8: //Nota de Lançamento CT Obras
                    return CreateSiafemDocNLCTObras(entity);
                default:
                    return new SIAFDOC();
            }
        }
        private static SFCODOC SiafisicoDocumentProvider(Subempenho entity, Documento document)
        {
            switch (entity.CenarioSiafemSiafisico)
            {
                case 1: //Liquidação de Empenho Tradicional – NL
                    document.TagId = true;
                    //return CreateSiafisicoDocConItemDetalha(document);
                    return CreateSiafisicoDocLiquidaNl(document);
                case 2: //Liquidação de Pregão Eletrônico – NLPREGAO
                    document.TagId = true;
                    return CreateSiafisicoDocNLPregaoExecuta(document);
                case 3: //Nota de Lançamento BEC – NLBEC
                    return CreateSiafisicoLiqNLBec(document);
                case 4: //Nota Lançamento de Contrato – NLCONTRATO
                    return CreateSiafisicoDocNLContrato(document);
                case 5: //Nota de Lançamento de Liquidação de Empenho – NLEMLIQ
                    return CreateSiafisicoDocNLEmLiq(document);
                default:
                    return new SFCODOC();
            }
        }

        private string ReturnMessageProviderForSiafemService(XmlDocument document, ILiquidacaoDespesa entity)
        {
            var withNumberArray = new[] { 6 };
            return withNumberArray.Contains(entity.CenarioSiafemSiafisico) ? ReturnMessageForSiafemService(document) : ReturnMessageWithStatusForSiafemService(document);
        }
        private string ReturnMessageForSiafemService(XmlDocument document)
        {
            var message = document.GetElementsByTagName("MsgErro").Item(0)?.InnerText;
            var numero = document.GetElementsByTagName("NumeroNL").Item(0)?.InnerText;

            if (!string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(numero))
                throw new SidsException($"SIAFEM - {message}");

            return numero;
        }
        private string ReturnMessageWithStatusForSiafemService(XmlDocument document)
        {
            var status = Convert.ToBoolean(document.GetElementsByTagName("StatusOperacao").Item(0)?.InnerText);
            var message = document.GetElementsByTagName("MsgErro").Item(0)?.InnerText;
            message = message ?? document.GetElementsByTagName("MsgRetorno").Item(0)?.InnerText;
            var numero = document.GetElementsByTagName("NumeroNL").Item(0)?.InnerText;

            if (!status || !string.IsNullOrWhiteSpace(message))
                throw new SidsException($"SIAFEM - {message}");

            return numero;
        }

        private string ReturnMessageProviderForSiafisicoService(XmlDocument document, Subempenho entity)
        {
            var withNumberArray = new[] { 4, 5 };
            return withNumberArray.Contains(entity.CenarioSiafemSiafisico)
                ? ReturnMessageForSiafisicoService(document)
                : ReturnMessageWithStatusForSiafisicoService(document);
        }
        private string ReturnMessageForSiafisicoService(XmlDocument document)
        {
            var message = document.GetElementsByTagName("MsgErro").Item(0)?.InnerText;
            var numero = document.GetElementsByTagName("NumeroNL").Item(0)?.InnerText;

            if (!string.IsNullOrWhiteSpace(message))
                throw new SidsException($"SIAFISICO - {message}");

            return numero;
        }
        private string ReturnMessageWithStatusForSiafisicoService(XmlDocument document)
        {
            var test = document.GetElementsByTagName("StatusOperacao")?.Item(0)?.InnerText;
            var status = test == "" ? false : Convert.ToBoolean(test);
            var message = document.GetElementsByTagName("Msg").Item(0)?.InnerText;
            if (string.IsNullOrEmpty(message))
                message = document.GetElementsByTagName("MsgErro").Item(0)?.InnerText;
            var numero = document.GetElementsByTagName("NumeroNL").Item(0)?.InnerText;
            numero = numero ?? document.GetElementsByTagName("NL").Item(0)?.InnerText;

            if (!status || !string.IsNullOrWhiteSpace(message) && string.IsNullOrWhiteSpace(numero))
                throw new SidsException($"SIAFISICO - {message}");

            return numero;
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







        private ConsultaNL ReturnMessageForQueryNumber(XmlDocument document)
        {

            ConsultaNL resultado = null;

            var status = Convert.ToBoolean(document.GetElementsByTagName("StatusOperacao").Item(0)?.InnerText);
            var message = document.GetElementsByTagName("MsgErro").Item(0)?.InnerText;

            if (string.IsNullOrEmpty(message))
                message = document.GetElementsByTagName("MsgRetorno").Item(0)?.InnerText;

            if (!status || !string.IsNullOrWhiteSpace(message))
                throw new SidsException("SIAFEM - " + message);

            var dict = new StringDictionary();
            dict.Add("Item", "Codigo");

            var doc = document.GetElementsByTagName("documento");
            XmlNode doc2 = doc.Count > 1 ? doc.Item(1).ParentNode : doc.Item(0).ParentNode;
            var xml = doc2.OuterXml.ToXml("SIAFEM");

            var docFixed = xml.RepairRepeatedTagWithNumbers("ItensLiquidados", dict);

            var documentos = docFixed.GetElementsByTagName("documento");

            foreach (XmlNode node in documentos)
            {
                var parent = node.ParentNode.ParentNode.ParentNode;

                var type = node.ParentNode.ParentNode.NodeType;

                if (parent == null || parent.Name == "Doc_Retorno")
                {
                    var child = node.OuterXml.ToXml("SIAFEM");

                    //var convertido = documentos.ConvertNodeTo<ConsultaNL>();
                    resultado = child.Deserialize<ConsultaNL>();
                }
            }


            return resultado;
        }

        static XmlDocument SiafisicoDocumentRepeatIdProvider(ILiquidacaoDespesa entity, XmlDocument response)
        {
            switch (entity.CenarioSiafemSiafisico)
            {
                case 1:
                case 2:
                    response.GenerateIdForTagName(false, "desc");
                    break;
                case 3:
                    response.GenerateIdForTagName(false, "linha", "NotaFiscal");
                    break;
                case 4: break;
                case 5:
                    response.GenerateIdForTagName(false, "obs", "NF");
                    break;
                default: break;
            }

            return response;
        }
        private static IEnumerable<string> FetchObservations(ILiquidacaoDespesa document)
        {
            return new List<string> {
                document.DescricaoObservacao1?.NormalizeForService(),
                document.DescricaoObservacao2?.NormalizeForService(),
                document.DescricaoObservacao3?.NormalizeForService()
            };
        }
        #endregion

        #region Siafem
        private static SIAFDOC CreateSiafemDocNL(ILiquidacaoDespesa document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFNL001",
                SiafemDocNL = new SiafemDocNL
                {
                    documento = new documento
                    {
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(false),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        CgcCpfUgfav = document.NumeroCNPJCPFCredor,
                        GestaoFav = document.CodigoGestaoCredor
                    },
                    Evento = new Evento
                    {
                        Repeticao = new Repeticao
                        {
                            desc = document.Eventos.Select(s => new desc
                            {
                                Evento = s.NumeroEvento,
                                InscricaoEvento = s.InscricaoEvento,
                                Classificacao = s.Classificacao,
                                Fonte = s.Fonte,
                                Valor = Convert.ToString(s.ValorUnitario)
                            }).ToList()
                        }
                    },
                    Observacao = new Observacao
                    {
                        Repeticao = new Repeticao
                        {
                            obs = FetchObservations(document)
                            .Where(w => !string.IsNullOrWhiteSpace(w)).Select(s => new obs
                            {
                                Observacao = s
                            }).ToList()
                        }
                    },
                    NotaFiscal = new NotaFiscal
                    {
                        Repeticao = new Repeticao
                        {
                            NF = document.Notas.Select(s => new NF
                            {
                                NotaFiscal = s.CodigoNotaFiscal
                            }).ToList()
                        }
                    }
                }
            };
        }
        private static SIAFDOC CreateSiafemDocNLObras(ILiquidacaoDespesa document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFNLOBRAS",
                SiafemDocNLObras = new SiafemDocNLObras
                {
                    documento = new documento
                    {
                        Ano = DateTime.Now.Year.ToString(),
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        Empenho = document.NumeroOriginalSiafemSiafisico?.Substring(6, 5),
                        Normal = document.Normal,
                        Estorno = document.Estorno,
                        AnoMedicao = document.AnoMedicao,
                        MesMedicao = document.MesMedicao,
                        Valor = Convert.ToString(document.Valor),
                        Percentual = (document.Percentual.Length > 3) ? document.Percentual.Substring(0, 3) : Convert.ToString(document.Percentual),
                        ObrasPatrimoniadas = document.TipoObraId.Equals(1) ? "X" : default(string),
                        ObrasNaoPatrimoniadas = document.TipoObraId.Equals(2) ? "X" : default(string),
                        Obs01 = document.DescricaoObservacao1?.NormalizeForService(),
                        Obs02 = document.DescricaoObservacao2?.NormalizeForService(),
                        Obs03 = document.DescricaoObservacao3?.NormalizeForService(),
                        NotaFiscal1 = document.Notas.FirstOrDefault(f => f.Ordem == 1)?.CodigoNotaFiscal,
                        NotaFiscal2 = document.Notas.FirstOrDefault(f => f.Ordem == 2)?.CodigoNotaFiscal,
                        NotaFiscal3 = document.Notas.FirstOrDefault(f => f.Ordem == 3)?.CodigoNotaFiscal,
                        NotaFiscal4 = document.Notas.FirstOrDefault(f => f.Ordem == 4)?.CodigoNotaFiscal,
                        NotaFiscal5 = document.Notas.FirstOrDefault(f => f.Ordem == 5)?.CodigoNotaFiscal,
                        NotaFiscal6 = document.Notas.FirstOrDefault(f => f.Ordem == 6)?.CodigoNotaFiscal,
                        NotaFiscal7 = document.Notas.FirstOrDefault(f => f.Ordem == 7)?.CodigoNotaFiscal,
                        NotaFiscal8 = document.Notas.FirstOrDefault(f => f.Ordem == 8)?.CodigoNotaFiscal,
                        NotaFiscal9 = document.Notas.FirstOrDefault(f => f.Ordem == 9)?.CodigoNotaFiscal,
                        NotaFiscal10 = document.Notas.FirstOrDefault(f => f.Ordem == 10)?.CodigoNotaFiscal,
                        NotaFiscal11 = document.Notas.FirstOrDefault(f => f.Ordem == 11)?.CodigoNotaFiscal,
                        NotaFiscal12 = document.Notas.FirstOrDefault(f => f.Ordem == 12)?.CodigoNotaFiscal,
                        NotaFiscal13 = document.Notas.FirstOrDefault(f => f.Ordem == 13)?.CodigoNotaFiscal,
                        NotaFiscal14 = document.Notas.FirstOrDefault(f => f.Ordem == 14)?.CodigoNotaFiscal,
                        NotaFiscal15 = document.Notas.FirstOrDefault(f => f.Ordem == 15)?.CodigoNotaFiscal,
                    }
                }
            };
        }
        private static SIAFDOC CreateSiafemDocNLCTObras(ILiquidacaoDespesa document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFNLCTObras",
                SiafemDocNLCTObras = new SiafemDocNLCTObras
                {
                    documento = new documento
                    {
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        CgcCpfUg = document.NumeroCNPJCPFCredor,
                        GestaoFav = document.CodigoGestaoCredor,
                        Normal = document.Normal,
                        Estorno = document.Estorno,
                        TipoObra = Convert.ToString(document.CodigoTipoDeObra),
                        UnidadeGestoraObra = document.CodigoUnidadeGestoraObra,
                        AnoMedicao = document.AnoMedicao,
                        MesMedicao = document.MesMedicao,
                        NumeroObra = document.NumeroObra,
                        Valor = Convert.ToString(document.Valor),
                        Obs01 = document.DescricaoObservacao1?.NormalizeForService(),
                        Obs02 = document.DescricaoObservacao2?.NormalizeForService(),
                        Obs03 = document.DescricaoObservacao3?.NormalizeForService()
                    }
                }
            };
        }

        private static SIAFDOC CreateSiafemConsultaNl(string unidadeGestora, string numeroSiafemSiafisco)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFConsultaNL",
                SiafemDocConsultaNL = new SiafemDocConsultaNL
                {
                    documento = new documento
                    {
                        UnidadeGestora = unidadeGestora,
                        Gestao = "16055",
                        NumeroNL = numeroSiafemSiafisco
                    }
                }
            };
        }
        private static SIAFDOC CreateSiafemConsultaNl(ILiquidacaoDespesa document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFConsultaNL",
                SiafemDocConsultaNL = new SiafemDocConsultaNL
                {
                    documento = new documento
                    {
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = "16055",
                        NumeroNL = document.NumeroSiafemSiafisico
                    }
                }
            };
        }
        #endregion

        #region Siafisico
        private static SFCODOC CreateSiafisicoDocConItemDetalha(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCOConItemDetalha",
                SiafisicoDocConItemDetalha = new SiafisicoDocConItemDetalha
                {
                    documento = new documento
                    {
                        DataEmissao = document.Subempenho.DataEmissao.ToSiafisicoDateTime() ?? " ",
                        UG = document.Subempenho.CodigoUnidadeGestora ?? " ",
                        CT = document.Subempenho.NumeroCT?.Substring(6, 5) ?? " ",
                        NE = document.Subempenho.NumeroOriginalSiafemSiafisico?.Substring(6, 5) ?? " ",
                        Gestao = document.Subempenho.CodigoGestao ?? " ",
                        Evento = Convert.ToString(document.Subempenho.TipoEventoId) ?? " ",
                        Observacao1 = document.Subempenho.DescricaoObservacao1?.NormalizeForService() ?? " ",
                        Observacao2 = document.Subempenho.DescricaoObservacao2?.NormalizeForService() ?? " ",
                        Observacao3 = document.Subempenho.DescricaoObservacao3?.NormalizeForService() ?? " ",
                        NF1 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 1)?.CodigoNotaFiscal ?? " ",
                        NF2 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 2)?.CodigoNotaFiscal ?? " ",
                        NF3 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 3)?.CodigoNotaFiscal ?? " ",
                        NF4 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 4)?.CodigoNotaFiscal ?? " ",
                        NF5 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 5)?.CodigoNotaFiscal ?? " ",
                        NF6 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 6)?.CodigoNotaFiscal ?? " ",
                        NF7 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 7)?.CodigoNotaFiscal ?? " ",
                        NF8 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 8)?.CodigoNotaFiscal ?? " ",
                        NF9 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 9)?.CodigoNotaFiscal ?? " ",
                        NF10 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 10)?.CodigoNotaFiscal ?? " ",
                        NF11 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 11)?.CodigoNotaFiscal ?? " ",
                        NF12 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 12)?.CodigoNotaFiscal ?? " ",
                        NF13 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 13)?.CodigoNotaFiscal ?? " ",
                        NF14 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 14)?.CodigoNotaFiscal ?? " ",
                        NF15 = document.Subempenho.Notas.FirstOrDefault(f => f.Ordem == 15)?.CodigoNotaFiscal ?? " "
                    },
                    TabelaItem = new TabelaItem
                    {
                        Repete = new Repete
                        {
                            desc = document.Subempenho.Itens.Select(s => new desc
                            {
                                SeqItem = s.SequenciaItem.ToString(), //s.SequenciaItem.ToString("D3") ?? " ",
                                Item = s.CodigoItemServico.FormatarCodigoItem(),
                                //QtdeItemInteira = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture) ?? " ",
                                QtdeItemInteira = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],
                                //QtdeItemDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString("D3") ?? " "
                                QtdeItemDecimal = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1]
                            }).ToList()
                        }
                    }
                }
            };
        }


        private static SFCODOC CreateSiafisicoDocLiquidaNl(Documento document)
        {
            var retorno = new SFCODOC
            {
                cdMsg = "SFCOLiquidaNL",
                SFCOLiquidaNL = new SiafisicoDocLiquidaNl()
            };

            var documento = new documento();
            documento.DataEmissao = document.Subempenho.DataEmissao.ToSiafisicoDateTime() ?? " ";
            documento.UnidadeGestora = document.Subempenho.CodigoUnidadeGestora ?? " ";
            documento.Gestao = document.Subempenho.CodigoGestao ?? " ";
            documento.EmpenhoOriginal = document.Subempenho.NumeroOriginalSiafemSiafisico?.Substring(6, 5) ?? " ";
            documento.ContratoOriginal = document.Subempenho.NumeroCT?.Substring(6, 5) ?? " ";

            documento.SERVICOSEMGERAL = document.Subempenho.TipoEventoId.Equals(511700) ? "X" : string.Empty;
            documento.SEGUROSEMGERAL = document.Subempenho.TipoEventoId.Equals(511701) ? "X" : string.Empty;
            documento.MATERIALDECONSUMO = document.Subempenho.TipoEventoId.Equals(511702) ? "X" : string.Empty;
            documento.MATERIALPERMANENTE = document.Subempenho.TipoEventoId.Equals(511703) ? "X" : string.Empty;
            documento.ALUGUEIS = document.Subempenho.TipoEventoId.Equals(511704) ? "X" : string.Empty;
            documento.IMPORTACAODEMATCONSUMO = document.Subempenho.TipoEventoId.Equals(516708) ? "X" : string.Empty;
            documento.IMPORTACAODEMATPERMANENTE = document.Subempenho.TipoEventoId.Equals(516715) ? "X" : string.Empty;
            documento.ATIVINDUSTRIALMATERIAPRIMA = document.Subempenho.TipoEventoId.Equals(511710) ? "X" : string.Empty;
            documento.ATIVINDUSTRIALMATEMBALAGEM = document.Subempenho.TipoEventoId.Equals(511711) ? "X" : string.Empty;

            documento.repeticaoItem = new repeticaoItem
            {
                linha = document.Subempenho.Itens.Select(s => new linha
                {
                    Item = s.CodigoItemServico.FormatarCodigoItem(),
                    UnidForn = s.CodigoUnidadeFornecimentoItem,
                    //QtdeItemInteira = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture) ?? " ",
                    QtdInteiro = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],
                    //QtdeItemDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString("D3") ?? " "
                    QtdDecimal = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1]
                }).ToList()
            };
   
            documento.Observacao = document.Subempenho.DescricaoObservacao1?.NormalizeForService()
                                 + document.Subempenho.DescricaoObservacao2?.NormalizeForService()
                                 + document.Subempenho.DescricaoObservacao3?.NormalizeForService() ?? " ";

            documento.repeticaoNf = new repeticaoNf
            {
                NotaFiscal = document.Subempenho.Notas.Select(s => s.CodigoNotaFiscal ?? " ").ToList()
            };


            retorno.SFCOLiquidaNL.documento = documento;
            return retorno;
        }

        private static SFCODOC CreateSiafisicoDocNLPregaoExecuta(Documento document)
        {

            document.TagId = true;
            return new SFCODOC
            {
                cdMsg = "SFCONLPregao",
                SFCONLPregao = new SFCONLPregao
                {
                    documento = new documento
                    {
                        DataEmissao = document.Subempenho.DataEmissao.ToSiafisicoDateTime() ?? " ",
                        UnidadeGestora = document.Subempenho.CodigoUnidadeGestora ?? " ",
                        ContratoOriginal = document.Subempenho.NumeroCT?.Substring(6, 5) ?? " ",
                        EmpenhoOriginal = document.Subempenho.NumeroOriginalSiafemSiafisico?.Substring(6, 5) ?? " ",
                        Gestao = document.Subempenho.CodigoGestao ?? " ",
                        Evento = Convert.ToString(document.Subempenho.TipoEventoId) ?? " ",

                        EventoServicoGeral = document.Subempenho.TipoEventoId.Equals(511700) ? "x" : " ",
                        EventoSegurosEmGeral = document.Subempenho.TipoEventoId.Equals(511701) ? "x" : " ",
                        EventoMaterialDeconsumo = document.Subempenho.TipoEventoId.Equals(511702) ? "x" : " ",
                        EventoMaterialPermanente = document.Subempenho.TipoEventoId.Equals(511703) ? "x" : " ",
                        EventoAtivIndustrialMateriaPrima = document.Subempenho.TipoEventoId.Equals(511710) ? "x" : " ",
                        EventoAtivIndustrialMatEmbalagem = document.Subempenho.TipoEventoId.Equals(511711) ? "x" : " ",

                        Observacao = document.Subempenho.DescricaoObservacao1?.NormalizeForService()
                                    + document.Subempenho.DescricaoObservacao2?.NormalizeForService()
                                    + document.Subempenho.DescricaoObservacao3?.NormalizeForService() ?? " ",

                        repeticaoNf = new repeticaoNf
                        {
                            NotaFiscal = document.Subempenho.Notas.Select(s => s.CodigoNotaFiscal ?? " ").ToList()
                        },
                        repeticaoItem = new repeticaoItem
                        {
                            linha = document.Subempenho.Itens.Select(s => new linha
                            {
                                //SeqItem = s.SequenciaItem.ToString(),
                                Item = s.CodigoItemServico.FormatarCodigoItem(),
                                UnidForn = Convert.ToInt32(s.CodigoUnidadeFornecimentoItem).ToString("D5"),
                                //QtdInteiro = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture),
                                QtdInteiro = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],
                                //QtdDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString("D3") ?? " ",
                                QtdDecimal = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1]
                            }).ToList()
                        }
                    }
                }
            };
        }
        private static SFCODOC CreateSiafisicoLiqNLBec(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCONLBEC001",
                SFCOLiqNLBec = new SFCOLiqNLBec
                {
                    documento = new documento
                    {
                        DataEmissao = document.Subempenho.DataEmissao.ToSiafisicoDateTime(),

                        UnidadeGestora = document.Subempenho.CodigoUnidadeGestora,

                        //ContratoOriginal = document.Subempenho.NumeroContrato,
                        ContratoOriginal = document.Subempenho.NumeroCT?.Substring(6, 5) ?? " ",

                        //EmpenhoOriginal = document.Subempenho.NumeroOriginalSiafemSiafisico?.Substring(6, 5),
                        EmpenhoOriginal = document.Subempenho.NumeroOriginalSiafemSiafisico?.Substring(6, 5) ?? " ",

                        Gestao = document.Subempenho.CodigoGestao,
                        Evento = Convert.ToString(document.Subempenho.TipoEventoId) ?? " ",

                        //Ua = document.Subempenho.UaConsumidora,
                        //ValorUa = Convert.ToString(document.Subempenho.Valor),
                        SERVICOSEMGERAL = document.Subempenho.TipoEventoId.Equals(511700) ? "x" : " ",
                        SEGUROSEMGERAL = document.Subempenho.TipoEventoId.Equals(511701) ? "x" : " ",
                        MATERIALDECONSUMO = document.Subempenho.TipoEventoId.Equals(511702) ? "x" : " ",
                        MATERIALPERMANENTE = document.Subempenho.TipoEventoId.Equals(511703) ? "x" : " ",
                        ATIVINDUSTRIALMATERIAPRIMA = document.Subempenho.TipoEventoId.Equals(511710) ? "x" : " ",
                        ATIVINDUSTRIALMATEMBALAGEM = document.Subempenho.TipoEventoId.Equals(511711) ? "x" : " ",

                        Observacao = document.Subempenho.DescricaoObservacao1?.NormalizeForService()
                                    + document.Subempenho.DescricaoObservacao2?.NormalizeForService()
                                    + document.Subempenho.DescricaoObservacao3?.NormalizeForService() ?? " ",

                        repeticaoNf = new repeticaoNf
                        {
                            NotaFiscal = document.Subempenho.Notas.Select(s => s.CodigoNotaFiscal).ToList()
                        },

                        repeticaoItem = new repeticaoItem
                        {
                            linha = document.Subempenho.Itens.Select(s => new linha
                            {
                                Item = s.CodigoItemServico.FormatarCodigoItem(),
                                UnidForn = Convert.ToInt32(s.CodigoUnidadeFornecimentoItem).ToString("D5"),
                                //QtdInteiro = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture),
                                QtdInteiro = s.QuantidadeMaterialServico > 0 ? s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0] : "0",
                                //QtdDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString("D3"),
                                QtdDecimal = s.QuantidadeMaterialServico > 0 ? s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1] : "000"
                            }).ToList()
                        }

                    }
                }
            };
        }
        private static SFCODOC CreateSiafisicoDocNLContrato(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCONLCONTRATO001",
                SiafisicoDocNLContrato = new SiafisicoDocNLContrato
                {
                    documento = new documento
                    {
                        DataEmissao = document.Subempenho.DataEmissao.ToSiafisicoDateTime() ?? " ",
                        UG = document.Subempenho.CodigoUnidadeGestora ?? " ",
                        Gestao = document.Subempenho.CodigoGestao ?? " ",
                        CgcCpfUGFav = document.Subempenho.NumeroCNPJCPFCredor ?? " ",
                        Valor = Convert.ToString(document.Subempenho.Valor) ?? " ",
                        Evento = Convert.ToString(document.Subempenho.CodigoEvento) ?? " ",
                        Observacao1 = document.Subempenho.DescricaoObservacao1?.NormalizeForService() ?? " ",
                        Observacao2 = document.Subempenho.DescricaoObservacao2?.NormalizeForService() ?? " ",
                        Observacao3 = document.Subempenho.DescricaoObservacao3?.NormalizeForService() ?? " ",
                    }
                }
            };
        }
        private static SFCODOC CreateSiafisicoDocNLEmLiq(Documento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCONLEmLiq",
                SiafisicoDocNLEmLiq = new SiafisicoDocNLEmLiq
                {
                    documento = new documento
                    {
                        DataEmissao = document.Subempenho.DataEmissao.ToSiafisicoDateTime(false),
                        UnidadeGestora = document.Subempenho.CodigoUnidadeGestora,
                        Gestao = document.Subempenho.CodigoGestao,
                        Normal = document.Subempenho.Normal,
                        Ano = document.Subempenho.AnoMedicao,
                        Empenho = document.Subempenho.NumeroOriginalSiafemSiafisico?.Substring(6, 5),
                        Valor = Convert.ToString(document.Subempenho.Valor)
                    },
                    Observacao = new Observacao
                    {
                        Repeticao = new Repeticao
                        {
                            obs = FetchObservations(document.Subempenho)
                            .Where(w => !string.IsNullOrWhiteSpace(w)).Select(s => new obs
                            {
                                Observacao = s
                            }).ToList()
                        }
                    },
                    NotaFiscal = new NotaFiscal
                    {
                        Repeticao = new Repeticao
                        {
                            NF = document.Subempenho.Notas.Select(s => new NF
                            {
                                NotaFiscal = s.CodigoNotaFiscal
                            }).ToList()
                        }
                    }
                }
            };
        }
        #endregion

        #endregion

        #region SubempenhoCancelamento

        #region Helpers
        private static SFCODOC SiafisicoCancelamentoDocumentProvider(SubempenhoCancelamento entity)
        {
            switch (entity.CenarioSiafemSiafisico)
            {
                case 9: //Estorno de Liquidação de Empenho – NL
                    return CreateSiafisicoSfconlEstornoCan(entity);
                case 10: //Anulação de Liquidação de Pregão Eletrônico – NLPREGAO
                    return CreateSiafisicoSfcoCanNlPregaoCan(entity);
                case 11: //Anulação de Nota de Lançamento BEC – NLBEC
                    return CreateSiafisicoSfcoCanNlBecCan(entity);
                case 4: //Anulação de Nota Lançamento de Contrato - NLCONTRATO
                    return CreateSiafisicoDocNlContratoCan(entity);
                case 5: //Anulação de Nota de Lançamento de Liquidação de Empenho – NLEMLIQ
                    return CreateSiafisicoDocNlEmLiqCan(entity);
                default:
                    return new SFCODOC();
            }
        }
        private string ReturnMessageProviderForSiafisicoServiceCan(XmlDocument document, SubempenhoCancelamento entity)
        {
            var withNumberArray = new[] { 4, 5 };
            return withNumberArray.Contains(entity.CenarioSiafemSiafisico)
                ? ReturnMessageForSiafisicoService(document)
                : ReturnMessageWithStatusForSiafisicoService(document);
        }

        #endregion

        #region Siafisico
        private static SFCODOC CreateSiafisicoSfconlEstornoCan(SubempenhoCancelamento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCONLEstorno",
                SFCONLEstorno = new SFCONLEstorno
                {
                    documento = new documento
                    {
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        EmpenhoOriginal = document.NumeroOriginalSiafemSiafisico.Substring(6, 5),
                        ContratoOriginal = document.NumeroCT == null ? "" : document.NumeroCT,

                        EventoServicoGeral = document.TipoEventoId.Equals(516700) ? "X" : "",
                        EventoSegurosEmGeral = document.TipoEventoId.Equals(516701) ? "X" : "",
                        EventoMaterialDeconsumo = document.TipoEventoId.Equals(516702) ? "X" : "",
                        EventoMaterialPermanente = document.TipoEventoId.Equals(516703) ? "X" : "",
                        EventoAlugueis = document.TipoEventoId.Equals(511704) ? "X" : "",
                        //EventoObrasEInstalacoes = document.TipoEventoId.Equals(511711) ? "X" : default(string),
                        EventoImportacaoDeMatConsumo = document.TipoEventoId.Equals(516708) ? "X" : "",
                        EventoImportacaoDeMatPermanente = document.TipoEventoId.Equals(516715) ? "X" : "",
                        EventoAtivIndustrialMateriaPrima = document.TipoEventoId.Equals(516710) ? "X" : "",
                        EventoAtivIndustrialMatEmbalagem = document.TipoEventoId.Equals(516711) ? "X" : "",

                        Observacao = document.DescricaoObservacao1?.NormalizeForService(),
                        repeticaoItem = new repeticaoItem
                        {
                            linha = document.Itens.Select(s => new linha
                            {
                                Item = s.CodigoItemServico.FormatarCodigoItem(),
                                UnidForn = s.CodigoUnidadeFornecimentoItem,
                                //QtdInteiro = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture),
                                QtdInteiro = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],
                                //QtdDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString(CultureInfo.InvariantCulture),
                                QtdDecimal = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1]
                            }).ToList()
                        },
                        repeticaoNf = new repeticaoNf
                        {
                            NotaFiscal = document.Notas.Select(s => s.CodigoNotaFiscal).ToList()
                        }
                    }
                }
            };
        }

        private static SFCODOC CreateSiafisicoSfcoCanNlPregaoCan(SubempenhoCancelamento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCOCanNLPregao",
                SFCOCanNLPregao = new SFCOCanNLPregao
                {
                    documento = new documento
                    {
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        EmpenhoOriginal = document.NumeroOriginalSiafemSiafisico.Substring(6, 5),
                        ContratoOriginal = document.NumeroCT,
                        NLReferencia = document.NlReferencia.Substring(6, 5),
                        SERVICOSEMGERAL = document.TipoEventoId.Equals(511700) ? "X" : string.Empty,
                        SEGUROSEMGERAL = document.TipoEventoId.Equals(511701) ? "X" : string.Empty,
                        MATERIALDECONSUMO = document.TipoEventoId.Equals(511702) ? "X" : string.Empty,
                        MATERIALPERMANENTE = document.TipoEventoId.Equals(511703) ? "X" : string.Empty,
                        ATIVINDUSTRIALMATERIAPRIMA = document.TipoEventoId.Equals(511710) ? "X" : string.Empty,
                        ATIVINDUSTRIALMATEMBALAGEM = document.TipoEventoId.Equals(511711) ? "X" : string.Empty,

                        Observacao = document.DescricaoObservacao1?.NormalizeForService(),
                        repeticaoItem = new repeticaoItem
                        {
                            linha = document.Itens.Select(s => new linha
                            {
                                Item = s.CodigoItemServico.FormatarCodigoItem(),
                                UnidForn = s.CodigoUnidadeFornecimentoItem,
                                //QtdInteiro = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture),
                                QtdInteiro = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[0],
                                //QtdDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString(CultureInfo.InvariantCulture),
                                QtdDecimal = s.QuantidadeMaterialServico.ZeroParaNulo().Split(',')[1]
                            }).ToList()
                        },
                        repeticaoNf = new repeticaoNf
                        {
                            NotaFiscal = document.Notas.Select(s => s.CodigoNotaFiscal).ToList()
                        }
                    }
                }
            };
        }

        private static SFCODOC CreateSiafisicoSfcoCanNlBecCan(SubempenhoCancelamento document)
        {
            var retorno = new SFCODOC
            {
                cdMsg = "SFCOCanNLBec"
            };
            var sfcoCanNLBec = new SFCOCanNLBec();
            var documento = new documento();
            documento.DataEmissao = document.DataEmissao.ToSiafisicoDateTime();
            documento.UnidadeGestora = document.CodigoUnidadeGestora;
            documento.Gestao = document.CodigoGestao;
            documento.EmpenhoOriginal = document.NumeroOriginalSiafemSiafisico.Substring(6, 5);
            documento.ContratoOriginal = document.NumeroCT ?? string.Empty;
            documento.NLReferencia = document.NlReferencia.Substring(6, 5);
            documento.SERVICOSEMGERAL = document.TipoEventoId.Equals(511700) ? "X" : string.Empty;
            documento.SEGUROSEMGERAL = document.TipoEventoId.Equals(511701) ? "X" : string.Empty;
            documento.MATERIALDECONSUMO = document.TipoEventoId.Equals(511702) ? "X" : string.Empty;
            documento.MATERIALPERMANENTE = document.TipoEventoId.Equals(511703) ? "X" : string.Empty;
            documento.ATIVINDUSTRIALMATERIAPRIMA = document.TipoEventoId.Equals(511710) ? "X" : string.Empty;
            documento.ATIVINDUSTRIALMATEMBALAGEM = document.TipoEventoId.Equals(511711) ? "X" : string.Empty;

            documento.Observacao = document.DescricaoObservacao1?.NormalizeForService();
            documento.repeticaoItem = new repeticaoItem
            {
                linha = document.Itens.Select(s => new linha
                {
                    Item = s.CodigoItemServico.FormatarCodigoItem(),
                    UnidForn = s.CodigoUnidadeFornecimentoItem,
                    //QtdInteiro = Convert.ToInt32(s.QuantidadeMaterialServico).ToString(CultureInfo.InvariantCulture),
                    //QtdDecimal = Convert.ToInt32((s.QuantidadeMaterialServico - (int)s.QuantidadeMaterialServico) * 100).ToString(CultureInfo.InvariantCulture),
                }).ToList()
            };

            documento.repeticaoNf = new repeticaoNf
            {
                NotaFiscal = document.Notas.Select(s => s.CodigoNotaFiscal).ToList()
            };

            sfcoCanNLBec.documento = documento;
            retorno.SFCOCanNLBec = sfcoCanNLBec;

            return retorno;
        }

        private static SFCODOC CreateSiafisicoDocNlContratoCan(SubempenhoCancelamento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCONLCONTRATO001",
                SiafisicoDocNLContrato = new SiafisicoDocNLContrato
                {
                    documento = new documento
                    {
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(),
                        UG = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        CgcCpfUGFav = document.NumeroCNPJCPFCredor,
                        Valor = Convert.ToString(document.Valor),
                        Evento = Convert.ToString(document.CodigoEvento),
                        Observacao1 = document.DescricaoObservacao1?.NormalizeForService(),
                        Observacao2 = document.DescricaoObservacao2?.NormalizeForService(),
                        Observacao3 = document.DescricaoObservacao3?.NormalizeForService()
                    }
                }
            };
        }

        private static SFCODOC CreateSiafisicoDocNlEmLiqCan(SubempenhoCancelamento document)
        {
            return new SFCODOC
            {
                cdMsg = "SFCONLEmLiq",
                SiafisicoDocNLEmLiq = new SiafisicoDocNLEmLiq
                {
                    documento = new documento
                    {
                        DataEmissao = document.DataEmissao.ToSiafisicoDateTime(false),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        Normal = document.Normal,
                        Estorno = document.Estorno,
                        Ano = document.DataEmissao.Year.ToString(),
                        Empenho = document.NumeroOriginalSiafemSiafisico?.Substring(6, 5),
                        Valor = Convert.ToString(document.Valor > 0 ? document.Valor : document.ValorRealizado)
                    },
                    Observacao = new Observacao
                    {
                        Repeticao = new Repeticao
                        {
                            obs = FetchObservations(document)
                            .Where(w => !string.IsNullOrWhiteSpace(w)).Select(s => new obs
                            {
                                Observacao = s
                            }).ToList()
                        }
                    },
                    NotaFiscal = new NotaFiscal
                    {
                        Repeticao = new Repeticao
                        {
                            NF = document.Notas.Select(s => new NF
                            {
                                NotaFiscal = s.CodigoNotaFiscal
                            }).ToList()
                        }
                    }
                }
            };
        }
        #endregion

        #endregion

        #region Rap
        private static SIAFDOC CreateSIAFDocInscRNP(IRap document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFINCRPNPTES",
                SIAFDocInscRNP = new SIAFDocInscRNP
                {
                    documento = new documento
                    {
                        Data = document.DataEmissao.ToSiafisicoDateTime(),
                        UG = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        NumeroEmpenho = document.NumeroOriginalSiafemSiafisico?.Substring(6, 5),
                        Valor = Convert.ToString(document.Valor),
                        Inclui = document.Normal,
                        Estorna = document.Estorno,
                        SERVICOSGERAISSEMCONTRATO = document.TipoServicoId.Equals(16) ? "X" : default(string),
                        SERVICOSGERAISCOMCONTRATO = document.TipoServicoId.Equals(17) ? "X" : default(string),
                        MATERIALCONSUMOSEMCONTRATO = document.TipoServicoId.Equals(18) ? "X" : default(string),
                        MATERIALCONSUMOCOMCONTRATO = document.TipoServicoId.Equals(19) ? "X" : default(string),
                        MATERIALPERMANENTESCONTRATO = document.TipoServicoId.Equals(20) ? "X" : default(string),
                        MATERIALPERMANENTECCONTRATO = document.TipoServicoId.Equals(21) ? "X" : default(string),
                        IMPORTACAODEBENSSCONTRATO = document.TipoServicoId.Equals(22) ? "X" : default(string),
                        IMPORTACAODEBENSCCONTRATO = document.TipoServicoId.Equals(23) ? "X" : default(string),
                        ATIVINDUSTMATPRIMASCONTR = document.TipoServicoId.Equals(24) ? "X" : default(string),
                        ATIVINDUSTMATPRIMACCONTR = document.TipoServicoId.Equals(25) ? "X" : default(string),
                        ATIVINDUSTMATEMBALSCONTR = document.TipoServicoId.Equals(26) ? "X" : default(string),
                        ATIVINDUSTMATEMBALCCONTR = document.TipoServicoId.Equals(27) ? "X" : default(string),
                        ATIVINDUSTOUTCUSTOSSCONTR = document.TipoServicoId.Equals(28) ? "X" : default(string),
                        ATIVINDUSTOUTCUSTOSCCONTR = document.TipoServicoId.Equals(29) ? "X" : default(string),
                        OBRASEINSTALACOESNAOPATRIM = document.TipoServicoId.Equals(30) ? "X" : default(string),
                        OBRASEINSTALACOESPATRIMONIADAS = document.TipoServicoId.Equals(31) ? "X" : default(string),
                        SUBSCRICAODEACOESEMPRDEP = document.TipoServicoId.Equals(8) ? "X" : default(string),
                        SUBSCRICAODEACOESEMPRNDEP = document.TipoServicoId.Equals(9) ? "X" : default(string),
                        OUTROSCREDORES = document.TipoServicoId.Equals(12) ? "X" : default(string),
                        Obs1 = document.DescricaoObservacao1?.NormalizeForService(),
                        Obs2 = document.DescricaoObservacao2?.NormalizeForService(),
                        Obs3 = document.DescricaoObservacao3?.NormalizeForService(),
                        NF1 = document.Notas?.FirstOrDefault(f => f.Ordem == 1)?.CodigoNotaFiscal,
                        NF2 = document.Notas?.FirstOrDefault(f => f.Ordem == 2)?.CodigoNotaFiscal,
                        NF3 = document.Notas?.FirstOrDefault(f => f.Ordem == 3)?.CodigoNotaFiscal,
                        NF4 = document.Notas?.FirstOrDefault(f => f.Ordem == 4)?.CodigoNotaFiscal,
                        NF5 = document.Notas?.FirstOrDefault(f => f.Ordem == 5)?.CodigoNotaFiscal,
                        NF6 = document.Notas?.FirstOrDefault(f => f.Ordem == 6)?.CodigoNotaFiscal,
                        NF7 = document.Notas?.FirstOrDefault(f => f.Ordem == 7)?.CodigoNotaFiscal,
                        NF8 = document.Notas?.FirstOrDefault(f => f.Ordem == 8)?.CodigoNotaFiscal,
                        NF9 = document.Notas?.FirstOrDefault(f => f.Ordem == 9)?.CodigoNotaFiscal,
                        NF10 = document.Notas?.FirstOrDefault(f => f.Ordem == 10)?.CodigoNotaFiscal,
                        NF11 = document.Notas?.FirstOrDefault(f => f.Ordem == 11)?.CodigoNotaFiscal,
                        NF12 = document.Notas?.FirstOrDefault(f => f.Ordem == 12)?.CodigoNotaFiscal,
                        NF13 = document.Notas?.FirstOrDefault(f => f.Ordem == 13)?.CodigoNotaFiscal,
                        NF14 = document.Notas?.FirstOrDefault(f => f.Ordem == 14)?.CodigoNotaFiscal
                    }
                }
            };
        }
        private static SIAFDOC CreateSIAFDocIncTraRPNP(IRap document)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFINCTRARPNP",
                SIAFDocIncTraRPNP = new SIAFDocIncTraRPNP
                {
                    documento = new documento
                    {
                        Data = document.DataEmissao.ToSiafisicoDateTime(),
                        UnidadeGestora = document.CodigoUnidadeGestora,
                        Gestao = document.CodigoGestao,
                        CnpjCpfUg = document.NumeroCNPJCPFCredor,
                        GestaoFavorecida = document.CodigoGestaoCredor,
                        NumeroEmpenho = document.NumeroOriginalSiafemSiafisico?.Substring(6, 5),
                        Classificacao = document.Classificacao,
                        AnoMedicao = document.AnoMedicao,
                        MesMedicao = document.MesMedicao,
                        Valor = document.Valor.ToString("D2"),
                        Inclui = document.Normal,
                        Estorna = document.Estorno,
                        SERVICOSGERAIS = document.TipoServicoId.Equals(1) ? "X" : default(string),
                        MATERIALDECONSUMO = document.TipoServicoId.Equals(2) ? "X" : default(string),
                        IMPORTACAODEBENS = document.TipoServicoId.Equals(3) ? "X" : default(string),
                        ATIVINDUSTRIALMATERIAPRIMA = document.TipoServicoId.Equals(4) ? "X" : default(string),
                        OUTROSCREDORESDEADESPEXANT = document.TipoServicoId.Equals(5) ? "X" : default(string),
                        OBRASPATRIMONIADAS = document.TipoServicoId.Equals(6) ? "X" : default(string),
                        OBRASNAOPATRIMONIADAS = document.TipoServicoId.Equals(7) ? "X" : default(string),
                        SUBSCRICAODEACOESEMPRDEP = document.TipoServicoId.Equals(8) ? "X" : default(string),
                        SUBSCRICAODEACOESEMPRNDEP = document.TipoServicoId.Equals(9) ? "X" : default(string),
                        OUTROSCREDORESPARCELTRIBUTOS = document.TipoServicoId.Equals(10) ? "X" : default(string),
                        OUTROSCREDORESDESAPROPRIACAO = document.TipoServicoId.Equals(11) ? "X" : default(string),
                        OUTROSCREDORES = document.TipoServicoId.Equals(12) ? "X" : default(string),
                        MATERIALPERMANENTE = document.TipoServicoId.Equals(13) ? "X" : default(string),
                        ATIVINDUSTRIALMATEMBALAGEM = document.TipoServicoId.Equals(14) ? "X" : default(string),
                        ATIVINDUSTRIALOUTROSCUSTOS = document.TipoServicoId.Equals(15) ? "X" : default(string),
                        Obs1 = document.DescricaoObservacao1.NormalizeForService(),
                        Obs2 = document.DescricaoObservacao2.NormalizeForService(),
                        Obs3 = document.DescricaoObservacao3.NormalizeForService(),
                        NF1 = document.Notas.FirstOrDefault(f => f.Ordem == 1)?.CodigoNotaFiscal,
                        NF2 = document.Notas.FirstOrDefault(f => f.Ordem == 2)?.CodigoNotaFiscal,
                        NF3 = document.Notas.FirstOrDefault(f => f.Ordem == 3)?.CodigoNotaFiscal,
                        NF4 = document.Notas.FirstOrDefault(f => f.Ordem == 4)?.CodigoNotaFiscal,
                        NF5 = document.Notas.FirstOrDefault(f => f.Ordem == 5)?.CodigoNotaFiscal,
                        NF6 = document.Notas.FirstOrDefault(f => f.Ordem == 6)?.CodigoNotaFiscal,
                        NF7 = document.Notas.FirstOrDefault(f => f.Ordem == 7)?.CodigoNotaFiscal,
                        NF8 = document.Notas.FirstOrDefault(f => f.Ordem == 8)?.CodigoNotaFiscal,
                        NF9 = document.Notas.FirstOrDefault(f => f.Ordem == 9)?.CodigoNotaFiscal,
                        NF10 = document.Notas.FirstOrDefault(f => f.Ordem == 10)?.CodigoNotaFiscal,
                        NF11 = document.Notas.FirstOrDefault(f => f.Ordem == 11)?.CodigoNotaFiscal,
                        NF12 = document.Notas.FirstOrDefault(f => f.Ordem == 12)?.CodigoNotaFiscal,
                        NF13 = document.Notas.FirstOrDefault(f => f.Ordem == 13)?.CodigoNotaFiscal,
                        NF14 = document.Notas.FirstOrDefault(f => f.Ordem == 14)?.CodigoNotaFiscal,
                        NF15 = document.Notas.FirstOrDefault(f => f.Ordem == 15)?.CodigoNotaFiscal
                    }
                }
            };
        }
        #endregion

        #endregion
    }

    #region Classe Concreta Documento
    internal class Documento
    {
        public Programa Programa { get; set; }
        public Fonte Fonte { get; set; }
        public Estrutura Estutura { get; set; }
        public Subempenho Subempenho { get; set; }
        public bool TagId { get; set; }
    }
    #endregion
}
