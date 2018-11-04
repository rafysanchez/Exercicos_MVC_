using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao
{
    public class PDExecucao : IPagamentoContaUnica
    {

        public PDExecucao()
        {
            this.Items = new List<PDExecucaoItem>();
            this.TransmitirProdesp = true;
            this.TransmitirSiafem = true;
            this.Confirmacao = new ConfirmacaoPagamento();

        }
        [Column("id_execucao_pd")]
        public int? IdExecucaoPD { get; set; }

        [Column("id_confirmacao_pagamento")]
        public int IdConfirmacaoPagamento {get; set;}

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

        [Column("ano_pd")]
        public string Ano { get; set; }

        [Column("valor_total")]
        public decimal? Valor { get; set; }

        [Column("nr_agrupamento")]
        public int? Agrupamento { get; set; }

        [Column("dt_cadastro")]
        public DateTime DataCadastro { get; set; }

        [NotMapped]
        public string NumOP { get; set; }

        [Column("fl_sistema_prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Column("fl_sistema_siafem_siafisico")]
        public bool? TransmitirSiafem { get; set; }

        public IEnumerable<PDExecucaoItem> Items { get; set; }

        public IEnumerable<PDExecucaoItem> ItemsConfirmacaoPagamento { get; set; }

        public IEnumerable<PDExecucaoItem> ItemsConfirmados { get; set; }

        //[Column("bl_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }
        //[Column("ds_status_prodesp")]
        public string StatusProdesp { get; set; }
        //[Column("dt_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }
        [Column("ds_transmissao_mensagem_prodesp")]
        public String MensagemServicoProdesp { get; set; }

        /* IPagamento Conta Unica Interface */

        #region IPagamentoContaUnica
        public int Id { get; set; }

        public bool CadastroCompleto { get; set; }

        [Column("cd_aplicacao_obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Column("dt_emissao")]
        public DateTime DataEmissao { get; set; }

        [Column("dt_vencimento")]
        public DateTime DataVencimento { get; set; }

        public int RegionalId { get; set; }

        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Column("id_tipo_documento")]
        public int DocumentoTipoId { get; set; }

        [Column("nr_documento")]
        public string NumeroDocumento { get; set; }

        [Column("nr_documento_gerador")]
        public string NumeroDocumentoGerador { get; set; }


        #region Confirmacao
        [Column("id_tipo_pagamento")]
        public int? TipoPagamento { get; set; }

        [Column("fl_confirmacao")]
        public bool EhConfirmacaoPagamento { get; set; }

        [Column("dt_confirmacao")]
        public DateTime? DataConfirmacao { get; set; } 
        #endregion


        public DocumentoTipo DocumentoTipo { get; set; }

        public ConfirmacaoPagamento Confirmacao { get; set; }
        #endregion IPagamentoContaUnica
    }
}
