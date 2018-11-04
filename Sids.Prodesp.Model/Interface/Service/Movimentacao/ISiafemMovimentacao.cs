namespace Sids.Prodesp.Model.Interface.Service.Movimentacao
{
    using Model.ValueObject.Service.Siafem.Movimentacao;

    public interface ISiafemMovimentacao
    {     
        string InserirInserirMovimentacaoOrcamentaria(string login, string password, string unidadeGestora, SIAFDOC document);

        //string InserirInserirMovimentacaoOrcamentaria(string login, string password, string unidadeGestora, SIAFDOC document);

        //string InserirDistribuicaoTesouroSiafem(string login, string password, string unidadeGestora, SIAFDOC document);

        //string InserirDistribuicaoNaoTesouroSiafem(string login, string password, string unidadeGestora, SIAFDOC document);

        //string InserirNotaCreditoSiafem(string login, string password, string unidadeGestora, SIAFDOC document);

        string Consultar(string login, string password, string unidadeGestora, SIAFDOC document);
    }
}
