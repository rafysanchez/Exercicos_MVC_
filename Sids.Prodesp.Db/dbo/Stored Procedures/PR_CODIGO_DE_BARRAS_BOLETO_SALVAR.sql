-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para salvar ou alterar bodigo de barras TAXAS
-- ===================================================================  
CREATE procedure [dbo].[PR_CODIGO_DE_BARRAS_BOLETO_SALVAR] 
			@id_codigo_de_barras_boleto int = null
			,@id_codigo_de_barras int  = null
		   ,@nr_conta_cob1 varchar(10) = null
           ,@nr_conta_cob2 varchar(10) = null
           ,@nr_conta_cob3 varchar(10) = null
           ,@nr_conta_cob4 varchar(10) = null
           ,@nr_conta_cob5 varchar(10) = null
           ,@nr_conta_cob6 varchar(10) = null
           ,@nr_digito char(1) = null
           ,@nr_conta_cob7 varchar(20) = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_codigo_de_barras_boleto](nolock)
		where	id_codigo_de_barras_boleto = @id_codigo_de_barras_boleto
	)
	begin

		update [contaunica].[tb_codigo_de_barras_boleto] set 
		id_codigo_de_barras = @id_codigo_de_barras
		   ,nr_conta_cob1 = @nr_conta_cob1 
           ,nr_conta_cob2 = @nr_conta_cob2
           ,nr_conta_cob3 = @nr_conta_cob3
           ,nr_conta_cob4 = @nr_conta_cob4
           ,nr_conta_cob5 = @nr_conta_cob5
           ,nr_conta_cob6 = @nr_conta_cob6
           ,nr_digito = @nr_digito
           ,nr_conta_cob7 = @nr_conta_cob7

		where	id_codigo_de_barras_boleto = @id_codigo_de_barras_boleto;

		select @id_codigo_de_barras_boleto;

	end
	else
	begin

		insert into [contaunica].[tb_codigo_de_barras_boleto]
           (id_codigo_de_barras
		   ,nr_conta_cob1
           ,nr_conta_cob2
           ,nr_conta_cob3
           ,nr_conta_cob4
		   ,nr_conta_cob5
		   ,nr_conta_cob6
		   ,nr_digito
		   ,nr_conta_cob7)
     VALUES
           (@id_codigo_de_barras
		   ,@nr_conta_cob1
           ,@nr_conta_cob2
           ,@nr_conta_cob3
           ,@nr_conta_cob4
		   ,@nr_conta_cob5
		   ,@nr_conta_cob6
		   ,@nr_digito
		   ,@nr_conta_cob7);

		select scope_identity();

	end

end