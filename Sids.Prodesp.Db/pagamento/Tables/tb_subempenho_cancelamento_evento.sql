CREATE TABLE [pagamento].[tb_subempenho_cancelamento_evento] (
    [id_subempenho_cancelamento_evento]                     INT          IDENTITY (1, 1) NOT NULL,
    [tb_subempenho_cancelamento_id_subempenho_cancelamento] INT          NULL,
    [cd_fonte]                                              VARCHAR (10) NULL,
    [cd_evento]                                             VARCHAR (6)  NULL,
    [cd_classificacao]                                      VARCHAR (9)  NULL,
    [ds_inscricao]                                          VARCHAR (22) NULL,
    [vl_evento]                                             INT          NULL,
    CONSTRAINT [PK_tb_subempenho_cancelamento_evento] PRIMARY KEY CLUSTERED ([id_subempenho_cancelamento_evento] ASC),
    CONSTRAINT [FK_tb_subempenho_cancelamento_evento_tb_subempenho_cancelamento] FOREIGN KEY ([tb_subempenho_cancelamento_id_subempenho_cancelamento]) REFERENCES [pagamento].[tb_subempenho_cancelamento] ([id_subempenho_cancelamento])
);



