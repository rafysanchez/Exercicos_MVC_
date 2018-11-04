-- ==============================================================    
-- Author:  Rodrigo Cesar de Freitas
-- Create date: 21/12/2016
-- Description:	Procedure para consulta tipo de empenho
 CREATE PROCEDURE [dbo].[PR_EMPENHO_TIPO_CONSULTAR]
	@id_empenho_tipo int = 0
,	@ds_empenho_tipo varchar(50) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		id_empenho_tipo
	,	ds_empenho_tipo
	FROM empenho.tb_empenho_tipo (nolock)
	where 
		( @id_empenho_tipo = 0 or id_empenho_tipo = @id_empenho_tipo ) and
		( @ds_empenho_tipo is null or ds_empenho_tipo = @ds_empenho_tipo )
	order by 
		id_empenho_tipo

END;