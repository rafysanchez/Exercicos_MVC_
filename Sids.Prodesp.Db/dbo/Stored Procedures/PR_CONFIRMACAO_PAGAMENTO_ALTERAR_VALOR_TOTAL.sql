 -- ==============================================================
-- Author:  JOSE BRAZ
-- Create date: 31/01/2018
-- Description: Procedure para Alterar pagamento.tb_confirmacao_pagamento
-- ==============================================================
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_ALTERAR_VALOR_TOTAL]
@id_confirmacao_pagamento int  ,
@vr_total_confirmado decimal(8,2) = NULL
AS
BEGIN
 SET NOCOUNT ON;

UPDATE
pagamento.tb_confirmacao_pagamento
 SET
vr_total_confirmado = @vr_total_confirmado
WHERE
id_confirmacao_pagamento = @id_confirmacao_pagamento
END

-----------------------------------------------------------------------------------------