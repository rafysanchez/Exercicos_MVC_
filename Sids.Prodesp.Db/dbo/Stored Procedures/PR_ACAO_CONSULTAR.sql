-- ==============================================================
-- Author:		Luis Santos
-- Create date: 29/08/2016
-- Description:	Procedure para consulta de log
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_ACAO_CONSULTAR]
 @id_acao int = null,
 @ds_acao varchar(50) = null
AS
BEGIN

	SET NOCOUNT ON;
	
	SELECT acao.[id_acao]
      ,acao.[ds_acao]
  FROM [seguranca].[tb_acao](nolock) acao
  where (acao.ds_acao = @ds_acao or @ds_acao is null)
  and (acao.id_acao = @id_acao or isnull(@id_acao,0) = 0)
  order by acao.ds_acao

END