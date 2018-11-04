-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para incluir Itens de Obs RE
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_ITENS_OBS_RE_SALVAR]
		@cd_relob_re VARCHAR(11) = NULL
		,@nr_ob_re VARCHAR(11) = NULL
		,@fg_prioridade VARCHAR(1) = NULL
		,@cd_tipo_ob INT = NULL
		,@ds_nome_favorecido VARCHAR(50) = NULL
		,@ds_banco_favorecido VARCHAR(5) = NULL
		,@cd_agencia_favorecido VARCHAR(5) = NULL
		,@ds_conta_favorecido VARCHAR(10) = NULL
		,@vl_ob DECIMAL(15,2) = NULL
AS

BEGIN

	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO [contaunica].[tb_itens_obs_re] (
			[cd_relob_re]
			,[nr_ob_re]
			,[fg_prioridade]
			,[cd_tipo_ob]
			,[ds_nome_favorecido]
			,[ds_banco_favorecido]
			,[cd_agencia_favorecido]
			,[ds_conta_favorecido]
			,[vl_ob]
		)
		VALUES 
		(
			@cd_relob_re
			,@nr_ob_re
			,@fg_prioridade
			,@cd_tipo_ob
			,@ds_nome_favorecido
			,@ds_banco_favorecido
			,@cd_agencia_favorecido
			,@ds_conta_favorecido
			,@vl_ob
		);
	
	COMMIT	
    
    SELECT @@IDENTITY

END