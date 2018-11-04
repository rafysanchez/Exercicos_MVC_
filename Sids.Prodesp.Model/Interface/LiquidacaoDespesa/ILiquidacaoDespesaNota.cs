namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    public interface ILiquidacaoDespesaNota
    {
        int Id { get; set; }
        int SubempenhoId { get; set; }
        string CodigoNotaFiscal { get; set; }
        int Ordem { get; set; }
    }
}