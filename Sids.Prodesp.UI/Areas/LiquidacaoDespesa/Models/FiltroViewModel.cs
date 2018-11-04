namespace Sids.Prodesp.UI.Areas.LiquidacaoDespesa.Models
{
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class FiltroViewModel
    {

        [Display(Name = "Nº Ap. Subempenho Prodesp")]
        public string NumeroSubempenhoProdesp { get; set; }

        [Display(Name = "Nº Ap. Subempenho SIAFEM/SIAFISICO")]
        public string NumeroSubempenhoSiafemSiafisico { get; set; }

        [Display(Name = "Nº Empenho Prodesp")]
        public string NumeroEmpenhoProdesp { get; set; }

        [Display(Name = "Nº Empenho Siafem")]
        public string NumeroEmpenhoSiafem { get; set; }

        [Display(Name = "Nº do Processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Cod. Aplicação/Obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Tipo Apropriação/Subempenho")]
        public string CenarioSiafemSiafisico { get; set; }
        public IEnumerable<SelectListItem> CenarioTipoListItems { get; set; }


        [Display(Name = "Tipo de Serviço")]
        public string ServicoTipoId { get; set; }
        public IEnumerable<SelectListItem> ServicoTipoListItems { get; set; }



        [Display(Name = "Status SIAFEM/SIAFISICO")]
        public string StatusSiafemSiafisico { get; set; }
        public IEnumerable<SelectListItem> StatusSiafemSiafisicoListItems { get; set; }

        [Display(Name = "Status Prodesp")]
        public string StatusProdesp { get; set; }
        public IEnumerable<SelectListItem> StatusProdespListItems { get; set; }

        [Display(Name = "Nº do Contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Data de Cadastro De")]
        public string DataCadastramentoDe { get; set; }

        [Display(Name = "Data de Cadastro Até")]
        public string DataCadastramentoAte { get; set; }

        [Display(Name = "CNPJ/CPF/UG Credor (SIAFEM / SIAFISICO)")]
        public string NumeroCNPJCPFCredor { get; set; }

        [Display(Name = "Gestão Credor (SIAFEM / SIAFISICO)")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Credor Organização (Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ / CPF (Prodesp)")]
        public string NumeroCNPJCPFFornecedor { get; set; }



        public FiltroViewModel CreateInstance(ILiquidacaoDespesa objModel, IEnumerable<CenarioTipo> cenario, DateTime de, DateTime ate)
        {
            List<CenarioTipo> cenarios;

            if (objModel is SubempenhoCancelamento)
                cenarios = cenario?.Where(w => w.Id > 3).ToList();
            else 
                cenarios = cenario?.Where(w => w.Id < 9).ToList();

            return new FiltroViewModel()
            {
                CenarioSiafemSiafisico = cenarios?.FirstOrDefault(w => w.Id == objModel.CenarioSiafemSiafisico)?.Descricao,
                CenarioTipoListItems = cenarios?
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == objModel.CenarioSiafemSiafisico
                }),

                StatusSiafemSiafisico = Convert.ToString((objModel.TransmitidoSiafem || objModel.TransmitidoSiafisico) && objModel.DataTransmitidoSiafemSiafisico > default(DateTime)).ToLower(),
                StatusSiafemSiafisicoListItems = new SelectListItem[] {
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

                DataCadastramentoDe = null,
                DataCadastramentoAte = null,
                CodigoAplicacaoObra = objModel.CodigoAplicacaoObra
            };
        }

        public FiltroViewModel CreateInstance(IRap entity, IEnumerable<ServicoTipo> servico, DateTime de, DateTime ate)
        {
            var filtro = new FiltroViewModel();

            //filtro.ServicoTipoId = Convert.ToString(servico
            //    .Where(w => w.Id == entity.TipoServicoId && (entity.TipoRap == w.TipoRap || string.IsNullOrEmpty(w.TipoRap)))
            //    .FirstOrDefault()?.Id);

            filtro.ServicoTipoListItems = servico
                .Where(w => entity.TipoRap == w.TipoRap)
                .Select(s => new SelectListItem
                {
                    Text = s.Descricao,
                    Value = s.Id.ToString(),
                    Selected = s.Id == entity.TipoServicoId
                });

            filtro.StatusSiafemSiafisico = Convert.ToString((entity.TransmitidoSiafisico || entity.TransmitidoSiafem) && entity.DataTransmitidoSiafemSiafisico > default(DateTime)).ToLower();
            filtro.StatusSiafemSiafisicoListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            filtro.StatusProdesp = Convert.ToString(entity.TransmitidoProdesp && entity.DataTransmitidoProdesp > default(DateTime)).ToLower();
            filtro.StatusProdespListItems = new SelectListItem[] {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            filtro.DataCadastramentoDe = null;
            filtro.DataCadastramentoAte = null;
            filtro.CodigoAplicacaoObra = entity.CodigoAplicacaoObra;
            filtro.NumeroEmpenhoProdesp = entity.NumeroOriginalProdesp;
            filtro.NumeroEmpenhoSiafem = entity.NumeroOriginalSiafemSiafisico;

            return filtro;
        }
    }
}