--CREATE PROCEDURE [dbo].[PR_MOVIMENTACAO_NC_CONSULTAR]          
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
        
        
        
--SELECT n.[id_nota_credito] as IdNotaCredito        
--	,n.[tb_programa_id_programa] as ProgramaId        
--	,n.[tb_fonte_id_fonte] as IdFonte            
--	,f.[cd_fonte] as Fonte        
--	,n.[tb_estrutura_id_estrutura] as IdEstrutura        
--	,n.[tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria]  as IdMovimentacao        
--	,n.[nr_agrupamento] as NrAgrupamento        
--	,n.[nr_seq] as NrSequencia         
--	,n.[nr_nota_credito] as NrNotaCredito   
--	,n.[nr_siafem]
--	,n.[vr_credito]     
--	,n.[cd_unidade_gestora_favorecido]  as UnidadeGestoraFavorecida  
--	,o.[cd_gestao_emitente] as IdGestaoEmitente  
--	,n.[cd_uo] as Uo   
--	,n.[cd_ugo] as Ugo      
--	,n.[fonte_recurso] as FonteRecurso
--	,n.[plano_interno] as plano_interno        
--	,n.[vr_credito] as ValorCredito       
--	,n.[ds_observacao] as ObservacaoNC  
--	,n.[ds_observacao2] as ObservacaoNC2 
--	,n.[ds_observacao3] as ObservacaoNC3      
--	,o.cd_unidade_gestora_emitente as UnidadeGestoraEmitente    
--	,n.eventoNC as EventoNC  
--	,n.cd_gestao_favorecido as IdGestaoFavorecida  
--	,n.[fg_transmitido_prodesp]   as StatusProdespItem  
--	,n.[ds_msgRetornoProdesp]   as MensagemProdespItem  
--	,n.[fg_transmitido_siafem]   as StatusSiafemItem  
--	,n.[ds_msgRetornoSiafem]  as MensagemSiafemItem  
--  FROM [movimentacao].[tb_credito_movimentacao] n        
--	inner join movimentacao.tb_movimentacao_orcamentaria o on n.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = o.id_movimentacao_orcamentaria    
--	left join configuracao.tb_fonte f on f.id_fonte = n.tb_fonte_id_fonte
        
--  WHERE         
--  ( n.tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria = @tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria )   and         
--  ( n.nr_agrupamento = @nr_agrupamento )           
        
--  ORDER BY id_nota_credito,nr_seq         

        
--  END