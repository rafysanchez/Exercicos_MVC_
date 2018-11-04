CREATE TABLE [contaunica].[tb_desdobramento_retencao] (
    [id_credor]          INT          IDENTITY (1, 1) NOT NULL,
    [nm_reduzido_credor] VARCHAR (14) NULL,
    [nr_cnpj_credor]     VARCHAR (14) NULL,
    CONSTRAINT [PK_tb_desdobramento_retencao] PRIMARY KEY CLUSTERED ([id_credor] ASC)
);

