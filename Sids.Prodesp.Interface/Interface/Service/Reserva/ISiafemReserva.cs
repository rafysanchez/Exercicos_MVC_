using Sids.Prodesp.Model.ValueObject.Service.Siafem.Reserva;

namespace Sids.Prodesp.Interface.Interface.Service.Reserva
{
    public interface ISiafemReserva
    {
        string InserirReservaSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirReservaSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
        string InserirCancelamentoReserva(string login, string senha, string unidadeGestora, SIAFDOC siafdoc); 
        string InserirReforcoSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirReforcoSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
        string ConsultaOC(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
        string ConsultaNr(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        
    }
}
