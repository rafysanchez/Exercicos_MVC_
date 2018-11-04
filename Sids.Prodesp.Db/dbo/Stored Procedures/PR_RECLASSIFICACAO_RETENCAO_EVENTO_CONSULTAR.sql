-- ===================================================================      
-- Author:		Luis Fernando
-- Create date: 11/07/2017
-- Description:	Procedure para consulta de itens para reclassificacao_retencao
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_RECLASSIFICACAO_RETENCAO_EVENTO_CONSULTAR]
	@id_reclassificacao_retencao_evento int = NULL
,	@id_reclassificacao_retencao int = NULL
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_reclassificacao_retencao_evento
		,	id_reclassificacao_retencao
		,	cd_fonte
		,	cd_evento
		,	cd_classificacao
		,	ds_inscricao
		,	vl_evento
		FROM contaunica.tb_reclassificacao_retencao_evento (nolock)
		WHERE 
	  		( nullif( @id_reclassificacao_retencao_evento, 0 ) is null or id_reclassificacao_retencao_evento = @id_reclassificacao_retencao_evento )
		and	( nullif( @id_reclassificacao_retencao, 0 ) is null or id_reclassificacao_retencao = @id_reclassificacao_retencao )
		ORDER BY 
			id_reclassificacao_retencao_evento
END