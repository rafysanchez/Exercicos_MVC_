using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class PreparacaoPagamentoTipoService
    {
        private readonly ICrudPreparacaoPagamentoTipo _repository;

        public PreparacaoPagamentoTipoService(ICrudPreparacaoPagamentoTipo repository)
        {
            this._repository = repository;
        }

        public IEnumerable<PreparacaoPagamentoTipo> Listar(PreparacaoPagamentoTipo preparacaoPagamentoTipo)
        {
            return _repository.Fatch(preparacaoPagamentoTipo);
        }
    }
}
