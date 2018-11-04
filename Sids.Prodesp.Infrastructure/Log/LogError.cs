namespace Sids.Prodesp.Infrastructure.Log
{
    using Helpers;
    using Model.Entity.Log;
    using Model.Interface.Log;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class LogErrorDal : ILogError
    {
        public void Save(LogAplicacao aplication)
        {
            DataHelper.Get<int>("PR_LOG_INCLUIR",
                new SqlParameter("@dt_log", aplication.Data),
                new SqlParameter("@id_usuario", aplication.UsuarioId),
                new SqlParameter("@id_tipo_log", aplication.AcaoId),
                new SqlParameter("@id_resultado", aplication.ResultadoId),
                new SqlParameter("@id_recurso", aplication.RecursoId),
                new SqlParameter("@ds_ip", aplication.Ip),
                new SqlParameter("@ds_url", aplication.Url),
                new SqlParameter("@id_navegador", aplication.NavegadorId),
                new SqlParameter("@ds_argumento", aplication.Argumento),
                new SqlParameter("@ds_versao", aplication.Versao),
                new SqlParameter("@ds_log", aplication.Descricao),
                new SqlParameter("@ds_navegador", aplication.Navegador),
                new SqlParameter("@ds_terminal", aplication.Terminal),
                new SqlParameter("@ds_xml", aplication.Xml)
            );
        }

        public IEnumerable<LogAplicacao> Fetch(LogFilter filter)
        {
            return DataHelper.List<LogAplicacao>("PR_LOG_CONSULTAR",
                new SqlParameter("@id_log", filter.Codigo),
                new SqlParameter("@id_recurso", filter.IdRecurso),
                new SqlParameter("@id_usuario", filter.IdUsuario),
                new SqlParameter("@id_acao", filter.IdAcao),
                new SqlParameter("@id_resultado", filter.IdResultado),
                new SqlParameter("@dt_inicial", filter.DataInicial),
                new SqlParameter("@dt_final", filter.DataFinal),
                new SqlParameter("@ds_argumento", filter.Argumento)
            );
        }

        public IEnumerable<LogResultado> GetLogResultado(string result = null)
        {
            return DataHelper.List<LogResultado>("PR_RESULTADO_CONSULTAR",
                new SqlParameter("@ds_resultado", result));
        }

        public IEnumerable<LogNavegador> GetLogNavegador(string browser = null)
        {
            return DataHelper.List<LogNavegador>("PR_NAVEGADOR_CONSULTAR",
                new SqlParameter("@ds_navegador", browser));
        }
    }
}
