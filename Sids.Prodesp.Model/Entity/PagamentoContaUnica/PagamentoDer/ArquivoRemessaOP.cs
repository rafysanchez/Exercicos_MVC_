using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class ArquivoRemessaOP 
    {

        public int indice { get; set; }

        public string OP { get; set; }

        [Display(Name = "Conta de Crédito")]
        public string ContaCredito { get; set; }

        public string Valor { get; set; }

        [Display(Name = "Ocorrência")]
        public string Ocorrencia { get; set; }



    }
}
