namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Interface.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValorEmpenhoViewModel
    {
        [Display(Name = "Janeiro")]
        public string Mes01 { get; set; }
        [Display(Name = "Fevereiro")]
        public string Mes02 { get; set; }
        [Display(Name = "Março")]
        public string Mes03 { get; set; }
        [Display(Name = "Abril")]
        public string Mes04 { get; set; }
        [Display(Name = "Maio")]
        public string Mes05 { get; set; }
        [Display(Name = "Junho")]
        public string Mes06 { get; set; }
        [Display(Name = "Julho")]
        public string Mes07 { get; set; }
        [Display(Name = "Agosto")]
        public string Mes08 { get; set; }
        [Display(Name = "Setembro")]
        public string Mes09 { get; set; }
        [Display(Name = "Outubro")]
        public string Mes10 { get; set; }
        [Display(Name = "Novembro")]
        public string Mes11 { get; set; }
        [Display(Name = "Dezembro")]
        public string Mes12 { get; set; }


        public string Total { get; set; }


        public ValorEmpenhoViewModel CreateInstance(int modelId, IEnumerable<IMes> meses)
        {
            var dictionary = meses
                .Where(w => w.Id == modelId)
                .Select(s => new { s.Descricao, s.ValorMes })
                .ToDictionary(d => d.Descricao, d => d.ValorMes);

            return dictionary.Any() ?
                new ValorEmpenhoViewModel()
                {
                    Mes01 = Convert.ToString(dictionary.ContainsKey("01") ? dictionary["01"] : default(decimal)),
                    Mes02 = Convert.ToString(dictionary.ContainsKey("02") ? dictionary["02"] : default(decimal)),
                    Mes03 = Convert.ToString(dictionary.ContainsKey("03") ? dictionary["03"] : default(decimal)),
                    Mes04 = Convert.ToString(dictionary.ContainsKey("04") ? dictionary["04"] : default(decimal)),
                    Mes05 = Convert.ToString(dictionary.ContainsKey("05") ? dictionary["05"] : default(decimal)),
                    Mes06 = Convert.ToString(dictionary.ContainsKey("06") ? dictionary["06"] : default(decimal)),
                    Mes07 = Convert.ToString(dictionary.ContainsKey("07") ? dictionary["07"] : default(decimal)),
                    Mes08 = Convert.ToString(dictionary.ContainsKey("08") ? dictionary["08"] : default(decimal)),
                    Mes09 = Convert.ToString(dictionary.ContainsKey("09") ? dictionary["09"] : default(decimal)),
                    Mes10 = Convert.ToString(dictionary.ContainsKey("10") ? dictionary["10"] : default(decimal)),
                    Mes11 = Convert.ToString(dictionary.ContainsKey("11") ? dictionary["11"] : default(decimal)),
                    Mes12 = Convert.ToString(dictionary.ContainsKey("12") ? dictionary["12"] : default(decimal)),
                    Total = Convert.ToString(dictionary.Sum(s => s.Value))
                }
                : new ValorEmpenhoViewModel();
        }
    }
}