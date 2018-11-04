-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 14/09/2017
-- Description: Procedure para consultar anulação de rap
-- =================================================================== 
CREATE procedure [dbo].[PR_PROGRAMACAO_DESEMBOLSO_AGRUPAMENTO_CONSULTAR_NUMERO]
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT TOP 1
		[nr_agrupamento]
	FROM [contaunica].[tb_programacao_desembolso_agrupamento]  (nolock)
	ORDER BY
		[nr_agrupamento] desc
   
end;