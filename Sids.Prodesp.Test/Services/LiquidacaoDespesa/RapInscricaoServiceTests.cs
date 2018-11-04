using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.LiquidacaoDespesa;
using Sids.Prodesp.Core.Services.WebService.LiquidacaoDespesa;
using Sids.Prodesp.Model.Base.LiquidacaoDespesa;
using Sids.Prodesp.Model.Entity.LiquidacaoDespesa;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test.Services.LiquidacaoDespesa
{
    [TestClass()]
    public class RapInscricaoServiceTests
    {
        private readonly RapInscricaoService _rapInscricaoService = App.RapInscricaoService;

        private readonly SiafemLiquidacaoDespesaService _siafemSiafisico = App.SiafemLiquidacaoDespesaService;

        private static RapInscricao _entity;
        private static RapInscricao _model1;
        private static RapInscricao _model2;
        private static RapInscricao _model3;
        private static RapInscricao _model4;
        private static IEnumerable<RapInscricao> _entityList;

        public RapInscricaoServiceTests()
        {
            CriarNovaEntidade();
            CreateInstance(); //only for prodesp service test
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            SalvarEntidadeNoRepositorio();

            var acao = RemoverEntidadeDoRepositorio(_entity);
            Assert.IsTrue(acao == AcaoEfetuada.Sucesso);
        }

        [TestMethod()]
        public void SalvarOuAlterarTest()
        {
            int id = SalvarEntidadeNoRepositorio();

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsTrue(id > 0);
        }


        [TestMethod()]
        public void ListarTest()
        {

            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            ListarRapInscricaoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(_entityList);
        }

        [TestMethod()]
        public void BuscarGridTest()
        {

            SalvarEntidadeNoRepositorio();


            BuscarGridRapInscricaoNoRepositorio(_entity, DateTime.Now, DateTime.Now);

            foreach (var rapInscricao in _entityList)
            {
                RemoverEntidadeDoRepositorio(rapInscricao);
            }

            Assert.IsTrue(_entityList.Any());
        }

        [TestMethod()]
        public void SelecionarTest()
        {
            SalvarEntidadeNoRepositorio();

            var entity = _entity;

            _entity = null;

            BuscarUmRapInscricaoNoRepositorio(entity);

            RemoverEntidadeDoRepositorio(entity);

            Assert.IsNotNull(_entity);
        }

        [TestMethod()]
        public void AssinaturasTest()
        {
            SalvarEntidadeNoRepositorio();

            var assinatura = _rapInscricaoService.Assinaturas(_entity);

            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(assinatura);
        }


        [TestMethod()]
        public void SalvarTodosOsCamposPreenchidos()
        {
                var protrietesInfo = _entity.GetType().GetProperties();

            foreach (var propertyInfo in protrietesInfo)
            {
                if (new List<string> { "Normal" , "Estorno" }.Contains(propertyInfo.Name)) continue;

                if (propertyInfo.Name == "CodigoNaturezaItem")
                    propertyInfo.SetValue(_entity,"1");


                if (propertyInfo.GetValue(_entity) is int)
                    if ((int)propertyInfo.GetValue(_entity) == default(int))
                        propertyInfo.SetValue(_entity, 1);

                if (propertyInfo.GetValue(_entity) is bool)
                    if ((bool)propertyInfo.GetValue(_entity) == default(bool))
                        propertyInfo.SetValue(_entity, true);


                if (propertyInfo.GetValue(_entity) is decimal)
                    if ((decimal)propertyInfo.GetValue(_entity) == default(decimal))
                        propertyInfo.SetValue(_entity, 1M);


                if (propertyInfo.GetValue(_entity) is DateTime)
                    if ((DateTime)propertyInfo.GetValue(_entity) == default(DateTime))
                        propertyInfo.SetValue(_entity, DateTime.Now);
                
                    if (propertyInfo.GetValue(_entity) == null)
                        propertyInfo.SetValue(_entity, "V");
            }

            SalvarEntidadeNoRepositorio();


            RemoverEntidadeDoRepositorio(_entity);

            Assert.IsNotNull(_entity);
        }

        [TestMethod()]
        public void TransmitirTest()
        {
            try
            {
                int id = SalvarEntidadeNoRepositorio();

                _rapInscricaoService.Transmitir(id, new Usuario {Codigo = 1, RegionalId = 16}, 0,0);
            }
            finally
            {
                RemoverEntidadeDoRepositorio(_entity);
            }
        }

        [TestMethod()]
        public void TransmitirTest1()
        {

        }
        private void CreateInstance()
        {
            _model1 = CreateInstanceEmpenho("139900009", "32", "11", "139900047", 100, null, null, null, 0, "2013NE00001 N.F.000001-06/13 2013NL00001");
            _model2 = CreateInstanceEmpenho("169900049", "20", "11", null, 30311111, "004", "R", null, 0, "2016NE00001                  2016NL00001");
            _model3 = CreateInstanceEmpenho("169900047", "20", "11", null, 0, null, null, "06065569828", 0, "2013NE00001 N.F.000001-06/13 2013NL00001");
            _model4 = CreateInstanceEmpenho("169900049", "20", "11", "179900001", 4444444, null, null, null, 0, "2016NE00001 N.F.123456-09/13 2016NL00001");
        }
        private RapInscricao CreateInstanceEmpenho(string empenho, string tarefa, string despesa, string recibo, int valor, string medicao, string natureza, string CFP, int org, string referencia)
        {
            RapInscricao model = new RapInscricao
            {
                NumeroOriginalProdesp = empenho,
                CodigoTarefa = tarefa,
                CodigoDespesa = despesa,
                ValorRealizado = 100,
                NumeroRecibo = recibo,
                CodigoNotaFiscalProdesp = null,
                CodigoEspecificacaoDespesa = "000",
                Referencia = referencia,
                NumeroProcesso = "DER",
                DescricaoAutorizadoSupraFolha = "test",
                CodigoAutorizadoAssinatura = "12345",
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                CodigoExaminadoAssinatura = "11111",
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                CodigoResponsavelAssinatura = "88888",
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NumeroMedicao = medicao,
                NumeroCNPJCPFFornecedor = CFP,
                CodigoCredorOrganizacao = org,
                DataRealizado = recibo == null ? DateTime.Now : default(DateTime),
                DescricaoEspecificacaoDespesa1 = "TEste"




            };

            return model;
        }


        private static void CriarNovaEntidade()
        {
            _entity = new RapInscricao();

            _entity.DataEmissao = DateTime.Now;
            _entity.DataCadastro = DateTime.Now;
            _entity.CodigoUnidadeGestora = "162101";
            _entity.CodigoGestao = "16055";
            _entity.NumeroCNPJCPFCredor = "00000028000129";
            _entity.DescricaoObservacao1 = "apropriacao para atender despesas referentes a ...";
            _entity.RegionalId = 16;
            _entity.ProgramaId = 1;
            _entity.NaturezaId = 1;
            _entity.TipoServicoId = 1;
            _entity.CodigoNaturezaItem = "30";
            _entity.TransmitirProdesp = true;
            _entity.TransmitirSiafem = true;
            _entity.NumeroOriginalProdesp = "169900029";
            _entity.NumeroOriginalSiafemSiafisico = "2017NE00010";
            _entity.DescricaoAutorizadoSupraFolha = "test";
            _entity.NumeroProcesso = "test";

            _entity.DescricaoUsoAutorizadoPor = "test";

            _entity.CodigoAutorizadoAssinatura = "11111";
            _entity.CodigoAutorizadoGrupo = 1;
            _entity.CodigoAutorizadoOrgao = "99";

            _entity.CodigoExaminadoAssinatura = "12345";
            _entity.CodigoExaminadoGrupo = 1;
            _entity.CodigoExaminadoOrgao = "99";

            _entity.CodigoResponsavelAssinatura = "88888";
            _entity.CodigoResponsavelGrupo = 1;
            _entity.CodigoResponsavelOrgao = "00";


            CriarNovaListaDeItensParaAEntidade(ref _entity);
            CriarNovaListaDeNotasFiscaisParaAEntidade(ref _entity);
            CriarNovaListaDeEventosParaAEntidade(ref _entity);
        }

        private static void CriarNovaListaDeNotasFiscaisParaAEntidade(ref RapInscricao entity)
        {
            entity.Notas = new List<LiquidacaoDespesaNota>
            {
                new LiquidacaoDespesaNota { SubempenhoId = entity.Id, CodigoNotaFiscal = "nada consta", Ordem = 1 }
            };
        }
        private static void CriarNovaListaDeItensParaAEntidade(ref RapInscricao entity)
        {
            entity.Itens = new List<LiquidacaoDespesaItem>
            {
                new LiquidacaoDespesaItem { SubempenhoId = entity.Id, CodigoItemServico = "0821-4", QuantidadeMaterialServico = 1M, CodigoUnidadeFornecimentoItem = "", StatusSiafisicoItem = "N", SequenciaItem = 1 }
            };
        }
        private static void CriarNovaListaDeEventosParaAEntidade(ref RapInscricao entity)
        {
            entity.Eventos = new List<LiquidacaoDespesaEvento>
            {
                new LiquidacaoDespesaEvento { SubempenhoId = entity.Id, NumeroEvento = "510600", Fonte = "004001001", InscricaoEvento = "2017NE00015", ValorUnitario = 10 }
            };
        }


        private int SalvarEntidadeNoRepositorio()
        {
            _entity.Id = _rapInscricaoService.SalvarOuAlterar(_entity, 0, 1);
            return _entity.Id;
        }
        private AcaoEfetuada RemoverEntidadeDoRepositorio(RapInscricao entity)
        {
            return _rapInscricaoService.Excluir(entity, 0, (short)EnumAcao.Excluir);
        }
        private void ListarRapInscricaoNoRepositorio(RapInscricao entity)
        {
            _entityList = _rapInscricaoService.Listar(entity);
        }
        private void BuscarUmRapInscricaoNoRepositorio(RapInscricao entity)
        {
            _entity = _rapInscricaoService.Selecionar(entity.Id);
        }
        private void BuscarGridRapInscricaoNoRepositorio(RapInscricao entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            _entityList = _rapInscricaoService.BuscarGrid(entity, de, ate);
        }

    }
}