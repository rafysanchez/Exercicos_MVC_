namespace Sids.Prodesp.Infrastructure.Security
{
    using Model.Entity.Seguranca;
    using Model.Interface.Security;
    using Model.Interface.Seguranca;
    using System.Linq;

    public class SSO : IAutenticacao
    {
        ICrudUsuario usuario;


        public SSO() { }
        public SSO(ICrudUsuario u)
        {
            usuario = u;
        }



        public bool Authenticate(Usuario entity)
        {
            return usuario.Fetch(new Usuario { ChaveDeAcesso = entity.ChaveDeAcesso}).FirstOrDefault() != null;
        }
    }
}
