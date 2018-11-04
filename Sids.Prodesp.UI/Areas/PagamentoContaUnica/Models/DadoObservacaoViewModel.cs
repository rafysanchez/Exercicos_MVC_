
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoObservacaoViewModel
    {
        [Display(Name = "Observação")]
        public string DescricaoObservacao1 { get; set; }
        public string DescricaoObservacao2 { get; set; }
        public string DescricaoObservacao3 { get; set; }

        public DadoObservacaoViewModel CreateInstance(ReclassificacaoRetencao objModel)
        {
            return new DadoObservacaoViewModel()
            {
                DescricaoObservacao1 = objModel.DescricaoObservacao1,
                DescricaoObservacao2 = objModel.DescricaoObservacao2,
                DescricaoObservacao3 = objModel.DescricaoObservacao3,
            };
        }
    }
}