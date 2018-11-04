namespace Sids.Prodesp.Model.Interface.Service.Reserva
{
    using Model.ValueObject.Service.Siafem.Reserva;

    public interface ISiafemReserva
    {
        string InserirReservaSiafem(string login, string senha, string unidadeGestora, SIAFDOC document);
        string InserirReservaSiafisico(string login, string senha, string unidadeGestora, SFCODOC document);
        string InserirCancelamentoReserva(string login, string senha, string unidadeGestora, SIAFDOC document); 
        string InserirReforcoSiafem(string login, string senha, string unidadeGestora, SIAFDOC document);
        string InserirReforcoSiafisico(string login, string senha, string unidadeGestora, SFCODOC document);
        string ConsultaOC(string login, string senha, string unidadeGestora, SFCODOC document);
        string ConsultaNr(string login, string senha, string unidadeGestora, SIAFDOC document);
        
    }
}
