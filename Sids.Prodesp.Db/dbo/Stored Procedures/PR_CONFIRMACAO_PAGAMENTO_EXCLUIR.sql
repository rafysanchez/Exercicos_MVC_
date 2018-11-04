 -- ==============================================================
-- Author:  JOSE BRAZ
-- Create date: 31/01/2018
-- Description: Procedure para Excluir pagamento.tb_confirmacao_pagamento
-- ==============================================================
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_EXCLUIR]

            @id_confirmacao_pagamento int
            AS
            BEGIN
            SET NOCOUNT ON;

            DELETE FROM pagamento.tb_confirmacao_pagamento
            WHERE id_confirmacao_pagamento =  @id_confirmacao_pagamento;
            END

-----------------------------------------------------------------------------------------