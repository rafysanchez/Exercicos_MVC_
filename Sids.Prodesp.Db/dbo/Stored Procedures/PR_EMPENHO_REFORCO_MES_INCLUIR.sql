-- ===================================================================      
-- Author:		Carlos Henrique Magalhaes
-- Create date: 26/12/2016
-- Description: Procedure para inclusão de valores para Reforço de empenho
CREATE PROCEDURE [dbo].[PR_EMPENHO_REFORCO_MES_INCLUIR]    
	@tb_empenho_reforco_id_empenho_reforco	int = NULL
,	@ds_mes									varchar(9) = NULL
,	@vr_mes									numeric = NULL
AS    
BEGIN      
    
	SET NOCOUNT ON;
	
	BEGIN TRANSACTION

		INSERT INTO empenho.tb_empenho_reforco_mes(
			tb_empenho_reforco_id_empenho_reforco
		,	ds_mes
		,	vr_mes
		)
		VALUES (			
			nullif(@tb_empenho_reforco_id_empenho_reforco, 0)
		,	@ds_mes
		,	@vr_mes
		)		
           
	COMMIT
    
    SELECT @@IDENTITY
END