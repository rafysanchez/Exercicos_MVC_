
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Excluir parametrização de NL
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_EXCLUIR]
	@id_nl_parametrizacao int
AS  
BEGIN  
	SET NOCOUNT ON;  

	DELETE FROM [pagamento].[tb_nl_parametrizacao]
	WHERE id_nl_parametrizacao =  @id_nl_parametrizacao;
END