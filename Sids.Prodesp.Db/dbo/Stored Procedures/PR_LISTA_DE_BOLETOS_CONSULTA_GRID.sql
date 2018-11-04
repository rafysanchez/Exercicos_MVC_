-- ===================================================================  
-- Author:		Carlos Henrique
-- Create date: 28/07/2017
-- Description: Procedure para consultar Lista Boleto grid
-- =================================================================== 
CREATE PROCEDURE [dbo].[PR_LISTA_DE_BOLETOS_CONSULTA_GRID]
	 @id_lista_de_boletos int = NULL
	,@nr_siafem_siafisico VARCHAR(11) = NULL	
	,@cd_gestao VARCHAR(5) = NULL
	,@dt_emissao date = NULL
	,@ds_nome_da_lista VARCHAR(50)= NULL
	,@nr_cnpj_favorecido VARCHAR(14) = NULL
	,@id_tipo_documento INT = NULL
	,@nr_documento VARCHAR(19) = NULL
	,@cd_barras VARCHAR(55) = NULL
	,@cd_transmissao_status_siafem_siafisico CHAR(1) = NULL
	,@dt_cadastramento_de date = NULL
	,@dt_cadastramento_ate date = NULL
	,@id_regional smallint = null
	,@id_tipo_de_boleto int = null

as  
begin  
  
 SET NOCOUNT ON;  
	/*
	SELECT 
	   a.[id_lista_de_boletos]
	  ,a.[nr_siafem_siafisico]
      ,a.[nr_cnpj_favorecido]
      ,a.[nr_total_de_credores]
	  ,a.[vl_total_da_lista]
	  ,a.[dt_emissao]
      ,a.[cd_transmissao_status_siafem_siafisico]
	  ,a.[bl_cadastro_completo]
	  ,a.[fl_transmissao_transmitido_siafem_siafisico]
	  ,a.[fl_sistema_siafem_siafisico]
	  ,a.[dt_cadastro]
	  ,a.[ds_transmissao_mensagem_siafem_siafisico]
      
  FROM [contaunica].[tb_lista_de_boletos] a (nolock)
	where
		( nullif( @id_lista_de_boletos, 0 ) is null or a.id_lista_de_boletos = @id_lista_de_boletos )
        and (@nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico )
		and ( @cd_gestao is null or cd_gestao = @cd_gestao ) 
		and ( @dt_emissao is null or dt_emissao = @dt_emissao ) 
		and ( @ds_nome_da_lista is null or ds_nome_da_lista like '%'+@ds_nome_da_lista+'%' ) 
		and ( @nr_cnpj_favorecido is null or nr_cnpj_favorecido = @nr_cnpj_favorecido ) 
		and( nullif( @id_tipo_documento, 0 ) is null or id_tipo_documento = @id_tipo_documento )
		and ( @nr_documento is null or nr_documento = @nr_documento) 
		and ( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico)
		and ( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de ) 
		and ( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate ) 
		And ( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )
		and (
			(@cd_barras is null or a.id_lista_de_boletos in (select id_lista_de_boletos from [contaunica].[tb_codigo_de_barras] b
															where id_codigo_de_barras in (
																						select id_codigo_de_barras from  [contaunica].[tb_codigo_de_barras_boleto] c
																						where c.nr_conta_cob1 + c.nr_conta_cob2 + c.nr_conta_cob3 + c.nr_conta_cob4 + c.nr_conta_cob5 + 
																							c.nr_conta_cob6 + c.nr_digito + c.nr_conta_cob7 like '%'+@cd_barras+'%'
																							)))
		 or (@cd_barras is null or a.id_lista_de_boletos in (select id_lista_de_boletos from [contaunica].[tb_codigo_de_barras] b
															where id_codigo_de_barras in (
																						select id_codigo_de_barras from  [contaunica].[tb_codigo_de_barras_taxas] d
																						where d.nr_conta1 + d.nr_conta2 + d.nr_conta3 + d.nr_conta4 like '%'+@cd_barras+'%')))
																	)
	Order by id_lista_de_boletos
	*/

	SELECT DISTINCT a.[id_lista_de_boletos],a.[nr_siafem_siafisico],a.[nr_cnpj_favorecido],a.[nr_total_de_credores],a.[vl_total_da_lista],a.[dt_emissao],a.[cd_transmissao_status_siafem_siafisico],a.[bl_cadastro_completo],a.[fl_transmissao_transmitido_siafem_siafisico],a.[fl_sistema_siafem_siafisico],a.[dt_cadastro],a.[ds_transmissao_mensagem_siafem_siafisico]
				--,b.id_tipo_de_boleto--, c.id_codigo_de_barras, c.id_codigo_de_barras_boleto
	FROM		[contaunica].[tb_lista_de_boletos] a (nolock)
	INNER JOIN	[contaunica].[tb_codigo_de_barras] b (nolock) ON b.id_lista_de_boletos = a.id_lista_de_boletos
	LEFT JOIN	[contaunica].[tb_codigo_de_barras_boleto] c (nolock) ON c.id_codigo_de_barras = b.id_codigo_de_barras
	LEFT JOIN	[contaunica].[tb_codigo_de_barras_taxas] d (nolock) ON d.id_codigo_de_barras = b.id_codigo_de_barras
	WHERE		( nullif(@id_tipo_de_boleto, 0) IS NULL OR b.id_tipo_de_boleto = @id_tipo_de_boleto)
	AND			(
					(@cd_barras IS NULL OR c.nr_conta_cob1 + c.nr_conta_cob2 + c.nr_conta_cob3 + c.nr_conta_cob4 + c.nr_conta_cob5 + c.nr_conta_cob6 + c.nr_digito + c.nr_conta_cob7 like '%'+@cd_barras+'%')
	OR			
				
					(@cd_barras IS NULL OR d.nr_conta1 + d.nr_conta2 + d.nr_conta3 + d.nr_conta4 like '%'+@cd_barras+'%')
				)
	
	AND			( nullif( @id_lista_de_boletos, 0 ) is null or a.id_lista_de_boletos = @id_lista_de_boletos )
    AND			(@nr_siafem_siafisico is null or nr_siafem_siafisico = @nr_siafem_siafisico )
	AND			( @cd_gestao is null or cd_gestao = @cd_gestao ) 
	AND			( @dt_emissao is null or dt_emissao = @dt_emissao ) 
	AND			( @ds_nome_da_lista is null or ds_nome_da_lista like '%'+@ds_nome_da_lista+'%' ) 
	AND			( @nr_cnpj_favorecido is null or nr_cnpj_favorecido = @nr_cnpj_favorecido ) 
	AND			( nullif( @id_tipo_documento, 0) is null or id_tipo_documento = @id_tipo_documento )
	AND			( @nr_documento is null or nr_documento = @nr_documento) 
	AND			( @cd_transmissao_status_siafem_siafisico is null or cd_transmissao_status_siafem_siafisico = @cd_transmissao_status_siafem_siafisico)
	AND			( @dt_cadastramento_de is null or [dt_cadastro] >= @dt_cadastramento_de ) 
	AND			( @dt_cadastramento_ate is null or [dt_cadastro] <= @dt_cadastramento_ate ) 
	AND			( nullif( @id_regional, 0 ) is null or [id_regional] = @id_regional )

	ORDER BY	a.id_lista_de_boletos

end;