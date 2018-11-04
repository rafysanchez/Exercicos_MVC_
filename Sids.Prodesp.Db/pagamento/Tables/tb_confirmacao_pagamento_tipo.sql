CREATE TABLE [pagamento].[tb_confirmacao_pagamento_tipo] (
    [id_confirmacao_tipo] INT           IDENTITY (1, 1) NOT NULL,
    [ds_confirmacao_tipo] NVARCHAR (50) NULL,
    CONSTRAINT [PK_tb_confirmacao_pagamento_tipo] PRIMARY KEY CLUSTERED ([id_confirmacao_tipo] ASC)
);

