using System;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;
using System.ComponentModel.DataAnnotations;

namespace Sids.Prodesp.Model.Entity.Log
{
    public class LogFilter
    {
        public int? Codigo { get; set; }
        public int? IdRecurso { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdAcao { get; set; }
        public int? IdResultado { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataInicial { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DataFinal { get; set; }
        public string Argumento { get; set; }

        public List<Funcionalidade> Recursos { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Acao> LogAcao { get; set; }
        public List<LogResultado> LogResultado { get; set; }

        public LogFilter()
        {
            Recursos = new List<Funcionalidade>();
            Usuarios = new List<Usuario>();
            LogAcao = new List<Acao>();
            LogResultado = new List<LogResultado>();
        }
    }
}