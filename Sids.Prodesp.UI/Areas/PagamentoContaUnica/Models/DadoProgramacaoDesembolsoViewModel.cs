using System;
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoProgramacaoDesembolsoViewModel
    {

        [Display(Name = "Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Data da Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "Data da Vencimento")]
        public string DataVencimento { get; set; }

        [Display(Name = "Nº do Contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Nº da Lista ou Anexo")]
        public string NumeroListaAnexo { get; set; }

        [Display(Name = "Nº da NL de Referência")]
        public string NumeroNLReferencia { get; set; }

        [Display(Name = "Finalidade")]
        public string Finalidade { get; set; }

        [Display(Name = "Nº Documento Gerador")]
        public string NumeroDocumentoGerador { get; set; }

        public DadoProgramacaoDesembolsoViewModel CreateInstance(ProgramacaoDesembolso entity)
        {
            return new DadoProgramacaoDesembolsoViewModel()
            {
                NumeroProcesso = entity.NumeroProcesso,
                CodigoAplicacaoObra = entity.CodigoAplicacaoObra,
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                DataEmissao = entity.DataEmissao.ToShortDateString(),
                DataVencimento = entity.DataVencimento == default(DateTime) ? null: entity.DataVencimento.ToShortDateString(),
                NumeroContrato = entity.NumeroContrato,
                NumeroListaAnexo = entity.NumeroListaAnexo,
                NumeroNLReferencia = entity.NumeroNLReferencia,
                Finalidade = entity.Finalidade,
                NumeroDocumentoGerador = entity.NumeroDocumentoGerador
            };
        }
    }
}