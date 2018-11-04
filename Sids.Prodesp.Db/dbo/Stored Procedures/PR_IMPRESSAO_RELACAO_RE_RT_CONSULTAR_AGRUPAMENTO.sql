
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 19/09/2018
-- Description: Procedure para consultar a lista de Re e/ou RT com as OBs transmitidas
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_AGRUPAMENTO]
	@nr_agrupamento INT = NULL
AS

BEGIN

--DECLARE
--	@nr_agrupamento INT = 5

	SET NOCOUNT ON;  

		SELECT DISTINCT
			--ROW_NUMBER() OVER(ORDER BY [cd_relob], [nr_ob] ASC) AS Row#
			ir.[nr_agrupamento]
			,ir.[cd_unidade_gestora]
			,ir.[cd_gestao]
			,ir.[cd_banco]
			,CASE 
				WHEN SUBSTRING(ir.[cd_relob], 5, 2) = 'RE' THEN re.[cd_relob_re]
				WHEN SUBSTRING(ir.[cd_relob], 5, 2) = 'RT' THEN rt.[cd_relob_rt]
				ELSE NULL
			END AS [cd_relob]
			,CASE 
				WHEN SUBSTRING(re.[cd_relob_re], 5, 2) = 'RE' THEN re.[nr_ob_re]
				WHEN SUBSTRING(rt.[cd_relob_rt], 5, 2) = 'RT' THEN rt.[nr_ob_rt]
				ELSE NULL
			END AS [nr_ob]
			,re.[fg_prioridade]
			,re.[cd_tipo_ob]
			,re.[ds_nome_favorecido]
			,rt.[cd_conta_bancaria_emitente]
			,rt.[cd_unidade_gestora_favorecida]
			,rt.[cd_gestao_favorecida]
			,rt.[ds_mnemonico_ug_favorecida]
			,CASE 
				WHEN re.[ds_banco_favorecido] != '' THEN re.[ds_banco_favorecido]
				WHEN rt.[ds_banco_favorecido] != '' THEN rt.[ds_banco_favorecido]
				ELSE NULL
			END AS [ds_banco_favorecido]
			,CASE 
				WHEN re.[cd_agencia_favorecido] != '' THEN re.[cd_agencia_favorecido]
				WHEN rt.[cd_agencia_favorecido] != '' THEN rt.[cd_agencia_favorecido]
				ELSE NULL
			END AS [cd_agencia_favorecido]
			,CASE 
				WHEN re.[ds_conta_favorecido] != '' THEN re.[ds_conta_favorecido]
				WHEN rt.[ds_conta_favorecido] != '' THEN rt.[ds_conta_favorecido]
				ELSE NULL
			END AS [ds_conta_favorecido]
			,CASE 
				WHEN re.[vl_ob] != 0 THEN re.[vl_ob]
				WHEN rt.[vl_ob] != 0 THEN rt.[vl_ob]
				ELSE NULL
			END AS [vl_ob]
		FROM [contaunica].[tb_impressao_relacao_re_rt] ir
			LEFT JOIN [contaunica].[tb_itens_obs_re] re ON ir.[cd_relob] = cd_relob_re
			LEFT JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = cd_relob_rt
		WHERE ( ( @nr_agrupamento IS NULL OR [nr_agrupamento] = @nr_agrupamento ) )
		--ORDER BY Row# ASC

END;