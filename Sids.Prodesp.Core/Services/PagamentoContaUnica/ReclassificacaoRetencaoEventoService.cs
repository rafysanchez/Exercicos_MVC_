using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    using Base;
    using Model.Enum;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;

    public class ReclassificacaoRetencaoEventoService : BaseService
    {
        private readonly ICrudPagamentoContaUnicaEvento<ReclassificacaoRetencaoEvento> _repository;


        public ReclassificacaoRetencaoEventoService(ILogError log, ICrudPagamentoContaUnicaEvento<ReclassificacaoRetencaoEvento> repository) : base(log)
        {
            _repository = repository;
        }


        public AcaoEfetuada Excluir(ReclassificacaoRetencaoEvento entity, int resource, short action)
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
        
        public AcaoEfetuada Excluir(IEnumerable<ReclassificacaoRetencaoEvento> entities, int resource, short action)
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
        

        public int SalvarOuAlterar(ReclassificacaoRetencaoEvento entity, int resource, short action)
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

        public IEnumerable<ReclassificacaoRetencaoEvento> Buscar(ReclassificacaoRetencaoEvento entity)
        {
            return _repository.Fetch(entity);
        }
    }
}
