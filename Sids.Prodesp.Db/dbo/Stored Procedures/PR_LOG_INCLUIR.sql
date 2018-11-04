
-- =====================================================================
-- Author:		Luis Fernando
-- Create date: 12/10/2016
-- Description:	Procedure para inclusão de Log de alterações no BD
-- =====================================================================

CREATE PROCEDURE [dbo].[PR_LOG_INCLUIR] 
  	 @dt_log			DATETIME		= null
	,@id_usuario		INT				= null
	,@id_tipo_log		INT				= null
	,@id_resultado		INT				= null
	,@id_recurso		INT				= null
	,@ds_ip				varchar(30)		= null
	,@ds_url			varchar(100)	= null
	,@id_navegador		int				= null
	,@ds_argumento		VARCHAR(MAX)	= null
	,@ds_versao			VARCHAR(20)		= null
	,@ds_log			text			= null
	,@ds_navegador		VARCHAR(50)		= null
	,@ds_terminal		VARCHAR(50)		= null
	,@ds_xml			text			= null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [seguranca].[tb_log_aplicacao]
		([id_resultado]
		,[id_navegador]
		,[id_acao]
		,[id_recurso]
		,[id_usuario]
		,[dt_log]
		,[ds_ip]
		,[ds_url]
		,[ds_argumento]
		,[ds_versao]
		,[ds_log]
		,[ds_navegador]
		,[ds_terminal]
		,[ds_xml])
	VALUES
        (@id_resultado
        ,@id_navegador
        ,@id_tipo_log
        ,@id_recurso
        ,@id_usuario
        ,@dt_log
        ,@ds_ip
        ,@ds_url
        ,@ds_argumento
		,@ds_versao
		,@ds_log
		,@ds_navegador
		,@ds_terminal
		,@ds_xml)
END