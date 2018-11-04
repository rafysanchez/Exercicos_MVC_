using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Interface.Security.Interface
{
    public interface IAutenticacao 
    {
        bool Autenticar(Usuario objModel);
    }
}
