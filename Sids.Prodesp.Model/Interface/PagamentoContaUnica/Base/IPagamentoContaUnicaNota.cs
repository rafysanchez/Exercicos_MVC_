namespace Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base
{
    public interface IPagamentoContaUnicaNota
    {
        int Id { get; set; }
        int IdReclassificacaoRetencao { get; set; }
        string CodigoNotaFiscal { get; set; }
        int Ordem { get; set; }
    }
}
