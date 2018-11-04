using System.Collections.Generic;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.Configuracao;

namespace Sids.Prodesp.UI.Areas.Configuracao.Models.Estrutura
{
    public class FiltroViewModel
    {
        public int Ano { get; set; }
        public string Ptres { get; set; }
        public string Cfp { get; set; }
        public int Programa { get; set; }
        public List<SelectListItem> Anos { get; set; }
        public List<Programa> Programas { get; set; }
        public List<SelectListItem> Fontes { get; set; }
    }
}