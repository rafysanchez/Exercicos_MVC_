CREATE TABLE [configuracao].[tb_estrutura] (
    [id_estrutura]        INT          IDENTITY (1, 1) NOT NULL,
    [id_programa]         INT          NOT NULL,
    [ds_nomenclatura]     VARCHAR (45) NULL,
    [cd_natureza]         VARCHAR (6)  NULL,
    [cd_macro]            VARCHAR (6)  NULL,
    [cd_codigo_aplicacao] VARCHAR (9)  NULL,
    [id_fonte]            VARCHAR (2)  NULL,
    CONSTRAINT [PK_tb_estrutura] PRIMARY KEY CLUSTERED ([id_estrutura] ASC),
    CONSTRAINT [FK_tb_estrutura_tb_programa] FOREIGN KEY ([id_programa]) REFERENCES [configuracao].[tb_programa] ([id_programa])
);




GO
CREATE NONCLUSTERED INDEX [tb_estrutura_FKIndex1]
    ON [configuracao].[tb_estrutura]([id_programa] ASC);

