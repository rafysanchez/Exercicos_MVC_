namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoCancelamentoTipo
    {
        IEnumerable<EmpenhoCancelamentoTipo> Fetch(EmpenhoCancelamentoTipo entity);
    }
}
