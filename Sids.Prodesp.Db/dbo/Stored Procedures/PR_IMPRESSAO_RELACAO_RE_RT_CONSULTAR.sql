
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 04/09/2018
-- Description: Procedure para consultar Impressão de Relação RE e RT
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR]
	@id_impressao_relacao_re_rt INT = NULL,
	@relRERT VARCHAR(11) = NULL
AS

BEGIN

--DECLARE
--	@id_impressao_relacao_re_rt INT = 558,
--	@relRERT VARCHAR(11) = NULL

SET NOCOUNT ON;  

SET @relRERT = 
(
	SELECT DISTINCT
		[cd_relob_re]
	FROM (
		SELECT  
			ir.[id_impressao_relacao_re_rt]
			,re.[cd_relob_re]
		FROM [contaunica].[tb_itens_obs_re] re
		INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_re
		UNION ALL
		SELECT 
			ir.[id_impressao_relacao_re_rt]
			,rt.[cd_relob_rt]
		FROM [contaunica].[tb_itens_obs_rt] rt
		INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_rt
	) AS ImpressaoRelacaoRERT
	WHERE ( @id_impressao_relacao_re_rt IS NULL OR [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt )
)

IF SUBSTRING(@relRERT, 5, 2) = 'RE'
	
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,re.[cd_relob_re]
		,ir.[dt_cadastramento]
		,ir.[dt_transmitido_siafem]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[fg_transmitido_siafem]
		,ir.[ds_msgRetornoTransmissaoSiafem]
		,re.[nr_ob_re] AS [nr_ob]
		,re.[fg_prioridade]
		,re.[cd_tipo_ob]
		,re.[ds_nome_favorecido]
		,re.[ds_banco_favorecido]
		,re.[cd_agencia_favorecido]
		,re.[ds_conta_favorecido]
		,re.[vl_ob]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
		INNER JOIN [contaunica].[tb_itens_obs_re] re ON ir.[cd_relob] = cd_relob_re
	WHERE ir.[cd_relob] = @relRERT
	ORDER BY id_impressao_relacao_re_rt

ELSE IF SUBSTRING(@relRERT, 5, 2) = 'RT'

	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,rt.[cd_relob_rt]
		,ir.[dt_cadastramento]
		,ir.[dt_transmitido_siafem]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[fg_transmitido_siafem]
		,ir.[ds_msgRetornoTransmissaoSiafem]
		,rt.[nr_ob_rt] AS [nr_ob]
		,rt.[cd_conta_bancaria_emitente]
		,rt.[cd_unidade_gestora_favorecida]
		,rt.[cd_gestao_favorecida]
		,rt.[ds_mnemonico_ug_favorecida]
		,rt.[ds_banco_favorecido]
		,rt.[cd_agencia_favorecido]
		,rt.[ds_conta_favorecido]
		,rt.[vl_ob]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
		INNER JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = cd_relob_rt
	WHERE ir.[cd_relob] = @relRERT
	ORDER BY id_impressao_relacao_re_rt

ELSE
	
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,rt.[cd_relob_rt]
		,ir.[dt_cadastramento]
		,ir.[dt_transmitido_siafem]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[fg_transmitido_siafem]
		,ir.[ds_msgRetornoTransmissaoSiafem]
		,rt.[nr_ob_rt] AS [nr_ob]
		,rt.[cd_conta_bancaria_emitente]
		,rt.[cd_unidade_gestora_favorecida]
		,rt.[cd_gestao_favorecida]
		,rt.[ds_mnemonico_ug_favorecida]
		,rt.[ds_banco_favorecido]
		,rt.[cd_agencia_favorecido]
		,rt.[ds_conta_favorecido]
		,rt.[vl_ob]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
		LEFT JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = cd_relob_rt
	WHERE ir.[id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt
	ORDER BY id_impressao_relacao_re_rt

END;