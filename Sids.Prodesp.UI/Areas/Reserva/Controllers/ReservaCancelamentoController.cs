using Sids.Prodesp.Model.ValueObject.Service.Siafem.Reserva;

namespace Sids.Prodesp.UI.Areas.Reserva.Controllers
{
    using Application;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface.Reserva;
    using Models;
    using Report;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using UI.Controllers.Base;
    public class ReservaCancelamentoController : ConsutasBaseController
    {
        private int _cancelamentoId;
        private List<ReservaCancelamento> _cancelamentos;

        public ReservaCancelamentoController()
        {
            _cancelamentos = new List<ReservaCancelamento>();
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _cancelamentoId = 0;
        }

        [PermissaoAcesso(Controller = typeof(ReservaCancelamentoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");


            App.PerfilService.SetCurrentFilter(null, "ReservaCancelamento");
            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(id));
            Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado();

            ViewBag.Usuario = userLogado;
            ViewBag.Filtro = new FiltroViewModel { RegionalId = userLogado.RegionalId == 1 ? 16 : (int)userLogado.RegionalId, AnoExercicio = (int)DateTime.Now.Year  }.GerarFiltro(new ReservaCancelamento());

            return View(_cancelamentos);
        }

        [PermissaoAcesso(Controller = typeof(ReservaCancelamentoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilAcaoService.SetCurrentFilter(f, "ReservaCancelamento");
                var obj = FiltrosDaPesquisa(f);

                _cancelamentos = App.ReservaCancelamentoService.BuscarGrid(obj as ReservaCancelamento).ToList();
                Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado();

                ViewBag.Usuario = userLogado;

                if (_cancelamentos.Count == 0)
                {
                    ExibirMensagemErro("Registro não encontrado.");
                }

                return View("Index", _cancelamentos);
            }

            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }

        }

        [PermissaoAcesso(Controller = typeof(ReservaCancelamentoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var entity = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Codigo = int.Parse(id) }).FirstOrDefault();
                if (entity.TransmitidoSiafem == true || entity.TransmitidoSiafisico == true || entity.TransmitidoProdesp == true)
                {
                    throw new Exception(string.Format("O Cancelamento foi transmitido, não é permitido excluir o Cancelamento após a transmissão"));
                }
                var result = App.ReservaCancelamentoService.Excluir(entity, (int)_funcId, (short)EnumAcao.Excluir).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Transmitir(DtoReservaCancelamentoSalvar dtoReservaCancelamentoSalvar)
        {
            try
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                _cancelamentoId = Salvar(dtoReservaCancelamentoSalvar,0);

                App.ReservaCancelamentoService.Transmitir(_cancelamentoId, usuario, (int)_funcId);  
                var cancelamento = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Codigo = _cancelamentoId }).FirstOrDefault();
                var result = new { Status = "Sucesso", cancelamento.Codigo, objModel = cancelamento };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status;

                var cancelamento = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Codigo = _cancelamentoId }).FirstOrDefault();



                if (cancelamento.StatusProdesp == "E" && cancelamento.TransmitirProdesp)
                    status = "Falha Prodesp";
                else if (cancelamento.StatusProdesp == "S" && cancelamento.StatusSiafemSiafisico == "S" && cancelamento.StatusDoc == false)
                    status = "Falha Doc";
                else status = "Falha";

                var result = new { Status = status, Msg = ex.Message, cancelamento.Codigo, objModel = cancelamento };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ReservaCancelamentoService.Transmitir(ids, usuario, (int)_funcId);
                var result = new { Status = (msg == "" ? "Sucesso" : "Falha"), Msg = msg };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [PermissaoAcesso(Controller = typeof(ReservaCancelamentoController), Operacao = "Incluir")]
        public ActionResult CreateThis(int id)
        {
            var userLogado = App.AutenticacaoService.GetUsuarioLogado();
            var entity = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Codigo = id }).FirstOrDefault();

            entity.Codigo = 0;
            ViewBag.Filtro = new ReservaCancelamentoViewModel().GerarViewModel(entity);
            ViewBag.Usuario = userLogado;

            NewReservaCancelamento(entity);

            return View("CreateEdit", entity);
        }

        public ActionResult Create(string id)
        {
            if (id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(id));

            var userLogado = App.AutenticacaoService.GetUsuarioLogado();
            var entity = new ReservaCancelamento
            {
                Regional = userLogado.RegionalId == 1 ? 16 : userLogado.RegionalId,
                Uo = "16055",
                DataEmissao = DateTime.Now,
                AnoExercicio = DateTime.Now.Year,
                Evento = 201100
            };

            GerarReservaCancelamentoCreate(ref entity);
            NewReservaCancelamento(entity);

            ViewBag.Filtro = new ReservaCancelamentoViewModel().GerarViewModel(entity);
            ViewBag.Usuario = userLogado;

            return View("CreateEdit", entity);
        }

        [PermissaoAcesso(Controller = typeof(ReservaCancelamentoController), Operacao = "Alterar")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                RedirectToAction("Index", "ReservaCancelamento");

            var userLogado = App.AutenticacaoService.GetUsuarioLogado();
            var entity = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento { Codigo = id }).FirstOrDefault();


            var msg = new List<string>
            {
                entity.MsgRetornoTransmissaoProdesp,
                entity.MsgRetornoTransSiafemSiafisico
            };

            if (!string.IsNullOrEmpty(entity.MsgRetornoTransmissaoProdesp) ||
                !string.IsNullOrEmpty(entity.MsgRetornoTransSiafemSiafisico))
            {
                ViewBag.MsgRetorno = string.Join("\n", msg.Where(x => x != null));
            }
            else
                ViewBag.MsgRetorno = null;


            ViewBag.Filtro = new ReservaCancelamentoViewModel().GerarViewModel(entity);
            ViewBag.Usuario = userLogado;

            return View("CreateEdit", entity);
        }

        [PermissaoAcesso(Controller = typeof(ReservaCancelamentoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("ReservaCancelamento");

                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _cancelamentos);
            }
        }

        private IReserva FiltrosDaPesquisa(FormCollection f)
        {
            IReserva obj = new ReservaCancelamento();
            IFiltroViewModel filtro = new FiltroViewModel().GerarFiltro(obj);

            ExtrairDadosFiltro(f, ref obj, ref filtro);
            ViewBag.Filtro = filtro;

            return obj;
        }

        private static void ExtrairDadosFiltro(FormCollection f, ref IReserva obj, ref IFiltroViewModel filtro)
        {
            if (!string.IsNullOrEmpty(f["Contrato"]))
            {
                obj.Contrato = f["Contrato"].Replace(".", "").Replace("-", "");
                filtro.Contrato = f["Contrato"];
            }

            if (!string.IsNullOrEmpty(f["Processo"]))
            {
                obj.Processo = f["Processo"];
                filtro.Processo = f["Processo"];
            }
            if (!string.IsNullOrEmpty(f["NumProdesp"]))
            {
                obj.NumProdesp = f["NumProdesp"];
                filtro.NumProdesp = f["NumProdesp"];
            }

            if (!string.IsNullOrEmpty(f["NumSiafemSiafisico"]))
            {
                obj.NumSiafemSiafisico = f["NumSiafemSiafisico"];
                filtro.NumSiafemSiafisico = f["NumSiafemSiafisico"];
            }

            if (!string.IsNullOrEmpty(f["Regional"]))
            {
                obj.Regional = short.Parse(f["Regional"]);
                filtro.RegionalId = short.Parse(f["Regional"]);
            }

            if (!string.IsNullOrEmpty(f["AnoExercicio"]))
            {
                obj.AnoExercicio = short.Parse(f["AnoExercicio"]);
                filtro.AnoExercicio = short.Parse(f["AnoExercicio"]);
            }

            if (!string.IsNullOrEmpty(f["Ptres"]))
            {
                obj.Programa = int.Parse(f["Ptres"]);  // passa para obj para buscar no banco
                filtro.Ptres = f["Ptres"];
            }

            if (!string.IsNullOrEmpty(f["Programa"]))
            {
                obj.Programa = int.Parse(f["Programa"]);    // passa para obj para buscar no banco
                filtro.Programa = int.Parse(f["Programa"]);
            }

            if (!string.IsNullOrEmpty(f["Natureza"]))
            {
                obj.Estrutura = int.Parse(f["Natureza"]);    // passa para obj para buscar no banco
                filtro.Natureza = int.Parse(f["Natureza"]);
            }

            if (!string.IsNullOrEmpty(f["StatusTransmitidoProdesp"]))
            {
                obj.StatusProdesp = f["StatusTransmitidoProdesp"];
                filtro.StatusProdesp = f["StatusTransmitidoProdesp"];
            }

            if (!string.IsNullOrEmpty(f["StatusSiafemSiafisico"]))
            {
                obj.StatusSiafemSiafisico = f["StatusSiafemSiafisico"];
                filtro.StatusSiafemSiafisico = f["StatusSiafemSiafisico"];
            }

            if (!string.IsNullOrEmpty(f["Obra"]))
            {
                obj.Obra = int.Parse(f["Obra"].Replace("-", ""));
                filtro.Obra = int.Parse(f["Obra"].Replace("-", ""));
            }

            if (!string.IsNullOrEmpty(f["DataInicial"]))
            {
                obj.DataEmissaoDe = Convert.ToDateTime(f["DataInicial"]);
                filtro.DataEmissaoDe = Convert.ToDateTime(f["DataInicial"]);
            }

            if (!string.IsNullOrEmpty(f["DataFinal"]))
            {
                obj.DataEmissaoAte = Convert.ToDateTime(f["DataFinal"]);
                filtro.DataEmissaoAte = Convert.ToDateTime(f["DataFinal"]);
            }
        }


        [HttpPost]
        public ActionResult Imprimir(int id)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                var cancelamento = App.ReservaCancelamentoService.Buscar(new ReservaCancelamento() { Codigo = id }).FirstOrDefault();

                ConsultaNr consultaNr = App.CommonService.ConsultaNr(cancelamento, usuario);

                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfReserva(consultaNr, "Cancelamento de Reserva", cancelamento); 

                var result = new { Status = "Sucesso" };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        #region Classes Internas
        public class DtoReservaCancelamentoSalvar
        {
            public ReservaCancelamento Cancelamento { get; set; }
            public List<ReservaCancelamentoMes> CancelamentoMes { get; set; }
        }
        #endregion

        #region Métodos Privados

        private static void GerarReservaCancelamentoCreate(ref ReservaCancelamento entity)
        {

            ReservaCancelamento entitySelected = App.ReservaCancelamentoService.BuscarAssinaturas(entity);

            if (entitySelected != null && entitySelected.Codigo > 0)
            {
                entity.AutorizadoAssinatura = entitySelected.AutorizadoAssinatura;
                entity.AutorizadoGrupo = entitySelected.AutorizadoGrupo;
                entity.AutorizadoOrgao = entitySelected.AutorizadoOrgao;
                entity.NomeAutorizadoAssinatura = entitySelected.NomeAutorizadoAssinatura;
                entity.AutorizadoCargo = entitySelected.AutorizadoCargo;
                entity.ExaminadoAssinatura = entitySelected.ExaminadoAssinatura;
                entity.ExaminadoGrupo = entitySelected.ExaminadoGrupo;
                entity.ExaminadoOrgao = entitySelected.ExaminadoOrgao;
                entity.NomeExaminadoAssinatura = entitySelected.NomeExaminadoAssinatura;
                entity.ExaminadoCargo = entitySelected.ExaminadoCargo;
                entity.ResponsavelAssinatura = entitySelected.ResponsavelAssinatura;
                entity.ResponsavelGrupo = entitySelected.ResponsavelGrupo;
                entity.ResponsavelOrgao = entitySelected.ResponsavelOrgao;
                entity.NomeResponsavelAssinatura = entitySelected.NomeResponsavelAssinatura;
                entity.ResponsavelCargo = entitySelected.ResponsavelCargo;
            }
        }

        private static void NewReservaCancelamento(ReservaCancelamento entity)
        {
            entity.Codigo = 0;
            entity.NumProdesp = null;
            entity.NumSiafemSiafisico = null;
            entity.DataCadastro = null;
            entity.DataTransmissaoProdesp = null;
            entity.DataTransmissaoSiafemSiafisico = null;
            entity.TransmitidoProdesp = false;
            entity.TransmitidoSiafisico = false;
            entity.TransmitidoSiafem = false;
            entity.TransmitirProdesp = false;
            entity.TransmitirSiafisico = false;
            entity.TransmitirSiafem = false;
        }

        [HttpPost]
        public ActionResult Save(DtoReservaCancelamentoSalvar dtoReservaCancelamentoSalvar)
        {
            try
            {
                var result = new { Status = "Sucesso", Id = Salvar(dtoReservaCancelamentoSalvar, (int)_funcId) };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private int Salvar(DtoReservaCancelamentoSalvar dtoReservaCancelamentoSalvar,int funcId)
        {
            var enumAcao = dtoReservaCancelamentoSalvar.Cancelamento.Codigo > 0
                ? EnumAcao.Alterar
                : EnumAcao.Inserir;

            return App.ReservaCancelamentoService.Salvar(
                dtoReservaCancelamentoSalvar.Cancelamento,
                dtoReservaCancelamentoSalvar.CancelamentoMes ?? new List<ReservaCancelamentoMes>(),
                funcId,
                (short)enumAcao);
        }

        #endregion
    }
}