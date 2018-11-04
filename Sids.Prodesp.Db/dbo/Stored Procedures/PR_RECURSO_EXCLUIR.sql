
-- ===================================================================  
-- Author:		Bruno Destro (FI054)  
-- Create date: 23/11/2012  
-- Description: Procedure para exclusão de Usuários da base de dados  
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RECURSO_EXCLUIR]   
	@id_recurso     INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  
	
	declare @id_recurso_acao int;

	Select id_recurso_acao FROM seguranca.tb_recurso_acao
	 WHERE id_recurso =  @id_recurso;

	DELETE FROM seguranca.tb_perfil_acao
	 WHERE id_recurso_acao in (Select id_recurso_acao FROM seguranca.tb_recurso_acao
								WHERE id_recurso =  @id_recurso);
	
	DELETE FROM seguranca.tb_recurso_acao
	 WHERE id_recurso =  @id_recurso;

	 
	DELETE FROM seguranca.tb_log_aplicacao
	 WHERE id_recurso =  @id_recurso;
	 
	DELETE FROM seguranca.tb_menu_item
	 WHERE id_recurso =  @id_recurso;

	DELETE FROM seguranca.tb_recurso
	 WHERE id_recurso =  @id_recurso;
  
END