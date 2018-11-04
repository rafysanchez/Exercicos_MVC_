
namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;

    public class DadoObservacaoViewModel
    {
        [Display(Name = "Observação")]
        public string DescricaoObservacao1 { get; set; }
        public string DescricaoObservacao2 { get; set; }
        public string DescricaoObservacao3 { get; set; }

        public DadoObservacaoViewModel CreateInstance(ILiquidacaoDespesa objModel)
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