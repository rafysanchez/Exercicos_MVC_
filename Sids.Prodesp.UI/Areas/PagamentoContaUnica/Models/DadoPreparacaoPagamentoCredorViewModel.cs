
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoPreparacaoPagamentoCredorViewModel
    {

        [Display(Name = "Credor - Organização")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ / CPF")]
        public string NumeroCNPJCPFCredor { get; set; }

        //obs: campos tipos de Despesa ocorrem em locais distintos na tela- motivo p/ haver dois campos
        [Display(Name = "Tipo de Despesa")]
        public string TipoDespesaCredor { get; set; }

        [Display(Name = "Nº do Contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Credor")]
        public string Credor1 { get; set; }
               
        public string Credor2 { get; set; }

        [Display(Name = "Endereço")]
        public string DescricaoLogradouroEntrega { get; set; }

        [Display(Name = "CEP")]
        public string NumeroCEPEntrega { get; set; }




        public DadoPreparacaoPagamentoCredorViewModel CreateInstance(PreparacaoPagamento entity)
        {
            return new DadoPreparacaoPagamentoCredorViewModel()
            {
                 CodigoCredorOrganizacao = entity.CodigoCredorOrganizacaoId,
                NumeroCNPJCPFCredor = entity.NumeroCnpjcpfCredor,
                TipoDespesaCredor = entity.CodigoDespesaCredor,
                NumeroContrato = entity.NumeroContrato,
                Credor1 = entity.Credor1,
                Credor2 = entity.Credor2,
                DescricaoLogradouroEntrega = entity.Endereco,
                NumeroCEPEntrega = entity.Cep
            };
        }
    }
}