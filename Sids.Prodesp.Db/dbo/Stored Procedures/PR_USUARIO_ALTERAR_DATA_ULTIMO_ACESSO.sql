
-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 20/10/2016  
-- Description: Procedure para atualização da data de ultimo acesso 
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_USUARIO_ALTERAR_DATA_ULTIMO_ACESSO]
	@id_usuario					INT,  
	@dt_ultimo_acesso			DATETIME		= NULL
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_usuario
	  SET  dt_ultimo_acesso  = ISNULL(@dt_ultimo_acesso, GETDATE())
		  ,nr_tentativa_login_invalidas = 0
		  ,ds_token = NULL
	 WHERE id_usuario			= @id_usuario
    
END