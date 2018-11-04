CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_DE_OB_EXCLUIR]   
	@id_autorizacao_ob int
AS  
BEGIN  

	SET NOCOUNT ON;  

	DELETE FROM [contaunica].[tb_autorizacao_ob_itens]
	WHERE id_autorizacao_ob = @id_autorizacao_ob

	DELETE FROM [contaunica].[tb_autorizacao_ob]
	WHERE id_autorizacao_ob = @id_autorizacao_ob

	SELECT @@ROWCOUNT;

END