CREATE TABLE [contaunica].[tb_programacao_desembolso_evento] (
    [id_programacao_desembolso_evento] INT          IDENTITY (1, 1) NOT NULL,
    [id_programacao_desembolso]        INT          NULL,
    [cd_fonte]                         VARCHAR (10) NULL,
    [cd_evento]                        VARCHAR (6)  NULL,
    [cd_classificacao]                 VARCHAR (9)  NULL,
    [ds_inscricao]                     VARCHAR (22) NULL,
    [vl_evento]                        INT          NULL,
    CONSTRAINT [PK_tb_programacao_desembolso_evento] PRIMARY KEY CLUSTERED ([id_programacao_desembolso_evento] ASC),
    CONSTRAINT [FK_tb_programacao_desembolso_evento_tb_programacao_desembolso] FOREIGN KEY ([id_programacao_desembolso]) REFERENCES [contaunica].[tb_programacao_desembolso] ([id_programacao_desembolso])
);



