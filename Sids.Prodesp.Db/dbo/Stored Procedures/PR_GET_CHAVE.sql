
-- ====================================================================
-- Author:		Luis Fernando
-- Create date: 09/11/2016
-- Description:	Procedure que retorna proxima chave livre para acesso ao webservice prodesp
-- ====================================================================
CREATE PROCEDURE [dbo].[PR_GET_CHAVE]
	@id_usuario int
AS
BEGIN
	declare @Regional varchar(2);

	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb..#chave') is not null DROP TABLE #chave
			create table #chave(id_chave int,	ds_chave varchar(50),	ds_senha varchar(100),);
	
	select @Regional = SUBSTRING(B.ds_regional , 3,2)
	from 
		seguranca.tb_usuario A
	join 
		seguranca.tb_regional B
	on 
		A.id_regional = b.id_regional
	where 
		id_usuario = @id_usuario

	insert into #chave
	SELECT top 1 
	   [id_chave]
      ,[ds_chave]
      ,[ds_senha]
	FROM 
		[reserva].[tb_chave_cicsmo]
	where 
		[bl_disponivel] = 1
	and
		substring(ds_chave,len(ds_chave)-1,2) = @Regional
	order by 
		[nr_ranking];

	update [reserva].[tb_chave_cicsmo] Set [bl_disponivel] = 0 
	where id_chave = (select id_chave from #chave);


	SELECT top 1 
		[id_chave]
      ,[ds_chave]
      ,[ds_senha]
	FROM 
		#chave
END