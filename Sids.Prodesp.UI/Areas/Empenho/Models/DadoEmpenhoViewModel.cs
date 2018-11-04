namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Interface.Empenho;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class DadoEmpenhoViewModel
    {
        [Display(Name = "Tipo de Empenho")]
        public string EmpenhoTipoId { get; set; }
        public IEnumerable<SelectListItem> EmpenhoTipoListItems { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Evento")]
        public string CodigoEvento { get; set; }

        [Display(Name = "Código NATUR (Item)")]
        public string CodigoNaturezaItem { get; set; }

        [Display(Name = "Data de Emissão")]
        public string DataEmissao { get; set; }

        [Display(Name = "OC")]
        public string NumeroOC { get; set; }

        [Display(Name = "UGF")]
        public string CodigoUnidadeGestoraFornecedora { get; set; }

        [Display(Name = "Gestão Fornecedora")]
        public string CodigoGestaoFornecedora { get; set; }

        //[Display(Name = "CNPJ / CPF / UG Credor (SIAFEM/SIAFISICO)")]
        [Display(Name = "CNPJ / CPF (SIAFEM/SIAFISICO)")]
        public string NumeroCNPJCPFUGCredor { get; set; }

        [Display(Name = "Gestão Credor (SIAFEM)")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Credor - Organização (Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ / CPF (Prodesp)")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        [Display(Name = "UO")]
        public string CodigoUO { get; set; }

        [Display(Name = "UGO")]
        public string CodigoUGO { get; set; }

        [Display(Name = "Município")]
        public string CodigoMunicipio { get; set; }

        [Display(Name = "Acordo")]
        public string DescricaoAcordo { get; set; }

        [Display(Name = "Modalidade")]
        public string ModalidadeId { get; set; }
        public IEnumerable<SelectListItem> ModalidadeListItems { get; set; }

        [Display(Name = "Licitação")]
        public string LicitacaoId { get; set; }
        public IEnumerable<SelectListItem> LicitacaoListItems { get; set; }

        [Display(Name = "Tipo de Aquisição")]
        public string TipoAquisicaoId { get; set; }
        public IEnumerable<SelectListItem> TipoAquisicaoListItems { get; set; }

        [Display(Name = "Nº do Contrato (fornec.)")]
        public string NumeroContratoFornecedor { get; set; }

        [Display(Name = "Número do Edital")]
        public string NumeroEdital { get; set; }

        [Display(Name = "Referência Legal")]
        public string DescricaoReferenciaLegal { get; set; }

        [Display(Name = "Origem Material")]
        public string OrigemMaterialId { get; set; }
        public IEnumerable<SelectListItem> OrigemMaterialListItems { get; set; }

        [Display(Name = "Destino do Recurso")]
        public string DestinoId { get; set; }
        public IEnumerable<SelectListItem> DestinoListItems { get; set; }

        [Display(Name = "Cód. Aplicação")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Local de Entrega")]
        public string DescricaoLocalEntregaSiafem { get; set; }

        [Display(Name = "Data de Entrega")]
        public string DataEntregaMaterial { get; set; }

        [Display(Name = "Unidade de Fornecimento")]
        public string CodigoUnidadeFornecimento { get; set; }


        [Display(Name = "Nº Processo (NE)")]
        public string NumeroProcessoNE { get; set; }


        [Display(Name = "Contabilizar como BEC?")]
        public bool ContBec { get; set; }

        public DadoEmpenhoViewModel CreateInstance(IEmpenho objModel, IEnumerable<EmpenhoTipo> empenhoTipo, IEnumerable<Modalidade> modalidade, IEnumerable<OrigemMaterial> material, IEnumerable<Licitacao> licitacao, IEnumerable<Destino> destino, IEnumerable<AquisicaoTipo> aquisicao)
        {
            return new DadoEmpenhoViewModel()
            {
                NumeroProcessoNE = objModel.NumeroProcessoNE,

                CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                CodigoGestao = objModel.CodigoGestao,
                CodigoEvento = objModel.CodigoEvento > 0 ? objModel.CodigoEvento.ToString() : default(string),
                DataEmissao = objModel.DataEmissao > default(DateTime) ? objModel.DataEmissao.ToShortDateString() : default(string),
                NumeroCNPJCPFUGCredor = objModel.NumeroCNPJCPFUGCredor,
                CodigoGestaoCredor = objModel.CodigoGestaoCredor,
                CodigoCredorOrganizacao = objModel.CodigoCredorOrganizacao > 0 ? objModel.CodigoCredorOrganizacao.ToString() : default(string),
                NumeroCNPJCPFFornecedor = objModel.NumeroCNPJCPFFornecedor,
                CodigoUO = objModel.CodigoUO > 0 ? objModel.CodigoUO.ToString() : default(string),

                CodigoUnidadeGestoraFornecedora = objModel.CodigoUnidadeGestoraFornecedora,
                CodigoGestaoFornecedora = objModel.CodigoGestaoFornecedora,

                ContBec = objModel.ContBec,


                TipoAquisicaoId = objModel.TipoAquisicaoId > 0 ? objModel.TipoAquisicaoId.ToString() : default(string),
                TipoAquisicaoListItems = aquisicao
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(TipoAquisicaoId)
                }),

                ModalidadeId = objModel.ModalidadeId > 0 ? objModel.ModalidadeId.ToString() : default(string),
                ModalidadeListItems = modalidade
                .Select(s => new SelectListItem
                {
                    Text = string.Concat(s.Id, "-", s.Descricao),
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(ModalidadeId)
                }),

                LicitacaoId = objModel.LicitacaoId,
                LicitacaoListItems = licitacao
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == LicitacaoId
                }),

                OrigemMaterialId = objModel.OrigemMaterialId > 0 ? objModel.OrigemMaterialId.ToString() : default(string),
                OrigemMaterialListItems = material
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(OrigemMaterialId)
                }),


                DestinoId = objModel.DestinoId,
                DestinoListItems = destino
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao.ToString(),
                    Value = s.Codigo,
                    Selected = s.Codigo == DestinoId
                })
            };
        }

        public DadoEmpenhoViewModel CreateInstance(Empenho objModel, IEnumerable<EmpenhoTipo> empenhoTipo, IEnumerable<Modalidade> modalidade, IEnumerable<OrigemMaterial> material, IEnumerable<Licitacao> licitacao, IEnumerable<Destino> destino, IEnumerable<AquisicaoTipo> aquisicao)
        {
            return new DadoEmpenhoViewModel()
            {
                EmpenhoTipoId = objModel.EmpenhoTipoId > 0 ? objModel.EmpenhoTipoId.ToString() : default(string),
                EmpenhoTipoListItems = empenhoTipo
                .Select(s => new SelectListItem
                {
                    Text = string.Concat(s.Id, "-", s.Descricao),
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(EmpenhoTipoId)
                }),

                NumeroProcessoNE = objModel.NumeroProcessoNE,

                CodigoUnidadeGestora = objModel.CodigoUnidadeGestora,
                CodigoGestao = objModel.CodigoGestao,
                CodigoEvento = objModel.CodigoEvento > 0 ? objModel.CodigoEvento.ToString() : default(string),
                CodigoNaturezaItem = objModel.CodigoNaturezaItem,
                DataEmissao = objModel.DataEmissao > default(DateTime) ? objModel.DataEmissao.ToShortDateString() : default(string),
                NumeroCNPJCPFUGCredor = objModel.NumeroCNPJCPFUGCredor,
                CodigoGestaoCredor = objModel.CodigoGestaoCredor,
                CodigoCredorOrganizacao = objModel.CodigoCredorOrganizacao > 0 ? objModel.CodigoCredorOrganizacao.ToString() : default(string),
                NumeroCNPJCPFFornecedor = objModel.NumeroCNPJCPFFornecedor,
                CodigoUO = objModel.CodigoUO > 0 ? objModel.CodigoUO.ToString() : default(string),

                NumeroOC = objModel.NumeroOC,
                CodigoUnidadeGestoraFornecedora = objModel.CodigoUnidadeGestoraFornecedora,
                CodigoGestaoFornecedora = objModel.CodigoGestaoFornecedora,

                TipoAquisicaoId = objModel.TipoAquisicaoId > 0 ? objModel.TipoAquisicaoId.ToString() : default(string),
                TipoAquisicaoListItems = aquisicao
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(TipoAquisicaoId)
                }),

                CodigoUGO = objModel.CodigoUGO > 0 ? objModel.CodigoUGO.ToString() : default(string),
                CodigoMunicipio = objModel.CodigoMunicipio,
                DescricaoAcordo = objModel.DescricaoAcordo,
                ModalidadeId = objModel.ModalidadeId > 0 ? objModel.ModalidadeId.ToString() : default(string),
                ModalidadeListItems = modalidade
                .Select(s => new SelectListItem
                {
                    Text = string.Concat(s.Id, "-", s.Descricao),
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(ModalidadeId)
                }),

                LicitacaoId = objModel.LicitacaoId,
                LicitacaoListItems = licitacao
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == LicitacaoId
                }),

                DescricaoReferenciaLegal = objModel.DescricaoReferenciaLegal,
                OrigemMaterialId = objModel.OrigemMaterialId > 0 ? objModel.OrigemMaterialId.ToString() : default(string),
                OrigemMaterialListItems = material
                .Select(s => new SelectListItem
                {
                    Text = string.Concat(s.Id, "-", s.Descricao),
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(OrigemMaterialId)
                }),

                DestinoId = objModel.DestinoId,
                DestinoListItems = destino
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao.ToString(),
                    Value = s.Codigo,
                    Selected = s.Codigo == DestinoId
                }),

                EventoListItems = new List<SelectListItem>
                {
                    new SelectListItem {
                    Text = "400051-Empenho novo com reserva",
                    Value = "400051",
                    Selected = "400051" == objModel.CodigoEvento.ToString()
                    },

                    new SelectListItem {
                    Text = "400091­-Empenho sem reserva",
                    Value = "400091",
                    Selected = "400091" == objModel.CodigoEvento.ToString()
                    },

                    new SelectListItem {
                    Text = "400098-Empenho de adiantamento",
                    Value = "400098",
                    Selected = "400098" == objModel.CodigoEvento.ToString()
                    }
                },

                CodigoAplicacaoObra = objModel.CodigoAplicacaoObra,
                DescricaoLocalEntregaSiafem = objModel.DescricaoLocalEntregaSiafem,
                DataEntregaMaterial = objModel.DataEntregaMaterial > default(DateTime) ? objModel.DataEntregaMaterial.ToShortDateString() : default(string),
                NumeroEdital = objModel.NumeroEdital,
                NumeroContratoFornecedor = objModel.NumeroContratoFornecedor,
                ContBec = objModel.ContBec
            };
        }

        public IEnumerable<SelectListItem> EventoListItems { get; set; }
    }
}