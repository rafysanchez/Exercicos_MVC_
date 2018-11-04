using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB
{
    public class OBAutorizacaoItem : Base.PagamentoContaUnica
    {
        [NotMapped]
        public int id_confirmacao_pagamento { get; set; }

        [NotMapped]
        public int id_confirmacao_pagamento_item { get; set; }

        [Column("id_programacao_desembolso_execucao_item")]
        public int? Codigo { get; set; }

        [Column("id_autorizacao_ob")]
        public int IdAutorizacaoOB { get; set; }

        [Column("id_autorizacao_ob_item")]
        public int IdAutorizacaoOBItem { get; set; }

        [Column("id_execucao_pd")]
        public int IdExecucaoPD { get; set; }

        [Column("id_execucao_pd_item")]
        public int IdExecucaoPDItem { get; set; }

        [Column("id_tipo_documento")]
        public int IdTipoDocumento { get; set; }

        [Column("nr_documento")]
        public string NumeroDocumento { get; set; }

        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Column("nr_documento_gerador")]
        public string NumeroDocumentoGerador { get; set; }

        [Column("ds_numpd")]
        public string NumPD { get; set; }

        [Column("id_tipo_pagamento")]
        public int? TipoPagamento { get; set; }

        [Column("ug")]
        public string UG { get; set; }

        [Column("ug_pagadora")]
        public string UGPagadora { get; set; }

        [Column("ug_liquidante")]
        public string UGLiquidante { get; set; }

        [Column("gestao_pagadora")]
        public string GestaoPagadora { get; set; }

        [Column("gestao_liguidante")]
        public string GestaoLiquidante { get; set; }

        [Column("favorecido")]
        public string Favorecido { get; set; }

        [Column("favorecidoDesc")]
        public string FavorecidoDesc { get; set; }

        [Column("ordem")]
        public string Ordem { get; set; }

        [Column("ano_pd")]
        public string AnoAserpaga { get; set; }

        [Column("valor")]
        public string ValorItem { get; set; }

        [Column("ds_noup")]
        public string NouP { get; set; }

        [Column("nr_agrupamento_pd")]
        public int? Agrupamento { get; set; }

        [Column("nr_agrupamento_ob")]
        public int? AgrupamentoItemOB { get; set; }

        [Column("ds_numob")]
        public string NumOB { get; set; }

        [Column("ob_cancelada")]
        public bool? OBCancelada { get; set; }

        [Column("cd_transmissao_item_status_siafem")]
        public string TransmissaoItemStatusSiafem { get; set; }

        [Column("fl_transmissao_item_siafem")]
        public bool TransmissaoItemTransmitidoSiafem { get; set; }

        [Column("dt_transmissao_item_transmitido_siafem")]
        public DateTime? TransmissaoItemDataSiafem { get; set; }

        [Column("ds_transmissao_item_mensagem_siafem")]
        public string TransmissaoItemMensagemSiafem { get; set; }

        [Column("cd_despesa")]
        public string CodigoDespesa { get; set; }

        [Column("nr_banco")]
        public string NumeroBanco { get; set; }

        [Column("ds_numop")]
        public string NumOP { get; set; }

        [Column("ds_consulta_op_prodesp")]
        public string MensagemRetornoConsultaOP { get; set; }

        [Column("nr_documento_gerador")]
        public string NumDoctoGerador { get; set; }

        [Column("ValorTotal")]
        public decimal? ValorTotal { get; set; }

        [Column("fl_sistema_prodesp")]
        public bool? fl_sistema_prodesp { get; set; }

        [Column("cd_transmissao_status_prodesp")]
        public string TransmissaoItemStatusProdesp { get; set; }

        [Column("fl_transmissao_transmitido_prodesp")]
        public bool? TransmissaoItemTransmitidoProdesp { get; set; }

        [Column("dt_transmissao_transmitido_prodesp")]
        public DateTime? TransmissaoItemDataProdesp { get; set; }

        [Column("ds_transmissao_mensagem_prodesp")]
        public string TransmissaoItemMensagemProdesp { get; set; }

        [Column("dt_confirmacao")]
        public DateTime? DataConfirmacaoItem { get; set; }
        
        [Column("cd_aplicacao_obra")]
        public override string CodigoAplicacaoObra { get; set; }

        public IEnumerable<OBAutorizacaoItem> ItensSelecionados { get; set; }

        [NotMapped]
        public bool Executar { get; set; }

        [NotMapped]
        public bool IsDesdobramento
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NumeroDocumentoGerador) && !NumeroDocumentoGerador.Substring(NumeroDocumentoGerador.Length - 1).Equals("0");
            }
        }
    }
}
