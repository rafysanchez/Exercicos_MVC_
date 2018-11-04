
namespace Sids.Prodesp.Core.Services.Reserva
{
    using Base;
    using Configuracao;
    using Seguranca;
    using WebService.Reserva;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Infrastructure;
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

    public class ReservaReforcoService : BaseService
    {
        #region Propriedades

        private readonly ICrudReservaReforco _reforco;
        private readonly ProdespReservaService _prodesp;
        private readonly SiafemReservaService _siafem;
        private readonly ReservaReforcoMesService _reforcoMes;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;
        private readonly RegionalService _regional;
        private int Recurso { get; set; }


        #endregion

        #region Construtores

        public ReservaReforcoService(
            ILogError l, ICrudReservaReforco p, IProdespReserva prodesp, ISiafemReserva siafem, 
            ICrudPrograma programa, ICrudFonte fonte, ICrudEstrutura estrutura,
            ICrudReservaReforcoMes mes, IRegional regional, IChaveCicsmo chave) : base(l)
        {
            _prodesp = new ProdespReservaService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemReservaService(l, siafem, programa, fonte, estrutura);
            _reforco = p;
            _programa = new ProgramaService(l, programa, estrutura);
            _reforcoMes = new ReservaReforcoMesService(l, mes);
            _chave = new ChaveCicsmoService(l, chave);
            _regional = new RegionalService(l,regional);
        }

        #endregion

        #region Metodos Publicos

        public int Salvar(ReservaReforco objModel, List<ReservaReforcoMes> reforcoMes, int recursoId, short actionId)
        {
            try
            {
                if (objModel.Codigo == 0)
                {
                    objModel.DataCadastro = DateTime.Now;
                    objModel.Codigo = _reforco.Add(objModel);
                }
                else
                {
                    _reforco.Edit(objModel);
                }

                if (reforcoMes != null)
                {
                    reforcoMes.ForEach(x => x.Id = objModel.Codigo);
                    _reforcoMes.Salvar(reforcoMes, 0, actionId);
                }

                var arg = $"Nº do Reforço Prodesp {objModel.NumProdesp}, Nº da reforço SIAFEM/SIAFISICO {objModel.NumSiafemSiafisico}.";

                if (recursoId > 0)
                    LogSucesso(actionId, recursoId, arg);

                return objModel.Codigo;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
        }

        public IEnumerable<ReservaReforco> Buscar(ReservaReforco objModel)
        {
            return _reforco.Fetch(objModel);
        }

        public IEnumerable<ReservaReforco> BuscarGrid(ReservaReforco objModel)
        {
            return _reforco.FetchForGrid(objModel);
        }

        public ReservaReforco BuscarAssinaturas(ReservaReforco entity)
        {
            return _reforco.BuscarAssinaturas(entity);
        }
        public AcaoEfetuada Excluir(ReservaReforco reforco, int recursoId, short actionId)
        {
            try
            {
                _reforco.Remove(reforco.Codigo);
                var arg = string.Format("Reforco {0}, Codigo {1}", reforco.Observacao, reforco.Codigo.ToString());
                if (recursoId > 0)
                    return LogSucesso(actionId, recursoId, arg);
                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: recursoId);
            }
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

        public void Transmitir(int reforcoId, Usuario usuario, int recursoId)  // utilizado na trasmissão
        {
            var reforco = Buscar(new ReservaReforco { Codigo = reforcoId }).FirstOrDefault();

            if (AppConfig.WsUrl != "siafemProd")
                if (reforco.TransmitirSiafem)
                    usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};
                else
                    usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};

            try
            {
                Transmissao(usuario, reforco,  recursoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Salvar(reforco, null, recursoId, (short)EnumAcao.Transmitir);
                if (reforco.TransmitidoProdesp == true)
                    SetCurrentTerminal("");
            }
        }

        public string Transmitir(List<int> reforcoIds, Usuario usuario, int recursoId)  // sobrecarga para retransmissão
        {

            string result = "";
            List<ReservaReforco> reforcos = new List<ReservaReforco>();



            foreach (var reforcoId in reforcoIds)
            {
                ReservaReforco reforco = new ReservaReforco();
                try
                {
                    reforco = Buscar(new ReservaReforco { Codigo = reforcoId }).FirstOrDefault();

                    if (AppConfig.WsUrl != "siafemProd")
                        if (reforco.TransmitirSiafem)
                            usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};
                        else
                            usuario = new Usuario { CPF = AppConfig.WsSiafisicoUser, SenhaSiafem = Encrypt(AppConfig.WsPassword) , RegionalId = 1};

                    var dtoErro = Retransmissao(usuario, reforco, recursoId);

                    if (reforco.TransmitirProdesp && (reforco.TransmitirSiafem || reforco.TransmitirSiafisico))
                        if (reforco.StatusProdesp == "S" && reforco.StatusSiafemSiafisico == "E")
                            result += ";" + result == null ? " / " : "" + "Erro " + dtoErro.Siafem + " para transmissão Reforço N° Prodesp " + reforco.NumProdesp;


                    if (reforco.TransmitirProdesp && (reforco.TransmitirSiafem || reforco.TransmitirSiafisico))
                        if (reforco.StatusProdesp == "E" && reforco.StatusSiafemSiafisico == "S")
                            result += ";" + result == null ? " / " : "" + "Erro " + dtoErro.Prodesp + " para transmissão Reforço N° SIAFEM/SIAFISICO " + reforco.NumSiafemSiafisico;

                    reforcos.Add(reforco);

                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
                }
            }


            var reforcosErros =
                reforcos.Where(x => (x.StatusProdesp == "E" && x.StatusSiafemSiafisico == "E") ||
                        (x.StatusProdesp == "E" && x.StatusSiafemSiafisico == "N") ||
                        (x.StatusProdesp == "N" && x.StatusSiafemSiafisico == "E")).ToList();

            if (reforcosErros.Count > 0)
                if (reforcos.Count == 1)
                    result += reforcos.FirstOrDefault().MsgRetornoTransSiafemSiafisico + Environment.NewLine + ";" + reforcos.FirstOrDefault().MsgRetornoTransmissaoProdesp;
                else
                    result += Environment.NewLine + "; / Alguns Reforços de Reserva não puderam ser transmitidos";

            return result;
        }
        #endregion

        #region Metodos Privados
        private void TransmitirProdesp(ReservaReforco reforco, int recursoId)
        {
            ChaveCicsmo chave = new ChaveCicsmo();
            try
            {
                var mes = _reforcoMes.Buscar(new ReservaReforcoMes { Id = reforco.Codigo }).Cast<IMes>().ToList();

                chave = _chave.ObterChave(recursoId);

                var result = _prodesp.InserirReservaReforco(reforco, mes, chave.Chave, chave.Senha);
                _chave.LiberarChave(chave.Codigo);

                reforco.NumProdesp = result.Replace(" ", "");
                reforco.TransmitidoProdesp = true;
                reforco.StatusProdesp = "S";
                reforco.DataTransmissaoProdesp = DateTime.Now;
                reforco.MsgRetornoTransmissaoProdesp = null;

                Salvar(reforco, null, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                reforco.StatusProdesp = "E";
                reforco.MsgRetornoTransmissaoProdesp = ex.Message;
                Salvar(reforco, null, 0, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
            }
        }

        private void TransmitirSiafem(ReservaReforco reforco, Usuario usuario, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;
                var mes = _reforcoMes.Buscar(new ReservaReforcoMes { Id = reforco.Codigo }).Cast<IMes>().ToList();
                var result = _siafem.InserirReservaSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), reforco, mes, ug);
                reforco.NumSiafemSiafisico = result;
                reforco.TransmitidoSiafem = true;
                reforco.StatusSiafemSiafisico = "S";
                reforco.DataTransmissaoSiafemSiafisico = DateTime.Now;
                reforco.MsgRetornoTransSiafemSiafisico = null;
                Salvar(reforco, null, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                reforco.MsgRetornoTransSiafemSiafisico = ex.Message;
                reforco.StatusSiafemSiafisico = "E";
                Salvar(reforco, null, 0, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (int)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirSiafisico(ReservaReforco reforco, Usuario usuario, int recursoId)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

                var mes = _reforcoMes.Buscar(new ReservaReforcoMes { Id = reforco.Codigo }).Cast<IMes>().ToList();
                var result = _siafem.InserirReservaSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), reforco, mes, ug);
                reforco.NumSiafemSiafisico = result;
                reforco.TransmitidoSiafisico = true;
                reforco.StatusSiafemSiafisico = "S";
                reforco.DataTransmissaoSiafemSiafisico = DateTime.Now;
                reforco.MsgRetornoTransSiafemSiafisico = null;

                Salvar(reforco, null, 0, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {

                reforco.StatusSiafemSiafisico = "E";
                reforco.MsgRetornoTransSiafemSiafisico = ex.Message;
                Salvar(reforco, null, 0, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
        }

        private void Transmissao(Usuario usuario, ReservaReforco reforco, int recursoId)
        {
            var chave = new ChaveCicsmo();
            try
            {

                if (reforco.TransmitirSiafem && reforco.TransmitidoSiafem == false)
                    TransmitirSiafem(reforco, usuario,  recursoId);

                if (reforco.TransmitirSiafisico && reforco.TransmitidoSiafisico == false)
                    TransmitirSiafisico(reforco, usuario,  recursoId);

                if ((reforco.TransmitirProdesp && reforco.TransmitidoProdesp == false))
                    TransmitirProdesp(reforco,  recursoId);

                if ((reforco.TransmitidoSiafisico == true || reforco.TransmitidoSiafem == true) && reforco.TransmitidoProdesp == true)
                {
                    chave = _chave.ObterChave(recursoId);
                    
                    reforco.StatusDoc = _prodesp.InserirDoc(reforco, chave.Chave, chave.Senha, "07");
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

        private DtoError Retransmissao(Usuario usuario, ReservaReforco reforco, int recursoId)
        {
            var error = new DtoError();
            var chave = new ChaveCicsmo();
            try
            {
                try
                {
                    if ((bool)reforco.TransmitirProdesp && reforco.TransmitidoProdesp == false)
                        TransmitirProdesp(reforco,  recursoId);
                }
                catch (Exception ex)
                {
                    error.Prodesp = ex.Message;
                }

                try
                {
                    if ((bool)reforco.TransmitirSiafem && reforco.TransmitidoSiafem == false)
                        TransmitirSiafem(reforco, usuario,  recursoId);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                try
                {
                    if ((bool)reforco.TransmitirSiafisico && reforco.TransmitidoSiafisico == false)
                        TransmitirSiafisico(reforco, usuario, recursoId);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }


                if (((bool)reforco.TransmitidoSiafisico || (bool)reforco.TransmitidoSiafem) && (bool)reforco.TransmitidoProdesp)
                {
                    chave = _chave.ObterChave(recursoId);
                    
                    reforco.StatusDoc = _prodesp.InserirDoc(reforco, chave.Chave, chave.Senha, "07");
                    _chave.LiberarChave(chave.Codigo);
                }
                return error;

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(chave.Codigo);
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
