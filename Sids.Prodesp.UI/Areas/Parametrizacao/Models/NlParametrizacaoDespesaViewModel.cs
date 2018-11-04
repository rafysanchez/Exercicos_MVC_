using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.UI.Areas.Parametrizacao.Models
{
    public class NlParametrizacaoDespesaViewModel
    {        
        public int Id { get; set; }
        public int IdTipo { get; set; }
        public int IdNlParametrizacao { get; set; }

        public NlParametrizacaoDespesaViewModel()
        {

        }

        public NlParametrizacaoDespesaViewModel(NlParametrizacaoDespesa entity)
        {
            this.Id = entity.Id;
            this.IdTipo = entity.IdTipo;
            this.IdNlParametrizacao = entity.IdNlParametrizacao;
        }

        public NlParametrizacaoDespesa ToEntity()
        {
            var entity = new NlParametrizacaoDespesa();
            entity.Id = this.Id;
            entity.IdTipo = this.IdTipo;
            entity.IdNlParametrizacao = this.IdNlParametrizacao;
            return entity;
        }
    }
}