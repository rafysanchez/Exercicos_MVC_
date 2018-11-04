namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.Seguranca;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class PesquisaSaldoRapViewModel
    {
        [Display(Name = "Ano")]
        public string AnoExercicio { get; set; }
        public IEnumerable<SelectListItem> AnoExercicioListItens { get; set; }


        [Display(Name = "Órgão (Regional)")]
        public string Orgao { get; set; }
        public IEnumerable<SelectListItem> OrgaoListItens { get; set; }


        public PesquisaSaldoRapViewModel CreateInstance(IRap entity, IEnumerable<int> anos, IEnumerable<Regional> regionais)
        {
            var ano = entity.NumeroAnoExercicio > 0 ? entity.NumeroAnoExercicio : DateTime.Now.Year;
            var viewModel = new PesquisaSaldoRapViewModel();
            
            viewModel.AnoExercicio = Convert.ToString(ano);
            viewModel.AnoExercicioListItens = anos
               .Select(s => new SelectListItem
               {
                   Text = Convert.ToString(s),
                   Value = Convert.ToString(s),
                   Selected = s == entity.NumeroAnoExercicio
               });


            viewModel.Orgao = entity.RegionalId > 0 ? Convert.ToString(entity.RegionalId) : default(string);
            viewModel.OrgaoListItens = regionais.Where(x => x.Id > 1)
               .Select(s => new SelectListItem
               {
                   Text = s.Descricao,
                   Value = s.Id.ToString(),
                   Selected = s.Id == entity.RegionalId
               });

            return viewModel;
        }
    }
}