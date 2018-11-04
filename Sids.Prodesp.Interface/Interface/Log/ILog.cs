using Sids.Prodesp.Model.Entity.Log;
using System;
using System.Collections.Generic;

namespace Sids.Prodesp.Interface.Interface.Log
{
    public interface ILogError
    {
        void SaveError(LogAplicacao log);
        void SaveError(Exception exception, string path);
        IEnumerable<LogAplicacao> Search(LogFilter filtro);
        IEnumerable<LogResultado> GetLogResultado(string obj = null);
        IEnumerable<LogNavegador> GetLogNavegador(string obj = null);
    }
}
