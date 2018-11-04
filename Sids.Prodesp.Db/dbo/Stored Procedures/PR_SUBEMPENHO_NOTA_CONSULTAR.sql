-- ==============================================================
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 09/02/2017
-- Description:	Procedure para consulta de notas para subempenho
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_SUBEMPENHO_NOTA_CONSULTAR]
	@id_subempenho_nota int = null
,	@tb_subempenho_id_subempenho int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_subempenho_nota
		,	tb_subempenho_id_subempenho
		,	cd_nota
		,	nr_ordem
		FROM pagamento.tb_subempenho_nota (nolock)
		WHERE 
	  		( nullif( @id_subempenho_nota, 0 ) is null or id_subempenho_nota = @id_subempenho_nota ) and
			( tb_subempenho_id_subempenho = @tb_subempenho_id_subempenho )
END