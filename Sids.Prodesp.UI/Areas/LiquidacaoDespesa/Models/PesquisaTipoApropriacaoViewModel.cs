namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class PesquisaTipoApropriacaoViewModel
    {
        [Display(Name = "Tipo de Apropriação / Subempenho")]
        public string CenarioSiafemSiafisico { get; set; }
        public IEnumerable<SelectListItem> CenarioListItens { get; set; }

        public string CenarioProdesp { get; set; }

        [Display(Name = "Nº do Empenho")]
        public string NumeroOriginalProdesp { get; set; }

        [Display(Name = "Nº de Subempenho")]
        public string NumeroSubempenhoProdesp { get; set; }

        [Display(Name = "Código da Tarefa")]
        public string CodigoTarefa { get; set; }

        [Display(Name = "Código da Despesa")]
        public string CodigoDespesa { get; set; }

        [Display(Name = "Valor Realizado")]
        public string ValorRealizado { get; set; }

        [Display(Name = "Valor a Anular")]
        public string ValorAnular { get; set; }

        [Display(Name = "Nùmero do Recibo")]
        public string NumeroRecibo { get; set; }

        [Display(Name = "Prazo de Pagamento")]
        public string PrazoPagamento { get; set; }

        [Display(Name = "Data da Realização")]
        public string DataRealizado { get; set; }
        

        public PesquisaTipoApropriacaoViewModel CreateInstance(ILiquidacaoDespesa entity, IEnumerable<CenarioTipo> cenarios)
        {

            var viewModel = new PesquisaTipoApropriacaoViewModel();

            viewModel.CenarioProdesp = entity.CenarioProdesp;

            viewModel.CenarioSiafemSiafisico = entity.CenarioSiafemSiafisico > 0 ? entity.CenarioSiafemSiafisico.ToString() : default(string);
            viewModel.CenarioListItens = cenarios
               .Select(s => new SelectListItem
               {
                   Text = s.Descricao,
                   Value = s.Id.ToString(),
                   Selected = s.Id == entity.CenarioSiafemSiafisico
               });

            viewModel.NumeroOriginalProdesp = entity.NumeroOriginalProdesp;
            viewModel.NumeroSubempenhoProdesp = entity.NumeroSubempenhoProdesp;
            viewModel.ValorAnular = entity.Id != 0 ? entity.ValorAnular.ToString() : default(string);
            viewModel.CodigoTarefa = entity.CodigoTarefa;
            viewModel.CodigoDespesa = entity.CodigoDespesa;
            viewModel.ValorRealizado = Convert.ToString(entity.ValorRealizado);
            viewModel.NumeroRecibo = entity.NumeroRecibo;
            viewModel.PrazoPagamento = entity.PrazoPagamento;
            viewModel.DataRealizado = entity.DataRealizado == default(DateTime) ? null : entity.DataRealizado.ToShortDateString();

            return viewModel;
        }
        
    }
}