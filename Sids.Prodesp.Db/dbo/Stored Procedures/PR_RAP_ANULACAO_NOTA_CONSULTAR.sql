-- ==============================================================
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017
-- Description:	Procedure para consulta de notas para anulação
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RAP_ANULACAO_NOTA_CONSULTAR]
	@id_rap_anulacao_nota int = null
,	@tb_rap_anulacao_id_rap_anulacao int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_rap_anulacao_nota as id_subempenho_nota
		,	tb_rap_anulacao_id_rap_anulacao as tb_subempenho_id_subempenho
		,	cd_nota
		,	nr_ordem
		FROM pagamento.tb_rap_anulacao_nota (nolock)
		WHERE 
	  		( nullif( @id_rap_anulacao_nota, 0 ) is null or id_rap_anulacao_nota = @id_rap_anulacao_nota ) and
			( tb_rap_anulacao_id_rap_anulacao = @tb_rap_anulacao_id_rap_anulacao)
END