using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB
{
    public class OBAutorizacao : IPagamentoContaUnica
    {

        public OBAutorizacao()
        {
            this.Items = new List<OBAutorizacaoItem>();
            this.TransmitirProdesp = true;
            this.TransmitirSiafem = true;

        }
        [Column("id_autorizacao_ob")]
        public int? IdAutorizacaoOB { get; set; }

        [Column("id_confirmacao_pagamento")]
        public int IdConfirmacaoPagamento { get; set; }

        [Column("id_execucao_pd")]
        public int IdExecucaoPD { get; set; }

        [NotMapped]
        public DateTime DataConfirmacaoCombo { get; set; }

        [Column("id_tipo_execucao_pd")]
        public int? TipoExecucao { get; set; }

        [Column("ug_pagadora")]
        public string UgPagadora { get; set; }

        [Column("gestao_pagadora")]
        public string GestaoPagadora { get; set; }

        [Column("ug_liquidante")]
        public string UgLiquidante { get; set; }

        [Column("gestao_liquidante")]
        public string GestaoLiquidante { get; set; }

        [Column("unidade_gestora")]
        public string UnidadeGestora { get; set; }

        [Column("gestao")]
        public string Gestao { get; set; }

        [Column("ano_ob")]
        public string AnoOB { get; set; }

        [Column("qtde_autorizacao")]
        public int QuantidadeAutorizacao { get; set; }

        [Column("valor_total")]
        public decimal? Valor { get; set; }

        [Column("nr_agrupamento")]
        public int? NumeroAgrupamento { get; set; }

        [Column("dt_cadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("fl_sistema_prodesp")]
        public bool? TransmitirProdesp { get; set; }

        [Column("cd_transmissao_status_siafem")]
        public string TransmissaoStatusSiafem { get; set; }

        [Column("ds_transmissao_mensagem_siafem")]
        public string TransmissaoMensagemSiafem { get; set; }

        [Column("dt_transmissao_transmitido_siafem")]
        public DateTime TransmissaoDataSiafem { get; set; }

        [Column("fl_transmissao_siafem")]
        public bool TransmissaoTransmitidoSiafem { get; set; }

        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Column("cd_aplicacao_obra")]
        public string CodigoAplicacaoObra { get; set; }


        #region Confirmacao
        [Column("id_tipo_pagamento")]
        public int? TipoPagamento { get; set; }

        [Column("fl_confirmacao")]
        public bool EhConfirmacaoPagamento { get; set; }

        [Column("dt_confirmacao")]
        public DateTime? DataConfirmacao { get; set; }
        #endregion

        public IEnumerable<OBAutorizacaoItem> Items { get; set; }

        public IEnumerable<OBAutorizacaoItem> ItemsConfirmacaoPagamento { get; set; }

        //[Column("bl_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }
        //[Column("ds_status_prodesp")]
        public string StatusProdesp { get; set; }
        //[Column("dt_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }
        //[Column("ds_transmissao_mensagem_prodesp")]
        public String MensagemServicoProdesp { get; set; }

        /* IPagamento Conta Unica Interface */

        #region IPagamentoContaUnica
        public int Id { get; set; }

        public bool CadastroCompleto { get; set; }

        public DateTime DataEmissao { get; set; }

        public int RegionalId { get; set; }

        public int DocumentoTipoId { get; set; }

        public string NumeroDocumento { get; set; }

        public DocumentoTipo DocumentoTipo { get; set; }
        public bool? TransmitirSiafem { get; set; }

        public string NumOB { get; set; }

        public string FavorecidoDesc { get; set; }

        public string CodigoDespesa { get; set; }

        public ConfirmacaoPagamento Confirmacao { get; set; }

        #endregion IPagamentoContaUnica
    }
}
