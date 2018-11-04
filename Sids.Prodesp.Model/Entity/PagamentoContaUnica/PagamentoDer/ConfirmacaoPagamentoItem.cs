using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class ConfirmacaoPagamentoItem
    {
        [Column("id_confirmacao_pagamento_item")]
        public int Id { get; set; }

        [Column("id_programacao_desembolso_execucao_item")]
        public int? IdPDExecucaoItem { get; set; }

        [Column("id_autorizacao_ob")]
        public int IdAutorizacaoOB { get; set; }

        [Column("id_autorizacao_ob_item")]
        public int? IdAutorizacaoOBItem { get; set; }

        [Column("id_confirmacao_pagamento")]
        public int? IdConfirmacaoPagamento { get; set; }

        [Column("id_tipo_documento")]
        public int IdTipoDocumento { get; set; }

        [Column("dt_confirmacao")]
        public DateTime? DataConfirmacaoItem { get; set; }

        [Column("id_regional")]
        public int IdRegional { get; set; }

        [Column("id_reclassificacao_retencao")]
        public int? IdReclassificacaoRetencao { get; set; }

        [Column("id_origem")]
        public int? IdOrigem { get; set; }

        [Column("id_despesa_tipo")]
        public int? IdTipoDespesa { get; set; }

        [Column("dt_vencimento")]
        public DateTime? DataVencimento { get; set; }

        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Column("cd_obra")]
        public string CodigoObra { get; set; }

        [Column("nr_op")]
        public string NumeroOp { get; set; }

        [Column("nr_banco_pagador")]
        public string NumeroBancoPagador { get; set; }

        [Column("nr_agencia_pagador")]
        public string NumeroAgenciaPagador { get; set; }

        [Column("nr_conta_pagador")]
        public string NumeroContaPagador { get; set; }

        [Column("nr_fonte_siafem")]
        public string NumeroFonteSiafem { get; set; }

        [Column("nr_emprenho")]
        public string NumeroEmpenho { get; set; }

        [Column("nr_processo")]
        public string NumeroProcesso { get; set; }

        [Column("nr_nota_fiscal")]
        public int? NumeroNotaFiscal { get; set; }

        [Column("nr_nl_documento")]
        public string NumeroNlDocumento { get; set; }

        [Column("vr_documento")]
        public decimal? ValorDocumento { get; set; }

        [Column("nr_natureza_despesa")]
        public int? NaturezaDespesa { get; set; }

        [Column("cd_credor_organizacao")]
        public int? CodigoOrganizacaoCredor { get; set; }

        [Column("nr_cnpj_cpf_ug_credor")]
        public string NumeroCnpjCpfUgCredor { get; set; }

        [Column("vr_total_confirmado")]
        public decimal? ValorTotalConfirmado { get; set; }

        [Column("ds_referencia")]
        public string Referencia{ get; set; }

        [Column("fl_transmissao_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }
        [Column("dt_transmissao_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }
        [Column("ds_transmissao_mensagem_prodesp")]
        public string MensagemServicoProdesp { get; set; }
        [Column("cd_transmissao_status_prodesp")]
        public string StatusProdesp { get; set; }

        public string AnoMedicao { get; set; }
        public string MesMedicao { get; set; }
        public string CodigoGestaoCredor { get; set; }
        public string CodigoEvento { get; set; }
        public string CodigoInscricao { get; set; }
        public string CodigoClassificacao { get; set; }
        public string Observacao { get; set; }
        public EnumFormaGerarNl FormaGerar { get; set; }
        [Column("nr_empenhoSiafem")]
        public string NumeroEmpenhoSiafem { get; set; }
        [Column("nr_banco_favorecido")]
        public string NumeroBancoFavorecido { get; set; }
        [Column("nr_agencia_favorecido")]
        public string NumeroAgenciaFavorecido { get; set; }
        [Column("nr_conta_favorecido")]
        public string NumeroContaFavorecido { get; set; }
        public int CodigoTipoDespesa{ get; set; }
        [Column("nr_documento")]
        public string NrDocumento { get; set; }
        [NotMapped]
        public int IdNlParametrizacao { get; set; }
        [Column("nr_documento_gerador")]
        public string NumeroDocumentoGerador { get; set; }
        [NotMapped]
        public int numeroTipoNl { get; set; }

        public List<ConfirmacaoPagamentoItem> ValidacaoItens(List<ConfirmacaoPagamentoItem> itens)
        {
            // Se houver itens com os dados nulos os mesmo são descartados
            return itens.Where(x => !string.IsNullOrEmpty(x.NumeroBancoFavorecido) && !string.IsNullOrEmpty(x.NumeroAgenciaFavorecido)).ToList();
        }


    }
    
}
