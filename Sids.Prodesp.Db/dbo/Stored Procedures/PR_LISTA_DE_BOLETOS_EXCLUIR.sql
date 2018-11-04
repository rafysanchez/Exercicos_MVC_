-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para exclusão de lista de boletos 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_LISTA_DE_BOLETOS_EXCLUIR]   
	@id_lista_de_boletos int = NULL
AS  
BEGIN  
	declare @ids table(id int);
	SET NOCOUNT ON;  

	insert into @ids(id)
	select id_codigo_de_barras from [contaunica].[tb_codigo_de_barras]
	WHERE id_lista_de_boletos = @id_lista_de_boletos;

	
	DELETE FROM [contaunica].tb_codigo_de_barras_boleto
	WHERE id_codigo_de_barras in (select id from @ids);

	DELETE FROM [contaunica].tb_codigo_de_barras_taxas
	WHERE id_codigo_de_barras in (select id from @ids);
	

	DELETE FROM [contaunica].[tb_codigo_de_barras]
	WHERE id_lista_de_boletos = @id_lista_de_boletos;

	DELETE FROM [contaunica].[tb_lista_de_boletos]
	WHERE id_lista_de_boletos = @id_lista_de_boletos;

	SELECT @@ROWCOUNT;

END