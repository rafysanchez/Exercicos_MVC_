using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Enum
{
    public enum EnumTipoNl
    {
        [Description("Repasse Financeiro")]
        RepasseFinanceiro = 1,
        [Description("Baixa de Pagamento de Fornecedor")]
        BaixaPagamentoFornecedor = 2,
        [Description("Repasse para Regionais")]
        RepasseRegionais = 3,
        [Description("Baixa de Jeton-Jari")]
        BaixaJetonJari = 4,
        [Description("Baixa de Devolução de Caução")]
        BaixaDevolucaoCaucao = 5,
        [Description("Baixa de Pagamento de Adiantamento")]
        BaixaPagamentoAdiantamento = 6,
        [Description("Baixa de Pagamento de Restituição de Multa")]
        BaixaPagamentoRestituicaoMulta = 7,
        [Description("Baixa de Pagamento de Licença-Prêmio")]
        BaixaPagamentoLicencaPremio = 8,
        [Description("Baixa de Pagamento de Bonificação por Resultados")]
        BaixaPagamentoBonificacaoResultados = 9,
        [Description("Baixa de Pagamento de Perito do Quadro/ Judicial")]
        BaixaPagamentoPeritoQuadroJudicial = 10,
        [Description("Baixa de Pagamento Outros")]
        BaixaPagamentoOutros = 11,
        [Description("Repasse de IR")]
        RepasseIR = 12,
        [Description("Baixa de ISSQN")]
        BaixaISSQN = 13
    }
}
