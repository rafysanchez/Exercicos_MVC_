CREATE PROCEDURE [dbo].[PR_EVENTO_INCLUIR]
   @id_nl_parametrizacao int
   ,@id_rap_tipo char(1) = null
   ,@id_documento_tipo int
   ,@nr_evento varchar(50) = null
   ,@nr_classificacao varchar(50) = null
   ,@ds_entrada_saida char(10) = null
   ,@nr_fonte varchar(50) = null
AS  
BEGIN TRANSACTION
     SET NOCOUNT ON;  
	 
INSERT INTO [pagamento].[tb_evento]
           ([id_nl_parametrizacao]
		   ,[id_rap_tipo]
           ,[id_documento_tipo]
		   ,[nr_evento]
		   ,[nr_classificacao]
		   ,[ds_entrada_saida]
		   ,[nr_fonte])
     VALUES
           (@id_nl_parametrizacao
		   ,@id_rap_tipo
           ,@id_documento_tipo
		   ,@nr_evento
		   ,@nr_classificacao
		   ,@ds_entrada_saida
		   ,@nr_fonte)
  
COMMIT

SELECT SCOPE_IDENTITY();