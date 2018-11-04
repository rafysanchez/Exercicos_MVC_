namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;

    public interface ICrudSubempenhoNota : ICrudBase<LiquidacaoDespesaNota>
    {
        /// <summary>
        /// Caso não exista insere uma única Nota Fiscal para Subempenho no repositório, caso contrário atualiza
        /// </summary>
        /// <param name="entity">A Nota Fiscal para inserir ou alterar</param>
        /// <returns>A chave de sistema (Id) da Nota Fiscal</returns>
        int Save(LiquidacaoDespesaNota entity);
    }
}
