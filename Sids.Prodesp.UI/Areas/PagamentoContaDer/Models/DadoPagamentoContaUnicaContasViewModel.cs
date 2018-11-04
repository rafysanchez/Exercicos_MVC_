
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Models
{
    public class DadoPagamentoContaUnicaContasViewModel
    {

       
        #region Conta de pagamento (properties)

        [Display(Name = "Unidade Gestora - CPF/CNPJ Credor")]
        public string NumeroCnpjcpfPagto { get; set; }

        [Display(Name = "Gestão")]
        public string GestaoPagto { get; set; }

        [Display(Name = "Banco")]
        public string NumeroBancoPagto { get; set; }
        
        [Display(Name = "Agência")]
        public string NumeroAgenciaPagto { get; set; }

        [Display(Name = "Conta")]
        public string NumeroContaPagto { get; set; }
        #endregion



        [Display(Name = "Qtde de OP's Preparadas(Prodesp)")]
        public int QuantidadeOpPreparada { get; set; }

        //[Display(Name = "Valor Total")]
        //public decimal ValorTotal { get; set; }



        [Display(Name = "Quantidade OP's no Arquivo")]
        public int? QuantidadeOpArquivo { get; set; }

        [Display(Name = "Valor Total Pago")]
        public int? ValorTotal { get; set; }

        [Display(Name = "Quantidade de Depósitos")]
        public int? QuantidadeDeposito { get; set; }


        [Display(Name = "Quantidade de DOC/TED")]
        public int? QuantidadeDocTed { get; set; }

        public string StatusProdesp { get; set; }




        public DadoPagamentoContaUnicaContasViewModel CreateInstance(ArquivoRemessa entity)
        {
            return new DadoPagamentoContaUnicaContasViewModel()
            {

                NumeroBancoPagto = entity.Banco,
                NumeroAgenciaPagto = entity.Agencia,
                NumeroContaPagto = entity.NumeroConta,
                ValorTotal = entity.ValorTotal,
                QuantidadeOpArquivo = entity.QtOpArquivo,
                QuantidadeDeposito = entity.QtDeposito,
                QuantidadeDocTed = entity.QtDocTed,
                StatusProdesp = entity.StatusProdesp

            };
        }


    }
}