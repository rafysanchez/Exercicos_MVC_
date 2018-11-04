using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento
{
    public class PreparacaoPagamento : Base.PagamentoContaUnica,IPagamentoContaUnicaProdesp
    {
        [Key]
        [Column("id_preparacao_pagamento")]
        public override int Id { get; set; }
        
        [Column("bl_transmitir_prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Column("bl_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }

        [Column("dt_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }

        [Column("ds_transmissao_mensagem_prodesp")]
        public string MensagemServicoProdesp { get; set; }

        [Column("ds_status_prodesp")]
        public string StatusProdesp { get; set; }

        [Column("ds_status_documento")]
        public bool StatusDocumento { get; set; }


        [Column("cd_credor_organizacao")]
        public string CodigoCredorOrganizacaoId { get; set; }

        [Column("nr_cpf_cnpj_credor")]
        public string NumeroCnpjcpfCredor { get; set; }

        [Column("id_tipo_preparacao_pagamento")]
        public int PreparacaoPagamentoTipoId { get; set; }

        [Column("nr_op_inicial")]
        public string NumeroOpInicial { get; set; }

        [Column("nr_op_final")]
        public string NumeroOpFinal { get; set; }

        [Column("nr_ano_exercicio")]
        public int AnoExercicio { get; set; }

        [Column("ds_referencia")]
        public string Referencia { get; set; }

        [Column("cd_assinatura")]
        public string CodigoAutorizadoAssinatura { get; set; }

        [Column("cd_grupo_assinatura")]
        public string CodigoAutorizadoGrupo { get; set; }

        [Column("cd_orgao_assinatura")]
        public string CodigoAutorizadoOrgao { get; set; }

        [Column("ds_cargo_assinatura")]
        public string DescricaoAutorizadoCargo { get; set; }

        [Column("nm_assinatura")]
        public string NomeAutorizadoAssinatura { get; set; }

        [Column("cd_contra_assinatura")]
        public string CodigoExaminadoAssinatura { get; set; }

        [Column("cd_grupo_contra_assinatura")]
        public string CodigoExaminadoGrupo { get; set; }

        [Column("cd_orgao_contra_assinatura")]
        public string CodigoExaminadoOrgao { get; set; }

        [Column("ds_cargo_contra_assinatura")]
        public string DescricaoExaminadoCargo { get; set; }

        [Column("nm_contra_assinatura")]
        public string NomeExaminadoAssinatura { get; set; }

        [Column("vr_documento")]
        public decimal ValorDocumento { get; set; }
        

        [Column("cd_conta")]
        public string CodigoConta { get; set; }

        [Column("nr_banco")]
        public string NumeroBanco { get; set; }

        [Column("nr_agencia")]
        public string NumeroAgencia { get; set; }

        [Column("nr_conta")]
        public string NumeroConta { get; set; }

        [Column("cd_despesa")]
        public string CodigoDespesa { get; set; }

        [Column("dt_vencimento")]
        public DateTime DataVencimento { get; set; }




        [Column("ds_despesa_credor")]
        public string CodigoDespesaCredor { get; set; }

        [Column("ds_credor1")]
        public string Credor1 { get; set; }

        [Column("ds_credor2")]
        public string Credor2 { get; set; }

        [Column("ds_endereco")]
        public string Endereco { get; set; }

        [Column("nr_cep")]
        public string Cep { get; set; }

        [Column("nr_banco_credor")]
        public string NumeroBancoCredor { get; set; }

        [Column("nr_agencia_credor")]
        public string NumeroAgenciaCredor { get; set; }

        [Column("nr_conta_credor")]
        public string NumeroContaCredor { get; set; }

        [Column("nr_banco_pgto")]
        public string NumeroBancoPagto { get; set; }

        [Column("nr_agencia_pgto")]
        public string NumeroAgenciaPagto { get; set; }

        [Column("nr_conta_pgto")]
        public string NumeroContaPagto { get; set; }


        [Column("qt_op_preparada")]
        public int QuantidadeOpPreparada { get; set; }

        [Column("vr_total")]
        public decimal ValorTotal { get; set; }

        [NotMapped]
        public override string CodigoAplicacaoObra { get; set; }

        public PreparacaoPagamentoTipo PreparacaoPagamentoTipo { get; set; }
    }
}
