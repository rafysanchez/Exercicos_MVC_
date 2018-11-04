
-- ==============================================================    
-- Author:  Daniel Gomes
-- Create date: 15/01/2018
-- Description: Procedure para Incluir uma parametrização de NL
-- ==============================================================    
  
CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_INCLUIR]
   @ds_observacao varchar(250) = null
   ,@id_nl_tipo int
   ,@bl_transmitir bit
   ,@nr_favorecida_cnpjcpfug varchar(15) = null
   ,@nr_favorecida_gestao int = null
   ,@nr_unidade_gestora int = null
   ,@id_parametrizacao_forma_gerar_nl int
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
INSERT INTO [pagamento].[tb_nl_parametrizacao]
           ([id_nl_tipo]
		   ,[ds_observacao]
		   ,[bl_transmitir]
           ,[nr_favorecida_cnpjcpfug]
		   ,[nr_favorecida_gestao]
		   ,[nr_unidade_gestora]
		   ,[id_parametrizacao_forma_gerar_nl])
     VALUES
           (@id_nl_tipo
		   ,@ds_observacao
		   ,@bl_transmitir
		   ,@nr_favorecida_cnpjcpfug
		   ,@nr_favorecida_gestao
		   ,@nr_unidade_gestora
		   ,@id_parametrizacao_forma_gerar_nl)
  
COMMIT

SELECT SCOPE_IDENTITY();