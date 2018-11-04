
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 09/11/2016
-- Description:	Procedure liberar para acesso ao webservice prodesp
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_LIBERAR_CHAVE]
	@id_chave int
AS
BEGIN

	SET NOCOUNT ON;

	update [reserva].[tb_chave_cicsmo] Set [bl_disponivel] = 1,[nr_ranking] = [nr_ranking] + 1
	where id_chave = @id_chave;

	update [reserva].[tb_chave_cicsmo] Set [nr_ranking] = 0
	where id_chave = @id_chave
	and [nr_ranking] > 1000;
END