
-- =====================================================================
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description:	Procedure para inclusão de credores no BD
-- =====================================================================

CREATE PROCEDURE [dbo].[PR_CREDOR_INCLUIR] 
  	 @nm_prefeitura			varchar(100)		= null
	,@cd_cpf_cnpj		varchar(14)				= null
	,@bl_conveniado		bit				= null
	,@bl_base_calculo		bit				= null
	,@nm_reduzido_credor		varchar(100)				= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [contaunica].[tb_Credor]
		(nm_prefeitura
           ,cd_cpf_cnpj_ug_credor
           ,bl_conveniado
           ,bl_base_calculo
           ,nm_reduzido_credor)
	VALUES
        (@nm_prefeitura
           ,@cd_cpf_cnpj
           ,@bl_conveniado
           ,@bl_base_calculo
           ,@nm_reduzido_credor)
END