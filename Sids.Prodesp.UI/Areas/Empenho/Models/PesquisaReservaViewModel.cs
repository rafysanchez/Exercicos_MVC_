namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PesquisaReservaViewModel
    {
        [Display(Name = "Nº Reserva Prodesp")]
        public string CodigoReserva { get; set; }


        public PesquisaReservaViewModel CreateInstance(string codigoReserva)
        {
            return new PesquisaReservaViewModel()
            {
                CodigoReserva = codigoReserva
            };
        }
    }
}