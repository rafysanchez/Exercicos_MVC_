namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface IPagamentoContaUnicaEvento
    {
        int Id { get; set; }
        int PagamentoContaUnicaId { get; set; }
        string NumeroEvento { get; set; }
        string Classificacao { get; set; }
        string Fonte { get; set; }
        string InscricaoEvento { get; set; }
        int ValorUnitario { get; set; }
    }
}
