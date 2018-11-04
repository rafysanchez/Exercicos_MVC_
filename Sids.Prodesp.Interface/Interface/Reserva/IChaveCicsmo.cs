using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Interface.Interface.Seguranca
{
    public interface IChaveCicsmo
    {
        ChaveCicsmo ObterChave(int userId);
        int LiberarChave(int id);
    }
}
