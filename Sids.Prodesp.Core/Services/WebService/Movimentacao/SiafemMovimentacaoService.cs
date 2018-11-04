using System.Web;

namespace Sids.Prodesp.Core.Services.WebService.Movimentacao
{
    using Base;
    using Extensions;
    using Infrastructure.Helpers;
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.Configuracao;
    using Model.Interface.Movimentacao;
    using Model.Interface.Log;
    using Model.Interface.Service.Movimentacao;
    using Model.ValueObject.Service.Siafem.Movimentacao;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml;
    using Model.Extension;
    using System.Collections.Specialized;
    using Model.Exceptions;
    using SIAFDOC = Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao.SIAFDOC;

    using Model.Entity.Movimentacao;
    using Model.Interface.Base;
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using System.Text.RegularExpressions;

    public class SiafemMovimentacaoService : BaseService
    {
        private readonly ISiafemMovimentacao _siafem;
        private readonly ICrudPrograma _programa;
        private readonly ICrudFonte _fonte;
        private readonly ICrudEstrutura _estutura;

        public SiafemMovimentacaoService(
            ILogError log, ISiafemMovimentacao siafem,
            ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estutura)
            : base(log)
        {
            _estutura = estutura;
            _fonte = fonte;
            _siafem = siafem;
            _programa = programa;
        }

        #region Metodos Publicos 



        public string InserirCancelamentoTesouroSiafem(string login, string senha, MovimentacaoOrcamentaria movimentacao, ref MovimentacaoCancelamento objModel, IEnumerable<MovimentacaoMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DocumentoCancelamento
                {
                    Cancelamento = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = movimentacao.IdPrograma }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = movimentacao.IdFonte }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = movimentacao.IdEstrutura }).FirstOrDefault(),
                    ValorMes = mes,
                    DataCadastro = movimentacao.DataCadastro,
                    UnidadeGestora = movimentacao.UnidadeGestoraEmitente
                };

                var siafdoc = GerarSiafdocCancelamentoTesouro(dtoSiafdoc);

                var response = _siafem.InserirInserirMovimentacaoOrcamentaria(login, senha, unidadeGestora, siafdoc).ToXml("SIAFEM");

                return ReturnMessageWithStatusForSiafemService(response);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirCancelamentoNaoTesouroSiafem(string login, string senha, MovimentacaoOrcamentaria movimentacao, ref MovimentacaoCancelamento objModel, IEnumerable<MovimentacaoMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DocumentoCancelamento
                {
                    Cancelamento = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = movimentacao.IdPrograma }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = movimentacao.IdFonte }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = movimentacao.IdEstrutura }).FirstOrDefault(),
                    ValorMes = mes,
                    DataCadastro = movimentacao.DataCadastro
                };

                var siafdoc = GerarSiafdocCancelamentoNaoTesouro(dtoSiafdoc);

                var response = _siafem.InserirInserirMovimentacaoOrcamentaria(login, senha, unidadeGestora, siafdoc).ToXml("SIAFEM");

                return ReturnMessageWithStatusForSiafemService(response);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }


        public string InserirDistribuicaoTesouroSiafem(string login, string senha, MovimentacaoOrcamentaria movimentacao, ref MovimentacaoDistribuicao objModel, IEnumerable<MovimentacaoMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DocumentoDistribuicao
                {
                    Distribuicao = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = movimentacao.IdPrograma }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = movimentacao.IdFonte }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = movimentacao.IdEstrutura }).FirstOrDefault(),
                    ValorMes = mes,
                    DataCadastro = movimentacao.DataCadastro
                };

                var siafdoc = GerarSiafdocDistribuicaoTesouro(dtoSiafdoc);

                var response = _siafem.InserirInserirMovimentacaoOrcamentaria(login, senha, unidadeGestora, siafdoc).ToXml("SIAFEM");

                return ReturnMessageWithStatusForSiafemService(response);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }

        public string InserirDistribuicaoNaoTesouroSiafem(string login, string senha, MovimentacaoOrcamentaria movimentacao, ref MovimentacaoDistribuicao objModel, IEnumerable<MovimentacaoMes> mes, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DocumentoDistribuicao
                {
                    Distribuicao = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = movimentacao.IdPrograma }).FirstOrDefault(),
                    Fonte = _fonte.Fetch(new Fonte { Id = movimentacao.IdFonte }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = movimentacao.IdEstrutura }).FirstOrDefault(),
                    ValorMes = mes,
                    DataCadastro = movimentacao.DataCadastro
                };

                var siafdoc = GerarSiafdocDistribuicaoNaoTesouro(dtoSiafdoc);

                var response = _siafem.InserirInserirMovimentacaoOrcamentaria(login, senha, unidadeGestora, siafdoc).ToXml("SIAFEM");

                return ReturnMessageWithStatusForSiafemService(response);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }


        public string InserirNotaCreditoSiafem(string login, string senha, ref MovimentacaoNotaDeCredito objModel, string unidadeGestora)
        {
            try
            {
                var dtoSiafdoc = new DocumentoNotaDeCredito
                {
                    NotaDeCredito = objModel,
                    Programa = _programa.Fetch(new Programa { Codigo = objModel.IdPrograma }).FirstOrDefault(),
                    Estutura = _estutura.Fetch(new Estrutura { Codigo = objModel.IdEstrutura }).FirstOrDefault()
                };

                var siafdoc = GerarSiafdocNotaDeCredito(dtoSiafdoc, objModel);

                var response = _siafem.InserirInserirMovimentacaoOrcamentaria(login, senha, unidadeGestora, siafdoc).ToXml("SIAFEM");

                return ReturnMessageWithStatusForSiafemServiceNC(response);
            }
            catch (Exception e)
            {
                HttpContext.Current.Session["xml"] = HttpContext.Current.Session["xmlSiafem"];
                HttpContext.Current.Session["xmlSiafem"] = "";
                throw new SidsException(e.Message);
            }
        }
        

        #endregion

        #region Metodos Privados

        #region Subempenho

        #region Helpers
        
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


        private string ReturnMessageWithStatusForSiafemServiceNC(XmlDocument document)
        {
            var message = document.GetElementsByTagName("MsgErro").Item(0)?.InnerText;

            message = message ?? document.GetElementsByTagName("MsgRetorno").Item(0)?.InnerText;
            var numero = document.GetElementsByTagName("NumeroNC").Item(0)?.InnerText;
            var regex = new Regex(@"\d{4}NC\d{5}");
            
            var match = regex.Match(numero);
            var status = match.Success;

            if (!status || !string.IsNullOrWhiteSpace(message))
                throw new SidsException($"SIAFEM - {message}");

            return numero;
        }

        private XmlDocument ConverterXml(string xml)
        {
            try
            {
                xml = xml.Replace("&", "&amp;");

                var document = new XmlDocument();

                document.LoadXml(xml);
                
                return document;
            }
            catch
            {
                throw new SiafemException(xml);
            }
        }

        private static IEnumerable<string> FetchObservationsNc(DocumentoNotaDeCredito document)
        {
            return new List<string> {
                document.NotaDeCredito.Observacao?.NormalizeForService(),
                document.NotaDeCredito.Observacao2?.NormalizeForService(),
                document.NotaDeCredito.Observacao3?.NormalizeForService()
            };
        }
        #endregion

        #region Siafem

        private static SIAFDOC GerarSiafdocCancelamentoTesouro(DocumentoCancelamento dtoSiafdoc)
        {
            var siafDoc = new SIAFDOC
            {
                cdMsg = "SIAFCandiscota",
                SiafemDocCandiscota = new SiafemDocMov()
                {
                    documento = new Model.ValueObject.Service.Siafem.Movimentacao.documento
                    {
                        DataEmissao = dtoSiafdoc.DataCadastro.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.UnidadeGestora,
                        Gestao = dtoSiafdoc.Cancelamento.GestaoFavorecida,
                        UgFavorecida = dtoSiafdoc.Cancelamento.UnidadeGestoraFavorecida,
                        GestaoFavorecida = dtoSiafdoc.Cancelamento.GestaoFavorecida,
                        FonteTesouro = dtoSiafdoc.Cancelamento.IdFonte.ToString().PadLeft(3, '0'),//ou 001
                        CategoriaGasto = dtoSiafdoc.Cancelamento.CategoriaGasto,

                        Observacao01 = dtoSiafdoc.Cancelamento.Observacao ?? string.Empty,
                        Observacao02 = dtoSiafdoc.Cancelamento.Observacao2 ?? string.Empty,
                        Observacao03 = dtoSiafdoc.Cancelamento.Observacao3 ?? string.Empty
                    }
                }
            };

            PreencherMeses(dtoSiafdoc, siafDoc.SiafemDocCandiscota.documento);

            return siafDoc;
        }


        private static SIAFDOC GerarSiafdocCancelamentoNaoTesouro(DocumentoCancelamento dtoSiafdoc)
        {
            var siafDoc = new SIAFDOC
            {
                cdMsg = "SIAFCandisdico",
                SiafemDocCandisdico= new SiafemDocMov
                {
                    documento = new Model.ValueObject.Service.Siafem.Movimentacao.documento
                    {
                        DataEmissao = dtoSiafdoc.DataCadastro.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.UnidadeGestora,
                        Gestao = dtoSiafdoc.GestaoEmitente,
                        UgFavorecida = dtoSiafdoc.Cancelamento.UnidadeGestoraFavorecida,
                        GestaoFavorecida = dtoSiafdoc.Cancelamento.GestaoFavorecida,
                        FonteNaoTesouro = dtoSiafdoc.Cancelamento.IdFonte.ToString().PadLeft(3, '0'),//ou 001
                        CategoriaGasto = dtoSiafdoc.Cancelamento.CategoriaGasto,
                        
                        Observacao01 = dtoSiafdoc.Cancelamento.Observacao ?? string.Empty,
                        Observacao02 = dtoSiafdoc.Cancelamento.Observacao2 ?? string.Empty,
                        Observacao03 = dtoSiafdoc.Cancelamento.Observacao3 ?? string.Empty
                    }
                }
            };

            PreencherMeses(dtoSiafdoc, siafDoc.SiafemDocCandisdico.documento);

            return siafDoc;
        }


        private static SIAFDOC GerarSiafdocDistribuicaoTesouro(DocumentoDistribuicao dtoSiafdoc)
        {
            var siafDoc = new SIAFDOC
            {
                cdMsg = "SIAFDiscota",
                SiafemDocDiscota = new SiafemDocMov
                {
                    documento = new Model.ValueObject.Service.Siafem.Movimentacao.documento
                    {
                        DataEmissao = dtoSiafdoc.DataCadastro.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.UnidadeGestora,
                        Gestao = dtoSiafdoc.GestaoEmitente,
                        UgFavorecida = dtoSiafdoc.Distribuicao.UnidadeGestoraFavorecida,
                        GestaoFavorecida = dtoSiafdoc.Distribuicao.GestaoFavorecida,
                        FonteTesouro = dtoSiafdoc.Distribuicao.IdFonte.ToString().PadLeft(3, '0'),//ou 001
                        CategoriaGasto = dtoSiafdoc.Distribuicao.CategoriaGasto,

                        Observacao01 = dtoSiafdoc.Distribuicao.Observacao ?? string.Empty,
                        Observacao02 = dtoSiafdoc.Distribuicao.Observacao2 ?? string.Empty,
                        Observacao03 = dtoSiafdoc.Distribuicao.Observacao3 ?? string.Empty
                    }                   
                }
            };

            PreencherMeses(dtoSiafdoc, siafDoc.SiafemDocDiscota.documento);

            return siafDoc;
        }


        private static SIAFDOC GerarSiafdocDistribuicaoNaoTesouro(DocumentoDistribuicao dtoSiafdoc)
        {
            var siafDoc = new SIAFDOC
            {
                cdMsg = "SIAFDisdicota",
                SiafemDocDisdicota = new SiafemDocMov
                {
                    documento = new Model.ValueObject.Service.Siafem.Movimentacao.documento
                    {
                        DataEmissao = dtoSiafdoc.DataCadastro.ToString("ddMMMyyyy").ToUpper(),
                        UnidadeGestora = dtoSiafdoc.UnidadeGestora,
                        Gestao = dtoSiafdoc.GestaoEmitente,
                        UgFavorecida = dtoSiafdoc.Distribuicao.UnidadeGestoraFavorecida,
                        GestaoFavorecida = dtoSiafdoc.Distribuicao.GestaoFavorecida,
                        FonteNaoTesouro = dtoSiafdoc.Distribuicao.IdFonte.ToString().PadLeft(3, '0'),//ou 001
                        CategoriaGasto = dtoSiafdoc.Distribuicao.CategoriaGasto,

                        Observacao01 = dtoSiafdoc.Distribuicao.Observacao ?? string.Empty,
                        Observacao02 = dtoSiafdoc.Distribuicao.Observacao2 ?? string.Empty,
                        Observacao03 = dtoSiafdoc.Distribuicao.Observacao3 ?? string.Empty
                    }                   
                }
            };

            PreencherMeses(dtoSiafdoc, siafDoc.SiafemDocDisdicota.documento);

            return siafDoc;
        }

        private static void PreencherMeses(DocumentoBase dtoSiafdoc, Model.ValueObject.Service.Siafem.Movimentacao.documento siafDoc)
        {
            siafDoc.Mes01 = dtoSiafdoc.ValorMes.Any() ? dtoSiafdoc.ValorMes.ElementAtOrDefault(0).Descricao : string.Empty;
            siafDoc.Mes02 = dtoSiafdoc.ValorMes.Count() > 1 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(1).Descricao : string.Empty;
            siafDoc.Mes03 = dtoSiafdoc.ValorMes.Count() > 2 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(2).Descricao : string.Empty;
            siafDoc.Mes04 = dtoSiafdoc.ValorMes.Count() > 3 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(3).Descricao : string.Empty;
            siafDoc.Mes05 = dtoSiafdoc.ValorMes.Count() > 4 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(4).Descricao : string.Empty;
            siafDoc.Mes06 = dtoSiafdoc.ValorMes.Count() > 5 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(5).Descricao : string.Empty;
            siafDoc.Mes07 = dtoSiafdoc.ValorMes.Count() > 6 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(6).Descricao : string.Empty;
            siafDoc.Mes08 = dtoSiafdoc.ValorMes.Count() > 7 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(7).Descricao : string.Empty;
            siafDoc.Mes09 = dtoSiafdoc.ValorMes.Count() > 8 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(8).Descricao : string.Empty;
            siafDoc.Mes10 = dtoSiafdoc.ValorMes.Count() > 9 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(9).Descricao : string.Empty;
            siafDoc.Mes11 = dtoSiafdoc.ValorMes.Count() > 10 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(10).Descricao : string.Empty;
            siafDoc.Mes12 = dtoSiafdoc.ValorMes.Count() > 11 ? dtoSiafdoc.ValorMes.ElementAtOrDefault(11).Descricao : string.Empty;
            siafDoc.Valor01 = dtoSiafdoc.ValorMes.Any() ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(0).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor02 = dtoSiafdoc.ValorMes.Count() > 1 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(1).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor03 = dtoSiafdoc.ValorMes.Count() > 2 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(2).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor04 = dtoSiafdoc.ValorMes.Count() > 3 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(3).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor05 = dtoSiafdoc.ValorMes.Count() > 4 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(4).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor06 = dtoSiafdoc.ValorMes.Count() > 5 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(5).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor07 = dtoSiafdoc.ValorMes.Count() > 6 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(6).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor08 = dtoSiafdoc.ValorMes.Count() > 7 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(7).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor09 = dtoSiafdoc.ValorMes.Count() > 8 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(8).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor10 = dtoSiafdoc.ValorMes.Count() > 9 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(9).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor11 = dtoSiafdoc.ValorMes.Count() > 10 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(10).ValorMes * 100)).ToString() : string.Empty;
            siafDoc.Valor12 = dtoSiafdoc.ValorMes.Count() > 11 ? ((int)(dtoSiafdoc.ValorMes.ElementAtOrDefault(11).ValorMes * 100)).ToString() : string.Empty;
        }



        private static SIAFDOC GerarSiafdocNotaDeCredito(DocumentoNotaDeCredito dtoSiafdoc, MovimentacaoNotaDeCredito mov)
        {
            var siafDoc = new SIAFDOC
            {
                cdMsg = "SIAFNC001",
                SiafemDocNC = new SiafemDocNC()
            };

            var doc = new documentoNC
            {
                DataEmissao = DateTime.Now.ToString("ddMMMyyyy").ToUpper(), //data atual
                UGEmitente = dtoSiafdoc.NotaDeCredito.UnidadeGestoraEmitente,
                GestaoEmitente = dtoSiafdoc.NotaDeCredito.GestaoEmitente,
                UGFavorecida = dtoSiafdoc.NotaDeCredito.UnidadeGestoraFavorecida,
                GestaoFavorecida = dtoSiafdoc.NotaDeCredito.GestaoFavorecida,
                Evento = dtoSiafdoc.NotaDeCredito.EventoNC
            };

            var celula = new SiafemDocCelula
            {
                Repeticao = new Model.ValueObject.Service.Siafem.Movimentacao.Repeticao
                {
                    desc = new des2
                    {
                        Id = "1",
                        UO = dtoSiafdoc.NotaDeCredito.Uo,
                        PT = dtoSiafdoc.Programa.Cfp.PadRight(17, '0'),
                        Fonte = mov.FonteRecurso,
                        NaturezaDespesa = dtoSiafdoc.Estutura.Natureza,
                        UGO = dtoSiafdoc.NotaDeCredito.Ugo,
                        PlanoInterno = string.IsNullOrEmpty(dtoSiafdoc.NotaDeCredito.PlanoInterno) ? string.Empty : dtoSiafdoc.NotaDeCredito.PlanoInterno,
                        Valor = ((int)(dtoSiafdoc.NotaDeCredito.Valor * 100)).ToString()
                    }
                }
            };

            var observacao = new Model.ValueObject.Service.Siafem.Movimentacao.Observacao
            {
                Repeticao = new Model.ValueObject.Service.Siafem.Movimentacao.Repeticao
                {
                    obs = FetchObservationsNc(dtoSiafdoc)
                                    .Where(w => !string.IsNullOrWhiteSpace(w)).Select(s => new Model.ValueObject.Service.Siafem.Movimentacao.obs
                                    {
                                        Id = s,
                                        Observacao = string.IsNullOrWhiteSpace(s) ? string.Empty : s
                                    }).ToList()
                }
            };

            siafDoc.SiafemDocNC.documento = doc;
            siafDoc.SiafemDocNC.celula = celula;
            siafDoc.SiafemDocNC.observacao = observacao;

            return siafDoc;
        }


        #endregion

        #endregion

        #endregion

        #region Impressão NL

        public RespostaConsultaNL ConsultaNL(string login, string password, string unidadeGestora, string gestao, string numeroNL, string tipoDocumento, string nrReducao, string nrSuplementacao)
        {
            try
            {
                var document = CreateSiafemConsultaNL(unidadeGestora, gestao, numeroNL);
                var response = _siafem.Consultar(login, password, unidadeGestora, document);
                var xm = ConverterXml(response);

                return ReturnMessageForQueryNumberNL(xm, tipoDocumento, nrReducao, nrSuplementacao);
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }

        private static SIAFDOC CreateSiafemConsultaNL(string unidadeGestora, string gestao, string numeroNL)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFConsultaNL",
                SiafemDocConsultaNL = new Model.ValueObject.Service.Siafem.Movimentacao.SiafemDocConsultaNL
                {
                    documento = new Model.ValueObject.Service.Siafem.Movimentacao.documento
                    {
                        UnidadeGestora = unidadeGestora,
                        Gestao = gestao,
                        NumeroNL = numeroNL
                    }
                }
            };
        }

        private RespostaConsultaNL ReturnMessageForQueryNumberNL(XmlDocument document, string tipoDocumento, string nrReducao, string nrSuplementacao)
        {
            RespostaConsultaNL resultado = null;

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

                    resultado = child.Deserialize<RespostaConsultaNL>();
                }
            }

            ListaEventosNL lista = new ListaEventosNL();
            XmlNode root = documentos.Item(0);
            int contador = 1;

            if (root.HasChildNodes)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    if (root.ChildNodes[i].Name == "Classificacao" + contador)
                    {
                        lista.Classificacao = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Evento" + contador)
                    {
                        lista.Evento = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Fonte" + contador)
                    {
                        lista.Fonte = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "RecDesp" + contador)
                    {
                        lista.RecDesp = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "InscEvento" + contador)
                    {
                        lista.InscEvento = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Valor" + contador)
                    {
                        lista.Valor = root.ChildNodes[i].InnerText;
                        contador++;
                        resultado.ListaEventosNL.Add(lista);
                        lista = new ListaEventosNL();
                    }
                }
            }

            if (resultado != null)
            {
                resultado.NumeroReducao = nrReducao;
                resultado.NumeroSuplementacao = nrSuplementacao;
                resultado.TipoDocumento = tipoDocumento;
            }

            return resultado;
        }

        #endregion

        #region Impressão NC

        public RespostaConsultaNC ConsultaNC(string login, string password, string unidadeGestora, string gestao, string numeroNC, string tipoDocumento, string nrReducao, string nrSuplementacao)
        {
            try
            {
                var document = CreateSiafemConsultaNC(unidadeGestora, gestao, numeroNC);
                var response = _siafem.Consultar(login, password, unidadeGestora, document);
                var xm = ConverterXml(response);

                return ReturnMessageForQueryNumberNC(xm, tipoDocumento, nrReducao, nrSuplementacao);
            }
            catch (Exception e)
            {
                throw new SidsException(e.Message);
            }
        }

        private static SIAFDOC CreateSiafemConsultaNC(string unidadeGestora, string gestao, string numeroNC)
        {
            return new SIAFDOC
            {
                cdMsg = "SIAFConsultaNC",
                SiafemDocConsultaNC = new SiafemDocConsultaNC
                {
                    documento = new Model.ValueObject.Service.Siafem.Movimentacao.documento
                    {
                        UnidadeGestora = unidadeGestora,
                        Gestao = gestao,
                        NumeroNC = numeroNC
                    }
                }
            };
        }

        private RespostaConsultaNC ReturnMessageForQueryNumberNC(XmlDocument document, string tipoDocumento, string nrReducao, string nrSuplementacao)
        {
            RespostaConsultaNC resultado = null;

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

                    resultado = child.Deserialize<RespostaConsultaNC>();
                }
            }

            ListaEventosNC lista = new ListaEventosNC();
            XmlNode root = documentos.Item(0);
            int contador = 1;

            if (root.HasChildNodes)
            {
                for (int i = 0; i < root.ChildNodes.Count; i++)
                {
                    if (root.ChildNodes[i].Name == "UO" + contador)
                    {
                        lista.UO = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Ugr" + contador)
                    {
                        lista.Ugr = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Fonte" + contador)
                    {
                        lista.Fonte = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "PlanoInterno" + contador)
                    {
                        lista.PlanoInterno = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Despesa" + contador)
                    {
                        lista.Despesa = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "Valor" + contador)
                    {
                        lista.Valor = root.ChildNodes[i].InnerText;
                    }
                    if (root.ChildNodes[i].Name == "PT" + contador)
                    {
                        lista.PT = root.ChildNodes[i].InnerText;
                        contador++;
                        resultado.ListaEventosNC.Add(lista);
                        lista = new ListaEventosNC();
                    }
                }
            }

            resultado.NumeroReducao = nrReducao;
            resultado.NumeroSuplementacao = nrSuplementacao;
            resultado.TipoDocumento = tipoDocumento;

            return resultado;
        }

        #endregion
    }

    #region Classe Concreta Documento
    internal class DocumentoBase
    {
        public Programa Programa { get; set; }
        public Fonte Fonte { get; set; }
        public Estrutura Estutura { get; set; }

        public IEnumerable<MovimentacaoMes> ValorMes { get; set; }

    }
    internal class DocumentoCancelamento : DocumentoBase
    {
        public string GestaoEmitente { get; set; }
        public string UnidadeGestora { get; set; }
        public DateTime DataCadastro { get; set; }
        public MovimentacaoCancelamento Cancelamento { get; set; }
    }
    internal class DocumentoNotaDeCredito : DocumentoBase
    {
        public MovimentacaoNotaDeCredito NotaDeCredito { get; set; }
    }
    internal class DocumentoDistribuicao : DocumentoBase
    {
        public string GestaoEmitente { get; set; }
        public string UnidadeGestora { get; set; }
        public DateTime DataCadastro { get; set; }
        public MovimentacaoDistribuicao Distribuicao { get; set; }
    }
    #endregion
}
