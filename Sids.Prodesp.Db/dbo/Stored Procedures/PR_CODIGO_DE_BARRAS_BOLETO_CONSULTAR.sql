-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para consultar codigo de barras boleto
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_CODIGO_DE_BARRAS_BOLETO_CONSULTAR]
	@id_codigo_de_barras_boleto int = 0,
	@id_codigo_de_barras int
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT [id_codigo_de_barras_boleto]
	,id_codigo_de_barras
      ,[nr_conta_cob1]
      ,[nr_conta_cob2]
      ,[nr_conta_cob3]
      ,[nr_conta_cob4]
      ,[nr_conta_cob5]
      ,[nr_conta_cob6]
      ,[nr_digito]
      ,[nr_conta_cob7]
  FROM [contaunica].[tb_codigo_de_barras_boleto] (nolock)
	where
		( nullif( @id_codigo_de_barras_boleto, 0 ) is null or id_codigo_de_barras_boleto = @id_codigo_de_barras_boleto )
		and
		--( nullif( @id_codigo_de_barras, 0 ) is null or id_codigo_de_barras = @id_codigo_de_barras )
		id_codigo_de_barras = @id_codigo_de_barras
	--Order by id_codigo_de_barras_boleto
	Order by id_codigo_de_barras, id_codigo_de_barras_boleto desc
end;