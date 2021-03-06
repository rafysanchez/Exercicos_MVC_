﻿namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Model.Extension;

    public class DadoRequisicaoRapViewModel
    {
        [Display(Name = "Nº do Empenho Prodesp")]
        public string NumeroOriginalProdesp { get; set; }

        [Display(Name = "Nº do Empenho SIAFEM")]
        public string NumeroOriginalSiafemSiafisico { get; set; }

        [Display(Name = "Nº do Contrato")]
        public string NumeroContrato { get; set; }


        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }


        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Valor")]
        public string Valor { get; set; }

        [Display(Name = "Data da Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "Código da Tarefa")]
        public string CodigoTarefa { get; set; }

        [Display(Name = "Código da Tarefa")]
        public string Tarefa { get; set; }
        
        [Display(Name = "Código da Despesa")]
        public string CodigoDespesa { get; set; }


        [Display(Name = "Valor Realizado")]
        public string ValorRealizado { get; set; }

        [Display(Name ="Valor Subempenhar")]
        public int ValorSubempenhar { get; set; }


        [Display(Name = "Prazo de Pagamento")]
        public string DescricaoPrazoPagamento { get; set; }

        [Display(Name = "Data de Realização")]
        public string DataRealizado { get; set; }

        [Display(Name = "CNPJ/CPF/UG Credor (SIAFEM)")]
        public string NumeroCNPJCPFCredor { get; set; }

        [Display(Name = "Gestão Credor (SIAFEM)")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Credor Organização(Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ/CPF (Prodesp)")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        [Display(Name = "Classificação")]
        public string Classificacao { get; set; }

        [Display(Name = "Ano / Medição")]
        public string AnoMedicao { get; set; }

        [Display(Name = "Mês / Medição")]
        public string MesMedicao { get; set; }

        [Display(Name = "Mês/Ano Medição")]
        public string AnoMes { get; set; } // verificar utilização

        [Display(Name = "Código da Nota fiscal(Prodesp)")]
        public string CodigoNotaFiscalProdesp { get; set; }

        [Display(Name = "Nº da Medição")]
        public string NumeroMedicao { get; set; }

        [Display(Name = "Natureza")]
        public string NaturezaSubempenhoId { get; set; }
        public IEnumerable<SelectListItem> NaturezaSubempenhoListItems { get; set; }

        [Display(Name = "PTRES")]
        public string CodigoPtres { get; set; }
        //public IEnumerable<SelectListItem> CodigoPtresListItems { get; set; }

        [Display(Name = "CFP")]
        public string ProgramaId { get; set; }
        public IEnumerable<SelectListItem> ProgramaListItems { get; set; }


        [Display(Name = "CED")]
        public string NaturezaId { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }


        [Display(Name = "Tipo de Serviço")]
        public string TipoServicoId { get; set; }
        public IEnumerable<SelectListItem> TipoServicoTipoListItems { get; set; }



        public DadoRequisicaoRapViewModel CreateInstance(RapRequisicao entity, IEnumerable<Estrutura> estrutura, IEnumerable<NaturezaTipo> tipoNatureza, IEnumerable<Programa> programa, IEnumerable<ServicoTipo> servicoTipo)
        {
            var _programa = programa.Where(x => x.Ano == DateTime.Now.Year).ToList();
            var _servicoTipo = servicoTipo.Where(w => w.TipoRap == entity.TipoRap || string.IsNullOrWhiteSpace(w.TipoRap)).ToList();
            var _natureza = estrutura.Where(x => _programa.Select(y => y.Codigo).ToList().Contains(x.Programa.Value) && (x.Programa == entity.ProgramaId || entity.ProgramaId == 0)).ToList();

            var _programasAnoAtual = programa.Where(x => x.Ano == entity.NumeroAnoExercicio);
            var _ptres = programa.Where(w => w.Codigo == entity.ProgramaId).SingleOrDefault()?.Ptres;

            var cpfAnterior = programa.FirstOrDefault(x => x.Codigo == entity.ProgramaId);
            var cpfAtual = _programasAnoAtual.FirstOrDefault(x => x.Cfp.Equals(cpfAnterior?.Cfp));

            var naturezaAnterior = estrutura.FirstOrDefault(x => x.Codigo == entity.NaturezaId);

            var _naturezasDoProgramaAnterior = estrutura.Where(x => x.Programa == cpfAnterior?.Codigo || cpfAnterior?.Codigo == 0);
            var _naturezasDoProgramaAtual = estrutura.Where(x => x.Programa == cpfAtual?.Codigo || cpfAtual?.Codigo == 0);

            var naturezaAtual = _naturezasDoProgramaAtual.FirstOrDefault(x => x.Natureza.Equals(naturezaAnterior?.Natureza) && x.Fonte == naturezaAnterior?.Fonte);
            var _naturezaAtualId = _naturezasDoProgramaAtual.FirstOrDefault(w => w.Natureza == naturezaAnterior?.Natureza && w.Fonte == naturezaAnterior?.Fonte)?.Codigo;

            var _naturezaId = _natureza.Where(w => w.Codigo == entity.NaturezaId).SingleOrDefault()?.Codigo;

            return new DadoRequisicaoRapViewModel()
            {
                NaturezaSubempenhoId = tipoNatureza.FirstOrDefault(w => w.Id == entity.NaturezaSubempenhoId)?.Id,
                NaturezaSubempenhoListItems = tipoNatureza.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id,
                    Selected = s.Id == NaturezaSubempenhoId
                }).ToList(),

                MesMedicao = entity.MesMedicao,
                AnoMedicao = entity.AnoMedicao,
                DataEmissao = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
                CodigoTarefa = entity.CodigoTarefa,
                Tarefa = entity.Tarefa,
                CodigoDespesa = entity.CodigoDespesa,
                ValorRealizado = Convert.ToString(entity.ValorRealizado),
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                Valor = entity.Valor < 1 ? default(string) : Convert.ToString(entity.Valor),
                CodigoCredorOrganizacao = entity.CodigoCredorOrganizacao < 1 ? default(string) : Convert.ToString(entity.CodigoCredorOrganizacao),
                CodigoNotaFiscalProdesp = entity.CodigoNotaFiscalProdesp,
                NumeroMedicao = entity.NumeroMedicao,
                NumeroOriginalProdesp = entity.NumeroOriginalProdesp,
                NumeroOriginalSiafemSiafisico = entity.NumeroOriginalSiafemSiafisico,
                NumeroContrato = entity.NumeroContrato,
                CodigoAplicacaoObra = entity.CodigoAplicacaoObra,
                DataRealizado = entity.DataRealizado == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
                ValorSubempenhar = entity.ValorSubempenhar,
                DescricaoPrazoPagamento = entity.DescricaoPrazoPagamento,
                Classificacao = entity.Classificacao,
                NumeroCNPJCPFCredor = entity.NumeroCNPJCPFCredor,
                CodigoGestaoCredor = entity.CodigoGestaoCredor,
                NumeroCNPJCPFFornecedor = entity.NumeroCNPJCPFFornecedor,

                CodigoPtres = _ptres,

                NaturezaId = Convert.ToString(_naturezaAtualId),
                NaturezaListItems = _naturezasDoProgramaAtual.OrderBy(x => x.Natureza)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Natureza.Formatar("0.0.00.00")} - {s.Fonte}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == _naturezaAtualId
                }),

                ProgramaId = entity.ProgramaId > 0 ? cpfAtual?.Codigo.ToString() : default(string),
                ProgramaListItems = _programasAnoAtual.OrderBy(x => x.Cfp)
                .Select(s => new SelectListItem
                {
                    Text = $"{s.Cfp.Formatar("00.000.0000.0000")} {s.Descricao}",
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == entity.ProgramaId
                }),

                TipoServicoId = Convert.ToString(entity.TipoServicoId),
                TipoServicoTipoListItems = _servicoTipo.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = Convert.ToString(s.Id),
                    Selected = s.Id == entity.TipoServicoId
                }).ToList(),

            };
        }


        public DadoRequisicaoRapViewModel CreateInstance(RapAnulacao entity, IEnumerable<EventoTipo> eventos, IEnumerable<Estrutura> estrutura, IEnumerable<NaturezaTipo> tipoNatureza, IEnumerable<ObraTipo> obraTipo)
        {

            return new DadoRequisicaoRapViewModel()
            {

                NaturezaSubempenhoId = tipoNatureza.FirstOrDefault(w => w.Id == entity.NaturezaSubempenhoId)?.Id,
                NaturezaSubempenhoListItems = tipoNatureza.Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id,
                    Selected = s.Id == NaturezaSubempenhoId
                }).ToList(),

                AnoMes = $"{entity.MesMedicao}{entity.AnoMedicao}",
                DataEmissao = entity.DataEmissao == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
                CodigoTarefa = entity.CodigoTarefa,
                Tarefa = entity.Tarefa,
                CodigoDespesa = entity.CodigoDespesa,
                ValorRealizado = Convert.ToString(entity.ValorRealizado),
                CodigoUnidadeGestora = entity.CodigoUnidadeGestora,
                CodigoGestao = entity.CodigoGestao,
                Valor = entity.Valor < 1 ? default(string) : Convert.ToString(entity.Valor),
                CodigoCredorOrganizacao = entity.CodigoCredorOrganizacao < 1 ? default(string) : Convert.ToString(entity.CodigoCredorOrganizacao),
                CodigoNotaFiscalProdesp = entity.CodigoNotaFiscalProdesp,
                NumeroMedicao = entity.NumeroMedicao,
                NumeroOriginalProdesp = entity.NumeroOriginalProdesp,
                NumeroOriginalSiafemSiafisico = entity.NumeroOriginalSiafemSiafisico,
                NumeroContrato = entity.NumeroContrato,
                CodigoAplicacaoObra = entity.CodigoAplicacaoObra,
                DataRealizado = entity.DataRealizado == default(DateTime) ? default(string) : Convert.ToString(entity.DataEmissao.ToShortDateString()),
                ValorSubempenhar = entity.ValorSubempenhar,
                DescricaoPrazoPagamento = entity.DescricaoPrazoPagamento,
                Classificacao = entity.Classificacao,
                NumeroCNPJCPFCredor = entity.NumeroCNPJCPFCredor,
                CodigoGestaoCredor = entity.CodigoGestaoCredor,
                NumeroCNPJCPFFornecedor = entity.NumeroCNPJCPFFornecedor,
                AnoMedicao = entity.AnoMedicao,
                MesMedicao = entity.MesMedicao,
            };
        }

    }
}