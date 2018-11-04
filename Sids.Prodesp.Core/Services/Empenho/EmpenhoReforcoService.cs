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

    public class EmpenhoReforcoService : EmpenhoBaseService<EmpenhoReforco, EmpenhoReforcoItem, EmpenhoReforcoMes>
    {
        private readonly ICrudEmpenhoReforco _repository;
        private readonly ProdespEmpenhoService _prodesp;
        private readonly SiafemEmpenhoService _siafem;
        private readonly EmpenhoReforcoMesService _mes;
        private readonly EmpenhoReforcoItemService _item;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;



        public EmpenhoReforcoService(ILogError l, ICrudEmpenhoReforco repository, ICrudEmpenhoReforcoMes mesDal, ICrudEmpenhoReforcoItem item,
            IProdespEmpenho prodesp, ISiafemEmpenho siafem, ICrudPrograma programa,
            ICrudFonte fonte, ICrudEstrutura estrutura, IRegional regional, IChaveCicsmo chave, ICommon c)
            //: base(l, c, new ProdespReservaWs(), new SiafemReservaWs(), siafem, chave)
            : base(l, prodesp, siafem, programa, fonte, estrutura, regional, chave, c, new EmpenhoReforcoItemService(l, item))
        {
            _prodesp = new ProdespEmpenhoService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemEmpenhoService(l, siafem, programa, fonte, estrutura);
            _mes = new EmpenhoReforcoMesService(l, mesDal);
            _item = new EmpenhoReforcoItemService(l, item);
            _repository = repository;
            _chave = new ChaveCicsmoService(l, chave);
            _programa = new ProgramaService(l, programa, estrutura);
        }


        public AcaoEfetuada Excluir(EmpenhoReforco objModel, int resource, short action)
        {
            try
            {
                _repository.Remove(objModel.Id);

                return resource > 0 
                    ? LogSucesso(action,resource, $"Reforço do Empenho : Codigo {objModel.Id}")
                    : AcaoEfetuada.Sucesso;
            }
            catch (Exception ex) { throw SaveLog(ex, (short?)action, resource); }
        }

        public int Salvar(EmpenhoReforco objModel, IEnumerable<EmpenhoReforcoMes> months, IEnumerable<EmpenhoReforcoItem> items, int resource, short action)
        {
            try
            {
                if (objModel.Id == 0)
                {
                    objModel.DataCadastramento = DateTime.Now;
                    objModel.Id = _repository.Add(objModel);
                }
                else { _repository.Edit(objModel); }

                if (items != null && items.Any())
                {
                    ((List<EmpenhoReforcoItem>)items).ForEach(f => f.EmpenhoId = objModel.Id);
                }
                _item.Salvar(objModel.Id, items, resource, action);

                if (months != null && months.Any(a => a.ValorMes > 0))
                {
                    ((List<EmpenhoReforcoMes>)months).ForEach(x => x.Id = objModel.Id);
                    _mes.Salvar(months, resource, action);
                }

                var arg = $@"
                    Nº do empenho Prodesp {objModel.NumeroEmpenhoProdesp}
                  , Nº do empenho SIAFEM {objModel.NumeroEmpenhoSiafem}
                  , Nº do empenho SIAFISICO {objModel.NumeroEmpenhoSiafisico}.";

                if (resource > 0) LogSucesso(action,resource, arg);

                return objModel.Id;
            }
            catch (Exception ex) { throw SaveLog(ex, (short)action, resource); }
        }

        public IEnumerable<EmpenhoReforco> Buscar(EmpenhoReforco objModel)
        {
            return _repository.Fetch(objModel);
        }

        public IEnumerable<EmpenhoReforco> BuscarGrid(EmpenhoReforco objModel)
        {
            return _repository.FetchForGrid(objModel);
        }

        public EmpenhoReforco BuscarAssinaturas(EmpenhoReforco objModel)
        {
            return _repository.BuscarAssinaturas(objModel);
        }
        public void Transmitir(int id, Usuario usuario, int resource)
        {
            var empenho = Buscar(new EmpenhoReforco { Id = id }).FirstOrDefault();

            if (AppConfig.WsUrl != "siafemProd")
            {
                usuario.CPF = empenho.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                usuario.SenhaSiafem = Encrypt(AppConfig.WsPassword);
            }

            try
            {
                Transmissao(usuario, empenho, resource);
                Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {

                Salvar(empenho, null, null, resource, (int)EnumAcao.Transmitir);
                throw SaveLog(ex, (short)EnumAcao.Transmitir, resource);
            }
            finally
            {
                if (empenho.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
            }
        }

        public string Transmitir(IList<int> ids, Usuario usuario, int resource)
        {
            var empenhos = new List<EmpenhoReforco>();
            var result = default(string);

            foreach (var empenhoId in ids)
            {
                var empenho = new EmpenhoReforco();
                try
                {
                    empenho = Buscar(new EmpenhoReforco { Id = empenhoId }).FirstOrDefault() ?? new EmpenhoReforco();
                    if (AppConfig.WsUrl != "siafemProd")
                    {
                        usuario.CPF = empenho.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                        usuario.SenhaSiafem = Encrypt(AppConfig.WsPassword);
                    }

                    var dtoErro = Retransmissao(usuario, empenho, resource);

                    if (empenho.TransmitirProdesp && (empenho.TransmitirSiafem || empenho.TransmitirSiafisico))
                        if (empenho.StatusProdesp == "E" && (empenho.StatusSiafemSiafisico == "S"))
                        {
                            result += $";Erro {dtoErro.Prodesp}";
                            result += $" para transmissão Reforço de Empenho  N° SIAFEM/SIAFISICO {empenho.NumeroEmpenhoSiafem}{empenho.NumeroEmpenhoSiafisico}";

                        }
                        else if ((empenho.StatusSiafemSiafisico == "E") && empenho.StatusProdesp == "S")
                            result += $";Erro {dtoErro.Siafem} para transmissão Reforço de Empenho N° Prodesp {empenho.NumeroEmpenhoProdesp}";

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
                    result += Environment.NewLine + "; / Alguns Reforços de Empenho  não puderam ser transmitidos";

            return result;
        }

        private void Transmissao(Usuario usuario, EmpenhoReforco objModel, int resource)
        {
            var key = new ChaveCicsmo();

            try
            {
                if (objModel.TransmitirSiafem && !objModel.TransmitidoSiafem)
                    TransmitirSiafem(objModel, usuario, resource);

                if (objModel.TransmitirSiafisico && !objModel.TransmitidoSiafisico)
                    TransmitirSiafisico(objModel, usuario, resource);

                if (objModel.TransmitirProdesp && !objModel.TransmitidoProdesp)
                    TransmitirProdesp(objModel, resource);

                if ((objModel.TransmitidoProdesp || objModel.TransmitidoSiafem) && objModel.TransmitidoSiafisico)
                {
                    key = _chave.ObterChave();
                    objModel.StatusDocumento = _prodesp.InserirDoc(objModel, key.Chave, key.Senha, "09");
                }
            }
            catch
            {
                throw;
            }
            finally { _chave.LiberarChave(key.Codigo); }
        }

        private DtoError Retransmissao(Usuario usuario, EmpenhoReforco objModel, int resource)
        {
            var error = new DtoError();
            var key = new ChaveCicsmo();

            try
            {
                try
                {
                    if (objModel.TransmitirSiafem && !objModel.TransmitidoSiafem) TransmitirSiafem(objModel, usuario, resource);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                try
                {
                    if (objModel.TransmitirSiafisico && !objModel.TransmitidoSiafisico) TransmitirSiafisico(objModel, usuario, resource);
                }
                catch (Exception ex)
                {
                    error.Siafem = ex.Message;
                }

                try
                {
                    if (string.IsNullOrWhiteSpace(error.Siafem) && objModel.TransmitirProdesp && !objModel.TransmitidoProdesp) TransmitirProdesp(objModel, resource);
                }
                catch (Exception ex)
                {
                    error.Prodesp = ex.Message;
                }

                if ((objModel.TransmitidoSiafisico || objModel.TransmitidoSiafem) && objModel.TransmitidoProdesp)
                {
                    key = _chave.ObterChave();
                    objModel.StatusDocumento = _prodesp.InserirDoc(objModel, key.Chave, key.Senha, "09");
                }
            }
            catch (Exception ex) { throw SaveLog(ex, (short?)EnumAcao.Transmitir, resource); }
            finally { _chave.LiberarChave(key.Codigo); }

            return error;
        }

        private void TransmitirProdesp(EmpenhoReforco objModel, int resource)
        {
            var key = new ChaveCicsmo();

            try
            {
                key = _chave.ObterChave();

                var mes = _mes.Buscar(new EmpenhoReforcoMes { Id = objModel.Id }).Cast<IMes>().ToList();

                objModel.NumeroEmpenhoProdesp = _prodesp.InserirEmpenhoReforco(
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

        private void TransmitirSiafem(EmpenhoReforco objModel, Usuario usuario, int resource)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;
                var months = _mes.Buscar(new EmpenhoReforcoMes { Id = objModel.Id }).Cast<IMes>().ToList();
                var items = _item.Buscar(new EmpenhoReforcoItem { EmpenhoId = objModel.Id }).ToList();

                objModel.NumeroEmpenhoSiafem = string.IsNullOrWhiteSpace(objModel.NumeroEmpenhoSiafem) ? _siafem.InserirEmpenhoReforcoSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), ref objModel, months, items, ug) : _siafem.InserirEmpenhoReforcoSiafem(usuario.CPF, Decrypt(usuario.SenhaSiafem), ref objModel, items, ug);

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

        private void TransmitirSiafisico(EmpenhoReforco objModel, Usuario usuario, int resource)
        {
            try
            {
                var ug = _regional.Buscar(new Regional { Id = Convert.ToInt32(usuario.RegionalId) }).First().Uge;
                var months = _mes.Buscar(new EmpenhoReforcoMes { Id = objModel.Id }).Cast<IMes>().ToList();
                var items = _item.Buscar(new EmpenhoReforcoItem { EmpenhoId = objModel.Id }).ToList();

                if (objModel.StatusSiafisicoCT != "S")
                {
                    objModel.StatusSiafisicoCT = "E";
                    objModel.NumeroCT = _siafem.InserirEmpenhoReforcoSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, months, ug);
                    objModel.StatusSiafisicoCT = "S";
                }

                foreach (var empenhoItem in items)
                {
                    if (empenhoItem.StatusSiafisicoItem == "N" || empenhoItem.StatusSiafisicoItem == "E")
                    {
                        InserirEmpenhoSiafisico(objModel, usuario, empenhoItem, ug, resource);
                    }
                    else if (empenhoItem.StatusSiafisicoItem == "S")
                    {
                        InserirEmpenhoSiafisicoAlteracao(objModel, usuario, empenhoItem, ug, resource);
                    }
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

        private void InserirEmpenhoSiafisicoAlteracao(EmpenhoReforco objModel, Usuario usuario, EmpenhoReforcoItem item, string ug, int resource)
        {
            try
            {
                item.StatusSiafisicoItem = "E";
                item.SequenciaItem = _siafem.InserirEmpenhoSiafisicoReforcoAlteracao(
                    usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, item, ug);
                item.StatusSiafisicoItem = "S";
            }
            finally
            {
                item.StatusSiafisicoItem = "S";
                _item.Salvar(item, resource, (int)EnumAcao.Transmitir);
            }
        }

        private void InserirEmpenhoSiafisico(EmpenhoReforco objModel, Usuario usuario, EmpenhoReforcoItem item, string ug, int resource)
        {
            try
            {
                item.StatusSiafisicoItem = "E";
                item.SequenciaItem = _siafem.InserirEmpenhoReforcoSiafisico(usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, item, ug);
                item.StatusSiafisicoItem = "S";
            }
            finally
            {
                _item.Salvar(item, resource, (int)EnumAcao.Transmitir);
            }
        }
    }
}






