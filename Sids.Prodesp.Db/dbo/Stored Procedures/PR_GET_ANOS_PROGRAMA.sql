
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 14/10/2016
-- Description:	Procedure que anos cadastados na tabela de Programas
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_GET_ANOS_PROGRAMA]
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT DISTINCT nr_ano_referencia 
	FROM configuracao.tb_programa(nolock)
	order by nr_ano_referencia desc;
END