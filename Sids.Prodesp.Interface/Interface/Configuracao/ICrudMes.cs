using Sids.Prodesp.Model.Entity.Configuracao;
using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Interface.Configuracao
{
    public interface ICrudMes
    {
        Mes SelecionarMes(int id);
        IEnumerable<Mes> ObterMeses();
    }
}