-- ===================================================================      
-- Author:  Rodrigo Cesar de Freitas  
-- Create date: 20/02/2017  
-- Description: Procedure para salvar ou alterar de itens para subempenho  
-- ===================================================================  
CREATE procedure [dbo].[PR_SUBEMPENHO_CANCELAMENTO_ITEM_SALVAR]  
 @id_subempenho_cancelamento_item int  
, @tb_subempenho_cancelamento_id_subempenho_cancelamento int  
, @cd_servico varchar(10) = null  
, @cd_unidade_fornecimento varchar(5) = null  
, @qt_material_servico decimal(12,3) = null  
, @cd_status_siafisico char(1) = null  
, @nr_sequencia int = null
, @transmitir bit = null  
as  
begin  
  
 set nocount on;  
  
 if exists (   
  select 1   
  from pagamento.tb_subempenho_cancelamento_item  
  where id_subempenho_cancelamento_item = @id_subempenho_cancelamento_item  
   and tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento  
 )  
 begin  
   
  update pagamento.tb_subempenho_cancelamento_item set   
    cd_servico = @cd_servico  
   , cd_unidade_fornecimento = @cd_unidade_fornecimento  
   , qt_material_servico = @qt_material_servico  
   , cd_status_siafisico = @cd_status_siafisico  
   , nr_sequencia = @nr_sequencia  
   , transmitir = @transmitir
  where id_subempenho_cancelamento_item = @id_subempenho_cancelamento_item  
   and tb_subempenho_cancelamento_id_subempenho_cancelamento = @tb_subempenho_cancelamento_id_subempenho_cancelamento  
  
  select @id_subempenho_cancelamento_item;  
  
 end  
 else  
 begin  
  
  insert into pagamento.tb_subempenho_cancelamento_item (  
    tb_subempenho_cancelamento_id_subempenho_cancelamento  
   , cd_servico  
   , cd_unidade_fornecimento  
   , qt_material_servico  
   , cd_status_siafisico  
   , nr_sequencia  
   , transmitir
  )  
  values (  
    @tb_subempenho_cancelamento_id_subempenho_cancelamento  
   , @cd_servico  
   , @cd_unidade_fornecimento  
   , @qt_material_servico  
   , @cd_status_siafisico  
   , @nr_sequencia  
   , @transmitir

  )    
             
  select scope_identity();  
  
 end  
   
end