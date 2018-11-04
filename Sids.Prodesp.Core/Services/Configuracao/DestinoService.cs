namespace Sids.Prodesp.Core.Services.Configuracao
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Interface.Configuracao;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;

    public class DestinoService : BaseService
    {
        ICrudDestino _destino;
        public DestinoService(ILogError l, ICrudDestino f) 
            : base(l)
        {
            _destino = f;
        }
       
        public IEnumerable<Destino> Buscar(Destino obj)
        {
            try
            {
                return _destino.Fetch(obj);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
}