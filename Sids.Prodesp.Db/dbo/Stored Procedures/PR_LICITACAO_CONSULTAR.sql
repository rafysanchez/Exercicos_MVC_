-- ==============================================================  
-- Author:  Rodrigo Cesar de Freitas 
-- Create date: 128/12/2016
-- Description: Procedure para consulta de licitacoes
-- ==============================================================  
CREATE PROCEDURE [dbo].[PR_LICITACAO_CONSULTAR]  
	@id_licitacao	INT = 0  
,	@ds_licitacao	VARCHAR(140) = NULL

AS  
BEGIN  
	SET NOCOUNT ON;
	
	SELECT 
		id_licitacao
	,	ds_licitacao 
	FROM empenho.tb_licitacao ( NOLOCK )
	WHERE 
       ( @id_licitacao = 0 or id_licitacao = @id_licitacao ) and
	   ( @ds_licitacao is null or ds_licitacao = @ds_licitacao )
	   ORDER BY id_licitacao
END