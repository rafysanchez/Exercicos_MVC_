using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class NlParametrizacaoDespesaTipo
    {
        [Column("id_despesa_tipo")]
        public int Id
        { get; set; }

        [Column("cd_despesa_tipo")]
        public int? Codigo { get; set; }

        [Column("ds_despesa_tipo")]
        public string Descricao { get; set; }
    }
}
