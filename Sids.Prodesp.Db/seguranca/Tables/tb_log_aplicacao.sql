CREATE TABLE [seguranca].[tb_log_aplicacao] (
    [id_log_aplicacao] INT            IDENTITY (1, 1) NOT NULL,
    [id_resultado]     SMALLINT       NULL,
    [id_navegador]     SMALLINT       NULL,
    [id_acao]          SMALLINT       NULL,
    [id_recurso]       INT            NULL,
    [id_usuario]       INT            NULL,
    [dt_log]           DATETIME       NULL,
    [ds_ip]            VARCHAR (15)   NULL,
    [ds_url]           VARCHAR (200)  NULL,
    [ds_argumento]     VARCHAR (8000) NULL,
    [ds_versao]        VARCHAR (20)   NULL,
    [ds_log]           TEXT           NULL,
    [ds_navegador]     VARCHAR (50)   NULL,
    [ds_terminal]      VARCHAR (50)   NULL,
    [ds_xml]           TEXT           NULL,
    CONSTRAINT [PK_tb_log_aplicacao] PRIMARY KEY CLUSTERED ([id_log_aplicacao] ASC),
    CONSTRAINT [FK_tb_log_aplicacao_tb_acao] FOREIGN KEY ([id_acao]) REFERENCES [seguranca].[tb_acao] ([id_acao]),
    CONSTRAINT [FK_tb_log_aplicacao_tb_navegador] FOREIGN KEY ([id_navegador]) REFERENCES [seguranca].[tb_navegador] ([id_navegador]),
    CONSTRAINT [FK_tb_log_aplicacao_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
);




GO
CREATE NONCLUSTERED INDEX [ix_tb_log_aplicacao_id_usuario]
    ON [seguranca].[tb_log_aplicacao]([id_usuario] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_log_aplicacao_id_recurso]
    ON [seguranca].[tb_log_aplicacao]([id_recurso] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_log_aplicacao_id_acao]
    ON [seguranca].[tb_log_aplicacao]([id_acao] ASC);


GO
CREATE NONCLUSTERED INDEX [ix_tb_log_aplicacao_dt_log]
    ON [seguranca].[tb_log_aplicacao]([dt_log] ASC);

