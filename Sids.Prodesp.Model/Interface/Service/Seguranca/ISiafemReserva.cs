namespace Sids.Prodesp.Model.Interface.Service.Seguranca
{
    using Model.ValueObject.Service.Login;

    public interface ISiafemSeguranca
    {
        string Login(string login,string password, string unidadeGestora, SIAFDOC document);
        string AlterarSenha(string login, string password,string unidadeGestora, SIAFDOC document);
    }
}
