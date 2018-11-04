
namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using System.ComponentModel.DataAnnotations;
    public class DadoSaldoValorAnulacao
    {
        [Display(Name = "Saldo Anterior do Subempenho")]
        public string ValorSaldoAnteriorSubempenho { get; set; }

        [Display(Name = "Valor Anulado")]
        public string ValorAnulado { get; set; }

        [Display(Name = "Saldo Após Anulação")]
        public string ValorSaldoAposAnulacao { get; set; }


        public DadoSaldoValorAnulacao CreateInstance(string ValorSaldoAnteriorSubempenho,string ValorAnulado,string ValorSaldoAposAnulacao,bool isNewRecord = false)
        {
            var viewModel = new DadoSaldoValorAnulacao();
            if (!isNewRecord) { 
                viewModel.ValorSaldoAnteriorSubempenho = ValorSaldoAnteriorSubempenho;
                viewModel.ValorAnulado = ValorAnulado;
                viewModel.ValorSaldoAposAnulacao = ValorSaldoAposAnulacao;
            }
            return viewModel;
        }
    }
}