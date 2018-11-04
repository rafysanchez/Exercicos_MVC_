using Sids.Prodesp.Model.Base.Empenho;
using Sids.Prodesp.Model.Entity.Empenho;
using Sids.Prodesp.Model.Enum;
using System.Collections.Generic;

namespace Sids.Prodesp.Model.Interface
{
    public interface IEmpenhoItemService<T> where T : BaseEmpenhoItem
    {
        AcaoEfetuada Alterar(T objModel, int recursoId, int acaoId);
        IEnumerable<T> Buscar(T objModel);
        AcaoEfetuada Excluir(T objModel, int recursoId, int acaoId);
        AcaoEfetuada Salvar(int empenhoId, IEnumerable<T> objModel, int recursoId, short acaoId);
        AcaoEfetuada Salvar(T objModel, int recursoId, int acaoId);
    }
}