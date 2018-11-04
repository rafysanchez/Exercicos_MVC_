-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 14/12/2016
-- Description: Procedure para consulta de destino
-- ==============================================================  

CREATE PROCEDURE [dbo].[PR_DESTINO_CONSULTAR]  
  @id_destino     INT =NULL,  
  @cd_destino	  CHAR(2) = NULL,
  @ds_destino     VARCHAR(140)= NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
  SELECT 
     destino.id_destino,  
     destino.cd_destino, 
     destino.cd_destino + ' - ' + destino.ds_destino as ds_destino
    FROM configuracao.tb_destino destino (NOLOCK)
 WHERE 
       (destino.id_destino = @id_destino or isnull(@id_destino,0)=0) and
	   (destino.cd_destino like '%' +@cd_destino +  '%' or isnull(@cd_destino,'')='') and
	   (destino.ds_destino like '%' +@ds_destino + '%' or isnull(@ds_destino,'')='')
	   ORDER BY destino.ds_destino
END