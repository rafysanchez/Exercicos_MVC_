namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;
    using Model.Enum;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;

    public class RapRequisicaoNotaService : BaseService
    {
        private ICrudRapRequisicaoNota _repository;


        public RapRequisicaoNotaService(ILogError log, ICrudRapRequisicaoNota repository) : base(log)
        {
            _repository = repository;
        }


        public AcaoEfetuada Excluir(LiquidacaoDespesaNota entity, int resource, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: resource);
            }
        }

        public AcaoEfetuada Excluir(IEnumerable<LiquidacaoDespesaNota> entities, int resource, short action)
        {
            try
            {
                foreach (var item in entities)
                    Excluir(item, resource, action);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }

        public AcaoEfetuada Alterar(LiquidacaoDespesaNota entity, int resource, short action)
        {
            try
            {
                _repository.Edit(entity);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: resource);
            }
        }

        public int SalvarOuAlterar(LiquidacaoDespesaNota entity, int resource, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (resource > 0)
                    LogSucesso(action, resource, $"SubempenhoNota {entity.Id}, Subempenho {entity.SubempenhoId}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }

        public IEnumerable<LiquidacaoDespesaNota> Buscar(LiquidacaoDespesaNota entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
