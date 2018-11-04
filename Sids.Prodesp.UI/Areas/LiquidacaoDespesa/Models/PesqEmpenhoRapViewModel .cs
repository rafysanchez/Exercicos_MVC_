

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;
    public class PesqEmpenhoRapViewModel
    {
        [Display(Name = "Nº do Empenho (Prodesp)")]
        public string NumeroEmpenho { get; set; }


        public PesqEmpenhoRapViewModel CreateInstance(IRap entity)
        {
            var viewModel = new PesqEmpenhoRapViewModel();
            viewModel.NumeroEmpenho = entity.NumeroEmpenho;


            return viewModel;

        }

    }
}