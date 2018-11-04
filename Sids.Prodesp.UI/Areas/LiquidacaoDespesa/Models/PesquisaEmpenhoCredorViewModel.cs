
namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;

    public class PesquisaEmpenhoCredorViewModel
    {
        [Display(Name = "Nome")]
        public string NomeCredor { get; set; }

        [Display(Name = "Credor Organização(Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ/CPF (Prodesp)")]
        public string NumeroCNPJCPFFornecedor { get; set; }
         
        [Display(Name = "Ano")]
        public string NumeroAnoExercicio { get; set; }
        
        public PesquisaEmpenhoCredorViewModel CreateInstance(Subempenho entity)
        {
            return new PesquisaEmpenhoCredorViewModel()
            {
                CodigoCredorOrganizacao = entity.CodigoCredorOrganizacao > 0? entity.CodigoCredorOrganizacao.ToString(): null,
                NumeroCNPJCPFFornecedor = entity.NumeroCNPJCPFFornecedor ,
                NumeroAnoExercicio = entity.NumeroAnoExercicio != 0 ? entity.NumeroAnoExercicio.ToString(): null,
                NomeCredor = entity.NomeCredor
            };
        }
    }
}