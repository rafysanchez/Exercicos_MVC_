using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.ValueObject;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.UI.Areas.PagamentoContaDer.Models;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;


namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Controllers
{
    public class PagamentoContaDerBaseController : BaseController
    {
        protected ArquivoRemessaFiltroGridViewModel _filterItems;
        protected readonly Usuario _userLoggedIn;
        protected int _modelId;
        protected static readonly IEnumerable<Regional> RegionalList = App.RegionalService.Buscar(new Regional()) ?? new List<Regional>();



        public PagamentoContaDerBaseController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _userLoggedIn = App.AutenticacaoService.GetUsuarioLogado();
            _modelId = 0;
        }


        protected T InitializeEntityModel<T>(T objModel)
        {
            var model = new object();

            if (objModel is ArquivoRemessa)
            {
                var arquivoRemessa = (ArquivoRemessa)Convert.ChangeType(objModel, typeof(ArquivoRemessa));
               arquivoRemessa.NumeroGeracao = null; 

                arquivoRemessa.Id = default(int);
                arquivoRemessa.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                //arquivoRemessa.CodigoConta = default(int);
                //arquivoRemessa.CodigoAssinatura = default(int);
                //arquivoRemessa.CodigoOrgaoAssinatura = default(int);
                //arquivoRemessa.CodigoGrupoAssinatura = default(int);
                //arquivoRemessa.NomeAssinatura = default(string);
                //arquivoRemessa.DesCargo = default(string);
                //arquivoRemessa.CodigoContraAssinatura = default(int);
                //arquivoRemessa.CodigoContraOrgaoAssinatura = default(int);
                //arquivoRemessa.CodigoContraGrupoAssinatura = default(int);
                //arquivoRemessa.NomeContraAssinatura = default(string);
                //arquivoRemessa.DesContraCargo = default(string);
                //arquivoRemessa.Banco = default(string);
                //arquivoRemessa.Agencia = default(string);
                //arquivoRemessa.NumeroConta = default(string);

                //arquivoRemessa.QtDeposito = default(int);
                //arquivoRemessa.QtOpArquivo = default(int);
                //arquivoRemessa.QtDocTed = default(int);
                //arquivoRemessa.ValorTotal =  default(int);


                arquivoRemessa.DataCadastro = DateTime.Now;
                arquivoRemessa.StatusProdesp = "N";
                arquivoRemessa.Cancelado = false;
                arquivoRemessa.CadastroCompleto = false;
                arquivoRemessa.TransmitirProdesp = true;
                arquivoRemessa.TransmitidoProdesp = false;         
        

                model = arquivoRemessa;
            }

            if (objModel is ConfirmacaoPagamento)
            {
                var ConfirmacaoPagamento = (ConfirmacaoPagamento)Convert.ChangeType(objModel, typeof(ConfirmacaoPagamento));
                ConfirmacaoPagamento.Id = default(int);
                ConfirmacaoPagamento.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;
                ConfirmacaoPagamento.DataCadastro = DateTime.Now;
                ConfirmacaoPagamento.StatusProdesp = "N";
                ConfirmacaoPagamento.TransmitidoProdesp = false;
                model = ConfirmacaoPagamento;
            }
            return (T)model;
        }
        //protected List<ConfirmacaoPagamentoFiltroViewModel> Display(ConfirmacaoPagamento entity)
        //{
        //    List<ConfirmacaoPagamentoFiltroViewModel> ret = new List<ConfirmacaoPagamentoFiltroViewModel>();

        //    InitializeCommonBags(entity);
        //    return ret;
        //}

        //protected IEnumerable<FiltroGridViewModel> Display(ConfirmacaoPagamento entity, FormCollection form)
        //{
        //    IEnumerable<ConfirmacaoPagamento> entities = new List<ConfirmacaoPagamento>();

        //    var model = GenerateFilterViewModel(form, entity);

        //    return InitializeFiltroGridViewModel(entities);
        //}
        protected IEnumerable<ArquivoRemessaFiltroGridViewModel> Display(ArquivoRemessa entity)
        {
            var filterModel = InitializeEntityModel(entity);

            var arquivoremessa = (ArquivoRemessa)Convert.ChangeType(filterModel, typeof(ArquivoRemessa));
            ViewBag.Filtro = InitializeFiltroViewModel(arquivoremessa);
            ViewBag.Usuario = _userLoggedIn;

            return new List<ArquivoRemessaFiltroGridViewModel>();
        }
        

        protected ArquivoRemessa InitializeEntityModel(ArquivoRemessa objModel)
        {
            var model = new ArquivoRemessa();
            model.Id = default(int);
            model.DataPreparacao = default(DateTime);
            model.DataTrasmitido = default(DateTime);
            model.DataCadastro = default(DateTime);
            model.StatusProdesp  = default(string); 
            model.DataPagamento= default(DateTime);
            model.ValorTotal= default(int);
            model.RegionalId = _userLoggedIn.RegionalId == 1 ? 16 : (short)_userLoggedIn.RegionalId;//verificar regional id BD e salvar
            return  model;
        }




        protected IEnumerable<ArquivoRemessaFiltroGridViewModel> Display(ArquivoRemessa entity, FormCollection form)
        {

            IEnumerable<ArquivoRemessa> entities = new List<ArquivoRemessa>();

            var model = GenerateFilterViewModel(form, entity);

            var inclusao = (ArquivoRemessa)Convert.ChangeType(model, typeof(ArquivoRemessa));
 
            entities = App.ArquivoRemessaService.BuscarGrid(inclusao, Convert.ToDateTime(((ArquivoRemessaFiltroViewModel)ViewBag.Filtro).DataCadastroDe), Convert.ToDateTime(((ArquivoRemessaFiltroViewModel)ViewBag.Filtro).DataCadastroAte));

            return InitializeFiltroGridViewModel(entities);
        }

        protected ArquivoRemessa GenerateFilterViewModel(FormCollection form, ArquivoRemessa entity)
        {
            ArquivoRemessaFiltroViewModel filter;

            filter = InitializeFiltroViewModel(entity);

            entity = MapViewModelToEntityModel(form, entity,ref filter);

            ViewBag.Filtro = filter;
            ViewBag.Usuario = _userLoggedIn;

            return entity;
        }


        private ArquivoRemessa MapViewModelToEntityModel(FormCollection form, ArquivoRemessa entity ,ref ArquivoRemessaFiltroViewModel viewModel)
        {

            if (!string.IsNullOrEmpty(form["NumeroGeracao"]))
            {
                entity.NumeroGeracao = Convert.ToInt32(form["NumeroGeracao"]);
               viewModel.NumeroGeracao = Convert.ToInt32(form["NumeroGeracao"]);
            }

            if (!string.IsNullOrEmpty(form["RegionalId"]))
            {
                entity.RegionalId = Convert.ToInt32(form["RegionalId"]);
                viewModel.RegionalId = Convert.ToInt32(form["RegionalId"]);

            }

            if (!string.IsNullOrEmpty(form["CodigoConta"]))
            {
                entity.CodigoConta = Convert.ToInt32(form["CodigoConta"]);
                viewModel.CodigoConta = Convert.ToInt32(form["CodigoConta"]);
            }

            if (!string.IsNullOrEmpty(form["TransmitidoProdesp"]))
            {
                entity.StatusProdesp = form["TransmitidoProdesp"];
                viewModel.TransmitidoProdesp = form["TransmitidoProdesp"];
            }

            if (!string.IsNullOrEmpty(form["Cancelado"]))
            {
                entity.Cancelado = form["Cancelado"] == "1" ? true : false;
                viewModel.Cancelado = form["Cancelado"] ;
            }

            if (!string.IsNullOrEmpty(form["DataCadastroDe"]))
            {
                entity.DataCadastro = Convert.ToDateTime(form["DataCadastroDe"]);
                viewModel.DataCadastroDe = Convert.ToDateTime( form["DataCadastroDe"]);
            }

            if (!string.IsNullOrEmpty(form["DataCadastroAte"]))
            {
                entity.DataCadastro = Convert.ToDateTime(form["DataCadastroAte"]);
                viewModel.DataCadastroAte = Convert.ToDateTime( form["DataCadastroAte"]);
            }
            return entity;
        }

        private IEnumerable<ArquivoRemessaFiltroGridViewModel> InitializeFiltroGridViewModel(IEnumerable<ArquivoRemessa> entities)
        {
            List<ArquivoRemessaFiltroGridViewModel> items = new List<ArquivoRemessaFiltroGridViewModel>();

            foreach (var entity in entities)
            {
                items.Add(new ArquivoRemessaFiltroGridViewModel().CreateInstance(entity));
            }

            return items;
        }

        private ArquivoRemessaFiltroViewModel InitializeFiltroViewModel(ArquivoRemessa entity)
        {
            return new ArquivoRemessaFiltroViewModel().CreateInstance(entity, RegionalList, new DateTime(), new DateTime());
        }

        protected void InitializeCommonBags<T>(T entity)
        {
            ViewBag.Usuario = _userLoggedIn;
        }

        protected T Display<T>(T objModel, bool isNewRecord)
        {
            if (isNewRecord)
            {
                objModel = InitializeEntityModel(objModel);
            }


            if (objModel is ArquivoRemessa)
            {
                var arquivoRemessa = (ArquivoRemessa)Convert.ChangeType(objModel, typeof(ArquivoRemessa));
                var msg = new List<string>
                {
                    arquivoRemessa.MensagemServicoProdesp,
                };

                ViewBag.MsgRetorno = !string.IsNullOrEmpty(arquivoRemessa.MensagemServicoProdesp) ? string.Join("\n", msg.Where(x => x != null)) : null;
                ViewBag.Usuario = _userLoggedIn;

                ViewBag.DadoPreparacaoPagamentoContas = InitializeDadoPagamentoContaUnicaContasViewModel(arquivoRemessa);
                ViewBag.DadoAssinatura = InitializeDadoAssinaturaViewModel(arquivoRemessa);

            }
                return objModel;
        }

        protected DadoAssinaturaViewModel InitializeDadoAssinaturaViewModel(ArquivoRemessa entity)
        {
            return GetSignaturesFromDomainModel(entity);
        }

        protected DadoPagamentoContaUnicaContasViewModel InitializeDadoPagamentoContaUnicaContasViewModel(ArquivoRemessa entity)
        {
            return new DadoPagamentoContaUnicaContasViewModel().CreateInstance(entity);
        }

        protected DadoAssinaturaViewModel GetSignaturesFromDomainModel(ArquivoRemessa entity)
        {

            var autorizado = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoAssinatura,
                CodigoGrupo = Convert.ToInt32(entity.CodigoGrupoAssinatura),
                CodigoOrgao = entity.CodigoOrgaoAssinatura,
                NomeAssinatura = entity.NomeAssinatura,
                DescricaoCargo = entity.DesCargo
            };

            var examinado = new Assinatura()
            {
                CodigoAssinatura = entity.CodigoContraAssinatura,
                CodigoGrupo = Convert.ToInt32(entity.CodigoContraGrupoAssinatura),
                CodigoOrgao = entity.CodigoContraOrgaoAssinatura,
                NomeAssinatura = entity.NomeContraAssinatura,
                DescricaoCargo = entity.DesContraCargo
            };

            return new  DadoAssinaturaViewModel().CreateInstance(autorizado, examinado, new Assinatura { });
        }

        public ActionResult ConsultarArquivoTipoDataVenc2(ArquivoRemessa entity)
        {
            try
            {
                var arquivoRemessa = App.CommonService.ConsultarArquivoTipoDataVenc2(entity);

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        ArquivoRemessa = arquivoRemessa
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ConsultarCancelamentoArquivo(ArquivoRemessa arquivoremessa)
        {
            try
            {
                var objModel = App.ArquivoRemessaService.Selecionar(Convert.ToInt32(arquivoremessa.Id));

                return Json(
                    new
                    {
                        Status = "Sucesso",
                        objModel = objModel
                    },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}