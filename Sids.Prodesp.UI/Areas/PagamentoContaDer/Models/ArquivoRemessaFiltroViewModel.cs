using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.Seguranca;
using System.Linq;

namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Models
{
    public class ArquivoRemessaFiltroViewModel
    {
        #region Propriedades

        public int Id { get; set; }


        public int IdArquivo { get; set; }


        //[Display(Name = "Número da Geração do arquivo")]
        [Display(Name = "Número da Geração do Arquivo")]
        public int? NumeroGeracao { get; set; }


        //[Display(Name = "Orgão")]
        //public string CodigoOrgaoAssinatura { get; set; }

        public IEnumerable<SelectListItem> OrgaoListItems { get; set; }

        [Display(Name = "Código da Conta")]
        public int? CodigoConta { get; set; }


        [Display(Name = "Data da Preparação do Arquivo")]
        public string DataPreparacao { get; set; }

        public string DataTransmitido { get; set; }

        [Display(Name = "Data para Pagamento")]
        public DateTime DataPagamento { get; set; }

        [Display(Name = "Quantidade de OP")]
        public int? QtOpArquivo { get; set; }

        [Display(Name = "Total")]
        public int? ValorTotal { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime? DataCadastro { get; set; }


        [Display(Name = "Data Cadastramento De")]
        public DateTime? DataCadastroDe { get; set; }

        [Display(Name = "Data Cadastramento Até")]
        public DateTime? DataCadastroAte { get; set; }

        [Display(Name = "Status da Transmissão")]
        public string TransmitidoProdesp { get; set; }


       
        public IList<SelectListItem> lstStatusProdesp { get; set; }

        [Display(Name = "Cancelado")]
        public string Cancelado { get; set; }
        public IEnumerable<SelectListItem> CanceladoListItem { get; set; }


        public bool CadastroCompleto { get; set; }

        public string DataTrasmitido { get; set; }

        [Display(Name = "Orgão")]
        public int RegionalId { get; set; }


        #endregion

        #region Singleton
        public ArquivoRemessaFiltroViewModel CreateInstance()
        {
            ArquivoRemessaFiltroViewModel filtro = new ArquivoRemessaFiltroViewModel();
            return filtro;
        }

       
        internal ArquivoRemessaFiltroViewModel CreateInstance(ArquivoRemessa entity, IEnumerable<Regional> regional, DateTime de, DateTime ate)
        {

            ArquivoRemessaFiltroViewModel filtro = new ArquivoRemessaFiltroViewModel();
            //  filtro.Id = entity.Id;


             //filtro.CodigoOrgaoAssinatura = entity.CodigoOrgaoAssinatura > 0 ? entity.CodigoOrgaoAssinatura.ToString() : default(string);
            filtro.OrgaoListItems = regional.Where(x => x.Id > 1).ToList()
          .Select(s => new SelectListItem
          {
              Text = s.Descricao,
              Value = s.Id.ToString(),
              Selected = s.Id == entity.RegionalId
          });


            filtro.Id = entity.Id;

            filtro.NumeroGeracao = entity.NumeroGeracao;


            filtro.RegionalId = entity.RegionalId;

            filtro.DataCadastroDe = null;
            filtro.DataCadastroAte = null;

            filtro.CodigoConta = entity.CodigoConta;

            filtro.DataPreparacao = entity.DataPreparacao.HasValue ? entity.DataPreparacao.ToString() : null;
            filtro.DataTrasmitido = entity.DataTrasmitido.HasValue ? entity.DataTrasmitido.ToString() : null;
            filtro.QtOpArquivo = entity.QtOpArquivo;
            filtro.ValorTotal = entity.ValorTotal;

            filtro.DataCadastro = entity.DataCadastro;

            filtro.TransmitidoProdesp = entity.StatusProdesp;
            filtro.lstStatusProdesp = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };


            filtro.Cancelado = entity.Cancelado.ToString();
            filtro.CanceladoListItem = new SelectListItem[] {
                    new SelectListItem { Text = "Sim", Value = "1" },
                    new SelectListItem { Text = "Não", Value = "0" }
                };



            return filtro;
        }
        #endregion

    }
}