

using Sids.Prodesp.Application;

namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Model.Extension;
    using Model.Entity.Seguranca;

    public class DadoApropriacaoEstruturaViewModel
    {


        [Display(Name = "Ano")]
        public string AnoExercicio { get; set; }  // utilizado na requisição
        public IEnumerable<SelectListItem> AnoExercicioListItens { get; set; }

        [Display(Name = "Órgão")]
        public string Regional { get; set; }
        public IEnumerable<SelectListItem> RegionalListItems { get; set; }


        [Display(Name = "CFP")]
        public string ProgramaId { get; set; }
        public IEnumerable<SelectListItem> ProgramaListItems { get; set; }


        [Display(Name = "CED")]
        public string Natureza { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }

        [Display(Name = "Origem do Recurso")]
        public string FonteId { get; set; }
        public IEnumerable<SelectListItem> FonteListItems { get; set; }


        [Display(Name = "Tipo de Natureza")]
        public string NaturezaSubempenhoId { get; set; }
        public IEnumerable<SelectListItem> NaturezaSubempenhoListItems { get; set; }


        [Display(Name = "Credor Organização(Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }


        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }


        [Display(Name = "CNPJ/CPF (Prodesp)")]
        public string NumeroCNPJCPFFornecedor { get; set; }


        [Display(Name ="Tipo de Serviço")]
        public string TipoServicoId { get; set; } // utilizado na requisicao
        public IEnumerable<SelectListItem> TipoServicoListItems { get; set; }


        public DadoApropriacaoEstruturaViewModel CreateInstance(Subempenho entity, IEnumerable<Estrutura> estrutura, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<Fonte> fonte, IEnumerable<NaturezaTipo> tipoNatureza)
        {
            var _programa = programa.Where(x => x.Ano == DateTime.Now.Year);
            var _natureza = estrutura.FirstOrDefault(w => w.Codigo == entity.NaturezaId);

            return new DadoApropriacaoEstruturaViewModel()
            {
                

                Natureza = Convert.ToString(_natureza?.Codigo),
                NaturezaListItems = estrutura.Select(s => new SelectListItem
                {
                    Text = $"{s.Natureza.Formatar("0.0.00.00")} - {s.Fonte}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == _natureza?.Codigo
                }),

                Regional = entity.RegionalId > 0 ? entity.RegionalId.ToString() : default(string),
                RegionalListItems = regional.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.RegionalId
                }).ToList(),

                ProgramaId = entity.ProgramaId > 0 ? entity.ProgramaId.ToString() : default(string),
                ProgramaListItems = _programa.Select(s => new SelectListItem
                {
                    Text = $"{s.Cfp.Formatar("00.000.0000.0000")} {s.Descricao}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == entity.ProgramaId
                }).ToList(),

                NaturezaSubempenhoId = tipoNatureza.FirstOrDefault(w => w.Id == entity.NaturezaSubempenhoId)?.Id,
                NaturezaSubempenhoListItems = tipoNatureza.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id,
                    Selected = s.Id == NaturezaSubempenhoId
                }).ToList(),

                FonteId = entity.FonteId > 0 ? entity.FonteId.ToString() : default(string),
                FonteListItems = fonte.Select(s => new SelectListItem
                {
                    Text = s.Codigo,
                    Value = s.Id.ToString(),
                    Selected = s.Codigo == FonteId
                }).ToList(),

                CodigoAplicacaoObra = entity.CodigoAplicacaoObra,
                NumeroCNPJCPFFornecedor = entity.NumeroCNPJCPFFornecedor,
                CodigoCredorOrganizacao = entity.CodigoCredorOrganizacao < 1 ? default(string) : Convert.ToString(entity.CodigoCredorOrganizacao)

            };
        }

        public DadoApropriacaoEstruturaViewModel CreateInstance(RapRequisicao entity, IEnumerable<Estrutura> estrutura, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<int> anos,IEnumerable<ServicoTipo> servicoTipo, IEnumerable<NaturezaTipo> tipoNatureza)
        {

            var _programa = programa.Where(x => x.Ano == DateTime.Now.Year).OrderBy(x => x.Cfp).ToList();
            var _servicoTipo = servicoTipo.Where(w => w.TipoRap == entity.TipoRap || string.IsNullOrWhiteSpace(w.TipoRap)).ToList();
            var _natureza = estrutura.Where(x => _programa.Select(y => y.Codigo).ToList().Contains(x.Programa.Value) && (x.Programa == entity.ProgramaId || entity.ProgramaId == 0)).ToList();
            var _naturezaId = _natureza.Where(w => w.Codigo == entity.NaturezaId).SingleOrDefault()?.Codigo;

            var ano = entity.NumeroAnoExercicio > 0 ? entity.NumeroAnoExercicio : DateTime.Now.Year;

            var dadoApropriacaoEstruturaViewModel = new DadoApropriacaoEstruturaViewModel()
            {
                AnoExercicio = Convert.ToString(ano),
                AnoExercicioListItens = anos
               .Select(s => new SelectListItem
               {
                   Text = Convert.ToString(s),
                   Value = Convert.ToString(s),
                   Selected = s == entity.NumeroAnoExercicio
               }),

                Natureza = Convert.ToString(_naturezaId),
                NaturezaListItems = _natureza.OrderBy(x => x.Natureza)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Natureza.Formatar("0.0.00.00")} - {s.Fonte}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == _naturezaId
                }),

                Regional = entity.RegionalId > 0 ? entity.RegionalId.ToString() : default(string),
                RegionalListItems = regional.Where(x => x.Id > 1)
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.RegionalId
                })
                .ToList(),

                ProgramaId = entity.ProgramaId > 0 ? entity.ProgramaId.ToString() : default(string),
                ProgramaListItems = _programa.Select(s => new SelectListItem
                {
                    Text = $"{s.Cfp.Formatar("00.000.0000.0000")} {s.Descricao}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == entity.ProgramaId
                }).ToList(),

                TipoServicoId = Convert.ToString(entity.TipoServicoId),
                TipoServicoListItems = _servicoTipo.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = Convert.ToString(s.Id),
                    Selected = s.Id == entity.TipoServicoId
                }).ToList(),

                NaturezaSubempenhoId = tipoNatureza.FirstOrDefault(w => w.Id == entity.CodigoNaturezaItem)?.Id,
                NaturezaSubempenhoListItems = tipoNatureza.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id,
                    Selected = s.Id == NaturezaSubempenhoId
                }).ToList(),
                              

                //CodigoCredorOrganizacao = entity.CodigoCredorOrganizacao < 1 ? default(string) : Convert.ToString(entity.CodigoCredorOrganizacao)

            };
            return dadoApropriacaoEstruturaViewModel;
        }

        public DadoApropriacaoEstruturaViewModel CreateInstance(RapAnulacao entity, IEnumerable<Estrutura> estrutura, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<int> anos, IEnumerable<ServicoTipo> servicoTipo, IEnumerable<NaturezaTipo> tipoNatureza)
        {
            var _programa = programa.Where(x => x.Ano == DateTime.Now.Year).OrderBy(x => x.Cfp).ToList();
            var _servicoTipo = servicoTipo.Where(w => w.TipoRap == entity.TipoRap || string.IsNullOrWhiteSpace(w.TipoRap)).ToList();
            var _natureza = estrutura.Where(x => _programa.Select(y => y.Codigo).ToList().Contains(x.Programa.Value) && (x.Programa == entity.ProgramaId || entity.ProgramaId == 0)).ToList();
            var _naturezaId = _natureza.Where(w => w.Codigo == entity.NaturezaId).SingleOrDefault()?.Codigo;

            var ano = entity.NumeroAnoExercicio > 0 ? entity.NumeroAnoExercicio : DateTime.Now.Year;

            var dadoApropriacaoEstruturaViewModel = new DadoApropriacaoEstruturaViewModel()
            {
                AnoExercicio = Convert.ToString(ano),
                AnoExercicioListItens = anos
               .Select(s => new SelectListItem
               {
                   Text = Convert.ToString(s),
                   Value = Convert.ToString(s),
                   Selected = s == entity.NumeroAnoExercicio
               }),

                Natureza = Convert.ToString(_naturezaId),
                NaturezaListItems = _natureza.OrderBy(x => x.Natureza)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Natureza.Formatar("0.0.00.00")} - {s.Fonte}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == _naturezaId
                }),

                Regional = entity.RegionalId > 0 ? entity.RegionalId.ToString() : default(string),
                RegionalListItems = regional.Where(x => x.Id > 1)
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.RegionalId
                })
                .ToList(),

                ProgramaId = entity.ProgramaId > 0 ? entity.ProgramaId.ToString() : default(string),
                ProgramaListItems = _programa.Select(s => new SelectListItem
                {
                    Text = $"{s.Cfp.Formatar("00.000.0000.0000")} {s.Descricao}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == entity.ProgramaId
                }).ToList(),

                TipoServicoId = Convert.ToString(entity.TipoServicoId),
                TipoServicoListItems = _servicoTipo.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = Convert.ToString(s.Id),
                    Selected = s.Id == entity.TipoServicoId
                }).ToList(),

                NaturezaSubempenhoId = tipoNatureza.FirstOrDefault(w => w.Id == entity.CodigoNaturezaItem)?.Id,
                NaturezaSubempenhoListItems = tipoNatureza.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id,
                    Selected = s.Id == NaturezaSubempenhoId
                }).ToList()
            };
        return dadoApropriacaoEstruturaViewModel;
        }

    }
}









