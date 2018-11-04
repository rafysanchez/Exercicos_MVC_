-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para consultar codigo de barras
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_CODIGO_DE_BARRAS_CONSULTAR]
	@id_codigo_de_barras int = 0,
	@id_lista_de_boletos int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT [id_codigo_de_barras]
      ,[id_tipo_de_boleto]
      ,[id_lista_de_boletos]
      ,[vr_boleto]
	  ,bl_transmitido_siafem
  FROM [contaunica].[tb_codigo_de_barras] (nolock)
	where
		( nullif( @id_codigo_de_barras, 0 ) is null or id_codigo_de_barras = @id_codigo_de_barras )
		and ( nullif( @id_lista_de_boletos, 0 ) is null or id_lista_de_boletos = @id_lista_de_boletos )
	Order by id_codigo_de_barras
end;