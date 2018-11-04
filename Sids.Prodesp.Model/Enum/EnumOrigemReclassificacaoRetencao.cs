using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Enum
{   public enum OrigemReclassificacaoRetencao
    {
        [Description("Reclassificação/Retenção")]
        ReclassificacaoRetencao = 1,

        [Description("Confirmação de Pagamento")]
        ConfirmacaoDePagamento = 2
    }
}
