using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using System;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class PesquisaImpressaoRelacaoRERTViewModel
    {
        [Display(Name = "Nº RE")]
        public string NumeroRE { get; set; }

        [Display(Name = "Nº RT")]
        public string NumeroRT { get; set; }

        [Display(Name = "Nº OB")]
        public string NumeroOB { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastramento { get; set; }

        [Display(Name = "Status SIAFEM")]
        public string StatusSiafem { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Banco")]
        public string CodigoBanco { get; set; }

        [Display(Name = "Data de Solicitação")]
        public DateTime? DataSolicitacao { get; set; }

        [Display(Name = "Nº Agrupamento")]
        public int? NumeroAgrupamento { get; set; }

        [Display(Name = "Cancelado")]
        public bool? FlagCancelamentoRERT { get; set; }

        public PesquisaImpressaoRelacaoRERTViewModel CreateInstance(ImpressaoRelacaoRERT entity)
        {
            return new PesquisaImpressaoRelacaoRERTViewModel
            {
                NumeroRE = entity.CodigoRelacaoRERT?.Substring(4,2) == "RE" ? entity.CodigoRelacaoRERT : null,

                NumeroRT = entity.CodigoRelacaoRERT?.Substring(4, 2) == "RT" ? entity.CodigoRelacaoRERT : null,

                NumeroOB = entity.CodigoOB,

                StatusSiafem = entity.StatusSiafem,

                DataCadastramento = entity.DataCadastramento,

                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,

                CodigoGestao = entity.CodigoGestao,

                CodigoBanco = entity.CodigoBanco,

                DataSolicitacao = entity.DataSolicitacao,

                NumeroAgrupamento = entity.NumeroAgrupamento,

                FlagCancelamentoRERT = entity.FlagCancelamentoRERT
            };
        }
    }
}