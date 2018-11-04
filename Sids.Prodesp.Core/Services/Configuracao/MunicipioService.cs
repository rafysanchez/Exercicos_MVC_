using System;
using System.Collections.Generic;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Interface.Configuracao;
using Sids.Prodesp.Model.Interface.Log;

namespace Sids.Prodesp.Core.Services.Configuracao
{
    public class MunicipioService : BaseService
    {
        private readonly ICrudMunicipio _municipio;
        public MunicipioService(ILogError l, ICrudMunicipio f) 
            : base(l)
        {
            _municipio = f;
        }

        public IEnumerable<Municipio> Buscar(Municipio obj)
        {
            try
            {
                return _municipio.Fetch(obj);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
}
