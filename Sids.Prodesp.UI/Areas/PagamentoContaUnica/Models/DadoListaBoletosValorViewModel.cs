using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Extension;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoListaBoletosValorViewModel
    {
        [Display(Name = "Total de Credores")]
        public string TotalCredores { get; set; }

        [Display(Name = "Total da Lista")]
        public string ValorTotalLista { get; set; }

        public DadoListaBoletosValorViewModel CreateInstance(ListaBoletos entity)
        {
            return new DadoListaBoletosValorViewModel
            {
                ValorTotalLista = $"R$ {string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", (Convert.ToDecimal(entity.ValorTotalLista)).ToString("N2"))}",
                TotalCredores = entity.TotalCredores.ZeroParaNulo()
            };
        }

    }
}