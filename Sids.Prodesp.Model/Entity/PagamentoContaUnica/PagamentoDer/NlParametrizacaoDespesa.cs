using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class NlParametrizacaoDespesa
    {
        [Column("id_despesa")]
        public int Id { get; set; }

        [Column("id_despesa_tipo")]
        public int IdTipo { get; set; }

        [Column("id_nl_parametrizacao")]
        public int IdNlParametrizacao { get; set; }
    }
}
