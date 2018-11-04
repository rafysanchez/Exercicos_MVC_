using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.Reserva;
using Sids.Prodesp.UI.Areas.Reserva.Models;
using Sids.Prodesp.UI.Controllers.Base;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.UI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Sids.Prodesp.UI.Areas.Reserva.Controllers
{
    using Model.Interface.Reserva;
    using Sids.Prodesp.Model.Entity.Reserva;
    public class ReservaController : ConsutasBaseController
    {
        private int _reservaId;
        private List<Reserva> _reservas;
        public ReservaController()
        {
            _reservas = new List<Reserva>();
            _funcId = App.FuncionalidadeService.ObterFuncionalidadeAtual();
            _reservaId = 0;
        }

        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Listar")]
        public ActionResult Index(string id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");


            App.PerfilService.SetCurrentFilter(null, "Reserva");
            App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(id));

            Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado(); // obtem o usuario logado , utilizado para desabilitar combo caso nao seja adm mestre
            ViewBag.Usuario = userLogado;
            ViewBag.Filtro = new FiltroViewModel { RegionalId = userLogado.RegionalId == 1 ? 16 : (int)userLogado.RegionalId, AnoExercicio = DateTime.Now.Year }.GerarFiltro(new Reserva());

            return View(_reservas);
        }

        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Listar"), HttpPost]
        public ActionResult Index(FormCollection f)
        {
            try
            {
                App.PerfilService.SetCurrentFilter(f, "Reserva");
                var obj = FiltrosDaPesquisa(f);  //cria o filtro com os dados fornecidos pelo usario na tela

                _reservas = App.ReservaService.BuscarGrid(obj as Reserva).ToList();

                Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado(); // obtem o usuario logado , utilizado para desabilitar combo caso nao seja adm mestre
                ViewBag.Usuario = userLogado;

                if (_reservas.Count == 0)
                {
                    ExibirMensagemErro("Registro não encontrado.");
                }
                _reservas.ForEach(x => x.ValorMes = x.ValorMes / 100);

                return View("Index", new List<Reserva>(_reservas));
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index");
            }
        }


        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Incluir")]
        public ActionResult Create(string id)
        {
            if (id != null)
                App.FuncionalidadeService.SalvarFuncionalidadeAtual(int.Parse(id));

            Usuario userLogado = App.AutenticacaoService.GetUsuarioLogado();
            Reserva reserva = new Reserva
            {
                Regional = userLogado.RegionalId == 1 ? 16 : userLogado.RegionalId,
                Uo = "16055",
                DataEmissao = DateTime.Now,
                AnoExercicio = DateTime.Now.Year,
                Evento = 201100
            };

            GerarReservaCreate(ref reserva);

            NewReserva(reserva);


            ViewBag.Filtro = new ReservaViewModel().GerarViewModel(reserva);
            ViewBag.Usuario = userLogado;
            return View("CreateEdit", reserva);

        }

        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Excluir")]
        public ActionResult Delete(string id)
        {
            try
            {
                var reserva = App.ReservaService.Buscar(new Reserva { Codigo = int.Parse(id) }).FirstOrDefault();
                if (reserva.TransmitidoSiafem == true || reserva.TransmitidoSiafisico == true || reserva.TransmitidoProdesp == true)
                {
                    throw new Exception(string.Format("A Reserva não pode ser excluída, pois foi transmitida"));
                }
                var result = App.ReservaService.Excluir(reserva, (int)_funcId, (short)EnumAcao.Excluir).ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = ex.Message.ToString();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Alterar")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                RedirectToAction("Index", "Reserva");

            var userLogado = App.AutenticacaoService.GetUsuarioLogado();

            var reserva = App.ReservaService.Buscar(new Reserva { Codigo = id }).FirstOrDefault();

            ViewBag.Filtro = new ReservaViewModel().GerarViewModel(reserva);
            
            var msg = new List<string>
            {
                reserva.MsgRetornoTransmissaoProdesp,
                reserva.MsgRetornoTransSiafemSiafisico
            };
            
            if (!string.IsNullOrEmpty(reserva.MsgRetornoTransmissaoProdesp) ||
                !string.IsNullOrEmpty(reserva.MsgRetornoTransSiafemSiafisico))
            {
                ViewBag.MsgRetorno = string.Join("\n", msg.Where(x => x != null));
            }
            else
                ViewBag.MsgRetorno = null;


            ViewBag.Filtro = new ReservaViewModel().GerarViewModel(reserva);
            ViewBag.Usuario = userLogado;


            return View("CreateEdit", reserva);

        }


        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Incluir")]
        public ActionResult CreateThis(int id)
        {
            var userLogado = App.AutenticacaoService.GetUsuarioLogado();

            var reserva = App.ReservaService.Buscar(new Reserva { Codigo = id }).FirstOrDefault();
            reserva.Codigo = 0;
            ViewBag.Filtro = new ReservaViewModel().GerarViewModel(reserva);
            ViewBag.Usuario = userLogado;

            NewReserva(reserva);

            return View("CreateEdit", reserva);

        }


        [HttpPost]
        public ActionResult Save(DtoReservaSalvar dtoReservaSalvar)
        {
            try
            {
                var result = new { Status = "Sucesso", Id = Salvar(dtoReservaSalvar,(int)_funcId) };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Transmitir(DtoReservaSalvar dtoReservaSalvar)
        {
            try
            {
                Usuario usuario = App.AutenticacaoService.GetUsuarioLogado();

                _reservaId = Salvar(dtoReservaSalvar,0);

                App.ReservaService.Transmitir(_reservaId, usuario, (int)_funcId);

                var reserva = App.ReservaService.Buscar(new Reserva { Codigo = _reservaId }).FirstOrDefault();

                var result = new { Status = "Sucesso", reserva.Codigo, objModel = reserva };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string status;

                var reserva = App.ReservaService.Buscar(new Reserva { Codigo = _reservaId }).FirstOrDefault();
                
                if (reserva.StatusProdesp == "E" && reserva.TransmitirProdesp)
                    status = "Falha Prodesp";
                else if (reserva.StatusProdesp == "S" && reserva.StatusSiafemSiafisico == "S" && reserva.StatusDoc == false)
                    status = "Falha Doc";
                else status = "Falha";

                var result = new { Status = status, Msg = ex.Message, reserva.Codigo, objModel = reserva };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Retransmitir(List<int> ids)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();
                var msg = App.ReservaService.Transmitir(ids, usuario, (int)_funcId);
                var result = new { Status = (msg == "" ? "Sucesso" : "Falha"), Msg = msg };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Imprimir(int id)
        {
            try
            {
                var usuario = App.AutenticacaoService.GetUsuarioLogado();

                var reserva = App.ReservaService.Buscar(new Reserva { Codigo = id }).FirstOrDefault();

                ConsultaNr consultaNr = App.ReservaService.ConsultaNr(reserva, usuario);

                Session[App.BaseService.GetCurrentIp()] = HelperReport.GerarPdfReserva(consultaNr, "Reserva", reserva); 

                var result = new { Status = "Sucesso" };

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var result = new { Status = "Falha", Msg = ex.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        

        [PermissaoAcesso(Controller = typeof(ReservaController), Operacao = "Listar")]
        public ActionResult AtualizarIndex()
        {
            try
            {
                var f = App.ReservaService.GetCurrentFilter("Reserva");

                return f != null ? Index(f) : RedirectToAction("Index", new { Id = _funcId.ToString() });
            }
            catch (Exception ex)
            {
                ExibirMensagemErro("Não foi possível concluir a consulta. " + ex.Message);
                return View("Index", _reservas);
            }
        }

        #region Classes Internas

        public class DtoReservaSalvar
        {
            public Reserva Reserva { get; set; }
            public List<ReservaMes> ReservaMes { get; set; }
        }

        #endregion


        #region Metodos Privados

        private IReserva FiltrosDaPesquisa(FormCollection f)
        {
            var obj = new Reserva();
            IFiltroViewModel filtro = new FiltroViewModel().GerarFiltro(obj); //cria os filtros e tambem retorna na index para preencher as combos novamente com todos os valores (sem filtro)
            ExtrairDadosFiltro(f, ref obj, ref filtro);
            ViewBag.Filtro = filtro;
            return obj;
        }

        private static void ExtrairDadosFiltro(FormCollection f, ref Reserva obj, ref IFiltroViewModel filtro)
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

            if (obj is Reserva)
            {
                if (!string.IsNullOrEmpty(f["cbxTipoReserva"]))
                {
                    obj.Tipo = int.Parse(f["cbxTipoReserva"]);
                    filtro.Tipo = int.Parse(f["cbxTipoReserva"]);
                }
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

        private static void GerarReservaCreate(ref Reserva objModel)
        {

            Reserva reserva = App.ReservaService.BuscarAssinaturas(objModel);

            if (reserva != null && reserva.Codigo > 0)
            {
                objModel.AutorizadoAssinatura = reserva.AutorizadoAssinatura;
                objModel.AutorizadoGrupo = reserva.AutorizadoGrupo;
                objModel.AutorizadoOrgao = reserva.AutorizadoOrgao;
                objModel.NomeAutorizadoAssinatura = reserva.NomeAutorizadoAssinatura;
                objModel.AutorizadoCargo = reserva.AutorizadoCargo;
                objModel.ExaminadoAssinatura = reserva.ExaminadoAssinatura;
                objModel.ExaminadoGrupo = reserva.ExaminadoGrupo;
                objModel.ExaminadoOrgao = reserva.ExaminadoOrgao;
                objModel.NomeExaminadoAssinatura = reserva.NomeExaminadoAssinatura;
                objModel.ExaminadoCargo = reserva.ExaminadoCargo;
                objModel.ResponsavelAssinatura = reserva.ResponsavelAssinatura;
                objModel.ResponsavelGrupo = reserva.ResponsavelGrupo;
                objModel.ResponsavelOrgao = reserva.ResponsavelOrgao;
                objModel.NomeResponsavelAssinatura = reserva.NomeResponsavelAssinatura;
                objModel.ResponsavelCargo = reserva.ResponsavelCargo;
            }

        }

        private static void NewReserva(Reserva reserva)
        {
            reserva.Codigo = 0;
            reserva.NumProdesp = null;
            reserva.NumSiafemSiafisico = null;
            reserva.DataCadastro = null;
            reserva.DataTransmissaoProdesp = null;
            reserva.DataTransmissaoSiafemSiafisico = null;
            reserva.TransmitidoProdesp = false;
            reserva.TransmitidoSiafisico = false;
            reserva.TransmitidoSiafem = false;
            reserva.TransmitirProdesp = false;
            reserva.TransmitirSiafisico = false;
            reserva.TransmitirSiafem = false;
            reserva.MsgRetornoTransSiafemSiafisico = null;
            reserva.MsgRetornoTransmissaoProdesp = null;
        }

        private int Salvar(DtoReservaSalvar dtoReservaSalvar,int funcId)
        {
            EnumAcao enumAcao = dtoReservaSalvar.Reserva.Codigo > 0 ? EnumAcao.Alterar : EnumAcao.Inserir;
            return App.ReservaService.Salvar(dtoReservaSalvar.Reserva, dtoReservaSalvar.ReservaMes ?? new List<ReservaMes>(), funcId, (short)enumAcao);
        }
        #endregion


    }
}


