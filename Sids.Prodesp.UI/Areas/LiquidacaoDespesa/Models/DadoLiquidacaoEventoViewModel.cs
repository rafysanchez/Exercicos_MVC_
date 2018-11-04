using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.LiquidacaoDespesa;

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Interface.LiquidacaoDespesa;
    using System.ComponentModel.DataAnnotations;

    public class DadoLiquidacaoEventoViewModel
    {
        public string Id { get; set; }
        public string SubempenhoId { get; set; }


        [Display(Name = "Evento")]
        public string NumeroEvento { get; set; }

        [Display(Name = "Evento")]
        public string CodigoEvento { get; set; }

        [Display(Name = "Inscrição do Evento")]
        public string InscricaoEvento { get; set; }

        [Display(Name = "Classificação")]
        public string Classificacao { get; set; }

        [Display(Name = "Fonte")]
        public string Fonte { get; set; }

        [Display(Name = "Valor do Evento")]
        public string ValorUnitario { get; set; }


        public string ValorTotal { get; set; }
        
        public DadoLiquidacaoEventoViewModel CreateInstance(ILiquidacaoDespesaEvento objModel, IEnumerable<CodigoEvento> eventos)
        {
            return new DadoLiquidacaoEventoViewModel
            {

                Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                SubempenhoId = objModel.SubempenhoId > 0 ? objModel.SubempenhoId.ToString() : default(string),

                NumeroEvento = objModel.NumeroEvento,
                InscricaoEvento = objModel.InscricaoEvento,
                Classificacao = objModel.Classificacao,
                Fonte = objModel.Fonte,
                ValorUnitario = objModel.ValorUnitario > 0 ? objModel.ValorUnitario.ToString() : default(string)
            };
        }
    }
}