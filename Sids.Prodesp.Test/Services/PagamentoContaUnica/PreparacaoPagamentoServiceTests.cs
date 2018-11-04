using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class PreparacaoPagamentoServiceTests
    {
        private readonly PreparacaoPagamento _entity;
        private List<PreparacaoPagamento> _entities;
        private Usuario _user;

        public PreparacaoPagamentoServiceTests()
        {
            _user = new Usuario { Codigo = 1, RegionalId = 16 };
            _entities = new List<PreparacaoPagamento>();
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
            _entities = App.PreparacaoPagamentoService.Listar(new PreparacaoPagamento()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            _entities = App.PreparacaoPagamentoService.Listar(new PreparacaoPagamento()).ToList();
            Assert.IsTrue(_entities.Any());
        }

        [TestMethod()]
        public void SelecionarTest()
        {
            var entity = App.PreparacaoPagamentoService.Selecionar(1);
            Assert.IsNotNull(entity);
        }


        [TestMethod()]
        public void TransmitirTest()
        {
            //SaveFactory();
            App.PreparacaoPagamentoService.Transmitir(5, _user, 0);
        }

        private void SaveFactory()
        {
            _entity.Id = App.PreparacaoPagamentoService.SalvarOuAlterar(_entity, 0, 1);
        }
        private AcaoEfetuada RemoveEntity()
        {
            return App.PreparacaoPagamentoService.Excluir(_entity, 0, 3);
        }

        private PreparacaoPagamento CreateEntityFactory()
        {
            return new PreparacaoPagamento
            {
                CadastroCompleto = true,
                CodigoAplicacaoObra = "1234567",
                DataEmissao = DateTime.Now,
                DataTransmitidoProdesp = DateTime.Now,
                DocumentoTipoId = 5,
                NumeroDocumento = "079900001004",
                NumeroContrato = "06000012",
                StatusProdesp = "N",
                TransmitirProdesp = true,
                TransmitidoProdesp = false,
                RegionalId = 16,
                CodigoConta = "234",
                NumeroConta = "0000",
                NumeroAgencia = "00000",
                NumeroBanco = "00000",
                NumeroContaCredor = "0000",
                NumeroAgenciaCredor = "00000",
                NumeroBancoCredor = "00000",
                NumeroContaPagto = "0000",
                NumeroAgenciaPagto = "00000",
                NumeroBancoPagto = "00000",
                Cep = "01234-022",
                Endereco = "Rua QUAS,123",
                Credor1 = "HIDEO KAMIYA",
                Credor2 = "TARGET",
                NumeroCnpjcpfCredor = "89907035904",
                CodigoDespesa = "74",
                CodigoAutorizadoAssinatura = "88888",
                CodigoExaminadoAssinatura = "00006",
                CodigoAutorizadoGrupo = "1",
                CodigoAutorizadoOrgao = "00",
                CodigoExaminadoGrupo = "6",
                CodigoCredorOrganizacaoId = "2",
                CodigoExaminadoOrgao = "00",
                CodigoDespesaCredor = "12",
                DescricaoAutorizadoCargo = "Teste",
                DescricaoExaminadoCargo = "Teste",
                DataVencimento = Convert.ToDateTime("29/04/2007"),
                ValorDocumento = 1,
                StatusDocumento = true,
                ValorTotal = 1,
                QuantidadeOpPreparada = 1,
                PreparacaoPagamentoTipoId = 1,
                NumeroOpInicial = null,
                NumeroOpFinal = null,
                NomeAutorizadoAssinatura = "Luis",
                NomeExaminadoAssinatura = "Luis",
                MensagemServicoProdesp = null,
                AnoExercicio = 2007,
                Referencia = "2007NE00123/ADIANTAMENTO",

            };
        }


    }
}