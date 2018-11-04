
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Alterar uma Despesa
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_DESPESA_ALTERAR]
	@id_despesa int = null
	,@id_despesa_tipo int = null
	,@id_nl_parametrizacao int = null
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE
		[pagamento].[tb_despesa] 
	SET 
		id_despesa_tipo = @id_despesa_tipo,
		id_nl_parametrizacao = @id_nl_parametrizacao
	WHERE
		id_despesa = @id_despesa
END