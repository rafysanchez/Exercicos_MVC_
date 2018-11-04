namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;

    public interface ICrudRapRequisicaoNota : ICrudBase<LiquidacaoDespesaNota>
    {
        int Save(LiquidacaoDespesaNota entity);
    }
}
