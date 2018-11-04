-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para consultar codigo de barras taxas
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_CODIGO_DE_BARRAS_TAXAS_CONSULTAR]
	@id_codigo_de_barras_taxas int,
	@id_codigo_de_barras int
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT [id_codigo_de_barras_taxas]
	  ,[id_codigo_de_barras]
      ,[nr_conta1]
      ,[nr_conta2]
      ,[nr_conta3]
      ,[nr_conta4]
  FROM [contaunica].[tb_codigo_de_barras_taxas] (nolock)
	where
		( nullif( @id_codigo_de_barras_taxas, 0 ) is null or id_codigo_de_barras_taxas = @id_codigo_de_barras_taxas )
		and
		--( nullif( @id_codigo_de_barras, 0 ) is null or id_codigo_de_barras = @id_codigo_de_barras )
		id_codigo_de_barras = @id_codigo_de_barras
	Order by [id_codigo_de_barras_taxas] desc
end;