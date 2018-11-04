namespace Sids.Prodesp.Model.Interface.Configuracao
{
    using Base;
    using Entity.Configuracao;
    using System.Collections.Generic;

    public interface ICrudEstrutura: ICrudBase<Estrutura>
    {
        IEnumerable<Estrutura> FetchByProgram(Programa entity);
    }
}
