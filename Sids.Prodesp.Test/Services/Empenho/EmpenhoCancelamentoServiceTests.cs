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
    public class EmpenhoCancelamentoServiceTests
    {
        EmpenhoCancelamento _model;
        IEnumerable<EmpenhoCancelamentoMes> _mes;
        IEnumerable<EmpenhoCancelamentoItem> _item;
        Usuario _usuario;

        public EmpenhoCancelamentoServiceTests()
        {
            _usuario = new Usuario { Codigo = 1, CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword, RegionalId = 1 };
            CreateInstance();
        }

        [TestMethod()]
        public void SalvarTest()
        {
            var id = App.EmpenhoCancelamentoService.Salvar(_model, _mes, _item, 0, 1);
            Assert.IsTrue(id > 0);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            var list = App.EmpenhoCancelamentoService.Buscar(new EmpenhoCancelamento()).ToList() ?? new List<EmpenhoCancelamento>();
            Assert.IsTrue(list.Any());
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            var acao = App.EmpenhoCancelamentoService.Excluir(_model, 0, 1);
            Assert.IsTrue(acao > 0);
        }

        [TestMethod()]
        public void TransmitirTest()
        {
            int id = 23;
            App.EmpenhoCancelamentoService.Transmitir(id, _usuario, 0);
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


        private static EmpenhoCancelamento CreateInstanceEmpenho()
        {
            return new EmpenhoCancelamento
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
                DataEmissao = Convert.ToDateTime("31/12/2016"),
                CodigoUnidadeGestora = "162101",
                DescricaoAutorizadoSupraFolha = "teste",
                CodigoEspecificacaoDespesa = "001",
                DescricaoEspecificacaoDespesa = "1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE",
                CodigoNaturezaItem = "30",
                CodigoUnidadeGestoraFornecedora = "162101",
                NumeroContrato = "1399179024",
                NumeroCNPJCPFFornecedor = "00000028000129",
                NumeroCNPJCPFUGCredor = "00000028000129",
                NumeroProcesso = "PROCESSO1",
                CodigoAutorizadoAssinatura = "11111",
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                NomeAutorizadoAssinatura = "<====NOME DA ASSINATURA 11111=====>",
                DescricaoAutorizadoCargo = "<==CARGO DA ASSINATURA HUMHUMHUM==>",
                CodigoExaminadoAssinatura = "12345",
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                NomeExaminadoAssinatura = "<======UMDOISTRESQUATROCINCO======>",
                DescricaoExaminadoCargo = "<========DIRETOR EXECUTIVO========>",
                CodigoResponsavelAssinatura = "88888",
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NomeResponsavelAssinatura = "ANALISTA SETOR",
                DescricaoResponsavelCargo = "KAMIYA",
                TransmitirProdesp = false,
                TransmitirSiafem = true,
                TransmitirSiafisico = false,
                ModalidadeId = 1,
                CodigoCredorOrganizacao = 2,
                LicitacaoId = "6",
                NumeroProcessoSiafisico = "processo1",
                NumeroEmpenhoSiafisico = "2016NE00001",
                OrigemMaterialId = 1,
                TipoAquisicaoId = 1,
                CodigoGestaoFornecedora = "16055",
                CodigoMunicipio = "100",
                DescricaoLocalEntregaSiafem = "Rua X"
            };
        }

        private IEnumerable<EmpenhoCancelamentoMes> CreateInsatanceEmpenhoMes(EmpenhoCancelamento objModel)
        {
            return new List<EmpenhoCancelamentoMes>
            {
                new EmpenhoCancelamentoMes { ValorMes = 123M, Id = objModel.Id, Descricao = "01" },
                new EmpenhoCancelamentoMes { ValorMes = 2852345M, Id = objModel.Id, Descricao= "04" },
                new EmpenhoCancelamentoMes { ValorMes = 3425M, Id = objModel.Id, Descricao= "07" },
                new EmpenhoCancelamentoMes { ValorMes = 412345M, Id = objModel.Id, Descricao= "10" }
            };
        }

        private IEnumerable<EmpenhoCancelamentoItem> CreateInsatanceEmpenhoItem(EmpenhoCancelamento objModel)
        {
            return new List<EmpenhoCancelamentoItem>
            {
                new EmpenhoCancelamentoItem { ValorUnitario = 123, EmpenhoId = objModel.Id },
                new EmpenhoCancelamentoItem { ValorUnitario = 2852345, EmpenhoId = objModel.Id },
                new EmpenhoCancelamentoItem { ValorUnitario = 3425, EmpenhoId = objModel.Id },
                new EmpenhoCancelamentoItem { ValorUnitario = 412345, EmpenhoId = objModel.Id }
            };
        }
    }
}