using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoImpressaoRelacaoReRtCadastrarViewModel
    {
        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Banco")]
        public string CodigoBanco { get; set; }

        public DadoImpressaoRelacaoReRtCadastrarViewModel CreateInstance(ImpressaoRelacaoReRtConsultaPaiVo entity)
        {
            var retorno = new DadoImpressaoRelacaoReRtCadastrarViewModel();

            retorno.CodigoUnidadeGestora = entity.CodigoUnidadeGestora;
            retorno.CodigoGestao = entity.CodigoGestao;
            retorno.CodigoBanco = entity.CodigoBanco;

            return retorno;
        }
    }
}