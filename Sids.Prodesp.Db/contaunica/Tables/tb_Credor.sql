CREATE TABLE [contaunica].[tb_Credor] (
    [id_credor]             INT           IDENTITY (1, 1) NOT NULL,
    [nm_prefeitura]         VARCHAR (100) NULL,
    [cd_cpf_cnpj_ug_credor] VARCHAR (14)  NULL,
    [bl_conveniado]         BIT           NULL,
    [bl_base_calculo]       BIT           NULL,
    [nm_reduzido_credor]    VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_Credor] PRIMARY KEY CLUSTERED ([id_credor] ASC)
);



