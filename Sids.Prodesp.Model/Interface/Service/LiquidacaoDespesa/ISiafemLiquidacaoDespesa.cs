namespace Sids.Prodesp.Model.Interface.Service.LiquidacaoDespesa
{
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;

    public interface ISiafemLiquidacaoDespesa
    {
        string InserirRapInscricaoSiafem(string login, string password, string unidadeGestora, SIAFDOC document);
        string InserirRapRequisicaoApoioSiafem(string login, string password, string unidadeGestora, SIAFDOC document);
        string InserirSubempenhoSiafem(string login, string password, string unidadeGestora, SIAFDOC document, bool tagId = false);
        string InserirSubempenhoSiafisico(string login, string password, string unidadeGestora, SFCODOC document, bool tagId = false);
        string Consultar(string login, string password, string unidadeGestora, SIAFDOC document);
    }
}
