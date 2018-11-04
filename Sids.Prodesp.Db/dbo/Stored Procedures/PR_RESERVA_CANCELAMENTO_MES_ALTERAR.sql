

-- ===================================================================    
-- Author:		carlos Henrique
-- Create date: 01/11/2016
-- Description: Procedure para alteração de Valores de reforços
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_RESERVA_CANCELAMENTO_MES_ALTERAR]      
	@id_cancelamento_mes			int,
	@id_cancelamento				int,
	@ds_mes					VARCHAR(2),
	@vr_mes					numeric
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
	SET NOCOUNT ON;    
   
	UPDATE reserva.tb_cancelamento_mes
		  SET id_cancelamento		= @id_cancelamento
			,ds_mes				= @ds_mes
			,vr_mes				= @vr_mes
	 WHERE id_cancelamento_mes		= @id_cancelamento_mes
END