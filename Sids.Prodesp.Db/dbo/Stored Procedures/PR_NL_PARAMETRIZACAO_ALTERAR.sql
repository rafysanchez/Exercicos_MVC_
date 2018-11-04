
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Alterar parametrização de NL
-- ==============================================================   

CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_ALTERAR]
	@id_nl_parametrizacao int
	,@id_nl_tipo int
	,@ds_observacao varchar(250) = null
	,@bl_transmitir bit
	,@nr_favorecida_cnpjcpfug varchar(15) = null
	,@nr_favorecida_gestao int = null
	,@nr_unidade_gestora int = null
	,@id_parametrizacao_forma_gerar_nl int
AS  
BEGIN    
 
	SET NOCOUNT ON;    
   
	UPDATE
		[pagamento].[tb_nl_parametrizacao]
	SET 
		ds_observacao = @ds_observacao
		,id_nl_tipo = @id_nl_tipo
		,bl_transmitir = @bl_transmitir
		,nr_favorecida_cnpjcpfug = @nr_favorecida_cnpjcpfug
		,nr_favorecida_gestao = @nr_favorecida_gestao
		,nr_unidade_gestora = @nr_unidade_gestora
		,id_parametrizacao_forma_gerar_nl = @id_parametrizacao_forma_gerar_nl
	WHERE
		id_nl_parametrizacao = @id_nl_parametrizacao
END