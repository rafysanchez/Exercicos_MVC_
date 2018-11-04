
CREATE PROCEDURE [dbo].[PR_AUTORIZACAO_DE_OB_ITEM_GRID]
	@id_programacao_desembolso_execucao_item int = NULL,
	@id_execucao_pd int = NULL,
	@ds_numpd varchar(50) = NULL,
	@ug varchar(50) = NULL,
	@gestao varchar(50) = NULL,
	@ug_pagadora varchar(50) = NULL,
	@ug_liquidante varchar(50) = NULL,
	@gestao_pagadora varchar(50) = NULL,
	@gestao_liguidante varchar(50) = NULL,
	@favorecido varchar(50) = NULL,
	@favorecidoDesc varchar(50) = NULL,
	@ordem varchar(50) = NULL,
	@ano_pd varchar(50) = NULL,
	@valor decimal(18,2) = NULL,
	@ds_noup varchar(50) = NULL,
	@nr_agrupamento_pd int = NULL,
	@ds_numob varchar(50) = NULL,
	@ob_cancelada bit = NULL,
	@fl_sistema_prodesp bit = NULL,
	@cd_transmissao_status_prodesp varchar(50) = NULL,
	@cd_transmissao_status_siafem char(1) = NULL,
	@fl_transmissao_transmitido_siafem bit = NULL,
	@dt_transmissao_transmitido_siafem date = NULL,
	@ds_transmissao_mensagem_siafem varchar(50) = NULL,
	@cd_despesa varchar(2) = NULL,
	@cd_aplicacao_obra varchar(140) = NULL,
	@nr_contrato varchar(13) = NULL,

	@tipoExecucao int = null,
	@de date = null,
	@ate date = null
AS
BEGIN
	
	SET NOCOUNT ON;

		/*Versão 1 - Todos os registros*/

		SELECT
				itens.[id_autorizacao_ob_item] 
				, itens.[id_autorizacao_ob] 
				, itens.[ds_numob] 
				, itens.[ds_numop] 
				, itens.[nr_documento_gerador] 
				, itens.[favorecidoDesc] 
				, itens.[cd_despesa] 
				, itens.[nr_banco] 
				, itens.[valor] 
				, ob.[id_confirmacao_pagamento] 
				, ob.[id_tipo_pagamento] 
				, ob.[id_execucao_pd] 
				, ob.[nr_agrupamento] 
				, ob.[ug_pagadora] 
				, ob.[gestao_pagadora] 
				, ob.[ug_liquidante] 
				, ob.[gestao_liquidante] 
				, ob.[unidade_gestora] 
				, ob.[gestao] 
				, ob.[qtde_autorizacao] 
				, ob.[dt_cadastro] 
				, ob.[cd_transmissao_status_siafem] 
				, ob.[dt_transmissao_transmitido_siafem] 
				, ob.[ds_transmissao_mensagem_siafem] 
				, ob.[nr_contrato] 
				, ob.[cd_aplicacao_obra]
				, cpi.dt_confirmacao
		FROM contaunica.tb_autorizacao_ob_itens (NOLOCK) itens
		LEFT JOIN contaunica.tb_autorizacao_ob (NOLOCK) ob ON ob.id_autorizacao_ob = itens.id_autorizacao_ob
		LEFT JOIN pagamento.tb_confirmacao_pagamento_item (NOLOCK) cpi ON cpi.id_autorizacao_ob = itens.id_autorizacao_ob AND cpi.id_autorizacao_ob_item = itens.id_autorizacao_ob_item 
		WHERE
		(@ug_pagadora is null or ob.ug_pagadora = @ug_pagadora) AND
		(@gestao_pagadora is null or ob.gestao_pagadora = @gestao_pagadora) AND
		(@ds_numob is null or itens.ds_numob LIKE '%' + @ds_numob + '%') AND
		--(@favorecido is null or itens.favorecido = @favorecido) AND
		(@favorecidoDesc is null or itens.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		(@cd_despesa is null or itens.cd_despesa = @cd_despesa) AND
		(@valor is null or itens.[valor] = @valor) AND
		(@cd_transmissao_status_siafem IS NULL OR ob.cd_transmissao_status_siafem = @cd_transmissao_status_siafem) AND
		(@de is null or ob.dt_cadastro >= @de )AND
		(@ate is null or ob.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00')) AND
		(@cd_aplicacao_obra IS NULL OR ob.cd_aplicacao_obra = @cd_aplicacao_obra) AND
		(@nr_contrato IS NULL OR ob.nr_contrato = @nr_contrato)
		

		/* Versão 2 registros agrupados */

		--SELECT	ob.[id_autorizacao_ob], ob.unidade_gestora, ob.gestao, ob.dt_cadastro, ob.[cd_transmissao_status_siafem], SUM(itens.valorDecimal) as valor_total
		--FROM contaunica.tb_autorizacao_ob_itens (NOLOCK) itens
		--LEFT JOIN contaunica.tb_autorizacao_ob (NOLOCK) ob ON ob.id_autorizacao_ob = itens.id_autorizacao_ob 
		--WHERE
		--(@ug_pagadora is null or ob.ug_pagadora = @ug_pagadora) AND
		--(@gestao_pagadora is null or ob.gestao_pagadora = @gestao_pagadora) AND
		--(@ds_numob is null or itens.ds_numob LIKE '%' + @ds_numob + '%') AND
		----(@favorecido is null or itens.favorecido = @favorecido) AND
		--(@favorecidoDesc is null or itens.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		--(@cd_despesa is null or itens.cd_despesa = @cd_despesa) AND
		--(@valor is null or itens.[valor] = @valor) AND
		--(@cd_transmissao_status_siafem IS NULL OR ob.cd_transmissao_status_siafem = @cd_transmissao_status_siafem) AND
		--(@de is null or ob.dt_cadastro >= @de )AND
		--(@ate is null or ob.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00')) AND
		--(@cd_aplicacao_obra IS NULL OR ob.cd_aplicacao_obra = @cd_aplicacao_obra) AND
		--(@nr_contrato IS NULL OR ob.nr_contrato = @nr_contrato)
		--GROUP BY ob.[id_autorizacao_ob], ob.unidade_gestora, ob.gestao, ob.dt_cadastro, ob.[cd_transmissao_status_siafem] 

		--/* Versão 3: registros agrupados e status siafem baseado no status de todos os itens */
		--CREATE TABLE #Temp(
		--	id_autorizacao_ob int, 
		--	id_autorizacao_ob_item int,
		--	unidade_gestora varchar(50), 
		--	gestao varchar(50), 
		--	dt_cadastro	datetime, 
		--	cd_transmissao_status_siafem char(1), 
		--	valor_total decimal(18,0)
		--)

		----Incluir numa temporária todos os registros baseado no filtro(exceto o filtro status transmissao siafem), a coluna status incluso com o campo do item(tb_autorizacao_ob_itens)
		--INSERT		#Temp
		--			(id_autorizacao_ob, id_autorizacao_ob_item, unidade_gestora, gestao, dt_cadastro, cd_transmissao_status_siafem, valor_total)
		--SELECT		ob.[id_autorizacao_ob], id_autorizacao_ob_item, ob.unidade_gestora, ob.gestao, ob.dt_cadastro, itens.[cd_transmissao_item_status_siafem], itens.valor
		--FROM		contaunica.tb_autorizacao_ob_itens (NOLOCK) itens
		--LEFT JOIN	contaunica.tb_autorizacao_ob (NOLOCK) ob ON ob.id_autorizacao_ob = itens.id_autorizacao_ob 
		--WHERE
		--(@ug_pagadora is null or ob.unidade_gestora = @ug_pagadora) AND
		--(@gestao_pagadora is null or ob.gestao = @gestao_pagadora) AND
		--(@ds_numob is null or itens.ds_numob LIKE '%' + @ds_numob + '%') AND
		----(@favorecido is null or itens.favorecido = @favorecido) AND
		--(@favorecidoDesc is null or itens.favorecidoDesc LIKE '%' + @favorecidoDesc + '%' ) AND
		--(@cd_despesa is null or itens.cd_despesa = @cd_despesa) AND
		----(@valor is null or itens.[valor] = @valor) AND
		----(@cd_transmissao_status_siafem IS NULL OR ob.cd_transmissao_status_siafem = @cd_transmissao_status_siafem) AND
		--(@de is null or ob.dt_cadastro >= @de )AND
		--(@ate is null or ob.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00')) AND
		--(@cd_aplicacao_obra IS NULL OR ob.cd_aplicacao_obra = @cd_aplicacao_obra) AND
		--(@nr_contrato IS NULL OR ob.nr_contrato = @nr_contrato)
		
		--DECLARE @i int, 
		--		@max int,
		--		@id_autorizacao_ob int,
		--		@statusSiafem char(1)

		--SELECT	@max = MAX(id_autorizacao_ob_item) FROM #Temp
		--SET		@i = 1;

		--WHILE(@i <= @max)
		--BEGIN
		--	--insert into @table_sequencia values(@i);

		--	SELECT	@id_autorizacao_ob = id_autorizacao_ob,
		--			@statusSiafem = cd_transmissao_status_siafem
		--	FROM	#Temp 
		--	WHERE	id_autorizacao_ob_item = @i

		--	IF(ISNULL(RTRIM(@statusSiafem), 'N') = 'E')
		--	BEGIN
		--		UPDATE	#Temp
		--		SET		cd_transmissao_status_siafem = 'E'
		--		WHERE	id_autorizacao_ob = @id_autorizacao_ob
		--	END
		--	IF(ISNULL(RTRIM(@statusSiafem), 'N') = 'N')
		--	BEGIN
		--		UPDATE	#Temp
		--		SET		cd_transmissao_status_siafem = 'N'
		--		WHERE	id_autorizacao_ob = @id_autorizacao_ob
		--	END
			
		--	SET @id_autorizacao_ob = 0
		--	SET @statusSiafem = ''
		--	SET @i = @i + 1;
		--END

		--SELECT		id_autorizacao_ob, unidade_gestora, gestao, dt_cadastro, cd_transmissao_status_siafem, SUM(valor_total) valor_total
		--FROM		#Temp
		--WHERE		(@cd_transmissao_status_siafem IS NULL OR cd_transmissao_status_siafem = @cd_transmissao_status_siafem)
		--GROUP BY	id_autorizacao_ob, unidade_gestora, gestao, dt_cadastro, cd_transmissao_status_siafem
		--HAVING		(@valor IS NULL OR SUM(valor_total) = @valor)

		--DROP TABLE	#Temp

END


/*

SELECT + ', ob.[' +  COLUMN_NAME + '] ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

SELECT + ', itens.[' +  COLUMN_NAME + '] ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob_itens'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_itens'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_itens'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao_itens'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_autorizacao_ob'

declare @d as datetime
set @d = '2017-01-01'
SELECT DATEADD(hh, DATEDIFF(hh,0,@d), '23:59:00')

declare @de as datetime
set @de = '2017-11-21'


declare @ate as datetime
set @ate = '2017-11-21'


select ex.dt_cadastro FROM contaunica.tb_programacao_desembolso_execucao_itens (NOLOCK) itens
	LEFT JOIN contaunica.tb_programacao_desembolso_execucao (NOLOCK) EX ON EX.id_execucao_pd = itens.id_execucao_pd 
where
(@de is null or ex.dt_cadastro >= @de )AND
(@ate is null or ex.dt_cadastro <= DATEADD(hh, DATEDIFF(hh,0,@ate), '23:59:00'))

*/