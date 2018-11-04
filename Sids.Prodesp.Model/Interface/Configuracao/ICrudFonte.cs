namespace Sids.Prodesp.Model.Interface.Configuracao
{
    using Base;
    using Model.Entity.Configuracao;
    using System.Collections.Generic;

    public interface ICrudFonte : ICrudBase<Fonte>
    {
        IEnumerable<Fonte> DuplicateCheck(Fonte entity);
    }
}
