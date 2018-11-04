using System;
using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.Configuracao;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class DesdobramentoService : CommonService
    {
        private readonly ICrudDesdobramento _repository;
        private readonly ProdespPagamentoContaUnicaService _prodesp;
        private readonly ChaveCicsmoService _chave;
        private readonly IdentificacaoDesdobramentoService _identityRepository;
        private readonly DesdobramentoTipoService _tipoRepository;
        private readonly DocumentoTipoService _docRepository;

        public DesdobramentoService(ILogError log, ICommon common, IChaveCicsmo chave, ICrudDesdobramento repository, IProdespPagamentoContaUnica prodesp)
            : base(log, common, chave)
        {

            _prodesp = new ProdespPagamentoContaUnicaService(new LogErrorDal(), new ProdespPagamentoContaUnicaWs());
            _chave = new ChaveCicsmoService(log, chave);
            _repository = repository;
            _identityRepository = new IdentificacaoDesdobramentoService(new IdentificacaoDesdobramentoDal());
            _tipoRepository = new DesdobramentoTipoService(new DesdobramentoTipoDal());
            _docRepository = new DocumentoTipoService(new DocumentoTipoDal());
        }

        public AcaoEfetuada Excluir(Desdobramento entity, int recursoId, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"Desdobramento : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        public int SalvarOuAlterar(Desdobramento entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (entity.IdentificacaoDesdobramentos != null)
                    SalvarOuAlterarIdentificacao(entity);

                if (recursoId > 0) LogSucesso(action, recursoId, $"Desdobramento : Codigo {entity.Id}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        private void SalvarOuAlterarIdentificacao(Desdobramento entity)
        {
            var salvos = _identityRepository.Listar(new IdentificacaoDesdobramento { Desdobramento = entity.Id });
            var deleta = salvos?.Where(w => entity.IdentificacaoDesdobramentos.All(a => a.Id != w.Id)) ?? new List<IdentificacaoDesdobramento>();

            foreach (var identificacaoDesdobramento in deleta)
            {
                _identityRepository.Excluir(identificacaoDesdobramento);
            }

            entity.IdentificacaoDesdobramentos.ToList().ForEach(x => x.Desdobramento = entity.Id);
            _identityRepository.SalvarOuAlterar(entity.IdentificacaoDesdobramentos);
        }

        public IEnumerable<Desdobramento> Listar(Desdobramento entity)
        {
            return _repository.Fetch(entity);
        }

        public IEnumerable<Desdobramento> BuscarGrid(Desdobramento entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var result = _repository.FetchForGrid(entity, de, ate);
            return result;
        }

        public Desdobramento Selecionar(int id)
        {
            var entity = _repository.Get(id);

            entity.IdentificacaoDesdobramentos = _identityRepository.Listar(new IdentificacaoDesdobramento { Desdobramento = id }).ToList();
            entity.DesdobramentoTipo = _tipoRepository.Listar(new DesdobramentoTipo { Id = entity.DesdobramentoTipoId }).FirstOrDefault();
            entity.DocumentoTipo = _docRepository.Listar(new DocumentoTipo { Id = entity.DocumentoTipoId }).FirstOrDefault();
            return entity;
        }

        public void Transmitir(int entityId, Usuario user, int recursoId)
        {

            var entity = Selecionar(entityId);

            try
            {
                Transmissao(user, entity, recursoId);
                SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                if (entity.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
            }
        }

        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var desdobramentos = new List<Desdobramento>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                var entity = new Desdobramento();
                try
                {
                    entity = Selecionar(empenhoId);

                    Retransmissao(user, entity, recursoId);

                    desdobramentos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
                    if (entity.TransmitidoProdesp) SetCurrentTerminal(string.Empty);
                }
            }

            var desdobramentoErros =
                desdobramentos.Where(x => x.StatusProdesp == "E").ToList();

            if (desdobramentoErros.Count > 0)
                if (desdobramentos.Count == 1)
                    result += desdobramentos.FirstOrDefault()?.MensagemServicoProdesp;
                else
                    result += Environment.NewLine + "; / Alguns Desdobramentos não puderam ser retransmitidos";


            return result;
        }

        private void Transmissao(Usuario user, Desdobramento entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {

                if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId);

            }
            catch
            {
                throw;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        private Error Retransmissao(Usuario user, Desdobramento entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                { if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId); }
                catch (Exception ex)
                { error.Prodesp = ex.Message; }

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
            return error;
        }

        private void TransmitirProdesp(Desdobramento entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                if (entity.DesdobramentoTipoId == 1)
                    _prodesp.Inserir_DesdobramentoISSQN(cicsmo.Chave, cicsmo.Senha, ref entity);
                else
                    _prodesp.Inserir_DesdobramentoOutros(cicsmo.Chave, cicsmo.Senha, ref entity);

                _chave.LiberarChave(cicsmo.Codigo);

                entity.TransmitidoProdesp = true;
                entity.StatusProdesp = "S";
                entity.DataTransmitidoProdesp = DateTime.Now;
                entity.MensagemServicoProdesp = null;
                
            }
            catch (Exception ex)
            {
                _chave.LiberarChave(cicsmo.Codigo);
                entity.StatusProdesp = "E";
               entity.MensagemServicoProdesp = ex.Message;
                throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
        }

        public void TransmitirAnulacaoProdesp(Desdobramento entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                _prodesp.AnulacaoDesdobramento(cicsmo.Chave, cicsmo.Senha, ref entity);

                _chave.LiberarChave(cicsmo.Codigo);

                entity.SituacaoDesdobramento = "S";
                entity.TransmitidoProdesp = true;
                entity.MensagemServicoProdesp = null;

            }
            catch (Exception ex)
            {
                _chave.LiberarChave(cicsmo.Codigo);
                entity.StatusProdesp = "E";
                entity.MensagemServicoProdesp = ex.Message;
                throw SaveLog(ex, (short?) EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
        }

        public IEnumerable<ConsultaDesdobramento> ConsultaDesdobramento(Desdobramento desdobramento)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave();
                return _prodesp.ConsultaDesdobramento(cicsmo.Chave, cicsmo.Senha, desdobramento.NumeroDocumento, desdobramento.DocumentoTipoId);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        internal class Error
        {
            public string Prodesp { get; set; }
            public string SiafemSiafisico { get; set; }
        }
    }
}
