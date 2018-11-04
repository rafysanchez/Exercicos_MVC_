CREATE TABLE [pagamento].[tb_evento_tipo] (
    [id_evento_tipo]   INT           NOT NULL,
    [ds_evento_tipo]   VARCHAR (140) NULL,
    [cd_tp_subempenho] INT           NULL,
    CONSTRAINT [PK_tb_evento_tipo] PRIMARY KEY CLUSTERED ([id_evento_tipo] ASC)
);



