using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.LiquidacaoDespesa;
using Sids.Prodesp.Model.Interface.LiquidacaoDespesa;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Interface.Movimentacao;
using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Sids.Prodesp.Model.Extension;


namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{



    public class DadoMovimentacaoViewModel
    {


        [Display(Name = "Tipo de Movimentação")]
        public string IdTipoMovimentacao { get; set; }
        public IEnumerable<SelectListItem> MovimentacaoListItems { get; set; }



        [Display(Name = "CFP(Programa de Trabalho)")]
        public string ProgramaId { get; set; }
        public IEnumerable<SelectListItem> ProgramaListItems { get; set; }

        [Display(Name = "CED(Natureza - Fonte)")]
        public string NaturezaId { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }

        [Display(Name = "Exercício")]
        public int AnoExercicio { get; set; }

        [Display(Name = "Processo")]
        public string NrProcesso { get; set; }


        [Display(Name = "Orig. Rec")]
        public string OrigemRecurso { get; set; }

        [Display(Name = "Dest. Rec")]
        public string DestinoRecurso { get; set; }

        [Display(Name = "Fl. Proc.")]
        public string FlProc { get; set; }



        [Display(Name = "Tipo de Documento")]
        public string IdTipoDocumento { get; set; }
        public IEnumerable<SelectListItem> DocumentoTipoListItems { get; set; }


        [Display(Name = "UG Emitente")]
        public string UnidadeGestoraEmitente { get; set; }


        [Display(Name = "Gestão Emitente")]
        public string IdGestaoEmitente { get; set; }

        [Display(Name = "UG Favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }


        [Display(Name = "Gestão Favorecida")]
        public string IdGestaoFavorecida { get; set; }

        [Display(Name = "Órgão")]
        public string NrOrgao { get; set; }

        [Display(Name = "Cod.Aplicação/Obra")]
        public string NrObra { get; set; }

        [Display(Name = "Evento Canc./Distr.")]
        public string CancDist { get; set; } //verificr este campo


        [Display(Name = "Fonte")]
        public string Fonte { get; set; }

        [Display(Name = "Categoria de Gasto")]
        public string CategoriaGasto { get; set; }

        [Display(Name = "Evento NC")]
        public string EventoNC { get; set; }

        [Display(Name = "UO")]
        public string Uo { get; set; }


        [Display(Name = "Fonte Recurso")]
        public string FonteRecurso { get; set; }

        [Display(Name = "Natureza Despesa")]
        public string NatDespesa { get; set; }

        [Display(Name = "UGO")]
        public string UGO { get; set; }

        [Display(Name = "Plano Interno")]
        public string PlanoInterno { get; set; }


        public DadoMovimentacaoViewModel CreateInstance(MovimentacaoOrcamentaria entity, IEnumerable<Estrutura> estrutura, IEnumerable<Programa> programa, IEnumerable<NaturezaTipo> tipoNatureza, IEnumerable<MovimentacaoTipo> movimentacao, IEnumerable<MovimentacaoDocumentoTipo> documento)
        {
            var _programa = programa.Where(x => x.Ano == DateTime.Now.Year);
            var _natureza = estrutura.FirstOrDefault(w => w.Codigo == entity.IdEstrutura);

            var obj = new DadoMovimentacaoViewModel();

            obj.IdTipoMovimentacao = Convert.ToString(entity.IdTipoMovimentacao);
            obj.NaturezaId = Convert.ToString(_natureza?.Codigo);


            obj.AnoExercicio = entity.AnoExercicio;

            obj.IdTipoDocumento = Convert.ToString(entity.IdTipoDocumento);
            
            obj.Fonte = entity.IdFonte.ToString().PadLeft(3, '0');
            obj.NaturezaListItems = estrutura.Select(s => new SelectListItem
            {
                Text = $"{s.Natureza.Formatar("0.0.00.00")} - {s.Fonte} - {s.Nomenclatura}",
                Value = s.Codigo.ToString(),
                Selected = s.Codigo == _natureza?.Codigo
            });

            obj.ProgramaListItems = _programa.Select(s => new SelectListItem
            {
                Text = $"{s.Cfp.Formatar("00.000.0000.0000")} {s.Descricao}",
                Value = s.Codigo.ToString(),
                Selected = s.Codigo == entity.IdPrograma
            }).ToList();

            obj.MovimentacaoListItems = movimentacao.Select(s => new SelectListItem
            {
                Text = s.Descricao,
                Value = s.Id.ToString(),
                Selected = s.Id == entity.IdTipoMovimentacao
            });

            obj.DocumentoTipoListItems = documento.Select(s => new SelectListItem
            {
                Text = s.Descricao,
                Value = s.Id.ToString(),
                Selected = s.Id == entity.IdTipoDocumento
            });

            return obj;
        }


        //public DadoApropriacaoViewModel CreateInstance(ILiquidacaoDespesa entity, IEnumerable<CodigoEvento> eventos, IEnumerable<EventoTipo> tipoeventos, IEnumerable<ObraTipo> obraTipo)
        //{

        //    CodigoTipoDeObra = entity.CodigoTipoDeObra;
        //    return new DadoApropriacaoViewModel()
        //    {
        //        CodigoEvento = Convert.ToString(entity.CodigoEvento),
        //        CodigoEventoListItems = eventos.Select(s => new SelectListItem
        //        {
        //            Text = s.Descricao,
        //            Value = s.Id.ToString(),
        //            Selected = s.Id == entity.CodigoEvento
        //        }),

        //        TipoEventoId = entity.TipoEventoId < 1 ? default(string) : Convert.ToString(entity.TipoEventoId),
        //        TipoEventoListItems = tipoeventos.Select(s => new SelectListItem
        //        {
        //            Text = s.Descricao,
        //            Value = s.Id.ToString(),
        //            Selected = s.Id == entity.TipoEventoId
        //        }),

        //        TipoObraId = entity.TipoObraId > 0 ? entity.TipoObraId.ToString() : default(string),
        //        TipoObraListItems = obraTipo.Select(s => new SelectListItem
        //        {
        //            Text = s.Descricao,
        //            Value = s.Id.ToString(),
        //            Selected = s.Id == entity.TipoObraId
        //        }),

        //        AnoMes = $"{entity.MesMedicao}{entity.AnoMedicao}",
        //        AnoMedicao = entity.AnoMedicao,
        //        MesMedicao = entity.MesMedicao,
        //        DataEmissao = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
        //        CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
        //        CodigoGestao = entity.CodigoGestao,
        //        Valor = entity.ValorAnular > 0 ? entity.ValorAnular.ToString() : entity.Valor.ToString(),
        //        Percentual = entity.Percentual,
        //        CodigoUnidadeGestoraObra = entity.CodigoUnidadeGestoraObra,
        //        CodigoTipoDeObra = entity.CodigoTipoDeObra,
        //        NumeroCNPJCPFCredor = entity.NumeroCNPJCPFCredor,
        //        NlReferencia = entity.NlReferencia,
        //        NumeroOriginalSiafemSiafisico = entity.NumeroOriginalSiafemSiafisico,
        //        NumeroCT = entity.NumeroCT,
        //        CodigoGestaoCredor = entity.CodigoGestaoCredor,
        //        NumeroObra = entity.NumeroObra,
        //        CodigoAplicacaoObra = entity.CodigoAplicacaoObra
        //    };
        //}
    }
}