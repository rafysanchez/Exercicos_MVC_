namespace Sids.Prodesp.Model.Interface.Empenho
{
    using Base;
    using Model.Entity.Empenho;
    using System.Collections.Generic;

    public interface ICrudEmpenhoCancelamento : ICrudBase<EmpenhoCancelamento>
    {
       IEnumerable<EmpenhoCancelamento> FetchForGrid(EmpenhoCancelamento entity);
        EmpenhoCancelamento BuscarAssinaturas(EmpenhoCancelamento objModel);
    }
}



