using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class ProgramacaoDesembolsoServiceTests
    {
        private readonly ProgramacaoDesembolso _entity;
        private List<ProgramacaoDesembolso> _entities;
        private readonly Usuario _user;

        public ProgramacaoDesembolsoServiceTests()
        {
            _user = new Usuario { Codigo = 1, RegionalId = 16 };
            _entities = new List<ProgramacaoDesembolso>();
            _entity = CreateEntityFactory();
        }
        
        [TestMethod()]
        public void ExcluirTest()
        {
            SaveFactory();
            Assert.IsTrue(RemoveEntity() == AcaoEfetuada.Sucesso);
        }
        
        [TestMethod()]
        public void SalvarOuAlterarTest()
        {
            SaveFactory();
            Assert.IsTrue(_entity.Id > 0);
            RemoveEntity();
        }
        
        [TestMethod()]
        public void ListarTest()
        {
            _entities = App.ProgramacaoDesembolsoService.Listar(new ProgramacaoDesembolso()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            _entities = App.ProgramacaoDesembolsoService.Listar(new ProgramacaoDesembolso()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void SelecionarTest()
        {
            var entity = App.ProgramacaoDesembolsoService.Selecionar(2);
            Assert.IsNotNull(entity);
        }
        
        [TestMethod()]
        public void TransmitirTest()
        {
            _entity.Id = 4;
            SaveFactory();
            App.ProgramacaoDesembolsoService.Transmitir(_entity.Id, _user, 0);
        }
        [TestMethod()]
        public void ImprimirTest()
        {
            //outos = 30
            //issqn = 24
            var pd = App.ProgramacaoDesembolsoService.Selecionar(5);
            var consultaDesdobramento = App.ProgramacaoDesembolsoService.ConsultaPD(new Usuario(), "162101","16055", pd.NumeroSiafem);

            var pdf = HelperReport.GerarPdfProgramacaoDesembolso(consultaDesdobramento, $"Programação Desembolso", pd);


            System.Diagnostics.Process.Start(@"C:\Users\810235\Documents\TestePDF.pdf");

            Assert.IsNotNull(pdf);
        }

        [TestMethod()]
        public void ImprimirAgrupamentoTest()
        {
            var pd = App.ProgramacaoDesembolsoService.Selecionar(33);
            var consultaDesdobramento = App.ProgramacaoDesembolsoService.ConsultaPD(new Usuario(), "162101", "16055", "2017PD00013");

            var pdf = HelperReport.GerarPdfProgramacaoDesembolsoAgrupamento(consultaDesdobramento, "Programação Desembolso", pd);
            
            System.Diagnostics.Process.Start(@"C:\Users\810235\Documents\TestePDF.pdf");

            Assert.IsNotNull(pdf);
        }
        private void SaveFactory()
        {
            _entity.Id = App.ProgramacaoDesembolsoService.SalvarOuAlterar(_entity, 0, 1);
        }

        private AcaoEfetuada RemoveEntity()
        {
            return App.ProgramacaoDesembolsoService.Excluir(_entity, 0, 3);
        }

        private ProgramacaoDesembolso CreateEntityFactory()
        {
            return new ProgramacaoDesembolso
            {
                CadastroCompleto = true,
                CodigoAplicacaoObra = "1234567",
                DataEmissao = DateTime.Now,
                DataVencimento = DateTime.Now,
                DocumentoTipoId = 5,
                NumeroDocumento = "079900001004",
                NumeroContrato = "06000012",
                NumeroNLReferencia = "2017NL00190",
                CodigoUnidadeGestora = "162101",
                CodigoGestao = "16055",
                StatusSiafem = "N",
                TransmitirSiafem = true,
                TransmitidoSiafem = false,
                RegionalId = 16,
                NumeroCnpjcpfPagto = "162184",
                GestaoPagto = "16055",
                NumeroContaPagto = "013000012",
                NumeroAgenciaPagto = "01897",
                NumeroBancoPagto = "001",
                NumeroCnpjcpfCredor = "00000028000129",
                GestaoCredor = " ",
                NumeroContaCredor = "000032247",
                NumeroAgenciaCredor = "06501",
                NumeroBancoCredor = "001",
                CodigoDespesa = "74",
                Valor = 1,
                NumeroProcesso = "processo1",
                Finalidade = "PD Teste",
                ProgramacaoDesembolsoTipoId = 1,
                MensagemServicoSiafem = null,
                Eventos = CreateEntityEventoFactory(),
                Agrupamentos = null//CreateEntityAgrupamentoFactory()
            };
        }

        private IEnumerable<ProgramacaoDesembolsoAgrupamento> CreateEntityAgrupamentoFactory()
        {
            return new List<ProgramacaoDesembolsoAgrupamento>
            {
                new ProgramacaoDesembolsoAgrupamento
                {
                    NomeCredorReduzido = "TARGET",
                    NumeroAgrupamento = 123,
                    NumeroCnpjcpfCredor = "00000028000129",
                    NumeroSiafem = "1122312",
                    MensagemServicoSiafem = "Teste teste",
                    DataVencimento = DateTime.Now,
                    Valor = 123
                }
            };
        }

        private IEnumerable<ProgramacaoDesembolsoEvento> CreateEntityEventoFactory()
        {
            return new List<ProgramacaoDesembolsoEvento>
            {
                new ProgramacaoDesembolsoEvento
                {
                    NumeroEvento = "700601",
                    InscricaoEvento = "2017NE00376",
                    ValorUnitario = 1,
                    Fonte = "004001001"

                }
            };
        }
    }
}