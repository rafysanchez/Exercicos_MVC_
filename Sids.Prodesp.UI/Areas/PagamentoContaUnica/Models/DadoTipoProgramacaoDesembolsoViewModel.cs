

using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoTipoProgramacaoDesembolsoViewModel
    {

        [Display(Name = "Tipo de Programação de Desembolso")]
        public string ProgramacaoDesembolsoTipoId { get; set; }
        public IEnumerable<SelectListItem> ProgramacaoDesembolsoTipoListItens { get; set; }



        public DadoTipoProgramacaoDesembolsoViewModel CreateInstance(ProgramacaoDesembolso entity, IEnumerable<ProgramacaoDesembolsoTipo> programacaoDesembolsoTipos)
        {

            return new DadoTipoProgramacaoDesembolsoViewModel()
            {
                ProgramacaoDesembolsoTipoId = Convert.ToString(entity.ProgramacaoDesembolsoTipoId),

                ProgramacaoDesembolsoTipoListItens = programacaoDesembolsoTipos
                  .Select(s => new SelectListItem
                  {
                      Text = s.Descricao,
                      Value = s.Id.ToString(),
                      Selected = s.Id == entity.ProgramacaoDesembolsoTipoId
                  }),


            };
        }
    }
}