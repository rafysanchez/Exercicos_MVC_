CREATE TABLE [reserva].[tb_reserva_mes] (
    [id_reserva_mes] INT          IDENTITY (1, 1) NOT NULL,
    [id_reserva]     INT          NOT NULL,
    [ds_mes]         VARCHAR (9)  NULL,
    [vr_mes]         DECIMAL (20) NULL,
    CONSTRAINT [PK_tb_reserva_mes] PRIMARY KEY CLUSTERED ([id_reserva_mes] ASC),
    CONSTRAINT [FK_tb_reserva_mes_tb_reserva] FOREIGN KEY ([id_reserva]) REFERENCES [reserva].[tb_reserva] ([id_reserva])
);




GO
CREATE NONCLUSTERED INDEX [tb_reserva_mês_FKIndex1]
    ON [reserva].[tb_reserva_mes]([id_reserva] ASC);

