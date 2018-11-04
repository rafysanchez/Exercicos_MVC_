using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoReclassificacaoRetencaoViewModel
    {

        [Display(Name = "Nº do Empenho SIAFEM/SIAFISICO")]
        public string NumeroOriginalSiafemSiafisico { get; set; }

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

        [Display(Name = "CNPJ/CPF/UG Credor (SIAFEM)")]
        public string NumeroCNPJCPFCredor { get; set;}
        
        [Display(Name = "Gestão Credor (SIAFEM)")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Mês / Medição")]
        public string MesMedicao { get; set; }

        [Display(Name = "Ano / Medição")]
        public string AnoMedicao { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Evento")]
        public string CodigoEvento { get; set; }

        [Display(Name = "Inscrição do Evento")]
        public string CodigoInscricao { get; set; }

        [Display(Name = "Classificação")]
        public string CodigoClassificacao { get; set; }

        [Display(Name = "Fonte")]
        public string CodigoFonte { get; set; }

        [Display(Name = "Para Restos a Pagar")]
        public string RestoPagarId { get; set; }

        public IEnumerable<SelectListItem> ParaRestoAPagarListItems { get; set; }

        [Display(Name = "NL Ref. Liq. da Medição")]
        public string NotaLancamenoMedicao { get; set; }

        [Display(Name = "CNPJ da Prefeitura")]
        public string NumeroCnpjPrefeitura { get; set; }

        public DadoReclassificacaoRetencaoViewModel CreateInstance(ReclassificacaoRetencao entity, IEnumerable<ParaRestoAPagar> restoAPagarList)
        {
            return new DadoReclassificacaoRetencaoViewModel
            {
                NumeroOriginalSiafemSiafisico = entity.NumeroOriginalSiafemSiafisico,
                NumeroProcesso = entity.NumeroProcesso,
                CodigoAplicacaoObra = entity.CodigoAplicacaoObra,
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                DataEmissao = entity.DataEmissao.ToShortDateString(),
                NumeroCNPJCPFCredor = entity.NumeroCNPJCPFCredor,
                CodigoGestaoCredor = entity.CodigoGestaoCredor,
                MesMedicao = entity.MesMedicao,
                AnoMedicao = entity.AnoMedicao,
                Valor = entity.Valor,
                CodigoEvento = entity.CodigoEvento,
                CodigoInscricao = entity.CodigoInscricao,
                CodigoClassificacao = entity.CodigoClassificacao,
                CodigoFonte = entity.CodigoFonte,
                RestoPagarId = entity.RestoPagarId,
                NotaLancamenoMedicao = entity.NotaLancamenoMedicao,
                NumeroCnpjPrefeitura = entity.NumeroCnpjPrefeitura,
                ParaRestoAPagarListItems = restoAPagarList.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Id +  " - "  + x.Descricao,
                    Selected = x.Id == entity.RestoPagarId
                })
            };
        }
    }
}