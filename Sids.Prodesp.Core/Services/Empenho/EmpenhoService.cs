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
    using Infrastructure.Services.Empenho;
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

    public class EmpenhoService : EmpenhoBaseService<Empenho, EmpenhoItem, EmpenhoMes>
    {
        private readonly ICrudEmpenho _repository;
        private readonly ProdespEmpenhoService _prodesp;
        private readonly SiafemEmpenhoService _siafem;
        private readonly EmpenhoMesService _mes;
        private readonly EmpenhoItemService _item;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;


        public EmpenhoService(ILogError l, ICrudEmpenho repository, ICrudEmpenhoMes mesDal, ICrudEmpenhoItem item,
            IProdespEmpenho prodesp, ISiafemEmpenho siafem, ICrudPrograma programa,
            ICrudFonte fonte, ICrudEstrutura estrutura, IRegional regional, IChaveCicsmo chave, ICommon c)
            : base(l, prodesp, siafem, programa, fonte, estrutura, regional, chave, c, new EmpenhoItemService(l, item))
        {
            _prodesp = new ProdespEmpenhoService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemEmpenhoService(l, siafem, programa, fonte, estrutura);
            _mes = new EmpenhoMesService(l, mesDal);
            _item = new EmpenhoItemService(l, item);
            _repository = repository;
            _chave = new ChaveCicsmoService(l, chave);
            _programa = new ProgramaService(l, programa, estrutura);
        }


        public AcaoEfetuada Excluir(Empenho empenho, int resource, short action)
        {
            try
            {
                _repository.Remove(empenho.Id);

                return resource > 0
                    ? LogSucesso(action, resource, $"Empenho : Codigo {empenho.Id}")
                    : AcaoEfetuada.Sucesso;
            }
            catch (Exception ex) { throw SaveLog(ex, action, resource); }
        }

        public int Salvar(Empenho objModel, IEnumerable<EmpenhoMes> months, IEnumerable<EmpenhoItem> items, int resource, short action)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(objModel.NumeroCT) && objModel.TransmitirSiafisico)
                {
                    //objModel.StatusProdesp = "N";
                    //objModel.StatusSiafemSiafisico = "N";
                    objModel.StatusSiafisicoCT = "N";
                    objModel.StatusSiafisicoNE = "N";
                }

                if (objModel.Id == 0)
                {
                    objModel.DataCadastramento = DateTime.Now;
                    objModel.Id = _repository.Add(objModel);
                }
                else
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - SALVAR - 1: OBJMODEL-DATAPRODESP = " + objModel.DataTransmitidoProdesp + " - OBJMODEL-DATASIAFEM = " + objModel.DataTransmitidoSiafem));
                    _repository.Edit(objModel);
                }

                if (items != null && items.Any())
                {
                    ((List<EmpenhoItem>)items).ForEach(f => f.EmpenhoId = objModel.Id);
                }
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - SALVAR - 2: OBJMODEL-DATAPRODESP = " + objModel.DataTransmitidoProdesp + " - OBJMODEL-DATASIAFEM = " + objModel.DataTransmitidoSiafem));
                _item.Salvar(objModel.Id, items, resource, action);

                if (months != null && months.Any(a => a.ValorMes > 0))
                {
                    ((List<EmpenhoMes>)months).ForEach(x => x.Id = objModel.Id);
                    _mes.Salvar(months, resource, action);
                }

                var arg = $@"
                    Nº do empenho Prodesp {objModel.NumeroEmpenhoProdesp}
                  , Nº do empenho SIAFEM {objModel.NumeroEmpenhoSiafem}
                  , Nº do empenho SIAFISICO {objModel.NumeroEmpenhoSiafisico}.";

                if (resource > 0) LogSucesso(action, resource, arg);

                return objModel.Id;
            }
            catch (Exception ex) { throw SaveLog(ex, (short)action, resource); }
        }

        public IEnumerable<Empenho> Buscar(Empenho objModel)
        {
            return _repository.Fetch(objModel);
        }

        public IEnumerable<Empenho> BuscarGrid(Empenho objModel)
        {
            return _repository.FetchForGrid(objModel);
        }

        public Empenho BuscarAssinaturas(Empenho objModel)
        {
            return _repository.BuscarAssinaturas(objModel);
        }

        public void Transmitir(int empenhoId, Usuario usuario, int resource)
        {
            var empenho = Buscar(new Empenho { Id = empenhoId }).FirstOrDefault();

            if (AppConfig.WsUrl != "siafemProd")
            {
                usuario.CPF = empenho.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                usuario.SenhaSiafem = Encrypt(AppConfig.WsPassword);
            }

            try
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMITIR - 1: EMPENHO-DATAPRODESP = " + empenho.DataTransmitidoProdesp + " - EMPENHO-DATASIAFEM = " + empenho.DataTransmitidoSiafem));
                Transmissao(usuario, empenho, resource);
                Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, resource);
            }
            finally
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMITIR - 2: EMPENHO-DATAPRODESP = " + empenho.DataTransmitidoProdesp + " - EMPENHO-DATASIAFEM = " + empenho.DataTransmitidoSiafem));
                Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
                if (empenho.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
            }
        }

        public string Transmitir(IEnumerable<int> empenhoIds, Usuario usuario, int resource)
        {
            var empenhos = new List<Empenho>();
            var result = default(string);

            foreach (var empenhoId in empenhoIds)
            {
                var empenho = new Empenho();
                try
                {
                    empenho = Buscar(new Empenho { Id = empenhoId }).FirstOrDefault() ?? new Empenho();
                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        usuario.CPF = empenho.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        usuario.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                    }

                    var dtoErro = Retransmissao(usuario, empenho, resource);

                    if (empenho.TransmitirProdesp && (empenho.TransmitirSiafem || empenho.TransmitirSiafisico))
                        if (empenho.StatusProdesp == "E" &&
                            (empenho.StatusSiafemSiafisico == "S"))
                        {
                            result += $";Erro {dtoErro.Prodesp}";
                            result += $" para transmissão Empenho N° SIAFEM/SIAFISICO {empenho.NumeroEmpenhoSiafem}{empenho.NumeroEmpenhoSiafisico}";

                        }
                        else if ((empenho.StatusSiafemSiafisico == "E") &&
                                 empenho.StatusProdesp == "S")
                            result += $";Erro {dtoErro.Siafem} para transmissão Empenho N° Prodesp {empenho.NumeroEmpenhoProdesp}";

                    empenhos.Add(empenho);

                    if ((empenho.StatusSiafemSiafisico == "S") && empenho.StatusProdesp == "S")
                        Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
                }
                finally
                {

                    Salvar(empenho, null, null, 0, (int)EnumAcao.Transmitir);
                    if (empenho.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
                }
            }

            var empenhosErros =
                empenhos.Where(x => (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "E")) ||
                        (x.StatusProdesp == "E" && (x.StatusSiafemSiafisico == "N")) ||
                        (x.StatusProdesp == "N" && (x.StatusSiafemSiafisico == "E"))).ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault()?.MensagemSiafemSiafisico + Environment.NewLine + " | " + empenhos.FirstOrDefault()?.MensagemServicoProdesp;
                else
                    result += Environment.NewLine + "; / Alguns Empenhos não puderam ser transmitidos";

            return result;
        }

        private void Transmissao(Usuario usuario, Empenho objModel, int resource)
        {
            var key = new ChaveCicsmo();

            try
            {
                if (objModel.TransmitirSiafem && !objModel.TransmitidoSiafem)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMISSÃO - SIAFEM - 1: OBJMODEL-DATAPRODESP = " + objModel.DataTransmitidoProdesp + " - OBJMODEL-DATASIAFEM = " + objModel.DataTransmitidoSiafem));
                    TransmitirSiafem(objModel, usuario, resource);
                }
                if (objModel.TransmitirSiafisico && !objModel.TransmitidoSiafisico)
                    TransmitirSiafisico(objModel, usuario, resource);

                if (objModel.TransmitirProdesp && !objModel.TransmitidoProdesp)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMISSÃO - PRODESP - 2: OBJMODEL-DATAPRODESP = " + objModel.DataTransmitidoProdesp + " - OBJMODEL-DATASIAFEM = " + objModel.DataTransmitidoSiafem));
                    TransmitirProdesp(objModel, resource);
                }

                if ((objModel.TransmitidoProdesp || objModel.TransmitidoSiafem) && objModel.TransmitidoSiafisico)
                {
                    key = _chave.ObterChave();
                    objModel.StatusDocumento = _prodesp.InserirDoc(objModel, key.Chave, key.Senha, "03");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }

        private DtoError Retransmissao(Usuario usuario, Empenho objModel, int resource)
        {
            var error = new DtoError();
            var key = new ChaveCicsmo();

            try
            {
                try
                {
                    if (objModel.TransmitirSiafem && !objModel.TransmitidoSiafem)
                        TransmitirSiafem(objModel, usuario, resource);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                try
                {
                    if (objModel.TransmitirSiafisico && !objModel.TransmitidoSiafisico)
                        TransmitirSiafisico(objModel, usuario, resource);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                try
                {
                    if (string.IsNullOrWhiteSpace(error.Siafem) && objModel.TransmitirProdesp &&
                        !objModel.TransmitidoProdesp) TransmitirProdesp(objModel, resource);
                }
                catch (Exception ex)
                {
                    error.Prodesp = ex.Message;
                }

                if ((objModel.TransmitidoSiafisico || objModel.TransmitidoSiafem) && objModel.TransmitidoProdesp)
                {
                    key = _chave.ObterChave();
                    objModel.StatusDocumento = _prodesp.InserirDoc(objModel, key.Chave, key.Senha, "03");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
            }
            finally { _chave.LiberarChave(key.Codigo); }

            return error;
        }

        private void TransmitirProdesp(Empenho objModel, int resource)
        {
            var key = new ChaveCicsmo();

            try
            {
                key = _chave.ObterChave();

                var mes = _mes.Buscar(new EmpenhoMes { Id = objModel.Id }).Cast<IMes>().ToList();
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMITIRPRODESP - 1: OBJMODEL = " + objModel.DataTransmitidoProdesp));
                objModel.NumeroEmpenhoProdesp = _prodesp.InserirEmpenho(objModel, mes, key.Chave, key.Senha).Replace(" ", "");
                objModel.TransmitidoProdesp = true;
                objModel.StatusProdesp = "S";
                objModel.DataTransmitidoProdesp = DateTime.Now;
                objModel.MensagemServicoProdesp = null;
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMITIRPRODESP - 2: OBJMODEL = " + objModel.DataTransmitidoProdesp));
            }
            catch (Exception ex)
            {
                objModel.StatusProdesp = "E";
                objModel.MensagemServicoProdesp = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource);
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
                Salvar(objModel, null, null, resource, (int)EnumAcao.Transmitir);
            }
        }

        private void TransmitirSiafem(Empenho objModel, Usuario usuario, int resource)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = Convert.ToInt32(usuario.RegionalId) }).FirstOrDefault()?.Uge;
                var months = _mes.Buscar(new EmpenhoMes { Id = objModel.Id }).Cast<IMes>().ToList();
                var items = _item.Buscar(new EmpenhoItem { EmpenhoId = objModel.Id }).Cast<EmpenhoItem>().ToList();
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMITIRSIAFEM - 1: OBJMODEL = " + objModel.DataTransmitidoSiafem));
                objModel.NumeroEmpenhoSiafem = _siafem.InserirEmpenhoSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), ref objModel, months, items, ug);
                objModel.TransmitidoSiafem = true;
                objModel.StatusSiafemSiafisico = "S";
                objModel.DataTransmitidoSiafem = DateTime.Now;
                objModel.MensagemSiafemSiafisico = null;
                Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("ERROR EMPENHO - TRANSMITIRSIAFEM - 2: OBJMODEL = " + objModel.DataTransmitidoSiafem));
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

        private void TransmitirSiafisico(Empenho objModel, Usuario usuario, int resource)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = Convert.ToInt32(usuario.RegionalId) }).FirstOrDefault()?.Uge;
                var months = _mes.Buscar(new EmpenhoMes { Id = objModel.Id }).Cast<IMes>().ToList();
                var items = _item.Buscar(new EmpenhoItem { EmpenhoId = objModel.Id }).ToList();

                if ((objModel.StatusSiafisicoCT != "S" || string.IsNullOrEmpty(objModel.StatusSiafisicoCT)) && string.IsNullOrEmpty(objModel.NumeroCT))
                {
                    objModel.StatusSiafisicoCT = "E";

                    EnumAcaoSiaf acaoSiaf = EnumAcaoSiaf.Alterar;

                    if (string.IsNullOrEmpty(objModel.NumeroCT))
                    {
                        //objModel.NumeroCT = _siafem.InserirEmpenhoSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, months, items, ug);
                        acaoSiaf = EnumAcaoSiaf.Inserir;
                    }
                    else
                    {
                        //objModel.NumeroCT = _siafem.InserirEmpenhoSiafisicoAlteracao(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, months, ug);
                        acaoSiaf = EnumAcaoSiaf.Alterar;
                    }

                    var senha = Decrypt(usuario.SenhaSiafem);

                    objModel.NumeroCT = base.TransmitirEmpenho(objModel, usuario, ug, months, items, acaoSiaf, senha);

                    objModel.StatusSiafisicoCT = "S";

                    base.TransmitirItens(EnumTipoServicoFazenda.Siafisico, objModel, items, usuario, ug, resource);
                }


                objModel.NumeroEmpenhoSiafisico = _siafem.ContablizarEmpenho(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, ug);
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

        private void InserirEmpenhoSiafisicoAlteracao(Empenho objModel, Usuario usuario, EmpenhoItem item, string ug, int resource)
        {
            try
            {
                item.StatusSiafisicoItem = "E";
                item.SequenciaItem = _siafem.InserirEmpenhoSiafisicoAlteracao(
                    usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, item, ug);
                item.StatusSiafisicoItem = "S";
            }
            finally
            {
                item.StatusSiafisicoItem = "S";
                _item.Salvar(item, resource, (int)EnumAcao.Transmitir);
            }
        }

        //private void InserirEmpenhoSiafisico(Empenho objModel, Usuario usuario, EmpenhoItem item, string ug)
        //{
        //    try
        //    {
        //        item.StatusSiafisicoItem = "E";
        //        item.SequenciaItem = _siafem.InserirEmpenhoSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, item, ug);
        //        item.StatusSiafisicoItem = "S";
        //    }
        //    finally
        //    {
        //        _item.Salvar(item, 0, (int)EnumAcao.Transmitir);
        //    }
        //}
    }
}
