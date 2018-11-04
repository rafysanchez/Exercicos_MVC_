namespace Sids.Prodesp.Model.Interface.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;

    public interface ICrudUsuario : ICrudBase<Usuario>
    {
        int UpdateADUser(Usuario entity);
        int UpdateLastAccess(Usuario entity);
        int UpdatePasswordExpirationDate(Usuario entity);
    }
}
