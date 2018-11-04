
-- ======================================================================
-- Author:		Luis Fernando
-- Create date: 29/02/2016
-- Description: Procedure para alteração de uma relação Perfil x Recurso
-- ======================================================================
CREATE PROCEDURE [dbo].[PR_PERFILRECURSO_ALTERAR]
	@id_perfil_recurso		INT,
	@id_perfil				INT,
	@id_recurso				INT,
	@bl_ativo				BIT = 1
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  

	 UPDATE seguranca.tb_perfil_recurso
		SET bl_ativo		     = @bl_ativo,
			id_perfil			 = @id_perfil,
			id_recurso			 = @id_recurso
	WHERE   id_perfil_recurso    = @id_perfil_recurso
		
END