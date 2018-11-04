namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    public interface ILiquidacaoDespesaEvento
    {
        int Id { get; set; }
        int SubempenhoId { get; set; }
        string NumeroEvento { get; set; }
        string Classificacao { get; set; }
        string Fonte { get; set; }
        string InscricaoEvento { get; set; }
        int ValorUnitario { get; set; }
    }
}