-- ===================================================================    
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 14/12/2016
-- Description: Procedure para alteração de tipo_reserva na base de dados    
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_TIPO_RESERVA_ALTERAR]      
	@id_tipo_reserva 				int,    
	@ds_tipo_reserva				VARCHAR(100)		
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE reserva.tb_tipo_reserva
	     SET ds_tipo_reserva			= @ds_tipo_reserva		  
	 WHERE   id_tipo_reserva			= @id_tipo_reserva
END