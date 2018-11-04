-- ===================================================================    
-- Author:		Alessandro de Santanao
-- Create date: 31/07/2018
-- Description: Procedure para salvar ou alterar Movimentação Orçamentaria
-- ===================================================================  
CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_SALVAR] 
	@id_movimentacao_orcamentaria int =NULL,
	@nr_agrupamento_movimentacao int =NULL,
	@nr_siafem varchar(15) =NULL,
	@tb_regional_id_regional int= NULL,
	@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao int =NULL,
	@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int =NULL,
	@cd_unidade_gestora_emitente int =NULL,
	@cd_gestao_emitente int =NULL,
	@nr_ano_exercicio int =NULL,
	@fg_transmitido_siafem char(1) =NULL,
	@bl_transmitido_siafem bit =NULL,
	@bl_transmitir_siafem bit =NULL,
	@dt_trasmitido_siafem datetime =NULL,
	@fg_transmitido_prodesp char(1) =NULL,
	@bl_transmitido_prodesp bit =NULL,
	@bl_transmitir_prodesp bit =NULL,
	@dt_trasmitido_prodesp datetime= NULL,
	@ds_msgRetornoProdesp varchar(140)= NULL,
	@ds_msgRetornoSiafem varchar(140)= NULL,
	@bl_cadastro_completo bit =NULL,
	@dt_cadastro datetime =NULL,
	@tb_programa_id_programa int =NULL    ,
    @tb_fonte_id_fonte varchar(10)= NULL ,   
    @tb_estrutura_id_estrutura int= NULL    


as  
begin

	set nocount on;

	if exists (
		select	1 
		from	[movimentacao].[tb_movimentacao_orcamentaria]  (nolock)
		where	id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria
	)
	begin

	 update [movimentacao].[tb_movimentacao_orcamentaria] set
	nr_siafem  = @nr_siafem,
	tb_regional_id_regional =nullif(@tb_regional_id_regional,0) ,
	tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao = @tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao ,
	tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria,
	cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente ,
	cd_gestao_emitente = @cd_gestao_emitente,
	nr_ano_exercicio  = @nr_ano_exercicio,
	fg_transmitido_siafem = @fg_transmitido_siafem,
	bl_transmitido_siafem =@bl_transmitido_siafem,
	bl_transmitir_siafem = @bl_transmitir_siafem,
	dt_trasmitido_siafem =    @dt_trasmitido_siafem,
	fg_transmitido_prodesp =@fg_transmitido_prodesp,
	bl_transmitido_prodesp =@bl_transmitido_prodesp,
	bl_transmitir_prodesp =@bl_transmitir_prodesp,
	dt_trasmitido_prodesp =@dt_trasmitido_prodesp,
	ds_msgRetornoProdesp =@ds_msgRetornoProdesp,
	ds_msgRetornoSiafem =@ds_msgRetornoSiafem,
	bl_cadastro_completo =@bl_cadastro_completo,
	tb_programa_id_programa = @tb_programa_id_programa,
	tb_fonte_id_fonte = @tb_fonte_id_fonte,
	tb_estrutura_id_estrutura = @tb_estrutura_id_estrutura


		where	id_movimentacao_orcamentaria = @id_movimentacao_orcamentaria;

		select @id_movimentacao_orcamentaria;

	end
	else
	begin

	insert into   [movimentacao].[tb_movimentacao_orcamentaria]    
	        ([nr_agrupamento_movimentacao]
           ,[nr_siafem]
           ,[tb_regional_id_regional]
           ,[tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao]
           ,[tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria]
           ,[cd_unidade_gestora_emitente]
           ,[cd_gestao_emitente]
           ,[nr_ano_exercicio]
           ,[fg_transmitido_siafem]
           ,[bl_transmitido_siafem]
		   ,[bl_transmitir_siafem]
           ,[dt_trasmitido_siafem]
           ,[fg_transmitido_prodesp]
           ,[bl_transmitido_prodesp]
		   ,[bl_transmitir_prodesp]
           ,[dt_trasmitido_prodesp]
           ,[ds_msgRetornoProdesp]
           ,[ds_msgRetornoSiafem]
           ,[bl_cadastro_completo]
		   ,[dt_cadastro]
		   ,[tb_programa_id_programa]
		   ,[tb_fonte_id_fonte]
		   ,[tb_estrutura_id_estrutura])
     VALUES
	 (
	nullif(@nr_agrupamento_movimentacao,0) ,
	@nr_siafem ,
	@tb_regional_id_regional ,
	@tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao ,
	@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria ,
	@cd_unidade_gestora_emitente ,
	@cd_gestao_emitente ,
	@nr_ano_exercicio ,
	'N' ,
	@bl_transmitido_siafem ,
	@bl_transmitir_siafem ,
	@dt_trasmitido_siafem ,
	'N' ,
	@bl_transmitido_prodesp ,
	@bl_transmitir_prodesp ,
	@dt_trasmitido_prodesp ,
	@ds_msgRetornoProdesp ,
	@ds_msgRetornoSiafem ,
	@bl_cadastro_completo ,
	@dt_cadastro,
	@tb_programa_id_programa,
	@tb_fonte_id_fonte,
	@tb_estrutura_id_estrutura
	 )

		select scope_identity();

	end

end