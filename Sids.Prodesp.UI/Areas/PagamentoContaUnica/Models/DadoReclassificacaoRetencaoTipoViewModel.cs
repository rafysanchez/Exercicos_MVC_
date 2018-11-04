using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoReclassificacaoRetencaoTipoViewModel
    {
        [Display(Name = "Normal ou Estorno?")]
        public string NormalEstorno { get; set; }

        [Display(Name = "Tipo de Reclassificação / Retenção")]
        public string ReclassificacaoRetencaoTipoId { get; set; }

        public IEnumerable<SelectListItem> NormalEstornoListItems { get; set; }

        public IEnumerable<SelectListItem> ReclassificacaoRetencaoTipoListItems { get; set; }

        public DadoReclassificacaoRetencaoTipoViewModel CreateInstance(ReclassificacaoRetencao entity, IEnumerable<ReclassificacaoRetencaoTipo> reclassificacaoRetencaoTipos)
        {
            return new DadoReclassificacaoRetencaoTipoViewModel
            {
                NormalEstornoListItems = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Normal", Value = "1", Selected = entity.NormalEstorno == "1"},
                    new SelectListItem {Text = "Estorno", Value = "2", Selected = entity.NormalEstorno == "2"}
                },
                NormalEstorno = entity.NormalEstorno,
                ReclassificacaoRetencaoTipoListItems = reclassificacaoRetencaoTipos.Select(x => new SelectListItem
                {
                    Text = x.Descricao,
                    Value = x.Id.ToString(),
                    Selected = x.Id == entity.ReclassificacaoRetencaoTipoId
                }),
                ReclassificacaoRetencaoTipoId = entity.ReclassificacaoRetencaoTipoId.ToString(),
            };
        }
    }
}