namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Base.LiquidacaoDespesa;

    public interface ICrudRapInscricaoNota : ICrudBase<LiquidacaoDespesaNota>
    {
        int Save(LiquidacaoDespesaNota entity);
    }
}
