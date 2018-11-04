-- ===================================================================  
-- Author:		Alessandro de Santana
-- Create date: 13/04/2018
-- Description: Procedure para consultar parametros da  programacao desembolso
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_PARAMETROS_CONSULTAR]

as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT * FROM [contaunica].[tb_programacao_desembolso_parametros]  (nolock)
	
end;