-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 26/01/2017
-- Description:	Procedure para consulta tipo de empenho
 CREATE PROCEDURE [dbo].[PR_EMPENHO_CANCELAMENTO_TIPO_CONSULTAR]
	@id_empenho_cancelamento_tipo int = 0
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		id_empenho_cancelamento_tipo
	,	ds_empenho_cancelamento_tipo
	,	nm_web_service
	,	fl_siafem
	FROM empenho.tb_empenho_cancelamento_tipo (nolock)
	where 
		( @id_empenho_cancelamento_tipo = 0 or id_empenho_cancelamento_tipo = @id_empenho_cancelamento_tipo )
	order by 
		ds_empenho_cancelamento_tipo

END;