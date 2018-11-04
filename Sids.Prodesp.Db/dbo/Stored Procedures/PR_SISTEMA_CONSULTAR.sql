
-- ==============================================================
-- Author:		Luis Santos
-- Create date: 19/09/2016
-- Description:	Procedure para consulta de Sistema
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_SISTEMA_CONSULTAR]
 @ds_sistema varchar(100) = null,
 @id_sistema int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT sis.[id_sistema]
      ,sis.[ds_sistema]
  FROM [seguranca].[tb_sistema] sis
  where (sis.[ds_sistema] = @ds_sistema or @ds_sistema is null)
  and (sis.[id_sistema] = @id_sistema or isnull(@id_sistema,0) = 0)
  order by sis.ds_sistema

END