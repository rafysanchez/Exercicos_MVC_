using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Enum
{
    public enum EnumTipoReclassificacaoRetencao
    {
        [Display(Name = "Nota de Lançamento (Retenção / Reclassificação) - SIAFNL001")]
        NotaLancamento = 2,

        [Display(Name = "Pagamento de Obras Sem OB")]
        PagamentoObrasSemOB = 3
    }
}
