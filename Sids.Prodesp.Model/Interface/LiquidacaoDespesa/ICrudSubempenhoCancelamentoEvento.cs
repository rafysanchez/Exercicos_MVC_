namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;

    public interface ICrudSubempenhoCancelamentoEvento : ICrudBase<LiquidacaoDespesaEvento>
    {
        /// <summary>
        /// Caso não exista insere um único Evento para Subempenho no repositório, caso contrário o atualiza
        /// </summary>
        /// <param name="entity">O Evento para inserir ou alterar</param>
        /// <returns>A chave de sistema (Id) do Evento</returns>
        int Save(LiquidacaoDespesaEvento entity);
    }
}
