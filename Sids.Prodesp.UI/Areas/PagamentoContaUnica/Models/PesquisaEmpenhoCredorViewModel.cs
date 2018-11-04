
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
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
        
        public PesquisaEmpenhoCredorViewModel CreateInstance(ReclassificacaoRetencao entity)
        {
            return new PesquisaEmpenhoCredorViewModel()
            {
                CodigoCredorOrganizacao = entity.CodigoCredorOrganizacaoId > 0? entity.CodigoCredorOrganizacaoId.ToString(): null,
                NumeroCNPJCPFFornecedor = entity.NumeroCNPJCPFFornecedorId ,
                NumeroAnoExercicio = entity.AnoExercicio != 0 ? entity.AnoExercicio.ToString(): null
            };
        }
    }
}