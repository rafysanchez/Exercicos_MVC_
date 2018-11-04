-- ===================================================================  
-- Author:		Carlos Henrique Magalhães
-- Create date: 31/03/2017
-- Description: Procedure para consultar anulação de rap
-- =================================================================== 
CREATE procedure [dbo].[PR_RAP_ANULACAO_CONSULTAR_ASSINATURA]
	@tb_regional_id_regional smallint
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT TOP 1
		id_rap_anulacao
	,	tb_regional_id_regional
	,	cd_assinatura_autorizado
	,	cd_assinatura_autorizado_grupo
	,	cd_assinatura_autorizado_orgao
	,	ds_assinatura_autorizado_cargo
	,	nm_assinatura_autorizado
	,	cd_assinatura_examinado
	,	cd_assinatura_examinado_grupo
	,	cd_assinatura_examinado_orgao
	,	ds_assinatura_examinado_cargo
	,	nm_assinatura_examinado
	,	cd_assinatura_responsavel
	,	cd_assinatura_responsavel_grupo
	,	cd_assinatura_responsavel_orgao
	,	ds_assinatura_responsavel_cargo
	,	nm_assinatura_responsavel
	FROM pagamento.tb_rap_anulacao (nolock)
	where
		tb_regional_id_regional = @tb_regional_id_regional
	ORDER BY
		id_rap_anulacao desc
   
end;