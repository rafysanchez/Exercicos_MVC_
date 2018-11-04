using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoProgramacaoDesembolsoPDBECViewModel
    {

        [Display(Name = "Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObraBec { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Data da Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "Data da Vencimento")]
        public string DataVencimento { get; set; }

        [Display(Name = "Nº do Contrato")]
        public string NumeroContratoBec { get; set; }

        [Display(Name = "Nº da Lista ou Anexo")]
        public string NumeroListaAnexo { get; set; }

        [Display(Name = "Nº da NL")]
        public string NumeroNLReferenciaBec { get; set; }

        [Display(Name = "Nº da CT")]
        public string NumeroCT { get; set; }

        [Display(Name = "Nº da NE")]
        public string NumeroNE { get; set; }

        [Display(Name = "Finalidade")]
        public string Finalidade { get; set; }

        [Display(Name = "Nº Documento Gerador")]
        public string NumeroDocumentoGerador { get; set; }

        [Display(Name = "Observação")]
        public string Obs { get; set; }

        public DadoProgramacaoDesembolsoPDBECViewModel CreateInstance(ProgramacaoDesembolso entity, IEnumerable<Regional> regional, Usuario logado)
        {
            return new DadoProgramacaoDesembolsoPDBECViewModel() {
                NumeroProcesso = entity.NumeroProcesso,
                CodigoAplicacaoObraBec = entity.CodigoAplicacaoObra,
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                DataEmissao = entity.DataEmissao.ToShortDateString(),
                DataVencimento = entity.DataVencimento == default(DateTime) ? null : entity.DataVencimento.ToShortDateString(),
                NumeroContratoBec = entity.NumeroContrato,
                NumeroListaAnexo = entity.NumeroListaAnexo,
                NumeroNLReferenciaBec = entity.NumeroNLReferencia,
                Finalidade = entity.Finalidade,
                NumeroDocumentoGerador = entity.NumeroDocumentoGerador,
                Obs = entity.Obs,
                NumeroNE = entity.NumeroNE,
                NumeroCT = entity.NumeroCT
            };
        }
    }
}