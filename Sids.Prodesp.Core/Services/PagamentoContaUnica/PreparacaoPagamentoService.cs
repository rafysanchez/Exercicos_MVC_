using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaDer;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.DataBase.Configuracao;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaDer;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Core.Services.Reserva;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class PreparacaoPagamentoService : CommonService
    {
        private readonly ICrudPreparacaoPagamento _repository;
        private readonly PreparacaoPagamentoTipoService _preparacaoPagamentoTipo;
        private readonly ProdespPagamentoContaUnicaService _prodespContaUnica;
        private readonly ProdespPagamentoContaDerService _prodespContaDer;
        private readonly ChaveCicsmoService _chave;
        public PreparacaoPagamentoService(ILogError log, ICommon common, IChaveCicsmo chave, IProdespPagamentoContaUnica prodesp, IProdespPagamentoContaDer prodespContaDer, ICrudPreparacaoPagamento repository) : base(log, common, chave)
        {
            _prodespContaDer = new ProdespPagamentoContaDerService(log, prodespContaDer); 
            _preparacaoPagamentoTipo = new PreparacaoPagamentoTipoService(new PreparacaoPagamentoTipoDal());
            _prodespContaUnica = new ProdespPagamentoContaUnicaService(log, prodesp);
            _repository = repository;
            _chave = new ChaveCicsmoService(log, chave);
        }

        public AcaoEfetuada Excluir(PreparacaoPagamento entity, int recursoId, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                if (recursoId > 0) return LogSucesso(action, recursoId, $"Preparação de Pagamento : Codigo {entity.Id}");

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        public int SalvarOuAlterar(PreparacaoPagamento entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (recursoId > 0 && entity.StatusProdesp == "S") LogSucesso(action, recursoId, $"Preparação de Pagamento : Codigo {entity.Id}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }
        


        public IEnumerable<PreparacaoPagamento> Listar(PreparacaoPagamento entity)
        {
            return _repository.Fetch(entity);
        }


        public PreparacaoPagamento Selecionar(int id)
        {
            var entity = _repository.Get(id);
            entity.PreparacaoPagamentoTipo = _preparacaoPagamentoTipo.Listar(new PreparacaoPagamentoTipo { Id = entity.PreparacaoPagamentoTipoId}).FirstOrDefault();
            return entity;
        }


        public IEnumerable<PreparacaoPagamento> BuscarGrid(PreparacaoPagamento entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var result = _repository.FetchForGrid(entity, de, ate).ToList();
            var tipos = _preparacaoPagamentoTipo.Listar(new PreparacaoPagamentoTipo());
            result.ForEach(x => x.PreparacaoPagamentoTipo = tipos.FirstOrDefault(t => t.Id == x.PreparacaoPagamentoTipoId));
            return result;
        }



        public void Transmitir(int entityId, Usuario user, int recursoId)
        {

            var entity = Selecionar(entityId);
            try
            {
                Transmissao(user, entity, recursoId);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
            }
        }

        public string Transmitir(IEnumerable<int> entityIdList, Usuario user, int recursoId)
        {
            var empenhos = new List<PreparacaoPagamento>();
            var result = default(string);

            foreach (var empenhoId in entityIdList)
            {
                var entity = new PreparacaoPagamento();
                try
                {
                    entity = Selecionar(empenhoId);

                    Retransmissao(user, entity, recursoId);
                    empenhos.Add(entity);
                }
                catch (Exception ex)
                {
                    throw SaveLog(ex, (short?)EnumAcao.Transmitir, recursoId);
                }
                finally
                {
                    SalvarOuAlterar(entity, recursoId, (short)EnumAcao.Transmitir);
                }
            }

            var empenhosErros = empenhos.Where(x => x.StatusProdesp == "E").ToList();

            if (empenhosErros.Count > 0)
                if (empenhos.Count == 1)
                    result += empenhos.FirstOrDefault()?.MensagemServicoProdesp;
                else
                    result += Environment.NewLine + "; / Algumas Preparação de Pagamento não puderam ser retransmitidos";


            return result;
        }

        private void Transmissao(Usuario user, PreparacaoPagamento entity, int recursoId)
        {
            try
            {
                if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity, recursoId);
            }
            catch
            {
                throw;
            }
        }

        private Error Retransmissao(Usuario user, PreparacaoPagamento entity, int recursoId)
        {
            var error = new Error();
            var cicsmo = new ChaveCicsmo();
            try
            {
                try
                { if (entity.TransmitirProdesp && !entity.TransmitidoProdesp) TransmitirProdesp(entity,  recursoId); }
                catch (Exception ex)
                { error.Prodesp = ex.Message; }
            }
            catch (Exception ex)
            {
                throw ;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
            return error;
        }

        private void TransmitirProdesp(PreparacaoPagamento entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                _prodespContaUnica.Inserir_PreparacaoPagamento(cicsmo.Chave, cicsmo.Senha, ref entity);
                
                entity.TransmitidoProdesp = true;
                entity.StatusProdesp = "S";
                entity.DataTransmitidoProdesp = DateTime.Now;
                entity.MensagemServicoProdesp = null;
            }
            catch (Exception ex)
            {
                entity.StatusProdesp = "E";
                entity.MensagemServicoProdesp = ex.Message;
                throw ;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
        }

        public object ImprimirProdesp(PreparacaoPagamento entity)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {

                cicsmo = _chave.ObterChave();
                                
                var result = _prodespContaDer.ImpressaoRelacaoOD(entity, cicsmo.Chave, cicsmo.Senha);

                return result;

            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        internal class Error
        {
            public string Prodesp { get; set; }
        }
    }
}
