using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Enum
{
    public enum EnumTipoDocumentoMovimentacao
    {
        CancelamentoReducao = 1,
        DistribuicaoSuplementacao = 2
    }
    public enum EnumTipoDocumentoMovimentacaoCompleto
    {
        [Description("Cancelamento")]
        Cancelamento = 1,
        [Description("Nota de Crédito")]
        NotaDeCredito = 2,
        [Description("Distribuição")]
        Distribuicao = 3,
        [Description("Redução")]
        Reducao = 4,
        [Description("Suplementação")]
        Suplementacao = 5
    }
}
