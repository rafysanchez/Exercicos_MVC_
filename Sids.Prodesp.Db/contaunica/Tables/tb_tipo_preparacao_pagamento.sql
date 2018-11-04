CREATE TABLE [contaunica].[tb_tipo_preparacao_pagamento] (
    [id_tipo_preparacao_pagamento] INT           IDENTITY (1, 1) NOT NULL,
    [ds_tipo_preparacao_pagamento] VARCHAR (100) NULL,
    CONSTRAINT [PK_tb_tipo_preparacao_pagamento] PRIMARY KEY CLUSTERED ([id_tipo_preparacao_pagamento] ASC)
);



