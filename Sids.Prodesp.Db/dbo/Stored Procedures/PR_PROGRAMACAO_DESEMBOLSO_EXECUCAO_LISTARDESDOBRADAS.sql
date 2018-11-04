
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_LISTARDESDOBRADAS] --'2018PD00401'
	@nr_siafem_siafisico varchar(11) = null
AS

	DECLARE @NumeroDocumentoGerador varchar(22)

	--IF LEN(@nr_siafem_siafisico) <= 5
	--	SET @tipo = 2

	--PD
	IF EXISTS(
			SELECT PD.id_programacao_desembolso
			FROM [contaunica].[tb_programacao_desembolso] PD (nolock)
			where nr_siafem_siafisico = @nr_siafem_siafisico
		)
		BEGIN 

			SELECT	@NumeroDocumentoGerador = nr_documento_gerador
			FROM	[contaunica].[tb_programacao_desembolso] PD (nolock)
			WHERE	nr_siafem_siafisico = @nr_siafem_siafisico

			SELECT DISTINCT
				PD.nr_siafem_siafisico as NumeroSiafem
				,PD.id_programacao_desembolso
				,EI.id_programacao_desembolso_execucao_item
				,PD.nr_cnpj_cpf_pgto
				,nr_siafem_siafisico
				,[PD].[id_tipo_documento]
				,[PD].[nr_contrato]
				,[PD].[nr_documento]
				,[PD].[nr_documento_gerador]
				,[PI].cd_transmissao_status_prodesp
				,[PI].fl_transmissao_transmitido_prodesp
				,[PI].dt_transmissao_transmitido_prodesp
				,[PI].ds_transmissao_mensagem_prodesp
				,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico
				,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico
				,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico
				,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico
			FROM		[contaunica].[tb_programacao_desembolso] PD (nolock)
			LEFT JOIN	contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PD.nr_siafem_siafisico
			LEFT JOIN	pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].id_execucao_pd = EI.id_execucao_pd
			WHERE		SUBSTRING(PD.nr_documento_gerador, 10, 8) = SUBSTRING(@NumeroDocumentoGerador, 10, 8)
			AND			PD.nr_siafem_siafisico <> @nr_siafem_siafisico
			AND			EI.nr_agrupamento_pd IS NULL
			ORDER BY NumeroSiafem DESC
		END

		ELSE

		BEGIN
			
			SELECT	@NumeroDocumentoGerador = nr_documento_gerador
			FROM	[contaunica].[tb_programacao_desembolso_agrupamento] PDA (nolock)
			WHERE	nr_programacao_desembolso = @nr_siafem_siafisico

			SELECT DISTINCT
				PDA.nr_programacao_desembolso as NumeroSiafem
				,EI.id_programacao_desembolso_execucao_item
				, PDA.nr_cnpj_cpf_pgto
				,PDA.[id_tipo_documento]
				--,[nr_contrato]
				,PDA.[nr_documento]
				,PDA.[nr_documento_gerador]

				,[PI].cd_transmissao_status_prodesp
					,[PI].fl_transmissao_transmitido_prodesp
					,[PI].dt_transmissao_transmitido_prodesp
					,[PI].ds_transmissao_mensagem_prodesp

				,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico
				,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico
				,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico
				,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico

			from [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock)
			LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PDA.nr_programacao_desembolso
			LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].id_execucao_pd = EI.id_execucao_pd
			WHERE	SUBSTRING(PDA.nr_documento_gerador, 10, 8) = SUBSTRING(@NumeroDocumentoGerador, 10, 8)
			AND		PDA.nr_programacao_desembolso <> @nr_siafem_siafisico
			AND		EI.nr_agrupamento_pd IS NULL
			ORDER BY PDA.nr_programacao_desembolso
		END