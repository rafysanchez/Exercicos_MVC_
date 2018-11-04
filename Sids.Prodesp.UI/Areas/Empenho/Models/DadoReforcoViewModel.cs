
namespace Sids.Prodesp.UI.Areas.Empenho.Models
{
    using Model.Entity.Configuracao;
    using Model.Interface.Empenho;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class DadoReforcoViewModel
    {
        [Display(Name = "Evento")]
        public string CodigoEvento { get; set; }

        [Display(Name = "UGF")]
        public string CodigoUnidadeGestoraFornecedora { get; set; }

        [Display(Name = "Gestão Fornecedora")]
        public string CodigoGestaoFornecedora { get; set; }

        [Display(Name = "Unidade de Fornecimento")]
        public string CodigoUnidadeFornecimento { get; set; }

        [Display(Name = "UO")]
        public string CodigoUO { get; set; }

        [Display(Name = "Credor - Organização (Prodesp)")]
        public string CodigoCredorOrganizacao { get; set; }

        [Display(Name = "CNPJ / CPF (Prodesp)")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        [Display(Name = "Código NATUR (Item)")]
        public string CodigoNaturezaItem { get; set; }

        [Display(Name = "Local de Entrega SIAFEM")]
        public string DescricaoLocalEntregaSiafem { get; set; }

        [Display(Name = "Município")]
        public string CodigoMunicipio { get; set; }

        [Display(Name = "Acordo")]
        public string DescricaoAcordo { get; set; }
        
        [Display(Name = "Cód. Aplicação")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Contabilizar como BEC?")]
        public bool ContBec { get; set; }

        [Display(Name = "Destino do Recurso")]
        public string DestinoId { get; set; }
        public IEnumerable<SelectListItem> DestinoListItems { get; set; }
        public IEnumerable<SelectListItem> EventoListItems { get; set; }


        public DadoReforcoViewModel CreateInstance(IEmpenho objModel, IEnumerable<Destino> destino)
        {
            return new DadoReforcoViewModel
            {
                CodigoEvento = objModel.CodigoEvento > 0 ? objModel.CodigoEvento.ToString() : default(string),
                CodigoCredorOrganizacao = objModel.CodigoCredorOrganizacao > 0 ? objModel.CodigoCredorOrganizacao.ToString() : default(string),
                NumeroCNPJCPFFornecedor = objModel.NumeroCNPJCPFFornecedor,
                CodigoUO = objModel.CodigoUO > 0 ? objModel.CodigoUO.ToString() : default(string),
                CodigoUnidadeGestoraFornecedora = objModel.CodigoUnidadeGestoraFornecedora,
                CodigoGestaoFornecedora = objModel.CodigoGestaoFornecedora,
                CodigoNaturezaItem = objModel.CodigoNaturezaItem,
                DescricaoLocalEntregaSiafem = objModel.DescricaoLocalEntregaSiafem,
                CodigoMunicipio = objModel.CodigoMunicipio,
                DescricaoAcordo = objModel.DescricaoAcordo,
                CodigoAplicacaoObra = objModel.CodigoAplicacaoObra,
                CodigoUnidadeFornecimento = objModel.CodigoUnidadeFornecimento,
                ContBec = objModel.ContBec,

                DestinoId = objModel.DestinoId,
                DestinoListItems = destino.Select(s => new SelectListItem
                {
                    Text = s.Descricao.ToString(),
                    Value = s.Codigo,
                    Selected = s.Codigo == objModel.DestinoId
                }),

                EventoListItems = new List<SelectListItem>
                {
                    new SelectListItem {
                    Text = "400052-­Reforço de empenho com Reserva",
                    Value = "400052",
                    Selected = "400052" == objModel.CodigoEvento.ToString()
                    },

                    new SelectListItem {
                    Text = "400092-Reforço de empenho sem reserva",
                    Value = "400092",
                    Selected = "400092" == objModel.CodigoEvento.ToString()
                    }
                }
            };
        }
    }
}