
-- ==============================================================    
-- Author:  Jose Antonio
-- Create date: 15/01/2018
-- Description: Procedure para Alterar tipo de despesa
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_TIPO_DESPESA_ALTERAR]
   @id_despesa_tipo int,
   @cd_despesa_tipo int,
   @ds_despesa_tipo varchar(50)
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE [pagamento].[tb_despesa_tipo] SET 
	cd_despesa_tipo = @cd_despesa_tipo,
	ds_despesa_tipo = @ds_despesa_tipo
	WHERE id_despesa_tipo = @id_despesa_tipo
END