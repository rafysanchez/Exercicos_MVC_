

-- ==============================================================
-- Author:		Luis Fernando
-- ALTER date: 08/12/2016
-- Description:	Procedure para incluir Cancelamento de Reserva 
-- ==============================================================

CREATE PROCEDURE [dbo].[PR_RESERVA_CANCELAMENTO_INCLUIR]
	@id_fonte int = null
,	@id_estrutura int = null
,	@id_programa int = null
,	@id_regional smallint = null
,	@cd_reserva int = null
,	@cd_contrato varchar( 12 ) = null
,	@cd_processo varchar( 50 ) = null
,	@nr_cancelamento_prodesp varchar(13) = null
,	@nr_cancelamento_siafem_siafisico varchar( 11 ) = null
,	@cd_obra int = null
,	@nr_oc varchar(11) = null
,	@cd_ugo varchar( 6 ) = null
,	@cd_uo varchar( 5 ) = null
,	@cd_evento int = null
,	@nr_ano_exercicio int = null
,	@cd_origem_recurso varchar( 9 ) = null
,	@cd_destino_recurso varchar( 2 ) = null
,	@ds_observacao varchar( 308 ) = null
,	@fg_transmitido_prodesp bit = null
,	@fg_transmitido_siafem bit = null
,	@fg_transmitido_siafisico bit = null
,	@bl_transmitir_prodesp bit = null
,	@bl_transmitir_siafem bit = null
,	@bl_transmitir_siafisico bit = null
,	@ds_autorizado_supra_folha varchar( 4 ) = null
,	@cd_especificacao_despesa varchar( 3 ) = null
,	@ds_especificacao_despesa varchar( 711 ) = null
,	@cd_autorizado_assinatura varchar( 5 ) = null
,	@cd_autorizado_grupo varchar( 1 ) = null
,	@cd_autorizado_orgao varchar( 2)  = null
,	@nm_autorizado_assinatura varchar( 55 ) = null
,	@ds_autorizado_cargo varchar( 55 ) = null
,	@cd_examinado_assinatura varchar( 5 ) = null
,	@cd_examinado_grupo varchar( 1 ) = null
,	@cd_examinado_orgao varchar( 2 ) = null
,	@nm_examinado_assinatura varchar( 55 ) = null
,	@ds_examinado_cargo varchar( 55 ) = null
,	@cd_responsavel_assinatura varchar( 5 ) = null
,	@cd_responsavel_grupo varchar( 1 ) = null
,	@cd_responsavel_orgao varchar( 2 ) = null
,	@nm_responsavel_assinatura varchar( 55 ) = null
,	@ds_responsavel_cargo varchar( 55 ) = null
,	@dt_emissao_cancelamento date = null
,	@ds_status_siafem_siafisico varchar( 1 ) = null
,	@ds_status_prodesp varchar( 1 ) = null
,	@ds_status_documento bit = null
,	@dt_transmissao_prodesp date = null
,	@dt_transmissao_siafem_siafisico date = null
,	@dt_cadastramento date = null
,	@bl_cadastro_completo bit = null
as
begin

	set nocount on;
	
	begin transaction
		
		insert into reserva.tb_cancelamento (
			id_fonte
		,	id_estrutura
		,	id_programa
		,	id_regional
		,	cd_reserva
		,	cd_contrato
		,	cd_processo
		,	nr_cancelamento_prodesp
		,	nr_cancelamento_siafem_siafisico
		,	cd_obra
		,	nr_oc
		,	cd_ugo
		,	cd_uo
		,	cd_evento
		,	nr_ano_exercicio
		,	cd_origem_recurso
		,	cd_destino_recurso
		,	ds_observacao
		,	fg_transmitido_prodesp
		,	fg_transmitido_siafem
		,	fg_transmitido_siafisico
		,	bl_transmitir_prodesp
		,	bl_transmitir_siafem
		,	bl_transmitir_siafisico
		,	ds_autorizado_supra_folha
		,	cd_especificacao_despesa
		,	ds_especificacao_despesa
		,	cd_autorizado_assinatura
		,	cd_autorizado_grupo
		,	cd_autorizado_orgao
		,	nm_autorizado_assinatura
		,	ds_autorizado_cargo
		,	cd_examinado_assinatura
		,	cd_examinado_grupo
		,	cd_examinado_orgao
		,	nm_examinado_assinatura
		,	ds_examinado_cargo
		,	cd_responsavel_assinatura
		,	cd_responsavel_grupo
		,	cd_responsavel_orgao
		,	nm_responsavel_assinatura
		,	ds_responsavel_cargo
		,	dt_emissao_cancelamento
		,	ds_status_siafem_siafisico
		,	ds_status_prodesp
		,	ds_status_documento
		,	dt_transmissao_prodesp
		,	dt_transmissao_siafem_siafisico
		,	dt_cadastramento
		,	bl_cadastro_completo
		)
		values (
			@id_fonte
		,	@id_estrutura
		,	@id_programa
		,	@id_regional
		,	@cd_reserva
		,	@cd_contrato
		,	@cd_processo
		,	@nr_cancelamento_prodesp
		,	@nr_cancelamento_siafem_siafisico
		,	@cd_obra
		,	@nr_oc
		,	@cd_ugo
		,	@cd_uo
		,	@cd_evento
		,	@nr_ano_exercicio
		,	@cd_origem_recurso
		,	@cd_destino_recurso
		,	@ds_observacao
		,	@fg_transmitido_prodesp
		,	@fg_transmitido_siafem
		,	@fg_transmitido_siafisico
		,	@bl_transmitir_prodesp
		,	@bl_transmitir_siafem
		,	@bl_transmitir_siafisico
		,	@ds_autorizado_supra_folha
		,	@cd_especificacao_despesa
		,	@ds_especificacao_despesa
		,	@cd_autorizado_assinatura
		,	@cd_autorizado_grupo
		,	@cd_autorizado_orgao
		,	@nm_autorizado_assinatura
		,	@ds_autorizado_cargo
		,	@cd_examinado_assinatura
		,	@cd_examinado_grupo
		,	@cd_examinado_orgao
		,	@nm_examinado_assinatura
		,	@ds_examinado_cargo
		,	@cd_responsavel_assinatura
		,	@cd_responsavel_grupo
		,	@cd_responsavel_orgao
		,	@nm_responsavel_assinatura
		,	@ds_responsavel_cargo
		,	@dt_emissao_cancelamento
		,	'N'
		,	'N'
		,	@ds_status_documento
		,	@dt_transmissao_prodesp
		,	@dt_transmissao_siafem_siafisico
		,	@dt_cadastramento
		,	@bl_cadastro_completo
		);

	commit

	select @@identity

end