namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;

    public interface ICrudSubempenhoCancelamentoItem : ICrudBase<LiquidacaoDespesaItem>
    {
        /// <summary>
        /// Caso não exista insere um único Item para Subempenho no repositório, caso contrário o atualiza
        /// </summary>
        /// <param name="entity">O Item para inserir ou alterar</param>
        /// <returns>A chave de sistema (Id) do Item</returns>
        int Save(LiquidacaoDespesaItem entity);
    }
}
