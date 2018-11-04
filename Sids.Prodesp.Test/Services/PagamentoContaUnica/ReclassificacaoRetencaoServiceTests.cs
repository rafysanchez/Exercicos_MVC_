using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class ReclassificacaoRetencaoServiceTests
    {
        private readonly ReclassificacaoRetencao _entity;
        private  List<ReclassificacaoRetencao> _entities;
        private Usuario _user;

        public ReclassificacaoRetencaoServiceTests()
        {
            _user = new Usuario { Codigo = 1, RegionalId = 16 };
            _entities = new List<ReclassificacaoRetencao>();
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
            _entities = App.ReclassificacaoRetencaoService.Listar(new ReclassificacaoRetencao()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            _entities = App.ReclassificacaoRetencaoService.Listar(new ReclassificacaoRetencao()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void SelecionarTest()
        {
            var entity = App.ReclassificacaoRetencaoService.Selecionar(1);
            Assert.IsNotNull(entity);
        }

        [TestMethod()]
        public void TransmitirTest()
        {

        }

        [TestMethod()]
        public void TransmitirTest1()
        {

        }

        private void SaveFactory()
        {
            _entity.Id = App.ReclassificacaoRetencaoService.SalvarOuAlterar(_entity, 0, 1);
        }
        private AcaoEfetuada RemoveEntity()
        {
            return App.ReclassificacaoRetencaoService.Excluir(_entity, 0, 3);
        }

        private ReclassificacaoRetencao CreateEntityFactory()
        {
            return new ReclassificacaoRetencao
            {
                CadastroCompleto = true,
                CodigoAplicacaoObra = "1234567",
                DataEmissao = DateTime.Now,
                DataTransmitidoSiafem = DateTime.Now,
                DocumentoTipoId = 5,
                Notas = CreateEntityNotaFactory(),
                Eventos = CreateEntityEventoFactory(),
                NumeroDocumento = "179900001001",
                NumeroContrato = "1399179024",
                StatusSiafem = "N",
                TransmitirSiafem = true,
                TransmitidoSiafem = false,
                RegionalId = 16
            };
        }

        private IEnumerable<ReclassificacaoRetencaoNota> CreateEntityNotaFactory()
        {
            return new List<ReclassificacaoRetencaoNota>
            {
                new ReclassificacaoRetencaoNota
                {
                    CodigoNotaFiscal = "Nada Consta",
                    Ordem = 1
                }
            };
        }

        private IEnumerable<ReclassificacaoRetencaoEvento> CreateEntityEventoFactory()
        {
            return new List<ReclassificacaoRetencaoEvento>
            {
                new ReclassificacaoRetencaoEvento
                {
                    NumeroEvento = "510600",
                    InscricaoEvento = "2017ne00330",
                    ValorUnitario = 1,
                    Fonte = "004001001"

                }
            };
        }
    }
}