-- ===================================================================    
-- Author:		Luis Fernando
-- Create date: 27/07/2017
-- Description: Procedure para salvar ou alterar bodigo de barras taxas
-- ===================================================================  
CREATE procedure [dbo].[PR_CODIGO_DE_BARRAS_TAXAS_SALVAR] 
			@id_codigo_de_barras_taxas int = null
		   ,@id_codigo_de_barras int = null
		   ,@nr_conta1 varchar(15) = null
		   ,@nr_conta2 varchar(15) = null
           ,@nr_conta3 varchar(15) = null
           ,@nr_conta4 varchar(15) = null
as
begin

	set nocount on;

	if exists (
		select	1 
		from	[contaunica].[tb_codigo_de_barras_taxas](nolock)
		where	id_codigo_de_barras_taxas = @id_codigo_de_barras_taxas
	)
	begin

		update [contaunica].[tb_codigo_de_barras_taxas] set 
			id_codigo_de_barras = @id_codigo_de_barras
			,nr_conta1 = @nr_conta1
		   ,nr_conta2 = @nr_conta2
           ,nr_conta3 = @nr_conta3
           ,nr_conta4 = @nr_conta4

		where	id_codigo_de_barras_taxas = @id_codigo_de_barras_taxas;

		select @id_codigo_de_barras_taxas;

	end
	else
	begin

		insert into [contaunica].[tb_codigo_de_barras_taxas]
           (
		   id_codigo_de_barras
		   ,nr_conta1
           ,nr_conta2
           ,nr_conta3
           ,nr_conta4)
     VALUES
           (
		   @id_codigo_de_barras
		   ,@nr_conta1
           ,@nr_conta2
           ,@nr_conta3
           ,@nr_conta4);

		select scope_identity();

	end

end