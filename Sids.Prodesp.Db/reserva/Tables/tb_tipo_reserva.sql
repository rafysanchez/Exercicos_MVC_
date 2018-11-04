CREATE TABLE [reserva].[tb_tipo_reserva] (
    [id_tipo_reserva] INT          IDENTITY (1, 1) NOT NULL,
    [ds_tipo_reserva] VARCHAR (50) NULL,
    CONSTRAINT [PK_tb_tipo_reserva] PRIMARY KEY CLUSTERED ([id_tipo_reserva] ASC)
);



