

using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoTipoPreparacaoPagamentoViewModel
    {

        [Display(Name = "Tipo de Preparação de Pagamento")]
        public string PreparacaoPagamentoTipoId { get; set; }
        public IEnumerable<SelectListItem> PreparacaoPagamentoTipoListItens { get; set; }



        public DadoTipoPreparacaoPagamentoViewModel CreateInstance(PreparacaoPagamento entity, IEnumerable<PreparacaoPagamentoTipo> preparacaoPagamentoTipo)
        {
            var preparacaoPagtoTipos = preparacaoPagamentoTipo as IList<PreparacaoPagamentoTipo> ?? preparacaoPagamentoTipo.ToList();

            return new DadoTipoPreparacaoPagamentoViewModel()
            {
                PreparacaoPagamentoTipoId = Convert.ToString(entity.PreparacaoPagamentoTipoId),

                PreparacaoPagamentoTipoListItens = preparacaoPagtoTipos
                  .Select(s => new SelectListItem
                  {
                      Text = s.Descricao,
                      Value = s.Id.ToString(),
                      Selected = s.Id == entity.PreparacaoPagamentoTipoId
                  }),


            };
        }
    }
}