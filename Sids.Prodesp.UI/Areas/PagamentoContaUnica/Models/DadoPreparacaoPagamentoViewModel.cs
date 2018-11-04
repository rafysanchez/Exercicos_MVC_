
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoPreparacaoPagamentoViewModel
    {
        [Display(Name = "Ano (Exercício)")]
        public string AnoExercicio { get; set; }

        [Display(Name = "Código da Conta")]
        public string CodigoConta { get; set; }

        [Display(Name = "Banco")]
        public string NumeroBanco { get; set; }

        [Display(Name = "Agência")]
        public string NumeroAgencia { get; set; }

        [Display(Name = "Conta")]
        public string NumeroConta { get; set; }

        [Display(Name = "Órgão (Regional)")]
        public string Regional { get; set; }
        public IEnumerable<SelectListItem> RegionalListItems { get; set; }

        //obs: campos tipos de Despesa ocorrem em locais distintos na tela- motivo p/ haver dois campos
        [Display(Name = "Tipo de Despesa")]
        public string TipoDespesa { get; set; }

        [Display(Name = "Data de Vencimento Até")]
        public string DataVencimento { get; set; }


        public DadoPreparacaoPagamentoViewModel CreateInstance(PreparacaoPagamento entity, IEnumerable<Regional> regional, Usuario logado)
        {
            var ano = entity.AnoExercicio > 0 ? entity.AnoExercicio : DateTime.Now.Year;
            var obj = new DadoPreparacaoPagamentoViewModel();
            obj.AnoExercicio = entity.AnoExercicio != 0 ? entity.AnoExercicio.ToString() : null;
            obj.Regional = entity.RegionalId > 0 ? entity.RegionalId.ToString() : default(string);
            obj.RegionalListItems = regional.Select(s => new SelectListItem
            {
                Text = s.Descricao,
                Value = s.Id.ToString(),
                Selected = s.Id == (logado.RegionalId == 1 ? 16 : logado.RegionalId)
            }).ToList();
            obj.CodigoConta = entity.CodigoConta;
            obj.NumeroBanco = entity.NumeroBanco;
            obj.NumeroAgencia = entity.NumeroAgencia;
            obj.NumeroConta = entity.NumeroConta;
            obj.DataVencimento = entity.DataVencimento == default(DateTime) ? null : entity.DataVencimento.ToString();
            obj.TipoDespesa = entity.CodigoDespesa;

            return obj;
        }
    }
}