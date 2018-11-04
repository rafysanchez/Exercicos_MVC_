using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoPagamentoContaUnicaAgrupamentoViewModel
    {
        [Display(Name = "Órgão (Regional)")]
        public string Orgao { get; set; }
        public IEnumerable<SelectListItem> RegionalListItems { get; set; }
        
        [Display(Name = "Tipo de Despesa")]
        public string TipoDespesa { get; set; }

        [Display(Name = "Data de Vencimento")]
        public string DataVencimento { get; set; }

        [Display(Name = "Tipo Documento")]
        public string DocumentoTipoId { get; set; }

        public IEnumerable<SelectListItem> DocumentoTipoListItems { get; set; }

        [Display(Name = "N° Documento")]
        public string NumeroDocumento { get; set; }

        public bool TransmitidoSiafem { get; set; }

        public DadoPagamentoContaUnicaAgrupamentoViewModel CreateInstance(ProgramacaoDesembolso entity, IEnumerable<DocumentoTipo> tipoDocumento,IEnumerable<Regional> regionais)
        {
            return new DadoPagamentoContaUnicaAgrupamentoViewModel
            {

                DocumentoTipoListItems= tipoDocumento.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.DocumentoTipoId
                }),
                NumeroDocumento = entity.NumeroDocumento,
                TipoDespesa = entity.CodigoDespesa,

                RegionalListItems = regionais.Where(x => x.Id > 1).
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.RegionalId
                }),

                TransmitidoSiafem = entity.Agrupamentos.Count(x => x.Id > 0) > 0
            };


        }

    }
}