
-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 22/02/2016  
-- Description: Procedure para alteração de Perfil na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_PERFIL_ALTERAR]   
	@id_perfil				INT,  
	@ds_perfil				VARCHAR(100),
	@ds_detalhe				VARCHAR(200) = null,
	@bl_ativo				BIT = 1
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
   
	UPDATE seguranca.tb_perfil
	  SET  ds_perfil				= @ds_perfil
		  ,bl_ativo					= @bl_ativo 
		  ,ds_detalhe				= @ds_detalhe 
	 WHERE id_perfil				= @id_perfil
    
END