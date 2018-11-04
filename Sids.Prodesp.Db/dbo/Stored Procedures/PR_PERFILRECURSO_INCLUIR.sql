
-- ======================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description: Procedure para alteração de uma relação Perfil x Recurso
-- ======================================================================
CREATE PROCEDURE [dbo].[PR_PERFILRECURSO_INCLUIR]
	@id_perfil				INT,
	@id_recurso				INT,
	@bl_ativo				BIT = 1
AS  
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  

	INSERT INTO seguranca.tb_perfil_recurso(
			bl_ativo
			,id_perfil
			,id_recurso
			,dt_criacao)
	VALUES (@bl_ativo,
			@id_perfil,
			@id_recurso,
			getdate());
		
COMMIT;
	SELECT SCOPE_IDENTITY();