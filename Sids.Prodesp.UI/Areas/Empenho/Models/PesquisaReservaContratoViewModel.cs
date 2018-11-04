using Sids.Prodesp.Model.Extension;

namespace Sids.Prodesp.UI.Areas.Empenho.Models
{  
    using System.ComponentModel.DataAnnotations;

    public class PesquisaReservaContratoViewModel
    {
        [Display(Name = "Nº Contrato Prodesp")]
        public string NumeroContrato { get; set; }


        public PesquisaReservaContratoViewModel CreateInstance(string numeroContrato)
        {
            return new PesquisaReservaContratoViewModel()
            {
                NumeroContrato = numeroContrato
            };
        }
    }
}