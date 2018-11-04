using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.ValueObject
{
    public class ConsultaDespesaNlVo
    {
        [Column("id_nl_tipo")]
        public int IdTipoNl { get; set; }

        [Column("ds_nl_tipo")]
        public string TipoNlDescricao { get; set; }
    }
}
