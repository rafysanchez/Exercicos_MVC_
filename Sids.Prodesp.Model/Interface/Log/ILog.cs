namespace Sids.Prodesp.Model.Interface.Log
{
    using Model.Entity.Log;
    using System.Collections.Generic;

    public interface ILogError
    {
        void Save(LogAplicacao aplication);
        IEnumerable<LogAplicacao> Fetch(LogFilter filter);
        IEnumerable<LogResultado> GetLogResultado(string result = null);
        IEnumerable<LogNavegador> GetLogNavegador(string browser = null);
    }
}
