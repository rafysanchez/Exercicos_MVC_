-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para incluir Itens de Obs RT
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_ITENS_OBS_RT_SALVAR]
		@cd_relob_rt VARCHAR(11) = NULL
		,@nr_ob_rt VARCHAR(11) = NULL
		,@cd_conta_bancaria_emitente VARCHAR(9) = NULL
		,@cd_unidade_gestora_favorecida VARCHAR(6) = NULL
		,@cd_gestao_favorecida VARCHAR(5) = NULL
		,@ds_mnemonico_ug_favorecida VARCHAR(15) = NULL
		,@ds_banco_favorecido VARCHAR(5) = NULL
		,@cd_agencia_favorecido VARCHAR(5) = NULL
		,@ds_conta_favorecido VARCHAR(10) = NULL
		,@vl_ob DECIMAL(15,2) = NULL
AS

BEGIN

	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO [contaunica].[tb_itens_obs_rt] (
			[cd_relob_rt]
			,[nr_ob_rt]
			,[cd_conta_bancaria_emitente]
			,[cd_unidade_gestora_favorecida]
			,[cd_gestao_favorecida]
			,[ds_mnemonico_ug_favorecida]
			,[ds_banco_favorecido]
			,[cd_agencia_favorecido]
			,[ds_conta_favorecido]
			,[vl_ob]
		)
		VALUES 
		(
			@cd_relob_rt
			,@nr_ob_rt
			,@cd_conta_bancaria_emitente
			,@cd_unidade_gestora_favorecida
			,@cd_gestao_favorecida
			,@ds_mnemonico_ug_favorecida
			,@ds_banco_favorecido
			,@cd_agencia_favorecido
			,@ds_conta_favorecido
			,@vl_ob
		);
	
	COMMIT	
    
    SELECT @@IDENTITY

END