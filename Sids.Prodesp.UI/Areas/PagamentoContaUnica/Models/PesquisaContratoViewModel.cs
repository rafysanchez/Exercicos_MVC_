using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class PesquisaContratoViewModel
    {
        [Display(Name = "Nº do Contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Cód. Aplicação / Obra")]
        public string CodigoAplicacaoObra { get; set; }



        public PesquisaContratoViewModel CreateInstance(IPagamentoContaUnica entity)
        {

            return new PesquisaContratoViewModel
            {
                NumeroContrato = entity.NumeroContrato,

                CodigoAplicacaoObra = entity.CodigoAplicacaoObra

            };
        }
    }
}