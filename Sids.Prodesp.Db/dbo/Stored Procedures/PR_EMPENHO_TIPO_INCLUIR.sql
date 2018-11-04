-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 21/12/2016
-- Description: Procedure para Incluir tipo de empenho   
CREATE PROCEDURE [dbo].[PR_EMPENHO_TIPO_INCLUIR]    
   @ds_empenho_tipo  VARCHAR(100)
AS
begin

	SET NOCOUNT ON;  

	BEGIN TRANSACTION
	 
		INSERT INTO empenho.tb_empenho_tipo ( ds_empenho_tipo )  
		VALUES ( @ds_empenho_tipo )  
  
	COMMIT

	SELECT SCOPE_IDENTITY();

end;