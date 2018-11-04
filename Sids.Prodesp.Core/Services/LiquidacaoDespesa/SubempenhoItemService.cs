namespace Sids.Prodesp.Core.Services.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;
    using Model.Enum;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Log;
    using Model.Exceptions;
    using System;
    using System.Collections.Generic;
    using Model.Extension;

    public class SubempenhoItemService : BaseService
    {
        private ICrudSubempenhoItem _repository;


        public SubempenhoItemService(ILogError log, ICrudSubempenhoItem repository) : base(log)
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

                return entity.Id;
            }
            catch (Exception ex)
            {
                    throw SaveLog(ex, action, resource);
            }
        }

        public IEnumerable<LiquidacaoDespesaItem> Buscar(LiquidacaoDespesaItem objModel)
        {
            var itens = _repository.Fetch(objModel);

            foreach (var obj in itens)
            {
                obj.QuantidadeMaterialServico = obj.QuantidadeMaterialServico.ParaDecimal();
            }
            
            return itens;
        }
    }
}
