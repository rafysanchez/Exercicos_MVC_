

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;
    public class PesqSubempInscritoRapViewModel
    {
        [Display(Name = "Nº do subempenho de Inscrição de Rap")]
        public string NumeroSubempenho { get; set; }

        [Display(Name = "Nº do Recibo")]
        public string NumeroRecibo { get; set; }

        public PesqSubempInscritoRapViewModel CreateInstance(IRap entity)
        {
            var viewModel = new PesqSubempInscritoRapViewModel();
            viewModel.NumeroSubempenho = entity.NumeroSubempenho;
            viewModel.NumeroRecibo = entity.NumeroRecibo;

            return viewModel;

        }

    }
}