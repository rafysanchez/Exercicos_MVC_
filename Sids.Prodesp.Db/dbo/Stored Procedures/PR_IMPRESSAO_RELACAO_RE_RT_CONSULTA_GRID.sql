
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 29/08/2018
-- Description: Procedure para consultar Impressão de Relação RE e RT
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTA_GRID]
	@cd_relre VARCHAR(11) = NULL,
	@cd_relrt VARCHAR(11) = NULL,
	@cd_relob VARCHAR(11) = NULL,
	@ds_status_siafem VARCHAR(1) = NULL,
	@dt_cadastramentoDe DATETIME = NULL,
	@dt_cadastramentoAte DATETIME = NULL,
	@cd_unidade_gestora VARCHAR(6) = NULL,
	@cd_gestao VARCHAR(5) = NULL,
	@cd_banco VARCHAR(5) = NULL,
	@dt_solicitacao datetime = NULL,
	@nr_agrupamento INT = NULL,
	@fg_cancelamento_relacao_re_rt BIT = NULL
AS

BEGIN

--DECLARE
--	@cd_relre VARCHAR(11) = NULL,
--	@cd_relrt VARCHAR(11) = NULL,
--	@cd_relob VARCHAR(11) = NULL,
--	@ds_status_siafem VARCHAR(1) = NULL,
--	@dt_cadastramentoDe DATETIME = NULL,
--	@dt_cadastramentoAte DATETIME = NULL,
--	@cd_unidade_gestora VARCHAR(6) = NULL,
--	@cd_gestao VARCHAR(5) = NULL,
--	@cd_banco VARCHAR(5) = NULL,
--	@dt_solicitacao datetime = NULL,
--	@nr_agrupamento INT = NULL,
--	@fg_cancelamento_relacao_re_rt BIT = NULL

SET NOCOUNT ON;  

SELECT DISTINCT
	[id_impressao_relacao_re_rt]
	,[nr_agrupamento]
	,CASE 
		WHEN SUBSTRING([cd_relob_re], 5, 2) = 'RE' THEN [cd_relob_re]
		WHEN SUBSTRING([cd_relob_re], 5, 2) = 'RT' THEN [cd_relob_re]
		ELSE NULL
	END AS [cd_relob]
	,[cd_unidade_gestora]
	,[cd_gestao]
	,[cd_banco]
	,[dt_solicitacao]
	,[ds_status_siafem]
	,[fg_cancelamento_relacao_re_rt]
	,[fg_transmitido_siafem]
FROM (
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,ir.[nr_agrupamento]
		,re.[cd_relob_re]
		,re.[nr_ob_re]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[dt_solicitacao]
		,ir.[ds_status_siafem]
		,ir.[fg_cancelamento_relacao_re_rt]
		,ir.[fg_transmitido_siafem]
		,ir.[dt_cadastramento]
	FROM [contaunica].[tb_itens_obs_re] re
	INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_re
	UNION ALL
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,ir.[nr_agrupamento]
		,rt.[cd_relob_rt]
		,rt.[nr_ob_rt]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[dt_solicitacao]
		,ir.[ds_status_siafem]
		,ir.[fg_cancelamento_relacao_re_rt]
		,ir.[fg_transmitido_siafem]
		,ir.[dt_cadastramento]
	FROM [contaunica].[tb_itens_obs_rt] rt
	INNER JOIN [contaunica].[tb_impressao_relacao_re_rt] ir ON ir.[cd_relob] = cd_relob_rt
	UNION ALL
	SELECT  
		ir.[id_impressao_relacao_re_rt]
		,ir.[nr_agrupamento]
		,rt.[cd_relob_rt]
		,rt.[nr_ob_rt]
		,ir.[cd_unidade_gestora]
		,ir.[cd_gestao]
		,ir.[cd_banco]
		,ir.[dt_solicitacao]
		,ir.[ds_status_siafem]
		,ir.[fg_cancelamento_relacao_re_rt]
		,ir.[fg_transmitido_siafem]
		,ir.[dt_cadastramento]
	FROM [contaunica].[tb_impressao_relacao_re_rt] ir
	LEFT JOIN [contaunica].[tb_itens_obs_rt] rt ON ir.[cd_relob] = rt.[cd_relob_rt]
	WHERE ir.[cd_relob] IS NULL
) AS ImpressaoRelacaoRERT
WHERE ( @ds_status_siafem IS NULL OR [ds_status_siafem] = @ds_status_siafem )
		AND ( FORMAT(@dt_cadastramentoDe, 'yyyy-MM-dd 00:00:00') IS NULL OR [dt_cadastramento] >= FORMAT(@dt_cadastramentoDe, 'yyyy-MM-dd 00:00:00') ) 
		AND ( FORMAT(@dt_cadastramentoAte, 'yyyy-MM-dd 23:59:59') IS NULL OR [dt_cadastramento] <= FORMAT(@dt_cadastramentoAte, 'yyyy-MM-dd 23:59:59') ) 
		AND ( @cd_unidade_gestora IS NULL OR [cd_unidade_gestora] = @cd_unidade_gestora ) 
		AND ( @cd_gestao IS NULL OR [cd_gestao] = @cd_gestao )
		AND ( @cd_banco IS NULL OR [cd_banco] = @cd_banco )
		AND ( @dt_solicitacao IS NULL OR [dt_solicitacao] = @dt_solicitacao ) 
		AND ( @nr_agrupamento IS NULL OR [nr_agrupamento] = @nr_agrupamento ) 
		AND ( @fg_cancelamento_relacao_re_rt IS NULL OR [fg_cancelamento_relacao_re_rt] = @fg_cancelamento_relacao_re_rt ) 
		AND ( ( @cd_relre IS NULL OR [cd_relob_re] = @cd_relre ) AND 
			( @cd_relrt IS NULL OR [cd_relob_re] = @cd_relrt ) AND 
			( @cd_relob IS NULL OR [nr_ob_re] = @cd_relob ) )
ORDER BY id_impressao_relacao_re_rt

END;