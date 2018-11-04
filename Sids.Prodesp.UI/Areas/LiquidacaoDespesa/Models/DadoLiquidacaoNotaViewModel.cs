namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class DadoLiquidacaoNotaViewModel
    {
        [Display(Name = "01")]
        public string Nota01 { get; set; }

        [Display(Name = "02")]
        public string Nota02 { get; set; }

        [Display(Name = "03")]
        public string Nota03 { get; set; }

        [Display(Name = "04")]
        public string Nota04 { get; set; }

        [Display(Name = "05")]
        public string Nota05 { get; set; }

        [Display(Name = "06")]
        public string Nota06 { get; set; }

        [Display(Name = "07")]
        public string Nota07 { get; set; }

        [Display(Name = "08")]
        public string Nota08 { get; set; }

        [Display(Name = "09")]
        public string Nota09 { get; set; }

        [Display(Name = "10")]
        public string Nota10 { get; set; }

        [Display(Name = "11")]
        public string Nota11 { get; set; }

        [Display(Name = "12")]
        public string Nota12 { get; set; }

        [Display(Name = "13")]
        public string Nota13 { get; set; }

        [Display(Name = "14")]
        public string Nota14 { get; set; }

        [Display(Name = "15")]
        public string Nota15 { get; set; }


        public DadoLiquidacaoNotaViewModel CreateInstance(ILiquidacaoDespesa entity)
        {
            var dictionary = entity.Notas != null
                ? entity.Notas.Select(s => new { s.Ordem, s.CodigoNotaFiscal }).ToDictionary(d => d.Ordem, d => d.CodigoNotaFiscal)
                : new Dictionary<int, string>();

            return new DadoLiquidacaoNotaViewModel()
            {
                Nota01 = Convert.ToString(dictionary.ContainsKey(1) ? dictionary[1] : default(string)),
                Nota02 = Convert.ToString(dictionary.ContainsKey(2) ? dictionary[2] : default(string)),
                Nota03 = Convert.ToString(dictionary.ContainsKey(3) ? dictionary[3] : default(string)),
                Nota04 = Convert.ToString(dictionary.ContainsKey(4) ? dictionary[4] : default(string)),
                Nota05 = Convert.ToString(dictionary.ContainsKey(5) ? dictionary[5] : default(string)),
                Nota06 = Convert.ToString(dictionary.ContainsKey(6) ? dictionary[6] : default(string)),
                Nota07 = Convert.ToString(dictionary.ContainsKey(7) ? dictionary[7] : default(string)),
                Nota08 = Convert.ToString(dictionary.ContainsKey(8) ? dictionary[8] : default(string)),
                Nota09 = Convert.ToString(dictionary.ContainsKey(9) ? dictionary[9] : default(string)),
                Nota10 = Convert.ToString(dictionary.ContainsKey(10) ? dictionary[10] : default(string)),
                Nota11 = Convert.ToString(dictionary.ContainsKey(11) ? dictionary[11] : default(string)),
                Nota12 = Convert.ToString(dictionary.ContainsKey(12) ? dictionary[12] : default(string)),
                Nota13 = Convert.ToString(dictionary.ContainsKey(13) ? dictionary[13] : default(string)),
                Nota14 = Convert.ToString(dictionary.ContainsKey(14) ? dictionary[14] : default(string)),
                Nota15 = Convert.ToString(dictionary.ContainsKey(15) ? dictionary[15] : default(string))
            };
        }
    }
}