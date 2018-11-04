using System;
using System.Linq;
using Sids.Prodesp.Model.Entity.Log;

namespace Sids.Prodesp.UI.Models.Log
{
    public class LogViewModel
    {
        public string Funcionalidade { get; set; }
        public string Acao { get; set; }
        public string Resultado { get; set; }
        public string Usuario { get; set; }
        public DateTime Data { get; set; }
        public string Versao { get; set; }
        public string Argumento { get; set; }
        public string IP { get; set; }
        public string Url { get; set; }
        public string Navegador { get; set; }
        public string Descricao { get; set; }
        public string Terminal { get; set; }
        public string Xml { get; set; }


        public LogViewModel GerarLogViewModels(LogFilter filtro, LogAplicacao logAplicacao)
        {
            var logViewModels = new LogViewModel
            {
                Funcionalidade = logAplicacao.RecursoId == null ? "" : filtro.Recursos.FirstOrDefault(r => r.Codigo == logAplicacao.RecursoId).Nome,
                Acao = logAplicacao.AcaoId == null ? "" : filtro.LogAcao.FirstOrDefault(r => r.Id == logAplicacao.AcaoId).Descricao,
                Resultado = logAplicacao.ResultadoId == null ? "" : filtro.LogResultado.FirstOrDefault(r => r.Id == logAplicacao.ResultadoId).Descricao,
                Usuario = logAplicacao.UsuarioId == 0 ? "" : filtro.Usuarios.FirstOrDefault(r => r.Codigo == logAplicacao.UsuarioId).Nome,
                Data = logAplicacao.Data,
                Versao = logAplicacao.Versao,
                Argumento = logAplicacao.Argumento,
                IP = logAplicacao.Ip,
                Navegador = logAplicacao.Navegador == null ? "" : logAplicacao.Navegador,//Id == null ? "" : App.BaseService.GetLogNavegador().FirstOrDefault(n => n.Id == logAplicacao.NavegadorId).Descricao,
                Url = logAplicacao.Url,
                Descricao = logAplicacao.Descricao,
                Terminal = logAplicacao.Terminal,
                Xml = logAplicacao.Xml
            };

            return logViewModels;
        }
    }
}