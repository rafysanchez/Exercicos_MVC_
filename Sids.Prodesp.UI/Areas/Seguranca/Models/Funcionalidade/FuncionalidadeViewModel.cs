using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.UI.Areas.Seguranca.Models.Funcionalidade
{
    public class FuncionalidadeViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public string Detalhes { get; set; }

        public string CaminhoFisico { get; set; }

        public string Acao { get; set; }

        public bool? Status { get; set; }
        public List<FuncionalidadeViewModel> GerarViewModels(List<Model.Entity.Seguranca.Funcionalidade> funcionalidades)
        {

            var funcionalidadeViewModels = funcionalidades.Select(x => new FuncionalidadeViewModel
            {
                Id = x.Codigo.ToString(),
                Nome = x.Nome.ToString(),
                Detalhes = x.Descricao,
                CaminhoFisico = x.URL,
                Acao = GerarAcoes(x.Acoes),
                Status = x.Status
            }).ToList();

            return funcionalidadeViewModels;
        }

        private string GerarAcoes(List<Acao> acoes )
        {
            string acao = "";

            foreach (var objAcao in acoes)
                
                acao = acao == ""? objAcao.Descricao : acao + " / " + objAcao.Descricao;

            return acao;
        }
    }
}