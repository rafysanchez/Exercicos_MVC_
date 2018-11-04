
-- ==============================================================  
-- Author:  Luis Fernando  
-- alter date: 11/08/2017
-- carlos henrique  
-- Description: Procedure para Consultar ultima assinatura de Reserva cadastrada  
-- ==============================================================  
  
CREATE PROCEDURE [dbo].[PR_RESERVA_CONSULTAR_ASSINATURA]  
   @id_regional     smallint = 0  
  
as  
begin  
  
 SET NOCOUNT ON;  
  
 SELECT top 1 [id_reserva]
    ,[id_fonte]  
    ,[id_estrutura]  
    ,[id_programa]  
    ,[id_tipo_reserva]  
    ,[id_regional]  
    ,[cd_contrato]  
    ,[cd_processo]  
    ,[nr_reserva_prodesp]  
    ,[nr_reserva_siafem_siafisico]  
    ,[cd_obra]  
    ,[nr_oc]  
    ,[cd_ugo]  
    ,[cd_uo]  
    ,[cd_evento]  
    ,[nr_ano_exercicio]  
    ,[nr_ano_referencia_reserva]  
    ,[cd_origem_recurso]  
    ,[cd_destino_recurso]  
    ,[ds_observacao]  
    ,[fg_transmitido_prodesp]  
    ,[fg_transmitido_siafem]  
    ,[fg_transmitido_siafisico]  
    ,[bl_transmitir_prodesp]  
    ,[bl_transmitir_siafem]  
    ,[bl_transmitir_siafisico]  
    ,[ds_autorizado_supra_folha]  
    ,[cd_especificacao_despesa]  
    ,[ds_especificacao_despesa]  
    ,[cd_autorizado_assinatura]  
    ,[cd_autorizado_grupo]  
    ,[cd_autorizado_orgao]  
    ,[nm_autorizado_assinatura]  
    ,[ds_autorizado_cargo]  
    ,[cd_examinado_assinatura]  
    ,[cd_examinado_grupo]  
    ,[cd_examinado_orgao]  
    ,[nm_examinado_assinatura]  
    ,[ds_examinado_cargo]  
    ,[cd_responsavel_assinatura]  
    ,[cd_responsavel_grupo]  
    ,[cd_responsavel_orgao]  
    ,[nm_responsavel_assinatura]  
    ,[ds_responsavel_cargo]  
    ,[dt_emissao_reserva]
	,[ds_status_siafem_siafisico]
	,[ds_status_prodesp]  
    ,[ds_status_documento]
    ,[dt_transmissao_prodesp]
    ,[dt_transmissao_siafem_siafisico]
	,[dt_cadastro]
    ,[bl_cadastro_completo]
	,[ds_msgRetornoTransmissaoProdesp]
	,[ds_msgRetornoTransSiafemSiafisico]

   FROM [reserva].[tb_reserva] 
   where id_regional = @id_regional or nullif(@id_regional,0) = 0
   order by id_reserva desc
  
end;