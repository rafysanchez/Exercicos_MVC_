
-- ==============================================================  
-- Author:  Luis Fernando  
-- alter date: 07/11/2016
-- carlos henrique  
-- Description: Procedure para Consultar de Reserva cadastrada  
-- ==============================================================  
  
CREATE PROCEDURE [dbo].[PR_RESERVA_CONSULTAR]  
  @id_reserva      int = null  
  ,@id_fonte      int = null  
  ,@id_estrutura     int = null  
  ,@id_programa     int = null  
  ,@id_tipo_reserva    int = null  
  ,@id_regional     smallint = null  
  ,@cd_contrato     varchar(12) = null  
  ,@cd_processo     varchar(50) = null  
  ,@nr_reserva_prodesp   varchar(9) = null  
  ,@nr_reserva_siafem_siafisico varchar(11) = null  
  ,@cd_obra      int = null  
  ,@nr_oc       varchar(5)= null  
  ,@cd_ugo      varchar(6) = null  
  ,@cd_uo       varchar(5) = null  
  ,@cd_evento      int = null  
  ,@nr_ano_exercicio    int = null  
  ,@nr_ano_referencia_reserva  int = null  
  ,@cd_origem_recurso    varchar(9) = null  
  ,@cd_destino_recurso   varchar(2) = null  
  ,@ds_observacao     varchar(308) = null  
  ,@fg_transmitido_prodesp  bit = null  
  ,@fg_transmitido_siafem   bit = null  
  ,@fg_transmitido_siafisico  bit = null  
  ,@bl_transmitir_prodesp      bit = null  
  ,@bl_transmitir_siafem   bit = null  
  ,@bl_transmitir_siafisico  bit = null  
  ,@ds_autorizado_supra_folha  varchar(4) = null  
  ,@cd_especificacao_despesa  varchar(3) = null  
  ,@ds_especificacao_despesa  varchar(711) = null  
  ,@cd_autorizado_assinatura  varchar(5) = null  
  ,@cd_autorizado_grupo   varchar(1) = null  
  ,@cd_autorizado_orgao   varchar(2) = null  
  ,@nm_autorizado_assinatura  varchar(55) = null  
  ,@ds_autorizado_cargo   varchar(55) = null  
  ,@cd_examinado_assinatura  varchar(5) = null  
  ,@cd_examinado_grupo   varchar(1) = null  
  ,@cd_examinado_orgao   varchar(2) = null  
  ,@nm_examinado_assinatura  varchar(55) = null  
  ,@ds_examinado_cargo   varchar(55) = null  
  ,@cd_responsavel_assinatura  varchar(5) = null  
  ,@cd_responsavel_grupo   varchar(1) = null  
  ,@cd_responsavel_orgao   varchar(2) = null  
  ,@nm_responsavel_assinatura  varchar(55) = null  
  ,@ds_responsavel_cargo   varchar(55) = null  
  ,@dt_emissao_reservaDe   date = null  
  ,@dt_emissao_reservaAte   date = null 
  ,@ds_status_siafem_siafisico varchar(1) = null
  ,@ds_status_prodesp   varchar(1) = null
  
as  
begin  
  
 SET NOCOUNT ON;  
  
 SELECT [id_reserva]  
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
   where (@id_reserva = id_reserva OR ISNULL(@id_reserva,0) = 0) AND  
   (@id_fonte = id_fonte OR ISNULL(@id_fonte,0) = 0) AND  
   (@id_estrutura = id_estrutura OR ISNULL(@id_estrutura,0) = 0) AND  
   (@id_programa = id_programa OR ISNULL(@id_programa,0) = 0) AND  
   (@id_tipo_reserva = id_tipo_reserva OR ISNULL(@id_tipo_reserva,0) = 0) AND  
   (@id_regional = id_regional OR ISNULL(@id_regional,0) = 0) AND  
   (@cd_contrato = cd_contrato OR ISNULL(@cd_contrato,'') = '') AND  
   (@cd_processo = cd_processo OR ISNULL(@cd_processo,'') = '') AND  
   (@nr_reserva_prodesp = nr_reserva_prodesp OR ISNULL(@nr_reserva_prodesp,'') = '') AND  
   (@nr_reserva_siafem_siafisico = nr_reserva_siafem_siafisico OR ISNULL(@nr_reserva_siafem_siafisico,'') = '') AND  
   (@cd_obra = cd_obra OR ISNULL(@cd_obra,0) = 0) AND  
   (@nr_oc = nr_oc OR ISNULL(@nr_oc,0) = 0) AND  
   (@cd_ugo = cd_ugo OR ISNULL(@cd_ugo,0) = 0) AND  
   (@cd_uo = cd_uo OR ISNULL(@cd_uo,0) = 0) AND  
   (@cd_evento = cd_evento OR ISNULL(@cd_evento,0) = 0) AND  
   (@nr_ano_exercicio = nr_ano_exercicio OR ISNULL(@nr_ano_exercicio,0) = 0) AND  
   (@nr_ano_referencia_reserva = nr_ano_referencia_reserva OR ISNULL(@nr_ano_referencia_reserva,0) = 0) AND  
   (@cd_origem_recurso = cd_origem_recurso OR ISNULL(@cd_origem_recurso,0) = 0) AND  
   (@cd_destino_recurso = cd_destino_recurso OR ISNULL(@cd_destino_recurso,0) = 0) AND  
   (@ds_observacao = ds_observacao OR ISNULL(@ds_observacao,'') = '') AND  
   (@fg_transmitido_prodesp = fg_transmitido_prodesp OR ISNULL(@fg_transmitido_prodesp,0) = 0) AND  
   ((@fg_transmitido_siafem = fg_transmitido_siafem OR ISNULL(@fg_transmitido_siafem,0) = 0) OR  
   (@fg_transmitido_siafisico = fg_transmitido_siafisico OR ISNULL(@fg_transmitido_siafisico,0) = 0)) AND  
   (@bl_transmitir_prodesp = bl_transmitir_prodesp OR ISNULL(@bl_transmitir_prodesp,0) = 0) AND  
   (@bl_transmitir_siafem = bl_transmitir_siafem OR ISNULL(@bl_transmitir_siafem,0) = 0) AND  
   (@bl_transmitir_siafisico = bl_transmitir_siafisico OR ISNULL(@bl_transmitir_siafisico,0) = 0) AND  
   (@ds_autorizado_supra_folha = ds_autorizado_supra_folha OR ISNULL(@ds_autorizado_supra_folha,'') = '') AND  
   (@cd_especificacao_despesa = cd_especificacao_despesa OR ISNULL(@cd_especificacao_despesa,0) = 0) AND  
   (@ds_especificacao_despesa = ds_especificacao_despesa OR ISNULL(@ds_especificacao_despesa,'') = '') AND  
   (@cd_autorizado_assinatura = cd_autorizado_assinatura OR ISNULL(@cd_autorizado_assinatura,0) = 0) AND  
   (@cd_autorizado_grupo = cd_autorizado_grupo OR ISNULL(@cd_autorizado_grupo,0) = 0) AND  
   (@cd_autorizado_orgao = cd_autorizado_orgao OR ISNULL(@cd_autorizado_orgao,0) = 0) AND  
   (@nm_autorizado_assinatura = nm_autorizado_assinatura OR ISNULL(@nm_autorizado_assinatura,'') ='') AND  
   (@ds_autorizado_cargo = ds_autorizado_cargo OR ISNULL(@ds_autorizado_cargo,'') = '') AND  
   (@cd_examinado_assinatura = cd_examinado_assinatura OR ISNULL(@cd_examinado_assinatura,0) = 0) AND  
   (@cd_examinado_grupo = cd_examinado_grupo OR ISNULL(@cd_examinado_grupo,0) = 0) AND  
   (@cd_examinado_orgao = cd_examinado_orgao OR ISNULL(@cd_examinado_orgao,0) = 0) AND  
   (@nm_examinado_assinatura = nm_examinado_assinatura OR ISNULL(@nm_examinado_assinatura,'') = '') AND  
   (@ds_examinado_cargo = ds_examinado_cargo OR ISNULL(@ds_examinado_cargo,'') = '') AND  
   (@cd_responsavel_assinatura = cd_responsavel_assinatura OR ISNULL(@cd_responsavel_assinatura,0) = 0) AND  
   (@cd_responsavel_grupo = cd_responsavel_grupo OR ISNULL(@cd_responsavel_grupo,0) = 0) AND  
   (@cd_responsavel_orgao = cd_responsavel_orgao OR ISNULL(@cd_responsavel_orgao,0) = 0) AND  
   (@nm_responsavel_assinatura = nm_responsavel_assinatura OR ISNULL(@nm_responsavel_assinatura,'') = '') AND  
   (@ds_responsavel_cargo = ds_responsavel_cargo OR ISNULL(@ds_responsavel_cargo,'') = '') AND  
   --(@dt_emissao_reserva = dt_emissao_reserva OR ISNULL(@dt_emissao_reserva,'') = '');  
   (cast(dt_emissao_reserva as date) >= @dt_emissao_reservaDe or @dt_emissao_reservaDe is null)  AND
   (cast(dt_emissao_reserva as date) <= @dt_emissao_reservaAte or @dt_emissao_reservaAte is null)   AND  
   ((ds_status_siafem_siafisico = @ds_status_siafem_siafisico OR ISNULL(@ds_status_siafem_siafisico,'') = '') OR  
   (ds_status_prodesp = @ds_status_prodesp OR ISNULL(@ds_status_prodesp,'') = ''))
  
end;