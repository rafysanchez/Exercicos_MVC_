namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using System;

    public interface IRap : ILiquidacaoDespesa
    {
        int ProgramaId { get; set; }
        int NaturezaId { get; set; }
        int CodigoCredorOrganizacao { get; set; }
        string CodigoNotaFiscalProdesp { get; set; }
        int NumeroAnoExercicio { get; set; }
        string DescricaoUsoAutorizadoPor { get; set; }
        string CodigoNaturezaItem { get; set; }
        int ValorCaucionado { get; set; }
        new string CodigoDespesa { get; set; }
        new DateTime DataRealizado { get; set; }
        string NumeroMedicao { get; set; }
        string DescricaoPrazoPagamento { get; set; }
        new string CodigoTarefa { get; set; }
        string Tarefa { get; set; }
        string NumeroCNPJCPFFornecedor { get; set; }
        new string NumeroRecibo { get; set; }
        string NumeroGuia { get; set; }
        string QuotaGeralAutorizadaPor { get; set; }
        string DadosCaucao { get; set; }
        int ValorSubempenhar { get; set; }
        string CodigoGestaoFornecedora { get; set; }
        string ValorAnulado { get; set; }
        string ValorSaldoAposAnulacao { get; set; }
        string ValorSaldoAnteriorSubempenho { get; set; }
        string Classificacao { get; set; }
        int TipoServicoId { get; set; }

        string NumeroSubempenho { get; set; }

        string TipoRap { get; }
        int CEDId { get; }

        string NumeroEmpenho { get; set; }
    }
}
