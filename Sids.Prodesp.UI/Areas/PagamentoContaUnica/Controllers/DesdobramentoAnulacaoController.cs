using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.UI.Security;
using System;
using System.Web.Mvc;
using Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers.Base;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Controllers
{
    public class DesdobramentoAnulacaoController : PagamentoContaUnicaController
    {
        string tipoDesdobramento = "Cancelamento";

        [PermissaoAcesso(Controller = typeof(DesdobramentoAnulacaoController), Operacao = "Incluir")]
        public ActionResult Cancelar(string id)
        {
            var objModel = App.DesdobramentoService.Selecionar(Convert.ToInt32(id));
            return View("Cancelar", Display(objModel, true, tipoDesdobramento));
        }




        public JsonResult Transmitir(Desdobramento desdobramento)
        {
            Desdobramento objModel;
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                desdobramento.RegionalId = usuario.RegionalId == 1 ? 16 : (short)usuario.RegionalId;
                objModel = App.DesdobramentoService.Selecionar(desdobramento.Id);
                ModelId = objModel.Id;
                App.DesdobramentoService.TransmitirAnulacaoProdesp(objModel, (int)_funcId);
                objModel = App.DesdobramentoService.Selecionar(ModelId);

                return Json(new { Status = "Sucesso", Codigo = objModel.Id, objModel });

            }
            catch (Exception ex)
            {
                objModel = App.DesdobramentoService.Selecionar(ModelId);
                return Json(new { Status = "Falha", Msg = ex.Message, Codigo = objModel.Id, objModel });
            }
        }


        //private Desdobramento SalvarService(Desdobramento desdobramento, int funcionalidade)
        //{
        //    new Desdobramento { Id = App.DesdobramentoService.SalvarOuAlterar(desdobramento, funcionalidade, Convert.ToInt16(EnumAcao.Alterar)) };

        //    return desdobramento;
        //}


        public ActionResult Edit(string id)
        {
            var objModel = App.DesdobramentoService.Selecionar(Convert.ToInt32(id));
            return View("Cancelar", Display(objModel, true, tipoDesdobramento));
        }



    }
}