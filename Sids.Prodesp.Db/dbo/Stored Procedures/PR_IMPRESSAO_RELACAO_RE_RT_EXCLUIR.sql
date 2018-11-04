-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para excluir Impressão de Relação RE e RT
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_EXCLUIR]
		@id_impressao_relacao_re_rt INT = NULL,
		@relRERT VARCHAR(11) = NULL
AS

BEGIN

	--DECLARE
	--@id_impressao_relacao_re_rt INT = 1,
	--@relRERT VARCHAR(11) = NULL

	SET NOCOUNT ON;

		SET @relRERT = (SELECT DISTINCT [cd_relob] FROM [contaunica].[tb_impressao_relacao_re_rt] WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt)

		IF SUBSTRING(@relRERT, 5, 2) = 'RE'

			BEGIN

				DELETE FROM [contaunica].[tb_itens_obs_re] WHERE [cd_relob_re] = @relRERT

			END
		
		ELSE

			BEGIN

				DELETE FROM [contaunica].[tb_itens_obs_rt] WHERE [cd_relob_rt] = @relRERT

			END

		DELETE FROM [contaunica].[tb_impressao_relacao_re_rt] WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt

END;