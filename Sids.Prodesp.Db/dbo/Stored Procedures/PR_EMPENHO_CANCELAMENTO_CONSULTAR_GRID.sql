-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 11/01/2017
-- Description: Procedure para exibir filtros de Reforço de Empenho
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_CONSULTAR_GRID]  
	@nr_empenhoProdesp varchar(13) =  NULL,
	@nr_empenhoSiafem varchar(11) =  NULL,
	@nr_empenhoSiafisico varchar(11) =  NULL,
	@cd_aplicacao_obra varchar(8) = NULL,
	@nr_processo varchar(11) =  NULL,
	@nr_ano_exercicio int = NULL,
	@tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo int = NULL,
	@tb_regional_id_regional smallint = NULL,
	@tb_programa_id_programa int = NULL,
	@tb_estrutura_id_estrutura int = NULL,
	@nr_natureza_item int = NULL,
	@tb_fonte_id_fonte int = NULL,
	@ds_status_prodesp varchar(1) =  NULL,
	@ds_status_siafem varchar(1) =  NULL,
	@ds_status_siafisico varchar(1) =  NULL,
	@nr_contrato varchar(13) = NULL,
	@tb_licitacao_id_licitacao varchar(2) = NULL,
	@nr_cnpj_cpf_ug_credor varchar(15) = NULL,
	@cd_gestao_credor varchar(6) = NULL,
	@cd_credor_organizacao int = NULL,
	@nr_cnpj_cpf_fornecedor varchar(15) = NULL,
	@dt_cadastramento_de date = NULL,
	@dt_cadastramento_ate date = NULL
	
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
			id_empenho_cancelamento
     	  , nr_empenhoProdesp
		  , nr_empenhoSiafem
		  , nr_empenhoSiafisico
		  ,	tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo
		  , tb_programa_id_programa
		  , nr_natureza_item
		  , tb_fonte_id_fonte
		  , tb_destino_cd_destino
		  , tb_licitacao_id_licitacao
		  , ds_status_prodesp
		  , ds_status_siafem
		  , ds_status_siafisico
		  , bl_transmitir_prodesp
		  , fg_transmitido_prodesp
		  , dt_transmitido_prodesp
		  , bl_transmitir_siafem
		  , fg_transmitido_siafem
		  , dt_transmitido_siafem
		  , bl_transmitir_siafisico
		  , fg_transmitido_siafisico
		  , dt_transmitido_siafisico
		  , bl_cadastro_completo
		  , ds_msgRetornoTransmissaoProdesp
		  , ds_msgRetornoTransmissaoSiafem
		  , ds_msgRetornoTransmissaoSiafisico

		  
	
	FROM empenho.tb_empenho_cancelamento (nolock)
	where
		( nullif( @tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo, 0 ) is null or tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo = @tb_empenho_cancelamento_tipo_id_empenho_cancelamento_tipo ) and
		( nullif( @tb_fonte_id_fonte, 0 ) is null or tb_fonte_id_fonte = @tb_fonte_id_fonte ) and
		( nullif( @tb_regional_id_regional, 0 ) is null or tb_regional_id_regional = @tb_regional_id_regional ) and
		( nullif( @tb_programa_id_programa, 0 ) is null or tb_programa_id_programa = @tb_programa_id_programa ) and
		( nullif( @tb_estrutura_id_estrutura, 0 ) is null or tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura ) and
		( @tb_licitacao_id_licitacao is null or tb_licitacao_id_licitacao = @tb_licitacao_id_licitacao ) and
		( @nr_empenhoProdesp is null or nr_empenhoProdesp = @nr_empenhoProdesp ) and
		( @nr_empenhoSiafem is null or nr_empenhoSiafem = @nr_empenhoSiafem ) and
		( @nr_empenhoSiafisico is null or nr_empenhoSiafisico = @nr_empenhoSiafisico ) and
		( @nr_processo is null or nr_processo = @nr_processo ) and
		( @cd_aplicacao_obra is null or cd_aplicacao_obra = @cd_aplicacao_obra ) and
		( nullif( @nr_ano_exercicio, 0 ) is null or nr_ano_exercicio = @nr_ano_exercicio ) and
		( nullif( @nr_natureza_item, 0 ) is null or nr_natureza_item = @nr_natureza_item ) and
		( @ds_status_prodesp is null or ds_status_prodesp = @ds_status_prodesp ) 
		
		
		and	(( @ds_status_siafem is null or ds_status_siafem = @ds_status_siafem ) 
		or	( @ds_status_siafisico is null or ds_status_siafisico = @ds_status_siafisico )) and
		
		( @nr_contrato  is null or nr_contrato = @nr_contrato ) and
	
		( @dt_cadastramento_de is null or dt_cadastramento >= @dt_cadastramento_de ) and 
		( @dt_cadastramento_ate is null or dt_cadastramento <= @dt_cadastramento_ate ) and

		( @nr_cnpj_cpf_ug_credor is null or nr_cnpj_cpf_ug_credor = @nr_cnpj_cpf_ug_credor ) and
		( @cd_gestao_credor is null or cd_gestao_credor = @cd_gestao_credor ) and
		( nullif( @cd_credor_organizacao, 0 ) is null or cd_credor_organizacao = @cd_credor_organizacao ) and
		( @nr_cnpj_cpf_fornecedor is null or nr_cnpj_cpf_fornecedor = @nr_cnpj_cpf_fornecedor )   
end;