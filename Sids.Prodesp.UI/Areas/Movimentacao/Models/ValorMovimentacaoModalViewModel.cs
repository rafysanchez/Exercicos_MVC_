namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    using Model.Entity.Movimentacao;
    using Model.Interface.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValorMovimentacaoModalViewModel
    {
        [Display(Name = "Janeiro")]
        public string Mes01Modal { get; set; }
        [Display(Name = "Fevereiro")]
        public string Mes02Modal { get; set; }
        [Display(Name = "Março")]
        public string Mes03Modal { get; set; }
        [Display(Name = "Abril")]
        public string Mes04Modal { get; set; }
        [Display(Name = "Maio")]
        public string Mes05Modal { get; set; }
        [Display(Name = "Junho")]
        public string Mes06Modal { get; set; }
        [Display(Name = "Julho")]
        public string Mes07Modal { get; set; }
        [Display(Name = "Agosto")]
        public string Mes08Modal { get; set; }
        [Display(Name = "Setembro")]
        public string Mes09Modal { get; set; }
        [Display(Name = "Outubro")]
        public string Mes10Modal { get; set; }
        [Display(Name = "Novembro")]
        public string Mes11Modal { get; set; }
        [Display(Name = "Dezembro")]
        public string Mes12Modal { get; set; }

        [Display(Name = "Q1")]
        public string TotalQ1Modal { get; set; }
        [Display(Name = "Q2")]
        public string TotalQ2Modal { get; set; }
        [Display(Name = "Q3")]
        public string TotalQ3Modal { get; set; }
        [Display(Name = "Q4")]
        public string TotalQ4Modal { get; set; }

        [Display(Name = "Total")]
        public string TotalModal { get; set; }


        public ValorMovimentacaoModalViewModel CreateInstance(int modelId, IEnumerable<MovimentacaoMes> meses)
        {
            var dictionary = meses
                .Where(w => w.IdCancelamento == modelId)
                .Select(s => new { s.Descricao, s.ValorMes })
                .ToDictionary(d => d.Descricao, d => d.ValorMes);

            return dictionary.Any() ?
                new ValorMovimentacaoModalViewModel()
                {
                    Mes01Modal = Convert.ToString(dictionary.ContainsKey("01") ? dictionary["01"] : default(decimal)),
                    Mes02Modal = Convert.ToString(dictionary.ContainsKey("02") ? dictionary["02"] : default(decimal)),
                    Mes03Modal = Convert.ToString(dictionary.ContainsKey("03") ? dictionary["03"] : default(decimal)),
                    Mes04Modal = Convert.ToString(dictionary.ContainsKey("04") ? dictionary["04"] : default(decimal)),
                    Mes05Modal = Convert.ToString(dictionary.ContainsKey("05") ? dictionary["05"] : default(decimal)),
                    Mes06Modal = Convert.ToString(dictionary.ContainsKey("06") ? dictionary["06"] : default(decimal)),
                    Mes07Modal = Convert.ToString(dictionary.ContainsKey("07") ? dictionary["07"] : default(decimal)),
                    Mes08Modal = Convert.ToString(dictionary.ContainsKey("08") ? dictionary["08"] : default(decimal)),
                    Mes09Modal = Convert.ToString(dictionary.ContainsKey("09") ? dictionary["09"] : default(decimal)),
                    Mes10Modal = Convert.ToString(dictionary.ContainsKey("10") ? dictionary["10"] : default(decimal)),
                    Mes11Modal = Convert.ToString(dictionary.ContainsKey("11") ? dictionary["11"] : default(decimal)),
                    Mes12Modal = Convert.ToString(dictionary.ContainsKey("12") ? dictionary["12"] : default(decimal)),
                    TotalModal = Convert.ToString(dictionary.Sum(s => s.Value))
                }
                : new ValorMovimentacaoModalViewModel();
        }
    }
}