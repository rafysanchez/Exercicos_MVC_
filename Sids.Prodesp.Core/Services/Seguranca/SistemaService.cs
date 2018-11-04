namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SistemaService : BaseService
    {
        const string LogTable = "tb_area";
        //private Infrastructure.Log.LogErrorDal logErrorDal;
        ISistema _sistema;

        public SistemaService(ILogError l, ISistema sistema) : base(l)
        {
            _sistema = sistema;
        }
        public List<Sistema> Buscar(Sistema obj)
        {
            try
            {
                return _sistema.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
}
