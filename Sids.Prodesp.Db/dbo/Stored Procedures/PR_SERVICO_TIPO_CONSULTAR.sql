CREATE procedure [dbo].[PR_SERVICO_TIPO_CONSULTAR] (
	@id_servico_tipo int = null
)
as
begin 

	select	id_servico_tipo
		,	ds_servico_tipo
		,	cd_rap_tipo
	from pagamento.tb_servico_tipo
	where	( @id_servico_tipo = 0 or id_servico_tipo = @id_servico_tipo)

end