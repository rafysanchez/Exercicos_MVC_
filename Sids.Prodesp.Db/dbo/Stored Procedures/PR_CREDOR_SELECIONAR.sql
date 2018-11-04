-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 29/06/2017
-- Description: Procedure para consultar credores
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_CREDOR_SELECIONAR] 
	@nm_prefeitura varchar(100) = null
	,@nm_reduzido_credor varchar(100) = null
	
	as
begin  
  
 SET NOCOUNT ON;  
  
	SELECT 
	   c.[id_credor]
      ,c.[nm_prefeitura]
      ,c.[cd_cpf_cnpj_ug_credor]
      ,c.[bl_conveniado]
      ,c.[bl_base_calculo]
      ,c.[nm_reduzido_credor]
	  ,r.nr_cnpj_credor
  FROM [contaunica].[tb_Credor] c (nolock)
  LEFT JOIN [contaunica].[tb_desdobramento_retencao] r on c.nm_reduzido_credor = r.nm_reduzido_credor
  WHERE (@nm_prefeitura is null or c.[nm_prefeitura] like '%'+ @nm_prefeitura + '%' )
  and  (@nm_reduzido_credor is null or c.[nm_reduzido_credor] = @nm_reduzido_credor )
	Order by c.[nm_prefeitura]
end;