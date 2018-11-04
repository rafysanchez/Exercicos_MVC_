using Sids.Prodesp.Model.ValueObject.Service.Login;

namespace Sids.Prodesp.Interface.Interface.Service.Seguranca
{
    public interface ISiafemSeguranca
    {
        string Login(string login,string senha, string unidadeGestora, SIAFDOC siafdoc);
        string AlterarSenha(string login, string senha,string unidadeGestora, SIAFDOC siafdoc);
        
    }
}
