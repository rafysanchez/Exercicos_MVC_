
-- ==============================================================
-- Author:		Luis Santos
-- Create date: 19/09/2016
-- Description:	Procedure para consulta de Area
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_AREA_CONSULTAR]
 @id_area int = null,
 @ds_area varchar(100) = null
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT area.[id_area]
      ,area.[ds_area]
  FROM [seguranca].[tb_area](nolock)  area
  where (area.ds_area = @ds_area or @ds_area is null)
  and (area.id_area = @id_area or isnull(@id_area,0) = 0)
  order by area.ds_area

END