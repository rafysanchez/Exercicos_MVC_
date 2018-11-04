-- ===================================================================      
-- Author:		Luis Fernando
-- Create date: 23/08/2017
-- Description:	Procedure para consulta de itens para programacao desembolso
-- ===================================================================      
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EVENTO_CONSULTAR]
	@id_programacao_desembolso_evento int = NULL
,	@id_programacao_desembolso int = NULL
AS
BEGIN
	SET NOCOUNT ON;

		SELECT
			id_programacao_desembolso_evento
		,	id_programacao_desembolso
		,	cd_fonte
		,	cd_evento
		,	cd_classificacao
		,	ds_inscricao
		,	vl_evento
		FROM contaunica.tb_programacao_desembolso_evento (nolock)
		WHERE 
	  		( nullif( @id_programacao_desembolso_evento, 0 ) is null or id_programacao_desembolso_evento = @id_programacao_desembolso_evento )
		and	( nullif( @id_programacao_desembolso, 0 ) is null or id_programacao_desembolso = @id_programacao_desembolso )
		ORDER BY 
			id_programacao_desembolso_evento
END