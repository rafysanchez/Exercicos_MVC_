namespace Sids.Prodesp.Model.Interface.Configuracao
{
    using Base;
    using Model.Entity.Configuracao;
    using System.Collections.Generic;

    public interface ICrudPrograma: ICrudBase<Programa>
    {
        IEnumerable<int> FetchProgramYears();
        IEnumerable<int> CopyProgramStructureFromYear(int year);
    }
}
