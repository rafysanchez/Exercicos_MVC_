-- =============================================
-- Author:		<Daniel Gomes>
-- Create date: <24/10/2018>
-- Description:	<Obtém a última assinatura utilizada em movimentação orçamentária>
-- =============================================
CREATE PROCEDURE PR_MOVIMENTACAO_ORCAMENTARIA_ULTIMA_ASSINATURA
AS
BEGIN
	SET NOCOUNT ON;
	

SELECT TOP 1
	[cd_autorizado_assinatura],
	[cd_autorizado_grupo],
	[cd_autorizado_orgao],
	[ds_autorizado_cargo],
	[nm_autorizado_assinatura],
	[cd_examinado_assinatura],
	[cd_examinado_grupo],
	[cd_examinado_orgao],
	[ds_examinado_cargo],
	[nm_examinado_assinatura],
	[cd_responsavel_assinatura],
	[cd_responsavel_grupo],
	[cd_responsavel_orgao],
	[ds_responsavel_cargo],
	[nm_responsavel_assinatura]
FROM movimentacao.tb_reducao_suplementacao
ORDER BY id_reducao_suplementacao DESC
END