
CREATE PROCEDURE[dbo].[PR_CONFIRMACAO_PAGAMENTO_INCLUIR_TOTAIS](
@id_confirmacao_pagamento int,
@nr_fonte_lista varchar(50) = null,
@vr_total_fonte_lista decimal(8,2) = null,
@vr_total_confirmar_ir decimal(8,2) = null,
@vr_total_confirmar_issqn decimal(8,2) = null,
@vr_total_confirmar decimal(8,2) = null
)
AS
BEGIN

	BEGIN TRANSACTION
	SET NOCOUNT ON;

	INSERT INTO pagamento.tb_confirmacao_pagamento_totais (
	id_confirmacao_pagamento,
    nr_fonte_lista,
    vr_total_fonte_lista,
    vr_total_confirmar_ir,
    vr_total_confirmar_issqn,
    vr_total_confirmar
	)
	VALUES(
	 @id_confirmacao_pagamento,
    @nr_fonte_lista,
    @vr_total_fonte_lista,
    @vr_total_confirmar_ir,
    @vr_total_confirmar_issqn,
    @vr_total_confirmar
	)
	COMMIT
	SELECT SCOPE_IDENTITY();
END

-----------------------------------------------------------------------------------------