
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 18/10/2016
-- Description:	Procedure para incluir de Reserva 
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_RESERVA_INCLUIR]
		@id_fonte						int = null
		,@id_estrutura					int = null
		,@id_programa					int = null
		,@id_tipo_reserva				int = null
		,@id_regional					smallint = null
		,@cd_contrato					varchar(12) = null
		,@cd_processo					varchar(50) = null
		,@nr_reserva_prodesp			varchar(9) = null
		,@nr_reserva_siafem_siafisico	varchar(11) = null
		,@cd_obra						int = null
		,@nr_oc							varchar(12) = null
		,@cd_ugo						varchar(6) = null
		,@cd_uo							varchar(5) = null
		,@cd_evento						int = null
		,@nr_ano_exercicio				int = null
		,@nr_ano_referencia_reserva		int = null
		,@cd_origem_recurso				varchar(9) = null
		,@cd_destino_recurso			varchar(2) = null
		,@ds_observacao					varchar(308) = null
		,@fg_transmitido_prodesp		bit = null
		,@fg_transmitido_siafem			bit = null
		,@fg_transmitido_siafisico		bit = null
		,@bl_transmitir_prodesp			bit = null
		,@bl_transmitir_siafem			bit = null
		,@bl_transmitir_siafisico		bit = null
		,@ds_autorizado_supra_folha		varchar(4) = null
		,@cd_especificacao_despesa		varchar(3) = null
		,@ds_especificacao_despesa		varchar(719) = null
		,@cd_autorizado_assinatura		varchar(5) = null
		,@cd_autorizado_grupo			varchar(1) = null
		,@cd_autorizado_orgao			varchar(2) = null
		,@nm_autorizado_assinatura		varchar(55) = null
		,@ds_autorizado_cargo			varchar(55) = null
		,@cd_examinado_assinatura		varchar(5) = null
		,@cd_examinado_grupo			varchar(1) = null
		,@cd_examinado_orgao			varchar(2) = null
		,@nm_examinado_assinatura		varchar(55) = null
		,@ds_examinado_cargo			varchar(55) = null
		,@cd_responsavel_assinatura		varchar(5) = null
		,@cd_responsavel_grupo			varchar(1) = null
		,@cd_responsavel_orgao			varchar(2) = null
		,@nm_responsavel_assinatura		varchar(55) = null
		,@ds_responsavel_cargo			varchar(55) = null
		,@dt_emissao_reserva			date = null
		,@dt_cadastro					date = null
		,@bl_cadastro_completo          bit = null
as
begin

	SET NOCOUNT ON;
	
	begin transaction
		INSERT INTO [reserva].[tb_reserva]
				([id_fonte]
				,[id_estrutura]
				,[id_programa]
				,[id_tipo_reserva]
				,[id_regional]
				,[cd_contrato]
				,[cd_processo]
				,[nr_reserva_prodesp]
				,[nr_reserva_siafem_siafisico]
				,[cd_obra]
				,[nr_oc]
				,[cd_ugo]
				,[cd_uo]
				,[cd_evento]
				,[nr_ano_exercicio]
				,[nr_ano_referencia_reserva]
				,[cd_origem_recurso]
				,[cd_destino_recurso]
				,[ds_observacao]
				,[fg_transmitido_prodesp]
				,[fg_transmitido_siafem]
				,[fg_transmitido_siafisico]
				,[bl_transmitir_prodesp]
				,[bl_transmitir_siafem]
				,[bl_transmitir_siafisico]
				,[ds_autorizado_supra_folha]
				,[cd_especificacao_despesa]
				,[ds_especificacao_despesa]
				,[cd_autorizado_assinatura]
				,[cd_autorizado_grupo]
				,[cd_autorizado_orgao]
				,[nm_autorizado_assinatura]
				,[ds_autorizado_cargo]
				,[cd_examinado_assinatura]
				,[cd_examinado_grupo]
				,[cd_examinado_orgao]
				,[nm_examinado_assinatura]
				,[ds_examinado_cargo]
				,[cd_responsavel_assinatura]
				,[cd_responsavel_grupo]
				,[cd_responsavel_orgao]
				,[nm_responsavel_assinatura]
				,[ds_responsavel_cargo]
				,[dt_emissao_reserva]
				,[dt_cadastro]
				,[bl_cadastro_completo]
				,[ds_status_siafem_siafisico]
				,[ds_status_prodesp])
			VALUES
				(@id_fonte
				,@id_estrutura
				,@id_programa
				,@id_tipo_reserva
				,@id_regional
				,@cd_contrato
				,@cd_processo
				,@nr_reserva_prodesp
				,@nr_reserva_siafem_siafisico
				,@cd_obra
				,@nr_oc
				,@cd_ugo
				,@cd_uo
				,@cd_evento
				,@nr_ano_exercicio
				,@nr_ano_referencia_reserva
				,@cd_origem_recurso
				,@cd_destino_recurso
				,@ds_observacao
				,@fg_transmitido_prodesp
				,@fg_transmitido_siafem
				,@fg_transmitido_siafisico
				,@bl_transmitir_prodesp
				,@bl_transmitir_siafem
				,@bl_transmitir_siafisico
				,@ds_autorizado_supra_folha
				,@cd_especificacao_despesa
				,@ds_especificacao_despesa
				,@cd_autorizado_assinatura
				,@cd_autorizado_grupo
				,@cd_autorizado_orgao
				,@nm_autorizado_assinatura
				,@ds_autorizado_cargo
				,@cd_examinado_assinatura
				,@cd_examinado_grupo
				,@cd_examinado_orgao
				,@nm_examinado_assinatura
				,@ds_examinado_cargo
				,@cd_responsavel_assinatura
				,@cd_responsavel_grupo
				,@cd_responsavel_orgao
				,@nm_responsavel_assinatura
				,@ds_responsavel_cargo
				,@dt_emissao_reserva
				,@dt_cadastro
				,@bl_cadastro_completo
				,'N'
				,'N');
	Commit
	
    
    SELECT @@IDENTITY
End