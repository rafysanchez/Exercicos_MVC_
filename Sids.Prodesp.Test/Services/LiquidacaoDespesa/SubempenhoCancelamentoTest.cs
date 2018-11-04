namespace Sids.Prodesp.Test.Services.LiquidacaoDespesa
{
    using Application;
    using Core.Base;
    using Core.Services.LiquidacaoDespesa;
    using Core.Services.WebService.LiquidacaoDespesa;
    using Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Entity.Seguranca;
    using Model.Enum;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class SubempenhoCancelamentoTest
    {
        #region Fields

        readonly SubempenhoCancelamentoService _subempenhoService = App.SubempenhoCancelamentoService;
        readonly SubempenhoCancelamentoEventoService _eventoService = App.SubempenhoCancelamentoEventoService;
        readonly SubempenhoCancelamentoNotaService _notaService = App.SubempenhoCancelamentoNotaService;
        readonly SubempenhoCancelamentoItemService _itemService = App.SubempenhoCancelamentoItemService;

        readonly SiafemLiquidacaoDespesaService _siafemSiafisico = App.SiafemLiquidacaoDespesaService;

        readonly Usuario _usuario;

        SubempenhoCancelamento _entity;

        #endregion
        
        public SubempenhoCancelamentoTest()
        {
            _usuario = new Usuario { Codigo = 1, CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword, RegionalId = 1 };
            _entity = CriarNovaEntidade();
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

            var item = _entity.Itens.FirstOrDefault(f => f.CodigoItemServico.Equals("000008")) as SubempenhoCancelamentoItem;

            item.QuantidadeMaterialServico = 12M;

            AlterarUmItemDaEntidadeNoRepositorio(item);

            item = null;

            BuscarUmSubempenhoNoRepositorio(_entity);

            var selectedItem = _entity.Itens.FirstOrDefault(f => f.CodigoItemServico.Equals("000008"));

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

            var evento = _entity.Eventos.FirstOrDefault(f => f.NumeroEvento.Equals("510600")) as SubempenhoCancelamentoEvento;

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

            var nota = _entity.Notas.FirstOrDefault(f => f.CodigoNotaFiscal.Equals("nada consta")) as SubempenhoCancelamentoNota;

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

        [TestMethod()]
        public void AnulacaoSubEmpenhoApoioTest()
        {
            var result = App.ProdespLiquidacaoDespesaService.InserirAnulacaoSubEmpenho(_entity, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }


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

        #endregion
        
        #region TestHelpers

        private void SalvarEntidadeNoRepositorio()
        {
            _entity.Id = _subempenhoService.SalvarOuAlterar(CriarNovaEntidade(), 0, (short)EnumAcao.Inserir);
        }
        private void AlterarEntidadeNoRepositorio(SubempenhoCancelamento entity)
        {
            _subempenhoService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void AlterarUmItemDaEntidadeNoRepositorio(SubempenhoCancelamentoItem entity)
        {
            _itemService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void AlterarUmEventoDaEntidadeNoRepositorio(SubempenhoCancelamentoEvento entity)
        {
            _eventoService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void AlterarUmaNotaDaEntidadeNoRepositorio(SubempenhoCancelamentoNota entity)
        {
            _notaService.SalvarOuAlterar(entity, 0, (short)EnumAcao.Alterar);
        }
        private void BuscarUmSubempenhoNoRepositorio(SubempenhoCancelamento entity)
        {
            _entity = _subempenhoService.Selecionar(entity.Id);
        }
        private AcaoEfetuada RemoverEntidadeDoRepositorio(SubempenhoCancelamento entity)
        {
            return _subempenhoService.Excluir(entity, 0, (short)EnumAcao.Excluir);
        }


        private void CallSiafemService()
        {
            _entity.NumeroSiafemSiafisico = _siafemSiafisico.InserirSubempenhoSiafem(AppConfig.WsSiafemUser, AppConfig.WsPassword, "162101", _entity);
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
                .InserirSubempenhoCancelamentoSiafisico(AppConfig.WsSiafisicoUser, AppConfig.WsPassword, "162101", _entity);
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

        private SubempenhoCancelamento CriarNovaEntidade()
        {
            var entity = new SubempenhoCancelamento();

            // entity.DataEmissao = Convert.ToDateTime("07/03/2017");
            // entity.CodigoUnidadeGestora = "162101";
            //  entity.CodigoGestao = "16055";
            //entity.CodigoGestaoCredor = "16055";
            //   entity.NumeroCNPJCPFCredor = "00000028000129";
            //   entity.DescricaoObservacao1 = "ANULACAO PARCIAL DA 2017NL00009";

            //entity.Id = default(int);
            //entity.DataCadastro = DateTime.Now;//default(DateTime);
            //entity.TransmitirProdesp = default(bool);
            //entity.TransmitirSiafemSiafisico = default(bool);
            //entity.NumeroSubempenhoProdesp = default(string);
            //entity.NumeroSubempenhoSiafemSiafisico = default(string);
            //entity.NumeroContrato = "1399179024";//default(string);
            //entity.CenarioSiafemSiafisico = default(int);
            entity.NumeroOriginalProdesp = "169900042001";
            //entity.CodigoTarefa = default(string);
            //entity.CodigoDespesa = default(string);
            //entity.ValorRealizado = default(int);
            //entity.NumeroRecibo = default(string);
            //entity.PrazoPagamento = default(string);
            //entity.DataRealizado = default(DateTime);
            //entity.CenarioProdesp = default(string);
            //entity.NumeroCT = "00001";//default(string);
            //entity.NumeroOriginalSiafemSiafisico = "00187";//default(string);
            //entity.CodigoUnidadeGestora = "180317";//default(string);
            //entity.CodigoGestao = "00001";//default(string);
            //entity.CodigoNotaFiscalProdesp = default(string);
            //entity.NumeroMedicao = default(string);
            //entity.NaturezaSubempenhoId = default(string);
            entity.Valor = 100;//default(int);
            //entity.CodigoEvento = 511202;//default(int);
            //entity.UgConsumidora = default(string);
            //entity.UaConsumidora = default(string);
            //entity.DataEmissao = Convert.ToDateTime("02/09/2016");//default(DateTime);
            //entity.TipoEventoId = 511202; //default(int);
            //entity.NumeroCNPJCPFCredor = "00000028000129";//default(string);
            //entity.CodigoGestaoCredor = default(string);
            //entity.AnoMedicao = "2016";//default(string);
            //entity.MesMedicao = "12";//default(string);
            //entity.Percentual = "1";//default(string);
            //entity.ClassificacaoDespesa = default(string);
            //entity.RegionalId = 16;//default(short);
            //entity.ProgramaId = 210;//default(int);
            //entity.NaturezaId = 1683;//default(int);
            //entity.FonteId = 6;//default(int);
            //entity.CodigoAplicacaoObra = "0042663";//"100000016";//"0042663";//default(string);
            //entity.TipoObraId = default(int);
            //entity.CodigoUnidadeGestoraObra = default(string);
            //entity.CodigoCredorOrganizacao = 2;//default(int);
            //entity.NumeroCNPJCPFFornecedor = "00000028000129";//default(string);
            //entity.DescricaoObservacao1 = "OBSERVAÇÃO";
            //entity.DescricaoObservacao2 = default(string);
            //entity.DescricaoObservacao3 = default(string);
            //entity.NumeroProcesso = "PROCESSO1";//default(string);
            //entity.NlRetencaoInss = default(string);
            //entity.Lista = default(string);
            //entity.Referencia = default(string);
            //entity.DescricaoAutorizadoSupraFolha = "Test";//default(string);
            //entity.CodigoEspecificacaoDespesa = "001";//default(string);
            //entity.DescricaoEspecificacaoDespesa1 = "DESCRIÇÃO";//default(string);
            //entity.DescricaoEspecificacaoDespesa2 = default(string);
            //entity.DescricaoEspecificacaoDespesa3 = default(string);
            //entity.DescricaoEspecificacaoDespesa4 = default(string);
            //entity.DescricaoEspecificacaoDespesa5 = default(string);
            //entity.DescricaoEspecificacaoDespesa6 = default(string);
            //entity.DescricaoEspecificacaoDespesa7 = default(string);
            //entity.DescricaoEspecificacaoDespesa8 = default(string);
            //entity.DescricaoEspecificacaoDespesa9 = default(string);
            //entity.CodigoAutorizadoAssinatura = 11111;//default(int);
            //entity.CodigoAutorizadoGrupo = 1;//default(int);
            //entity.CodigoAutorizadoOrgao = "99";//default(string);
            //entity.DescricaoAutorizadoCargo = "<====NOME DA ASSINATURA 11111=====>";//default(string);
            //entity.NomeAutorizadoAssinatura = "<==CARGO DA ASSINATURA HUMHUMHUM==>";//default(string);
            //entity.CodigoExaminadoAssinatura = 12345;//default(int);
            //entity.CodigoExaminadoGrupo = 1;//default(int);
            //entity.CodigoExaminadoOrgao = "99";//default(string);
            //entity.DescricaoExaminadoCargo = "<======UMDOISTRESQUATROCINCO======>";//default(string);
            //entity.NomeExaminadoAssinatura = "<========DIRETOR EXECUTIVO========>";//default(string);
            //entity.CodigoResponsavelAssinatura = 88888;//default(int);
            //entity.CodigoResponsavelGrupo = 1;//default(int);
            //entity.CodigoResponsavelOrgao = "00";//default(string);
            //entity.DescricaoResponsavelCargo = "ANALISTA SETOR";//default(string);
            //entity.NomeResponsavelAssinatura = "KAMIYA";//default(string);
            //entity.NumeroGuia = default(string);
            //entity.QuotaGeralAutorizadaPor = default(string);
            //entity.ValorCaucionado = default(int);
            //entity.StatusProdesp = default(string);
            //entity.TransmitidoProdesp = false;//default(bool);
            //entity.DataTransmitidoProdesp = default(DateTime);
            //entity.MensagemProdesp = default(string);
            //entity.StatusSiafemSiafisico = default(string);
            //entity.TransmitidoSiafemSiafisico = true;//default(bool);
            //entity.DataTransmitidoSiafemSiafisico = default(DateTime);
            //entity.MensagemSiafemSiafisico = default(string);
            //entity.CadastroCompleto = default(bool);
            //entity.StatusDocumento = default(bool);

            entity.Itens = CriarNovaListaDeItensParaAEntidade(entity);
            entity.Notas = CriarNovaListaDeNotasFiscaisParaAEntidade(entity);
            entity.Eventos = CriarNovaListaDeEventosParaAEntidade(entity);

            return entity;
        }
        private IEnumerable<SubempenhoCancelamentoNota> CriarNovaListaDeNotasFiscaisParaAEntidade(SubempenhoCancelamento entity)
        {
            return new List<SubempenhoCancelamentoNota>
            {
                new SubempenhoCancelamentoNota { SubempenhoId = entity.Id, CodigoNotaFiscal = "nada consta", Ordem = 1 }
            };
        }
        private IEnumerable<SubempenhoCancelamentoItem> CriarNovaListaDeItensParaAEntidade(SubempenhoCancelamento entity)
        {
            return new List<SubempenhoCancelamentoItem>
            {
                new SubempenhoCancelamentoItem { SubempenhoId = entity.Id, CodigoItemServico = "00000821-4", QuantidadeMaterialServico = 1M, CodigoUnidadeFornecimentoItem = "12345", StatusSiafisicoItem = "N", SequenciaItem = 1 }
            };
        }
        private IEnumerable<SubempenhoCancelamentoEvento> CriarNovaListaDeEventosParaAEntidade(SubempenhoCancelamento entity)
        {
            return new List<SubempenhoCancelamentoEvento>
            {
                new SubempenhoCancelamentoEvento { SubempenhoId = entity.Id, NumeroEvento = "515600", Fonte = "004001001", InscricaoEvento = "2017NE00015", ValorUnitario = 1 }
            };
        }

        #endregion
    }
}
