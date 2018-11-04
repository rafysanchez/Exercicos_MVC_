using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.Interface.Configuracao;
using Sids.Prodesp.Model.Interface.Empenho;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Seguranca;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.Empenho;

namespace Sids.Prodesp.Core.Services.Empenho
{
    using Base;
    using Configuracao;
    using Infrastructure.Services.Reserva;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using Model.Interface;
    using Reserva;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using WebService;
    using WebService.Empenho;
    using Sids.Prodesp.Model.ValueObject;
    using Infrastructure;

    public class EmpenhoCancelamentoService : CommonService
    {
        private readonly ICrudEmpenhoCancelamento _repository;
        private readonly ProdespEmpenhoService _prodesp;
        private readonly SiafemEmpenhoService _siafem;
        private readonly EmpenhoCancelamentoMesService _mes;
        private readonly EmpenhoCancelamentoItemService _item;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;


        public EmpenhoCancelamentoService(ILogError l, ICrudEmpenhoCancelamento repository, ICrudEmpenhoCancelamentoMes mes, ICrudEmpenhoCancelamentoItem item,
            IProdespEmpenho prodesp, ISiafemEmpenho siafem, ICrudPrograma programa,
            ICrudFonte fonte, ICrudEstrutura estrutura, IRegional regional, IChaveCicsmo chave, ICommon c)
            : base(l, c, new ProdespReservaWs(), new SiafemReservaWs(), siafem, chave)
        {
            _prodesp = new ProdespEmpenhoService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemEmpenhoService(l, siafem, programa, fonte, estrutura);
            _mes = new EmpenhoCancelamentoMesService(l, mes);
            _item = new EmpenhoCancelamentoItemService(l, item);
            _repository = repository;
            _chave = new ChaveCicsmoService(l, chave);
            _programa = new ProgramaService(l, programa, estrutura);
        }


        public AcaoEfetuada Excluir(EmpenhoCancelamento objModel, int resource, short action)
        {
            try
            {
                _repository.Remove(objModel.Id);

                return resource > 0
                    ? LogSucesso(action,resource, $"Cancelamento do Empenho : Codigo {objModel.Id}")
                    : AcaoEfetuada.Sucesso;
            }
            catch (Exception ex) { throw SaveLog(ex, (short?)action, resource); }
        }

        public int Salvar(EmpenhoCancelamento objModel, IEnumerable<EmpenhoCancelamentoMes> months, IEnumerable<EmpenhoCancelamentoItem> items, int resource, short action)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 1: OBJMODEL = " + objModel.Id + " - MONTHS = " + months + " - ITEMS = " + items + " - ACTION = " + action));
            try
            {
                if (objModel.Id == 0)
                {
                    objModel.DataCadastramento = DateTime.Now;
                    objModel.Id = _repository.Add(objModel);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 2"));
                }
                else
                {
                    _repository.Edit(objModel);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 3"));
                }

                if (items != null && items.Any())
                {
                    ((List<EmpenhoCancelamentoItem>)items).ForEach(f => f.EmpenhoId = objModel.Id);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 4"));
                }

                _item.Salvar(objModel.Id, items, resource, action);

                if (months != null && months.Any(a => a.ValorMes > 0))
                {
                    ((List<EmpenhoCancelamentoMes>)months).ForEach(x => x.Id = objModel.Id);
                    _mes.Salvar(months, resource, action);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 5"));
                }

                var arg = $@"
                    Nº do empenho Prodesp {objModel.NumeroEmpenhoProdesp}
                  , Nº do empenho SIAFEM {objModel.NumeroEmpenhoSiafem}
                  , Nº do empenho SIAFISICO {objModel.NumeroEmpenhoSiafisico}.";

                if (resource > 0)
                {
                    LogSucesso(action, resource, arg);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 6"));
                }

                    return objModel.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - SALVAR - 7: EX = " + ex));
            }
        }

        public IEnumerable<EmpenhoCancelamento> Buscar(EmpenhoCancelamento objModel)
        {
            return _repository.Fetch(objModel);
        }

        public IEnumerable<EmpenhoCancelamento> BuscarGrid(EmpenhoCancelamento objModel)
        {
            return _repository.FetchForGrid(objModel);
        }

        public EmpenhoCancelamento BuscarAssinaturas(EmpenhoCancelamento objModel)
        {
            return _repository.BuscarAssinaturas(objModel);
        }

        public void Transmitir(int id, Usuario usuario, int resource)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR - 1: ID = " + id + " - USUARIO = " + usuario + " - RESOURCE = " + resource));
            var empenho = Buscar(new EmpenhoCancelamento { Id = id }).FirstOrDefault();

            if (AppConfig.WsUrl != "siafemProd")
            {
                usuario.CPF = empenho.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                usuario.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR - 2"));
            }

            try
            {
                Transmissao(usuario, empenho, resource);
                Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR - 3"));
            }
            catch (Exception ex)
            {
                Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (short)EnumAcao.Transmitir, resource);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR - 4: EX = " + ex));
            }
            finally
            {
                if (empenho.TransmitidoProdesp)
                {
                    SetCurrentTerminal(string.Empty);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR - 5: EMPTY = " + string.Empty));
                }
            }
        }

        public string Transmitir(IEnumerable<int> ids, Usuario usuario, int resource)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 1: IDS = " + ids + " - USUARIO = " + usuario + " - RESOURCE = " + resource));
            var empenhos = new List<EmpenhoCancelamento>();
            var result = default(string);

            foreach (var empenhoId in ids)
            {
                var empenho = new EmpenhoCancelamento();
                try
                {
                    empenho = Buscar(new EmpenhoCancelamento { Id = empenhoId }).FirstOrDefault() ?? new EmpenhoCancelamento();
                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        usuario.CPF = empenho.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        usuario.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 2"));
                    }

                    var dtoErro = Retransmissao(usuario, empenho, resource);

                    if (empenho.TransmitirProdesp && (empenho.TransmitirSiafem || empenho.TransmitirSiafisico))
                    {
                        if (empenho.StatusProdesp == "E" && empenho.StatusSiafemSiafisico == "S")
                        {
                            result += $";Erro {dtoErro.Prodesp}";
                            result += $" para transmissão Cancelamento de Empenho N° SIAFEM/SIAFISICO {empenho.NumeroEmpenhoSiafem}{empenho.NumeroEmpenhoSiafisico}";
                            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 3"));
                        }
                        else if ((empenho.StatusSiafemSiafisico == "E") && empenho.StatusProdesp == "S")
                        {
                            result += $";Erro {dtoErro.Siafem} para transmissão Cancelamento de Empenho N° Prodesp {empenho.NumeroEmpenhoProdesp}";
                            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 4"));
                        }

                        empenhos.Add(empenho);

                        if ((empenho.StatusSiafemSiafisico == "S") && empenho.StatusProdesp == "S")
                        {
                            Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
                            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 5"));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 6: EX = " + ex));
                }
                finally
                {
                    if (empenho.TransmitidoProdesp)
                    {
                        SetCurrentTerminal(string.Empty);
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 7: EMPTY = " + string.Empty));
                    }
                }
            }

            var empenhosErros =
                empenhos.Where(x => ((x.StatusProdesp == "E" || x.StatusProdesp == null) && (x.StatusSiafemSiafisico == "E")) ||
                        (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "N")) ||
                        (x.StatusProdesp == "N" && (x.StatusSiafemSiafisico == "E"))).ToList();

            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 8"));

            if (empenhosErros.Count > 0)
            {
                if (empenhos.Count == 1)
                {
                    result += empenhos.FirstOrDefault()?.MensagemSiafemSiafisico + Environment.NewLine + " | " + empenhos.FirstOrDefault()?.MensagemServicoProdesp;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 9"));
                }
                else
                {
                    result += Environment.NewLine + "; / Alguns Cancelamentos de Empenho não puderam ser transmitidos";
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 10"));
                }
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIR 2 - 11"));
            }

            return result;
        }

        private void Transmissao(Usuario usuario, EmpenhoCancelamento objModel, int resource)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMISSAO - 1: OBJMODEL = " + objModel.Id + " - USUARIO = " + usuario + " - RESOURCE = " + resource));
            var key = new ChaveCicsmo();

            try
            {
                if (objModel.TransmitirSiafem && !objModel.TransmitidoSiafem)
                    TransmitirSiafem(objModel, usuario, resource);

                if (objModel.TransmitirSiafisico && !objModel.TransmitidoSiafisico)
                    TransmitirSiafisico(objModel, usuario, resource);

                if (objModel.TransmitirProdesp && !objModel.TransmitidoProdesp)
                    TransmitirProdesp(objModel, resource);

                if ((objModel.TransmitidoSiafisico || objModel.TransmitidoSiafem) && objModel.TransmitidoProdesp)
                {
                    key = _chave.ObterChave();
                    objModel.StatusDocumento = _prodesp.InserirDoc(objModel, key.Chave, key.Senha, "04");
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMISSAO - 2"));
                }
            }
            catch (Exception ex)
            {
                throw;
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMISSAO - 3: EX = " + ex));
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMISSAO - 4"));
            }
        }

        private DtoError Retransmissao(Usuario usuario, EmpenhoCancelamento objModel, int resource)
        {
            var error = new DtoError();
            var key = new ChaveCicsmo();

            try
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 1: OBJMODEL = " + objModel.Id + " - USUARIO = " + usuario + " - RESOURCE = " + resource));
                try
                {
                    if (objModel.TransmitirSiafem && !objModel.TransmitidoSiafem)
                    {
                        TransmitirSiafem(objModel, usuario, resource);
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 2"));
                    }
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 3: EX = " + ex));
                }

                try
                {
                    if (objModel.TransmitirSiafisico && !objModel.TransmitidoSiafisico)
                    {
                        TransmitirSiafisico(objModel, usuario, resource);
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 4"));
                    }
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 5: EX = " + ex));
                }

                try
                {
                    if (string.IsNullOrWhiteSpace(error.Siafem) && objModel.TransmitirProdesp && !objModel.TransmitidoProdesp)
                    {
                        TransmitirProdesp(objModel, resource);
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 6"));
                    }
                }
                catch (Exception ex)
                {
                    error.Prodesp = ex.Message;
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 7: EX = " + ex));
                }

                if ((objModel.TransmitidoSiafisico || objModel.TransmitidoSiafem) && objModel.TransmitidoProdesp)
                {
                    key = _chave.ObterChave();
                    objModel.StatusDocumento = _prodesp.InserirDoc(objModel, key.Chave, key.Senha, "04");
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 8"));
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 9: EX = " + ex));
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - RETRANSMISSAO - 10"));
            }

            return error;
        }

        private void TransmitirProdesp(EmpenhoCancelamento objModel, int resource)
        {
            var key = new ChaveCicsmo();

            try
            {
                key = _chave.ObterChave();
                var mes = _mes.Buscar(new EmpenhoCancelamentoMes { Id = objModel.Id }).Cast<IMes>().ToList();

                objModel.NumeroEmpenhoProdesp = _prodesp.InserirEmpenhoCancelamento(
                    objModel, mes, key.Chave, key.Senha).Replace(" ", "");
                objModel.TransmitidoProdesp = true;
                objModel.StatusProdesp = "S";
                objModel.DataTransmitidoProdesp = DateTime.Now;
                objModel.MensagemServicoProdesp = null;

                Salvar(objModel, null, null, resource, (int)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                objModel.StatusProdesp = "E";
                objModel.MensagemServicoProdesp = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
            }
            finally { _chave.LiberarChave(key.Codigo); }
        }

        private void TransmitirSiafem(EmpenhoCancelamento objModel, Usuario usuario, int resource)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = Convert.ToInt32(usuario.RegionalId) }).FirstOrDefault()?.Uge;
                var months = _mes.Buscar(new EmpenhoCancelamentoMes { Id = objModel.Id }).Cast<IMes>();
                var items = _item.Buscar(new EmpenhoCancelamentoItem { EmpenhoId = objModel.Id }).Cast<IEmpenhoItem>();

                objModel.NumeroEmpenhoSiafem = _siafem.InserirEmpenhoCancelamentoSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), ref objModel, months, ug);


                _siafem.InserirEmpenhoCancelamentoSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), ref objModel, items, ug);

                objModel.TransmitidoSiafem = true;
                objModel.StatusSiafemSiafisico = "S";
                objModel.DataTransmitidoSiafem = DateTime.Now;
                objModel.MensagemSiafemSiafisico = null;

                Salvar(objModel, null, null, resource, (int)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                objModel.StatusSiafemSiafisico = "E";
                objModel.MensagemSiafemSiafisico = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
            }
        }

        private void TransmitirSiafisico(EmpenhoCancelamento objModel, Usuario usuario, int resource)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO CANCELAMENTO - TRANSMITIRSIAFISICO - 1: OBJMODEL = " + objModel + " - USUARIO = " + usuario + " - RESOURCE = " + resource));
            try
            {
                var ug = _regional.Buscar(new Regional { Id = Convert.ToInt32(usuario.RegionalId) }).First().Uge;
                var months = _mes.Buscar(new EmpenhoCancelamentoMes { Id = objModel.Id }).Cast<IMes>();
                var items = _item.Buscar(new EmpenhoCancelamentoItem { EmpenhoId = objModel.Id });

                if (objModel.StatusSiafisicoCT != "S")
                {
                    objModel.StatusSiafisicoCT = "E";
                    objModel.EmpenhoCancelamentoTipoId = 8;
                    objModel.NumeroCT = _siafem.InserirEmpenhoCancelamentoSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, months, ug);
                    objModel.StatusSiafisicoCT = "S";
                }

                foreach (var empenhoItem in items)
                {
                    if (empenhoItem.StatusSiafisicoItem == "N" || empenhoItem.StatusSiafisicoItem == "E")
                    {
                        InserirEmpenhoCancelamentoSiafisico(objModel, usuario, empenhoItem, ug, resource);
                    }
                }

                objModel.EmpenhoCancelamentoTipoId = 9;
                objModel.NumeroEmpenhoSiafisico = _siafem.ContablizarEmpenhoCancelamento(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, months, ug);

                objModel.TransmitidoSiafisico = true;
                objModel.StatusSiafemSiafisico = "S";
                objModel.DataTransmitidoSiafisico = DateTime.Now;
                objModel.MensagemSiafemSiafisico = null;
            }
            catch (Exception ex)
            {
                objModel.StatusSiafemSiafisico = "E";
                objModel.MensagemSiafemSiafisico = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
            }
            finally
            {
                Salvar(objModel, null, null, resource, (int)EnumAcao.Transmitir);
            }
        }

        private void InserirEmpenhoCancelamentoSiafisico(EmpenhoCancelamento objModel, Usuario usuario, EmpenhoCancelamentoItem item, string ug, int resource)
        {
            try
            {
                item.StatusSiafisicoItem = "E";
                item.SequenciaItem = _siafem.InserirEmpenhoCancelamentoSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, item, ug);
                item.StatusSiafisicoItem = "S";
            }
            finally
            {
                _item.Salvar(item, resource, (int)EnumAcao.Transmitir);
            }
        }

    }
}






