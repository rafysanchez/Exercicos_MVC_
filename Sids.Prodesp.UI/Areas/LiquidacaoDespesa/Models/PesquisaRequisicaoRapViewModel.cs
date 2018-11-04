
namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;
    public class PesquisaRequisicaoRapViewModel
    {
        [Display(Name = "Nº Requisição de Rap Prodesp")]
        public string NumeroRequisicaoRap { get; set; }

        public string CodigoTarefa { get; set; }
        public string CodigoDespesa { get; set; }
        public string CodigoAplicacaoObra { get; set; }

        public PesquisaRequisicaoRapViewModel CreateInstance(RapAnulacao requisicaoRap)
        {
            return new PesquisaRequisicaoRapViewModel()
            {
                NumeroRequisicaoRap = requisicaoRap.NumeroRequisicaoRap,
                CodigoTarefa = requisicaoRap.CodigoTarefa,
                CodigoDespesa = requisicaoRap.CodigoDespesa,
                CodigoAplicacaoObra = requisicaoRap.CodigoAplicacaoObra
            };
        }
    }
}

