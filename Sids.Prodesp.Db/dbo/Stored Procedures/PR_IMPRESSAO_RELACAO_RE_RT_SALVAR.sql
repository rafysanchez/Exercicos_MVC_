-- ===================================================================  
-- Author:		Rodrigo Ohashi
-- Create date: 03/09/2018
-- Description: Procedure para incluir Impressão de Relação RE e RT
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_IMPRESSAO_RELACAO_RE_RT_SALVAR]
		@id_impressao_relacao_re_rt INT = NULL
		,@cd_relob VARCHAR(11) = NULL
		,@nr_ob VARCHAR(11) = NULL
		,@cd_relatorio VARCHAR(11) = NULL
		,@nr_agrupamento INT = NULL
		,@cd_unidade_gestora VARCHAR(6) = NULL
		,@ds_nome_unidade_gestora VARCHAR(30) = NULL
		,@cd_gestao VARCHAR(5) = NULL
		,@ds_nome_gestao VARCHAR(30) = NULL
		,@cd_banco VARCHAR(5) = NULL
		,@ds_nome_banco VARCHAR(30) = NULL
		,@ds_texto_autorizacao VARCHAR(70) = NULL
		,@ds_cidade VARCHAR(30) = NULL
		,@ds_nome_gestor_financeiro VARCHAR(30) = NULL
		,@ds_nome_ordenador_assinatura VARCHAR(30) = NULL
		,@dt_solicitacao DATETIME = NULL
		,@dt_referencia DATETIME = NULL
		,@dt_cadastramento DATETIME = NULL
		,@dt_emissao DATETIME = NULL
		,@vl_total_documento DECIMAL(15,2) = NULL
		,@vl_extenso VARCHAR(255) = NULL
		,@fg_transmitido_siafem BIT = NULL
		,@fg_transmitir_siafem BIT = NULL
		,@dt_transmitido_siafem DATETIME = NULL
		,@ds_status_siafem VARCHAR(1) = NULL
		,@ds_msgRetornoTransmissaoSiafem VARCHAR(140) = NULL
		,@fg_cancelamento_relacao_re_rt BIT = NULL
		,@nr_agencia VARCHAR(5) = NULL
		,@ds_nome_agencia VARCHAR(30) = NULL
		,@nr_conta_c VARCHAR(10) = NULL
AS

BEGIN

	SET NOCOUNT ON;
	
	IF EXISTS (SELECT 1 FROM [contaunica].[tb_impressao_relacao_re_rt] (NOLOCK) WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt)
	
		BEGIN

			UPDATE [contaunica].[tb_impressao_relacao_re_rt] SET
				[cd_relob] = @cd_relob
				,[nr_ob] = @nr_ob
				,[cd_relatorio] = @cd_relatorio
				,[nr_agrupamento] = @nr_agrupamento
				,[cd_unidade_gestora] = @cd_unidade_gestora
				,[ds_nome_unidade_gestora] = @ds_nome_unidade_gestora
				,[cd_gestao] = @cd_gestao
				,[ds_nome_gestao] = @ds_nome_gestao
				,[cd_banco] = @cd_banco
				,[ds_nome_banco] = @ds_nome_banco
				,[ds_texto_autorizacao] = @ds_texto_autorizacao
				,[ds_cidade] = @ds_cidade
				,[ds_nome_gestor_financeiro] = @ds_nome_gestor_financeiro
				,[ds_nome_ordenador_assinatura] = @ds_nome_ordenador_assinatura
				,[dt_solicitacao] = @dt_solicitacao
				,[dt_referencia] = @dt_referencia
				,[dt_cadastramento] = @dt_cadastramento
				,[dt_emissao] = @dt_emissao
				,[vl_total_documento] = @vl_total_documento
				,[vl_extenso] = @vl_extenso
				,[fg_transmitido_siafem] = @fg_transmitido_siafem
				,[fg_transmitir_siafem] = @fg_transmitir_siafem
				,[dt_transmitido_siafem] = @dt_transmitido_siafem
				,[ds_status_siafem] = @ds_status_siafem
				,[ds_msgRetornoTransmissaoSiafem] = @ds_msgRetornoTransmissaoSiafem
				,[fg_cancelamento_relacao_re_rt] = @fg_cancelamento_relacao_re_rt
				,[nr_agencia] = @nr_agencia
				,[ds_nome_agencia] = @ds_nome_agencia
				,[nr_conta_c] = @nr_conta_c
			WHERE [id_impressao_relacao_re_rt] = @id_impressao_relacao_re_rt

			SELECT @id_impressao_relacao_re_rt;

		END

	ELSE

		BEGIN

			INSERT INTO [contaunica].[tb_impressao_relacao_re_rt] (
				[cd_relob]
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
			)
			VALUES 
			(
				@cd_relob
				,@nr_ob
				,@cd_relatorio
				,@nr_agrupamento
				,@cd_unidade_gestora
				,@ds_nome_unidade_gestora
				,@cd_gestao
				,@ds_nome_gestao
				,@cd_banco
				,@ds_nome_banco
				,@ds_texto_autorizacao
				,@ds_cidade
				,@ds_nome_gestor_financeiro
				,@ds_nome_ordenador_assinatura
				,@dt_solicitacao
				,@dt_referencia
				,@dt_cadastramento
				,@dt_emissao
				,@vl_total_documento
				,@vl_extenso
				,@fg_transmitido_siafem
				,@fg_transmitir_siafem
				,@dt_transmitido_siafem
				,@ds_status_siafem
				,@ds_msgRetornoTransmissaoSiafem
				,@fg_cancelamento_relacao_re_rt
				,@nr_agencia
				,@ds_nome_agencia
				,@nr_conta_c
			);
    
			SELECT scope_identity();

		END

END