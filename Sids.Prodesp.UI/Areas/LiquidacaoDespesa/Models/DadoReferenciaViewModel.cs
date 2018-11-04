namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DadoReferenciaViewModel
    {
        [Display(Name = "Referência")]
        public string Referencia { get; set; }


        [Display(Name = "Data Vencimento")] //exibe apenas no retorno da transmissão 
        public string DataVencimento { get; set; }

        public bool ReferenciaDigitada { get; set; }

        public DadoReferenciaViewModel CreateInstance(string referencia, DateTime dataVencimento, bool referenciaDigitada)
        {
            return new DadoReferenciaViewModel()
            {
                Referencia = referencia,
                DataVencimento = dataVencimento.ToString(),
                ReferenciaDigitada = referenciaDigitada
            };
        }

        public DadoReferenciaViewModel CreateInstance(string referencia)
        {
            return new DadoReferenciaViewModel()
            {
                Referencia = referencia
            };
        }
    }
}