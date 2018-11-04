-- ==============================================================
-- Author:		Carlos Henrique Magalhães
-- Create date: 31/03/2017
-- Description:	Procedure para consulta de notas para subempenho
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RAP_INSCRICAO_NOTA_CONSULTAR]
	@id_rap_inscricao_nota int = null
,	@tb_rap_inscricao_id_rap_inscricao int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_rap_inscricao_nota as id_subempenho_nota
		,	tb_rap_inscricao_id_rap_inscricao as tb_subempenho_id_subempenho
		,	cd_nota
		,	nr_ordem
		FROM pagamento.tb_rap_inscricao_nota (nolock)
		WHERE 
	  		( nullif( @id_rap_inscricao_nota, 0 ) is null or id_rap_inscricao_nota = @id_rap_inscricao_nota ) and
			( tb_rap_inscricao_id_rap_inscricao = @tb_rap_inscricao_id_rap_inscricao )
END