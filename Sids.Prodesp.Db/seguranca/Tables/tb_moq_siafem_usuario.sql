CREATE TABLE [seguranca].[tb_moq_siafem_usuario] (
    [id_usuario]        INT           IDENTITY (1, 1) NOT NULL,
    [ds_login]          VARCHAR (11)  NULL,
    [ds_senha]          VARCHAR (200) NULL,
    [bl_senha_expirada] BIT           NULL
);

