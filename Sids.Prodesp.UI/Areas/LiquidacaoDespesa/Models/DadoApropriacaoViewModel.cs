namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Entity.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Model.Extension;


    public class DadoApropriacaoViewModel
    {
        [Display(Name = "NL Referência")]
        public string NlReferencia { get; set; }

        [Display(Name = "Empenho SIAFEM/SIAFISICO")]
        public string NumeroOriginalSiafemSiafisico { get; set; }


        [Display(Name = "Nº do CT")]
        public string NumeroCT { get; set; }


        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }


        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }


        [Display(Name = "Cód. Nota Fiscal(Prodesp)")]
        public string CodigoNotaFiscalProdesp { get; set; }


        [Display(Name = "Valor")]
        public string Valor { get; set; }

        //[Display(Name = "Valor Realizado")]
        //public string ValorRealizado { get; set; }

        //[Display(Name = "Valor a Anular")]
        //public string ValorAnular { get; set; }


        [Display(Name = "Nº da Medição")]
        public string NumeroMedicao { get; set; }

        [Display(Name = "Tipo Evento")]
        public string TipoEventoId { get; set; }
        public IEnumerable<SelectListItem> TipoEventoListItems { get; set; }

        [Display(Name = "Código do Evento")]
        public string CodigoEvento { get; set; }


        [Display(Name = "Data da Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "Ano / Medição")]
        public string AnoMedicao { get; set; }

        [Display(Name = "Mês / Medição")]
        public string MesMedicao { get; set; }

        [Display(Name = "Mês/Ano Medição")]
        public string AnoMes { get; set; }

        [Display(Name = "Percentual")]
        public string Percentual { get; set; }
              

        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }

        public IEnumerable<SelectListItem> CodigoEventoListItems { get; set; }

        [Display(Name = "CNPJ/CPF/UG Credor (SIAFEM/SIAFISICO)")]
        public string NumeroCNPJCPFCredor { get; set; }

        [Display(Name = "Gestão Credor (SIAFEM/SIAFISICO)")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Credor Organização(Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "Tipo de Obra")]
        public string TipoObraId { get; set; }
        public IEnumerable<SelectListItem> TipoObraListItems { get; set; }

        [Display(Name = "Tipo de Obra")]
        public string CodigoTipoDeObra { get; set; }
       

        [Display(Name = "Unidade Gestora da Obra")]
        public string CodigoUnidadeGestoraObra { get; set; }

        [Display(Name = "Número Obra")]
        public string NumeroObra { get; set; }


     

        [Display(Name = "Regional")]
        public string RegionalId { get; set; }

        public List<SelectListItem> RegionalListItems { get; set; }

        [Display(Name = "CFP")]
        public string ProgramaId { get; set; }
        public List<SelectListItem> ProgramaListItems { get; set; }

        [Display(Name = "CED")]
        public string NaturezaId { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }

        [Display(Name = "Fonte")]
        public string FonteId { get; set; }

        [Display(Name = "CNPJ/CPF Fornecedor")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        public List<SelectListItem> FonteListItems { get; set; }

        [Display(Name = "Tipo de Natureza")] //utilizado no subempenho como TIpo Natureza
        public string NaturezaSubempenhoId { get; set; }
        public List<SelectListItem> NaturezaSubempenhoListItems { get; set; }

        public DadoApropriacaoViewModel CreateInstance(Subempenho entity, IEnumerable<CodigoEvento> eventos, IEnumerable<EventoTipo> eventosTipo , IEnumerable<Estrutura> estrutura, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<Fonte> fonte, IEnumerable<NaturezaTipo> tipoNatureza, IEnumerable<ObraTipo> obraTipo)
        {
            var _programa = programa.Where(x => x.Ano == DateTime.Now.Year);
            var _natureza = estrutura.FirstOrDefault(w => w.Codigo == entity.NaturezaId);
           

            //Alex 04/05/2017 - Tratando a fonte para 2 digitos e removendo as repetidas.
            IList<Fonte> _fonte = new List<Fonte>();
            foreach (var item in fonte)
            {
                _fonte.Add(new Fonte()
                {
                    Id = item.Id,
                    Codigo = item.Codigo.Substring(1, 2),
                    Descricao = item.Descricao
                });
            }            
            _fonte = _fonte.GroupBy(x => x.Codigo)
                   .Select(grp => grp.First())
                   .ToList();

            return new DadoApropriacaoViewModel()
            {

                CodigoEvento = Convert.ToString(entity.CodigoEvento),
                CodigoEventoListItems = eventos.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.CodigoEvento
                }),

                TipoEventoId = entity.TipoEventoId < 1 ? default(string) : Convert.ToString(entity.TipoEventoId),
                TipoEventoListItems = eventosTipo
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.TipoEventoId
                }),

                NaturezaId = Convert.ToString(_natureza?.Codigo),
                NaturezaListItems = estrutura.Select(s => new SelectListItem
                {
                    Text = $"{s.Natureza.Formatar("0.0.00.00")} - {s.Fonte}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == _natureza?.Codigo
                }),

                RegionalId = entity.RegionalId > 0 ? entity.RegionalId.ToString() : default(string),
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

                FonteId = entity.FonteId > 0 ? entity.FonteId.ToString() : default(string),
                //FonteListItems = fonte.Select(s => new SelectListItem
                //{
                //    Text = s.Codigo,
                //    Value = s.Id.ToString(),
                //    Selected = s.Codigo == FonteId
                //}).ToList(),

                //Alex 04/05/2017 - _fonte Tratada para 2 digitos e removendo as repetidas.
                FonteListItems = _fonte.Select(s => new SelectListItem
                {
                    Text = s.Codigo,
                    Value = s.Id.ToString(),
                    Selected = s.Codigo == FonteId
                }).ToList(),

                NaturezaSubempenhoId = tipoNatureza.FirstOrDefault(w => w.Id == entity.NaturezaSubempenhoId)?.Id,
                NaturezaSubempenhoListItems = tipoNatureza.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id,
                    Selected = s.Id == NaturezaSubempenhoId
                }).ToList(),

                TipoObraId = entity.TipoObraId > 0 ? entity.TipoObraId.ToString() : default(string),

                TipoObraListItems = obraTipo.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.TipoObraId
                }).Where(c => c != null).ToList(),


                CodigoTipoDeObra = Convert.ToString(entity.CodigoTipoDeObra),

                AnoMes = $"{entity.MesMedicao}{entity.AnoMedicao}",
                MesMedicao = entity.MesMedicao,
                AnoMedicao = entity.AnoMedicao,
                DataEmissao = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                CodigoNotaFiscalProdesp = entity.CodigoNotaFiscalProdesp,
                NumeroMedicao = entity.NumeroMedicao,
                Valor = entity.ValorRealizado > 0 ? entity.ValorRealizado.ToString() : entity.Valor.ToString(),
                Percentual = entity.Percentual,
                CodigoUnidadeGestoraObra = entity.CodigoUnidadeGestoraObra,
                CodigoCredorOrganizacao = entity.CodigoCredorOrganizacao < 1 ? default(string) : Convert.ToString(entity.CodigoCredorOrganizacao),
                NumeroCNPJCPFFornecedor = entity.NumeroCNPJCPFFornecedor,
                NumeroCNPJCPFCredor = entity.NumeroCNPJCPFCredor,
                NumeroOriginalSiafemSiafisico = entity.NumeroOriginalSiafemSiafisico,
                CodigoGestaoCredor = entity.CodigoGestaoCredor,
                NumeroObra = entity.NumeroObra,
                NumeroCT = entity.NumeroCT,
                CodigoAplicacaoObra = entity.CodigoAplicacaoObra
            };
        }


        public DadoApropriacaoViewModel CreateInstance(ILiquidacaoDespesa entity, IEnumerable<CodigoEvento> eventos, IEnumerable<EventoTipo> tipoeventos, IEnumerable<ObraTipo> obraTipo)
        {

            CodigoTipoDeObra = entity.CodigoTipoDeObra;
            return new DadoApropriacaoViewModel()
            {
                CodigoEvento = Convert.ToString(entity.CodigoEvento),
                CodigoEventoListItems = eventos.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.CodigoEvento
                }),

                TipoEventoId = entity.TipoEventoId < 1 ? default(string) : Convert.ToString(entity.TipoEventoId),
                TipoEventoListItems = tipoeventos.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.TipoEventoId
                }),

                TipoObraId = entity.TipoObraId > 0 ? entity.TipoObraId.ToString() : default(string),
                TipoObraListItems = obraTipo.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.TipoObraId
                }),

                AnoMes = $"{entity.MesMedicao}{entity.AnoMedicao}",
                AnoMedicao = entity.AnoMedicao,
                MesMedicao = entity.MesMedicao,
                DataEmissao = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                Valor = entity.ValorAnular > 0 ? entity.ValorAnular.ToString() : entity.Valor.ToString(),
                Percentual = entity.Percentual,
                CodigoUnidadeGestoraObra = entity.CodigoUnidadeGestoraObra,
                CodigoTipoDeObra = entity.CodigoTipoDeObra,
                NumeroCNPJCPFCredor = entity.NumeroCNPJCPFCredor,
                NlReferencia = entity.NlReferencia,
                NumeroOriginalSiafemSiafisico = entity.NumeroOriginalSiafemSiafisico,
                NumeroCT = entity.NumeroCT,
                CodigoGestaoCredor = entity.CodigoGestaoCredor,
                NumeroObra = entity.NumeroObra,
                CodigoAplicacaoObra = entity.CodigoAplicacaoObra
            };
        }
    }
}