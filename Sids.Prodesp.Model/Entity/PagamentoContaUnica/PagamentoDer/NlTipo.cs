using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class NlTipo
    {
        [Column("id_nl_tipo")]
        public int Id { get; set; }

        [Column("ds_nl_tipo")]
        public string Descricao { get; set; }
    }
}
