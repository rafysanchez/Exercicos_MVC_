namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    public interface ILiquidacaoDespesaItem
    {
        int Id { get; set; }
        int SubempenhoId { get; set; }
        int SequenciaItem { get; set; }
        string CodigoItemServico { get; set; }
        string CodigoUnidadeFornecimentoItem { get; set; }
        decimal QuantidadeMaterialServico { get; set; }
        string StatusSiafisicoItem { get; set; }
        bool? Transmitir { get; set; }
        decimal QuantidadeLiquidar { get; set; }
        decimal Valor { get; set; }
    }
}