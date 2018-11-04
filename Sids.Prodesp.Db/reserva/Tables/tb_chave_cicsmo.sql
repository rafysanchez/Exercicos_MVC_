CREATE TABLE [reserva].[tb_chave_cicsmo] (
    [id_chave]      INT           IDENTITY (1, 1) NOT NULL,
    [ds_chave]      VARCHAR (50)  NULL,
    [ds_senha]      VARCHAR (100) NULL,
    [bl_disponivel] BIT           NULL,
    [nr_ranking]    INT           NULL,
    CONSTRAINT [PK_tb_chave_cicsmo] PRIMARY KEY CLUSTERED ([id_chave] ASC)
);



