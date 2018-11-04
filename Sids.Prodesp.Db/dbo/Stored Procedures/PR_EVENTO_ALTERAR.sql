
-- ==============================================================    
-- Author:  Rodrigo Borghi
-- Create date: 20/09/2018
-- Description: Procedure para Alterar Evento
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_EVENTO_ALTERAR]
	@id_evento int
	,@id_nl_parametrizacao int
	,@id_rap_tipo char(1) = null
	,@id_documento_tipo int
	,@nr_evento varchar(50) = null
	,@nr_classificacao varchar(50) = null
	,@ds_entrada_saida char(10) = null
	,@nr_fonte varchar(50) = null
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE
		[pagamento].[tb_evento]
	SET 
		id_nl_parametrizacao = @id_nl_parametrizacao
		,id_rap_tipo = @id_rap_tipo
		,id_documento_tipo = @id_documento_tipo
		,nr_evento = @nr_evento
		,nr_classificacao = @nr_classificacao
		,ds_entrada_saida = @ds_entrada_saida
		,nr_fonte = @nr_fonte
	WHERE
		id_evento = @id_evento
END

-- ==============================================================    
-- Author:  Rodrigo Borghi
-- Create date: 20/09/2018
-- Description: Procedure para Incluir um Evento
-- ==============================================================    
SET ANSI_NULLS ON