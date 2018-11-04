using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Application;
using model = Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.UI.Areas.Seguranca.Models.Usuario
{
    public class GridUsuarioViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Regional { get; set; }
        public string Area { get; set; }
        public string Perfil { get; set; }
        public string Sistema { get; set; }
        public bool? Status { get; set; }

        public List<GridUsuarioViewModel> GerarGrid(List<model.Usuario> usuarios)
        {
            var areas = App.AreaService.Buscar(new model.Area()).ToList();
            var regionais = App.RegionalService.Buscar(new model.Regional()).ToList();
            var sistemas = App.SistemaService.Buscar(new model.Sistema()).ToList();
            var perfis = App.PerfilUsuarioService.Buscar(new model.PerfilUsuario { Status = true});

            var gridUsuarioViewModel = usuarios.Select(x => new GridUsuarioViewModel
            {
                Codigo = x.Codigo,
                Nome = x.Nome,
                CPF = x.CPF,
                Email = x.Email,
                Regional = x.RegionalId == null? "": regionais.FirstOrDefault(a => a.Id == x.RegionalId).Descricao,
                Area = x.AreaId == null ? "" : areas.FirstOrDefault(a => a.Id == x.AreaId).Descricao,
                Sistema = x.SistemaId == null ? "" : sistemas.FirstOrDefault(a => a.Id == x.SistemaId).Descricao,
                Perfil = GerarPerfis(perfis.Where(p => p.Usuario == x.Codigo).ToList()),
                Status = x.Status
            }).ToList();

            return gridUsuarioViewModel;
        }
        private string GerarPerfis(List<model.PerfilUsuario> perfis)
        {
            string perfil = "";

            foreach (var objPerfil in perfis)

                perfil = perfil == "" ? objPerfil.DescPerfil : perfil + " / " + objPerfil.DescPerfil;

            return perfil;
        }
    }
}