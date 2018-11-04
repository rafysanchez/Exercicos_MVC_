using System.ComponentModel;

namespace Sids.Prodesp.Model.Enum
{
    public enum EnumTipoPagamento
    {
        [Description("ORDEM BANCARIA")]
        OrdemBancaria = 11,
        [Description("CHEQUE PGTO")]
        ChequePgto = 12,
        [Description("CHEQUE PRACA")]
        ChequePraca = 13,
        [Description("CHEQUE FORA")]
        ChequeFora = 14,
        [Description("DINHEIRO")]
        Dinheiro = 15,
        [Description("LIMITE DE SAQUE")]
        LimiteDeSaque = 16,
        [Description("SUPRIMENTO")]
        Suprimento = 17,
        [Description("DESPESA - COMPENS")]
        DespesaCompens = 18,
        [Description("RECEITA - COMPENS")]
        ReceitaCompens = 19,
        [Description("BOLETIM")]
        Boletim = 21,
        [Description("CAUCAO TITULOS")]
        CaucaoTitulos = 22,
        [Description("CREDORES")]
        Credores = 23
    }
}
