using Sids.Prodesp.Model.Entity.Reserva;

namespace Sids.Prodesp.Model.Interface.Reserva
{
    public interface IChaveCicsmo
    {
        ChaveCicsmo Retrieve(int userId);
        int Release(int id);
    }
}
