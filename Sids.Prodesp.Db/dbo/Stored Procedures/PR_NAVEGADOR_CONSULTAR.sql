
-- ==============================================================
-- Author:		Luis Santos
-- Create date: 29/08/2016
-- Description:	Procedure para consulta de log
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_NAVEGADOR_CONSULTAR]
 @ds_navegador varchar(20) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT nav.id_navegador
      ,nav.ds_navegador
  FROM [seguranca].[tb_navegador] nav
  Where nav.ds_navegador = @ds_navegador or @ds_navegador is null


END