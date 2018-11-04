using System;
using Sids.Prodesp.Model.Entity.Movimentacao;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;
using System.Linq;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    public class FiltroGridViewModel
    {
        #region gridMovimentacao properties
        public int Id { get; set; }
        public int IdMovimentacao { get; set; }

        public int NumSequencia { get; set; }

        [Display(Name = "UG Emitente")]
        public string UnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string idGestao { get; set; }

        [Display(Name = "N° Agrupamento")]
        public int? numAgrupamento { get; set; }

        [Display(Name = "N° Documento")]
        public string numDocumento { get; set; }
        

        [Display(Name = "UG Favorecido")]
        public string ugFavorecido { get; set; }

        [Display(Name = "Gestão Favorecida")]
        public string idGestaoFavorecida { get; set; }

        [Display(Name = "C.F.P. (Programa de Trabalho)")]
        public int? idCFP { get; set; }

        [Display(Name = "C.E.D. (Natureza)")]
        public int? idCED { get; set; }

        [Display(Name = "C.F.P. (Programa de Trabalho)")]
        public string cdCFP { get; set; }

        [Display(Name = "C.E.D. (Natureza)")]
        public string cdCED { get; set; }

        [Display(Name = "Status Prodesp")]
        public string TransmitidoProdesp { get; set; }
        [Display(Name = "Status Siafem")]
        public string TransmitidoSiafem { get; set; }
    
        public string MensagemProdesp { get; set; }

        public string MensagemSiafem { get; set; }
        
        [Display(Name = "Data Cadastramento De")]
        public DateTime? DataCadastroDe { get; set; }

        [Display(Name = "Data Cadastramento Até")]
        public DateTime? DataCadastroAte { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime? DataCadastro{ get; set; }


        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        public int? tipoDocumento { get; set; }

        //public int? tipoMovimentacao { get; set; }

        //public bool CadastroCompleto { get; set; }

        public bool PodeExcluir { get; set; }

        public bool PodeAlterar { get; set; }
        
        public string DescDocumento { get; set; }

        //public int? FuncionalidadeAtual { get; set; }

        #endregion


        public FiltroGridViewModel CreateInstance(MovimentacaoOrcamentaria entity)
        {           
            FiltroGridViewModel filtro = new FiltroGridViewModel();

            filtro.Id = entity.Id;
            filtro.IdMovimentacao = entity.IdMovimentacao;

            filtro.NumSequencia = entity.nr_sequencia;

            filtro.UnidadeGestora = entity.UnidadeGestoraEmitente;
            filtro.idGestao = entity.GestaoEmitente;

            filtro.ugFavorecido = entity.UGFavorecida;
            filtro.idGestaoFavorecida = entity.IdGestaoFavorecida;
           

            filtro.numAgrupamento = entity.NrAgrupamento;
            filtro.numDocumento = entity.NumSiafem;

            filtro.tipoDocumento = entity.IdTipoDocumento;

            filtro.DescDocumento = entity.DescDocumento;

            filtro.cdCED = entity.CdMatureza;
            filtro.cdCFP = entity.CdEstrutura;
            filtro.DataCadastro = entity.DataCadastro;

            filtro.MensagemProdesp = entity.MensagemProdesp;
            filtro.MensagemSiafem = entity.MensagemSiafem;

            filtro.DataCadastroDe = null;
            filtro.DataCadastroAte = null;

            filtro.TransmitidoSiafem= string.IsNullOrEmpty(entity.StatusSiafem) || entity.StatusSiafem.Equals("N") ? "Não Transmitido" : entity.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";

            filtro.TransmitidoProdesp = string.IsNullOrEmpty(entity.StatusProdesp) || entity.StatusProdesp.Equals("N") ? "Não Transmitido" : entity.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            filtro.PodeExcluir = entity.PodeExcluir;

            filtro.PodeAlterar = entity.PodeAlterar;

            filtro.Valor = entity.Valor;

            return filtro;            
        }
    }
}