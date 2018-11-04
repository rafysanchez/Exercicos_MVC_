
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    using System.ComponentModel.DataAnnotations;

    public class DadoPagamentoContaUnicaEventoViewModel
    {
        public string Id { get; set; }
        public string PagamentoContaUnicaId { get; set; }


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
        
        public DadoPagamentoContaUnicaEventoViewModel CreateInstance(IPagamentoContaUnicaEvento objModel)
        {
            return new DadoPagamentoContaUnicaEventoViewModel
            {

                Id = objModel.Id > 0 ? objModel.Id.ToString() : default(string),
                PagamentoContaUnicaId = objModel.PagamentoContaUnicaId > 0 ? objModel.PagamentoContaUnicaId.ToString() : default(string),

                NumeroEvento = objModel.NumeroEvento,
                InscricaoEvento = objModel.InscricaoEvento,
                Classificacao = objModel.Classificacao,
                Fonte = objModel.Fonte,
                ValorUnitario = objModel.ValorUnitario > 0 ? objModel.ValorUnitario.ToString() : default(string)
            };
        }
        
    }
}