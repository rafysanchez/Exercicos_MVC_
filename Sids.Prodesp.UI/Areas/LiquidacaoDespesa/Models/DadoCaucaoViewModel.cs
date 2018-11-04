namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DadoCaucaoViewModel
    {

        [Display(Name = "Quota Geral - Uso Autorizado Por")]
        public string QuotaGeralAutorizadaPor { get; set; }

        [Display(Name = "Dados da Caução - Número da Guia")]
        public string NumeroGuia { get; set; }

        [Display(Name = "Valor Caucionado")]
        public string ValorCaucionado { get; set; }

        [Display(Name = "Valor Realizado")]
        public string ValorRealizado { get; set; }


        public DadoCaucaoViewModel CreateInstance(string quotaGeralAutorizadoPor, string numeroGuia, int valorCaucionado, int valorRealizado = 0)
        {
            var viewModel = new DadoCaucaoViewModel();

            viewModel.QuotaGeralAutorizadaPor = quotaGeralAutorizadoPor;
            viewModel.NumeroGuia = numeroGuia;
            viewModel.ValorCaucionado = valorCaucionado > 0 ? Convert.ToString(valorCaucionado) : default(string);
            viewModel.ValorRealizado = valorRealizado > 0 ? Convert.ToString(valorRealizado) : default(string);

            return viewModel;
        }
    }
}