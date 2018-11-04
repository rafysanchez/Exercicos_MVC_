-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para exclusão de codigo de barras
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_CODIGO_DE_BARRAS_EXCLUIR]   
	@id_codigo_de_barras int = NULL
AS  
BEGIN  
	SET NOCOUNT ON;  
		
	DELETE FROM [contaunica].tb_codigo_de_barras_boleto
	WHERE id_codigo_de_barras = @id_codigo_de_barras;

	DELETE FROM [contaunica].tb_codigo_de_barras_taxas
	WHERE id_codigo_de_barras = @id_codigo_de_barras;

	DELETE FROM [contaunica].[tb_codigo_de_barras]
	WHERE id_codigo_de_barras = @id_codigo_de_barras;

	SELECT @@ROWCOUNT;

END