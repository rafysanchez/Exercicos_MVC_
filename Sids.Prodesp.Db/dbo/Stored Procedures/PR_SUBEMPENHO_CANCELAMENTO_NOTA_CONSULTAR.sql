-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/02/2017
-- Description:	Procedure para consulta de notas para subempenho
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_CANCELAMENTO_NOTA_CONSULTAR]
	@id_subempenho_cancelamento_nota int = null
,	@tb_subempenho_cancelamento_id_subempenho_cancelamento int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_subempenho_cancelamento_nota as id_subempenho_nota
		,	tb_subempenho_cancelamento_id_subempenho_cancelamento as tb_subempenho_id_subempenho
		,	cd_nota
		,	nr_ordem
		FROM pagamento.tb_subempenho_cancelamento_nota (nolock)
		WHERE 
	  		( nullif( @id_subempenho_cancelamento_nota, 0 ) is null or id_subempenho_cancelamento_nota = @id_subempenho_cancelamento_nota ) and
			( tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento )
END