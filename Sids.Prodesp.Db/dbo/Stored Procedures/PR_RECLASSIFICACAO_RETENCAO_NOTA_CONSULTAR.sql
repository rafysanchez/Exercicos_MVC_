-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 11/07/20174
-- Description:	Procedure para consulta de notas para reclassificacao_retencao
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_NOTA_CONSULTAR]
	@id_reclassificacao_retencao_nota int = null
,	@id_reclassificacao_retencao int = null
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_reclassificacao_retencao_nota
		,	id_reclassificacao_retencao
		,	cd_nota
		,	nr_ordem
		FROM contaunica.tb_reclassificacao_retencao_nota (nolock)
		WHERE 
	  		( nullif( @id_reclassificacao_retencao_nota, 0 ) is null or id_reclassificacao_retencao_nota = @id_reclassificacao_retencao_nota ) and
			( id_reclassificacao_retencao = @id_reclassificacao_retencao )
END