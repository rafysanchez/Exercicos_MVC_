using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class ListaBoletosServiceTests
    {
        private readonly ListaBoletos _entity;
        private List<ListaBoletos> _entities;
        private readonly Usuario _user;

        public ListaBoletosServiceTests()
        {
            _user = new Usuario { Codigo = 1, RegionalId = 16 };
            _entities = new List<ListaBoletos>();
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
            SaveFactory();
            _entities = App.ListaBoletosService.Listar(new ListaBoletos()).ToList();
            Assert.IsTrue(_entities.Any());
            RemoveEntity();
        }

        [TestMethod()]
        public void SelecionarTest()
        {

            _entities = App.ListaBoletosService.Listar(new ListaBoletos()).ToList();
            var entity = App.ListaBoletosService.Selecionar(_entities.FirstOrDefault().Id);
            Assert.IsNotNull(entity);
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            _entities = App.ListaBoletosService.Listar(new ListaBoletos()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void TransmitirTest()
        {

            SaveFactory();
            App.ListaBoletosService.Transmitir(_entity.Id, _user, 0);
        }

        private void SaveFactory()
        {
            _entity.Id = App.ListaBoletosService.SalvarOuAlterar(_entity, 0, 1);
        }

        private AcaoEfetuada RemoveEntity()
        {
            return App.ListaBoletosService.Excluir(_entity, 0, 3);
        }

        private static ListaBoletos CreateEntityFactory()
        {
            return new ListaBoletos
            {
                Id = 18,
                CadastroCompleto = true,
                CodigoAplicacaoObra = "1234567",
                DataEmissao = DateTime.Now,
                DataTransmitidoSiafem = DateTime.Now,
                DocumentoTipoId = 5,
                NumeroDocumento = "179900001001",
                NumeroContrato = "1399179024",
                StatusSiafem = "N",
                TransmitirSiafem = true,
                TransmitidoSiafem = false,
                RegionalId = 16,
                CodigoUnidadeGestora = "162101",
                CodigoGestao = "16055",
                NumeroCnpjcpfFavorecido = "29979036000140",
                NomeLista = "Lista gerada no test unitáro",
                TotalCredores = 15,
                ValorTotalLista = 10,
                ListaCodigoBarras = CreateEntityCodigoFactory()
            };
        }

        private static IEnumerable<ListaCodigoBarras> CreateEntityCodigoFactory()
        {
            return new List<ListaCodigoBarras>
            {
                new ListaCodigoBarras
                {
                    TipoBoletoId = 2,
                    CodigoBarraTaxa = new CodigoBarraTaxa
                    {
                        NumeroConta1 = "858000000097",
                        NumeroConta2 = "116602702642",
                        NumeroConta3 = "044416618008",
                        NumeroConta4 = "010220170476"
                    }
                },
                new ListaCodigoBarras
                {
                    TipoBoletoId = 2,
                    CodigoBarraTaxa = new CodigoBarraTaxa
                    {
                        NumeroConta1 = "850200000066",
                        NumeroConta2 = "077402702644",
                        NumeroConta3 = "065525404001",
                        NumeroConta4 = "014420170472"
                    }
                }
            };
        }

        private static CodigoBarraTaxa CreateEntityBoletoFactory()
        {
            return new CodigoBarraTaxa
            {
                NumeroConta1 = "858800006022",
                NumeroConta2 = "056502702643",
                NumeroConta3 = "044416618008",
                NumeroConta4 = "010220170476"
            };
        }

        private static CodigoBarraBoleto CreateEntityTaxaFactory()
        {
            return new CodigoBarraBoleto
            {
                NumeroConta1 = "23456789",
                NumeroConta2 = "23456789",
                NumeroConta3 = "23456789",
                NumeroConta4 = "23456789",
                NumeroConta5 = "23456789",
                NumeroConta6 = "23456789",
                NumeroDigito = "1",
                NumeroConta7 = "23456789",
            };
        }
    }
}
