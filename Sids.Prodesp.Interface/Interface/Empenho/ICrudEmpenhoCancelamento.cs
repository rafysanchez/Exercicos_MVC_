
namespace Sids.Prodesp.Interface.Interface.Empenho
{
    using Base;
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoCancelamento : ICrudBase<EmpenhoCancelamento>
    {
       IEnumerable<EmpenhoCancelamento> BuscarGrid(EmpenhoCancelamento objModel);
        EmpenhoCancelamento BuscarAssinaturas(EmpenhoCancelamento objModel);
    }
}



