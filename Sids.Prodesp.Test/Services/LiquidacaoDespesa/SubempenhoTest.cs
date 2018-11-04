using Sids.Prodesp.UI.Report;

namespace Sids.Prodesp.Test.Services.LiquidacaoDespesa
{
    using Application;
    using Core.Base;
    using Core.Services.LiquidacaoDespesa;
    using Core.Services.WebService.LiquidacaoDespesa;
    using Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Enum;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class SubempenhoTest
    {
        #region Fields

        readonly SubempenhoService _subempenhoService = App.SubempenhoService;
        readonly SubempenhoEventoService _eventoService = App.SubempenhoEventoService;
        readonly SubempenhoNotaService _notaService = App.SubempenhoNotaService;
        readonly SubempenhoItemService _itemService = App.SubempenhoItemService;

        readonly SiafemLiquidacaoDespesaService _siafemSiafisico = App.SiafemLiquidacaoDespesaService;

        static Subempenho _entity;

        #endregion

        public SubempenhoTest()
        {            
            CriarNovaEntidade();
            CreateInstance(); //only for prodesp service test
        }

        #region Repository Tests

        [TestMethod]
        public void TesteCriarUmNovoSubempenho()
        {
            Assert.IsNotNull(_entity);
        }

        [TestMethod]
        public void TesteSalvarUmNovoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsTrue(_entity.Id > 0);
        }

        [TestMethod]
        public void TesteSelecionarUmSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(_entity);
        }

        [TestMethod]
        public void TesteSelecionarOsItensDoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(_entity.Itens);
        }

        [TestMethod]
        public void TesteSelecionarOsEventosDoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(_entity.Eventos);
        }

        [TestMethod]
        public void TesteSelecionarAsNotasFiscaisDoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(_entity.Notas);
        }

        [TestMethod]
        public void TesteEditarUmSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            entity.DescricaoEspecificacaoDespesa8 = "UPDATE TEST OK";

            AlterarEntidadeNoRepositorio(entity);

            BuscarUmSubempenhoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.AreEqual("UPDATE TEST OK", _entity.DescricaoEspecificacaoDespesa8);
        }

        [TestMethod]
        public void TesteEditarUmItemDoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            var item = _entity.Itens.FirstOrDefault(f => f.CodigoItemServico.Equals("0821-4")) as SubempenhoItem;

            item.QuantidadeMaterialServico = 12M;

            AlterarUmItemDaEntidadeNoRepositorio(item);

            item = null;

            BuscarUmSubempenhoNoRepositorio(_entity);

            var selectedItem = _entity.Itens.FirstOrDefault(f => f.CodigoItemServico.Equals("0821-4"));

            RemoverEntidadeDoRepositorio(_entity);

            Assert.AreEqual(12M, selectedItem.QuantidadeMaterialServico);
        }

        [TestMethod]
        public void TesteEditarUmEventoDoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            var evento = _entity.Eventos.FirstOrDefault(f => f.NumeroEvento.Equals("510600")) as SubempenhoEvento;

            evento.ValorUnitario = 120;

            AlterarUmEventoDaEntidadeNoRepositorio(evento);

            evento = null;

            BuscarUmSubempenhoNoRepositorio(_entity);

            var selectedEvent = _entity.Eventos.FirstOrDefault(f => f.NumeroEvento.Equals("510600"));

            RemoverEntidadeDoRepositorio(_entity);

            Assert.AreEqual(120, selectedEvent.ValorUnitario);
        }

        [TestMethod]
        public void TesteEditarUmaNotaDoSubempenhoNoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmSubempenhoNoRepositorio(entity);

            var nota = _entity.Notas.FirstOrDefault(f => f.CodigoNotaFiscal.Equals("nada consta")) as SubempenhoNota;

            nota.CodigoNotaFiscal = "1620";

            AlterarUmaNotaDaEntidadeNoRepositorio(nota);

            nota = null;

            BuscarUmSubempenhoNoRepositorio(_entity);

            var selectedInvoice = _entity.Notas.FirstOrDefault(f => f.CodigoNotaFiscal.Equals("1620"));

            RemoverEntidadeDoRepositorio(_entity);

            Assert.AreEqual("1620", selectedInvoice.CodigoNotaFiscal);
        }

        [TestMethod]
        public void TesteExcluirUmSubempenhoDoRepositorio()
        {
            SalvarEntidadeNoRepositorio();

            Assert.AreEqual(AcaoEfetuada.Sucesso, RemoverEntidadeDoRepositorio(_entity));
        }

        #endregion

        #region Services Tests

        #region Siafem
        [TestMethod()]
        public void TesteInserirSubempenhoSiafemNotaDeLancamento()
        {
            CriarNovaEntidade();

            //_entity.CodigoEvento = 1;

            InserirSubempenhoSiafemNotaDeLancamento();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()]
        public void TesteInserirSubempenhoSiafemLiquidacaoDeMedicaoDeObras()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafemLiquidacaoDeMedicaoDeObras();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()]
        public void TesteInserirSubempenhoSiafemNotaDeLancamentoCtObras()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafemNotaDeLancamentoCtObras();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()]
        public void TesteConsultaNotaLancamento()
        {
            var subempenho = new Subempenho {NumeroSiafemSiafisico = "2017NL00005", CodigoUnidadeGestora = "162101", TipoEventoId =  8, TransmitirSiafem = true};

            var consultaNl =  App.SiafemLiquidacaoDespesaService.ConsultaNL(AppConfig.WsSiafemUser, AppConfig.WsPassword, "162101", subempenho.NumeroSiafemSiafisico);

            var pdf = HelperReport.GerarPdfLiquidacaoDespesa(consultaNl, "Lançamento de Apropriação / Subempenho", subempenho);
            
            System.Diagnostics.Process.Start(@"C:\Users\810235\Documents\TestePDF.pdf");

            Assert.IsNotNull(consultaNl);
        }

        #endregion

        #region Siafisico

        [TestMethod()]
        public void TesteInserirSubempenhoSiafisicoLiquidacaoDeEmpenhoTradicional()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafisicoLiquidacaoDeEmpenhoTradicional();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()]
        public void TesteInserirSubempenhoSiafisicoLiquidacaoDePregaoEletrônico()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafisicoLiquidacaoDePregaoEletrônico();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()]
        public void TesteInserirSubempenhoSiafisicoNotaDeLancamentoBec()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafisicoNotaDeLancamentoBec();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()] //serviço retorna erro de execução
        public void TesteInserirSubempenhoSiafisicoNotaLancamentoDeContrato()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafisicoNotaLancamentoDeContrato();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        [TestMethod()]
        public void TesteInserirSubempenhoSiafisicoNotaDeLancamentoDeLiquidacaoDeEmpenho()
        {
            CriarNovaEntidade();

            InserirSubempenhoSiafisicoNotaDeLancamentoDeLiquidacaoDeEmpenho();

            Assert.IsNotNull(_entity.NumeroSiafemSiafisico);
        }

        #endregion
        
        #region Prodesp
        private Subempenho _model1;
        private Subempenho _model2;
        private Subempenho _model3;
        private Subempenho _model4;

        [TestMethod()]
        public void InserirSubEmpenhoContratoReciboTest()
        {
            var result = App.ProdespLiquidacaoDespesaService.InserirSubEmpenho(_model1, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirSubEmpenhoContratoSemReciboTest()
        {
            var result = App.ProdespLiquidacaoDespesaService.InserirSubEmpenho(_model2, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirSubEmpenhoOrganizacao7Test()
        {
            var result = App.ProdespLiquidacaoDespesaService.InserirSubEmpenho(_model3, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirSubEmpenhoContratoReciboCheioTest()
        {
            var result = App.ProdespLiquidacaoDespesaService.InserirSubEmpenho(_model4, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirSubEmpenhoApoioTest()
        {
            var result = App.ProdespLiquidacaoDespesaService.InserirSubEmpenhoApoio(_model1, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        private void CreateInstance()
        {
            _model1 = CreateInstanceEmpenho("139900009", "32", "11", "139900047", 100, null, null, null, 0, "2013NE00001 N.F.000001-06/13 2013NL00001");
            _model2 = CreateInstanceEmpenho("169900049", "20", "11", null, 30311111, "004", "R", null, 0, "2016NE00001                  2016NL00001");
            _model3 = CreateInstanceEmpenho("169900047", "20", "11", null, 0, null, null, "06065569828", 0, "2013NE00001 N.F.000001-06/13 2013NL00001");
            _model4 = CreateInstanceEmpenho("169900049", "20", "11", "179900001", 4444444, null, null, null, 0, "2016NE00001 N.F.123456-09/13 2016NL00001");
        }
        private Subempenho CreateInstanceEmpenho(string empenho, string tarefa, string despesa, string recibo, int valor, string medicao, string natureza, string CFP, int org, string referencia)
        {
            Subempenho model = new Subempenho
            {
                NumeroOriginalProdesp = empenho,
                CodigoTarefa = tarefa,
                CodigoDespesa = despesa,
                ValorRealizado = 100,
                NumeroRecibo = recibo,
                PrazoPagamento = "030",
                CodigoNotaFiscalProdesp = null,
                CodigoEspecificacaoDespesa = "000",
                Referencia = referencia,
                NumeroProcesso = "DER",
                DescricaoAutorizadoSupraFolha = "test",
                CodigoAutorizadoAssinatura = "12345",
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                CodigoExaminadoAssinatura = "11111",
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                CodigoResponsavelAssinatura = "88888",
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NlRetencaoInss = null,
                Lista = null,
                NumeroMedicao = medicao,
                NumeroCNPJCPFFornecedor = CFP,
                CodigoCredorOrganizacao = org,
                DataRealizado = recibo == null ? DateTime.Now : default(DateTime),
                NaturezaSubempenhoId = natureza,
                DescricaoEspecificacaoDespesa1 = "TEste"




            };

            return model;
        }
        #endregion

        #endregion

        #region TestHelpers

        private void SalvarEntidadeNoRepositorio()
        {
            CriarNovaEntidade();
            _entity.Id = _subempenhoService.SalvarOuAlterar(_entity, 0, (short)EnumAcao.Inserir);
        }
        private void AlterarEntidadeNoRepositorio(Subempenho entity)
        {
            _subempenhoService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void AlterarUmItemDaEntidadeNoRepositorio(SubempenhoItem entity)
        {
            _itemService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void AlterarUmEventoDaEntidadeNoRepositorio(SubempenhoEvento entity)
        {
            _eventoService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void AlterarUmaNotaDaEntidadeNoRepositorio(SubempenhoNota entity)
        {
            _notaService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void BuscarUmSubempenhoNoRepositorio(Subempenho entity)
        {
            _entity = _subempenhoService.Selecionar(entity.Id);
        }
        private AcaoEfetuada RemoverEntidadeDoRepositorio(Subempenho entity)
        {
            return _subempenhoService.Excluir(entity, 0, (short)EnumAcao.Excluir);
        }


        private void CallSiafemService()
        {
            _entity.NumeroSiafemSiafisico = _siafemSiafisico
                .InserirSubempenhoSiafem(AppConfig.WsSiafemUser, AppConfig.WsPassword, "162101", _entity);
        }
        public void InserirSubempenhoSiafemNotaDeLancamento()
        {
            _entity.CenarioSiafemSiafisico = 6;

            CallSiafemService();
        }
        public void InserirSubempenhoSiafemLiquidacaoDeMedicaoDeObras()
        {
            _entity.CenarioSiafemSiafisico = 7;

            CallSiafemService();
        }
        public void InserirSubempenhoSiafemNotaDeLancamentoCtObras()
        {
            _entity.CenarioSiafemSiafisico = 8;

            CallSiafemService();
        }


        private void CallSiafisicoService()
        {
            _entity.NumeroSiafemSiafisico = _siafemSiafisico
                .InserirSubempenhoSiafisico(AppConfig.WsSiafisicoUser, AppConfig.WsPassword, "162101", _entity);
        }
        public void InserirSubempenhoSiafisicoLiquidacaoDeEmpenhoTradicional()
        {
            _entity.CenarioSiafemSiafisico = 1;

            CallSiafisicoService();
        }
        public void InserirSubempenhoSiafisicoLiquidacaoDePregaoEletrônico()
        {
            _entity.CenarioSiafemSiafisico = 2;

            CallSiafisicoService();
        }
        public void InserirSubempenhoSiafisicoNotaDeLancamentoBec()
        {
            _entity.CenarioSiafemSiafisico = 3;

            CallSiafisicoService();
        }
        public void InserirSubempenhoSiafisicoNotaLancamentoDeContrato()
        {
            _entity.CenarioSiafemSiafisico = 4;

            CallSiafisicoService();
        }
        public void InserirSubempenhoSiafisicoNotaDeLancamentoDeLiquidacaoDeEmpenho()
        {
            _entity.CenarioSiafemSiafisico = 5;

            CallSiafisicoService();
        }

        private static void CriarNovaEntidade()
        {
            _entity = new Subempenho();

            _entity.DataEmissao = Convert.ToDateTime("02/03/2017");
            _entity.CodigoUnidadeGestora = "162101";
            _entity.CodigoGestao = "16055";
            _entity.NumeroCNPJCPFCredor = "00000028000129";
            _entity.DescricaoObservacao1 = "apropriacao para atender despesas referentes a ...";

            //_entity.CodigoGestaoCredor = "16055";
            //_entity.NumeroOriginalProdesp = "179900005";
            //_entity.CodigoTarefa = "02";
            //_entity.CodigoDespesa = "18";
            //_entity.ValorRealizado = 10;
            //_entity.PrazoPagamento = "003";
            //_entity.DataRealizado = new DateTime(2017, 02, 20);

            //_entity.TipoObraId = 1;
            //_entity.AnoMedicao = "2017";
            //_entity.MesMedicao = "02";

            //_entity.Id = default(int);
            //_entity.DataCadastro = DateTime.Now;//default(DateTime);
            //_entity.TransmitirProdesp = default(bool);
            //_entity.TransmitirSiafemSiafisico = default(bool);
            //_entity.NumeroSubempenhoProdesp = default(string);
            //_entity.NumeroSubempenhoSiafemSiafisico = default(string);
            //_entity.NumeroContrato = "1399179024";//default(string);
            //_entity.CenarioSiafemSiafisico = default(int);
            //_entity.NumeroEmpenhoProdesp = default(string);
            //_entity.CodigoTarefa = default(string);
            //_entity.CodigoDespesa = default(string);
            //_entity.ValorRealizado = default(int);
            //_entity.NumeroRecibo = default(string);
            //_entity.PrazoPagamento = default(string);
            //_entity.DataRealizado = default(DateTime);
            //_entity.CenarioProdesp = default(string);
            //_entity.NumeroCT = "00001";//default(string);
            //_entity.NumeroOriginalSiafemSiafisico = "00187";//default(string);
            //_entity.CodigoUnidadeGestora = "180317";//default(string);
            //_entity.CodigoGestao = "00001";//default(string);
            //_entity.CodigoNotaFiscalProdesp = default(string);
            //_entity.NumeroMedicao = default(string);
            //_entity.NaturezaSubempenhoId = default(string);
            //_entity.Valor = 1;//default(int);
            //_entity.CodigoEvento = 511202;//default(int);
            //_entity.UgConsumidora = default(string);
            //_entity.UaConsumidora = default(string);
            //_entity.DataEmissao = Convert.ToDateTime("02/09/2016");//default(DateTime);
            //_entity.TipoEventoId = 511202; //default(int);
            //_entity.NumeroCNPJCPFCredor = "00000028000129";//default(string);
            //_entity.CodigoGestaoCredor = default(string);
            //_entity.AnoMedicao = "2016";//default(string);
            //_entity.MesMedicao = "12";//default(string);
            //_entity.Percentual = "1";//default(string);
            //_entity.ClassificacaoDespesa = default(string);
            //_entity.RegionalId = 16;//default(short);
            //_entity.ProgramaId = 210;//default(int);
            //_entity.NaturezaId = 1683;//default(int);
            //_entity.FonteId = 6;//default(int);
            //_entity.CodigoAplicacaoObra = "0042663";//"100000016";//"0042663";//default(string);
            //_entity.TipoObraId = default(int);
            //_entity.CodigoUnidadeGestoraObra = default(string);
            //_entity.CodigoCredorOrganizacao = 2;//default(int);
            //_entity.NumeroCNPJCPFFornecedor = "00000028000129";//default(string);
            //_entity.DescricaoObservacao1 = "OBSERVAÇÃO";
            //_entity.DescricaoObservacao2 = default(string);
            //_entity.DescricaoObservacao3 = default(string);
            //_entity.NumeroProcesso = "PROCESSO1";//default(string);
            //_entity.NlRetencaoInss = default(string);
            //_entity.Lista = default(string);
            //_entity.Referencia = default(string);
            //_entity.DescricaoAutorizadoSupraFolha = "Test";//default(string);
            //_entity.CodigoEspecificacaoDespesa = "001";//default(string);
            //_entity.DescricaoEspecificacaoDespesa1 = "DESCRIÇÃO";//default(string);
            //_entity.DescricaoEspecificacaoDespesa2 = default(string);
            //_entity.DescricaoEspecificacaoDespesa3 = default(string);
            //_entity.DescricaoEspecificacaoDespesa4 = default(string);
            //_entity.DescricaoEspecificacaoDespesa5 = default(string);
            //_entity.DescricaoEspecificacaoDespesa6 = default(string);
            //_entity.DescricaoEspecificacaoDespesa7 = default(string);
            //_entity.DescricaoEspecificacaoDespesa8 = default(string);
            //_entity.DescricaoEspecificacaoDespesa9 = default(string);
            //_entity.CodigoAutorizadoAssinatura = 11111;//default(int);
            //_entity.CodigoAutorizadoGrupo = 1;//default(int);
            //_entity.CodigoAutorizadoOrgao = "99";//default(string);
            //_entity.DescricaoAutorizadoCargo = "<====NOME DA ASSINATURA 11111=====>";//default(string);
            //_entity.NomeAutorizadoAssinatura = "<==CARGO DA ASSINATURA HUMHUMHUM==>";//default(string);
            //_entity.CodigoExaminadoAssinatura = 12345;//default(int);
            //_entity.CodigoExaminadoGrupo = 1;//default(int);
            //_entity.CodigoExaminadoOrgao = "99";//default(string);
            //_entity.DescricaoExaminadoCargo = "<======UMDOISTRESQUATROCINCO======>";//default(string);
            //_entity.NomeExaminadoAssinatura = "<========DIRETOR EXECUTIVO========>";//default(string);
            //_entity.CodigoResponsavelAssinatura = 88888;//default(int);
            //_entity.CodigoResponsavelGrupo = 1;//default(int);
            //_entity.CodigoResponsavelOrgao = "00";//default(string);
            //_entity.DescricaoResponsavelCargo = "ANALISTA SETOR";//default(string);
            //_entity.NomeResponsavelAssinatura = "KAMIYA";//default(string);
            //_entity.NumeroGuia = default(string);
            //_entity.QuotaGeralAutorizadaPor = default(string);
            //_entity.ValorCaucionado = default(int);
            //_entity.StatusProdesp = default(string);
            //_entity.TransmitidoProdesp = false;//default(bool);
            //_entity.DataTransmitidoProdesp = default(DateTime);
            //_entity.MensagemProdesp = default(string);
            //_entity.StatusSiafemSiafisico = default(string);
            //_entity.TransmitidoSiafemSiafisico = true;//default(bool);
            //_entity.DataTransmitidoSiafemSiafisico = default(DateTime);
            //_entity.MensagemSiafemSiafisico = default(string);
            //_entity.CadastroCompleto = default(bool);
            //_entity.StatusDocumento = default(bool);

            CriarNovaListaDeItensParaAEntidade(_entity);
            CriarNovaListaDeNotasFiscaisParaAEntidade(_entity);
            CriarNovaListaDeEventosParaAEntidade(_entity);
        }
        private static void CriarNovaListaDeNotasFiscaisParaAEntidade(Subempenho entity)
        {
            entity.Notas = new List<SubempenhoNota>
            {
                new SubempenhoNota { SubempenhoId = entity.Id, CodigoNotaFiscal = "nada consta", Ordem = 1 }
            };
        }
        private static void CriarNovaListaDeItensParaAEntidade(Subempenho entity)
        {
            entity.Itens = new List<SubempenhoItem>
            {
                new SubempenhoItem { SubempenhoId = entity.Id, CodigoItemServico = "0821-4", QuantidadeMaterialServico = 1M, CodigoUnidadeFornecimentoItem = "", StatusSiafisicoItem = "N", SequenciaItem = 1 }
            };
        }
        private static void CriarNovaListaDeEventosParaAEntidade(Subempenho entity)
        {
            entity.Eventos = new List<SubempenhoEvento>
            {
                new SubempenhoEvento { SubempenhoId = entity.Id, NumeroEvento = "510600", Fonte = "004001001", InscricaoEvento = "2017NE00015", ValorUnitario = 10 }
            };
        }

        #endregion
    }
}
