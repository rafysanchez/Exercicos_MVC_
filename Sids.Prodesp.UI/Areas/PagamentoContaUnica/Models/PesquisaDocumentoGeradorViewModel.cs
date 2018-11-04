using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class PesquisaDocumentoGeradorViewModel
    {

        [Display(Name = "Tipo Documento")]
        public string DocumentoTipoId { get; set; }
        public IEnumerable<SelectListItem> DocumentoTipoListItems { get; set; }

        [Display(Name = "Nº do Documento")]
        public string NumeroDocumento { get; set; }


        [Display(Name = "Valor do Documento")]
        public decimal ValorDocumento { get; set; }



        public PesquisaDocumentoGeradorViewModel CreateInstance(PreparacaoPagamento entity, IEnumerable<DocumentoTipo> tipoDocumento)
        {
            var documentoTipos = tipoDocumento as IList<DocumentoTipo> ?? tipoDocumento.ToList();

            return new PesquisaDocumentoGeradorViewModel()
            {
                DocumentoTipoId = Convert.ToString(entity.DocumentoTipoId),

                DocumentoTipoListItems = documentoTipos.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.DocumentoTipoId
                }),

                NumeroDocumento = entity.NumeroDocumento,
                ValorDocumento = entity.ValorDocumento

            };
        }
        public PesquisaDocumentoGeradorViewModel CreateInstance(ProgramacaoDesembolso entity, IEnumerable<DocumentoTipo> tipoDocumento)
        {
            var documentoTipos = tipoDocumento as IList<DocumentoTipo> ?? tipoDocumento.ToList();

            return new PesquisaDocumentoGeradorViewModel()
            {
                DocumentoTipoId = Convert.ToString(entity.DocumentoTipoId),

                DocumentoTipoListItems = documentoTipos.
                Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.DocumentoTipoId
                }),

                NumeroDocumento = entity.NumeroDocumento
            };
        }
    }
}