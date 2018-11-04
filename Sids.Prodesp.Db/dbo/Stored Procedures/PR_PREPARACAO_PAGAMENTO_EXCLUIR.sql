-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 08/08/2017
-- Description: Procedure para exclusão de preparacao pagamento 
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PREPARACAO_PAGAMENTO_EXCLUIR]   
	@id_preparacao_pagamento int = NULL
AS  
BEGIN  

	SET NOCOUNT ON;  
	
	DELETE FROM [contaunica].[tb_preparacao_pagamento]
	WHERE id_preparacao_pagamento = @id_preparacao_pagamento;

	SELECT @@ROWCOUNT;

END