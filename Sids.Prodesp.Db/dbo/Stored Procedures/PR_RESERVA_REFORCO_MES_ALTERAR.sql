
-- ===================================================================    
-- Author:		carlos Henrique
-- Create date: 01/11/2016
-- Description: Procedure para alteração de Valores de reforços
-- ===================================================================  

CREATE PROCEDURE [dbo].[PR_RESERVA_REFORCO_MES_ALTERAR]      
	@id_reforco_mes			int,
	@id_reforco				int,
	@ds_mes					VARCHAR(2),
	@vr_mes					numeric
AS  
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
	SET NOCOUNT ON;    
   
	UPDATE reserva.tb_reforco_mes
		  SET id_reforco		= @id_reforco
			,ds_mes				= @ds_mes
			,vr_mes				= @vr_mes
	 WHERE id_reforco_mes		= @id_reforco_mes
END