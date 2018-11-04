ALTER TABLE [seguranca].[tb_menu_item] DROP CONSTRAINT [FK_tb_menu_item_tb_menu]
ALTER TABLE [seguranca].[tb_menu_item] DROP CONSTRAINT [FK_tb_menu_item_tb_recurso]
ALTER TABLE [seguranca].[tb_perfil_recurso] DROP CONSTRAINT [FK_tb_perfil_recurso_tb_perfil]
ALTER TABLE [seguranca].[tb_perfil_recurso] DROP CONSTRAINT [FK_tb_perfil_recurso_tb_recurso]
ALTER TABLE [seguranca].[tb_recurso_acao] DROP CONSTRAINT [FK_tb_recurso_acao_tb_acao]
ALTER TABLE [seguranca].[tb_recurso_acao] DROP CONSTRAINT [FK_tb_recurso_acao_tb_recurso]


DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=1
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=2
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=3
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=4
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=5
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=6
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=7
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=8
DELETE FROM [pagamento].[tb_despesa_tipo] WHERE [id_despesa_tipo]=9


UPDATE [seguranca].[tb_menu_item] SET [ds_rotulo]='Preparação de Arquivo Remessa' WHERE [id_menu_item]=52
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=10, [ds_despesa_tipo]='SOMA DE PESSOAL E REFLEXOS' WHERE [id_despesa_tipo]=10
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=11, [ds_despesa_tipo]='FOLHA DE PAGAMENTO' WHERE [id_despesa_tipo]=11
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=12, [ds_despesa_tipo]='AUXILIO FUNERAL' WHERE [id_despesa_tipo]=12
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=13, [ds_despesa_tipo]='CONSIGNACOES' WHERE [id_despesa_tipo]=13
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=14, [ds_despesa_tipo]='REQUISITORIOS DE PESSOAL' WHERE [id_despesa_tipo]=14
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=15, [ds_despesa_tipo]='ENCARGOS SOCIAIS' WHERE [id_despesa_tipo]=15
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=16, [ds_despesa_tipo]='INDENIZACOES FERIAS' WHERE [id_despesa_tipo]=16
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=17, [ds_despesa_tipo]='LEILAO DE VEICULOS' WHERE [id_despesa_tipo]=17
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=18, [ds_despesa_tipo]='REMUNERACAO DE JETONS' WHERE [id_despesa_tipo]=18
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=19, [ds_despesa_tipo]='DIARIAS PAGAS PELA UNIDADE' WHERE [id_despesa_tipo]=19
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=20, [ds_despesa_tipo]='SOMA DE CONTRATOS E SERVICOS' WHERE [id_despesa_tipo]=20
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=21, [ds_despesa_tipo]='EQUIPES CONTRATADAS' WHERE [id_despesa_tipo]=21
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=22, [ds_despesa_tipo]='LOCACAO EM GERAL' WHERE [id_despesa_tipo]=22
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=23, [ds_despesa_tipo]='DEVOLUCAO DE CAUCAO' WHERE [id_despesa_tipo]=23
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=24, [ds_despesa_tipo]='FISCALIZACAO' WHERE [id_despesa_tipo]=24
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=25, [ds_despesa_tipo]='REMOCAO E GUARDA DE VEICULOS NOS PATEOS' WHERE [id_despesa_tipo]=25
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=26, [ds_despesa_tipo]='TRABALHOS PERICIAIS' WHERE [id_despesa_tipo]=26
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=27, [ds_despesa_tipo]='OUTROS CONTRATOS E SERVICOS' WHERE [id_despesa_tipo]=27
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=28, [ds_despesa_tipo]='SERVICOS DE CUSTEIO - BIRD' WHERE [id_despesa_tipo]=28
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=29, [ds_despesa_tipo]='POSTO FISCALIZACAO FRONTEIRA' WHERE [id_despesa_tipo]=29
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=30, [ds_despesa_tipo]='SOMA DE EMPREITEIROS' WHERE [id_despesa_tipo]=30
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=31, [ds_despesa_tipo]='TREVOS E 3A. FAIXAS' WHERE [id_despesa_tipo]=31
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=32, [ds_despesa_tipo]='PROGR.TRANSP.LOG./MEIO AMB.PROJ2476-CAF' WHERE [id_despesa_tipo]=32
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=33, [ds_despesa_tipo]='PROGR.TRANSP.LOG./MEIO AMB.PROJ2392-BIRD' WHERE [id_despesa_tipo]=33
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=34, [ds_despesa_tipo]='RESTAURACAO E MELHORIA DE RODOVIAS' WHERE [id_despesa_tipo]=34
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=35, [ds_despesa_tipo]='DUPLICACAO DE RODOVIAS' WHERE [id_despesa_tipo]=35
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=36, [ds_despesa_tipo]='CONSERVACAO/MANUTENCAO/SINALIZACAO' WHERE [id_despesa_tipo]=36
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=37, [ds_despesa_tipo]='PROGR.DE LOGIST.E TRANSP.PROJ2478-B.B' WHERE [id_despesa_tipo]=37
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=38, [ds_despesa_tipo]='RODOVIAS VICINAIS' WHERE [id_despesa_tipo]=38
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=39, [ds_despesa_tipo]='JUROS/CORRECAO MONETARIA' WHERE [id_despesa_tipo]=39
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=40, [ds_despesa_tipo]='SOMA DE FORNECEDORES' WHERE [id_despesa_tipo]=40
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=41, [ds_despesa_tipo]='CONTRATOS DE FORNECIMENTOS' WHERE [id_despesa_tipo]=41
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=42, [ds_despesa_tipo]='AQUISICAO DE MATERIAIS - BEC E PREGAO' WHERE [id_despesa_tipo]=42
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=43, [ds_despesa_tipo]='PEQUENOS FORNECIMENTOS' WHERE [id_despesa_tipo]=43
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=44, [ds_despesa_tipo]='FORNECIMENTO P/CONVENIO-PETROBRAS' WHERE [id_despesa_tipo]=44
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=45, [ds_despesa_tipo]='FORNECIMENTO DE EMULSAO ASFALTICA' WHERE [id_despesa_tipo]=45
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=46, [ds_despesa_tipo]='OPERACAO E CONTROLE DE RODOVIAS' WHERE [id_despesa_tipo]=46
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=47, [ds_despesa_tipo]='RECUPER.DE ESTRADAS MUNICIPAIS-BIRD II' WHERE [id_despesa_tipo]=47
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=48, [ds_despesa_tipo]='RESTITUICAO DE MULTAS' WHERE [id_despesa_tipo]=48
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=49, [ds_despesa_tipo]='CORRECAO MONETARIA FORNECEDORES' WHERE [id_despesa_tipo]=49
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=50, [ds_despesa_tipo]='SOMA DE DESPESAS DE EMPRESTIMOS' WHERE [id_despesa_tipo]=50
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=51, [ds_despesa_tipo]='VICINAIS III/BIRD - NAO ELEGIVEIS' WHERE [id_despesa_tipo]=51
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=52, [ds_despesa_tipo]='PROGR.EXEC.OBRAS RODO-NORTE-BID-PRN-BID' WHERE [id_despesa_tipo]=52
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=53, [ds_despesa_tipo]='VIC.IV/BIRDII-04/2015-BID-FASEII 2510' WHERE [id_despesa_tipo]=53
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=54, [ds_despesa_tipo]='COMPLEXO VIARIO JACU-PESSEGO' WHERE [id_despesa_tipo]=54
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=55, [ds_despesa_tipo]='MARGINAIS TIETE E PINHEIROS' WHERE [id_despesa_tipo]=55
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=56, [ds_despesa_tipo]='ECO TURISMO/TAMOIOS/TRAVESSIA SECA' WHERE [id_despesa_tipo]=56
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=57, [ds_despesa_tipo]='RODOANEL NORTE/SUL E LESTE' WHERE [id_despesa_tipo]=57
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=58, [ds_despesa_tipo]='DESENV.SIST.VIARIO METROP.SP/POLO ITAQUE' WHERE [id_despesa_tipo]=58
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=59, [ds_despesa_tipo]='VICINAIS III/BIRD - ELEGIVEIS' WHERE [id_despesa_tipo]=59
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=60, [ds_despesa_tipo]='SOMA DE ENTIDADES ESTADUAIS' WHERE [id_despesa_tipo]=60
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=61, [ds_despesa_tipo]='CONTRATOS' WHERE [id_despesa_tipo]=61
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=62, [ds_despesa_tipo]='VICII/IV-BID/BIRD-04/2015-BID FASEI 2477' WHERE [id_despesa_tipo]=62
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=63, [ds_despesa_tipo]='OUTROS' WHERE [id_despesa_tipo]=63
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=64, [ds_despesa_tipo]='REMUNERACAO DE ESTAGIARIOS' WHERE [id_despesa_tipo]=64
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=65, [ds_despesa_tipo]='VICINAIS I-FONTE 001.012.787' WHERE [id_despesa_tipo]=65
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=66, [ds_despesa_tipo]='RECUP.ROD.C/FINANC.DO BID-BID II' WHERE [id_despesa_tipo]=66
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=67, [ds_despesa_tipo]='FERNAO DIAS-2A.ETAPA' WHERE [id_despesa_tipo]=67
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=68, [ds_despesa_tipo]='RECUPER.RODOVIAS C/FINANCIAMENTO DO BID' WHERE [id_despesa_tipo]=68
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=69, [ds_despesa_tipo]='RESSARCIMENTO COMISSIONADOS' WHERE [id_despesa_tipo]=69
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=70, [ds_despesa_tipo]='SOMA DE OUTROS DESEMBOLSOS' WHERE [id_despesa_tipo]=70
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=71, [ds_despesa_tipo]='UTILIDADE PUBLICA' WHERE [id_despesa_tipo]=71
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=72, [ds_despesa_tipo]='COMBUSTIVEIS E LUBRIFICANTES' WHERE [id_despesa_tipo]=72
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=73, [ds_despesa_tipo]='DESAPROPRIACAO' WHERE [id_despesa_tipo]=73
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=74, [ds_despesa_tipo]='ADIANTAMENTOS' WHERE [id_despesa_tipo]=74
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=75, [ds_despesa_tipo]='DESPESAS EM GERAL' WHERE [id_despesa_tipo]=75
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=76, [ds_despesa_tipo]='PREFEITURAS (ARE E AUX.ESPECIAL)' WHERE [id_despesa_tipo]=76
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=77, [ds_despesa_tipo]='PREF.MUN.(RECURSOS VINCULADOS)' WHERE [id_despesa_tipo]=77
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=78, [ds_despesa_tipo]='SUPRIMENTOS' WHERE [id_despesa_tipo]=78
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=79, [ds_despesa_tipo]='DEVOLUCAO RETENCOES INDEVIDAS' WHERE [id_despesa_tipo]=79
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=80, [ds_despesa_tipo]='SOMA DE OUTROS CONTR.E SERVICOS' WHERE [id_despesa_tipo]=80
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=81, [ds_despesa_tipo]='SERVICOS DE VIGILANCIA' WHERE [id_despesa_tipo]=81
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=82, [ds_despesa_tipo]='SERVICOS DE LIMPEZA' WHERE [id_despesa_tipo]=82
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=83, [ds_despesa_tipo]='SERV.OPERACAO PRACA DE PEDAGIO' WHERE [id_despesa_tipo]=83
UPDATE [pagamento].[tb_despesa_tipo] SET [cd_despesa_tipo]=84, [ds_despesa_tipo]='SERVICOS DE TRANSPORTE NUMERARIO' WHERE [id_despesa_tipo]=84


UPDATE [seguranca].[tb_perfil] SET [ds_perfil]='SOF Administrador
', [ds_detalhe]='Administrador SOF
' WHERE [id_perfil]=6
UPDATE [seguranca].[tb_perfil] SET [ds_perfil]='CDF Administrador
', [ds_detalhe]='Usuário Administrador da área CDF
' WHERE [id_perfil]=9


--SET IDENTITY_INSERT [seguranca].[tb_perfil] ON
--INSERT INTO [seguranca].[tb_perfil] ([id_perfil], [ds_perfil], [ds_detalhe], [bl_ativo], [dt_criacao], [bl_administrador]) VALUES (5, 'SOF Operador', 'Operador SOF', 1, '20180618 10:52:00.000', 0)
--INSERT INTO [seguranca].[tb_perfil] ([id_perfil], [ds_perfil], [ds_detalhe], [bl_ativo], [dt_criacao], [bl_administrador]) VALUES (7, 'Prodesp
--', 'Prodesp
--', 1, '20170817 15:40:00.000', NULL)
--INSERT INTO [seguranca].[tb_perfil] ([id_perfil], [ds_perfil], [ds_detalhe], [bl_ativo], [dt_criacao], [bl_administrador]) VALUES (8, 'Acesso Prodesp', 'Perfil para acompanhamento do Projeto SIDS pela equipe Prodesp. Deverá ser desativo ao término do projeto.', 1, '20180618 10:52:00.000', 0)
--INSERT INTO [seguranca].[tb_perfil] ([id_perfil], [ds_perfil], [ds_detalhe], [bl_ativo], [dt_criacao], [bl_administrador]) VALUES (10, 'CDF Operador
--', 'Usuário Operador da área CDF
--', 1, '20161116 19:12:00.000', NULL)
--SET IDENTITY_INSERT [seguranca].[tb_perfil] OFF


SET IDENTITY_INSERT [seguranca].[tb_recurso_acao] ON
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (226, 50, 1)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (227, 50, 2)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (228, 50, 5)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (229, 50, 6)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (230, 50, 7)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (231, 50, 8)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (232, 50, 3)
SET IDENTITY_INSERT [seguranca].[tb_recurso_acao] OFF


INSERT INTO [pagamento].[tb_parametrizacao_forma_gerar_nl] ([id_parametrizacao_forma_gerar_nl], [ds_gerar_nl]) VALUES (1, 'OP')
INSERT INTO [pagamento].[tb_parametrizacao_forma_gerar_nl] ([id_parametrizacao_forma_gerar_nl], [ds_gerar_nl]) VALUES (2, 'Credor e Empenho')
INSERT INTO [pagamento].[tb_parametrizacao_forma_gerar_nl] ([id_parametrizacao_forma_gerar_nl], [ds_gerar_nl]) VALUES (3, 'Empenho')


SET IDENTITY_INSERT [seguranca].[tb_recurso] ON
INSERT INTO [seguranca].[tb_recurso] ([id_recurso], [no_recurso], [ds_recurso], [ds_keywords], [ds_url], [bl_publico], [bl_ativo], [dt_criacao], [id_menu_url]) VALUES (50, 'Impressão de Relação de RE e RT', 'Pesquisa de Impressão de Relação de RE e RT', NULL, 'Listar Impressão de Relação de RE e RT', 1, 1, '20180828 10:43:45.037', 59)
SET IDENTITY_INSERT [seguranca].[tb_recurso] OFF


INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (85, 85, 'FORNECIMENTO DE AGUA')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (86, 86, 'FORNECIMENTO DE LUZ')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (87, 87, 'TELECOMUNICACOES')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (88, 88, 'CORREIOS')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (89, 89, 'OUTROS CONTRATOS E SERVICOS')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (91, 91, 'OBRAS-SANTANDER/MIGA-UCPR-F007502072')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (96, 96, 'TAXA DE ADIMIN.A SAO PAULO (SPPREV)')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (98, 98, 'INVESTIMENTOS-F-007501075-DER/BB')
INSERT INTO [pagamento].[tb_despesa_tipo] ([id_despesa_tipo], [cd_despesa_tipo], [ds_despesa_tipo]) VALUES (99, 99, 'INV.F007571075/13-F007531079/14-BB-RESSA')


SET IDENTITY_INSERT [seguranca].[tb_menu_item] ON
INSERT INTO [seguranca].[tb_menu_item] ([id_menu_item], [id_menu], [id_recurso], [ds_rotulo], [ds_abrir_em], [nr_ordem], [bl_ativo], [dt_criacao]) VALUES (54, 7, 50, 'Lista de Impressão de Relação de RE e RT', NULL, 8, 1, '20180828 10:55:55.383')
SET IDENTITY_INSERT [seguranca].[tb_menu_item] OFF


SET IDENTITY_INSERT [seguranca].[tb_menu_url] ON
INSERT INTO [seguranca].[tb_menu_url] ([id_menu_url], [ds_area], [ds_controller], [ds_action], [ds_url]) VALUES (59, 'PagamentoContaUnica', 'ImpressaoRelacaoRERT', 'Index', 'Listar Impressão de Relação de RE e RT')
INSERT INTO [seguranca].[tb_menu_url] ([id_menu_url], [ds_area], [ds_controller], [ds_action], [ds_url]) VALUES (60, 'PagamentoContaUnica', 'ImpressaoRelacaoRERT', 'Create', 'Cadastrar Impressão de Relação de RE e RT')
SET IDENTITY_INSERT [seguranca].[tb_menu_url] OFF


SET IDENTITY_INSERT [movimentacao].[tb_tipo_documento_movimentacao] ON
INSERT INTO [movimentacao].[tb_tipo_documento_movimentacao] ([id_tipo_documento_movimentacao], [ds_tipo_documento_movimentacao]) VALUES (1, 'Cancelamento/Redução')
INSERT INTO [movimentacao].[tb_tipo_documento_movimentacao] ([id_tipo_documento_movimentacao], [ds_tipo_documento_movimentacao]) VALUES (2, 'Distribuição/Suplementação')
SET IDENTITY_INSERT [movimentacao].[tb_tipo_documento_movimentacao] OFF


SET IDENTITY_INSERT [movimentacao].[tb_tipo_movimentacao_orcamentaria] ON
INSERT INTO [movimentacao].[tb_tipo_movimentacao_orcamentaria] ([id_tipo_movimentacao_orcamentaria], [ds_tipo_movimentacao_orcamentariao]) VALUES (1, 'Transferencia (com NC na Distribuição)')
INSERT INTO [movimentacao].[tb_tipo_movimentacao_orcamentaria] ([id_tipo_movimentacao_orcamentaria], [ds_tipo_movimentacao_orcamentariao]) VALUES (2, 'Estorno (com NC no Cancelamento)')
INSERT INTO [movimentacao].[tb_tipo_movimentacao_orcamentaria] ([id_tipo_movimentacao_orcamentaria], [ds_tipo_movimentacao_orcamentariao]) VALUES (3, 'Redistribuição (sem NC)')
SET IDENTITY_INSERT [movimentacao].[tb_tipo_movimentacao_orcamentaria] OFF


ALTER TABLE [seguranca].[tb_menu_item]
    WITH NOCHECK ADD CONSTRAINT [FK_tb_menu_item_tb_menu] FOREIGN KEY ([id_menu]) REFERENCES [seguranca].[tb_menu] ([id_menu])
ALTER TABLE [seguranca].[tb_menu_item]
    WITH NOCHECK ADD CONSTRAINT [FK_tb_menu_item_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
ALTER TABLE [seguranca].[tb_perfil_recurso]
    ADD CONSTRAINT [FK_tb_perfil_recurso_tb_perfil] FOREIGN KEY ([id_perfil]) REFERENCES [seguranca].[tb_perfil] ([id_perfil])
ALTER TABLE [seguranca].[tb_perfil_recurso]
    ADD CONSTRAINT [FK_tb_perfil_recurso_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
ALTER TABLE [seguranca].[tb_recurso_acao]
    ADD CONSTRAINT [FK_tb_recurso_acao_tb_acao] FOREIGN KEY ([id_acao]) REFERENCES [seguranca].[tb_acao] ([id_acao])
ALTER TABLE [seguranca].[tb_recurso_acao]
    WITH NOCHECK ADD CONSTRAINT [FK_tb_recurso_acao_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
