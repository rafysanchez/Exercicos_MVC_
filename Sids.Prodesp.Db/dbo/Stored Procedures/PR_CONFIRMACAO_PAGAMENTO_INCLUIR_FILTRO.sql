
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_FILTRO](
@id_confirmacao_pagamento_tipo int,
@nr_conta varchar(20) = NULL,
@dt_preparacao datetime  = NULL,
@id_tipo_pagamento int  = NULL,
@dt_confirmacao datetime  = NULL,
@id_tipo_documento int  = NULL,
@nr_documento varchar(19)  = NULL
)
AS
BEGIN

	DECLARE @nr_agrupamento_novo INT;

	SET @nr_agrupamento_novo = COALESCE((SELECT MAX(nr_agrupamento) FROM pagamento.tb_confirmacao_pagamento), 0) + 1

	BEGIN TRANSACTION
	SET NOCOUNT ON;

	INSERT INTO pagamento.tb_confirmacao_pagamento (
	id_confirmacao_pagamento_tipo,
	nr_conta,
	dt_preparacao,
	id_tipo_pagamento,
	dt_confirmacao,
    id_tipo_documento,
	nr_documento,
	nr_agrupamento
	)
	VALUES(
	@id_confirmacao_pagamento_tipo,
	@nr_conta,
	@dt_preparacao,
	@id_tipo_pagamento,
	@dt_confirmacao,
	@id_tipo_documento,
	@nr_documento,
	@nr_agrupamento_novo
	)
	COMMIT
	SELECT SCOPE_IDENTITY();
END

-----------------------------------------------------------------------------------------