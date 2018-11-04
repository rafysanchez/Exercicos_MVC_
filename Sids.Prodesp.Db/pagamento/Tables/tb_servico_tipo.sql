CREATE TABLE [pagamento].[tb_servico_tipo] (
    [id_servico_tipo] INT           IDENTITY (1, 1) NOT NULL,
    [ds_servico_tipo] VARCHAR (140) NOT NULL,
    [cd_rap_tipo]     VARCHAR (2)   NULL,
    CONSTRAINT [PK_tb_servico_tipo] PRIMARY KEY CLUSTERED ([id_servico_tipo] ASC),
    CONSTRAINT [IX_tb_servico_tipo_ds_servico_tipo] UNIQUE NONCLUSTERED ([ds_servico_tipo] ASC)
);



