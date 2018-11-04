namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Interface.Empenho;
    using System.ComponentModel.DataAnnotations;

    public class DadoDespesaViewModel
    {
        [Display(Name = "Autorizado no processo supra as folhas")]
        public string DescricaoAutorizadoSupraFolha { get; set; }

        [Display(Name = "Código de Especificação de Despesa")]
        public string CodigoEspecificacaoDespesa { get; set; }

        [Display(Name = "Especificação de Despesa (Prodesp)")]
        public string DescricaoEspecificacaoDespesa { get; set; }
       
        
        public DadoDespesaViewModel CreateInstance(IEmpenho objModel)
        {
            return new DadoDespesaViewModel()
            {
                DescricaoAutorizadoSupraFolha = objModel.DescricaoAutorizadoSupraFolha,
                CodigoEspecificacaoDespesa = objModel.CodigoEspecificacaoDespesa,
                DescricaoEspecificacaoDespesa = objModel.DescricaoEspecificacaoDespesa                
            };
        }
    }
}