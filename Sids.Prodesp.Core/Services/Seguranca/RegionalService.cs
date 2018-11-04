namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Base;
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RegionalService : BaseService
    {
        private readonly IRegional _regional;

        public RegionalService(ILogError l, IRegional regional) : base(l)
        {
            _regional = regional;
        }
        public List<Regional> Buscar(Regional obj)
        {
            try
            {
                return _regional.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
}
