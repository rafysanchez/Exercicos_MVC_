
CREATE PROCEDURE [dbo].[PR_LOG_CONSULTAR]
	@id_log int = null,
	@id_recurso int = null,
	@id_usuario int = null,
	@id_acao int = null,
	@id_resultado int = null,
	@dt_inicial date = null,
	@dt_final date = null,
	@ds_argumento varchar(max) = null
AS
BEGIN

	SET NOCOUNT ON;

	set @ds_argumento = Ltrim(Rtrim(@ds_argumento));

	SELECT loga.[id_log_aplicacao]
      ,loga.[id_resultado]
      ,loga.[id_navegador]
      ,loga.[id_acao]
      ,loga.[id_recurso]
      ,loga.[id_usuario]
      ,loga.[dt_log]
      ,loga.[ds_ip]
      ,loga.[ds_url]
      ,loga.[ds_argumento]	
      ,loga.[ds_versao]	
	  ,loga.ds_log
	  ,loga.ds_navegador
	  ,loga.ds_terminal
	  ,loga.ds_xml
  FROM [seguranca].[tb_log_aplicacao](nolock) loga

	
	where (loga.id_log_aplicacao = @id_log or @id_log is null)
	and (loga.id_resultado = @id_resultado or @id_resultado is null)
	and (loga.id_recurso = @id_recurso or @id_recurso is null)
	and (loga.id_usuario = @id_usuario or @id_usuario is null)
	and (loga.id_acao = @id_acao or @id_acao is null)

	and (cast(loga.dt_log as date) >= @dt_inicial or @dt_inicial is null)
	and (cast(loga.dt_log as date) <= @dt_final or @dt_final is null)

	and (loga.ds_argumento like '%' + @ds_argumento +'%' or @ds_argumento is null or @ds_argumento = '')
	order by loga.dt_log desc;
END