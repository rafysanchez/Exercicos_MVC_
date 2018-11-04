namespace Sids.Prodesp.Model.Base.LiquidacaoDespesa
{
    using Interface.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LiquidacaoDespesa : ILiquidacaoDespesa
    {
        public LiquidacaoDespesa()
        {

            this.TransmitirSiafem = true;
            this.TransmitirSiafisico = false;
            this.TransmitirProdesp = true;
        }

        [Column("id_subempenho")]
        public virtual int Id { get; set; }

        [Column("cd_tarefa")]
        public string CodigoTarefa { get; set; }

        [Column("cd_despesa")]
        public string CodigoDespesa { get; set; }

        [Column("nr_recibo")]
        public string NumeroRecibo { get; set; }

        [Column("ds_prazo_pagamento")]
        public string PrazoPagamento { get; set; }

        [Column("dt_realizado")]
        public DateTime DataRealizado { get; set; }

        [Column("dt_cadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("dt_vencimento")]
        public DateTime DataVencimento { get; set; }

        [Column("tb_regional_id_regional")]
        public short RegionalId { get; set; }

        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Prodesp")]
        [Column("fl_sistema_prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Column("fl_sistema_siafem_siafisico")]
        public bool TransmitirSiafem { get; set; }

        [Column("fl_sistema_siafisico")]
        public bool TransmitirSiafisico { get; set; }

        [Column("nr_prodesp")]
        public string NumeroProdesp { get; set; }

        [Column("nr_empenho_prodesp")]
        public virtual string NumeroOriginalProdesp { get; set; }

        [Column("nr_siafem_siafisico")]
        public string NumeroSiafemSiafisico { get; set; }

        [Column("tb_cenario_id_cenario")]
        public int CenarioSiafemSiafisico { get; set; }

        [Column("vl_realizado")]
        public int ValorRealizado { get; set; }

        [Column("cd_cenario_prodesp")]
        public string CenarioProdesp { get; set; }


        [Column("nr_nl_referencia")]
        public string NlReferencia { get; set; }

        [Column("nr_ct")]
        public string NumeroCT { get; set; }

        [Column("nr_empenho_siafem_siafisico")]
        public string NumeroOriginalSiafemSiafisico { get; set; }

        [Column("cd_unidade_gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Column("cd_gestao")]
        public string CodigoGestao { get; set; }

        [Column("cd_aplicacao_obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Column("cd_obra_tipo")]
        public string CodigoTipoDeObra { get; set; }

        [Column("vl_valor")]
        public int Valor { get; set; }

        [Column("dt_emissao")]
        public DateTime DataEmissao { get; set; }

        [Column("tb_evento_tipo_id_evento_tipo")]
        public int TipoEventoId { get; set; }

        [Column("cd_evento")]
        public int CodigoEvento { get; set; }

        [Column("nr_cnpj_cpf_credor")]
        public string NumeroCNPJCPFCredor { get; set; }

        [Column("cd_gestao_credor")]
        public string CodigoGestaoCredor { get; set; }

        [Column("nr_ano_medicao")]
        public string AnoMedicao { get; set; }

        [Column("nr_mes_medicao")]
        public string MesMedicao { get; set; }

        [Column("nr_percentual")]
        public string Percentual { get; set; }

        [Column("tb_obra_tipo_id_obra_tipo")]
        public int TipoObraId { get; set; }

        [Column("cd_unidade_gestora_obra")]
        public string CodigoUnidadeGestoraObra { get; set; }

        [Column("ds_observacao_1")]
        public string DescricaoObservacao1 { get; set; }

        [Column("ds_observacao_2")]
        public string DescricaoObservacao2 { get; set; }

        [Column("ds_observacao_3")]
        public string DescricaoObservacao3 { get; set; }

        [Column("nr_despesa_processo")]
        public string NumeroProcesso { get; set; }

        [Column("ds_despesa_referencia")]
        public string Referencia { get; set; }

        [Column("ds_despesa_autorizado_supra_folha")]
        public string DescricaoAutorizadoSupraFolha { get; set; }

        [Column("cd_despesa_especificacao_despesa")]
        public string CodigoEspecificacaoDespesa { get; set; }

        [Column("ds_despesa_especificacao_1")]
        public string DescricaoEspecificacaoDespesa1 { get; set; }

        [Column("ds_despesa_especificacao_2")]
        public string DescricaoEspecificacaoDespesa2 { get; set; }

        [Column("ds_despesa_especificacao_3")]
        public string DescricaoEspecificacaoDespesa3 { get; set; }

        [Column("ds_despesa_especificacao_4")]
        public string DescricaoEspecificacaoDespesa4 { get; set; }

        [Column("ds_despesa_especificacao_5")]
        public string DescricaoEspecificacaoDespesa5 { get; set; }

        [Column("ds_despesa_especificacao_6")]
        public string DescricaoEspecificacaoDespesa6 { get; set; }

        [Column("ds_despesa_especificacao_7")]
        public string DescricaoEspecificacaoDespesa7 { get; set; }

        [Column("ds_despesa_especificacao_8")]
        public string DescricaoEspecificacaoDespesa8 { get; set; }

        [Column("ds_despesa_especificacao_9")]
        public string DescricaoEspecificacaoDespesa9 { get; set; }

        [Column("cd_assinatura_autorizado")]
        public string CodigoAutorizadoAssinatura { get; set; }

        [Column("cd_assinatura_autorizado_grupo")]
        public int CodigoAutorizadoGrupo { get; set; }

        [Column("cd_assinatura_autorizado_orgao")]
        public string CodigoAutorizadoOrgao { get; set; }

        [Column("ds_assinatura_autorizado_cargo")]
        public string DescricaoAutorizadoCargo { get; set; }

        [Column("nm_assinatura_autorizado")]
        public string NomeAutorizadoAssinatura { get; set; }

        [Column("cd_assinatura_examinado")]
        public string CodigoExaminadoAssinatura { get; set; }

        [Column("cd_assinatura_examinado_grupo")]
        public int CodigoExaminadoGrupo { get; set; }

        [Column("cd_assinatura_examinado_orgao")]
        public string CodigoExaminadoOrgao { get; set; }

        [Column("ds_assinatura_examinado_cargo")]
        public string DescricaoExaminadoCargo { get; set; }

        [Column("nm_assinatura_examinado")]
        public string NomeExaminadoAssinatura { get; set; }

        [Column("cd_assinatura_responsavel")]
        public string CodigoResponsavelAssinatura { get; set; }

        [Column("cd_assinatura_responsavel_grupo")]
        public int CodigoResponsavelGrupo { get; set; }

        [Column("cd_assinatura_responsavel_orgao")]
        public string CodigoResponsavelOrgao { get; set; }

        [Column("ds_assinatura_responsavel_cargo")]
        public string DescricaoResponsavelCargo { get; set; }

        [Column("nm_assinatura_responsavel")]
        public string NomeResponsavelAssinatura { get; set; }

        [Column("cd_transmissao_status_prodesp")]
        public string StatusProdesp { get; set; }

        [Column("fl_transmissao_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }

        [Column("dt_transmissao_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }

        [Column("ds_transmissao_mensagem_prodesp")]
        public string MensagemProdesp { get; set; }

        [Column("cd_transmissao_status_siafem_siafisico")]
        public string StatusSiafemSiafisico { get; set; }

        [Column("fl_transmissao_transmitido_siafem_siafisico")]
        public bool TransmitidoSiafem { get; set; }

        [Column("fl_transmissao_transmitido_siafisico")]
        public bool TransmitidoSiafisico { get; set; }

        [Column("dt_transmissao_transmitido_siafem_siafisico")]
        public DateTime DataTransmitidoSiafemSiafisico { get; set; }

        [Column("ds_transmissao_mensagem_siafem_siafisico")]
        public string MensagemSiafemSiafisico { get; set; }

        [Column("fl_documento_completo")]
        public bool CadastroCompleto { get; set; }

        [Column("fl_documento_status")]
        public bool StatusDocumento { get; set; }

        [Column("nr_obra")]
        public string NumeroObra { get; set; }

        [Column("tb_natureza_tipo_id_natureza_tipo")]
        public string NaturezaSubempenhoId { get; set; }

        [Column("ds_nl_retencao_inss")]
        public string NlRetencaoInss { get; set; }

        [Column("ds_lista")]
        public string Lista { get; set; }


        public virtual IEnumerable<LiquidacaoDespesaEvento> Eventos { get; set; }
        public virtual IEnumerable<LiquidacaoDespesaNota> Notas { get; set; }
        public virtual IEnumerable<LiquidacaoDespesaItem> Itens { get; set; }



        public virtual string Normal { get; }
        public virtual string Estorno { get; }

        [Column("nr_subempenho_prodesp")]
        public string NumeroSubempenhoProdesp { get; set; }

        [Column("vl_anular")]
        public int ValorAnular { get; set; }

        [NotMapped]
        [Column("fl_referencia_digitada")]
        public bool ReferenciaDigitada { get; set; }

        public int ProgramaId { get; set; }
    }
}
