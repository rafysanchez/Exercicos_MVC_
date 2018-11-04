
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 01/11/2016
-- Description:	Procedure para consulta de Valor de reservar cadastrados
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RESERVA_MES_CONSULTAR]   
	@id_reserva_mes				int = null,
	@id_reserva			int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [id_reserva_mes]
		  ,[id_reserva]
		  ,[ds_mes]
		  ,[vr_mes]
		FROM [reserva].[tb_reserva_mes](nolock)	  A
	  WHERE ([id_reserva_mes] = @id_reserva_mes OR ISNULL(@id_reserva_mes,0) = 0)
			AND ([id_reserva] = @id_reserva)
	ORDER BY A.[ds_mes] 
END