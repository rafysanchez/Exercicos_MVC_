-- ===================================================================  
-- Author:		Luis Fernando
-- Create date: 26/07/2017
-- Description: Procedure para consultar lista de boletos
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_LISTA_DE_BOLETOS_CONSULTAR]
	@id_lista_de_boletos int = 0,
	@id_regional int = 0
as  
begin  
  
 SET NOCOUNT ON;  
  
	SELECT [id_lista_de_boletos]
      ,[id_regional]
      ,[id_tipo_documento]
      ,[dt_cadastro]
      ,[nr_siafem_siafisico]
      ,[nr_contrato]
      ,[nr_documento]
      ,[cd_unidade_gestora]
      ,[cd_gestao]
      ,[cd_aplicacao_obra]
      ,[dt_emissao]
      ,[nr_cnpj_favorecido]
      ,[ds_nome_da_lista]
      ,[ds_copiar_da_lista]
      ,[nr_total_de_credores]
      ,[vl_total_da_lista]
      ,[fl_sistema_siafem_siafisico]
      ,[cd_transmissao_status_siafem_siafisico]
      ,[fl_transmissao_transmitido_siafem_siafisico]
      ,[dt_transmissao_transmitido_siafem_siafisico]
      ,[ds_transmissao_mensagem_siafem_siafisico]
      ,[bl_cadastro_completo]
	  ,[id_codigo_de_barras]
  FROM [contaunica].[tb_lista_de_boletos] (nolock)
	where
		( nullif( @id_lista_de_boletos, 0 ) is null or id_lista_de_boletos = @id_lista_de_boletos )
        And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
	Order by id_lista_de_boletos
end;