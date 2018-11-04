using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento
{

    public class DadoDesdobramentoTipoViewModel
    {


        [Display(Name = "Tipo Desdobramento")]
        public string DesdobramentoTipoId { get; set; }
        public IEnumerable<SelectListItem> DesdobramentoTipoListItems { get; set; }



        public DadoDesdobramentoTipoViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento entity,IEnumerable<DesdobramentoTipo> desdobramentoTipos)
        {

            return new DadoDesdobramentoTipoViewModel
            {
                DesdobramentoTipoId = Convert.ToString(entity.DesdobramentoTipoId),

                DesdobramentoTipoListItems = desdobramentoTipos.ToList().Where(x => x.Id < 3).Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Descricao,
                    Selected = x.Id == entity.DesdobramentoTipoId
                })

            };
        }


    }
}