CREATE TABLE [contaunica].[tb_itens_obs_re] (
    [id_tb_itens_obs_re]    INT             IDENTITY (1, 1) NOT NULL,
    [cd_relob_re]           VARCHAR (11)    NULL,
    [nr_ob_re]              VARCHAR (11)    NULL,
    [fg_prioridade]         VARCHAR (1)     NULL,
    [cd_tipo_ob]            INT             NULL,
    [ds_nome_favorecido]    VARCHAR (50)    NULL,
    [ds_banco_favorecido]   VARCHAR (3)     NULL,
    [cd_agencia_favorecido] VARCHAR (5)     NULL,
    [ds_conta_favorecido]   VARCHAR (10)    NULL,
    [vl_ob]                 DECIMAL (15, 2) NULL,
    CONSTRAINT [PK_tb_itens_obs_re] PRIMARY KEY CLUSTERED ([id_tb_itens_obs_re] ASC)
);



