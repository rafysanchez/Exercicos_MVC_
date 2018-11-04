CREATE TABLE [contaunica].[tb_itens_obs_rt] (
    [id_tb_itens_obs_rt]            INT             IDENTITY (1, 1) NOT NULL,
    [cd_relob_rt]                   VARCHAR (11)    NULL,
    [nr_ob_rt]                      VARCHAR (11)    NULL,
    [cd_conta_bancaria_emitente]    VARCHAR (9)     NULL,
    [cd_unidade_gestora_favorecida] VARCHAR (6)     NULL,
    [cd_gestao_favorecida]          VARCHAR (5)     NULL,
    [ds_mnemonico_ug_favorecida]    VARCHAR (15)    NULL,
    [ds_banco_favorecido]           VARCHAR (3)     NULL,
    [cd_agencia_favorecido]         VARCHAR (5)     NULL,
    [ds_conta_favorecido]           VARCHAR (10)    NULL,
    [vl_ob]                         DECIMAL (15, 2) NULL,
    CONSTRAINT [PK_tb_itens_obs_rt] PRIMARY KEY CLUSTERED ([id_tb_itens_obs_rt] ASC)
);



