CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_CANCELAMENTO_REDUCAO_CONSULTAR]        
           --@id_cancelamento_movimentacao int =NULL,          
           --@tb_fonte_id_fonte int =NULL,          
           @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria int =NULL,          
           @nr_agrupamento int =NULL          
           --@nr_seq int =NULL          
           --@nr_nota_cancelamento varchar(10) =NULL,          
           --@cd_unidade_gestora varchar(15) =NULL,          
           --@nr_categoria_gasto varchar(15) =NULL,          
           --@ds_observacao varchar(231) =NULL,          
           --@fg_transmitido_prodesp char(1) =NULL,          
           --@fg_transmitido_siafem char(1)= NULL          
          
            
AS              
BEGIN              
 SET NOCOUNT ON;            
          
          
          
SELECT c.[id_cancelamento_movimentacao] as IdCancelamento          
      ,c.[tb_fonte_id_fonte]  as Fonte          
      ,c.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria] as IdMovimentacao          
      ,c.[nr_agrupamento] as NrAgrupamento          
      ,c.[nr_seq] as NrSequencia          
      ,c.[nr_siafem] as NrSiafem          
      ,c.[cd_unidade_gestora] as UnidadeGestoraFavorecida       
   ,o.[cd_unidade_gestora_emitente] as UnidadeGestoraEmitente       
      ,c.[nr_categoria_gasto] as CategoriaGasto          
      ,c.[ds_observacao] as ObservacaoCancelamento          
      ,c.[ds_observacao2] as ObservacaoCancelamento2          
      ,c.[ds_observacao3] as ObservacaoCancelamento3          
      ,c.[fg_transmitido_prodesp]   as StatusProdespItem        
      ,c.[ds_msgRetornoProdesp]   as MensagemProdespItem        
      ,c.[fg_transmitido_siafem]   as StatusSiafemItem        
      ,c.[ds_msgRetornoSiafem]  as MensagemSiafemItem        
   ,rs.id_reducao_suplementacao as IdReducaoSuplementacao         
   ,rs.cd_destino_recurso as DestinoRecurso          
   ,rs.nr_orgao as NrOrgao          
   ,rs.nr_suplementacao_reducao as NrSuplementacaoReducao          
   ,c.valor as ValorCancelamentoReducao          
   ,c.cd_gestao_favorecido  as IdGestaoFavorecida      
   ,o.cd_gestao_emitente as IdGestaoEmitente  
   ,c.eventoNC as EventoNC         
  , rs.flag_red_sup as FlagRedistribuicao       
      ,rs.[fl_proc]   as FlProc      
      ,rs.[nr_processo]   as NrProcesso      
      ,rs.[nr_orgao]   as  NrOrgao      
      ,rs.[nr_obra]   as  NrObra      
      ,rs.[cd_origem_recurso]   as  OrigemRecurso      
      ,rs.[cd_destino_recurso]  as DestinoRecurso      
      ,rs.[cd_especificacao_despesa]  as EspecDespesa      
      ,rs.[ds_especificacao_despesa]  as DescEspecDespesa      
      ,rs.[cd_autorizado_assinatura] as CodigoAutorizadoAssinatura       
      ,rs.[cd_autorizado_grupo]  as CodigoAutorizadoGrupo      
      ,rs.[cd_autorizado_orgao]  as CodigoAutorizadoOrgao      
      ,rs.[ds_autorizado_cargo]   as DescricaoAutorizadoCargo      
      ,rs.[nm_autorizado_assinatura]   as NomeAutorizadoAssinatura      
      ,rs.[cd_examinado_assinatura]  as CodigoExaminadoAssinatura      
      ,rs.[cd_examinado_grupo]  as CodigoExaminadoGrupo      
      ,rs.[cd_examinado_orgao]  as  CodigoExaminadoOrgao      
      ,rs.[ds_examinado_cargo]  as DescricaoExaminadoCargo      
      ,rs.[nm_examinado_assinatura]  as NomeExaminadoAssinatura      
      ,rs.[cd_responsavel_assinatura]   as CodigoResponsavelAssinatura      
      ,rs.[cd_responsavel_grupo]   as CodigoResponsavelGrupo       
      ,rs.[cd_responsavel_orgao]  as CodigoResponsavelOrgao      
      ,rs.[ds_responsavel_cargo]  as DescricaoResponsavelCargo      
   ,rs.[nm_responsavel_assinatura] as NomeResponsavelAssinatura      
   ,rs.[TotalQ1]      
   ,rs.[TotalQ2]      
   ,rs.[TotalQ3]      
   ,rs.[TotalQ4]      
   , o.tb_programa_id_programa as  ProgramaId      
   ,o.tb_estrutura_id_estrutura as NaturezaId    
   ,o.tb_tipo_documento_movimentacao_id_tipo_documento_movimentacao as IdTipoDocumento    
   ,o.tb_tipo_movimentacao_orcamentaria_id_tipo_movimentacao_orcamentaria as IdTipoMovimentacao    
       
             
  FROM [movimentacao].[tb_cancelamento_movimentacao]   c          
      inner  join [movimentacao].[tb_reducao_suplementacao]  rs on c.id_cancelamento_movimentacao = rs.tb_cancelamento_movimentacao_id_cancelamento_movimentacao      
   left join    [movimentacao].[tb_movimentacao_orcamentaria] o on c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria     
          
  WHERE           
  (  c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and           
  (  c.nr_agrupamento = @nr_agrupamento )            
  -- ( nullif( @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria, 0 ) is null or c.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and           
  --( nullif( @nr_agrupamento, 0 ) is null or c.nr_agrupamento = @nr_agrupamento )              
  --( nullif( @nr_seq, 0 ) is null or c.nr_seq = @nr_seq )              
          
          
  ORDER BY id_cancelamento_movimentacao,c.nr_seq          
          
          
          
  END