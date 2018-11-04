using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class ConfirmacaoPagamento
    {
        [Column("id_confirmacao_pagamento")]
        public int Id { get; set; }

        public int id_confirmacao_pagamento { get; set; }

        [Column("id_confirmacao_pagamento_item")]
        public int id_confirmacao_pagamento_item { get; set; }

        public string acao { get; set; }

        [Column("id_confirmacao_pagamento_tipo")]
        public int? TipoConfirmacao { get; set; }

        [Column("id_tipo_documento")]
        public int? IdTipoDocumento { get; set; }
        [Column("id_tipo_pagamento")]
        public int? TipoPagamento { get; set; }
        [Column("id_execucao_pd")]
        public int? IdExecucaoPD { get; set; }
        [Column("id_autorizacao_ob")]
        public int? IdAutorizacaoOB { get; set; }
        [NotMapped]
        [Column("nr_conta")]
        public string NumeroConta { get; set; }
        [NotMapped]
        [Column("nr_documento")]
        public string NumeroDocumento { get; set; }        
        [NotMapped]
        public string NumeroOP { get; set; }
        [Column("ano_referencia")]
        public int? AnoReferencia { get; set; }
        [Column("dt_cadastro")]
        public DateTime? DataCadastro { get; set; }
        [Column("dt_confirmacao")]
        public DateTime? DataConfirmacao { get; set; }
        [Column("dt_modificacao")]
        public DateTime? DataModificacao { get; set; }
        [NotMapped]
        [Column("dt_preparacao")]
        public DateTime? DataPreparacao { get; set; }

        [Column("vr_total_confirmado")]
        public decimal ValorTotalConfirmado { get; set; }

        [Column("fl_transmissao_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }
        [Column("dt_transmissao_transmitido_prodesp")]
        public DateTime? DataTransmitidoProdesp { get; set; }
        [Column("ds_transmissao_mensagem_prodesp")]
        public string MensagemServicoProdesp { get; set; }
        [Column("cd_transmissao_status_prodesp")]
        public string StatusProdesp { get; set; }
        [NotMapped]
        public int RegionalId { get; set; }
        [Column("nr_agrupamento")]
        public int? CodigoAgrupamentoConfirmacaoPagamento { get; set; }

        public IEnumerable<ConfirmacaoPagamentoItem> Items { get; set; }

        [NotMapped]
        public string Orgao { get; set; }
        [NotMapped]
        public string TipoDespesa { get; set; }

        public int DespesaTipo { get; set; }
        [NotMapped]
        public string NumeroContrato { get; set; }
        [NotMapped]
        public string CodigoObra { get; set; }
        [NotMapped]
        public string NomeReduzidoCredor { get; set; }
        [NotMapped]
        public string CPF_CNPJ { get; set; }
        [NotMapped]
        public DateTime? DataCadastroDe { get; set; }
        [NotMapped]
        public DateTime? DataCadastroAte { get; set; }
        [NotMapped]
        public decimal ValorConfirmacao { get; set; }
        [NotMapped]
        public string OrigemConfirmacao { get; set; }

        public int Origem { get; set; }
        [NotMapped]
        public string NumeroBaixaRepasse { get; set; }
        [NotMapped]
        public string Banco { get; set; }
        [NotMapped]
        public string Valor { get; set; }
        [NotMapped]
        public string Agencia { get; set; }
        [NotMapped]
        public string Conta { get; set; }
        [NotMapped]
        public string TransmissaoConfirmacao { get; set; }
        [NotMapped]
        public string NumeroNLBaixaRepasse { get; set; }
        [NotMapped]
        public string NLDocumento { get; set; }
        [NotMapped]
        public string Fonte { get; set; }

        public string FonteLista { get; set; }
        [NotMapped]
        public string ValorTotalConfirmarISSQN { get; set; }
        [NotMapped]
        public string ValorTotalConfirmarIR { get; set; }

        public decimal ValorTotalFonte { get; set; }
        [NotMapped]
        public string DataVencimento { get; set; }
        [NotMapped]
        public string TipoDocumento { get; set; }
        [NotMapped]
        public string FonteSIAFEM { get; set; }
        [NotMapped]
        public string NumeroEmpenho { get; set; }
        [NotMapped]
        public string NumeroProcesso { get; set; }
        [NotMapped]
        public string NotaFiscal { get; set; }
        [NotMapped]
        public string ValorDocumento { get; set; }

        public decimal ValorDocumentoDecimal { get; set; }
        [NotMapped]
        public string NaturezaDespesa { get; set; }
        [NotMapped]
        public string CredorOrganizacaoCredorOriginal { get; set; }
        [NotMapped]
        public string CPFCNPJCredorOriginal { get; set; }
        [NotMapped]
        public string CredorOriginal { get; set; }
        [NotMapped]
        public string Referencia { get; set; }
        [NotMapped]
        public string CredorOrganizacao { get; set; }
        [NotMapped]
        public string ValorDesdobradoCredor { get; set; }
        [NotMapped]
        public string BancoFavorecido { get; set; }
        [NotMapped]
        public string AgenciaFavorecido { get; set; }
        [NotMapped]
        public string ContaFavorecido { get; set; }
        [NotMapped]
        public string Impressora { get; set; }
        [NotMapped]
        public string TipoSistema { get; set; }
        [NotMapped]
        public string CPF_CNPJ_Credor { get; set; }
        [NotMapped]
        public string StatusTransmissaoConfirmacao { get; set; }
        [NotMapped]
        public string MensagemErroRetornadaTransmissaoConfirmacaoPagamento { get; set; }
        [NotMapped]
        public string Chave { get; set; }
        [NotMapped]
        public string Senha { get; set; }

        [NotMapped]
        public bool Totalizador { get; set; }
        [NotMapped]
        public string CredorOrganizacaoCredorDocto { get; set; }
        [NotMapped]
        public string numeroEmpenhoSiafem { get; set; }

        public string NumeroDocumentoItem  { get; set; }

        [NotMapped]
        public string TipoDocumentoItem { get; set; }

        [NotMapped]
        public string ContaProdesp { get; set; }

        [NotMapped]
        public string DataRealizacao { get; set; }

        [NotMapped]
        public string NomeReduzidoCredorDocto { get; set; }

    }
}
