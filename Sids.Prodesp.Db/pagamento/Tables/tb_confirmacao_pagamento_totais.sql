CREATE TABLE [pagamento].[tb_confirmacao_pagamento_totais] (
    [id_confirmacao_pagamento_total] INT            IDENTITY (1, 1) NOT NULL,
    [id_confirmacao_pagamento]       INT            NOT NULL,
    [nr_fonte_lista]                 VARCHAR (50)   NULL,
    [vr_total_fonte_lista]           DECIMAL (8, 2) NULL,
    [vr_total_confirmar_ir]          DECIMAL (8, 2) NULL,
    [vr_total_confirmar_issqn]       DECIMAL (8, 2) NULL,
    [vr_total_confirmar]             DECIMAL (8, 2) NULL,
    CONSTRAINT [PK_tb_confirmacao_pagamento_totais] PRIMARY KEY CLUSTERED ([id_confirmacao_pagamento_total] ASC, [id_confirmacao_pagamento] ASC),
    CONSTRAINT [FK_tb_confirmacao_pagamento_totais_tb_confirmacao_pagamento] FOREIGN KEY ([id_confirmacao_pagamento]) REFERENCES [pagamento].[tb_confirmacao_pagamento] ([id_confirmacao_pagamento])
);

