using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoItemBase
    {
        [Column("fg_transmitido_prodesp")]
        public string StatusProdesp { get; set; }

        [Column("ds_msgRetornoProdesp")]
        public string MensagemProdesp { get; set; }

        [Column("fg_transmitido_siafem")]
        public string StatusSiafem { get; set; }

        [Column("ds_msgRetornoSiafem")]
        public string MensagemSiafem { get; set; }

        [Column("dt_transmitido_siafem")]
        public DateTime DataSiafem { get; set; }

        [Column("nr_siafem")]
        public string NumeroSiafem { get; set; }
    }
}
