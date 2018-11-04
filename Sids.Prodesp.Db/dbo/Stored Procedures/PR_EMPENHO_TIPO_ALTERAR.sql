-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 21/12/2016
-- Description: Procedure para alteração de tipo_reserva na base de dados    
CREATE PROCEDURE [dbo].[PR_EMPENHO_TIPO_ALTERAR]      
	@id_empenho_tipo int    
,	@ds_empenho_tipo VARCHAR(100)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE empenho.tb_empenho_tipo
	SET 
		ds_empenho_tipo			= @ds_empenho_tipo		  
	WHERE
		id_empenho_tipo			= @id_empenho_tipo
END