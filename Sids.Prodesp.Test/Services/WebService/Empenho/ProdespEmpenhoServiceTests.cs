namespace Sids.Prodesp.Test.Services.WebService.Empenho
{
    using Application;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Interface.Base;
    using Model.Interface.Empenho;
    using System;
    using System.Collections.Generic;
    using Sids.Prodesp.Infrastructure;

    [TestClass()]
    public class ProdespEmpenhoServiceTests
    {
        private Model.Entity.Empenho.Empenho _model;
        private IList<IMes> _mes;
        private List<EmpenhoItem> _item;
        private Usuario _usuario;
        private EmpenhoReforco _modelReforco;

        public ProdespEmpenhoServiceTests()
        {
            _usuario = new Usuario { Codigo = 1, CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword };
            CreateInstance();
        }

        [TestMethod()]
        public void InserirEmpenhoTest()
        {
            var result = App.ProdespEmpenhoService.InserirEmpenho(_model, _mes, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirEmpenhoReforcoTest()
        {
            var result = App.ProdespEmpenhoService.InserirEmpenhoReforco(_modelReforco, _mes, "SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirEmpenhoCancelamentoTest()
        {
            var result = App.ProdespEmpenhoService.InserirEmpenhoCancelamento((EmpenhoCancelamento)(IEmpenho)_model, _mes, "", "");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirDocTest()
        {

        }

        [TestMethod()]
        public void ConsultaContratoTest()
        {

        }

        [TestMethod()]
        public void ConsultaReservaEstruturaTest()
        {

        }

        [TestMethod()]
        public void ConsultaReservaTest()
        {

        }

        [TestMethod()]
        public void ConsultaEmpenhoTest()
        {

        }

        [TestMethod()]
        public void ConsultaEspecificacaoDespesaTest()
        {

        }

        [TestMethod()]
        public void ConsultaAssinaturaTest()
        {
            
        }




        private void CreateInstance()
        {
            _model = CreateInstanceEmpenho();
            _modelReforco = CreateInstanceEmpenhoReforco();
            _mes = CreateInsatanceEmpenhoMes(_model);
            _item = CreateInsatanceEmpenhoItem(_model);
        }

        private Model.Entity.Empenho.Empenho CreateInstanceEmpenho()
        {
            return new Model.Entity.Empenho.Empenho
            {
                NumeroAnoExercicio = 2017,
                RegionalId = 16,
                ProgramaId = 210,
                FonteId = 6,
                NaturezaId = 1683,
                CodigoReserva =null,
                NumeroEmpenhoSiafem = "",
                NumeroEmpenhoProdesp = "",
                DestinoId = "24",
                CodigoGestao = "16055",
                CodigoEvento = 400051,
                CodigoGestaoCredor = "1234",
                DataEmissao = DateTime.Now,
                CodigoUnidadeGestora = "162101",
                DescricaoAutorizadoSupraFolha = "test",
                CodigoEspecificacaoDespesa = "001",
                DescricaoEspecificacaoDespesa = "1RESRE; ; ; ; ; ; ; ; ; ",
                CodigoNaturezaItem = "30",
                CodigoUnidadeGestoraFornecedora = "162101",
                NumeroContrato = "1399179024",
                NumeroCNPJCPFFornecedor = "009408250000130",
                NumeroCNPJCPFUGCredor = "009408250000130",
                NumeroProcesso = "PROCESSO GGGG",
                CodigoAutorizadoAssinatura = 11111.ToString(),
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                NomeAutorizadoAssinatura = "<====NOME DA ASSINATURA 11111=====>",
                DescricaoAutorizadoCargo = "<==CARGO DA ASSINATURA HUMHUMHUM==>",
                CodigoExaminadoAssinatura = 12345.ToString(),
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                NomeExaminadoAssinatura = "<======UMDOISTRESQUATROCINCO======>",
                DescricaoExaminadoCargo = "<========DIRETOR EXECUTIVO========>",
                CodigoResponsavelAssinatura = 88888.ToString(),
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NomeResponsavelAssinatura = "ANALISTA SETOR",
                DescricaoResponsavelCargo = "KAMIYA",
                TransmitirProdesp = true,
                TransmitirSiafem = false,
                TransmitirSiafisico = false,
                EmpenhoTipoId = 03,
                DescricaoBairroEntrega = "G",
                ModalidadeId = 3,
                DataEntregaMaterial = DateTime.Now,
                DescricaoLogradouroEntrega = "Teste",
                CodigoCredorOrganizacao = 2,
                NumeroCEPEntrega = "01000000",
               LicitacaoId = "1",
               CodigoAplicacaoObra = "3379024",
               NumeroProcessoSiafisico = "processo1",
               NumeroEmpenhoSiafisico = "2017NE00001",
               NumeroProcessoNE = "Teste"
            };
        }



        private EmpenhoReforco CreateInstanceEmpenhoReforco()
        {
            return new EmpenhoReforco
            {
                NumeroAnoExercicio = 2016,
                RegionalId = 16,
                ProgramaId = 210,
                FonteId = 6,
                NaturezaId = 1683,
                NumeroEmpenhoSiafem = "",
                NumeroEmpenhoProdesp = "",
                DestinoId = "24",
                CodigoGestao = "16055",
                CodigoEvento = 201100,
                CodigoGestaoCredor = "1234",
                DataEmissao = Convert.ToDateTime("30/12/2016"),
                CodigoUnidadeGestora = "162101",
                DescricaoAutorizadoSupraFolha = "test",
                CodigoEspecificacaoDespesa = "001",
                DescricaoEspecificacaoDespesa = "1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE",
                CodigoNaturezaItem = "24",
                CodigoUnidadeGestoraFornecedora = "162101",
                NumeroContrato = "1399179024",
                NumeroCNPJCPFFornecedor = "009408250000130",
                NumeroCNPJCPFUGCredor = "009408250000130",
                NumeroProcesso = "PROCESSO GGGG",
                CodigoAutorizadoAssinatura = 11111.ToString(),
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                NomeAutorizadoAssinatura = "<====NOME DA ASSINATURA 11111=====>",
                DescricaoAutorizadoCargo = "<==CARGO DA ASSINATURA HUMHUMHUM==>",
                CodigoExaminadoAssinatura = 12345.ToString(),
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                NomeExaminadoAssinatura = "<======UMDOISTRESQUATROCINCO======>",
                DescricaoExaminadoCargo = "<========DIRETOR EXECUTIVO========>",
                CodigoResponsavelAssinatura = 88888.ToString(),
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NomeResponsavelAssinatura = "ANALISTA SETOR",
                DescricaoResponsavelCargo = "KAMIYA",
                TransmitirProdesp = true,
                TransmitirSiafem = false,
                TransmitirSiafisico = false,
                ModalidadeId = 3,
                CodigoCredorOrganizacao = 2,
                LicitacaoId = "1",
                NumeroProcessoSiafisico = "processo1",
                NumeroEmpenhoSiafisico = "2016NE00012",
                NumeroProcessoNE = "Teste",
                CodigoEmpenho = "169900048"
            };
        }
        private IList<IMes> CreateInsatanceEmpenhoMes(Model.Entity.Empenho.Empenho objModel)
        {
            return new List<IMes>
            {
                new EmpenhoMes {ValorMes = 2000M, Id = objModel.Id, Descricao= "12" }
            };
        }

        private List<EmpenhoItem> CreateInsatanceEmpenhoItem(Model.Entity.Empenho.Empenho objModel)
        {
            return new List<EmpenhoItem>
            {
                new EmpenhoItem { ValorUnitario = 123, EmpenhoId = objModel.Id },
                new EmpenhoItem { ValorUnitario = 2852345, EmpenhoId = objModel.Id },
                new EmpenhoItem { ValorUnitario = 3425, EmpenhoId = objModel.Id },
                new EmpenhoItem { ValorUnitario = 412345, EmpenhoId = objModel.Id }
            };
        }
    }
}
