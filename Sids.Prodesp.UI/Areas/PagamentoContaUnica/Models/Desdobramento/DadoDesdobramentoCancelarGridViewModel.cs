

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
    public class DadoDesdobramentoCancelarGridViewModel
    {

        [Display(Name = "Nome Reduzido")]
        public string ReduzidoCredor { get; set; }

        [Display(Name = "Desdobramento")]
        public string Desdobramento { get; set; }

        [Display(Name = "Valor Distribuição")]
        public string ValorDistribuicao { get; set; }

        [Display(Name = "tipo de Bloqueio")]
        public string TipoBloqueio { get; set; }

        public DadoDesdobramentoCancelarGridViewModel CreateInstance(IdentificacaoDesdobramento identity, Desdobramento entity, IEnumerable<Reter> reters, IEnumerable<DesdobramentoTipo> desdobramentoTipos)
        {

            var reterList = reters as IList<Reter> ?? reters.ToList();

            return new DadoDesdobramentoCancelarGridViewModel
            {
                Desdobramento = entity.DesdobramentoTipoId == 1 ? desdobramentoTipos.Where(x => x.Id == identity.DesdobramentoTipoId).Select(x => x.Id + " - " + x.Descricao).FirstOrDefault() : identity.Sequencia.ToString("D2"),
                ReduzidoCredor = identity.NomeReduzidoCredor,
                ValorDistribuicao = identity.ValorDesdobrado == 0 ? default(string) : identity.ValorDesdobrado.ToString(),
                TipoBloqueio = identity.TipoBloqueio
            };
        }

    }
}