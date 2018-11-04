
-- ==============================================================  
-- Author:  Carlos Henrique 
-- Create date: 07/10/2016  
-- Description: Procedure para consulta de fontes utilizadas na reserva
-- ==============================================================  

CREATE PROCEDURE [dbo].[PR_FONTE_CONSULTAR]  
  @id_fonte     INT =NULL,  
  @cd_fonte		VARCHAR(10) = NULL,
  @ds_fonte     VARCHAR(45)= NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
  SELECT 
     f.[id_fonte],  
     f.[cd_fonte] , 
     f.[ds_fonte]  
    
    FROM configuracao.tb_fonte (NOLOCK) f   
 WHERE 
       (id_fonte = @id_fonte or  isnull(@id_fonte,0)=0) and
	   (f.cd_fonte like '%' +@cd_fonte +  '%' or isnull(@cd_fonte,'')='') and
	   (f.ds_fonte like '%' +@ds_fonte + '%' or isnull(@ds_fonte,'')='')
	   ORDER BY f.[cd_fonte] 	
END