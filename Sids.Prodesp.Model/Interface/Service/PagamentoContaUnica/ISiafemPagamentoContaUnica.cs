using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;

namespace Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica
{
    public interface ISiafemPagamentoContaUnica
    {
        string InserirReclassificacaoRetencao(string login, string password, string unidadeGestora, SIAFDOC document);

        string InserirListaBoletos(string login, string password, string unidadeGestora, SIAFDOC document);

        string InserirProgramacaoDesembolso(string login, string password, string unidadeGestora, SIAFDOC document);

        string InserirProgramacaoDesembolsoSiafisico(string login, string password, string unidadeGestora, ValueObject.Service.Siafem.LiquidacaoDespesa.SFCODOC document);

        string CancelarProgramacaoDesembolso(string login, string password, string unidadeGestora, SIAFDOC document);

        string Consultar(string login, string password, string unidadeGestora, SIAFDOC document);

        //SIAFConsultaPD
        string ConsultaPD(string login, string password, string unidadeGestora, SIAFDOC document);

        //SIAFListaPd
        string ListaPd(string login, string password, string unidadeGestora, SIAFDOC document);

        //SIAFExepd2
        string ExecutaPD(string login, string password, string unidadeGestora, SIAFDOC document);

        //SIAFConsultaOB
        string ConsultaOB(string login, string password, string unidadeGestora, SIAFDOC document);

        string AutorizaOB(string login, string password, string unidadeGestora, SIAFDOC document);

        string CancelaOB(string login, string password, string unidadeGestora, SIAFDOC document);

        string CancelarImpressaoRelacaoReRt(string login, string password, string unidadeGestora, SIAFDOC document);

        string TransmitirImpressaoRelacaoReRt(string login, string password, string unidadeGestora, SIAFDOC document);
    }
}