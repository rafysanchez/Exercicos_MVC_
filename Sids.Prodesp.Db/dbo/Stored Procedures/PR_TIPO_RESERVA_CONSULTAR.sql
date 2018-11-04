
-- ==============================================================
-- Author:		Luis Santos
-- Create date: 01/11/2016
-- Description:	Procedure para consulta Tipo de Reserva
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_TIPO_RESERVA_CONSULTAR]
	@id_tipo_reserva int = null,
	@ds_tipo_reserva varchar(50) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT A.[id_tipo_reserva]
      ,A.[ds_tipo_reserva]
  FROM [reserva].[tb_tipo_reserva](nolock) A
  where (A.[id_tipo_reserva] = @id_tipo_reserva or isnull(@id_tipo_reserva,0) = 0)
  order by a.ds_tipo_reserva

END