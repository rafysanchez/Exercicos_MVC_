namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Extension;
    using Model.Interface.Empenho;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class FiltroViewModel
    {
        [Display(Name = "Nº Empenho Prodesp")]
        public string NumeroEmpenhoProdesp { get; set; }

        [Display(Name = "Nº Empenho SIAFEM")]
        public string NumeroEmpenhoSiafem { get; set; }

        [Display(Name = "Nº Empenho SIAFISICO")]
        public string NumeroEmpenhoSiafisico { get; set; }

        [Display(Name = "Nº Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Cód. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }


        [Display(Name = "Ano")]
        public string NumeroAnoExercicio { get; set; }
        public IEnumerable<SelectListItem> NumeroAnoExercicioListItems { get; set; }

        [Display(Name = "Órgão")]
        public string RegionalId { get; set; }
        public IEnumerable<SelectListItem> RegionalListItems { get; set; }

        [Display(Name = "PTRES")]
        public string CodigoPtres { get; set; }
        public IEnumerable<SelectListItem> CodigoPtresListItems { get; set; }


        [Display(Name = "CFP")]
        public string ProgramaId { get; set; }
        public IEnumerable<SelectListItem> ProgramaListItems { get; set; }

        [Display(Name = "CED")]
        public string NaturezaId { get; set; }
        public IEnumerable<SelectListItem> NaturezaListItems { get; set; }

        [Display(Name = "Cód. Natureza (Item)")]
        public string CodigoNaturezaItem { get; set; }

        [Display(Name = "Origem do Recurso")]
        public string FonteId { get; set; }
        public IEnumerable<SelectListItem> FonteListItems { get; set; }

        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }
        public IEnumerable<SelectListItem> StatusProdespListItems { get; set; }

        public IEnumerable<SelectListItem> StatusSiafemListItems { get; set; }
        
        public IEnumerable<SelectListItem> StatusSiafisicoListItems { get; set; }


        [Display(Name = "Status SIAFEM / SIAFISICO")]
        public string StatusSiafemSiafisico { get; set; }


        [Display(Name = "Número do Contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Licitação")]
        public string LicitacaoId { get; set; }
        public IEnumerable<SelectListItem> LicitacaoListItems { get; set; }

        [Display(Name = "Data de Cadastramento De")]
        public string DataCadastramentoDe { get; set; }

        [Display(Name = "Data de Cadastramento Até")]
        public string DataCadastramentoAte { get; set; }

        [Display(Name = "CNPJ / CPF / UG Credor")]
        public string NumeroCNPJCPFUGCredor { get; set; }

        [Display(Name = "Gestão Credor")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Credor Organização")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ / CPF")]
        public string NumeroCNPJCPFFornecedor { get; set; }


        public FiltroViewModel CreateInstance(IEmpenho objModel, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<Estrutura> estrutura, IEnumerable<Fonte> fonte, IEnumerable<Licitacao> licitacao, IEnumerable<int> anos)
        {
            var _programa = programa.Where(x => x.Ano == (objModel.NumeroAnoExercicio == 0? DateTime.Now.Year: objModel.NumeroAnoExercicio)).ToList();
            var _ptres = _programa.Where(w => w.Codigo == objModel.ProgramaId).SingleOrDefault()?.Ptres;
            var _natureza = estrutura.Where(x => _programa.Select(y => y.Codigo).ToList().Contains(x.Programa.Value) && (x.Programa == objModel.ProgramaId || objModel.ProgramaId == 0)).ToList();
            var _naturezaId = _natureza.Where(w => w.Codigo == objModel.NaturezaId).SingleOrDefault()?.Codigo;
            
            return new FiltroViewModel()
            {
                NumeroEmpenhoProdesp = objModel.NumeroEmpenhoProdesp,
                NumeroEmpenhoSiafem = objModel.NumeroEmpenhoSiafem,
                NumeroEmpenhoSiafisico = objModel.NumeroEmpenhoSiafisico,
                NumeroProcesso = objModel.NumeroProcesso,

                NumeroAnoExercicio = objModel.NumeroAnoExercicio > 0 ? objModel.NumeroAnoExercicio.ToString() : default(string),
                NumeroAnoExercicioListItems = anos
                .Select(s => new SelectListItem
                {
                    Text = s.ToString(),
                    Value = s.ToString(),
                    Selected = s == objModel.NumeroAnoExercicio
                }),

                RegionalId = objModel.RegionalId > 0 ? objModel.RegionalId.ToString() : default(string),
                RegionalListItems = regional.Where(x => x.Id > 1)
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.RegionalId
                }),

                ProgramaId = objModel.ProgramaId > 0 ? objModel.ProgramaId.ToString() : default(string),
                ProgramaListItems = _programa.OrderBy(x => x.Cfp)
                .Select(s => new SelectListItem
                {
                    Text = string.Format("{0} {1}", s.Cfp.Formatar("00.000.0000.0000"), s.Descricao),
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == objModel.ProgramaId
                }),

                CodigoPtres = _ptres,
                CodigoPtresListItems = _programa.OrderBy(x => x.Ptres)
                .Select(s => new SelectListItem
                {
                    Text = s.Ptres,
                    Value = s.Ptres,
                    Selected = s.Ptres == _ptres
                }),

                NaturezaId = Convert.ToString(_naturezaId),
                NaturezaListItems = _natureza.OrderBy(x => x.Natureza)
                .Select(s => new SelectListItem
                {
                    Text = string.Format("{0} - {1}", s.Natureza.Formatar("0.0.00.00"), s.Fonte),
                    Value = s.Codigo.ToString(),
                    Selected = s.Codigo == _naturezaId
                }),

                FonteId = objModel.FonteId > 0 ? objModel.FonteId.ToString() : default(string),
                FonteListItems = fonte.OrderBy(x => x.Codigo)
                .Select(s => new SelectListItem
                {
                    Text = s.Codigo,
                    Value = s.Id.ToString(),
                    Selected = s.Codigo == (objModel.FonteId > 0 ? objModel.FonteId.ToString() : default(string))
                }),

                StatusSiafemSiafisico = Convert.ToString(objModel.StatusSiafemSiafisico == "S").ToLower(),
                StatusSiafemListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },
                

                StatusProdesp = Convert.ToString(objModel.TransmitidoProdesp && objModel.DataTransmitidoProdesp > default(DateTime)).ToLower(),
                StatusProdespListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                },

                NumeroContrato = objModel.NumeroContrato,

                LicitacaoId = objModel.LicitacaoId,
                LicitacaoListItems = licitacao
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.LicitacaoId
                }),

                DataCadastramentoDe = null,
                DataCadastramentoAte = null,
                NumeroCNPJCPFUGCredor = objModel.NumeroCNPJCPFUGCredor,
                CodigoGestaoCredor = objModel.CodigoGestaoCredor,
                CodigoCredorOrganizacao = objModel.CodigoCredorOrganizacao > 0 ? objModel.CodigoCredorOrganizacao.ToString() : default(string),
                NumeroCNPJCPFFornecedor = objModel.NumeroCNPJCPFFornecedor
            };
        }

        public FiltroViewModel CreateInstance(Empenho objModel, IEnumerable<Regional> regional, IEnumerable<Programa> programa, IEnumerable<Estrutura> estrutura, IEnumerable<Fonte> fonte, IEnumerable<Licitacao> licitacao, IEnumerable<int> anos)
        {
            var filtro = CreateInstance((IEmpenho)objModel, regional, programa, estrutura, fonte, licitacao, anos);
            filtro.CodigoAplicacaoObra = objModel.CodigoAplicacaoObra;

            return filtro;
        }
    }
}