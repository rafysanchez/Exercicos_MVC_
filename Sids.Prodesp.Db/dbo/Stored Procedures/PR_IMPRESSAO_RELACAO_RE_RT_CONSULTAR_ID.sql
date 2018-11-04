
-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 04/09/2018
-- Description: Procedure para consultar Impressão de Relação RE e RT por ID
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_CONSULTAR_ID]
	@id_impressao_relacao_re_rt INT = NULL
AS

BEGIN

--DECLARE
--	@id_impressao_relacao_re_rt INT = 36

SET NOCOUNT ON;  

	BEGIN

		SELECT
			[id_impressao_relacao_re_rt]
		   ,[cd_relob]
		   ,[nr_ob]
		   ,[cd_relatorio]
           ,[nr_agrupamento]
           ,[cd_unidade_gestora]
           ,[ds_nome_unidade_gestora]
           ,[cd_gestao]
           ,[ds_nome_gestao]
           ,[cd_banco]
           ,[ds_nome_banco]
           ,[ds_texto_autorizacao]
           ,[ds_cidade]
           ,[ds_nome_gestor_financeiro]
           ,[ds_nome_ordenador_assinatura]
           ,[dt_solicitacao]
           ,[dt_referencia]
           ,[dt_cadastramento]
           ,[dt_emissao]
           ,[vl_total_documento]
           ,[vl_extenso]
           ,[fg_transmitido_siafem]
           ,[fg_transmitir_siafem]
           ,[dt_transmitido_siafem]
           ,[ds_status_siafem]
           ,[ds_msgRetornoTransmissaoSiafem]
           ,[fg_cancelamento_relacao_re_rt]
           ,[nr_agencia]
           ,[ds_nome_agencia]
           ,[nr_conta_c]
		FROM [contaunica].[tb_impressao_relacao_re_rt]
		WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt

	END

END;