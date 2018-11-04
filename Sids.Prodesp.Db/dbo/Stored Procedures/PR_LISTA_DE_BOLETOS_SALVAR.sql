-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para salvar ou alterar lista de boletos
-- ===================================================================  
CREATE procedure [dbo].[PR_LISTA_DE_BOLETOS_SALVAR] 
	@id_lista_de_boletos int= NULL
    ,@id_regional smallint = NULL
	,@id_tipo_documento int = NULL
	,@dt_cadastro date = NULL
	,@nr_siafem_siafisico varchar(11) = NULL
	,@nr_contrato varchar(13) = NULL
	,@nr_documento varchar(19) = NULL
	,@cd_unidade_gestora varchar(6) = NULL
	,@cd_gestao varchar(5) = NULL
	,@cd_aplicacao_obra varchar(140) = NULL
	,@dt_emissao date = NULL
	,@nr_cnpj_favorecido varchar(14) = NULL
	,@ds_nome_da_lista varchar(50) = NULL
	,@ds_copiar_da_lista varchar(1) = NULL
	,@nr_total_de_credores int = NULL
	,@vl_total_da_lista decimal(18,2) = NULL
	,@fl_sistema_siafem_siafisico bit = NULL
	,@cd_transmissao_status_siafem_siafisico char(1) = NULL
	,@fl_transmissao_transmitido_siafem_siafisico bit = NULL
	,@dt_transmissao_transmitido_siafem_siafisico date = NULL
	,@ds_transmissao_mensagem_siafem_siafisico varchar(140) = NULL
	,@bl_cadastro_completo bit = NULL
	,@id_codigo_de_barras int = NULL
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_lista_de_boletos](nolock)
		where	id_lista_de_boletos = @id_lista_de_boletos
	)
	begin

		update [contaunica].[tb_lista_de_boletos] set 
			id_tipo_documento	=	nullif(@id_tipo_documento,0)
			,id_regional	=	nullif(@id_regional,0)
			,nr_siafem_siafisico	=	@nr_siafem_siafisico
			,[nr_contrato] = @nr_contrato
			,[nr_documento] = @nr_documento
			,[cd_unidade_gestora] = @cd_unidade_gestora
			,[cd_gestao] = @cd_gestao
			,[cd_aplicacao_obra] = @cd_aplicacao_obra
			,[dt_emissao] = @dt_emissao
			,[nr_cnpj_favorecido] = @nr_cnpj_favorecido
			,[ds_nome_da_lista] = @ds_nome_da_lista
			,[ds_copiar_da_lista] = @ds_copiar_da_lista
			,[nr_total_de_credores] = @nr_total_de_credores
			,[vl_total_da_lista] = @vl_total_da_lista
			,[fl_sistema_siafem_siafisico] = @fl_sistema_siafem_siafisico
			,[cd_transmissao_status_siafem_siafisico] = @cd_transmissao_status_siafem_siafisico
			,[fl_transmissao_transmitido_siafem_siafisico] = @fl_transmissao_transmitido_siafem_siafisico
			,[dt_transmissao_transmitido_siafem_siafisico] = @dt_transmissao_transmitido_siafem_siafisico
			,[ds_transmissao_mensagem_siafem_siafisico] = @ds_transmissao_mensagem_siafem_siafisico
			,[bl_cadastro_completo] = @bl_cadastro_completo
			,[id_codigo_de_barras] = @id_codigo_de_barras

		where	id_lista_de_boletos = @id_lista_de_boletos;

		select @id_lista_de_boletos;

	end
	else
	begin

		Select	@id_codigo_de_barras = ISNULL(MAX(id_codigo_de_barras), 0) + 1
		From	[contaunica].[tb_codigo_de_barras]
		
		insert into [contaunica].[tb_lista_de_boletos] (
			[id_regional]
           ,[id_tipo_documento]
           ,[dt_cadastro]
           ,[nr_siafem_siafisico]
           ,[nr_contrato]
           ,[nr_documento]
           ,[cd_unidade_gestora]
           ,[cd_gestao]
           ,[cd_aplicacao_obra]
           ,[dt_emissao]
           ,[nr_cnpj_favorecido]
           ,[ds_nome_da_lista]
           ,[ds_copiar_da_lista]
           ,[nr_total_de_credores]
           ,[vl_total_da_lista]
           ,[fl_sistema_siafem_siafisico]
           ,[cd_transmissao_status_siafem_siafisico]
           ,[fl_transmissao_transmitido_siafem_siafisico]
           ,[dt_transmissao_transmitido_siafem_siafisico]
           ,[ds_transmissao_mensagem_siafem_siafisico]
           ,[bl_cadastro_completo]
		   ,[id_codigo_de_barras]
		   )
		values
		(
			
			nullif(@id_regional,0)
			,nullif(@id_tipo_documento,0)
			,getdate()
			,@nr_siafem_siafisico
			,@nr_contrato
			,@nr_documento
			,@cd_unidade_gestora 
			,@cd_gestao 
			,@cd_aplicacao_obra 
			,@dt_emissao
			,@nr_cnpj_favorecido 
			,@ds_nome_da_lista 
			,@ds_copiar_da_lista
			,@nr_total_de_credores
			,@vl_total_da_lista
			,@fl_sistema_siafem_siafisico 
			,'N' 
			,@fl_transmissao_transmitido_siafem_siafisico 
			,@dt_transmissao_transmitido_siafem_siafisico
			,@ds_transmissao_mensagem_siafem_siafisico
			,@bl_cadastro_completo 
			,@id_codigo_de_barras
		);

		select scope_identity();

	end

end