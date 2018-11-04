
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 01/11/2016
-- Description:	Procedure para consulta de Valor de reforco cadastrados
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_RESERVA_CANCELAMENTO_MES_CONSULTAR]   
	@id_cancelamento_mes				int = null,
	@id_cancelamento			int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [id_cancelamento_mes]
		  ,[id_cancelamento]
		  ,[ds_mes]
		  ,[vr_mes]
		FROM [reserva].[tb_cancelamento_mes](nolock)	  A
	  WHERE ([id_cancelamento_mes] = @id_cancelamento_mes OR ISNULL(@id_cancelamento_mes,0) = 0)
			AND ([id_cancelamento] = @id_cancelamento)
	ORDER BY A.[ds_mes] 
END