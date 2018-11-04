using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoDesdobramentoViewModel
    {

        [Display(Name = "N° Documento")]
        public string NumeroDocumento { get; set; }

        [Display(Name = "Cod. Serviço")]
        public string CodigoServico { get; set; }

        //Valor distribuido e Valor documento original
        [Display(Name = "Valor Distribuição")]
        public decimal ValorDistribuido { get; set; }

        [Display(Name = "Descrição de Serviço")]
        public string DescricaoServico { get; set; }

        [Display(Name = "Credor")]
        public string DescricaoCredor { get; set; }


        [Display(Name = "Nome Reduzido do Credor")]
        public string NomeReduzidoCredor { get; set; }


        [Display(Name = "Tipo Despesa")]
        public string TipoDespesa { get; set; }


        [Display(Name = "Aceita credor de desd. diferente.")]
        public bool AceitaCredor { get; set; }


        [Display(Name = "Tipo Desdobramento")]
        public string NumeroDesdobramentoTipoId { get; set; }

        public IEnumerable<SelectListItem> DesdobramentoTipoListItems { get; set; }


        [Display(Name = "Tipo Documento")]
        public string DocumentoTipoId { get; set; }

        public IEnumerable<SelectListItem> DocumentoTipoListItems { get; set; }




        public DadoDesdobramentoViewModel CreateInstance(Model.Entity.PagamentoContaUnica.Desdobramento.Desdobramento entity, IEnumerable<DesdobramentoTipo> tipoDesdobramentos, IEnumerable<DocumentoTipo> tipoDocumento)
        {
            var documentoTipos = tipoDocumento as IList<DocumentoTipo> ?? tipoDocumento.ToList();

            return new DadoDesdobramentoViewModel
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
                CodigoServico = entity.CodigoServico,
                ValorDistribuido = entity.ValorDistribuido,
                DescricaoServico = entity.DescricaoServico,
                DescricaoCredor = entity.DescricaoCredor,
                NomeReduzidoCredor = entity.NomeReduzidoCredor,
                TipoDespesa = entity.TipoDespesa,
                AceitaCredor = entity.AceitaCredor

            };


        }
        public DadoDesdobramentoViewModel CreateInstance(IPagamentoContaUnicaSiafem entity, IEnumerable<DocumentoTipo> tipoDocumento)
        {
            var documentoTipos = tipoDocumento as IList<DocumentoTipo> ?? tipoDocumento.ToList();

            return new DadoDesdobramentoViewModel
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