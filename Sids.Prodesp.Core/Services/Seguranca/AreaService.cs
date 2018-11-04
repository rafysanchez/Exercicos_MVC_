namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AreaService : BaseService
    {
        const string LogTable = "tb_area";
        //private Infrastructure.Log.LogErrorDal logErrorDal;
        IArea _area;

        public AreaService(ILogError l, IArea area) : base(l)
        {
            _area = area;
        }

        public List<Area> Buscar(Area obj)
        {
            try
            {
                return _area.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
}
