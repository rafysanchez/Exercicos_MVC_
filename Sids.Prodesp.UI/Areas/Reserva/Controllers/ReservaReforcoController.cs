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
    public class ReservaReforcoController : ConsutasBaseController
    {
        private int _reforcoId;
        private List<ReservaReforco> _reforcos;

        public ReservaReforcoController()
        {
            _reforcos = new List<ReservaReforco>();
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _reforcoId = 0;
        }

        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            App.PerfilService.SetCurrentFilter(null, "ReservaReforco");
            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(id));
            Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado();

            ViewBag.Usuario = userLogado;
            ViewBag.Filtro = new FiltroViewModel { RegionalId = userLogado.RegionalId == 1 ? 16 : (int)userLogado.RegionalId, AnoExercicio = DateTime.Now.Year}.GerarFiltro(new ReservaReforco());

            return View(_reforcos);
        }

        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilAcaoService.SetCurrentFilter(f, "ReservaReforco");
                var obj = FiltrosDaPesquisa(f);

                _reforcos = App.ReservaReforcoService.BuscarGrid(obj as ReservaReforco).ToList();
                Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado();

                ViewBag.Usuario = userLogado;

                if (_reforcos.Count == 0)
                {
                    ExibirMensagemErro("Registro não encontrado.");
                }

                return View("Index", _reforcos);
            }

            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }

        }

        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var entity = App.ReservaReforcoService.Buscar(new ReservaReforco { Codigo = int.Parse(id) }).FirstOrDefault();
                if (entity.TransmitidoSiafem == true || entity.TransmitidoSiafisico == true || entity.TransmitidoProdesp == true)
                {
                    throw new Exception(string.Format("O Reforço foi transmitido, não é permitido excluir o Reforço após a transmissão"));
                }
                var result = App.ReservaReforcoService.Excluir(entity, (int)_funcId, (short)EnumAcao.Excluir).ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Transmitir(DtoReservaReforcoSalvar dtoReservaReforcoSalvar)
        {
            try
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                _reforcoId = Salvar(dtoReservaReforcoSalvar,0);

                App.ReservaReforcoService.Transmitir(_reforcoId, usuario, (int)_funcId); 
                var reforco = App.ReservaReforcoService.Buscar(new ReservaReforco { Codigo = _reforcoId }).FirstOrDefault();
                var result = new { Status = "Sucesso", reforco.Codigo, objModel = reforco };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status;

                var reforco = App.ReservaReforcoService.Buscar(new ReservaReforco { Codigo = _reforcoId }).FirstOrDefault();


                if (reforco.StatusProdesp == "E" && reforco.TransmitirProdesp)
                    status = "Falha Prodesp";
                else if (reforco.StatusProdesp == "S" && reforco.StatusSiafemSiafisico == "S" && reforco.StatusDoc == false)
                    status = "Falha Doc";
                else status = "Falha";

                var result = new { Status = status, Msg = ex.Message, reforco.Codigo, objModel = reforco };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ReservaReforcoService.Transmitir(ids, usuario, (int)_funcId);
                var result = new { Status = (msg == "" ? "Sucesso" : "Falha"), Msg = msg };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Incluir")]
        public ActionResult CreateThis(int id)
        {
            var userLogado = App.AutenticacaoService.GetUsuarioLogado();
            var entity = App.ReservaReforcoService.Buscar(new ReservaReforco { Codigo = id }).FirstOrDefault();

            entity.Codigo = 0;
            ViewBag.Filtro = new ReservaReforcoViewModel().GerarViewModel(entity);
            ViewBag.Usuario = userLogado;

            NewReservaReforco(entity);

            return View("CreateEdit", entity);
        }

        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(id));

            var userLogado = App.AutenticacaoService.GetUsuarioLogado();
            var entity = new ReservaReforco
            {
                Regional = userLogado.RegionalId == 1 ? 16 : userLogado.RegionalId,
                Uo = "16055",
                DataEmissao = DateTime.Now,
                AnoExercicio = DateTime.Now.Year,
                Evento = 201100
            };

            GerarReservaReforcoCreate(ref entity);
            NewReservaReforco(entity);

            ViewBag.Filtro = new ReservaReforcoViewModel().GerarViewModel(entity);
            ViewBag.Usuario = userLogado;

            return View("CreateEdit", entity);
        }

        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Alterar")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                RedirectToAction("Index", "ReservaReforco");

            var userLogado = App.AutenticacaoService.GetUsuarioLogado();
            var entity = App.ReservaReforcoService.Buscar(new ReservaReforco { Codigo = id }).FirstOrDefault();


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

            ViewBag.Filtro = new ReservaReforcoViewModel().GerarViewModel(entity);
            ViewBag.Usuario = userLogado;

            return View("CreateEdit", entity);
        }

        [PermissaoAcesso(Controller = typeof(ReservaReforcoController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ProgramaService.GetCurrentFilter("ReservaReforco");

                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _reforcos);
            }
        }

        private IReserva FiltrosDaPesquisa(FormCollection f)
        {
            IReserva obj = new ReservaReforco();
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

                var reforco = App.ReservaReforcoService.Buscar(new ReservaReforco() { Codigo = id }).FirstOrDefault();

                ConsultaNr consultaNr = App.CommonService.ConsultaNr(reforco, usuario);

                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfReserva(consultaNr, "Reforço de Reserva", reforco);

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
        public class DtoReservaReforcoSalvar
        {
            public ReservaReforco Reforco { get; set; }
            public List<ReservaReforcoMes> ReforcoMes { get; set; }
        }
        #endregion

        #region Métodos Privados

        private static void GerarReservaReforcoCreate(ref ReservaReforco entity)
        {

            ReservaReforco entitySelected = App.ReservaReforcoService.BuscarAssinaturas(entity);

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

        private static void NewReservaReforco(ReservaReforco entity)
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
        public ActionResult Save(DtoReservaReforcoSalvar dtoReservaReforcoSalvar)
        {
            try
            {
                var result = new { Status = "Sucesso", Id = Salvar(dtoReservaReforcoSalvar,(int)_funcId) };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        private int Salvar(DtoReservaReforcoSalvar dtoReservaReforcoSalvar,int funcId)
        {
            var enumAcao = dtoReservaReforcoSalvar.Reforco.Codigo > 0
                ? EnumAcao.Alterar
                : EnumAcao.Inserir;

            return App.ReservaReforcoService.Salvar(
                dtoReservaReforcoSalvar.Reforco,
                dtoReservaReforcoSalvar.ReforcoMes?? new List<ReservaReforcoMes>(),
                funcId,
                (short)enumAcao);
        }
        
        #endregion
    }
}