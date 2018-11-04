
CREATE PROCEDURE [dbo].[PR_CONFIRMACAO_PAGAMENTO_ITEM_RELACIONARDESDOBRADOS]
@id_execucao_pd int = NULL,
@id_programacao_desembolso_execucao_item int = NULL,
@id_autorizacao_ob int = NULL,
@id_autorizacao_ob_item int = NULL,
@nr_documento_gerador varchar(22) = NULL

AS
BEGIN
BEGIN TRANSACTION
SET NOCOUNT ON;

	IF ISNULL(@id_execucao_pd, 0) <> 0 
	BEGIN 
		--Verificar se já existe desdobramento
		IF EXISTS(	SELECT	id_confirmacao_pagamento_item
					FROM	pagamento.tb_confirmacao_pagamento_item 
					WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17))
		BEGIN
			
			--Caso o desdobrado não esteja em algum agrupamento
			IF EXISTS (	SELECT	id_programacao_desembolso_execucao_item 
							FROM	contaunica.tb_programacao_desembolso_execucao_item
							WHERE	nr_documento_gerador = @nr_documento_gerador
							AND		nr_agrupamento_pd = 0)
			BEGIN
				
				SELECT	@id_execucao_pd = id_execucao_pd FROM contaunica.tb_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_pd <> 0

				--PRINT @id_execucao_pd
				--PRINT @id_programacao_desembolso_execucao_item

				--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
				UPDATE	pagamento.tb_confirmacao_pagamento_item
				SET		id_execucao_pd = @id_execucao_pd,
						id_programacao_desembolso_execucao_item = @id_programacao_desembolso_execucao_item 
				WHERE	nr_documento_gerador = @nr_documento_gerador

			END
			 
		END
	END
	ELSE
	BEGIN
		
		IF ISNULL(@id_autorizacao_ob, 0) <> 0
		BEGIN
			--Verificar se já existe desdobramento
			IF EXISTS(	SELECT	id_confirmacao_pagamento_item
						FROM	pagamento.tb_confirmacao_pagamento_item 
						WHERE	LEFT(nr_documento_gerador, 17) = LEFT(@nr_documento_gerador, 17))
			BEGIN
			
				--Caso o desdobrado não esteja em algum agrupamento
				IF EXISTS (	SELECT	id_autorizacao_ob_item 
								FROM	contaunica.tb_autorizacao_ob_itens
								WHERE	nr_documento_gerador = @nr_documento_gerador
								AND		nr_agrupamento_ob = 0)
				BEGIN
				
					SELECT	@id_autorizacao_ob = id_autorizacao_ob FROM contaunica.tb_autorizacao_ob_itens
					WHERE	nr_documento_gerador = @nr_documento_gerador AND nr_agrupamento_ob <> 0

					--PRINT @id_autorizacao_ob
					--PRINT @id_autorizacao_ob_item

					--Atualizar o id_execucao_pd para pertencer ao agrupamento corrente
					UPDATE	pagamento.tb_confirmacao_pagamento_item
					SET		id_autorizacao_ob = @id_autorizacao_ob,
							id_autorizacao_ob_item = @id_autorizacao_ob_item 
					WHERE	nr_documento_gerador = @nr_documento_gerador

				END
			 
			END
		END

	END

COMMIT
END