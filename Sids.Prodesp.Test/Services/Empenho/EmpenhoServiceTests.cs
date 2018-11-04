using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Core.Services.Empenho;


namespace Sids.Prodesp.Test.Services.Empenho
{
    using Application;
    using Infrastructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass()]
    public class EmpenhoServiceTests
    {
        Empenho _model;
        List<EmpenhoMes> _mes;
        List<EmpenhoItem> _item;
        Usuario _usuario;

        public EmpenhoServiceTests()
        {
            _usuario = new Usuario {Codigo = 1, CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword, RegionalId = 1};
            CreateInstance();
        }

        [TestMethod()]
        public void SalvarTest()
        {
            var id = App.EmpenhoService.Salvar(_model, _mes, _item, 0, 1);
            Assert.IsTrue(id > 0);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            var list = App.EmpenhoService.Buscar(new Empenho()).ToList() ?? new List<Empenho>();
            Assert.IsTrue(list.Any());
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            var acao = App.EmpenhoService.Excluir(_model, 0, 1);
            Assert.IsTrue(acao > 0);
        }

        [TestMethod()]
        public void TransmitirTest()
        {
            int id = 23;
            App.EmpenhoService.Transmitir(id, _usuario, 0);
            Assert.IsTrue(1 > 0);
        }

        [TestMethod()]
        public void TransmitirTest1()
        {

        }


        private void CreateInstance()
        {
            _model = CreateInstanceEmpenho();
            _mes = CreateInsatanceEmpenhoMes(_model);
            _item = CreateInsatanceEmpenhoItem(_model);
        }


        private static Empenho CreateInstanceEmpenho()
        {
            return new Empenho
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
                CodigoEvento = 400051,
                CodigoGestaoCredor = "16055",
                DataEmissao = DateTime.Now,
                CodigoUnidadeGestora = "162101",
                DescricaoAutorizadoSupraFolha = "teste",
                CodigoEspecificacaoDespesa = "001",
                DescricaoEspecificacaoDespesa = "1RESRE; ; ; ; ; ; ; ; ; ",
                CodigoNaturezaItem = "30",
                CodigoUnidadeGestoraFornecedora = "162101",
                NumeroContrato = "1399179024",
                NumeroCNPJCPFFornecedor = "00000028000129",
                NumeroCNPJCPFUGCredor = "00000028000129",
                NumeroProcesso = "PROCESSO1",
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
                TransmitirProdesp = false,
                TransmitirSiafem = true,
                TransmitirSiafisico = false,
                EmpenhoTipoId = 9,
                DescricaoBairroEntrega = "G",
                ModalidadeId = 1,
                DataEntregaMaterial = DateTime.Now,
                DescricaoLogradouroEntrega = "Teste",
                CodigoCredorOrganizacao = 2,
                NumeroCEPEntrega = "01000000",
                LicitacaoId = "6",
                CodigoAplicacaoObra = "0042663",
                NumeroProcessoSiafisico = "processo1",
                NumeroEmpenhoSiafisico = "2016NE00001",
                OrigemMaterialId = 1,
                CodigoUGO = 162101,
                DescricaoReferenciaLegal = "Lei 866693",
                TipoAquisicaoId = 1,
                CodigoGestaoFornecedora = "16055",
                CodigoMunicipio = "100",
                DescricaoLocalEntregaSiafem = "Rua X"
            };
        }

        private List<EmpenhoMes> CreateInsatanceEmpenhoMes(Empenho objModel)
        {
            return new List<EmpenhoMes>
            {
                new EmpenhoMes {ValorMes = 123M, Id = objModel.Id, Descricao = "01"},
                new EmpenhoMes {ValorMes = 2852345M, Id = objModel.Id, Descricao= "04" },
                new EmpenhoMes {ValorMes = 3425M, Id = objModel.Id, Descricao= "07" },
                new EmpenhoMes {ValorMes = 412345M, Id = objModel.Id, Descricao= "10" }
            };
        }

        private List<EmpenhoItem> CreateInsatanceEmpenhoItem(Empenho objModel)
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
