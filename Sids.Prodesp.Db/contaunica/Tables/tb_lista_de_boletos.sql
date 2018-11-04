CREATE TABLE [contaunica].[tb_lista_de_boletos] (
    [id_lista_de_boletos]                         INT             IDENTITY (1, 1) NOT NULL,
    [id_regional]                                 SMALLINT        NULL,
    [id_tipo_documento]                           INT             NULL,
    [dt_cadastro]                                 DATE            NULL,
    [nr_siafem_siafisico]                         VARCHAR (11)    NULL,
    [nr_contrato]                                 VARCHAR (13)    NULL,
    [nr_documento]                                VARCHAR (19)    NULL,
    [cd_unidade_gestora]                          VARCHAR (6)     NULL,
    [cd_gestao]                                   VARCHAR (5)     NULL,
    [cd_aplicacao_obra]                           VARCHAR (140)   NULL,
    [dt_emissao]                                  DATE            NULL,
    [nr_cnpj_favorecido]                          VARCHAR (14)    NULL,
    [ds_nome_da_lista]                            VARCHAR (50)    NULL,
    [ds_copiar_da_lista]                          VARCHAR (1)     NULL,
    [nr_total_de_credores]                        INT             NULL,
    [vl_total_da_lista]                           DECIMAL (18, 2) NULL,
    [fl_sistema_siafem_siafisico]                 BIT             NULL,
    [cd_transmissao_status_siafem_siafisico]      CHAR (1)        NULL,
    [fl_transmissao_transmitido_siafem_siafisico] BIT             NULL,
    [dt_transmissao_transmitido_siafem_siafisico] DATE            NULL,
    [ds_transmissao_mensagem_siafem_siafisico]    VARCHAR (140)   NULL,
    [bl_cadastro_completo]                        BIT             NULL,
    [id_codigo_de_barras]                         INT             NULL,
    CONSTRAINT [PK_tb_lista_de_boletos] PRIMARY KEY CLUSTERED ([id_lista_de_boletos] ASC),
    CONSTRAINT [FK_tb_lista_de_boletos_tb_regional] FOREIGN KEY ([id_regional]) REFERENCES [seguranca].[tb_regional] ([id_regional]),
    CONSTRAINT [FK_tb_lista_de_boletos_tb_tipo_documento] FOREIGN KEY ([id_tipo_documento]) REFERENCES [contaunica].[tb_tipo_documento] ([id_tipo_documento])
);



