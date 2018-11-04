
-- ==============================================================
-- Author:		Luis Santos
-- Create date: 19/09/2016
-- Description:	Procedure para consulta de regional
-- ==============================================================
 CREATE PROCEDURE [dbo].[PR_REGIONAL_CONSULTAR]
 @ds_regional varchar(100) = null,
 @id_regional int = null,
 @cd_orgao varchar(2) = null
AS
BEGIN

	SET NOCOUNT ON;

	SELECT reg.[id_regional]
      ,reg.[ds_regional]
	  ,reg.[cd_uge]
  FROM [seguranca].[tb_regional] reg (nolock)
  where (reg.ds_regional = @ds_regional or @ds_regional is null)
  and (reg.id_regional = @id_regional or isnull(@id_regional,0) = 0)
  and (substring(reg.ds_regional,3,2) = @cd_orgao or isnull(@cd_orgao,0) = 0)
  order by reg.ds_regional

END