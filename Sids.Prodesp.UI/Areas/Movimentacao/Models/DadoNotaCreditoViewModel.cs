using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Movimentacao;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{
    using Model.Interface.Movimentacao;
    using System.ComponentModel.DataAnnotations;

    public class DadoNotaCreditoViewModel
    {
        public string Id { get; set; }

        public int IdNotaCredito { get; set; }

        public int IdMovimentacao { get; set; }

        public int NrAgrupamento { get; set; }


        public int NrSequencia { get; set; }


        [Display(Name = "N° NC")]
        public string NrNotaCredito { get; set; }

        public string Ugo { get; set; }

        [Display(Name = "UG Emitente")]
        public string UnidadeGestoraEmitente { get; set; }

        [Display(Name = "Gestão Emitente")]
        public string IdGestaoEmitente { get; set; }

        [Display(Name = "UG Favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Gestão Favorecida")]
        public string IdGestaoFavorecida { get; set; }

        [Display(Name = "Órgão")]
        public string NrOrgao { get; set; }

        [Display(Name = "Fonte")]
        public string Fonte { get; set; }

        [Display(Name = "Categoria de Gasto")]
        public string CategoriaGasto { get; set; }

        [Display(Name = "Total")]
        public int ValorTotal { get; set; }

        public string EventoNC { get; set; }
                
        public int ProgramaId { get; set; }

        public int NaturezaId { get; set; }

        public int IdTipoDocumento { get; set; }
        

        public string Uo { get; set; }

        public string FonteRecurso { get; set; }
        

        public int IdEstrutura { get; set; }

        [Display(Name = "Status Prodesp")]
        public string TransmitidoProdesp { get; set; }

        [Display(Name = "Status Siafem")]
        public string TransmitidoSiafem { get; set; }

        public string MensagemProdesp { get; set; }
        

        public string MensagemSiafem { get; set; }
        

        public string Observacao { get; set; }

        public string Observacao2 { get; set; }

        public string Observacao3 { get; set; }

        public decimal Valor { get; set; }

        public string CanDis { get; set; }
        
        public DadoNotaCreditoViewModel CreateInstance(MovimentacaoNotaDeCredito objModel)
        {
            DadoNotaCreditoViewModel dado = new DadoNotaCreditoViewModel();
            dado.IdNotaCredito = objModel.Id > 0 ? objModel.Id : default(int);
            dado.NrNotaCredito = objModel.NumeroSiafem;
            dado.UnidadeGestoraEmitente = objModel.UnidadeGestoraEmitente;
            dado.IdGestaoEmitente = objModel.GestaoEmitente;
            dado.UnidadeGestoraFavorecida = objModel.UnidadeGestoraFavorecida;
            dado.Fonte = objModel.IdFonte.ToString().PadLeft(3, '0');
            dado.Valor = objModel.Valor;

            dado.IdMovimentacao = objModel.IdMovimentacao;
            dado.NrAgrupamento = objModel.NrAgrupamento;
            dado.NrSequencia = objModel.NrSequencia;

            dado.IdEstrutura = objModel.IdEstrutura;
            dado.Ugo = objModel.Ugo;

            dado.Uo = objModel.Uo;
            dado.FonteRecurso = objModel.FonteRecurso;

            dado.Observacao = objModel.Observacao;
            dado.Observacao2 = objModel.Observacao2;
            dado.Observacao3 = objModel.Observacao3;
            dado.IdGestaoFavorecida = objModel.GestaoFavorecida;

            dado.ProgramaId = objModel.IdPrograma;
            dado.NaturezaId = objModel.IdEstrutura;
            dado.IdTipoDocumento = 2;

            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;
            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";
            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            dado.CanDis = objModel.CanDis;

            return dado;
        }
    }
}