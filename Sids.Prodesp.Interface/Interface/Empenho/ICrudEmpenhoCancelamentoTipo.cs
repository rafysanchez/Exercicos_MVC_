namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoCancelamentoTipo
    {
        IEnumerable<EmpenhoCancelamentoTipo> Buscar(EmpenhoCancelamentoTipo objModel);
    }
}
