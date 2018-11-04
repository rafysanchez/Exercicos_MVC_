
-- ==============================================================
-- Author:		Luis Santos
-- Create date: 29/08/2016
-- Description:	Procedure para consulta de log
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_RESULTADO_CONSULTAR]
@ds_resultado varchar(20) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT res.[id_resultado]
      ,res.[ds_resultado]
  FROM [seguranca].[tb_resultado] res
  where res.ds_resultado = @ds_resultado or @ds_resultado is null


END