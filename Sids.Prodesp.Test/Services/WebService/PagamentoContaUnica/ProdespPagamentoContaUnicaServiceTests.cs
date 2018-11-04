using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;

namespace Sids.Prodesp.Test.Services.WebService.PagamentoContaUnica
{
    [TestClass()]
    public class ProdespPagamentoContaUnicaServiceTests
    {

        Desdobramento _desdobramento = new Desdobramento();

        private readonly Desdobramento _entity;
        private List<Desdobramento> _entities;

        public ProdespPagamentoContaUnicaServiceTests()
        {
            _entities = new List<Desdobramento>();
            _entity = CreateEntityFactory();
        }


        [TestMethod()]
        public void DesdobramentoApoioTest()
        {

            _desdobramento = CreateInstanceDesdobramento();
            var result = App.ProdespPagamentoContaUnicaService.DesdobramentoApoio(_desdobramento, "SIDS000100","DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ConsultaCredorReduzidoTest()
        {
            var result = App.ProdespPagamentoContaUnicaService.ConsultaCredorReduzido("SIDS000100", "DERSIAFEM22716");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void  ConsultarPreparacaoPgtoTipoDespesaDataVencTest()
        {
           var _pgtoTipoDespesaDataVencTest = CreateInstancePreparacaoPagamento();
            var result = App.CommonService.ConsultarPreparacaoPgtoTipoDespesaDataVenc(_pgtoTipoDespesaDataVencTest);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Inserir_DesdobramentoISSQNTest()
        {
            var entity = _entity;
            App.ProdespPagamentoContaUnicaService.Inserir_DesdobramentoISSQN("SIDS000100", "DERSIAFEM22716", ref entity);

        }

        [TestMethod()]
        public void Inserir_DesdobramentoOutrosTest()
        {
            var entity = _entity;
            App.ProdespPagamentoContaUnicaService.Inserir_DesdobramentoOutros("SIDS000100", "DERSIAFEM22716", ref entity);

        }


        private Desdobramento CreateInstanceDesdobramento()
        {
            Desdobramento model = new Desdobramento
            {
                NumeroDocumento = "169900049026011",
                NumeroContrato = "",
                // CodigoServico = "2001",
                CodigoServico = null,
                // ValorDistribuido = 1,
                ValorDistribuido = 0,
                DescricaoServico = "",
                DescricaoCredor = "",
                NomeReduzidoCredor = "",
                TipoDespesa = "",
                ValorIssqn = 0,
                ValorIr = 0,
                ValorInss = 0,
                AceitaCredor = true,
                DocumentoTipoId = 11,
            };
            return model;
        }

        private Desdobramento CreateEntityFactory()
        {
            return new Desdobramento
            {
                AceitaCredor = true,
                CadastroCompleto = true,
                CodigoAplicacaoObra = "1234567",
                CodigoServico = "12345",
                DataEmissao = DateTime.Now,
                DataTransmitidoProdesp = DateTime.Now,
                DescricaoCredor = "TesteLuis",
                DescricaoServico = "Test Luis",
                DocumentoTipoId = 1,
                DesdobramentoTipoId = 1,
                NumeroDesdobramentoTipoId = 1,
                IdentificacaoDesdobramentos = CreateEntityIdentityFactory(130),
                NomeReduzidoCredor = "Teste luis",
                NumeroDocumento = "179900001/001",
                NumeroContrato = "1399179024",
                StatusProdesp = "N",
                TipoDespesa = "001",
                ValorInss = 123,
                ValorDistribuido = 123,
                ValorIssqn = 123,
                ValorIr = 123,
                TransmitirProdesp = true,
                TransmitidoProdesp = false,
                MensagemServicoProdesp = "Teste",
                RegionalId = 16,
            };
        }

        private IEnumerable<IdentificacaoDesdobramento> CreateEntityIdentityFactory(int qtd)
        {
            var lista = new List<IdentificacaoDesdobramento>();

            for (int i = 0; i < qtd; i++)
            {
                lista.Add(new IdentificacaoDesdobramento
                {
                    NomeReduzidoCredor = "Teste"+i,
                    TipoBloqueio = "",
                    ReterId = 1,
                    ValorDesdobrado = 123,
                    ValorPercentual = 123,
                    DesdobramentoTipoId = 1,
                    ValorDistribuicao = 123

                });
            }
            return lista;
        }



        private PreparacaoPagamento CreateInstancePreparacaoPagamento()
        {
            PreparacaoPagamento model = new PreparacaoPagamento
            {
                
                AnoExercicio = 2017,
                DataVencimento = DateTime.Now,
                RegionalId = 16,
                CodigoDespesa = "11",

                CodigoAutorizadoAssinatura = "88888",
                CodigoAutorizadoGrupo = "1",
                CodigoAutorizadoOrgao = "00",

                CodigoExaminadoAssinatura = "00006",
                CodigoExaminadoGrupo = "6",
                CodigoExaminadoOrgao = "00",

                CodigoConta = "234",

            };
            return model;
        }


    }
}