namespace Sids.Prodesp.Model.Entity.Reserva
{
    using Interface.Reserva;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ReservaReforco: IReserva
    {
        //  Properties
        
        /// <summary>
        /// Codigo do Reforço
        /// </summary>
        [Display(Name = "Códico")]
        [Column("id_reforco")]
        public int Codigo { get; set; }
        
        /// <summary>
        /// Codigo do Reforço Prodesp
        /// </summary>
        [Display(Name = "Número do Reforço Prodesp")]
        [Column("nr_reforco_prodesp")]
        public string NumProdesp { get; set; }

        /// <summary>
        /// Numero do refoço Siafem com Siafisco
        /// </summary>
        [Display(Name = "Número do Reforço Siafem/Siafisico")]
        [Column("nr_reforco_siafem_siafisico")]
        public string NumSiafemSiafisico { get; set; }

        /// <summary>
        /// Codigo da Reserva
        /// </summary>
        [Display(Name = "Reserva")]
        [Column("cd_reserva")]
        public int? Reserva { get; set; }
        /// <summary>
        /// Codigo da Fonte
        /// </summary>
        [Display(Name = "Fonte")]
        [Column("id_fonte")]
        public int? Fonte { get; set; }

        /// <summary>
        /// Codigo da Estrutura
        /// </summary>
        [Display(Name = "Estrutura")]
        [Column("id_estrutura")]
        public int? Estrutura { get; set; }

        /// <summary>
        /// Codigo do Programa
        /// </summary>
        [Display(Name = "Programa")]
        [Column("id_programa")]
        public int? Programa { get; set; }

        /// <summary>
        /// Codigo da Regional
        /// </summary>
        [Display(Name = "Regional")]
        [Column("id_regional")]
        public short? Regional { get; set; }

        /// <summary>
        /// Codigo do Contrato
        /// </summary>
        [Display(Name = "Contrato")]
        [Column("cd_contrato")]
        public string Contrato { get; set; }

        /// <summary>
        /// Codigo do Processo
        /// </summary>
        [Display(Name = "Processo")]
        [Column("cd_processo")]
        public string Processo { get; set; }

        /// <summary>
        /// Codigo da Obra
        /// </summary>
        [Display(Name = "Obra")]
        [Column("cd_obra")]
        public int? Obra { get; set; }

        /// <summary>
        /// Numero da OC
        /// </summary>
        [Display(Name = "Oc")]
        [Column("nr_oc")]
        public string Oc { get; set; }

        /// <summary>
        /// Código da Ugo
        /// </summary>
        [Display(Name = "Ugo")]
        [Column("cd_ugo")]
        public string Ugo { get; set; }

        /// <summary>
        /// Codigo da Uo
        /// </summary>
        [Display(Name = "Uo")]
        [Column("cd_uo")]
        public string Uo { get; set; }

        /// <summary>
        /// Codigo do Evento
        /// </summary>
        [Display(Name = "Evento")]
        [Column("cd_evento")]
        public int? Evento { get; set; }

        /// <summary>
        /// Ano do Exercicio
        /// </summary>
        [Display(Name = "Ano de Exercicio")]
        [Column("nr_ano_exercicio")]
        public int? AnoExercicio { get; set; }

        /// <summary>
        /// Codigo da Origem do Recurso
        /// </summary>
        [Display(Name = "Origem Recurso")]
        [Column("cd_origem_recurso")]
        public string OrigemRecurso { get; set; }

        /// <summary>
        /// Codigo do De destino do recurso
        /// </summary>
        [Display(Name = "Destino Recurso")]
        [Column("cd_destino_recurso")]
        public string DestinoRecurso { get; set; }

        /// <summary>
        /// Descrição de observação
        /// </summary>
        [Display(Name = "Observacao")]
        [Column("ds_observacao")]
        public string Observacao { get; set; }

        /// <summary>
        /// Flag Transmitido prodesp
        /// </summary>
        [Display(Name = "Transmitido Prodesp")]
        [Column("fg_transmitido_prodesp")]
        public bool? TransmitidoProdesp { get; set; }

        /// <summary>
        /// Flag transmitido Siafem
        /// </summary>
        [Display(Name = "Transmitido Siafem")]
        [Column("fg_transmitido_siafem")]
        public bool? TransmitidoSiafem { get; set; }

        /// <summary>
        /// flag trasmitido Siafisico
        /// </summary>
        [Display(Name = "Transmitido Siafisico")]
        [Column("fg_transmitido_siafisico")]
        public bool? TransmitidoSiafisico { get; set; }

        /// <summary>
        /// Trasmitir para Prodesp
        /// </summary>
        [Display(Name = "Transmitir Prodesp")]
        [Column("bl_transmitir_prodesp")]
        public bool TransmitirProdesp { get; set; }

        /// <summary>
        /// Trasmitir para Siafem
        /// </summary>
        [Display(Name = "Transmitir Siafem")]
        [Column("bl_transmitir_siafem")]
        public bool TransmitirSiafem { get; set; }

        /// <summary>
        /// Trasmitir para Siafisico
        /// </summary>
        [Display(Name = "Transmitir Siafisico")]
        [Column("bl_transmitir_siafisico")]
        public bool TransmitirSiafisico { get; set; }

        /// <summary>
        /// Descrição da folha Supra
        /// </summary>
        [Display(Name = "Autorizado Supra Folha")]
        [Column("ds_autorizado_supra_folha")]
        public string AutorizadoSupraFolha { get; set; }

        /// <summary>
        /// Codigo da especificação da despesa
        /// </summary>
        [Display(Name = "Especificacao Despesa")]
        [Column("cd_especificacao_despesa")]
        public string EspecificacaoDespesa { get; set; }

        /// <summary>
        /// Descrição da especificação da despesa
        /// </summary>
        [Display(Name = "Especificacao Despesa")]
        [Column("ds_especificacao_despesa")]
        public string DescEspecificacaoDespesa { get; set; }

        /// <summary>
        /// Codigo da autorização da assinatura
        /// </summary>
        [Display(Name = "Autorizado Assinatura")]
        [Column("cd_autorizado_assinatura")]
        public string AutorizadoAssinatura { get; set; }

        /// <summary>
        /// Codigo da autorizãção do grupo
        /// </summary>
        [Display(Name = "Autorizado Grupo")]
        [Column("cd_autorizado_grupo")]
        public string AutorizadoGrupo { get; set; }

        /// <summary>
        /// Codigo da autorização do orgão
        /// </summary>
        [Display(Name = "Autorizado Orgao")]
        [Column("cd_autorizado_orgao")]
        public string AutorizadoOrgao { get; set; }

        /// <summary>
        /// Nome da autorização da assinatura
        /// </summary>
        [Display(Name = "Nome Autorizado Assinatura")]
        [Column("nm_autorizado_assinatura")]
        public string NomeAutorizadoAssinatura { get; set; }

        /// <summary>
        /// descrição da autorização do cargo
        /// </summary>
        [Display(Name = "Autorizado Cargo")]
        [Column("ds_autorizado_cargo")]
        public string AutorizadoCargo { get; set; }

        /// <summary>
        /// Codigo de exame da assinatura
        /// </summary>
        [Display(Name = "Examinado Assinatura")]
        [Column("cd_examinado_assinatura")]
        public string ExaminadoAssinatura { get; set; }

        /// <summary>
        /// Código de exame do grupo
        /// </summary>
        [Display(Name = "Examinado Grupo")]
        [Column("cd_examinado_grupo")]
        public string ExaminadoGrupo { get; set; }

        /// <summary>
        /// Codigo de exame do Orgão
        /// </summary>
        [Display(Name = "Examinado Orgao")]
        [Column("cd_examinado_orgao")]
        public string ExaminadoOrgao { get; set; }

        /// <summary>
        /// Descrição exame da assinatura
        /// </summary>
        [Display(Name = "Nome Examinado Assinatura")]
        [Column("nm_examinado_assinatura")]
        public string NomeExaminadoAssinatura { get; set; }

        /// <summary>
        /// Descrição do cargo examinado
        /// </summary>
        [Display(Name = "Examinado Cargo")]
        [Column("ds_examinado_cargo")]
        public string ExaminadoCargo { get; set; }

        /// <summary>
        /// Codigo do responsavel assinatura
        /// </summary>
        [Display(Name = "Responsavel Assinatura")]
        [Column("cd_responsavel_assinatura")]
        public string ResponsavelAssinatura { get; set; }

        /// <summary>
        /// Codigo do grupo responsavel
        /// </summary>
        [Display(Name = "Responsavel Grupo")]
        [Column("cd_responsavel_grupo")]
        public string ResponsavelGrupo { get; set; }

        /// <summary>
        /// Codigo do orgao responsavel
        /// </summary>
        [Display(Name = "Responsavel Orgao")]
        [Column("cd_responsavel_orgao")]
        public string ResponsavelOrgao { get; set; }

        /// <summary>
        /// Nome do responsavel pela assinatura
        /// </summary>
        [Display(Name = "Nome Responsavel Assinatura")]
        [Column("nm_responsavel_assinatura")]
        public string NomeResponsavelAssinatura { get; set; }

        /// <summary>
        /// Descrição do responsavel pelo cargo
        /// </summary>
        [Display(Name = "Responsavel Cargo")]
        [Column("ds_responsavel_cargo")]
        public string ResponsavelCargo { get; set; }

        [Display(Name = "Data Emissão")]
        [Column("dt_emissao_reforco")]
        public DateTime DataEmissao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]  //estas propriedades não estarão presentes no banco, servindo para armazenamento de intervalo de datas
        public DateTime? DataEmissaoDe { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //estas propriedades não estarão presentes no banco, servindo para armazenamento de intervalo de datas
        public DateTime? DataEmissaoAte { get; set; }

        /// <summary>
        /// Mensagem para obtenção de status Siafem/Siafisico webservice
        /// </summary>
        [Display(Name = "Mensagem Siafem/Siafisico")]
        [Column("ds_status_siafem_siafisico")]
        public string StatusSiafemSiafisico { get; set; }

        /// <summary>
        /// Mensagem para obtenção de status da Prodesp webservice
        /// </summary>
        [Display(Name = "Mensagem Prodesp")]
        [Column("ds_status_prodesp")]
        public string StatusProdesp { get; set; }

        /// <summary>
        /// Mensagem para obtenção de status do documento
        /// </summary>
        [Display(Name = "Status Documento")]
        [Column("ds_status_documento")]
        public bool StatusDoc { get; set; }

        [Display(Name = "Data Transmissão Prodesp")]
        [Column("dt_transmissao_prodesp")]
        public DateTime? DataTransmissaoProdesp { get; set; }

        [Display(Name = "Data Transmissão Siafem Siafisico")]
        [Column("dt_transmissao_siafem_siafisico")]
        public DateTime? DataTransmissaoSiafemSiafisico { get; set; }

        [Display(Name = "Data Cadastramento")]
        [Column("dt_cadastramento")]
        public DateTime? DataCadastro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "Cadastro Completo?")]
        [Column("bl_cadastro_completo")]
        public bool CadastroCompleto { get; set; }

        [Display(Name = "")]
        [Column("cd_cfp")]
        public string CfpPrograma { get; set; }    //propriedade provem da tabela "tb_programa" , campo não existente na tabela "tb_reforco",mas utilizado na procedure: [PR_REFORCO_CONSULTAR_GRID]

        [Display(Name = "")]
        [Column("ds_programa")]
        public string DescricaoPrograma { get; set; }    //propriedade provem da tabela "tb_programa" , campo não existente na tabela "tb_reforco",mas utilizado na procedure: [PR_REFORCO_CONSULTAR_GRID]

        [Display(Name = "")]
        [Column("cd_natureza")]
        public string NaturezaEstrutura { get; set; } //propriedade provem da tabela "tb_estrutura" , campo não existente na tabela "tb_reforco", mas utilizado na procedure: [PR_REFORCO_CONSULTAR_GRID]


        [Display(Name = "valorMes")]
        [Column("vr_mes")]
        public decimal ValorMes { get; set; }

        [Display(Name ="Retorno Transmissão Prodesp")]
        [Column("ds_msgRetornoTransmissaoProdesp")]
        public string MsgRetornoTransmissaoProdesp { get; set; }


        [Display(Name = "Retorno Transmissão Siafem Siafisico")]
        [Column("ds_msgRetornoTransSiafemSiafisico")]
        public string MsgRetornoTransSiafemSiafisico { get; set; }

        public int? Tipo
        {
            get
            {
                return 0;
            }
            
        }
    }
}
