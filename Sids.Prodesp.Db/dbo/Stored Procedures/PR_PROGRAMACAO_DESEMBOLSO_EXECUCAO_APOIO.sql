CREATE PROCEDURE [dbo].[PR_PROGRAMACAO_DESEMBOLSO_EXECUCAO_APOIO]  
 @tipo int,  
 @nr_siafem_siafisico varchar(11) = null  
AS  
  
 --DECLARE @tipo int = 1 --PD ou OB  
  
 --IF LEN(@nr_siafem_siafisico) <= 5  
 -- SET @tipo = 2  
  
 --PD  
 IF @tipo = 1  
 BEGIN   
   
  IF EXISTS(  
   SELECT PD.id_programacao_desembolso  
   FROM [contaunica].[tb_programacao_desembolso] PD (nolock)  
   where nr_siafem_siafisico = @nr_siafem_siafisico  
  )  
  BEGIN   
  print 'existe'
   SELECT TOP 1  
     PD.nr_agrupamento  
    ,PD.id_programacao_desembolso  
    ,EI.id_programacao_desembolso_execucao_item  
    ,PD.nr_cnpj_cpf_pgto  
    ,nr_siafem_siafisico  
    ,[PD].[id_tipo_documento]  
    ,[PD].[nr_contrato]  
    ,[PD].[nr_documento]  
    ,[PD].[nr_documento_gerador]  
    ,[PI].cd_transmissao_status_prodesp  
    ,[PI].fl_transmissao_transmitido_prodesp  
    ,[PI].dt_transmissao_transmitido_prodesp  
    ,[PI].ds_transmissao_mensagem_prodesp  
	,EI.ob_cancelada as Cancelado
    ,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico  
    ,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico  
    ,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico  
    ,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico
	, PD.cd_aplicacao_obra
   FROM [contaunica].[tb_programacao_desembolso] PD (nolock)  
   LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PD.nr_siafem_siafisico  
   LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].nr_documento_gerador = EI.nr_documento_gerador  
   where  
    ( @nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico )  
    --AND nr_agrupamento_pd <> 0  
   ORDER BY [PI].cd_transmissao_status_prodesp DESC  
  END  
  
  ELSE  
  
  BEGIN  
  print 'não existe'
   Select TOP 1  
   PDA.nr_agrupamento  
   ,EI.id_programacao_desembolso_execucao_item  
   , PDA.nr_cnpj_cpf_pgto  
   ,PDA.[id_tipo_documento]  
   ,PD.[nr_contrato]  
   ,PDA.[nr_documento]  
   ,PDA.[nr_documento_gerador]  
  
   ,[PI].cd_transmissao_status_prodesp  
   ,[PI].fl_transmissao_transmitido_prodesp  
   ,[PI].dt_transmissao_transmitido_prodesp  
   ,[PI].ds_transmissao_mensagem_prodesp  
  
   ,EI.ob_cancelada as Cancelado
   ,EI.cd_transmissao_status_siafem as cd_transmissao_status_siafem_siafisico  
   ,EI.fl_transmissao_transmitido_siafem as fl_transmissao_transmitido_siafem_siafisico  
   ,EI.dt_transmissao_transmitido_siafem as dt_transmissao_transmitido_siafem_siafisico  
   ,EI.ds_transmissao_mensagem_siafem as ds_transmissao_mensagem_siafem_siafisico  
   , PD.cd_aplicacao_obra  
   from [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock)  
   LEFT JOIN contaunica.tb_programacao_desembolso AS PD ON PD.id_programacao_desembolso = PDA.id_programacao_desembolso
   LEFT JOIN contaunica.tb_programacao_desembolso_execucao_item AS EI ON EI.ds_numpd = PDA.nr_programacao_desembolso  
   LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON [PI].nr_documento_gerador = PDA.nr_documento_gerador  
   WHERE ( @nr_siafem_siafisico IS NULL OR PDA.nr_programacao_desembolso = @nr_siafem_siafisico )  
   --AND nr_agrupamento_pd = 0  
   --ORDER BY EI.id_execucao_pd DESC  
   ORDER BY [PI].cd_transmissao_status_prodesp DESC  
  END  
 END  
   
 ELSE   
 --Tipo = 2 --OB  
 BEGIN  
  IF LEN(@nr_siafem_siafisico) <= 5  
	SET @nr_siafem_siafisico = CONVERT(VARCHAR, YEAR(GETDATE()), 4) + 'OB' + @nr_siafem_siafisico

  SELECT TOP 1  
   PDE.ds_numob  
   ,AI.id_autorizacao_ob  
   ,AI.id_autorizacao_ob_item  
   ,[PI].id_confirmacao_pagamento_item
   ,[PDE].id_execucao_pd  
   ,[PDE].id_programacao_desembolso_execucao_item  
   ,PDE.nr_op as ds_numop  
   ,PDE.ds_consulta_op_prodesp  
   ,PDE.id_execucao_pd AS nr_agrupamento_ob  
   ,[PDE].id_programacao_desembolso_execucao_item  as id_execucao_pd_item  
   ,[PDE].[id_tipo_documento]  
   ,[PDE].[nr_documento]  
   ,[PDE].[nr_contrato]  
   ,[PDE].[nr_documento_gerador]  
   ,[PDE].ob_cancelada as Cancelado
   ,[PI].cd_transmissao_status_prodesp cd_transmissao_status_prodesp -- Transmissão Confirmação de Pagto  
   ,[PI].fl_transmissao_transmitido_prodesp fl_transmissao_transmitido_prodesp  
   ,[PI].dt_transmissao_transmitido_prodesp dt_transmissao_transmitido_prodesp  
   ,[PI].ds_transmissao_mensagem_prodesp ds_transmissao_mensagem_prodesp  
   ,AI.cd_transmissao_item_status_siafem -- Transmissão Autorização de OB  
   ,AI.fl_transmissao_item_siafem  
   ,AI.dt_transmissao_item_transmitido_siafem  
   ,AI.ds_transmissao_item_mensagem_siafem  
   ,ISNULL(PD.cd_despesa, PDA.cd_despesa) AS cd_despesa  
   ,ISNULL(PD.cd_aplicacao_obra, PD2.cd_aplicacao_obra) AS cd_aplicacao_obra
  FROM contaunica.tb_programacao_desembolso_execucao_item AS PDE   
  LEFT JOIN [contaunica].[tb_programacao_desembolso_agrupamento] AS PDA (nolock) ON RIGHT(PDA.nr_programacao_desembolso,5) = RIGHT(PDE.ds_numpd ,5)
  LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD (nolock) ON PD.id_programacao_desembolso = PDA.id_programacao_desembolso
  LEFT JOIN [contaunica].[tb_programacao_desembolso] AS PD2 (nolock) ON RIGHT(PD2.nr_siafem_siafisico,5) = RIGHT(PDE.ds_numpd ,5)
  LEFT JOIN pagamento.tb_confirmacao_pagamento_item AS [PI] ON PDE.id_programacao_desembolso_execucao_item = PI.id_programacao_desembolso_execucao_item 
  LEFT JOIN contaunica.tb_autorizacao_ob_itens AS AI ON PDE.id_programacao_desembolso_execucao_item = AI.id_execucao_pd_item
  
  where RIGHT(PDE.ds_numob, 5) = RIGHT(@nr_siafem_siafisico, 5)
  ORDER BY AI.id_autorizacao_ob DESC  
  
 END