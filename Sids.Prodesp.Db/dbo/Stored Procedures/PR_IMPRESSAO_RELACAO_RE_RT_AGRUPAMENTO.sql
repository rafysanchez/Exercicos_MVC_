
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 18/09/2018
-- Description: Procedure que retorna o incremento +1 do campo agrupamento
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_AGRUPAMENTO]

AS

BEGIN

SET NOCOUNT ON;  

	BEGIN
	
		IF (SELECT MAX([nr_agrupamento] + 1) FROM [contaunica].[tb_impressao_relacao_re_rt]) IS NULL

			SELECT 1

		ELSE

			SELECT
				MAX([nr_agrupamento] + 1)
			FROM [contaunica].[tb_impressao_relacao_re_rt]

	END

END;