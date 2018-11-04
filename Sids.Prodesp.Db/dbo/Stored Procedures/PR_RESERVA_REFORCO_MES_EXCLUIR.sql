
-- ===================================================================  
-- Author:		Carlos Henrique
-- Create date: 01/11/2016  
-- Description: Procedure para exclusão de Valores de Reservas
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_RESERVA_REFORCO_MES_EXCLUIR]   
	@id_reforco_mes     INT
AS  
BEGIN  
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON;  

	DELETE FROM reserva.tb_reforco_mes
	 WHERE id_reforco_mes =  @id_reforco_mes;
  
END