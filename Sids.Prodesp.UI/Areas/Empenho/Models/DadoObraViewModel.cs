namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DadoObraViewModel
    {
        [Display(Name = "UG Obra")]
        public string CodigoUGObra { get; private set; }

        [Display(Name = "Tipo de Obra")]
        public string TipoObraId { get; private set; }

        [Display(Name = "Ano de Assinatura do Contrato")]
        public string NumeroAnoContrato { get; private set; }

        [Display(Name = "Mês de Assinatura do Contrato")]
        public string NumeroMesContrato { get; private set; }

        [Display(Name = "Nº da Obra")]
        public string NumeroObra { get; private set; }


        public DadoObraViewModel CreateInstance(int codigoUGOObra, int tipoObraId, int anoContrato, int mesContrato, string numeroObra)
        {
            return new DadoObraViewModel()
            {
                CodigoUGObra = codigoUGOObra > 0 ? codigoUGOObra.ToString() : default(string),
                TipoObraId = tipoObraId > 0 ? tipoObraId.ToString() : default(string),
                NumeroAnoContrato = anoContrato > 0 ? anoContrato.ToString() : default(string),
                NumeroMesContrato = mesContrato > 0 ? mesContrato.ToString() : default(string),
                NumeroObra = numeroObra
            };
        }
    }
}