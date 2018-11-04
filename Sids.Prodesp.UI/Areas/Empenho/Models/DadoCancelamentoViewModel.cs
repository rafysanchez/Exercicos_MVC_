namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Empenho;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class DadoCancelamentoViewModel
    {
        public string Id { get; private set; }

        [Display(Name = "Tipo de Cancelamento")]
        public string EmpenhoCancelamentoTipoId { get; set; }
        public IEnumerable<SelectListItem> EmpenhoCancelamentoTipoListItems { get; set; }



        public DadoCancelamentoViewModel CreateInstance(EmpenhoCancelamento objModel, IEnumerable<EmpenhoCancelamentoTipo> tipos)
        {
            return new DadoCancelamentoViewModel()
            {
                EmpenhoCancelamentoTipoId = objModel.EmpenhoCancelamentoTipoId.ToString(),
                EmpenhoCancelamentoTipoListItems = tipos.Where(x => x.Siafem)
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.EmpenhoCancelamentoTipoId
                })
            };
        }
    }
}