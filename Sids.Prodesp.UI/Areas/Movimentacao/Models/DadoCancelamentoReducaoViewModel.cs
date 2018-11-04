using Sids.Prodesp.Model.Entity.Movimentacao;
using System.ComponentModel.DataAnnotations;

namespace Sids.Prodesp.UI.Areas.Movimentacao.Models
{

    public class DadoCancelamentoReducaoViewModel
    {
        [Display(Name = "Seq")]
        public int NrSequencia { get; set; }

        [Display(Name = "N° Redução")]
        public string NrSuplementacaoReducao { get; set; }

        [Display(Name = "N° Canc")]
        public string NrNotaCancelamento { get; set; }

        [Display(Name = "UG Emitente")]
        public string UnidadeGestoraEmitente { get; set; }

        [Display(Name = "Gestão Emitente")]
        public string GestaoEmitente { get; private set; }

        [Display(Name = "UG Favorecida")]
        public string UnidadeGestoraFavorecida { get; set; }

        [Display(Name = "Órgão")]
        public string NrOrgao { get; set; }

        [Display(Name = "Fonte")]
        public string Fonte { get; set; }

        [Display(Name = "Categoria de Gasto")]
        public string CategoriaGasto { get; set; }

        [Display(Name = "Total")]
        public decimal Valor { get; set; }

        [Display(Name = "Status Prodesp")]
        public string TransmitidoProdesp { get; set; }

        [Display(Name = "Status Prodesp")]
        public string MensagemProdesp { get; set; }

        [Display(Name = "Status Siafem")]
        public string TransmitidoSiafem { get; set; }

        [Display(Name = "Status Siafem")]
        public string MensagemSiafem { get; set; }

        public DadoCancelamentoReducaoViewModel CreateInstance(MovimentacaoCancelamento objModel, string ug, string gestao)
        {
            DadoCancelamentoReducaoViewModel dado = new DadoCancelamentoReducaoViewModel();

            dado.NrSequencia = objModel.NrSequencia;
            dado.NrNotaCancelamento = objModel.NumeroSiafem;
            dado.UnidadeGestoraEmitente = ug;
            dado.GestaoEmitente = gestao;
            dado.UnidadeGestoraFavorecida = objModel.UnidadeGestoraFavorecida;
            dado.Fonte = objModel.IdFonte.ToString().PadLeft(3, '0');
            dado.CategoriaGasto = objModel.CategoriaGasto;
            dado.Valor = objModel.Valor;

            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";

            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;

            return dado;
        }
        public DadoCancelamentoReducaoViewModel CreateInstance(MovimentacaoDistribuicao objModel, string ug, string gestao)
        {
            DadoCancelamentoReducaoViewModel dado = new DadoCancelamentoReducaoViewModel();

            dado.NrSequencia = objModel.NrSequencia;
            dado.NrNotaCancelamento = objModel.NumeroSiafem;
            dado.UnidadeGestoraEmitente = ug;
            dado.GestaoEmitente = gestao;
            dado.UnidadeGestoraFavorecida = objModel.UnidadeGestoraFavorecida;
            dado.Fonte = objModel.IdFonte?.ToString();
            dado.CategoriaGasto = objModel.CategoriaGasto;
            dado.Valor = objModel.Valor;

            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";

            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;

            return dado;
        }

        public DadoCancelamentoReducaoViewModel CreateInstance(MovimentacaoReducaoSuplementacao objModel, string ug, string gestao)
        {
            DadoCancelamentoReducaoViewModel dado = new DadoCancelamentoReducaoViewModel();

            dado.NrSequencia = objModel.NrSequencia;
            dado.UnidadeGestoraEmitente = ug;
            dado.GestaoEmitente = gestao;
            dado.Fonte = objModel.IdFonte.ToString().PadLeft(3, '0');
            dado.Valor = objModel.Valor;

            dado.NrOrgao = objModel.NrOrgao;

            dado.NrSuplementacaoReducao = objModel.NrSuplementacaoReducao;

            dado.TransmitidoSiafem = string.IsNullOrEmpty(objModel.StatusSiafem) || objModel.StatusSiafem.Equals("N") ? "Não Transmitido" : objModel.StatusSiafem.Equals("E") ? "Erro" : "Sucesso";
            dado.TransmitidoProdesp = string.IsNullOrEmpty(objModel.StatusProdesp) || objModel.StatusProdesp.Equals("N") ? "Não Transmitido" : objModel.StatusProdesp.Equals("E") ? "Erro" : "Sucesso";

            dado.MensagemProdesp = objModel.MensagemProdesp;
            dado.MensagemSiafem = objModel.MensagemSiafem;

            return dado;
        }
    }
}