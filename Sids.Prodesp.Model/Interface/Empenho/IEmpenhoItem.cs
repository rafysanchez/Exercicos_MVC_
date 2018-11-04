namespace Sids.Prodesp.Model.Interface.Empenho
{
    public interface IEmpenhoItem
    {
        int Id { get; set; }
        int EmpenhoId { get; set; }
        string CodigoItemServico { get; set; }
        string CodigoUnidadeFornecimentoItem { get; set; }
        string DescricaoUnidadeMedida { get; set; }
        decimal QuantidadeMaterialServico { get; set; }
        string DescricaoJustificativaPreco { get; set; }
        string DescricaoItemSiafem { get; set; }
        decimal ValorUnitario { get; set; }
        decimal ValorTotal { get; set; }
        string StatusSiafisicoItem { get; set; }
        int SequenciaItem { get; set; }
    }
}
