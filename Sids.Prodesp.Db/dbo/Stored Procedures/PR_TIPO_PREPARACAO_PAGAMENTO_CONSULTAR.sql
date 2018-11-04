-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 08/08/2017
-- Description: Procedure para consulta de tipo de preparacao_pagamento
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_TIPO_PREPARACAO_PAGAMENTO_CONSULTAR]  
	@id_tipo_preparacao_pagamento	INT = 0  
,	@ds_tipo_preparacao_pagamento	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		[id_tipo_preparacao_pagamento]
      ,[ds_tipo_preparacao_pagamento]
  FROM [contaunica].[tb_tipo_preparacao_pagamento] ( NOLOCK )
	WHERE 
       ( @id_tipo_preparacao_pagamento = 0 or id_tipo_preparacao_pagamento = @id_tipo_preparacao_pagamento ) and
	   ( @ds_tipo_preparacao_pagamento is null or ds_tipo_preparacao_pagamento = @ds_tipo_preparacao_pagamento )
	   ORDER BY ds_tipo_preparacao_pagamento
END