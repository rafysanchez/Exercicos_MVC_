
CREATE PROCEDURE dbo.PR_CONFIRMACAO_PAGAMENTO_DELETE
(	
      @id_confirmacao_pagamento int = null,
	  @id_confirmacao_pagamento_item int = null
)
As
Begin
BEGIN TRANSACTION  
SET NOCOUNT ON;  

Begin
	DELETE FROM [pagamento].[tb_confirmacao_pagamento]
    WHERE id_confirmacao_pagamento =  @id_confirmacao_pagamento

End

Begin
	 DELETE FROM [pagamento].[tb_confirmacao_pagamento_item]
     WHERE id_confirmacao_pagamento =  @id_confirmacao_pagamento
	 And id_confirmacao_pagamento_item = @id_confirmacao_pagamento_item
End
COMMIT
End