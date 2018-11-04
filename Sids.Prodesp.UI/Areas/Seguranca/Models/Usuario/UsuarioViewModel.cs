using System.Collections.Generic;
using model = Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.UI.Areas.Seguranca.Models.Usuario
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        public string ChaveDeAcesso { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public int? SistemaId { get; set; }
        public int? AreaId { get; set; }
        public int? RegionalId { get; set; }

        public List<model.Sistema> Sistema { get; set; }
        
        public List<model.Area> Area { get; set; }
        
        public List<model.Regional> Regional { get; set; }
        
    }
}