CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_ORCAMENTARIA_CONSULTAR_GRID] 
 @nr_agrupamento_movimentacao int = NULL,
 @nr_siafem varchar(15) = NULL,
 @tipo_documento int = NULL,
 @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int = NULL,
 @cd_unidade_gestora_emitente varchar(10) = NULL,
 @cd_gestao_emitente varchar(10) = NULL,
 @cd_unidade_gestora_favorecido varchar(10) = NULL,
 @cd_gestao_favorecido varchar(10) = NULL,
 @tb_programa_id_programa int = NULL,
 @cd_natureza int = NULL,
 @dt_cadastramento_de date = NULL, 
 @dt_cadastramento_ate date = NULL,
 @fg_transmitido_siafem char(1) = NULL,
 @fg_transmitido_prodesp char(1) = NULL
AS     
 
 --declare
 -- @nr_agrupamento_movimentacao int = 221,
 --@nr_siafem varchar(15) = NULL,
 --@tipo_documento int = NULL,
 --@tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria int = NULL,
 --@cd_unidade_gestora_emitente varchar(10) = NULL,
 --@cd_gestao_emitente varchar(10) = NULL,
 --@cd_unidade_gestora_favorecido varchar(10) = NULL,
 --@cd_gestao_favorecido varchar(10) = NULL,
 --@tb_programa_id_programa int = NULL,
 --@cd_natureza int = NULL,
 --@dt_cadastramento_de date = NULL, 
 --@dt_cadastramento_ate date = NULL,
 --@fg_transmitido_siafem char(1) = NULL,
 --@fg_transmitido_prodesp char(1) = NULL

BEGIN      
 SET NOCOUNT ON;

	CREATE TABLE #auxiliar
	(
		IdMovimentacao int,
		Id int,
		numAgrupamento int,
		nr_seq int,
		DescDocumento varchar(30),  
		NumSiafem varchar(30),
		UnidadeGestoraEmitente varchar(30), 
		UnidadeGestoraFavorecida varchar(30), 
		idCFP varchar(30),
		idCED varchar(30),
		Valor decimal(18,2),
		DataCadastro datetime,
		TransmitidoProdesp char(1),
		TransmitidoSiafem char(1),
		MsgProdesp  varchar(200),
		MsgSiafem varchar(200),
		PodeExcluir bit,
	)
	
	 IF( nullif(@tipo_documento,0) is null or @tipo_documento = 1)
	 BEGIN
	    --CANCELAMENTOS
		INSERT INTO #auxiliar (
			IdMovimentacao,
			Id ,
			numAgrupamento ,
			nr_seq ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem
		)
		(
		SELECT 
			o.id_movimentacao_orcamentaria,
			c.id_cancelamento_movimentacao,
			c.nr_agrupamento,
			c.nr_seq,
			'Cancelamento',
			c.nr_siafem,
			o.cd_unidade_gestora_emitente,
			c.cd_unidade_gestora,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			rs.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			rs.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem
		FROM  [movimentacao].[tb_cancelamento_movimentacao] c
			LEFT JOIN [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			LEFT JOIN [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			LEFT JOIN [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura
			LEFT JOIN [movimentacao].[tb_reducao_suplementacao] rs on rs.nr_seq = c.nr_seq
				and rs.flag_red_sup = 'R' and rs.valor = c.valor and rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
				and rs.nr_agrupamento = c.nr_agrupamento
		WHERE 1 = 1 AND
			(NULLIF( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   AND   
			(@nr_siafem is null or c.nr_siafem = @nr_siafem) AND
			(NULLIF( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   AND   
			(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) AND  
			(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) AND  
			(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora = @cd_unidade_gestora_favorecido) AND  
			(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) AND  
			(NULLIF(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) AND  
			(NULLIF(@cd_natureza,0) is null or e.cd_natureza = @cd_natureza) AND  
			( @dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     AND  
			( @dt_cadastramento_ate is null or o.[dt_cadastro] < DATEADD(day, 1, @dt_cadastramento_ate) )   AND
			(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) AND  
			(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp)
			)
END
	IF(nullif(@tipo_documento,0) is null or @tipo_documento = 2)
	BEGIN
		--NOTAS CREDITOS
		INSERT INTO #auxiliar
		(
			IdMovimentacao,
			Id ,
			numAgrupamento ,
			nr_seq ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem
		)			
		(
		SELECT 
			o.id_movimentacao_orcamentaria,
			c.id_nota_credito,
			c.nr_agrupamento,
			c.nr_seq,
			'Nota de Crédito',
			c.nr_siafem,
			o.cd_unidade_gestora_emitente,
			c.cd_unidade_gestora_favorecido,
			p.cd_cfp,
			e.cd_natureza,
			c.vr_credito,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem
		FROM  [movimentacao].[tb_credito_movimentacao] c
			LEFT JOIN [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			LEFT JOIN [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			LEFT JOIN [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura
		WHERE 1 = 1 AND
			(NULLIF( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   AND   
			(@nr_siafem is null or c.nr_siafem = @nr_siafem) AND  
			(NULLIF( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   AND   
			(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) AND  
			(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) AND  
			(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido) AND  
			(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) AND  
			(NULLIF(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) AND  
			(NULLIF(@cd_natureza,0) is null or e.cd_natureza = @cd_natureza) AND  
			(@dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     AND  
			(@dt_cadastramento_ate is null or o.[dt_cadastro] < DATEADD(day, 1, @dt_cadastramento_ate) )   AND
			(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) AND  
			(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 
		)
	END

	IF(nullif(@tipo_documento,0) is null or @tipo_documento = 3)
	BEGIN
		--DISTRIBUICAO
		INSERT INTO #auxiliar
		(
			IdMovimentacao,
			Id ,
			numAgrupamento ,
			nr_seq ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem
		)			
		(
		SELECT 
			o.id_movimentacao_orcamentaria,
			c.id_distribuicao_movimentacao,
			c.nr_agrupamento,
			c.nr_seq,
			'Distribuição',
			c.nr_siafem,
			o.cd_unidade_gestora_emitente,
			c.cd_unidade_gestora_favorecido,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			rs.fg_transmitido_prodesp,
			c.fg_transmitido_siafem,
			rs.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem
		FROM [movimentacao].[tb_distribuicao_movimentacao] c
			LEFT JOIN [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			LEFT JOIN [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			LEFT JOIN [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura
			LEFT JOIN [movimentacao].[tb_reducao_suplementacao] rs on rs.nr_seq = c.nr_seq
				and rs.flag_red_sup = 'S' and rs.valor = c.valor and rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
				and rs.nr_agrupamento = c.nr_agrupamento
		WHERE 1 = 1 AND
			(NULLIF( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   AND   
			(@nr_siafem is null or c.nr_siafem = @nr_siafem) AND  
			(NULLIF( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   AND   
			(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) AND  
			(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) AND  
			(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora_favorecido = @cd_unidade_gestora_favorecido) AND  
			(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) AND  
			(NULLIF(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) AND  
			(NULLIF(@cd_natureza,0) is null or e.cd_natureza = @cd_natureza) AND 
			(@dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     AND  
			(@dt_cadastramento_ate is null or o.[dt_cadastro] < DATEADD(day, 1, @dt_cadastramento_ate) )   AND
			(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) AND  
			(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) 
		)
	END

	IF(nullif(@tipo_documento,0) is null or @tipo_documento = 4)
	BEGIN
		--REDUCAO
		INSERT INTO #auxiliar
		(
			IdMovimentacao,
			Id ,
			numAgrupamento ,
			nr_seq ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem
		)			
		(
		SELECT 
			o.id_movimentacao_orcamentaria,
			c.id_reducao_suplementacao,
			c.nr_agrupamento,
			c.nr_seq,
			'Redução',
			c.nr_suplementacao_reducao,
			o.cd_unidade_gestora_emitente,
			c.cd_unidade_gestora,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			c2.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			c.ds_msgRetornoSiafem
		FROM  [movimentacao].[tb_reducao_suplementacao] c
			LEFT JOIN [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			LEFT JOIN [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			LEFT JOIN [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura
			LEFT JOIN [movimentacao].[tb_cancelamento_movimentacao] c2
				on c2.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
				and c2.nr_agrupamento = c.nr_agrupamento
				and c.flag_red_sup = 'R' and c2.nr_seq = c.nr_seq and c2.valor = c.valor
		WHERE 1 = 1 AND
			(NULLIF( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   AND   
			(@nr_siafem is null or c.nr_suplementacao_reducao = @nr_siafem) AND
			(NULLIF( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   AND   
			(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) AND  
			(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) AND  
			(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora = @cd_unidade_gestora_favorecido) AND  
			(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) AND  
			(NULLIF(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) AND  
			(NULLIF(@cd_natureza,0) is null or e.cd_natureza = @cd_natureza) AND  
			(@dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     AND  
			(@dt_cadastramento_ate is null or o.[dt_cadastro] < DATEADD(day, 1, @dt_cadastramento_ate) )   AND
			(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) AND  
			(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) AND c.flag_red_sup = 'R'
		)
	END

	IF(nullif(@tipo_documento,0) is null or @tipo_documento = 5)
	BEGIN
		--SUPLEMENTACAO
		INSERT INTO #auxiliar
		(
			IdMovimentacao,
			Id ,
			numAgrupamento ,
			nr_seq ,
			DescDocumento ,  
			NumSiafem ,
			UnidadeGestoraEmitente, 
			UnidadeGestoraFavorecida, 
			idCFP ,
			idCED ,
			Valor ,
			DataCadastro ,
			TransmitidoProdesp ,
			TransmitidoSiafem ,
			MsgProdesp  ,
			MsgSiafem
		)			
		(
		SELECT 
			o.id_movimentacao_orcamentaria,
			c.id_reducao_suplementacao,
			c.nr_agrupamento,
			c.nr_seq,
			'Suplementação',
			c.nr_suplementacao_reducao,
			o.cd_unidade_gestora_emitente,
			c.cd_unidade_gestora,
			p.cd_cfp,
			e.cd_natureza,
			c.valor,
			o.dt_cadastro,
			c.fg_transmitido_prodesp,
			d.fg_transmitido_siafem,
			c.ds_msgRetornoProdesp,
			d.ds_msgRetornoSiafem
		FROM  [movimentacao].[tb_reducao_suplementacao] c
			LEFT JOIN [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria
			LEFT JOIN [configuracao].[tb_programa] p on o.tb_programa_id_programa = p.id_programa
			LEFT JOIN [configuracao].[tb_estrutura] e on o.tb_estrutura_id_estrutura = e.id_estrutura
			LEFT JOIN [movimentacao].[tb_distribuicao_movimentacao] d
				on d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria
				and d.nr_agrupamento = c.nr_agrupamento
				and d.nr_seq = c.nr_seq and d.valor = c.valor
		WHERE 1 = 1 AND
			(NULLIF( @nr_agrupamento_movimentacao, 0 ) is null or c.nr_agrupamento = @nr_agrupamento_movimentacao )   AND   
			(@nr_siafem is null or c.nr_suplementacao_reducao = @nr_siafem) AND  
			(NULLIF( @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria, 0 ) is null or o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria = @tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria )   AND   
			(@cd_unidade_gestora_emitente is null or o.cd_unidade_gestora_emitente = @cd_unidade_gestora_emitente) AND  
			(@cd_gestao_emitente is null or o.cd_gestao_emitente = @cd_gestao_emitente) AND  
			(@cd_unidade_gestora_favorecido is null or c.cd_unidade_gestora = @cd_unidade_gestora_favorecido) AND  
			(@cd_gestao_favorecido is null or c.cd_gestao_favorecido = @cd_gestao_favorecido) AND  
			(NULLIF(@tb_programa_id_programa,0 )    is null or o.tb_programa_id_programa = @tb_programa_id_programa) AND  
			(NULLIF(@cd_natureza,0) is null or e.cd_natureza = @cd_natureza) AND  
			(@dt_cadastramento_de is null or o.[dt_cadastro] >= @dt_cadastramento_de )     AND  
			(@dt_cadastramento_ate is null or o.[dt_cadastro] < DATEADD(day, 1, @dt_cadastramento_ate) )   AND
			(@fg_transmitido_siafem is null or c.fg_transmitido_siafem = @fg_transmitido_siafem) AND  
			(@fg_transmitido_prodesp is null or c.fg_transmitido_prodesp = @fg_transmitido_prodesp) AND c.flag_red_sup = 'S'
		)
	END

	SELECT 
		a.IdMovimentacao,
		a.Id  as id_movimentacao_orcamentaria,
		a.numAgrupamento  as nr_agrupamento_movimentacao,
		a.nr_seq as nr_sequencia,
		a.DescDocumento  as desc_documento,  
		a.NumSiafem  as nr_siafem,
		a.UnidadeGestoraEmitente as cd_unidade_gestora_emitente, 
		a.UnidadeGestoraFavorecida as Ug_favorecida, 
		a.idCFP  as cd_estrutura,
		a.idCED  as cd_natureza,
		a.Valor  as valor_geral,
		a.DataCadastro  as dt_cadastro,
		a.TransmitidoProdesp  as fg_transmitido_prodesp,
		a.TransmitidoSiafem  as fg_transmitido_siafem,
		a.MsgProdesp   as ds_msgRetornoProdesp,
		a.MsgSiafem  as ds_msgRetornoSiafem,
		CAST((CASE
			WHEN 
				EXISTS(SELECT 1 FROM movimentacao.tb_cancelamento_movimentacao c
					WHERE c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (c.fg_transmitido_prodesp = 'S' OR c.fg_transmitido_siafem = 'S'))
			OR
				EXISTS(SELECT 1 FROM movimentacao.tb_credito_movimentacao nc
					WHERE nc.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (nc.fg_transmitido_prodesp = 'S' OR nc.fg_transmitido_siafem = 'S'))
			OR
				EXISTS(SELECT 1 FROM movimentacao.tb_distribuicao_movimentacao d
					WHERE d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (d.fg_transmitido_prodesp = 'S' OR d.fg_transmitido_siafem = 'S'))
			OR
				EXISTS(SELECT 1 FROM movimentacao.tb_reducao_suplementacao rs
					WHERE rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (rs.fg_transmitido_prodesp = 'S' OR rs.fg_transmitido_siafem = 'S'))
			THEN
				0
			ELSE
				1
			END) AS BIT) AS PodeExcluir,
		CAST((CASE
			WHEN 
				EXISTS(SELECT 1 FROM movimentacao.tb_cancelamento_movimentacao c
					WHERE c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (c.fg_transmitido_prodesp = 'S' OR c.fg_transmitido_siafem = 'S'))
			OR
				EXISTS(SELECT 1 FROM movimentacao.tb_credito_movimentacao nc
					WHERE nc.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (nc.fg_transmitido_prodesp = 'S' OR nc.fg_transmitido_siafem = 'S'))
			OR
				EXISTS(SELECT 1 FROM movimentacao.tb_distribuicao_movimentacao d
					WHERE d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (d.fg_transmitido_prodesp = 'S' OR d.fg_transmitido_siafem = 'S'))
			OR
				EXISTS(SELECT 1 FROM movimentacao.tb_reducao_suplementacao rs
					WHERE rs.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = a.IdMovimentacao
					AND (rs.fg_transmitido_prodesp = 'S' OR rs.fg_transmitido_siafem = 'S'))
			THEN
				0
			ELSE
				1
			END) AS BIT) AS PodeAlterar
	FROM #auxiliar a
	ORDER BY a.DataCadastro--numAgrupamento,DescDocumento

END