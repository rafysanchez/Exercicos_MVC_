using Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho;

namespace Sids.Prodesp.Interface.Interface.Service.Empenho
{
    public interface ISiafemEmpenho
    {
        string InserirEmpenhoSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirEmpenhoSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);

        string InserirEmpenhoReforcoSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirEmpenhoReforcoSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);

        string InserirEmpenhoCancelamentoSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirEmpenhoCancelamentoSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);

        string Consultar(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string Consultar(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
    }
}
