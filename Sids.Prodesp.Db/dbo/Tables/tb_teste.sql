CREATE TABLE [dbo].[tb_teste] (
    [id]      INT            NOT NULL,
    [nome]    VARCHAR (50)   NULL,
    [email]   VARCHAR (100)  NULL,
    [contato] VARCHAR (500)  NULL,
    [valor]   DECIMAL (8, 2) NULL,
    CONSTRAINT [PK_tb_teste] PRIMARY KEY CLUSTERED ([id] ASC)
);

