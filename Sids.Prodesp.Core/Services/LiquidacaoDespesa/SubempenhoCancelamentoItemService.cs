namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;
    using Model.Enum;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;

    public class SubempenhoCancelamentoItemService : BaseService
    {
        private ICrudSubempenhoCancelamentoItem _repository;


        public SubempenhoCancelamentoItemService(ILogError log, ICrudSubempenhoCancelamentoItem repository) : base(log)
        {
            _repository = repository;
        }


        public AcaoEfetuada Excluir(LiquidacaoDespesaItem entity, int resource, short action)
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

        public AcaoEfetuada Excluir(IEnumerable<LiquidacaoDespesaItem> entities, int resource, short action)
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

        public AcaoEfetuada Alterar(LiquidacaoDespesaItem entity, int resource, short action)
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

        public int SalvarOuAlterar(LiquidacaoDespesaItem entity, int resource, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (resource > 0)
                    LogSucesso(action, resource, $"SubempenhoItem {entity.Id}, Subempenho {entity.SubempenhoId}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }

        public IEnumerable<LiquidacaoDespesaItem> Buscar(LiquidacaoDespesaItem objModel)
        {
            return _repository.Fetch(objModel);
        }
    }
}
