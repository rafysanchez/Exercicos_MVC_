using Microsoft.Ajax.Utilities;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;


namespace Sids.Prodesp.UI.Controllers.Base
{
    using Application;
    using Model.Base.Empenho;
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.ValueObject.Service.Prodesp.Common;
    using Model.ValueObject.Service.Siafem.Empenho;
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    public class ConsutasBaseController : BaseController
    {
     
        private delegate T ConsultaEsturura<T>(int anoExercicio, short regionalId, string cfp, string natureza, int programa, string origemRecurso, string processo);
        protected static readonly IEnumerable<Regional> RegionalList = App.RegionalService.Buscar(new Regional()) ?? new List<Regional>();
        [HttpPost]
        public JsonResult ConsultarContrato(Contrato contrato)
        {
            try
            {
                var contratos = App.CommonService.ConsultarContrato(contrato.NumContrato, contrato.Type);

                var result = new
                {
                    Status = "Sucesso",
                    Contrato = contratos
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarPorEstrutura(DtoEstrutura estrutura)
        {
            try
            {
                ConsultaEsturura<object> consultaEsturura;

                if (estrutura.Tipo == "Reserva")
                    consultaEsturura = App.CommonService.ConsultaReservaEstrutura;
                else
                    consultaEsturura = App.CommonService.ConsultaEmpenhoEstrutura;

                var estruturas = consultaEsturura(estrutura.AnoExercicio,
                        estrutura.RegionalId,
                        estrutura.Cfp,
                        estrutura.Natureza,
                        estrutura.Programa,
                        estrutura.OrigemRecurso,
                        estrutura.Processo
                    );

                var result = new
                {
                    Status = "Sucesso",
                    Estrutura = estruturas
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarReserva(string reserva)
        {
            try
            {
                var reservas = App.CommonService.ConsultarReserva(reserva);
                var reservaFromDatabase = App.ReservaService.Buscar(new Reserva() { NumProdesp = reserva }).FirstOrDefault();

                var result = new
                {
                    Status = "Sucesso",
                    Reserva = reservas,
                    ReservaFromDatabase = reservaFromDatabase
                };

                if (!string.IsNullOrWhiteSpace(reservas.OutErro))
                    throw new Exception(string.Concat("Prodesp - {0}", reservas.OutErro));

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarEmpenho(string empenho)
        {
            try
            {

                var subempenhoProdespFormatado = empenho.Formatar("000000000/000");
                var empenhoProdespFormatado = subempenhoProdespFormatado.Split('/')[0];

                var empenhos = App.CommonService.ConsultarEmpenho(empenhoProdespFormatado);

                while (empenhos.OutCnpjCpf.Length > 14)
                         empenhos.OutCnpjCpf = empenhos.OutCnpjCpf.Remove(0,1);



                int empenhoId = App.EmpenhoService.BuscarGrid(new Empenho { NumeroEmpenhoProdesp = empenho }).FirstOrDefault()?.Id ?? default(int);

                var empenhoFromDatabase = empenhoId > 0 ? App.EmpenhoService.Buscar(new Empenho { Id = empenhoId }).FirstOrDefault() : default(Empenho);

                var result = new
                {
                    Status = "Sucesso",
                    Empenho = empenhos,
                    EmpenhoFromDatabase = empenhoFromDatabase
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarSubempenho(string subempenho)
        {
            try
            {
                var subempenhoProdespFormatado = subempenho.Formatar("000000000/000");
                var empenhoProdespFormatado = subempenhoProdespFormatado.Split('/')[0];

                var subempenhoProdespFromWs = App.CommonService.ConsultarSubempenho(subempenho);
                var empenhoProdespFromDb = App.EmpenhoService.BuscarGrid(new Empenho { NumeroEmpenhoProdesp = empenhoProdespFormatado }).FirstOrDefault();


                var subempenhoFromDb = App.SubempenhoService.BuscarGrid(new Subempenho() { NumeroProdesp = subempenhoProdespFormatado }).FirstOrDefault();
                if (subempenhoFromDb != null)
                {
                    subempenhoFromDb = App.SubempenhoService.Selecionar(subempenhoFromDb.Id);
                    subempenhoFromDb.Id = default(int);
                    //subempenhoFromDb.NumeroSiafemSiafisico = null;
                    subempenhoFromDb.NlReferencia = subempenhoFromDb.NumeroSiafemSiafisico;
                    //entity.NumeroProdesp = null;
                    subempenhoFromDb.DataCadastro = default(DateTime);
                    subempenhoFromDb.DataTransmitidoProdesp = default(DateTime);
                    subempenhoFromDb.DataTransmitidoSiafemSiafisico = default(DateTime);
                    subempenhoFromDb.MensagemProdesp = null;
                    subempenhoFromDb.MensagemSiafemSiafisico = null;
                    subempenhoFromDb.TransmitidoProdesp = false;
                    subempenhoFromDb.TransmitidoSiafem = false;
                    subempenhoFromDb.TransmitidoSiafisico = false;

                    subempenhoFromDb.StatusProdesp = "N";
                    subempenhoFromDb.StatusSiafemSiafisico = "N";
                    subempenhoFromDb.StatusSiafemSiafisico = "N";
                }

                var result = new
                {
                    Status = "Sucesso",
                    Subempenho = subempenhoProdespFromWs,
                    EmpenhoFromDatabase = empenhoProdespFromDb,
                    SubempenhoFromDatabase = subempenhoFromDb
                };

                if (!string.IsNullOrWhiteSpace(subempenhoProdespFromWs.outErro))
                    throw new Exception(string.Concat("Prodesp - {0}", subempenhoProdespFromWs.outErro));

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarEspecificacao(string codigo)
        {
            try
            {
                var despesas = App.CommonService.ConsultarEspecificacaoDespesa(codigo);
                var result = new
                {
                    Status = "Sucesso",
                    Especificacao = despesas,
                    Msg = despesas.outSucesso
                };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarAssinatura(string codigo, int tipo)
        {
            try
            {
                var assinaturas = App.CommonService.ConsultarAssinatura(codigo, tipo);
                var result = new
                {
                    Status = "Sucesso",
                    Assinatura = assinaturas
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarOc(DtoOc dtoOc)
        {
            try
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                var oc = App.CommonService.ConsultaOC(usuario, dtoOc.Oc, dtoOc.Ugo, dtoOc.Ug);

                var fontes = App.FonteService.Buscar(new Fonte { Codigo = oc.Fonte }).ToList();

                oc.Fonte = fontes.Any() ? fontes.First().Id.ToString() : "0";

                var result = new
                {
                    Status = "Sucesso",
                    Oc = oc
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ConsultarNl(string numeroNl, EnumTipoOperacaoEmpenho? origem)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                ConsultaNL consultaNl = App.CommonService.ConsultaNL(usuario, string.Empty, string.Empty, numeroNl);
                ConsultaNe consultaNe = null;
                ConsultaCt consultaCt = null;

                consultaNe = VerificarSeNlPossuiNe(usuario, consultaNl, consultaNe);
                if (consultaNe == null)
                {
                    return Json(new
                    {
                        Status = "Falha",
                        Msg = "NL sem NE relacionada"

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<BaseEmpenhoItem> items = new List<BaseEmpenhoItem>();

                    if (!string.IsNullOrWhiteSpace(consultaNe.NumeroContrato))
                    {
                        consultaCt = App.CommonService.ConsultaCt(usuario, consultaNe.NumeroContrato, "");
                        
                        items = MontarBaseItem(consultaCt);

                        List<int> codigosItensIncluir = consultaNl.ItensLiquidados.Select(x => x.Codigo).ToList();
                        items = items.Where(x => codigosItensIncluir.Contains(int.Parse(x.CodigoItemServico.RemoveSpecialChar()))).ToList();
                        

                        foreach (var item in items)
                        {
                            var itemDaNl = consultaNl.ItensLiquidados.FirstOrDefault(x => x.Codigo == int.Parse(item.CodigoItemServico.RemoveSpecialChar()));
                            if (itemDaNl != null)
                            {
                                item.QuantidadeMaterialServico = Decimal.Parse(itemDaNl.Quantidade);
                                item.ValorTotal = Decimal.Parse(itemDaNl.Valor);
                            }
                        }
                    }

                    var result = new
                    {
                        Status = "Sucesso",
                        Ct = consultaCt,
                        baseItem = items
                    };

                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private static ConsultaNe VerificarSeNlPossuiNe(Usuario usuario, ConsultaNL consultaNl, ConsultaNe consultaNe)
        {
            if (!string.IsNullOrEmpty(consultaNl.InscEvento1) || !string.IsNullOrEmpty(consultaNl.InscEvento2))
            {
                var patternNe = @"\d{4}NE\d{5}";

                var regex = new Regex(patternNe);

                var match1 = regex.Match(consultaNl.InscEvento1);
                if (match1.Success)
                {
                    var empenho = new Empenho { NumeroEmpenhoSiafem = (consultaNl.InscEvento1) };
                    consultaNe = App.CommonService.ConsultaNe(empenho, usuario);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(consultaNl.InscEvento2))
                    {
                        match1 = regex.Match(consultaNl.InscEvento2);
                        if (match1.Success)
                        {
                            var empenho = new Empenho { NumeroEmpenhoSiafem = (consultaNl.InscEvento1) };
                            consultaNe = App.CommonService.ConsultaNe(empenho, usuario);
                        }
                    }
                }
            }

            return consultaNe;
        }

        [HttpPost]
        public JsonResult ConsultarCtPorNe(string numeroNe, EnumTipoOperacaoEmpenho? origem,string numDoc)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                var empenho = new Empenho { NumeroEmpenhoSiafem = (numeroNe) };
                ConsultaNe consultaNe = App.CommonService.ConsultaNe(empenho, usuario);

                return ConsultarCt(consultaNe.NumeroContrato, origem,numDoc);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarCt(string NumCt, EnumTipoOperacaoEmpenho? origem, string numDoc)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(NumCt))
                {
                    return Json(new { Status = "Falha", Msg = "CT não encontrada." }, JsonRequestBehavior.AllowGet);
                }

                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                ConsultaCt ct = App.CommonService.ConsultaCt(usuario, NumCt, "");

                List<BaseEmpenhoItem> items = CalcularSaldo(usuario, ct, origem,numDoc);

                var result = new
                {
                    Status = "Sucesso",
                    Ct = ct,
                    baseItem = items
                };

                return Json(result, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
        protected static List<BaseEmpenhoItem> CalcularSaldo(Usuario usuario, ConsultaCt ct)
        {
            return CalcularSaldo(usuario, ct, EnumTipoOperacaoEmpenho.Empenho,"");
        }
        protected static List<BaseEmpenhoItem> CalcularSaldo(Usuario usuario, ConsultaCt ct, EnumTipoOperacaoEmpenho? origem,string numDoc)
        {

            List<BaseEmpenhoItem> items = new List<BaseEmpenhoItem>();

            //testar implementação
            var subempenho = App.SubempenhoService.BuscarGrid(new Subempenho { NumeroOriginalSiafemSiafisico = ct.ContratoEmpenhado, NumeroProdesp = numDoc }).FirstOrDefault();
            if(subempenho != null)
            subempenho = App.SubempenhoService.Selecionar(subempenho?.Id ?? 0);


            if (origem == EnumTipoOperacaoEmpenho.ProgramacaoDesembolso)
            {
                items = MontarBaseItem02(subempenho);
            }else
            {
                items = MontarBaseItem(ct);
            }

            PreencherDadosFonte(ct);

            var operacao = origem.HasValue ? origem.Value : EnumTipoOperacaoEmpenho.Empenho;

            if (!string.IsNullOrWhiteSpace(ct.ContratoEmpenhado))
            {
                var ne = App.CommonService.ConsultaPrecoNE(usuario, ct.ContratoEmpenhado, string.Empty);

                if (ne != null)
                {
                    foreach (var item in items)
                    {
                        var itemNe = ne.RepeteDescricao.FirstOrDefault(r => r.Item == item.CodigoItemServico);
                        if (itemNe != null)
                        {
                            //item.CodigoItemServico = item.CodigoItemServico.FormatarCodigoItem();

                            switch (operacao)
                            {
                                case EnumTipoOperacaoEmpenho.SubempenhoAnulacao:
                                    item.ValorTotal = Convert.ToDecimal(itemNe.VlrLiquidado);
                                    item.QuantidadeMaterialServico = Convert.ToDecimal(itemNe.QtdLiquidado);
                                    break;
                                case EnumTipoOperacaoEmpenho.Empenho:
                                case EnumTipoOperacaoEmpenho.Reforco:
                                case EnumTipoOperacaoEmpenho.Cancelamento:
                                case EnumTipoOperacaoEmpenho.Subempenho:

                                case EnumTipoOperacaoEmpenho.ProgramacaoDesembolso:
                                    item.ValorTotal = item.ValorTotal;
                                    item.QuantidadeMaterialServico = item.QuantidadeMaterialServico;
                                    break;
                                default:
                                    //item.ValorTotal = itemNe.SaldoCalculado * 100;
                                    //item.QuantidadeMaterialServico = itemNe.QuantidadeCalculada * 1000;
                                    item.ValorTotal = itemNe.SaldoCalculado;
                                    item.QuantidadeMaterialServico = itemNe.QuantidadeCalculada;
                                    break;
                            }
                        }
                    }
                }
            }

            return items;
        }

        private static void PreencherDadosFonte(ConsultaCt ct)
        {
            if (ct != null)
            {
                var fontes = App.FonteService.Buscar(new Fonte { Codigo = ct?.Fonte }).ToList();
                ct.Fonte = fontes.Any() ? fontes.FirstOrDefault().Codigo.ToString() : "0";
                ct.OrigemRecurso = fontes.Any() ? fontes.FirstOrDefault().Id.ToString() : "0"; 
            }
        }








        private static List<BaseEmpenhoItem> MontarBaseItem(ConsultaCt ct)
        {
            var descricoes = ct?.RepeteDescricao ?? new List<Descricao>();
            var items = new List<BaseEmpenhoItem>();

            foreach (var item in descricoes)
            {
                EmpenhoItem baseItem = new EmpenhoItem();

                baseItem.CodigoItemServico = item?.Item.Replace("-", "");
                baseItem.CodigoUnidadeFornecimentoItem = item?.UFor.ToString();
                //baseItem.QuantidadeMaterialServico = Convert.ToDecimal(item?.Quantidade.Replace(",", ""));
                //baseItem.ValorUnitario = Convert.ToDecimal(item?.ValorUnitario.Replace(",", ""));
                //baseItem.ValorTotal = Convert.ToDecimal(item?.PrecoTotal.Replace(",", ""));
                baseItem.QuantidadeMaterialServico = Convert.ToDecimal(item?.Quantidade);
                baseItem.ValorUnitario = Convert.ToDecimal(item?.ValorUnitario);
                baseItem.ValorTotal = Convert.ToDecimal(item?.PrecoTotal);
                baseItem.DescricaoJustificativaPreco = item?.Justificativa;
                baseItem.SequenciaItem = Convert.ToInt32(item?.Seq);
                baseItem.StatusSiafisicoItem = "S";

                items.Add(baseItem);
            }
            return items;
        }


        private static List<BaseEmpenhoItem> MontarBaseItem02(Subempenho ct)
        {
            var descricoes = ct.Itens;
            var items = new List<BaseEmpenhoItem>();

            foreach (var item in descricoes)
            {
                EmpenhoItem baseItem = new EmpenhoItem();

                baseItem.CodigoItemServico = item.CodigoItemServico.Replace("-", "");
                baseItem.CodigoUnidadeFornecimentoItem = item.CodigoUnidadeFornecimentoItem;
                baseItem.QuantidadeMaterialServico = Convert.ToDecimal(item.QuantidadeMaterialServico);
                //baseItem.ValorUnitario = Convert.ToDecimal(item?.ValorUnitario);
                baseItem.ValorTotal = Convert.ToDecimal(item.Valor);
                //baseItem.DescricaoJustificativaPreco = item?.Justificativa;
                baseItem.SequenciaItem = Convert.ToInt32(item.SequenciaItem);
                baseItem.StatusSiafisicoItem = "S";

                items.Add(baseItem);
            }
            return items;
        }


        [HttpGet]
        public JsonResult ConsultarPrecoNE(string NumeroNE)
        {
            try
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();
                var ct = App.CommonService.ConsultaPrecoNE(usuario, NumeroNE, "");
                return Json(ct, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult ConsultarEmpenhos(DtoEmpenhos dtoEmpenhos)
        {
            try
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                var empenhos = App.CommonService.ConsultaEmpenhos(
                    usuario,
                    dtoEmpenhos.CgcCpf,
                    dtoEmpenhos.Data,
                    dtoEmpenhos.Fonte,
                    dtoEmpenhos.Gestao,
                    dtoEmpenhos.UnidadeGestora,
                    dtoEmpenhos.GestaoCredor,
                    dtoEmpenhos.Licitacao,
                    dtoEmpenhos.Modalidade,
                    dtoEmpenhos.Natureza,
                    dtoEmpenhos.NumEmpenho,
                    dtoEmpenhos.Processo
                    );

                var result = new
                {
                    Status = "Sucesso",
                    Empenhos = empenhos.Repete
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ConsultarNe(string numNe)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                var empenho = new EmpenhoReforco { NumeroEmpenhoSiafem = (numNe) };

                ConsultaNe consultaNe = App.CommonService.ConsultaNe(empenho, usuario);

                int empenhoId = App.EmpenhoService.BuscarGrid(new Empenho { NumeroEmpenhoSiafem = numNe }).FirstOrDefault()?.Id ?? default(int);

                empenhoId = empenhoId == default(int)
                    ? App.EmpenhoService.BuscarGrid(new Empenho { NumeroEmpenhoSiafisico = numNe }).FirstOrDefault()?.Id ??
                      default(int)
                    : empenhoId;

                var empenhoFromDatabase = empenhoId > 0 ? App.EmpenhoService.Buscar(new Empenho { Id = empenhoId }).FirstOrDefault() : default(Empenho);

                var result = new
                {
                    Status = "Sucesso",
                    Empenho = consultaNe,
                    dadosDatabase = empenhoFromDatabase
                };

                //  var result = new { Status = "Sucesso", Empenho = consultaNe };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ConsultarSubempenhoApoio(Subempenho subempenho)
        {
            try
            {
                var consultaApoio = ExecuteConsultaApoio(subempenho);
                var _orgao = App.RegionalService.Buscar(new Regional());
                var _estrutura = App.EstruturaService.Buscar(new Estrutura());
                var _programa = App.ProgramaService.Buscar(new Programa()).Where(x => x.Ano == DateTime.Now.Year);


                //Alex - 05/05/2017 Adicionado tratamento para incluir o outOrigemRecurso e tratar o CFP
                ConsultaApoio _consultaApoio = new ConsultaApoio()
                {
                    outAplicObra = consultaApoio.outAplicObra,
                    outCED = consultaApoio.outCED,
                    outCFP = consultaApoio.outCFP.Substring(0, 17),
                    outOrigemRecurso = consultaApoio.outCFP.Substring(18, 2),
                    outCGC = consultaApoio.outCGC,
                    outContrato = consultaApoio.outContrato,
                    outNotaFiscal = consultaApoio.outNotaFiscal,
                    outCredor1 = consultaApoio.outCredor1,
                    outCredor2 = consultaApoio.outCredor2,
                    outErro = consultaApoio.outErro,
                    outInfoTransacao = consultaApoio.outInfoTransacao,
                    outNatureza = consultaApoio.outNatureza,
                    outNumMedicao = consultaApoio.outNumMedicao,
                    outOrganiz = consultaApoio.outOrganiz,
                    outOrgao = consultaApoio.outOrgao,
                    outSucesso = consultaApoio.outSucesso
                };

                _consultaApoio.outOrgao = Convert.ToString(_orgao.FirstOrDefault(f => f.Descricao.Contains(_consultaApoio.outOrgao))?.Id);

                _consultaApoio.outCED = Convert.ToString(_estrutura.FirstOrDefault(f => f.Natureza.Contains(_consultaApoio.outCED.Replace(".", string.Empty)))?.Codigo);

                //Alex - 05/05/2017 alterado para selecionar atraves do valor do combobox
                _consultaApoio.outCFP = Convert.ToString(_programa.FirstOrDefault(f => f.Cfp == _consultaApoio.outCFP.Replace(".", string.Empty).Replace("-", string.Empty).Substring(0, 13))?.Codigo);

                ////Alex - 05/05/2017 Adicionado pegar o Id da fonte confermo lógica já implementada

                //_consultaApoio.outOrigemRecurso = Convert.ToString(_fonte.FirstOrDefault(f => f.Codigo.Substring(1,2).Contains(_consultaApoio.outOrigemRecurso))?.Id);

                _consultaApoio.outAplicObra = _consultaApoio.outAplicObra.Replace(".", string.Empty);
                _consultaApoio.outCGC = _consultaApoio.outCGC.Replace("-", string.Empty);

                var result = new
                {
                    Status = "Sucesso",
                    SubempenhoApoio = _consultaApoio
                };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ConsultarAnulacaoApoio(SubempenhoCancelamento anulacao)
        {
            try
            {
                var consultaAnulacao = ExecuteConsultaAnulacaoApoio(anulacao);

                var result = new { Status = "Sucesso", SubempenhoCancelamento = consultaAnulacao };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarRapRequisicaoApoio(RapRequisicao entity)
        {
            try
            {
                var rapRequisicao = App.CommonService.ConsultarRapRequisicaoApoio(entity);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        RapRequisicao = rapRequisicao
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarRapAnulacaoApoio(string requisicaoAnulacaoRap)
        {
            try
            {
                return Json(new { Status = "Sucesso", RapAnulacao = App.CommonService.ConsultarRapAnulacaoApoio(requisicaoAnulacaoRap) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultaEmpenhoSaldoRAP(RapInscricao entity)
        {
            try
            {
                var empenhoSaldo = App.CommonService.ConsultaEmpenhoSaldo(entity);

                return Json(new { Status = "Sucesso", EmpenhoSaldo = empenhoSaldo }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarEmpenhoPorCredor(Subempenho subempenho)
        {
            try
            {
                var credores = App.CommonService.ConsultarEmpenhoCredor(subempenho);

                var result = new
                {
                    Status = "Sucesso",
                    Subempenho = credores
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ConsultarRap(string rap)
        {
            try
            {
                int? rapId = App.RapRequisicaoService.BuscarGrid(new RapRequisicao { NumeroProdesp = rap }).FirstOrDefault()?.Id;

                var entity = rapId == null ? null : App.RapRequisicaoService.Selecionar((int)rapId);

                if (entity != null)
                {
                    entity.NumeroProdesp = null;
                    entity.NumeroSiafemSiafisico = null;
                }

                var result = new
                {
                    Status = "Sucesso",
                    dataBaseEntity = entity
                };
                
                return Json(result, JsonRequestBehavior.AllowGet);
                
            }

            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        private static ConsultaApoio ExecuteConsultaApoio(Subempenho subempenho)
        {
            return TryConvert<ConsultaApoio>(App.CommonService.ConsultarSubEmpenhoApoio(subempenho));
        }

        private static ConsultaAnulacaoApoio ExecuteConsultaAnulacaoApoio(SubempenhoCancelamento anulacao)
        {
            return TryConvert<ConsultaAnulacaoApoio>(App.CommonService.ConsultarAnulacaoApoio(anulacao));
        }

        public ActionResult ConsultarDesdobramentoApoio(Desdobramento entity)
        {
            try
            {
                var desdobramento = App.CommonService.ConsultarDesdobramentoApoio(entity);
                var contrato = "";
                var obra = "";

                switch (entity.DocumentoTipoId)
                {
                    case 5:
                        var subempenho = App.SubempenhoService.BuscarGrid(new Subempenho { NumeroProdesp = entity.NumeroDocumento }).FirstOrDefault();
                        if (subempenho == null) break;
                        subempenho = App.SubempenhoService.Selecionar(subempenho?.Id ?? 0);
                        contrato = subempenho?.NumeroContrato;
                        obra = subempenho?.CodigoAplicacaoObra;
                        break;
                    case 11:
                        var rap = App.RapRequisicaoService.BuscarGrid(new RapRequisicao() { NumeroProdesp = entity.NumeroDocumento }).FirstOrDefault();
                        if (rap == null) break;
                        rap = App.RapRequisicaoService.Selecionar(rap?.Id ?? 0);
                        contrato = rap?.NumeroContrato;
                        obra = rap?.CodigoAplicacaoObra;
                        break;
                }


                return Json(
                    new
                    {
                        Status = "Sucesso",
                        Desdobramento = desdobramento,
                        NumeroContrato = contrato,
                        CodigoAplicacaoObra = obra
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarPreparacaoPgtoDocGerador(PreparacaoPagamento entity)
        {
            try
            {
                var preparacaoPagamento = App.CommonService.ConsultarPreparacaoPagtoDocGeradorApoio(entity);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        PreparacaoPagamento = preparacaoPagamento
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarPreparacaoPgtoTipoDespesaDataVenc(PreparacaoPagamento entity)
        {
            try
            {
                var preparacaoPagamento = App.CommonService.ConsultarPreparacaoPgtoTipoDespesaDataVenc(entity);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        PreparacaoPagamento = preparacaoPagamento
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarDocumento(Desdobramento entity)
        {
            try
            {
                var contrato = "";
                var obra = "";
                decimal? valor = 0;

                switch (entity.DocumentoTipoId)
                {
                    case 5:
                        var subempenho = App.SubempenhoService.BuscarGrid(new Subempenho { NumeroProdesp = entity.NumeroDocumento }).FirstOrDefault();
                        if (subempenho == null) break;
                        subempenho = App.SubempenhoService.Selecionar(subempenho?.Id ?? 0);
                        contrato = subempenho?.NumeroContrato;
                        obra = subempenho?.CodigoAplicacaoObra;
                        valor = subempenho?.ValorRealizado / (decimal)100; // GAMBETA o valor está vindo inteiro, é preciso trabalhar com decimal isso assim que possível.
                        break;
                    case 11:
                        var rap = App.RapRequisicaoService.BuscarGrid(new RapRequisicao() { NumeroProdesp = entity.NumeroDocumento }).FirstOrDefault();
                        if (rap == null) break;
                        rap = App.RapRequisicaoService.Selecionar(rap?.Id ?? 0);
                        contrato = rap?.NumeroContrato;
                        obra = rap?.CodigoAplicacaoObra;
                        valor = rap?.ValorRealizado / (decimal)100; // GAMBETA o valor está vindo inteiro, é preciso trabalhar com decimal isso assim que possível.
                        break;
                }


                return Json(
                    new
                    {
                        Status = "Sucesso",
                        NumeroContrato = contrato,
                        CodigoAplicacaoObra = obra,
                        Valor = valor
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarDesdobramento(Desdobramento entity)
        {
            try
            {
                var desdobramento = App.CommonService.ConsultaDesdobramento(entity);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        Desdobramento = desdobramento
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ConsultaDesdobramento()
        {
            try
            {

                return View();
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarNomeReduzido(string nome)
        {
    
            try
            {
                var credor = App.CredorService.Listar(new Credor());

                var credorSelecao = credor.Where(s => s.NomeReduzidoCredor == nome).FirstOrDefault();

                if (credorSelecao != null)
                {

                    if (credorSelecao.CpfCnpjUgCredor.Length < 14)
                        credorSelecao.CpfCnpjUgCredor = credorSelecao.CnpjCredor ?? string.Empty;


                    return Json(
                        new
                        {
                            Status = "Sucesso",
                            Credor = credorSelecao
                        },
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { Status = "Falha", Msg = "Credor não cadastrado no SIDS. Por favor atualize a tabela de Credores." };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarNomePrefeirura(string nome)
        {
            try
            {
                var credor = App.CredorService.Listar(new Credor());

                var credorSelecao = credor.Where(s => s.Prefeitura == nome).FirstOrDefault();

                if (credorSelecao.CpfCnpjUgCredor.Length < 14)
                    credorSelecao.CpfCnpjUgCredor = credorSelecao.CnpjCredor ?? string.Empty;

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        Credor = credorSelecao
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarDocumentoGerador(ProgramacaoDesembolso entity)
        {
            var mensagem = "";
            try
            {

                var programacaoDesembolso = App.CommonService.ConsultaDocumentoGerador(entity).ToList();

                if(entity.ProgramacaoDesembolsoTipoId == 2)
                    mensagem = App.CommonService.MsgDesprezados(entity);

                programacaoDesembolso.ForEach(x =>
                {
                    var firstOrDefault = RegionalList.FirstOrDefault(r => r.Descricao.Substring(2, 2) == x.Regional);
                    if (firstOrDefault != null)
                        x.RegionalId = (short)firstOrDefault.Id;
                });

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        ProgramacaoDesembolso = programacaoDesembolso,
                        mensagem = mensagem
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarCancelamentoOpApoio(ProgramacaoDesembolso programacaoDesembolso)
        {
            try
            {
                var objModel = App.CommonService.CancelamentoOpApoio(programacaoDesembolso);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        objModel = objModel.FirstOrDefault()
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarBloqueioOpApoio(ProgramacaoDesembolso programacaoDesembolso)
        {
            try
            {
                var objModel = App.CommonService.BloqueioOpApoio(programacaoDesembolso);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        objModel = objModel.FirstOrDefault()
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        //
        [HttpPost]
        public JsonResult ConsultarEmpenhoRap(RapRequisicao entity)
        {
            try
            {
                var rapRequisicao = App.CommonService.ConsultarEmpenhoRap(entity);

                var empenhos = App.CommonService.ConsultarEmpenho(entity.NumeroProdesp);




                ConsultaEmpenhoRap objModel = (ConsultaEmpenhoRap)rapRequisicao;

                objModel.outNrContrato = empenhos.OutNumContrato;
                objModel.outNrEmpenho = entity.NumeroProdesp;

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        RapRequisicao = objModel
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }











        private static T TryConvert<T>(object objA) where T : new()
        {
            var objB = new T();
            foreach (var match in from propertiesFromA in GetPropertiesFromClass(objA)
                                  from propertiesFromB in GetPropertiesFromClass(objB)
                                  where propertiesFromA.Name == propertiesFromB.Name && propertiesFromA.PropertyType == propertiesFromB.PropertyType
                                  select new { PropertiesFromA = propertiesFromA, PropertiesFromB = propertiesFromB })
            {
                match.PropertiesFromB.SetValue(objB, match.PropertiesFromA.GetValue(objA, null), null);
            }
            return objB;
        }

        private static IEnumerable<PropertyInfo> GetPropertiesFromClass(object obj)
        {
            return from prop in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                   where prop.CanRead && prop.CanWrite
                   select prop;
        }

        #region Classes Internas

        public class Contrato
        {
            public string NumContrato { get; set; }
            public string Type { get; set; }
        }
        public class ConsultaApoio
        {
            public string outAplicObra { get; set; }
            public string outCED { get; set; }
            public string outCFP { get; set; }
            public string outOrigemRecurso { get; set; }
            public string outCGC { get; set; }
            public string outCredor1 { get; set; }
            public string outInfoTransacao { get; set; }
            public string outOrganiz { get; set; }
            public string outOrgao { get; set; }
            public string outSucesso { get; set; }
            public string outErro { get; set; }
            public string outContrato { get; set; }
            public string outNotaFiscal { get; set; }
            public string outCredor2 { get; set; }
            public string outNatureza { get; set; }
            public string outNumMedicao { get; set; }
        }

        public class ConsultaCredor
        {
            public string outCGC { get; set; }
            public string outContrato { get; set; }
            public string outDisponivelSubEmpenhar { get; set; }
            public string outErro { get; set; }
            public string outLiqEmpenhado { get; set; }
            public string outLiqSubempenhado { get; set; }
            public string outNrEmpenho { get; set; }
            public string outOrganiz { get; set; }
            public string outSucesso { get; set; }
            // public virtual IEnumerable<ListConsultaEmpenhoCredor> ListConsultarEmpenhoCredor { get; set; }

            //ListConsultarEmpenhoCredor = result.Select(x => new ListConsultaEmpenhoCredor 
            //    }).ToList()
            //outNrEmpenho = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outNrEmpenho").GetValue(x).ToString(),
            //outContrato   = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outContrato").GetValue(x).ToString(),
            //outLiqEmpenhado  = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outLiqEmpenhado").GetValue(x).ToString(),
            //outLiqSubempenhado = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outLiqSubempenhado").GetValue(x).ToString(),
            //outDisponivelSubEmpenhar = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outDisponivelSubEmpenhar").GetValue(x).ToString()

        }

        public class ConsultaAnulacaoApoio
        {
            public string outDataRealizacao { get; set; }
            public string outErro { get; set; }
            public string outNumeroAnulacao { get; set; }
            public string outSaldoAnteriorAnul { get; set; }
            public string outSucesso { get; set; }
            public string outTerminal { get; set; }
            public string outTipoDocumento { get; set; }
            public string outValorRealizado { get; set; }
            public string outVencimento { get; set; }
            public string outInfoTransacao { get; set; }
        }
        public class DtoOc
        {
            public string Oc { get; set; }
            public string Ugo { get; set; }
            public string Ug { get; set; }
        }
        public class DtoEmpenhos
        {
            public string CgcCpf { get; set; }
            public string Data { get; set; }
            public string Fonte { get; set; }
            public string GestaoCredor { get; set; }
            public string Licitacao { get; set; }
            public string Modalidade { get; set; }
            public string Natureza { get; set; }
            public string NumEmpenho { get; set; }
            public string Processo { get; set; }
            public string Gestao { get; set; }
            public string UnidadeGestora { get; set; }
        }
        public class DtoEstrutura
        {
            public int AnoExercicio { get; set; }
            public short RegionalId { get; set; }
            public string Cfp { get; set; }
            public string Natureza { get; set; }
            public int Programa { get; set; }
            public string OrigemRecurso { get; set; }
            public string Processo { get; set; }
            public string Tipo { get; set; }

        }
        #endregion
    }
}