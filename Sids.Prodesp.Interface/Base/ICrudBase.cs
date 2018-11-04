using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Base
{
    public interface ICrudBase<TEntity>
    {
        string ObterLogNome();
        int Inserir(TEntity objModel);
        int Alterar(TEntity objModel);
        int Excluir(int id);
        IEnumerable<TEntity> Buscar(TEntity objModel);
    }
}
