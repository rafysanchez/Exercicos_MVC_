using login =Sids.Prodesp.Model.ValueObject.Service.Login;
using Sids.Prodesp.Model.ValueObject.Service.Reserva;
using Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem;
using SIAFDOC = Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem.SIAFDOC;

namespace Sids.Prodesp.Interface.Interface.Service
{
    public interface ISiafem
    {
        string Login(string login,string senha, string unidadeGestora, login.SIAFDOC siafdoc);
        string AlterarSenha(string login, string senha,string unidadeGestora, login.SIAFDOC siafdoc);
        string InserirReservaSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirReservaSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
        string InserirCancelamentoReserva(string login, string senha, string unidadeGestora, SIAFDOC siafdoc); 
        string InserirReforcoSiafem(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        string InserirReforcoSiafisico(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
        string ConsultaOC(string login, string senha, string unidadeGestora, SFCODOC siafdoc);
        string ConsultaNr(string login, string senha, string unidadeGestora, SIAFDOC siafdoc);
        
    }
}
