--CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_DISTRIBUICAO_SUPLEMENTACAO_CONSULTAR]       
--           --@id_cancelamento_movimentacao int =NULL,          
--           --@tb_fonte_id_fonte int =NULL,          
--           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,          
--           @nr_agrupamento int =NULL          
--           --@nr_seq int =NULL          
--           --@nr_nota_cancelamento varchar(10) =NULL,          
--           --@cd_unidade_gestora varchar(15) =NULL,          
--           --@nr_categoria_gasto varchar(15) =NULL,          
--           --@ds_observacao varchar(231) =NULL,          
--           --@fg_transmitido_prodesp char(1) =NULL,          
--           --@fg_transmitido_siafem char(1)= NULL          
          
            
--AS              
--BEGIN              
-- SET NOCOUNT ON;            
          
          
          
--SELECT d.id_distribuicao_movimentacao as IdDistribuicao          
--      ,d.[tb_fonte_id_fonte] as Fonte          
--      ,d.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao          
--      ,d.[nr_agrupamento] as NrAgrupamento          
--      ,d.[nr_seq] as NrSequencia          
--      ,d.[nr_nota_distribuicao] as NrNotaDistribuicao           
--      ,d.[cd_unidade_gestora_favorecido] as UnidadeGestoraFavorecida     
--   ,o.[cd_unidade_gestora_emitente] as UnidadeGestoraEmitente         
--      ,d.[nr_categoria_gasto] as CategoriaGasto          
--      ,d.[ds_observacao] as ObservacaoCancelamento         
--   ,d.[ds_observacao2] as ObservacaoCancelamento2        
--   ,d.[ds_observacao3] as ObservacaoCancelamento3         
--       ,d.[fg_transmitido_prodesp]   as StatusProdespItem        
--      ,d.[ds_msgRetornoProdesp]   as MensagemProdespItem        
--      ,d.[fg_transmitido_siafem]   as StatusSiafemItem        
--      ,d.[ds_msgRetornoSiafem]  as MensagemSiafemItem         
--   ,rs.id_reducao_suplementacao as IdReducaoSuplementacao          
--   ,rs.cd_destino_recurso as DestinoRecurso          
--   ,rs.nr_orgao as NrOrgao          
--   ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao          
--   ,d.valor as ValorDistribuicaoSuplementacao        
--   ,d.cd_gestao_favorecido    as IdGestaoFavorecida    
--   ,d.eventoNC as EventoNC        
--   , rs.flag_red_sup as FlagRedistribuicao      
--    ,rs.tb_programa_id_programa as  ProgramaId  
--      ,rs.[fl_proc]   as FlProc  
--      ,rs.[nr_processo]   as NrProcesso  
--      ,rs.[nr_orgao]   as  NrOrgao  
--      ,rs.[nr_obra]   as  NrObra  
--      ,rs.[cd_origem_recurso]   as  OrigemRecurso  
--      ,rs.[cd_destino_recurso]  as DestinoRecurso  
--      ,rs.[cd_especificacao_despesa]  as EspecDespesa  
--      ,rs.[ds_especificacao_despesa]  as DescEspecDespesa  
--      ,rs.[cd_autorizado_assinatura] as CodigoAutorizadoAssinatura   
--      ,rs.[cd_autorizado_grupo]  as CodigoAutorizadoGrupo  
--      ,rs.[cd_autorizado_orgao]  as CodigoAutorizadoOrgao  
--      ,rs.[ds_autorizado_cargo]   as DescricaoAutorizadoCargo  
--      ,rs.[nm_autorizado_assinatura]   as NomeAutorizadoAssinatura  
--      ,rs.[cd_examinado_assinatura]  as CodigoExaminadoAssinatura  
--      ,rs.[cd_examinado_grupo]  as CodigoExaminadoGrupo  
--      ,rs.[cd_examinado_orgao]  as  CodigoExaminadoOrgao  
--      ,rs.[ds_examinado_cargo]  as DescricaoExaminadoCargo  
--      ,rs.[nm_examinado_assinatura]  as NomeExaminadoAssinatura  
--      ,rs.[cd_responsavel_assinatura]   as CodigoResponsavelAssinatura  
--      ,rs.[cd_responsavel_grupo]   as CodigoResponsavelGrupo   
--      ,rs.[cd_responsavel_orgao]  as CodigoResponsavelOrgao  
--      ,rs.[ds_responsavel_cargo]  as DescricaoResponsavelCargo  
--   ,rs.[nm_responsavel_assinatura] as NomeResponsavelAssinatura  
--   ,rs.[TotalQ1]  
--   ,rs.[TotalQ2]  
--   ,rs.[TotalQ3]  
--   ,rs.[TotalQ4]  
--  , o.tb_programa_id_programa as  ProgramaId      
--   ,o.tb_estrutura_id_estrutura as NaturezaId    
--   ,o.tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao as IdTipoDocumento    
--   ,o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria as IdTipoMovimentacao    
             
--  FROM [movimentacao].[tb_distribuicao_movimentacao]   D          
--      left  join [movimentacao].[tb_reducao_suplementacao]  rs on d.id_distribuicao_movimentacao = rs.tb_distribuicao_movimentacao_id_distribuicao_movimentacao     
--    left join    [movimentacao].[tb_movimentacao_orcamentaria] o on d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria        
          
--  WHERE           
--  (  d.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and           
--  ( d.nr_agrupamento = @nr_agrupamento )              
--  --( nullif( @nr_seq, 0 ) is null or d.nr_seq = @nr_seq )              
          
          
--  ORDER BY id_distribuicao_movimentacao,d.nr_seq          
          
          
          
--  END