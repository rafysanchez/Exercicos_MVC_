
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoPagamentoContaUnicaContasViewModel
    {

        #region Conta do Credor  (properties)

        [Display(Name = "Unidade Gestora")]
        public string NumeroCnpjcpfCredor { get; set; }

        [Display(Name = "Gestão")]
        public string GestaoCredor { get; set; }

        [Display(Name = "Banco")]
        public string NumeroBancoCredor { get; set; }

        [Display(Name = "Agência")]
        public string NumeroAgenciaCredor { get; set; }

        [Display(Name = "Conta")]
        public string NumeroContaCredor { get; set; }
        #endregion


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

        [Display(Name = "Valor Total(Prodesp)")]
        public decimal ValorTotal { get; set; }
        

        public DadoPagamentoContaUnicaContasViewModel CreateInstance(PreparacaoPagamento entity)
        {
            return new DadoPagamentoContaUnicaContasViewModel()
            {
                NumeroBancoCredor = entity.NumeroBancoCredor,
                NumeroAgenciaCredor = entity.NumeroAgenciaCredor,
                NumeroContaCredor = entity.NumeroContaCredor,

                NumeroBancoPagto = entity.NumeroBancoPagto,
                NumeroAgenciaPagto = entity.NumeroAgenciaPagto,
                NumeroContaPagto = entity.NumeroContaPagto,

                QuantidadeOpPreparada = entity.QuantidadeOpPreparada,
                ValorTotal = entity.ValorTotal

            };
        }


        public DadoPagamentoContaUnicaContasViewModel CreateInstance(ProgramacaoDesembolso entity)
        {
            return new DadoPagamentoContaUnicaContasViewModel()
            {
                NumeroBancoCredor = entity.NumeroBancoCredor,
                NumeroAgenciaCredor = entity.NumeroAgenciaCredor,
                NumeroContaCredor = entity.NumeroContaCredor,

                NumeroBancoPagto = entity.NumeroBancoPagto,
                NumeroAgenciaPagto = entity.NumeroAgenciaPagto,
                NumeroContaPagto = entity.NumeroContaPagto,
                
                NumeroCnpjcpfCredor = entity.NumeroCnpjcpfCredor,
                GestaoPagto = entity.GestaoPagto,
                GestaoCredor = entity.GestaoCredor,
                NumeroCnpjcpfPagto = entity.NumeroCnpjcpfPagto

            };
        }
    }
}