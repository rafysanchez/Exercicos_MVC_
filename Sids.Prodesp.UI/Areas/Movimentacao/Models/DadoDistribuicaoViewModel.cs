using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Movimentacao;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    using Model.Interface.Movimentacao;
    using System.ComponentModel.DataAnnotations;

    public class DadoDistribuicaoViewModel
    {
        public string Id { get; set; }

        public int IdMovimentacao { get; set; }

        public int NrAgrupamento { get; set; }

        public int NrSequencia { get; set; }

        [Display(Name = "N° Distr.")]
        public string NrNotaDistribuicao { get; set; }

        [Display(Name = "UG Emitente")]
        public string UnidadeGestoraEmitente { get; set; }

        [Display(Name = "UG Favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Órgão")]
        public string NrOrgao { get; set; }

        [Display(Name = "Fonte")]
        public string Fonte { get; set; }

        [Display(Name = "Categoria de Gasto")]
        public string CategoriaGasto { get; set; }

        [Display(Name = "Total")]
        public decimal ValorTotal { get; set; }        
    }
}