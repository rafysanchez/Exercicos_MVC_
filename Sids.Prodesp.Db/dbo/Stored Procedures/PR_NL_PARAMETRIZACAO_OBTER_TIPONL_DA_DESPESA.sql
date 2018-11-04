
-- ===================================================================  
-- Author:		Daniel Gomes
-- Create date: 22/01/2018
-- Description: Procedure para obter o tipo de NL pelo tipo da despesa
-- =================================================================== 

CREATE PROCEDURE [dbo].[PR_NL_PARAMETRIZACAO_OBTER_TIPONL_DA_DESPESA]
	@cd_despesa_tipo int
AS  
BEGIN  
  
 SET NOCOUNT ON;  
  
	SELECT 		  
		p.id_nl_tipo,
		nt.ds_nl_tipo
	FROM [pagamento].[tb_nl_parametrizacao] p (NOLOCK)
	JOIN [pagamento].[tb_despesa] d (NOLOCK) on d.id_nl_parametrizacao = p.id_nl_parametrizacao
	JOIN [pagamento].[tb_despesa_tipo] dt (NOLOCK) on dt.id_despesa_tipo = d.id_despesa_tipo	
	JOIN [pagamento].[tb_nl_tipo] nt (NOLOCK) on nt.id_nl_tipo = p.id_nl_tipo
	WHERE
		dt.cd_despesa_tipo = @cd_despesa_tipo
	ORDER BY p.id_nl_tipo
END;