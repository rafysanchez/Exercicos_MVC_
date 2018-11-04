using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Model.Entity.Log;

namespace Sids.Prodesp.UI.Models.Log
{
    public class FiltroLogViewModel
    {
        public string Id { get; set; }
        public string Funcionalidade { get; set; }
        public string Acao { get; set; }
        public string Resultado { get; set; }
        public string Usuario { get; set; }
        public string Data { get; set; }
        public string Versao { get; set; }
        public string Argumento { get; set; }

        public List<FiltroLogViewModel> GerarLogViewModels(LogFilter filtro, List<LogAplicacao> logAplicacao)
        {
            List<FiltroLogViewModel> logViewModels = logAplicacao.Select(x => new FiltroLogViewModel
            {
                Id = x.Id.ToString(),
                Funcionalidade = x.RecursoId == null ? "" : filtro.Recursos.Where(r => r.Codigo == x.RecursoId).First().Nome,
                Acao = x.AcaoId == null ? "" : filtro.LogAcao.Where(r => r.Id == x.AcaoId).First().Descricao,
                Resultado = x.ResultadoId == null ? "" : filtro.LogResultado.Where(r => r.Id == x.ResultadoId).First().Descricao,
                Usuario = x.UsuarioId == 0 ? "" : filtro.Usuarios.Where(r => r.Codigo == x.UsuarioId).First().Nome,
                Data = x.Data.ToString(),
                Versao = x.Versao,
                Argumento = x.Argumento == null ? "" : x.Argumento
            }).ToList();


            return logViewModels;
        }
    }
}