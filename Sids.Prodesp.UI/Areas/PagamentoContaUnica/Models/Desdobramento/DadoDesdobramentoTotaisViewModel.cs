
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.Desdobramento
{
    public class DadoDesdobramentoTotaisViewModel
    {
        [Display(Name = "Total ISSQN")]
        public string ValorIssqn { get; set; }


        [Display(Name = "Total IR")]
        public string ValorIr { get; set; }


        [Display(Name = "Total INSS")]
        public string ValorInss { get; set; }

        public DadoDesdobramentoTotaisViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento entity)
        {
           
            return new DadoDesdobramentoTotaisViewModel
            {
                ValorIssqn = entity.ValorIssqn ==0 ?default(string): entity.ValorIssqn.ToString(CultureInfo.InvariantCulture),
                ValorIr = entity.ValorIr == 0 ? default(string) : entity.ValorIr.ToString(CultureInfo.InvariantCulture),
                ValorInss = entity.ValorInss == 0 ? default(string) : entity.ValorInss.ToString(CultureInfo.InvariantCulture)

            };
        }
    }
}