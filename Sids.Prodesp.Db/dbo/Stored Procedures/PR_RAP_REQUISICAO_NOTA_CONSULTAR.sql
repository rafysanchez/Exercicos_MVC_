-- ==============================================================
-- Author:		Carlos Henrique Magalhães
-- Create date: 03/04/2017
-- Description:	Procedure para consulta de notas para requisição
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RAP_REQUISICAO_NOTA_CONSULTAR]
	@id_rap_requisicao_nota int = null
,	@tb_rap_requisicao_id_rap_requisicao int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_rap_requisicao_nota as id_subempenho_nota
		,	tb_rap_requisicao_id_rap_requisicao as tb_subempenho_id_subempenho
		,	cd_nota
		,	nr_ordem
		FROM pagamento.tb_rap_requisicao_nota (nolock)
		WHERE 
	  		( nullif( @id_rap_requisicao_nota, 0 ) is null or id_rap_requisicao_nota = @id_rap_requisicao_nota ) and
			( tb_rap_requisicao_id_rap_requisicao = @tb_rap_requisicao_id_rap_requisicao )
END