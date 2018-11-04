
-- ==============================================================
-- Author:		Luis Fernando
-- Create date: 14/10/2016
-- Description:	Procedure para consulta de Programas cadastrados
-- ==============================================================
CREATE PROCEDURE [dbo].[PR_PROGRAMA_CONSULTAR]   
	@id_programa				int = null,    
	@cd_ptres					VARCHAR(6) = null,
	@cd_cfp						VARCHAR(16) = null,
	@ds_programa				VARCHAR(60) = null,
	@nr_ano_referencia			int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [id_programa]
			,[cd_ptres]
			,[cd_cfp]
			,[ds_programa]
			,[nr_ano_referencia]
		FROM configuracao.tb_programa (nolock)	  A
	  WHERE ([id_programa] = @id_programa OR ISNULL(@id_programa,0) = 0)
			AND ([ds_programa] like '%'+ @ds_programa+'%' OR ISNULL(@ds_programa,'') = '')
			AND ([cd_ptres] = @cd_ptres OR ISNULL(@cd_ptres,'') = '')
			AND ([cd_cfp] = @cd_cfp	OR ISNULL(@cd_cfp,'') = '')
			AND ([nr_ano_referencia] = @nr_ano_referencia OR ISNULL(@nr_ano_referencia,0) = 0)
	ORDER BY A.[ds_programa] 
END