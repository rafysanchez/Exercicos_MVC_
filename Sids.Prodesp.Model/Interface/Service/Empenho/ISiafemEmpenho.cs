namespace Sids.Prodesp.Model.Interface.Service.Empenho
{
    using Model.ValueObject.Service.Siafem.Empenho;

    public interface ISiafemEmpenho
    {
        string InserirEmpenhoSiafem(string login, string password, string unidadeGestora, SIAFDOC document);
        string InserirEmpenhoSiafisico(string login, string password, string unidadeGestora, SFCODOC document);

        string InserirEmpenhoReforcoSiafem(string login, string password, string unidadeGestora, SIAFDOC document);
        string InserirEmpenhoReforcoSiafisico(string login, string password, string unidadeGestora, SFCODOC document);

        string InserirEmpenhoCancelamentoSiafem(string login, string password, string unidadeGestora, SIAFDOC document);
        string InserirEmpenhoCancelamentoSiafisico(string login, string password, string unidadeGestora, SFCODOC document);

        string Consultar(string login, string password, string unidadeGestora, SIAFDOC document);
        string Consultar(string login, string password, string unidadeGestora, SFCODOC document);
    }
}
