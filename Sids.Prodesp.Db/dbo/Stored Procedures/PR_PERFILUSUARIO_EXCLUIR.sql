
-- ==========================================================================  
-- Author:  Luis Fernando
-- Create date: 30/10/2016 
-- Description: Procedure para exclusão do relacionamento Perfil x Usuário
-- ========================================================================== 

CREATE PROCEDURE [dbo].[PR_PERFILUSUARIO_EXCLUIR]
	@id_perfil_usuario	INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  


	DELETE from seguranca.tb_perfil_usuario
	 WHERE id_perfil_usuario = @id_perfil_usuario;
  
END