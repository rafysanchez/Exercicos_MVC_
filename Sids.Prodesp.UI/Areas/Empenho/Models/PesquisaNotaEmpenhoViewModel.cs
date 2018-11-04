
namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Extension;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class PesquisaNotaEmpenhoViewModel
    {
        [DisplayName("Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [DisplayName("Gestão")]
        public string CodigoGestao { get; set; }

        [DisplayName("Nº do Empenho")]
        public string CodigoEmpenhoOriginal { get; set; }

        [DisplayName("Data da Emissão")]
        public string DataEmissao { get; set; }

        [DisplayName("CNPJ/CPF/UG Credor SIAFEM/ SIAFISICO")]
        public string NumeroCNPJCPFUGCredor { get; set; }

        [DisplayName("Gestão Credor (SIAFEM/SIAFISICO)")]
        public string CodigoGestaoCredor { get; set; }
                
        [Display(Name = "Modalidade")]
        public string ModalidadeId { get; set; }
        public IEnumerable<SelectListItem> ModalidadeListItems { get; set; }

        [Display(Name = "Licitação")]
        public string LicitacaoId { get; set; }
        public IEnumerable<SelectListItem> LicitacaoListItems { get; set; }


        [DisplayName("Fonte")]
        public string CodigoFonteSiafisico { get; set;}
        public IEnumerable<SelectListItem> FonteListItems { get; set; }
                    
        [DisplayName("Natureza")]
        public string CodigoNaturezaNe { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }

        [DisplayName("Nº do Processo NE")]
        public string NumeroProcessoNE { get; private set; }
        
        public PesquisaNotaEmpenhoViewModel CreateInstance(
        string CodigoUnidadeGestora,
        string CodigoGestao,
        string CodigoEmpenhoOriginal,
        DateTime DataEmissao,
        string NumeroCNPJCPFUGCredor,
        string CodigoGestaoCredor,
        int ModalidadeId,
        string LicitacaoId,
        string CodigoNaturezaNe,
        string CodigoFonteSiafisico, 
        string NumeroProcessoNE, 
        IEnumerable<Estrutura> estrutura,
        IEnumerable<Fonte> fonte, 
        IEnumerable<Modalidade> modalidade, 
        IEnumerable<Licitacao> licitacao)
        {
            var viewModel = new PesquisaNotaEmpenhoViewModel();
                                  
            viewModel.NumeroProcessoNE = NumeroProcessoNE;
            viewModel.CodigoUnidadeGestora = CodigoUnidadeGestora;
            viewModel.CodigoGestao = CodigoGestao;
            viewModel.CodigoEmpenhoOriginal = CodigoEmpenhoOriginal;
            viewModel.DataEmissao = DataEmissao > default(DateTime) ? DataEmissao.ToShortDateString() : default(string);
            viewModel.NumeroCNPJCPFUGCredor = NumeroCNPJCPFUGCredor;
            viewModel.CodigoGestaoCredor = CodigoGestaoCredor;

            viewModel.CodigoNaturezaNe = CodigoNaturezaNe;
            viewModel.NaturezaListItems = estrutura
                .Select(s => new SelectListItem
                {
                    Text = string.Format("{0} - {1}", s.Natureza.Formatar("0.0.00.00"), s.Fonte),
                    Value = string.Format("{0}{1}", s.Natureza, s.Fonte),
                   //Selected = s.Codigo == Convert.ToInt32(viewModel.CodigoNaturezaNe)
                    Selected = s.Codigo == Convert.ToInt32(viewModel.CodigoNaturezaNe)
                }).ToList();


            viewModel.ModalidadeId = ModalidadeId > 0 ? ModalidadeId.ToString() : default(string);
            viewModel.ModalidadeListItems = modalidade
                .Select(s => new SelectListItem
                {
                    Text = string.Concat(s.Id, "-", s.Descricao),
                    Value = s.Id.ToString(),
                    Selected = s.Id == Convert.ToInt32(viewModel.ModalidadeId)
                })
                .ToList();

            viewModel.LicitacaoId = LicitacaoId;
            viewModel.LicitacaoListItems = licitacao
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value= s.Id.ToString(),
                    Selected = s.Id == LicitacaoId
                }).ToList();

            viewModel.CodigoFonteSiafisico = CodigoFonteSiafisico;
            viewModel.FonteListItems = fonte
                .Select(s => new SelectListItem
                {
                    Text = s.Codigo,
                    Value = s.Codigo.ToString(),
                    Selected = s.Id == Convert.ToInt32(viewModel.CodigoFonteSiafisico)
                }).ToList();

            return viewModel;
        }
    }
}