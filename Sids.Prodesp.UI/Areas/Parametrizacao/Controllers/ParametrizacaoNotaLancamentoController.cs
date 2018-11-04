using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.UI.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sids.Prodesp.UI.Areas.Parametrizacao.Models;
using Sids.Prodesp.Model.ValueObject;

namespace Sids.Prodesp.UI.Areas.Parametrizacao.Controllers
{
    public class ParametrizacaoNotaLancamentoController : BaseController
    {
        public ParametrizacaoNotaLancamentoController()
        {
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
        }

        public ActionResult Index(int id)
        {
            try
            {
                var model = new NlParametrizacaoViewModel();

                ViewBag.TiposNl = App.NlParametrizacaoService.TiposNl(id);
                ViewBag.TiposDespesa = App.NlParametrizacaoService.DespesaTipos(id);
                ViewBag.TiposRap = App.NlParametrizacaoService.TiposRap(id);
                ViewBag.TiposDocumento = App.NlParametrizacaoService.TiposDocumento(id);
                ViewBag.TipoFormaGerarNl = App.NlParametrizacaoService.TipoFormaGerarNl(id);

                return View("CreateEdit", model);
            }
            catch
            {
                return RedirectToAction("Index", "Home", new { Area = string.Empty });
            }
        }

        public ActionResult VerificarTipoDespesa(int tiponl, int codigo)
        {
            ResultadoOperacaoObjetoVo<ConsultaDespesaNlVo> resposta = null;

            try
            {
                resposta = App.NlParametrizacaoService.VerificarTipoDespesa(tiponl, codigo, _funcId);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Atualizar(NlParametrizacaoViewModel vm)
        {
            ResultadoOperacaoVo resposta = new ResultadoOperacaoVo();

            try
            {
                var entity = vm.ToEntity();
                var retorno = App.NlParametrizacaoService.Salvar(entity, _funcId);

                if (retorno == Model.Enum.AcaoEfetuada.Sucesso)
                {
                    resposta.Sucesso = true;
                }
                else
                {
                    resposta.Sucesso = false;
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Selecionar(int idTipoNL)
        {
            ResultadoOperacaoObjetoVo<NlParametrizacaoViewModel> resposta = new ResultadoOperacaoObjetoVo<NlParametrizacaoViewModel>();

            try
            {
                var retorno = App.NlParametrizacaoService.Selecionar(new NlParametrizacao() { Tipo = idTipoNL });

                if (retorno == null)
                {
                    resposta.Sucesso = false;
                    resposta.Mensagem = "Registro não encontrado";
                }
                else
                {
                    resposta.Sucesso = true;
                    resposta.Objeto = new NlParametrizacaoViewModel(retorno);
                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, ex.Message);
            }

            return Json(resposta, JsonRequestBehavior.AllowGet);
        }
    }
}