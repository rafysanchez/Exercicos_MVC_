using Sids.Prodesp.Model.Interface.Empenho;

namespace Sids.Prodesp.Model.Base.Empenho
{
    using System;
    using Interface;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEmpenho : IEmpenho
    {
        protected BaseEmpenho()
        {
            this.TransmitirProdesp = true;
            this.TransmitirSiafem = true;
            this.TransmitirSiafisico = false;
        }


        [Display(Name = "Nº da Reserva")]
        [Column("cd_reserva")]
        public string CodigoReserva { get; set; }

        [Column("id_empenho")]
        public virtual int Id { get; set; }

        [Column("bl_cadastro_completo")]
        public bool CadastroCompleto { get; set; }

        [Display(Name = "Código da Assinatura")]
        [Column("cd_autorizado_assinatura")]
        public string CodigoAutorizadoAssinatura { get; set; }

        [Display(Name = "Cód. Aplicação")]
        [Column("cd_aplicacao_obra")]
        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_autorizado_grupo")]
        public int CodigoAutorizadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_autorizado_orgao")]
        public string CodigoAutorizadoOrgao { get; set; }

        [Display(Name = "Credor - Organização (Prodesp)")]
        [Column("cd_credor_organizacao")]
        public int CodigoCredorOrganizacao { get; set; }

        [Display(Name = "Código de Especificação de Despesa")]
        [Column("cd_especificacao_despesa")]
        public string CodigoEspecificacaoDespesa { get; set; }

        [Display(Name = "Evento")]
        [Column("cd_evento")]
        public int CodigoEvento { get; set; }

        [Display(Name = "Código da Assinatura")]
        [Column("cd_examinado_assinatura")]
        public string CodigoExaminadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_examinado_grupo")]
        public int CodigoExaminadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_examinado_orgao")]
        public string CodigoExaminadoOrgao { get; set; }

        [Display(Name = "Gestão")]
        [Column("cd_gestao")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Gestão Credor (SIAFEM/SIAFISICO)")]
        [Column("cd_gestao_credor")]
        public string CodigoGestaoCredor { get; set; }

        [Display(Name = "Gestão Fornecedora")]
        [Column("cd_gestao_fornecedora")]
        public string CodigoGestaoFornecedora { get; set; }

        [Display(Name = "Código da Assinatura")]
        [Column("cd_responsavel_assinatura")]
        public string CodigoResponsavelAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_responsavel_grupo")]
        public int CodigoResponsavelGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_responsavel_orgao")]
        public string CodigoResponsavelOrgao { get; set; }

        [Display(Name = "Unidade de Fornecimento")]
        [Column("cd_unidade_fornecimento")]
        public string CodigoUnidadeFornecimento { get; set; }

        [Display(Name = "Unidade Gestora")]
        [Column("cd_unidade_gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "UGF")]
        [Column("cd_unidade_gestora_fornecedora")]
        public string CodigoUnidadeGestoraFornecedora { get; set; }

        [Display(Name = "UO")]
        [Column("cd_uo")]
        public int CodigoUO { get; set; }

        [Display(Name = "Data de Cadastramento")]
        [Column("dt_cadastramento")]
        public DateTime DataCadastramento { get; set; }

        [Display(Name = "Data de Cadastramento Até")]
        public DateTime DataCadastramentoAte { get; set; }

        [Display(Name = "Data de Cadastramento De")]
        public DateTime DataCadastramentoDe { get; set; }

        [Display(Name = "Data de Emissão")]
        [Column("dt_emissao")]
        public DateTime DataEmissao { get; set; }

        [Column("dt_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }

        [Column("dt_transmitido_siafem")]
        public DateTime DataTransmitidoSiafem { get; set; }

        [Column("dt_transmitido_siafisico")]
        public DateTime DataTransmitidoSiafisico { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_autorizado_cargo")]
        public string DescricaoAutorizadoCargo { get; set; }

        [Display(Name = "Autorizado no processo supra as folhas")]
        [Column("ds_autorizado_supra_folha")]
        public string DescricaoAutorizadoSupraFolha { get; set; }

        [Display(Name = "Especificação de Despesa (Prodesp)")]
        [Column("ds_especificacao_despesa")]
        public string DescricaoEspecificacaoDespesa { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_examinado_cargo")]
        public string DescricaoExaminadoCargo { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_responsavel_cargo")]
        public string DescricaoResponsavelCargo { get; set; }

        [Display(Name = "Destino do Recurso")]
        [Column("tb_destino_cd_destino")]
        public string DestinoId { get; set; }

        [Display(Name = "Origem do Recurso")]
        [Column("tb_fonte_id_fonte")]
        public int FonteId { get; set; }

        [Display(Name = "Licitação")]
        [Column("tb_licitacao_id_licitacao")]
        public string LicitacaoId { get; set; }

        [Column("ds_msgRetornoTransmissaoProdesp")]
        public string MensagemServicoProdesp { get; set; }

        public string MensagemServicoSiafem { get; set; }

        [Column("ds_msgRetornoTransmissaoSiafisico")]
        public string MensagemServicoSiafisico { get; set; }

        [Display(Name = "Modalidade")]
        [Column("tb_modalidade_id_modalidade")]
        public int ModalidadeId { get; set; }

        [Display(Name = "CED")]
        [Column("tb_estrutura_id_estrutura")]
        public int NaturezaId { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_autorizado_assinatura")]
        public string NomeAutorizadoAssinatura { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_examinado_assinatura")]
        public string NomeExaminadoAssinatura { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_responsavel_assinatura")]
        public string NomeResponsavelAssinatura { get; set; }

        [Display(Name = "Ano")]
        [Column("nr_ano_exercicio")]
        public int NumeroAnoExercicio { get; set; }

        [Display(Name = "CNPJ / CPF (Prodesp)")]
        [Column("nr_cnpj_cpf_fornecedor")]
        public string NumeroCNPJCPFFornecedor { get; set; }

        [Display(Name = "CNPJ / CPF / UG Credor (SIAFEM/SIAFISICO)")]
        [Column("nr_cnpj_cpf_ug_credor")]
        public string NumeroCNPJCPFUGCredor { get; set; }

        [Display(Name = "Número do Contrato")]
        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Nº do CT")]
        [Column("nr_ct")]
        public string NumeroCT { get; set; }


        [Display(Name = "Nº do CT Original")]
        [Column("nr_ct_original")]
        public string NumeroOriginalCT { get; set; }

        [Column("ds_msgRetornoTransmissaoSiafem")]
        public string MensagemSiafemSiafisico { get; set; }

        [Display(Name = "Nº Empenho Prodesp")]
        [Column("nr_empenhoProdesp")]
        public string NumeroEmpenhoProdesp { get; set; }

        [Display(Name = "Nº Empenho SIAFEM")]
        [Column("nr_empenhoSiafem")]
        public string NumeroEmpenhoSiafem { get; set; }

        [Display(Name = "Nº Empenho SIAFISICO")]
        [Column("nr_empenhoSiafisico")]
        public string NumeroEmpenhoSiafisico { get; set; }

        [Display(Name = "Nº do Processo")]
        [Column("nr_processo")]
        public string NumeroProcesso { get; set; }

        [Display(Name = "Nº do Processo (NE)")]
        [Column("nr_processo_ne")]
        public string NumeroProcessoNE { get; set; }

        [Display(Name = "Nº do Processo (Siafisico)")]
        [Column("nr_processo_siafisico")]
        public string NumeroProcessoSiafisico { get; set; }

        [Display(Name = "Origem Material")]
        [Column("tb_origem_material_id_origem_material")]
        public int OrigemMaterialId { get; set; }

        [Display(Name = "CFP")]
        [Column("tb_programa_id_programa")]
        public int ProgramaId { get; set; }

        [Display(Name = "Órgão")]
        [Column("tb_regional_id_regional")]
        public short RegionalId { get; set; }

        [Column("ds_status_documento")]
        public bool StatusDocumento { get; set; }

        [Display(Name = "Status Prodesp")]
        [Column("ds_status_prodesp")]
        public string StatusProdesp { get; set; }

        [Display(Name = "Status SIAFEM/SIAFISICO")]
        [Column("ds_status_siafem")]
        public string StatusSiafemSiafisico { get; set; }

        [Display(Name = "Tipo de Aquisição")]
        [Column("tb_aquisicao_tipo_id_aquisicao_tipo")]
        public int TipoAquisicaoId { get; set; }

        [Display(Name = "Tipo de Obra")]
        [Column("cd_tipo_obra")]
        public int TipoObraId { get; set; }

        [Column("fg_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }

        [Column("fg_transmitido_siafem")]
        public bool TransmitidoSiafem { get; set; }

        [Column("fg_transmitido_siafisico")]
        public bool TransmitidoSiafisico { get; set; }

        [Display(Name = "PRODESP")]
        [Column("bl_transmitir_prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Display(Name = "SIAFEM")]
        [Column("bl_transmitir_siafem")]
        public bool TransmitirSiafem { get; set; }

        [Display(Name = "SIAFISICO")]
        [Column("bl_transmitir_siafisico")]
        public bool TransmitirSiafisico { get; set; }

        [Display(Name = "Código NATUR (Item)")]
        [Column("nr_natureza_item")]
        public string CodigoNaturezaItem { get; set; }


        [Column("ds_status_siafisico_ne")]
        public string StatusSiafisicoNE { get; set; }

        [Column("ds_status_siafisico_ct")]
        public string StatusSiafisicoCT { get; set; }

        [Display(Name = "Município")]
        [Column("cd_municipio")]
        public string CodigoMunicipio { get; set; }

        [Display(Name = "Acordo")]
        [Column("ds_acordo")]
        public string DescricaoAcordo { get; set; }

        [Display(Name = "Local de Entrega")]
        [Column("ds_local_entrega_siafem")]
        public string DescricaoLocalEntregaSiafem { get; set; }

        [Display(Name = "Contabilizar como BEC?")]
        [Column("bl_contBec")]
        public bool ContBec { get; set; }


    }
}
