using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.ListaDeBoletos
{
    public class DadoListaBoletosViewModel
    {
        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Data de Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "CNPJ do Favorecido")]
        public string NumeroCnpjcpfFavorecido { get; set; }

        [Display(Name = "Nome da Lista")]
        public string NomeLista { get; set; }

        public DadoListaBoletosViewModel CreateInstance(ListaBoletos entity)
        {
            return new DadoListaBoletosViewModel
            {
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                DataEmissao = entity.DataEmissao.ToShortDateString(),
                NomeLista = entity.NomeLista,
                NumeroCnpjcpfFavorecido = entity.NumeroCnpjcpfFavorecido
            };

            
        }

    }
}