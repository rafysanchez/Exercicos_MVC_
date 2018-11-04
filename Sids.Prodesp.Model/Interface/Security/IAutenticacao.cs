namespace Sids.Prodesp.Model.Interface.Security
{
    using Sids.Prodesp.Model.Entity.Seguranca;

    public interface IAutenticacao 
    {
        bool Authenticate(Usuario entity);
    }
}
