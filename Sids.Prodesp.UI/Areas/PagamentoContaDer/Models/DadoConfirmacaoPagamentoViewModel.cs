using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Models
{
    public class DadoConfirmacaoPagamentoViewModel
    {
        public DadoConfirmacaoPagamentoViewModel()
        {
            Items = new List<DadoConfirmacaoPagamentoItemViewModel>();
        }

        public DadoConfirmacaoPagamentoViewModel(ConfirmacaoPagamento entity)
        {
            Id = entity.Id;
            IdTipoDocumento = entity.IdTipoDocumento;
            IdTipoPagamento = entity.TipoPagamento;
            MensagemServicoProdesp = entity.MensagemServicoProdesp;
            //NumeroConta = entity.NumeroConta;
            //NumeroDocumento = entity.NumeroDocumento;
            NumOP = entity.NumeroOP;
            RegionalId = entity.RegionalId;
            StatusProdesp = entity.StatusProdesp;
            TransmitidoProdesp = entity.TransmitidoProdesp;
            AnoReferencia = entity.AnoReferencia;
            DataTransmitidoProdesp = entity.DataTransmitidoProdesp;
            DataCadastro = entity.DataCadastro;
            DataConfirmacao = entity.DataConfirmacao;
            DataModificacao = entity.DataModificacao;
            //DataPreparacao = entity.DataPreparacao;
            // TransmitirSiafem = entity.TransmitirSiafem;
            ProdespDescricao = entity.MensagemServicoProdesp;
            ProdespStatus = entity.StatusProdesp;
            ProdespTransmitidoEm = entity.DataTransmitidoProdesp;
            ProdespTransmitido = entity.TransmitidoProdesp;
        }

        public ConfirmacaoPagamento ToEntity()
        {
            var retorno = new ConfirmacaoPagamento();
            retorno.Id = Id;
            retorno.IdTipoDocumento = IdTipoDocumento;
            retorno.TipoPagamento = IdTipoPagamento;
            retorno.MensagemServicoProdesp = MensagemServicoProdesp;
            //retorno.NumeroConta = NumeroConta;
            //retorno.NumeroDocumento = NumeroDocumento;
            retorno.RegionalId = RegionalId;
            retorno.StatusProdesp = StatusProdesp;
            retorno.TransmitidoProdesp = TransmitidoProdesp;
            retorno.AnoReferencia = AnoReferencia;
            retorno.DataTransmitidoProdesp = DataTransmitidoProdesp;
            retorno.DataCadastro = DataCadastro;
            retorno.DataConfirmacao = DataConfirmacao;
            retorno.DataModificacao = DataModificacao;
            //retorno.DataPreparacao = DataPreparacao;
            //retorno.TransmitirSiafem = TransmitirSiafem;

            var novosItens = new List<ConfirmacaoPagamentoItem>();

            foreach (DadoConfirmacaoPagamentoItemViewModel item in Items)
            {
                var novoItem = item.ToEntity();
                novosItens.Add(novoItem);                
            }

            retorno.Items = novosItens;

            return retorno;
        }

        public int Id { get; set; }
        public int? AnoReferencia { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumOP { get; set; }
        public int? IdTipoPagamento { get; set; }
        public DateTime? DataConfirmacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public string NumeroConta { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataModificacao { get; set; }
        public bool TransmitidoProdesp { get; set; }
        public DateTime? DataTransmitidoProdesp { get; set; }
        public string MensagemServicoProdesp { get; set; }
        public string StatusProdesp { get; set; }
        public int RegionalId { get; set; }
        public bool? ProdespTransmitido { get; set; }
        public string ProdespStatus { get; set; }
        public DateTime? ProdespTransmitidoEm { get; set; }
        public string ProdespDescricao { get; set; }

        /// <summary>
        /// Trasmitir para Siafem
        /// </summary>
        [Display(Name = "Transmitir Siafem")]
        public bool TransmitirSiafem { get; set; }

        public List<DadoConfirmacaoPagamentoItemViewModel> Items { get; set; }

        public int? TipoPagamento { get; set; }
    }

    #region ConfirmacaoPagamentoItem

    public class DadoConfirmacaoPagamentoItemViewModel
    {
        public DadoConfirmacaoPagamentoItemViewModel() { }
        public int Id { get; set; }
        public int? IdConfirmacaoPagamento { get; set; }
        public DateTime? DataConfirmacaoItem { get; set; }
        public int IdRegional { get; set; }
        public int? IdReclassificacaoRetencao { get; set; }
        public int? IdOrigem { get; set; }
        public int? IdTipoDespesa { get; set; }
        public DateTime? DtVencimento { get; set; }
        public string NumeroContrato { get; set; }
        public int? CdObra { get; set; }
        public string NrOp { get; set; }
        public string NumeroBancoPagador { get; set; }
        public string NumeroAgenciaPagador { get; set; }
        public string NrContaPagador { get; set; }
        public string NumeroFonteSiafem { get; set; }
        public string NrEmprenho { get; set; }
        public int? NrProcesso { get; set; }
        public int? NrNotaFiscal { get; set; }
        public string NrDocumento { get; set; }
        public decimal? ValorDocumento { get; set; }
        public int? NrNaturezaDespesa { get; set; }
        public int? CdCredorOrganizacao { get; set; }
        public string NrCnpjCpfUgCredor { get; set; }
        public decimal? ValorTotalConfirmado { get; set; }
        public bool? FlTransmissao { get; set; }
        public DateTime? DataTransmissao { get; set; }
        public string Referencia { get; set; }
        #region Reclassificação e Retenção
        public string NomeReduzidoCredor { get; set; }
        public string CodigoBancoCredor { get; set; }
        public string NumeroAgenciaCredor { get; set; }
        public string NumeroContaCredor { get; set; }
        public string AnoMedicao { get; set; }
        public string MesMedicao { get; set; }
        public string CodigoGestaoCredor { get; set; }
        public string CodigoEvento { get; set; }
        public string CodigoInscricao { get; set; }
        public string CodigoClassificacao { get; set; }
        public string Observacao { get; set; }
        #endregion

        public DadoConfirmacaoPagamentoItemViewModel(ConfirmacaoPagamentoItem entity)
        {
            this.Id = entity.Id;
            this.IdConfirmacaoPagamento = entity.IdConfirmacaoPagamento;
            this.DataConfirmacaoItem = entity.DataConfirmacaoItem;
            this.IdTipoDespesa = entity.IdTipoDespesa;
            this.IdOrigem = entity.IdOrigem;
            this.IdReclassificacaoRetencao = entity.IdReclassificacaoRetencao;
            this.IdRegional = entity.IdRegional;
            this.NumeroAgenciaPagador = entity.NumeroAgenciaPagador;
            this.NumeroBancoPagador = entity.NumeroBancoPagador;
            this.AnoMedicao = entity.AnoMedicao;
            this.MesMedicao = entity.MesMedicao;
            this.CodigoGestaoCredor = entity.CodigoGestaoCredor;
            this.CodigoEvento = entity.CodigoEvento;
            this.CodigoInscricao = entity.CodigoInscricao;
            this.CodigoClassificacao = entity.CodigoClassificacao;
            this.NumeroFonteSiafem = entity.NumeroFonteSiafem;
            this.ValorDocumento = entity.ValorDocumento;
            this.Observacao = entity.Observacao;
            this.NrNaturezaDespesa = entity.NaturezaDespesa;
            this.Referencia = entity.Referencia;
        }

        public ConfirmacaoPagamentoItem ToEntity()
        {
            var retorno = new ConfirmacaoPagamentoItem();
            retorno.Id = this.Id;
            retorno.IdConfirmacaoPagamento = this.IdConfirmacaoPagamento;
            retorno.DataConfirmacaoItem = this.DataConfirmacaoItem;
            retorno.IdTipoDespesa = this.IdTipoDespesa;
            retorno.IdOrigem = this.IdOrigem;
            retorno.IdReclassificacaoRetencao = this.IdReclassificacaoRetencao;
            retorno.IdRegional = this.IdRegional;
            retorno.NumeroAgenciaPagador = this.NumeroAgenciaPagador;
            retorno.NumeroBancoPagador = this.NumeroBancoPagador;
            retorno.AnoMedicao = this.AnoMedicao;
            retorno.MesMedicao = this.MesMedicao;
            retorno.CodigoGestaoCredor = this.CodigoGestaoCredor;
            retorno.CodigoEvento = this.CodigoEvento;
            retorno.CodigoInscricao = this.CodigoInscricao;
            retorno.CodigoClassificacao = this.CodigoClassificacao;
            retorno.NumeroFonteSiafem = this.NumeroFonteSiafem;
            retorno.ValorDocumento = this.ValorDocumento;
            retorno.Observacao = this.Observacao;
            retorno.NaturezaDespesa = this.NrNaturezaDespesa;
            retorno.Referencia = this.Referencia;
            return retorno;
        }

        public ConfirmacaoPagamentoItem ToEntity(PDExecucaoItem entity)
        {
            var retorno = new ConfirmacaoPagamentoItem();
            retorno.Id = entity.Id;
            retorno.IdConfirmacaoPagamento = entity.Id;
            return retorno;
        }
    }
    #endregion ConfirmacaoPagamentoItem
}

