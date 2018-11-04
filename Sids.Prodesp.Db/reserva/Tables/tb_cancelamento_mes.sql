CREATE TABLE [reserva].[tb_cancelamento_mes] (
    [id_cancelamento_mes] INT          IDENTITY (1, 1) NOT NULL,
    [id_cancelamento]     INT          NOT NULL,
    [ds_mes]              VARCHAR (9)  NULL,
    [vr_mes]              DECIMAL (20) NULL,
    CONSTRAINT [PK_tb_cancelamento_mes] PRIMARY KEY CLUSTERED ([id_cancelamento_mes] ASC)
);



