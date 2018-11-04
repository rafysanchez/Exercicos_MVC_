using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using System;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models.ListaDeBoletos
{
    public class DadoCodigoDeBarrasViewModel
    {
        [Display(Name="Código de Barras")]
        public string CodigoDeBarras { get; set; }

        public string NumeroConta1 { get; set; }

        public string NumeroConta2 { get; set; }

        public string NumeroConta3 { get; set; }

        public string NumeroConta4 { get; set; }

        public string NumeroConta5 { get; set; }

        public string NumeroConta6 { get; set; }

        public string NumeroConta7 { get; set; }

        public string NumeroDigito { get; set; }

        public decimal Valor { get; set; }

        public IEnumerable<SelectListItem> TipoBoletoListItems { get; set; }

        [Display(Name = "Tipo Boleto")]
        public string TipoBoleto { get; set; }

        public int TipoBoletoId { get; set; }

        public  DadoCodigoDeBarrasViewModel CreateInstance(IEnumerable<TipoBoleto> tiposBoleto)
        {
            return new DadoCodigoDeBarrasViewModel
            {
               
                TipoBoletoListItems = tiposBoleto.Select(x => new SelectListItem
                {
                    Text = x.Descricao,
                    Value = x.Id.ToString()
                })
            };
        }
        public DadoCodigoDeBarrasViewModel CreateInstance(CodigoBarraTaxa entity,decimal valor)
        {

            return new DadoCodigoDeBarrasViewModel
            {
                CodigoDeBarras = $"{entity.NumeroConta1}."+
                $"{entity.NumeroConta2}." +
                $"{entity.NumeroConta3}." +
                $"{entity.NumeroConta4}",
                Valor = valor
            };
        }

        public DadoCodigoDeBarrasViewModel CreateInstance(CodigoBarraBoleto entity, decimal valor)
        {

            return new DadoCodigoDeBarrasViewModel
            {
                CodigoDeBarras = $"{entity.NumeroConta1}." +
                $"{entity.NumeroConta2}." +
                $"{entity.NumeroConta3}." +
                $"{entity.NumeroConta4}." +
                $"{entity.NumeroConta5}." +
                $"{entity.NumeroConta6}." +
                $"{entity.NumeroDigito}." +
                $"{entity.NumeroConta7}",
                Valor = valor
            };
        }

    }
}