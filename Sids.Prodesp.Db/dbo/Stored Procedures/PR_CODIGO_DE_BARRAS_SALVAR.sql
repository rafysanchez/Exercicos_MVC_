-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para salvar ou alterar bodigo de barras
-- ===================================================================  
CREATE procedure [dbo].[PR_CODIGO_DE_BARRAS_SALVAR] 
			@id_codigo_de_barras int = null
		   ,@id_tipo_de_boleto int = null
           ,@id_lista_de_boletos int = null
           ,@vr_boleto decimal(18,2) = null
		   ,@bl_transmitido_siafem bit = null
as
begin

	--SELECT @id_codigo_de_barras
	--	RETURN

	set nocount on;



	if exists (
		select	1 
		from	[contaunica].[tb_codigo_de_barras](nolock)
		where	id_codigo_de_barras = @id_codigo_de_barras
	)
	begin

		update [contaunica].[tb_codigo_de_barras] set 
			id_tipo_de_boleto	=	nullif(@id_tipo_de_boleto,0)
			,id_lista_de_boletos	=	nullif(@id_lista_de_boletos,0)
			,vr_boleto = @vr_boleto
			,bl_transmitido_siafem = @bl_transmitido_siafem

		where	id_codigo_de_barras = @id_codigo_de_barras;

		select @id_codigo_de_barras;

	end
	else
	begin

		Select	@id_codigo_de_barras = ISNULL(MAX(id_codigo_de_barras), 0) + 1
		From	[contaunica].[tb_codigo_de_barras]

		--SELECT @id_codigo_de_barras
		--RETURN

		insert into [contaunica].[tb_codigo_de_barras]
           ([id_codigo_de_barras]
		   ,[id_tipo_de_boleto]
           ,[id_lista_de_boletos]
           ,[vr_boleto]
		   ,bl_transmitido_siafem)
     VALUES
		    (@id_codigo_de_barras
           ,nullif(@id_tipo_de_boleto,0)
           ,nullif(@id_lista_de_boletos,0)
           ,@vr_boleto
		   ,@bl_transmitido_siafem
		   );

		--select scope_identity();
		select @id_codigo_de_barras

	end

end