namespace Sids.Prodesp.Core.Services.Empenho
{
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using Model.Interface.Log;
    using System.Collections.Generic;

    public class EmpenhoCancelamentoTipoService
    {
        private readonly ICrudEmpenhoCancelamentoTipo _crud;

        public EmpenhoCancelamentoTipoService(ILogError l, ICrudEmpenhoCancelamentoTipo crud)
        {
            _crud = crud;
        }

        public IEnumerable<EmpenhoCancelamentoTipo> Buscar(EmpenhoCancelamentoTipo objModel)
        {
            return _crud.Fetch(objModel);
        }
    }
}
