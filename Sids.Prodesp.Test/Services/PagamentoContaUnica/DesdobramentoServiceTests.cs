using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.UI.Report;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class DesdobramentoServiceTests
    {
        private readonly Desdobramento _entity;
        private List<Desdobramento> _entities;
        private readonly Usuario _user;

        public DesdobramentoServiceTests()
        {
            _user = new Usuario {Codigo =  1, RegionalId = 16};
            _entities = new List<Desdobramento>();
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
            _entities = App.DesdobramentoService.Listar(new Desdobramento()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            _entities = App.DesdobramentoService.Listar(new Desdobramento()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void SelecionarTest()
        {
            var entity = App.DesdobramentoService.Selecionar(_entity.Id);
            Assert.IsNotNull(entity);
        }

        [TestMethod()]
        public void TransmitirTest()
        {

        }

        [TestMethod()]
        public void TransmitirTest1()
        {
            SaveFactory();
            App.DesdobramentoService.Transmitir(_entity.Id,_user,0);
        }

        [TestMethod()]
        public void ConsultaDesdobramentoTest()
        {
            //outos = 30
            //issqn = 24
            var desdobramento = App.DesdobramentoService.Selecionar(30);
            var consultaDesdobramento = App.DesdobramentoService.ConsultaDesdobramento(desdobramento);

            var pdf = HelperReport.GerarPdfDesdobramento(consultaDesdobramento, $"Desdobramento para {desdobramento.DesdobramentoTipo.Descricao}", desdobramento);


            System.Diagnostics.Process.Start(@"C:\Users\810235\Documents\TestePDF.pdf");

            Assert.IsNotNull(pdf);
        }

        private void SaveFactory()
        {
            _entity.Id = App.DesdobramentoService.SalvarOuAlterar(_entity, 0, 1);
        }
        private AcaoEfetuada RemoveEntity()
        {
            return App.DesdobramentoService.Excluir(_entity, 0, 3);
        }


        private Desdobramento CreateEntityFactory()
        {
            return new Desdobramento
            {
                AceitaCredor = true,
                CadastroCompleto = true,
                CodigoAplicacaoObra = "1234567",
                CodigoServico = "2001",
                DataEmissao = DateTime.Now,
                DataTransmitidoProdesp = DateTime.Now,
                DescricaoCredor = "BENEDITO ANTONIO",
                DescricaoServico = "SERVIÇOS PORTUáRIOS, FERROPORTUáRIOS, UTILIZAÇãO DE PORT",
                DocumentoTipoId = 5,
                DesdobramentoTipoId = 2,
                NumeroDesdobramentoTipoId = 1,
                IdentificacaoDesdobramentos = CreateEntityIdentityFactory(),
                NomeReduzidoCredor = "BENEDITO",
                NumeroDocumento = "179900001001",
                NumeroContrato = "1399179024",
                StatusProdesp = "N",
                TipoDespesa = "18",
                ValorDistribuido = 10,
                TransmitirProdesp = true,
                TransmitidoProdesp = false,
                RegionalId = 16
            };
        }

        private IEnumerable<IdentificacaoDesdobramento> CreateEntityIdentityFactory()
        {
            return new List<IdentificacaoDesdobramento>
            {
                new IdentificacaoDesdobramento
                {
                    DesdobramentoTipoId = 1,
                    NomeReduzidoCredor = "ISS/TATUI",
                    TipoBloqueio = "",
                    ValorDistribuicao = 5,
                    ValorDesdobrado = 4

                },new IdentificacaoDesdobramento
                {
                    DesdobramentoTipoId = 1,
                    NomeReduzidoCredor = "ISS/CAMPINAS",
                    TipoBloqueio = "",
                    ValorDistribuicao = 5,
                    ValorDesdobrado = 5,

                },new IdentificacaoDesdobramento
                {
                    DesdobramentoTipoId = 2,
                    NomeReduzidoCredor = "JANE",
                    TipoBloqueio = "",
                    ValorDistribuicao = 0

                }
            };
        }
    }
}