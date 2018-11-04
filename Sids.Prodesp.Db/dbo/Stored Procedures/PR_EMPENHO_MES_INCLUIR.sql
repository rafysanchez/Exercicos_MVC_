-- ===================================================================      
-- Author:		Rodrigo Cesar de Freitas
-- Create date: 20/12/2016
-- Description: Procedure para inclusão de valores para empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_MES_INCLUIR]    
	@tb_empenho_id_empenho	int = null
,	@ds_mes					varchar(9) = null
,	@vr_mes					numeric = null
AS    
BEGIN      
    
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO empenho.tb_empenho_mes(
			tb_empenho_id_empenho
		,	ds_mes
		,	vr_mes
		)
		VALUES (			
			nullif( @tb_empenho_id_empenho, 0 )
		,	@ds_mes
		,	@vr_mes
		)		
           
	COMMIT
    
    SELECT @@IDENTITY
END