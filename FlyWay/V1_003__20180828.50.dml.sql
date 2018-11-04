ALTER TABLE [seguranca].[tb_menu_item] DROP CONSTRAINT [FK_tb_menu_item_tb_menu]
ALTER TABLE [seguranca].[tb_menu_item] DROP CONSTRAINT [FK_tb_menu_item_tb_recurso]
ALTER TABLE [seguranca].[tb_recurso_acao] DROP CONSTRAINT [FK_tb_recurso_acao_tb_acao]
ALTER TABLE [seguranca].[tb_recurso_acao] DROP CONSTRAINT [FK_tb_recurso_acao_tb_recurso]

UPDATE [seguranca].[tb_menu_item] SET [nr_ordem]=1 WHERE [id_menu_item]=45
SET IDENTITY_INSERT [pagamento].[tb_confirmacao_pagamento_tipo] ON
INSERT INTO [pagamento].[tb_confirmacao_pagamento_tipo] ([id_confirmacao_tipo], [ds_confirmacao_tipo]) VALUES (1, N'Confirmação de Pagamento por Documento')
INSERT INTO [pagamento].[tb_confirmacao_pagamento_tipo] ([id_confirmacao_tipo], [ds_confirmacao_tipo]) VALUES (2, N'Confirmação de Pagamento por Lote')
SET IDENTITY_INSERT [pagamento].[tb_confirmacao_pagamento_tipo] OFF

SET IDENTITY_INSERT [seguranca].[tb_recurso_acao] ON
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (210, 48, 1)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (211, 48, 2)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (212, 48, 3)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (213, 48, 4)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (214, 48, 5)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (215, 48, 6)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (216, 48, 7)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (217, 48, 8)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (218, 49, 1)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (219, 49, 2)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (220, 49, 3)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (221, 49, 4)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (222, 49, 5)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (223, 49, 6)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (224, 49, 7)
INSERT INTO [seguranca].[tb_recurso_acao] ([id_recurso_acao], [id_recurso], [id_acao]) VALUES (225, 49, 8)
SET IDENTITY_INSERT [seguranca].[tb_recurso_acao] OFF

SET IDENTITY_INSERT [seguranca].[tb_recurso] ON
INSERT INTO [seguranca].[tb_recurso] ([id_recurso], [no_recurso], [ds_recurso], [ds_keywords], [ds_url], [bl_publico], [bl_ativo], [dt_criacao], [id_menu_url]) VALUES (48, 'Preparação de Arquivo Remessa ', 'Pesquisa de Preparação de Arquivo Remessa', NULL, 'Listar Preparação de Arquivo Remessa', 1, 1, '20180618 10:50:13.340', 57)
INSERT INTO [seguranca].[tb_recurso] ([id_recurso], [no_recurso], [ds_recurso], [ds_keywords], [ds_url], [bl_publico], [bl_ativo], [dt_criacao], [id_menu_url]) VALUES (49, 'Movimentação Orçamentária', 'Pesquisa de Movimentação Orçamentária', NULL, 'Listar Movimentação Orçamentaria', 1, 1, '20180719 14:28:11.680', 58)
SET IDENTITY_INSERT [seguranca].[tb_recurso] OFF

SET IDENTITY_INSERT [seguranca].[tb_menu_item] ON
INSERT INTO [seguranca].[tb_menu_item] ([id_menu_item], [id_menu], [id_recurso], [ds_rotulo], [ds_abrir_em], [nr_ordem], [bl_ativo], [dt_criacao]) VALUES (52, 9, 48, 'Preparação de Arquivo Remessa ', NULL, 2, 1, '20180618 10:51:36.707')
INSERT INTO [seguranca].[tb_menu_item] ([id_menu_item], [id_menu], [id_recurso], [ds_rotulo], [ds_abrir_em], [nr_ordem], [bl_ativo], [dt_criacao]) VALUES (53, 10, 49, 'Movimentação Orçamentária', NULL, 1, 1, '20180719 14:30:08.807')
SET IDENTITY_INSERT [seguranca].[tb_menu_item] OFF

SET IDENTITY_INSERT [seguranca].[tb_menu] ON
INSERT INTO [seguranca].[tb_menu] ([id_menu], [id_recurso], [ds_menu], [nr_ordem], [bl_ativo], [dt_criacao]) VALUES (10, NULL, 'Movimentação', 8, 1, '20180719 14:19:32.370')
SET IDENTITY_INSERT [seguranca].[tb_menu] OFF

SET IDENTITY_INSERT [seguranca].[tb_menu_url] ON
INSERT INTO [seguranca].[tb_menu_url] ([id_menu_url], [ds_area], [ds_controller], [ds_action], [ds_url]) VALUES (56, 'PagamentoContaUnica', 'AutorizacaoDeOB', 'Create', 'Cadastrar Autorização de OB')
INSERT INTO [seguranca].[tb_menu_url] ([id_menu_url], [ds_area], [ds_controller], [ds_action], [ds_url]) VALUES (57, 'PagamentoContaDer', 'ArquivoRemessa', 'Index', 'Listar Preparação de Arquivo Remessa')
INSERT INTO [seguranca].[tb_menu_url] ([id_menu_url], [ds_area], [ds_controller], [ds_action], [ds_url]) VALUES (58, 'Movimentacao', 'Movimentacao', 'Index', 'Listar Movimentação Orçamentaria')
SET IDENTITY_INSERT [seguranca].[tb_menu_url] OFF

ALTER TABLE [seguranca].[tb_menu_item]
    WITH NOCHECK ADD CONSTRAINT [FK_tb_menu_item_tb_menu] FOREIGN KEY ([id_menu]) REFERENCES [seguranca].[tb_menu] ([id_menu])
ALTER TABLE [seguranca].[tb_menu_item]
    WITH NOCHECK ADD CONSTRAINT [FK_tb_menu_item_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
ALTER TABLE [seguranca].[tb_recurso_acao]
    ADD CONSTRAINT [FK_tb_recurso_acao_tb_acao] FOREIGN KEY ([id_acao]) REFERENCES [seguranca].[tb_acao] ([id_acao])
ALTER TABLE [seguranca].[tb_recurso_acao]
    WITH NOCHECK ADD CONSTRAINT [FK_tb_recurso_acao_tb_recurso] FOREIGN KEY ([id_recurso]) REFERENCES [seguranca].[tb_recurso] ([id_recurso])
