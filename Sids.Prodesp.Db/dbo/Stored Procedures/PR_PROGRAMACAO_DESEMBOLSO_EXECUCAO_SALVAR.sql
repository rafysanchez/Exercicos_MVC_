-- ===================================================================  
-- Author:		JOSE G BRAZ
-- Create date: 05/10/2017
-- Description: Procedure para exclusão de execusao de pd
-- =================================================================== 


CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_SALVAR]   
@id_execucao_pd int = NULL,
@id_tipo_execucao_pd int = NULL,
@ug_pagadora varchar(20) = NULL,
@gestao_pagadora varchar(20) = NULL,
@ug_liquidante varchar(20) = NULL,
@gestao_liquidante varchar(20) = NULL,
@unidade_gestora varchar(20) = NULL,
@gestao varchar(20) = NULL,
@ano_pd varchar(20) = NULL,
@valor_total decimal = NULL,
@nr_agrupamento int = NULL,
@dt_cadastro datetime = NULL,
@fl_sistema_prodesp bit = 0,
@fl_sistema_siafem_siafisico bit = 0,
@id_tipo_pagamento int = NULL,
@fl_confirmacao bit = 0,
@dt_confirmacao datetime = null
AS  
BEGIN  

set nocount on;

	if exists ( 
		select	1 
		from	contaunica.tb_programacao_desembolso_execucao
		where	id_execucao_pd = @id_execucao_pd
	)
	begin
	
		update contaunica.tb_programacao_desembolso_execucao set 
			id_tipo_execucao_pd = @id_tipo_execucao_pd,
			ug_pagadora = @ug_pagadora, 
			gestao_pagadora = @gestao_pagadora, 
			ug_liquidante = @ug_liquidante, 
			gestao_liquidante = @gestao_liquidante, 
			unidade_gestora = @unidade_gestora, 
			gestao = @gestao, 
			ano_pd = @ano_pd, 
			valor_total = @valor_total, 
			nr_agrupamento = @nr_agrupamento, 
			fl_sistema_prodesp = @fl_sistema_prodesp, 
			fl_sistema_siafem_siafisico = @fl_sistema_siafem_siafisico,
			id_tipo_pagamento = @id_tipo_pagamento,
			fl_confirmacao = @fl_confirmacao,
			dt_confirmacao = @dt_confirmacao
		WHERE id_execucao_pd = @id_execucao_pd
 
		select @id_execucao_pd;
	end
	else
	begin

		insert into contaunica.tb_programacao_desembolso_execucao (
			id_tipo_execucao_pd,
			ug_pagadora,
			gestao_pagadora,
			ug_liquidante,
			gestao_liquidante,
			unidade_gestora,
			gestao,
			ano_pd,
			valor_total,
			nr_agrupamento,
			dt_cadastro,
			fl_sistema_prodesp,
			fl_sistema_siafem_siafisico,
			id_tipo_pagamento,
			fl_confirmacao,
			dt_confirmacao
		)
		values (
		  @id_tipo_execucao_pd
		, @ug_pagadora
		, @gestao_pagadora
		, @ug_liquidante
		, @gestao_liquidante
		, @unidade_gestora
		, @gestao
		, @ano_pd
		, @valor_total
		, @nr_agrupamento
		, ISNULL(@dt_cadastro, GETDATE())
		, @fl_sistema_prodesp
		, @fl_sistema_siafem_siafisico
		, @id_tipo_pagamento
		, @fl_confirmacao
		, @dt_confirmacao
		)			
           
		select scope_identity();

	end

END


/* 

SELECT  + '[' +  COLUMN_NAME + '] ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME +' = ' + '@' +  COLUMN_NAME + ', ' FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT COLUMN_NAME + ','  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT ',	@' + COLUMN_NAME  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'

SELECT '@'+COLUMN_NAME +' ' + DATA_TYPE +' = NULL,'  FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'tb_programacao_desembolso_execucao'
*/