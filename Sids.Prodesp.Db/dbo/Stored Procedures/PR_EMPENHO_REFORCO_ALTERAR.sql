-- ===================================================================      
-- Author:  Carlos Henrique Magalhães 06/01//2014  
-- Create date: 20/12/2016  
-- Description: Procedure para alteração de empenhos      
-- ===================================================================    
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_ALTERAR]        
 @id_empenho_reforco int,  
 @tb_regional_id_regional smallint = NULL,  
 @tb_programa_id_programa int = NULL,  
 @tb_estrutura_id_estrutura int = NULL,  
 @tb_fonte_id_fonte int = NULL,  
 @tb_licitacao_id_licitacao int = NULL,  
 @tb_modalidade_id_modalidade int = NULL,  
 @tb_aquisicao_tipo_id_aquisicao_tipo int = NULL,  
 @tb_origem_material_id_origem_material int = NULL,  
 @tb_destino_cd_destino int = NULL,  
 @cd_fonte_siafisico varchar(30) = NULL,  
 @cd_reserva varchar(9) = NULL,  
 @cd_empenho varchar(11) = NULL,  
 @cd_empenho_original varchar(11)= NULL,  
 @ds_acordo varchar(7) = NULL,  
 @nr_ano_exercicio int = NULL,  
 @nr_processo varchar(58) =  NULL,  
 @nr_processo_ne varchar(15) = NULL,  
 @nr_processo_siafisico varchar(11)  = NULL,  
 @nr_contrato varchar(13) = NULL,  
 @nr_ct varchar(11) =  NULL,  
 @nr_ct_original varchar(11) =  NULL,  
 @nr_empenhoProdesp varchar(13) =  NULL,  
 @nr_empenhoSiafem varchar(11) =  NULL,  
 @nr_empenhoSiafisico varchar(11) =  NULL,  
 @cd_aplicacao_obra varchar(8) = NULL,  
 @nr_natureza_item char(2) = NULL,  
 @nr_natureza_ne varchar(12) = NULL,  
 @dt_cadastramento date = NULL,  
 @nr_cnpj_cpf_ug_credor varchar(15) = NULL,  
 @cd_gestao_credor int = NULL,  
 @cd_credor_organizacao int = NULL,  
 @nr_cnpj_cpf_fornecedor varchar(15) = NULL,  
 @cd_unidade_gestora int = NULL,  
 @cd_gestao int = NULL,  
 @cd_evento int = NULL,  
 @dt_emissao date = NULL,  
 @cd_unidade_gestora_fornecedora int = NULL,  
 @cd_gestao_fornecedora int = NULL,  
 @cd_uo int = NULL,  
 @cd_unidade_fornecimento varchar(5) =  NULL,  
 @ds_autorizado_supra_folha varchar(4) =  NULL,  
 @cd_especificacao_despesa varchar(3) = NULL,  
 @ds_especificacao_despesa varchar(711) =  NULL,  
 @cd_autorizado_assinatura varchar(5) = NULL,  
 @cd_autorizado_grupo int = NULL,  
 @cd_autorizado_orgao varchar(2) = NULL,  
 @nm_autorizado_assinatura varchar(55) =  NULL,  
 @ds_autorizado_cargo varchar(55) =  NULL,  
 @cd_examinado_assinatura varchar(5) = NULL,  
 @cd_examinado_grupo int = NULL,  
 @cd_examinado_orgao varchar(2) = NULL,  
 @nm_examinado_assinatura varchar(55) =  NULL,  
 @ds_examinado_cargo varchar(55) =  NULL,  
 @cd_responsavel_assinatura varchar(5) = NULL,  
 @cd_responsavel_grupo int = NULL,  
 @cd_responsavel_orgao varchar(2) = NULL,  
 @nm_responsavel_assinatura varchar(55) =  NULL,  
 @ds_responsavel_cargo varchar(140) =  NULL,  
 @bl_transmitir_prodesp bit = NULL,  
 @fg_transmitido_prodesp bit = NULL,  
 @dt_transmitido_prodesp date = NULL,  
 @bl_transmitir_siafem bit = NULL,  
 @fg_transmitido_siafem bit = NULL,  
 @dt_transmitido_siafem date = NULL,  
 @bl_transmitir_siafisico bit = NULL,  
 @fg_transmitido_siafisico bit = NULL,  
 @dt_transmitido_siafisico date = NULL,  
 @ds_status_prodesp varchar(1) =  NULL,  
 @ds_status_siafem varchar(1) =  NULL,  
 @ds_status_siafisico varchar(1) =  NULL,  
 @ds_status_documento bit = NULL,  
 @bl_cadastro_completo bit = NULL,  
 @ds_msgRetornoTransmissaoProdesp varchar(140) =  NULL,  
 @ds_msgRetornoTransmissaoSiafem varchar(140) =  NULL,  
 @ds_msgRetornoTransmissaoSiafisico varchar(140) =  NULL,  
 @status_siafisico_ne char(1) = NULL,  
 @status_siafisico_ct char(1) = NULL,  
 @cd_municipio varchar(4) = NULL,  
 @ds_local_entrega_siafem varchar(45)= NULL  ,
 @bl_contBec bit = NULL
AS    
BEGIN      
  
 SET NOCOUNT ON;      
     
 UPDATE empenho.tb_empenho_reforco  
 SET   
   tb_regional_id_regional = nullif( @tb_regional_id_regional, 0 )  
 , tb_programa_id_programa = nullif( @tb_programa_id_programa, 0 )  
 , tb_estrutura_id_estrutura = nullif( @tb_estrutura_id_estrutura, 0 )  
 , tb_fonte_id_fonte = nullif( @tb_fonte_id_fonte, 0 )  
 , tb_licitacao_id_licitacao = nullif( @tb_licitacao_id_licitacao , 0 )  
 , tb_modalidade_id_modalidade  = nullif( @tb_modalidade_id_modalidade , 0 )  
 , tb_aquisicao_tipo_id_aquisicao_tipo  = nullif( @tb_aquisicao_tipo_id_aquisicao_tipo , 0 )  
 , tb_origem_material_id_origem_material  = nullif( @tb_origem_material_id_origem_material , 0 )  
 , tb_destino_cd_destino = nullif( @tb_destino_cd_destino, '')  
 , cd_fonte_siafisico = @cd_fonte_siafisico  
 , cd_reserva = @cd_reserva  
 , cd_empenho = @cd_empenho  
 , cd_empenho_original = @cd_empenho_original  
 , ds_acordo = @ds_acordo  
 , nr_ano_exercicio = @nr_ano_exercicio  
 , nr_processo = @nr_processo  
 , nr_processo_ne =@nr_processo_ne  
 , nr_processo_siafisico =@nr_processo_siafisico  
 , nr_contrato =@nr_contrato  
 , nr_ct =@nr_ct  
 , nr_ct_original = @nr_ct_original  
 , nr_empenhoProdesp =@nr_empenhoProdesp  
 , nr_empenhoSiafem =@nr_empenhoSiafem  
 , nr_empenhoSiafisico =@nr_empenhoSiafisico  
 , cd_aplicacao_obra = @cd_aplicacao_obra  
 , nr_natureza_item =@nr_natureza_item  
 , nr_natureza_ne =@nr_natureza_ne  
 , nr_cnpj_cpf_ug_credor =@nr_cnpj_cpf_ug_credor  
 , cd_gestao_credor =@cd_gestao_credor  
 , cd_credor_organizacao =@cd_credor_organizacao  
 , nr_cnpj_cpf_fornecedor =@nr_cnpj_cpf_fornecedor  
 , cd_unidade_gestora =@cd_unidade_gestora  
 , cd_gestao =@cd_gestao  
 , cd_evento =@cd_evento  
 , dt_emissao =@dt_emissao  
 , cd_unidade_gestora_fornecedora =@cd_unidade_gestora_fornecedora  
 , cd_gestao_fornecedora =@cd_gestao_fornecedora  
 , cd_uo =@cd_uo  
 , cd_unidade_fornecimento =@cd_unidade_fornecimento  
 , ds_autorizado_supra_folha =@ds_autorizado_supra_folha  
 , cd_especificacao_despesa =@cd_especificacao_despesa  
 , ds_especificacao_despesa =@ds_especificacao_despesa  
 , cd_autorizado_assinatura =@cd_autorizado_assinatura  
 , cd_autorizado_grupo =@cd_autorizado_grupo  
 , cd_autorizado_orgao =@cd_autorizado_orgao  
 , nm_autorizado_assinatura =@nm_autorizado_assinatura  
 , ds_autorizado_cargo =@ds_autorizado_cargo  
 , cd_examinado_assinatura =@cd_examinado_assinatura  
 , cd_examinado_grupo =@cd_examinado_grupo  
 , cd_examinado_orgao =@cd_examinado_orgao  
 , nm_examinado_assinatura =@nm_examinado_assinatura  
 , ds_examinado_cargo =@ds_examinado_cargo  
 , cd_responsavel_assinatura =@cd_responsavel_assinatura  
 , cd_responsavel_grupo =@cd_responsavel_grupo  
 , cd_responsavel_orgao =@cd_responsavel_orgao  
 , nm_responsavel_assinatura =@nm_responsavel_assinatura  
 , ds_responsavel_cargo =@ds_responsavel_cargo  
 , bl_transmitir_prodesp =@bl_transmitir_prodesp  
 , fg_transmitido_prodesp =@fg_transmitido_prodesp  
 , dt_transmitido_prodesp =@dt_transmitido_prodesp  
 , bl_transmitir_siafem =@bl_transmitir_siafem  
 , fg_transmitido_siafem =@fg_transmitido_siafem  
 , dt_transmitido_siafem =@dt_transmitido_siafem  
 , bl_transmitir_siafisico =@bl_transmitir_siafisico  
 , fg_transmitido_siafisico =@fg_transmitido_siafisico  
 , dt_transmitido_siafisico =@dt_transmitido_siafisico  
 , ds_status_prodesp =@ds_status_prodesp  
 , ds_status_siafem =@ds_status_siafem  
 , ds_status_siafisico =@ds_status_siafisico  
 , ds_status_documento =@ds_status_documento  
 , bl_cadastro_completo =@bl_cadastro_completo  
 , ds_msgRetornoTransmissaoProdesp =@ds_msgRetornoTransmissaoProdesp  
 , ds_msgRetornoTransmissaoSiafem =@ds_msgRetornoTransmissaoSiafem  
 , ds_msgRetornoTransmissaoSiafisico =@ds_msgRetornoTransmissaoSiafisico  
 , ds_status_siafisico_ne  =@status_siafisico_ne   
 , ds_status_siafisico_ct =@status_siafisico_ct   
 , cd_municipio  =@cd_municipio   
 , ds_local_entrega_siafem =@ds_local_entrega_siafem   
 , bl_contBec = @bl_contBec
    
 WHERE id_empenho_reforco = @id_empenho_reforco  
  
END