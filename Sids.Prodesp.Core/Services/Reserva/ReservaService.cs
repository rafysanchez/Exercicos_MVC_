namespace Sids.Prodesp.Core.Services.Reserva
{
    using Configuracao;
    using Infrastructure;
    using Infrastructure.Services.Empenho;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface;
    using Model.Interface.Base;
    using Model.Interface.Configuracao;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using Model.Interface.Seguranca;
    using Model.Interface.Service;
    using Model.Interface.Service.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using WebService;
    using WebService.Reserva;

    public class ReservaService : CommonService
    {
        private readonly ICrudReserva _reserva;
        private readonly ProdespReservaService _prodesp;
        private readonly SiafemReservaService _siafem;
        private readonly ReservaMesService _reservaMes;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;



        public ReservaService(ILogError l, ICrudReserva p, IProdespReserva prodesp, ISiafemReserva siafem, ICrudPrograma programa,
            ICrudFonte fonte, ICrudEstrutura estrutura, ICrudReservaMes reservaMes, IRegional regional, IChaveCicsmo chave, ICommon c) 
            : base(l,c,prodesp,siafem,new SiafemEmpenhoWs(), chave)
        {
            _prodesp = new ProdespReservaService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemReservaService(l, siafem, programa, fonte, estrutura);
            _reservaMes = new ReservaMesService(l, reservaMes);
            _reserva = p;
            _chave = new ChaveCicsmoService(l, chave);
            _programa = new ProgramaService(l, programa, estrutura);
        }


        public AcaoEfetuada Excluir(Reserva reserva, int recursoId, short actionId)
        {
            try
            {
                _reserva.Remove(reserva.Codigo);

                var arg = $"Reserva {reserva.Observacao}, Codigo {reserva.Codigo.ToString()}";

                if (recursoId > 0)
                    return LogSucesso(actionId, recursoId, arg);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public int Salvar(Reserva objModel, List<ReservaMes> reservaMes, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                {
                    objModel.DataCadastro = DateTime.Now;
                    objModel.Codigo = _reserva.Add(objModel);
                }
                else
                {
                    _reserva.Edit(objModel);
                }

                if (reservaMes != null)
                {
                    reservaMes.ForEach(x => x.Id = objModel.Codigo);
                    _reservaMes.Salvar(reservaMes, 0, actionId);
                }

                var arg =
                    $"Nº da reserva Prodesp {objModel.NumProdesp}, Nº da reserva SIAFEM/SIAFISICO {objModel.NumSiafemSiafisico}.";

                if (recursoId > 0)
                    LogSucesso(actionId, recursoId, arg);

                return objModel.Codigo;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public IEnumerable<Reserva> Buscar(Reserva objModel)
        {
            return _reserva.Fetch(objModel);
        }

        public Reserva BuscarAssinaturas(Reserva objModel)
        {
            return _reserva.BuscarAssinaturas(objModel);
        }

        public IEnumerable<Reserva> BuscarGrid(Reserva objModel)
        {
            return _reserva.FetchForGrid(objModel);
        }


        public void Transmitir(int reservaId, Usuario usuario, int recursoId)
        {

            var reserva = Buscar(new Reserva { Codigo = reservaId }).FirstOrDefault();

            if (AppConfig.WsUrl != "siafemProd")
                if (reserva.TransmitirSiafem)
                    usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};
                else
                    usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};

            try
            {
                Transmissao(usuario, reserva, recursoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Salvar(reserva, null, recursoId, (short)EnumAcao.Transmitir);
                if (reserva.TransmitidoProdesp == true)
                    SetCurrentTerminal("");
            }
        }

        public string Transmitir(List<int> reservaIds, Usuario usuario, int recursoId)
        {

            var result = "";
            var reservas = new List<Reserva>();

            foreach (var reservaId in reservaIds)
            {
                var reserva = new Reserva();
                try
                {
                    reserva = Buscar(new Reserva { Codigo = reservaId }).FirstOrDefault();

                    if (AppConfig.WsUrl != "siafemProd")
                        if (reserva.TransmitirSiafem)
                            usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};
                        else
                            usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};

                    var dtoErro = Retransmissao(usuario, reserva, recursoId);

                    if (reserva.TransmitirProdesp && (reserva.TransmitirSiafem || reserva.TransmitirSiafisico))
                        if (reserva.StatusProdesp == "S" && reserva.StatusSiafemSiafisico == "E")
                            result += ";" + result == null ? " / " : "" + "Erro " + dtoErro.Siafem + " para transmissão Reserva N° Prodesp " + reserva.NumProdesp;


                    if (reserva.TransmitirProdesp && (reserva.TransmitirSiafem || reserva.TransmitirSiafisico))
                        if (reserva.StatusProdesp == "E" && reserva.StatusSiafemSiafisico == "S")
                            result += ";" + result == null ? " / " : "" + "Erro " + dtoErro.Prodesp + " para transmissão Reserva N° SIAFEM/SIAFISICO " + reserva.NumSiafemSiafisico;

                    reservas.Add(reserva);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    Salvar(reserva, null, recursoId, (short)EnumAcao.Transmitir);
                    if (reserva.TransmitidoProdesp == true)
                        SetCurrentTerminal("");
                }
            }

            var reservasErros =
                reservas.Where(x => (x.StatusProdesp == "E" && x.StatusSiafemSiafisico == "E") ||
                        (x.StatusProdesp == "E" && x.StatusSiafemSiafisico == "N") ||
                        (x.StatusProdesp == "N" && x.StatusSiafemSiafisico == "E")).ToList();

            if (reservasErros.Count > 0)
                if (reservas.Count == 1)
                    result += reservas.FirstOrDefault().MsgRetornoTransSiafemSiafisico + Environment.NewLine + ";" + reservas.FirstOrDefault().MsgRetornoTransmissaoProdesp;
                else
                    result += Environment.NewLine + "; / Algumas reservas não puderam ser transmitidas";

            return result;
        }

        public List<SelectListItem> ObterAnos(DateTime referenceDate = default(DateTime))
        {
            referenceDate = referenceDate == default(DateTime) ? DateTime.Now : referenceDate;

            var restriction = new int[] { referenceDate.Year - 1, referenceDate.Year };
            var anos = _programa.GetAnosPrograma().ToList();

            if (anos.All(x => x != DateTime.Now.Year))
                anos.Add(DateTime.Now.Year);

            return anos.Where(w => restriction.Contains(w)).Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString()
            }).ToList();
        }

        #region Metodos Privados
        private void Transmissao(Usuario usuario, Reserva reserva, int recursoId)
        {
            var chave = new ChaveCicsmo();
            try
            {

                if (reserva.TransmitirSiafem && reserva.TransmitidoSiafem == false)
                    TransmitirSiafem(reserva, usuario, recursoId);

                if (reserva.TransmitirSiafisico && reserva.TransmitidoSiafisico == false)
                    TransmitirSiafisico(reserva, usuario, recursoId);

                if ((reserva.TransmitirProdesp && reserva.TransmitidoProdesp == false))
                    TransmitirProdesp(reserva, recursoId);

                if ((reserva.TransmitidoSiafisico == true || reserva.TransmitidoSiafem == true) && reserva.TransmitidoProdesp == true)
                {
                    chave = _chave.ObterChave(recursoId);
                    reserva.StatusDoc = false;
                    reserva.StatusDoc = _prodesp.InserirDoc(reserva, chave.Chave, chave.Senha, "01");
                    _chave.LiberarChave(chave.Codigo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        private DtoError Retransmissao(Usuario usuario, Reserva reserva, int recursoId)
        {
            var error = new DtoError();
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                try
                {
                    if (reserva.TransmitirProdesp && reserva.TransmitidoProdesp == false)
                        TransmitirProdesp(reserva, recursoId);
                }
                catch (Exception ex)
                {
                    error.Prodesp = ex.Message;
                }

                try
                {
                    if (reserva.TransmitirSiafem && reserva.TransmitidoSiafem == false)
                        TransmitirSiafem(reserva, usuario, recursoId);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                try
                {
                    if (reserva.TransmitirSiafisico && reserva.TransmitidoSiafisico == false)
                        TransmitirSiafisico(reserva, usuario, recursoId);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                if (((bool)reserva.TransmitidoSiafisico || (bool)reserva.TransmitidoSiafem) && (bool)reserva.TransmitidoProdesp)
                {
                    chave = _chave.ObterChave(recursoId);
                    reserva.StatusDoc = false;
                    reserva.StatusDoc = _prodesp.InserirDoc(reserva, chave.Chave, chave.Senha, "01");
                    _chave.LiberarChave(chave.Codigo);
                }
                return error;
            }
            catch (Exception ex)
            {
                _chave.LiberarChave(chave.Codigo);
                throw ex;
            }
        }

        private void TransmitirProdesp(Reserva reserva, int recursoId)
        {
            var chave = new ChaveCicsmo();
            try
            {
                var mes = _reservaMes.Buscar(new ReservaMes { Id = reserva.Codigo }).Cast<IMes>().ToList();

                chave = _chave.ObterChave(recursoId);

                var result = _prodesp.InserirReserva(reserva, mes, chave.Chave, chave.Senha);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: PRODESP NUMERO" + result));
                _chave.LiberarChave(chave.Codigo);

                reserva.NumProdesp = result.Replace(" ", "");
                reserva.TransmitidoProdesp = true;
                reserva.StatusProdesp = "S";
                reserva.DataTransmissaoProdesp = DateTime.Now;
                reserva.MsgRetornoTransmissaoProdesp = null;

                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: PRODESP SALVANDO..."));
                Salvar(reserva, null, 0, (short)EnumAcao.Transmitir);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: PRODESP SALVAMENTO OK!"));
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: PRODESP " + ex.ToString()));
                _chave.LiberarChave(chave.Codigo);
                reserva.StatusProdesp = "E";
                reserva.MsgRetornoTransmissaoProdesp = ex.Message;
                
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: PRODESP SALVAMENTO OK!"));

                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirSiafem(Reserva reserva, Usuario usuario, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;
                var mes = _reservaMes.Buscar(new ReservaMes { Id = reserva.Codigo }).Cast<IMes>().ToList();
                var result = _siafem.InserirReservaSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), reserva, mes, ug);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: SIAFEM NUMERO" + result));
                reserva.NumSiafemSiafisico = result;
                reserva.TransmitidoSiafem = true;
                reserva.StatusSiafemSiafisico = "S";
                reserva.DataTransmissaoSiafemSiafisico = DateTime.Now;
                reserva.MsgRetornoTransSiafemSiafisico = null;

                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: SIAFEM SALVANDO..."));
                Salvar(reserva, null, 0, (short)EnumAcao.Transmitir);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: SIAFEM SALVAMENTO OK!"));
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: SIAFEM " + ex.ToString()));
                reserva.StatusSiafemSiafisico = "E";
                reserva.MsgRetornoTransSiafemSiafisico = ex.Message;

                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: SIAFEM SALVANDO..."));
                Salvar(reserva, null, 0, (int)EnumAcao.Transmitir);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: SIAFEM SALVAMENTO OK!"));

                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }

        }

        private void TransmitirSiafisico(Reserva reserva, Usuario usuario, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;
                var mes = _reservaMes.Buscar(new ReservaMes { Id = reserva.Codigo }).Cast<IMes>().ToList();
                var result = _siafem.InserirReservaSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), reserva, mes, ug);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: SIAFISICO NUMERO" + result));
                reserva.NumSiafemSiafisico = result;
                reserva.TransmitidoSiafisico = true;
                reserva.StatusSiafemSiafisico = "S";
                reserva.DataTransmissaoSiafemSiafisico = DateTime.Now;
                reserva.MsgRetornoTransSiafemSiafisico = null;

                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: SIAFISICO SALVANDO..."));
                Salvar(reserva, null, 0, (short)EnumAcao.Transmitir);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA: SIAFISICO SALVAMENTO OK!"));
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: SIAFISICO " + ex.ToString()));
                reserva.StatusSiafemSiafisico = "E";
                reserva.MsgRetornoTransSiafemSiafisico = ex.Message;
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: SIAFISICO SALVANDO..."));
                Salvar(reserva, null, 0, (int)EnumAcao.Transmitir);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERRO RESERVA CATCH: SIAFISICO SALVAMENTO OK!"));
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        #endregion

        internal class DtoError
        {
            public string Prodesp { get; set; }
            public string Siafem { get; set; }
        }

    }
}
