using System;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;
using System.Linq;

namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Models
{
    public class ArquivoRemessaFiltroGridViewModel
    {

        #region gridArquivoRemessa properties
      

        public int Id { get; set; }


        public int IdArquivo { get; set; }


        //[Display(Name = "Número da Geração do arquivo")]
        [Display(Name = "NGA")]
        public int? NumeroGeracao { get; set; }


        //[Display(Name = "Orgão")]
        //public string CodigoOrgaoAssinatura { get; set; }

        public IEnumerable<SelectListItem> OrgaoListItems { get; set; }

        [Display(Name = "Código da Conta")]
        public int? CodigoConta { get; set; }


        [Display(Name = "Data da Preparação do Arquivo")]
        public string DataPreparacao { get; set; }

        [Display(Name = "Data para Pagamento")]
        public string DataPagamento { get; set; }

        [Display(Name = "Quantidade de OP")]
        public int? QtOpArquivo { get; set; }

        [Display(Name = "Total")]
        public int? ValorTotal { get; set; }


        [Display(Name = "Status da Transmissão")]
        public string TransmitidoProdesp { get; set; }


        public IList<SelectListItem> lstStatusProdesp { get; set; }

        [Display(Name = "Cancelado")]
        public string Cancelado { get; set; }
        public IEnumerable<SelectListItem> CanceladoListItem { get; set; }


        public bool CadastroCompleto { get; set; }


        [Display(Name = "Data Cadastramento De")]
        public DateTime? DataCadastroDe { get; set; }

        [Display(Name = "Data Cadastramento Até")]
        public DateTime? DataCadastroAte { get; set; }

        [Display(Name = "Data da Preparação do Arquivo")]
        public string DataTrasmitido { get; set; }

        [Display(Name = "Orgão")]
          public int RegionalId { get; set; }


        public string MensagemProdesp { get; set; }



        #endregion



        public ArquivoRemessaFiltroGridViewModel CreateNewInstance(ConfirmacaoPagamento entity)
        {
            return new ArquivoRemessaFiltroGridViewModel();
        }

        public ArquivoRemessaFiltroGridViewModel CreateNewInstance(ConfirmacaoPagamentoFiltroViewModel entity)
        {
            return new ArquivoRemessaFiltroGridViewModel();    
        }

        public ArquivoRemessaFiltroGridViewModel CreateNewInstance(ArquivoRemessa entity)
        {
            return new ArquivoRemessaFiltroGridViewModel();
        }

        public ArquivoRemessaFiltroGridViewModel CreateNewInstance(ArquivoRemessaFiltroViewModel entity)
        {
            return new ArquivoRemessaFiltroGridViewModel();
        }

        public ArquivoRemessaFiltroGridViewModel CreateInstance(ArquivoRemessa entity)
        {

            ArquivoRemessaFiltroGridViewModel filtro = new ArquivoRemessaFiltroGridViewModel();

            filtro.Id = entity.Id;

            filtro.RegionalId = entity.RegionalId > 0 ? entity.RegionalId : default(int);

            filtro.CodigoConta = entity.CodigoConta;
            filtro.NumeroGeracao = entity.NumeroGeracao;
            filtro.CadastroCompleto = entity.CadastroCompleto;

            filtro.DataCadastroDe = null;
            filtro.DataCadastroAte = null;

            //filtro.TransmitidoProdesp = entity.TransmitidoProdesp         ;
            filtro.TransmitidoProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            filtro.MensagemProdesp = entity.MensagemServicoProdesp;

            filtro.lstStatusProdesp = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };


            filtro.Cancelado = entity.Cancelado == true ? "Sim" : "Não";
            filtro.CanceladoListItem = new SelectListItem[] {
                    new SelectListItem { Text = "Sim", Value = "1" },
                    new SelectListItem { Text = "Não", Value = "0" }
                };

            filtro.DataPreparacao = entity.DataPreparacao.HasValue ? entity.DataPreparacao.Value.ToShortDateString() : null;
            filtro.DataPagamento = entity.DataPagamento.HasValue ? entity.DataPagamento.Value.ToShortDateString() : null;
            filtro.DataTrasmitido = entity.DataTrasmitido.HasValue ? entity.DataTrasmitido.Value.ToShortDateString() : null;
            filtro.QtOpArquivo = entity.QtOpArquivo;
            filtro.ValorTotal = entity.ValorTotal;


            return filtro;



        }
        public ArquivoRemessaFiltroGridViewModel CreateInstance(ConfirmacaoPagamento entity)
        {

            ArquivoRemessaFiltroGridViewModel filtro = new ArquivoRemessaFiltroGridViewModel();

            filtro.Id = entity.Id;

            filtro.RegionalId = entity.RegionalId > 0 ? entity.RegionalId : default(int);
            

            filtro.DataCadastroDe = null;
            filtro.DataCadastroAte = null;
            
            filtro.TransmitidoProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            filtro.MensagemProdesp = entity.MensagemServicoProdesp;

            filtro.lstStatusProdesp = new List<SelectListItem> {
                    new SelectListItem { Text = "Sucesso", Value = "S" },
                    new SelectListItem { Text = "Erro", Value = "E" },
                    new SelectListItem { Text = "Não transmitido", Value = "N" }
                };

            
            filtro.CanceladoListItem = new SelectListItem[] {
                    new SelectListItem { Text = "Sim", Value = "1" },
                    new SelectListItem { Text = "Não", Value = "0" }
                };

            filtro.DataPreparacao = entity.DataPreparacao.HasValue ? entity.DataPreparacao.Value.ToShortDateString() : null;


            return filtro;



        }
    }
}