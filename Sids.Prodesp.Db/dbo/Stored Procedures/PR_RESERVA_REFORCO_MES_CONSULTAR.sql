
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 01/11/2016
-- Description:	Procedure para consulta de Valor de reforco cadastrados
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RESERVA_REFORCO_MES_CONSULTAR]   
	@id_reforco_mes				int = null,
	@id_reforco			int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [id_reforco_mes]
		  ,[id_reforco]
		  ,[ds_mes]
		  ,[vr_mes]
		FROM [reserva].[tb_reforco_mes](nolock)	  A
	  WHERE ([id_reforco_mes] = @id_reforco_mes OR ISNULL(@id_reforco_mes,0) = 0)
			AND ([id_reforco] = @id_reforco)
	ORDER BY A.[ds_mes] 
END