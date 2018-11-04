-- ==============================================================  
-- Author:  Luis Fernando
-- Create date: 13/09/2017
-- Description: Procedure para consulta de municipio
-- ==============================================================  

CREATE PROCEDURE [dbo].[PR_MUNICIPIO_CONSULTAR]  
  @cd_municipio	  CHAR(4) = NULL,
  @ds_municipio     VARCHAR(40)= NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
  SELECT 
     municipio.cd_municipio, 
     municipio.cd_municipio + ' - ' + municipio.ds_municipio as ds_municipio
    FROM configuracao.tb_municipio municipio (NOLOCK)
 WHERE 
	   (municipio.cd_municipio like '%' +@cd_municipio +  '%' or isnull(@cd_municipio,'')='') and
	   (municipio.ds_municipio like '%' +@ds_municipio + '%' or isnull(@ds_municipio,'')='')
	   ORDER BY municipio.ds_municipio
END