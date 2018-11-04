using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Core.Services.WebService.Reserva;
using System.Configuration;

namespace Sids.Prodesp.Core.Services.Reserva
{
    using Base;
    using Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface.Base;
    using Model.Interface.Configuracao;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using Model.Interface.Seguranca;
    using Model.Interface.Service.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Model.Interface;
    using Infrastructure;

    public class ReservaCancelamentoService : BaseService
    {
        #region Propriedades

        private readonly ICrudReservaCancelamento _cancelamento;
        private readonly ProdespReservaService _prodesp;
        private readonly SiafemReservaService _siafem;
        private readonly ReservaCancelamentoMesService _cancelamentoMes;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;
        private readonly RegionalService _regional;
        private int Recurso { get; set; }


        #endregion

        #region Construtores

        public ReservaCancelamentoService(
            ILogError l, ICrudReservaCancelamento p, IProdespReserva prodesp, ISiafemReserva siafem, ICrudPrograma programa, 
            ICrudFonte fonte, ICrudEstrutura estrutura, ICrudReservaCancelamentoMes mes, 
            IRegional regional, IChaveCicsmo chave) : base(l)
        {
            _prodesp = new ProdespReservaService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemReservaService(l, siafem, programa, fonte, estrutura);
            _cancelamento = p;
            _cancelamentoMes = new ReservaCancelamentoMesService(l, mes);
            _chave = new ChaveCicsmoService(l, chave);
            _programa = new ProgramaService(l, programa, estrutura);
            _regional = new RegionalService(l, regional);

        }

        #endregion

        #region Metodos Publicos

        public int Salvar(ReservaCancelamento objModel, List<ReservaCancelamentoMes> cancelamentoMes, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                {
                    objModel.DataCadastro = DateTime.Now;
                    objModel.Codigo = _cancelamento.Add(objModel);
                }
                else
                {
                    _cancelamento.Edit(objModel);
                }

                if (cancelamentoMes != null)
                {
                    cancelamentoMes.ForEach(x => x.Id = objModel.Codigo);
                    _cancelamentoMes.Salvar(cancelamentoMes, 0, actionId);
                }

                var arg = string.Format("Nº do Cancelamento Prodesp {0}, Nº da reforço SIAFEM/SIAFISICO {1}.",
                                         objModel.NumProdesp, objModel.NumSiafemSiafisico);

                if (recursoId > 0)
                    LogSucesso(actionId, recursoId, arg);

                return objModel.Codigo;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public IEnumerable<ReservaCancelamento> Buscar(ReservaCancelamento objModel)
        {
            return _cancelamento.Fetch(objModel);
        }

        public IEnumerable<ReservaCancelamento> BuscarGrid(ReservaCancelamento objModel)
        {
            return _cancelamento.FetchForGrid(objModel);
        }


        public ReservaCancelamento BuscarAssinaturas(ReservaCancelamento entity)
        {
            return _cancelamento.BuscarAssinaturas(entity);
        }

        public AcaoEfetuada Excluir(ReservaCancelamento cancelamento, int recursoId, short actionId)
        {
            try
            {
                _cancelamento.Remove(cancelamento.Codigo);

                var arg = String.Format("Cancelamento {0}, Codigo {1}", cancelamento.Observacao, cancelamento.Codigo);

                if (recursoId > 0)
                    return LogSucesso(actionId, recursoId, arg);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }


        public void Transmitir(int reservaId, Usuario usuario, int recursoId)
        {
            var reserva = Buscar(new ReservaCancelamento { Codigo = reservaId }).FirstOrDefault();

            if (AppConfig.WsUrl != "siafemProd")
                if (reserva.TransmitirSiafem)
                    usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};
                else
                    usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};

            try
            {
                Transmissao(usuario, reserva, recursoId);
            }
            catch
            {
                throw;
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
            var cancelamentos = new List<ReservaCancelamento>();

            foreach (var reservaId in reservaIds)
            {
                var cancelamento = new ReservaCancelamento();
                try
                {
                    cancelamento = Buscar(new ReservaCancelamento { Codigo = reservaId }).FirstOrDefault();

                    if (AppConfig.WsUrl != "siafemProd")
                        if (cancelamento.TransmitirSiafem)
                            usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };
                        else
                            usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};

                    var dtoErro = Retransmissao(usuario, cancelamento, recursoId);

                    if (cancelamento.TransmitirProdesp && (cancelamento.TransmitirSiafem || cancelamento.TransmitirSiafisico))
                        if (cancelamento.StatusProdesp == "S" && cancelamento.StatusSiafemSiafisico == "E")
                            result += ";" + result == null ? " / " : "" + "Erro " + dtoErro.Siafem + " para transmissão Cancelamento N° Prodesp " + cancelamento.NumProdesp;


                    if (cancelamento.TransmitirProdesp && (cancelamento.TransmitirSiafem || cancelamento.TransmitirSiafisico))
                        if (cancelamento.StatusProdesp == "E" && cancelamento.StatusSiafemSiafisico == "S")
                            result += ";" + result == null ? " / " : "" + "Erro " + dtoErro.Prodesp + " para transmissão Cancelamento N° SIAFEM/SIAFISICO " + cancelamento.NumSiafemSiafisico;

                    cancelamentos.Add(cancelamento);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    Salvar(cancelamento, null, recursoId, (int)EnumAcao.Transmitir);
                    if (cancelamento.TransmitidoProdesp == true)
                        SetCurrentTerminal("");
                }
            }

            var cancelamentosErros =
                cancelamentos.Where(x => (x.StatusProdesp == "E" && x.StatusSiafemSiafisico == "E") ||
                        (x.StatusProdesp == "E" && x.StatusSiafemSiafisico == "N") ||
                        (x.StatusProdesp == "N" && x.StatusSiafemSiafisico == "E")).ToList();

            if (cancelamentosErros.Count > 0)
                if (cancelamentosErros.Count == 1)
                    result += cancelamentos.FirstOrDefault().MsgRetornoTransSiafemSiafisico + Environment.NewLine + ";" + cancelamentos.FirstOrDefault().MsgRetornoTransmissaoProdesp;
                else
                    result += Environment.NewLine + ";Alguns cancelamentos não puderam ser transmitidos";

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

        #endregion

        #region Metodos Privados
        private void Transmissao(Usuario usuario, ReservaCancelamento cancelamento, int recursoId)
        {
            var chave = new ChaveCicsmo();
            try
            {

                if ((cancelamento.TransmitirProdesp && cancelamento.TransmitidoProdesp == false))
                    TransmitirProdesp(cancelamento, recursoId);

                if (cancelamento.TransmitirSiafem && cancelamento.TransmitidoSiafem == false)
                    TransmitirSiafem(cancelamento, usuario, recursoId);

                if ((cancelamento.TransmitidoSiafisico == true || cancelamento.TransmitidoSiafem == true) && cancelamento.TransmitidoProdesp == true)
                {
                    chave = _chave.ObterChave(recursoId);

                    cancelamento.StatusDoc = _prodesp.InserirDoc(cancelamento, chave.Chave, chave.Senha, "02");
                    _chave.LiberarChave(chave.Codigo);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        private DtoError Retransmissao(Usuario usuario, ReservaCancelamento cancelamento, int recursoId)
        {
            var error = new DtoError();
            var chave = new ChaveCicsmo();
            try
            {
                try
                {
                    if (cancelamento.TransmitirProdesp && cancelamento.TransmitidoProdesp == false)
                        TransmitirProdesp(cancelamento, recursoId);
                }
                catch (Exception ex)
                {
                    error.Prodesp = ex.Message;
                }

                try
                {
                    if (cancelamento.TransmitirSiafem && cancelamento.TransmitidoSiafem == false)
                        TransmitirSiafem(cancelamento, usuario, recursoId);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                if ((cancelamento.TransmitidoSiafisico == true || cancelamento.TransmitidoSiafem == true) && cancelamento.TransmitidoProdesp == true)
                {
                    chave = _chave.ObterChave(recursoId);

                    cancelamento.StatusDoc = _prodesp.InserirDoc(cancelamento, chave.Chave, chave.Senha, "02");
                    _chave.LiberarChave(chave.Codigo);
                }

                return error;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        private void TransmitirProdesp(ReservaCancelamento cancelamento, int recursoId)
        {
            var chave = new ChaveCicsmo();
            try
            {
                chave = _chave.ObterChave(recursoId);

                var mes = _cancelamentoMes.Buscar(new ReservaCancelamentoMes { Id = cancelamento.Codigo }).Cast<IMes>().ToList();
                var result = _prodesp.InserirReservaCancelamento(cancelamento, mes, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);

                cancelamento.NumProdesp = result.Replace(" ", "");
                cancelamento.TransmitidoProdesp = true;
                cancelamento.StatusProdesp = "S";
                cancelamento.DataTransmissaoProdesp = DateTime.Now;
                cancelamento.MsgRetornoTransmissaoProdesp = null;

                Salvar(cancelamento, null, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                cancelamento.StatusProdesp = "E";
                cancelamento.MsgRetornoTransmissaoProdesp = ex.Message;
                Salvar(cancelamento, null, 0, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        private void TransmitirSiafem(ReservaCancelamento cancelamento, Usuario usuario, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

                var mes = _cancelamentoMes.Buscar(new ReservaCancelamentoMes { Id = cancelamento.Codigo }).ToList();
                var result = _siafem.InserirReservaCancelamento(usuario.CPF, Decrypt(usuario.SenhaSiafem), cancelamento, mes, ug);
                cancelamento.NumSiafemSiafisico = result;
                cancelamento.TransmitidoSiafem = true;
                cancelamento.StatusSiafemSiafisico = "S";
                cancelamento.DataTransmissaoSiafemSiafisico = DateTime.Now;
                cancelamento.MsgRetornoTransSiafemSiafisico = null;

                Salvar(cancelamento, null, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                cancelamento.StatusSiafemSiafisico = "E";
                cancelamento.MsgRetornoTransSiafemSiafisico = ex.Message;
                Salvar(cancelamento, null, 0, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        #endregion

        #region Classes Internas

        internal class DtoError
        {
            public string Prodesp { get; set; }
            public string Siafem { get; set; }
        }

        #endregion

    }
}
