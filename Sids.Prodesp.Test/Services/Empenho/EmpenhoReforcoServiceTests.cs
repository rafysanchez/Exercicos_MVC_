
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;

using Sids.Prodesp.Model.Entity.Seguranca;
using System;
using System.Collections.Generic;

namespace Sids.Prodesp.Test.Services.Empenho
{
    using Infrastructure;
    using Sids.Prodesp.Model.Entity.Empenho;
    [TestClass()]
    public class EmpenhoReforcoServiceTests
    {
         EmpenhoReforco _reforco;
         List<EmpenhoReforcoMes> _reforcoMes;
         List<EmpenhoReforcoItem> _reforcoItem;
         Usuario _usuario;

        public EmpenhoReforcoServiceTests()
        {
            _usuario = new Usuario { Codigo = 1, CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword, RegionalId = 1 };
            CreateInstance();
        }

        [TestMethod()]
        public void SalvarTest()
        {
            int id = App.EmpenhoReforcoService.Salvar(_reforco, _reforcoMes, _reforcoItem, 0, 1);
            Assert.IsTrue(id > 0);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            var list = App.EmpenhoReforcoService.Buscar(new EmpenhoReforco());
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ExcluirTest()
        {
            var acao = App.EmpenhoReforcoService.Excluir(_reforco, 0, 1);
            Assert.IsTrue(acao > 0);
        }

        private void CreateInstance()
        {
            _reforco = CriarInstanciaReforco();
            _reforcoMes = CriarInstanciaReforcoMes(_reforco);
            _reforcoItem = CriarInstanciaReforcoItem(_reforco);
        }
        
        private static EmpenhoReforco CriarInstanciaReforco()
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

        static List<EmpenhoReforcoMes> CriarInstanciaReforcoMes(EmpenhoReforco objModel)
        {
            return new List<EmpenhoReforcoMes>
            {
               new EmpenhoReforcoMes {ValorMes = 123M, Id = objModel.Id, Descricao = "01"},
               new EmpenhoReforcoMes {ValorMes = 3425M, Id = objModel.Id, Descricao= "07" },
               new EmpenhoReforcoMes {ValorMes = 412345M, Id = objModel.Id, Descricao= "10" },
               new EmpenhoReforcoMes {ValorMes = 2852345M, Id = objModel.Id, Descricao= "04" },
            };
        }

        private List<EmpenhoReforcoItem> CriarInstanciaReforcoItem(EmpenhoReforco objModel)
        {
            return new List<EmpenhoReforcoItem>
            {
                new EmpenhoReforcoItem { ValorUnitario = 123, EmpenhoId = objModel.Id },
                new EmpenhoReforcoItem { ValorUnitario = 2852345, EmpenhoId = objModel.Id },
                new EmpenhoReforcoItem { ValorUnitario = 3425, EmpenhoId = objModel.Id },
                new EmpenhoReforcoItem { ValorUnitario = 412345, EmpenhoId = objModel.Id }
            };
        }       

       
    }
}
