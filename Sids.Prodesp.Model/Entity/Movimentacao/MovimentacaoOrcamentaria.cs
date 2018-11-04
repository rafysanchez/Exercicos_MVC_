using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Entity.Configuracao;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoOrcamentaria
    {
        #region base
        public string UnidadeGestoraFavorecida { get; set; }

        public string IdGestaoFavorecida { get; set; }

        public string AutorizadoSupraFolha { get; set; }

        [Display(Name = "Código da Assinatura")]
        [Column("cd_autorizado_assinatura")]
        public string CodigoAutorizadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_autorizado_grupo")]
        public int CodigoAutorizadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_autorizado_orgao")]
        public string CodigoAutorizadoOrgao { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_autorizado_cargo")]
        public string DescricaoAutorizadoCargo { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_autorizado_assinatura")]
        public string NomeAutorizadoAssinatura { get; set; }

        [Display(Name = "Código da Assinatura")]
        [Column("cd_examinado_assinatura")]
        public string CodigoExaminadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_examinado_grupo")]
        public int CodigoExaminadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_examinado_orgao")]
        public string CodigoExaminadoOrgao { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_examinado_cargo")]
        public string DescricaoExaminadoCargo { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_examinado_assinatura")]
        public string NomeExaminadoAssinatura { get; set; }

        [Display(Name = "Código da Assinatura")]
        [Column("cd_responsavel_assinatura")]
        public string CodigoResponsavelAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_responsavel_grupo")]
        public int CodigoResponsavelGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_responsavel_orgao")]
        public string CodigoResponsavelOrgao { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_responsavel_cargo")]
        public string DescricaoResponsavelCargo { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_responsavel_assinatura")]
        public string NomeResponsavelAssinatura { get; set; }

        [Column("tb_programa_id_programa")]
        public int IdPrograma { get; set; }

        [Column("tb_fonte_id_fonte")]
        public int IdFonte { get; set; }

        [Column("tb_estrutura_id_estrutura")]
        public int IdEstrutura { get; set; }

        public string NrNotaCredito { get; set; }
        #endregion


        [Column("id_movimentacao_orcamentaria")]
        public int Id { get; set; }

        public int IdMovimentacao { get; set; }

        [Column("nr_siafem")]
        public string NumSiafem { get; set; }        

        [Column("nr_agrupamento_movimentacao")]
        public int NrAgrupamento { get; set; }

        public int nr_sequencia { get; set; }

        [Column("tb_regional_id_regional")]
        public int IdRegional { get; set; }

        [Column("tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria")]
        public int IdTipoMovimentacao { get; set; }

        [Column("tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao")]
        public int IdTipoDocumento { get; set; }

        [Column("cd_unidade_gestora_emitente")]
        public string UnidadeGestoraEmitente { get; set; }

        [Column("cd_gestao_emitente")]
        public string GestaoEmitente { get; set; }

        [Column("nr_ano_exercicio")]
        public int AnoExercicio { get; set; }

        [Column("dt_cadastro")]
        public DateTime DataCadastro { get; set; }

        [Column("bl_transmitido_siafem")]
        public bool TransmitidoSiafem{ get; set; }

        [Column("bl_transmitir_siafem")]
        public bool TransmitirSiafem { get; set; }

        [Column("fg_transmitido_siafem")]
        public string StatusSiafem { get; set; }

        [Column("dt_trasmitido_siafem")]
        public DateTime DataSiafem { get; set; }

        [Column("bl_transmitir_prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Column("bl_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }

        [Column("fg_transmitido_prodesp")]
        public string StatusProdesp { get; set; }

        [Column("dt_trasmitido_prodesp")]
        public DateTime DataProdesp { get; set; }

        [Column("cd_cfp")]
        public long IdCFP { get; set; }

        [Column("cd_ced")]
        public int IdCED { get; set; }

        [Column("ds_msgRetornoProdesp")]
        public string MensagemProdesp { get; set; }

        [Column("ds_msgRetornoSiafem")]
        public string MensagemSiafem { get; set; }

        [Column("bl_cadastro_completo")]
        public bool CadastroCompleto { get; set; }

        [Column("valor_geral")]
        public decimal Valor { get; set; }

        [NotMapped]
        [Column("desc_documento")]
        public string DescDocumento { get; set; }
        
        [NotMapped]
        [Column("Ug_favorecida")]
        public string UGFavorecida { get; set; }

        [NotMapped]
        [Column("cd_estrutura")]
        public string CdEstrutura { get; set; }

        [NotMapped]
        [Column("cd_natureza")]
        public string CdMatureza { get; set; }


        public string EspecDespesa { get; set; }

        public string DescEspecDespesa { get; set; }

        [Display(Name = "Observação Cancelamento/Distribuição")]
        public string ObservacaoCancelamento { get; set; }

        public string ObservacaoCancelamento2 { get; set; }

        public string ObservacaoCancelamento3 { get; set; }
        
        [Display(Name = "Observação Nota de Crédito")]
        public string ObservacaoNC { get; set; }

        public string ObservacaoNC2 { get; set; }

        public string ObservacaoNC3 { get; set; }

        public bool PodeExcluir { get; set; }

        public bool PodeAlterar { get; set; }



        public List<MovimentacaoCancelamento> Cancelamento { get; set; }

        public List<MovimentacaoNotaDeCredito> NotasCreditos { get; set; }

        public List<MovimentacaoDistribuicao> Distribuicao { get; set; }

        public List<MovimentacaoReducaoSuplementacao> ReducaoSuplementacao { get; set; }

        public List<MovimentacaoMes> Meses { get; set; }
        public string CfpDesc { get; set; }
        public string CedDesc { get; set; }

        public MovimentacaoOrcamentaria()
        {
            Cancelamento = new List<MovimentacaoCancelamento>();
            NotasCreditos = new List<MovimentacaoNotaDeCredito>();
            Distribuicao = new List<MovimentacaoDistribuicao>();
            ReducaoSuplementacao = new List<MovimentacaoReducaoSuplementacao>();
            Meses = new List<MovimentacaoMes>();
        }
    }
}
