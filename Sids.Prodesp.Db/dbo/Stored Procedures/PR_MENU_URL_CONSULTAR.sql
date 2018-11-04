
-- ========================================================
-- Author:		Luis Fernando
-- Create date: 28/06/2017
-- Description:	Procedure para consulta de menu
-- ========================================================

Create PROCEDURE [dbo].[PR_MENU_URL_CONSULTAR]
	@id_menu_url		int				= null
as
begin
	SET NOCOUNT ON;
	
	select [id_menu_url]
      ,[ds_area]
      ,[ds_controller]
      ,[ds_action]
      ,[ds_url]
  FROM [seguranca].[tb_menu_url]
	where   (id_menu_url = @id_menu_url or isnull(@id_menu_url,0) = 0)
	
end