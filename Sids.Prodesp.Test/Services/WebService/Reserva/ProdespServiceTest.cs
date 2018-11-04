using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Infrastructure.ProdespReserva;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Reserva;

namespace Sids.Prodesp.Test.Services.WebServie.Reserva
{
    [TestClass]
    public class ProdespServiceTest
    {
        [TestMethod]
        public void TestarInclusaoReservaProdespWs()
        {
            var programa = new Programa { Ptres = "165501", Cfp = "261220100490804" };
            var fonte = new Fonte { Codigo = "001001001" };
            var estrutura = new Estrutura { Natureza = "319011", Aplicacao = "1600205" };
            var reservaMes = new List<ReservaMes>
            {
                new ReservaMes {ValorMes = 1, Descricao = "01"},
                new ReservaMes {ValorMes = 2, Descricao = "04"},
                new ReservaMes {ValorMes = 3, Descricao = "07"},
                new ReservaMes {ValorMes = 4, Descricao = "10"}
            };

            Model.Entity.Reserva.Reserva reserva = new Model.Entity.Reserva.Reserva
            {
                AnoExercicio = 2016,
                AnoReferencia = 2016,
                Regional = 3,
                AutorizadoAssinatura = "11111",
                AutorizadoGrupo = "1",
                AutorizadoOrgao = "99",
                AutorizadoSupraFolha = "fl01",
                DestinoRecurso = "04",
                ExaminadoAssinatura = "12345",
                ExaminadoGrupo = "1",
                ExaminadoOrgao = "99",
                Processo = "teste App",
                OrigemRecurso = fonte.Codigo.Substring(1, 2),
                ResponsavelAssinatura = "88888",
                ResponsavelGrupo = "1",
                ResponsavelOrgao = "00",
                EspecificacaoDespesa = "001",
                DescEspecificacaoDespesa = "TESTE",
                DataEmissao = DateTime.Parse("15/02/2016"),
                Uo = "16055",
                Ugo = "122101",
                Oc = "00259",
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23"
            };

            var reservaFiltersType = GerarReservaFiltersType(reserva, reservaMes, programa, estrutura, fonte);

            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            var ressult = prodespWs.Procedure_InclusaoReserva(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());

            Assert.IsNotNull(ressult);
        }

        [TestMethod]
        public void TransmitirReforco()
        {
            var fonte = new Fonte { Codigo = "004001001" };

            var reforcoMes = new List<ReservaReforcoMes>
            {
                new ReservaReforcoMes { ValorMes =(decimal) 1.00, Descricao= "11"}
            };

            var reforco = new ReservaReforco
            {
                Reserva = 169900444,
                AnoExercicio = 2016,
                Regional = 3,
                AutorizadoAssinatura = "11111",
                AutorizadoGrupo = "1",
                AutorizadoOrgao = "99",
                AutorizadoSupraFolha = "fl01",
                DestinoRecurso = "24",
                ExaminadoAssinatura = "12345",
                ExaminadoGrupo = "1",
                ExaminadoOrgao = "99",
                Processo = "teste App",
                OrigemRecurso = fonte.Codigo.Substring(1, 2),
                ResponsavelAssinatura = "88888",
                ResponsavelGrupo = "1",
                ResponsavelOrgao = "00",
                EspecificacaoDespesa = "001",
                DescEspecificacaoDespesa = "TESTE",
                DataEmissao = DateTime.Parse("15/02/2016"),
                Uo = "16055",
                Ugo = "122101",
                Oc = "00259",
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23"

            };


            var reforcoFiltersType = GerarReforcoFiltersType("SIDS000199", "DERSIAFEM22716", reforco, reforcoMes);
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            var ressult = prodespWs.Procedure_ReforcoReserva(reforcoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());

            Assert.IsNotNull(ressult);

        }


        [TestMethod]
        public void TestarInclusaoCancelamentoProdespWs()
        {
            var fonte = new Fonte { Codigo = "004001001" };
            var cancelamentoMes = new List<ReservaCancelamentoMes>
            {
                new ReservaCancelamentoMes {ValorMes = (decimal) 1.00, Descricao = "11"}
            };
            var cancelamento = new ReservaCancelamento
            {
                Reserva = 169900444,
                AnoExercicio = 2016,
                Regional = 3,
                AutorizadoAssinatura = "11111",
                AutorizadoGrupo = "1",
                AutorizadoOrgao = "99",
                AutorizadoSupraFolha = "fl01",
                DestinoRecurso = "24",
                ExaminadoAssinatura = "12345",
                ExaminadoGrupo = "1",
                ExaminadoOrgao = "99",
                Processo = "teste App",
                OrigemRecurso = fonte.Codigo.Substring(1, 2),
                ResponsavelAssinatura = "88888",
                ResponsavelGrupo = "1",
                ResponsavelOrgao = "00",
                EspecificacaoDespesa = "001",
                DescEspecificacaoDespesa = "TESTE",
                DataEmissao = DateTime.Parse("15/02/2016"),
                Uo = "16055",
                Ugo = "122101",
                Oc = "00259",
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23"
            };
            var reservaFiltersType = GerarAnulacaoFiltersType("SIDS000199", "DERSIAFEM22716", cancelamento, cancelamentoMes);
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();
            var ressult = prodespWs.Procedure_AnulacaoReserva(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());

            Assert.IsNotNull(ressult);
        }

        [TestMethod]
        public void TestarInclusaoDocProdespWs()
        {
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();
            var ressult = prodespWs.Procedure_InclusaoDocSIAFEM(GerarDocSiafemFiltersType(), new ModelVariablesType(), new EnvironmentVariablesType());

            Assert.IsNotNull(ressult);
        }

        [TestMethod]
        public void TestarConsultaContratoProdespWs()
        {
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();
            var ressult = prodespWs.Procedure_ConsultaContrato(GerarContratoFiltersType(), new ModelVariablesType(), new EnvironmentVariablesType()).ToList();
            var infoConsultaContrato = ressult.Select(x => new InfoConsultaContrato
            {
                OutData = x.outData,
                OutEvento = x.outEvento,
                OutNumero = x.outNumero,
                OutValor = x.outValor
            }).ToList();
            var consultaContrato = new ConsultaContrato
            {
                OutContrato = ressult[0].outContrato,
                OutCpfcnpj = ressult[0].outCPFCNPJ,
                OutCodObra = ressult[0].outCodObra,
                OutContratada = ressult[0].outContratada,
                OutObjeto = ressult[0].outObjeto,
                OutProcesSiafem = ressult[0].outProcesSiafem,
                OutPrograma = ressult[0].outPrograma,
                ListConsultaContrato = infoConsultaContrato
            };

            Assert.IsNotNull(consultaContrato);
        }

        [TestMethod]
        public void TestarConsultaReservaProdespWs()
        {
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            var ressult = prodespWs.Procedure_ConsultaReserva(GerarReservaFiltersType(), new ModelVariablesType(), new EnvironmentVariablesType()).ToList();


            Assert.IsNotNull(ressult);
        }

        [TestMethod]
        public void TestarConsultaEspecificacaoDesp()
        {
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            var ressult = prodespWs.Procedure_ConsultaEspecificacoes(GerarEspecificacaoFiltersType(), new ModelVariablesType(), new EnvironmentVariablesType()).ToList();


            Assert.IsNotNull(ressult);
        }

        [TestMethod]
        public void TestarConsultaEmpenhoProdespWs()
        {
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            var result = prodespWs.Procedure_ConsultaEmpenho(GerarEmpenhoFiltersType(), new ModelVariablesType(), new EnvironmentVariablesType()).ToList()[0];
            var consulta = new ConsultaReserva
            {
                OutNumReserva = "149900001",
                OutCed1 = result.outCED_1,
                OutCed2 = result.outCED_2,
                OutCed3 = result.outCED_3,
                OutCed4 = result.outCED_4,
                OutCed5 = result.outCED_5,
                OutCfp1 = result.outCFP_1,
                OutCfp2 = result.outCFP_2,
                OutCfp3 = result.outCFP_3,
                OutCfp4 = result.outCFP_4,
                OutCfp5 = result.outCFP_5,
                OutCodAplicacao = result.outCodAplicacao,
                OutCodObra = result.outCodObra,
                OutDataEmissao = result.outDataEmissao,
                OutDestRecurso = result.outDestRecurso,
                OutErro = result.outErro,
                OutEspecDespesa1 = result.outEspecDespesa_1,
                OutEspecDespesa2 = result.outEspecDespesa_2,
                OutEspecDespesa3 = result.outEspecDespesa_3,
                OutEspecDespesa4 = result.outEspecDespesa_4,
                OutEspecDespesa5 = result.outEspecDespesa_5,
                OutEspecDespesa6 = result.outEspecDespesa_6,
                OutEspecDespesa7 = result.outEspecDespesa_7,
                OutEspecDespesa8 = result.outEspecDespesa_8,
                OutEspecDespesa9 = result.outEspecDespesa_9,
                //OutIdentContrato = result.outIdentContrato,
                OutNumProcesso = result.outNumProcesso,
                OutPrevInic = result.outPrevInic,
                OutSaldoQ1 = result.outSaldoQ1,
                OutSaldoQ2 = result.outSaldoQ2,
                OutSaldoQ3 = result.outSaldoQ3,
                OutSaldoQ4 = result.outSaldoQ4,
                // OutSaldoReserva = result.outSaldoReserva,
                OutSeqTam = result.outSeqTAM,
                OutSucesso = result.outSucesso,
                OutValorAnulado = result.outValorAnulado,
                //OutValorEmpenhado = result.outValorEmpenhado,
                OutValorReforco = result.outValorReforco,
                //OutValorReserva = result.outValorReserva
            };

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestarConsultaPorEstruturaProdespWs()
        {
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            var resultado = prodespWs.Procedure_ConsultaReservaEstrutura(GerarReservaEstruturaFiltersType(), new ModelVariablesType(), new EnvironmentVariablesType()).ToList();

            var estrutura = resultado.Select(x => new ConsultaReservaEstrutura
            {

                OutCodAplicacao = x.outCodAplicacao,
                OutDataInic = x.outDataInic,
                OutDispEmpenhar = x.outDispEmpenhar,
                OutNrReserva = x.outNrReserva,
                OutValorAtual = x.outValorAtual,
                OutValorEmpenhado = x.outValorEmpenhado,
                OutErro = x.outErro,
                // OutCED = x.
            }).ToList();


            ConsultaReservaEstrutura consultaReservaEstrutura = new ConsultaReservaEstrutura
            {
                OutCodAplicacao = resultado[0].outCodAplicacao,
                OutDataInic = resultado[0].outDataInic,
                OutDispEmpenhar = resultado[0].outDispEmpenhar,
                OutNrReserva = resultado[0].outNrReserva,
                OutValorAtual = resultado[0].outValorAtual,
                OutValorEmpenhado = resultado[0].outValorEmpenhado,
                OutErro = resultado[0].outValorEmpenhado
            };
            Assert.IsNotNull(consultaReservaEstrutura);
        }


        #region Metodos Privados
        private static Procedure_InclusaoReservaFiltersType GerarReservaFiltersType(Model.Entity.Reserva.Reserva reserva, List<ReservaMes> reservaMes, Programa programa, Estrutura estrutura, Fonte fonte)
        {
            var regional = App.RegionalService.Buscar(new Regional()).ToList();
            var reservaFiltersType = new Procedure_InclusaoReservaFiltersType
            {
                inOperador = "roque   00",
                inChave = "manurafa2016",

                inAnoRefRes = reserva.AnoReferencia.ToString().Substring(2, 2),
                inCFPRes_1 = programa.Cfp.Substring(0, 2),
                inCFPRes_2 = programa.Cfp.Substring(2, 3),
                inCFPRes_3 = programa.Cfp.Substring(5, 4),
                inCFPRes_4 = programa.Cfp.Substring(9, 4),
                inCFPRes_5 = programa.Cfp.Substring(13, 2) + "00",

                inCEDRes_1 = estrutura.Natureza.ToString().Substring(0, 1),
                inCEDRes_2 = estrutura.Natureza.ToString().Substring(1, 1),
                inCEDRes_3 = estrutura.Natureza.ToString().Substring(2, 1),
                inCEDRes_4 = estrutura.Natureza.ToString().Substring(3, 1),
                inCEDRes_5 = estrutura.Natureza.ToString().Substring(4, 2),

                inOrgao = regional.FirstOrDefault(x => x.Id == reserva.Regional).Descricao.Substring(2, 2),
                inCodAplicacaoRes = estrutura.Aplicacao.ToString(),
                inOrigemRecursoRes = reserva.OrigemRecurso,
                inDestinoRecursoRes = reserva.DestinoRecurso,
                inNumProcessoRes = reserva.Processo,
                inAutoProcFolhasRes = reserva.AutorizadoSupraFolha,
                inCodEspecDespesaRes = reserva.EspecificacaoDespesa,
                inEspecifDespesaRes = reserva.DescEspecificacaoDespesa,

                inAutoPorAssRes = reserva.AutorizadoAssinatura,
                inAutoPorGrupoRes = reserva.AutorizadoGrupo,
                inAutoPorOrgaoRes = reserva.AutorizadoOrgao,
                inExamPorAssRes = reserva.ExaminadoAssinatura,
                inExamPorGrupoRes = reserva.ExaminadoGrupo,
                inExamPorOrgaoRes = reserva.ExaminadoOrgao,
                inRespEmissaoAssRes = reserva.ResponsavelAssinatura,
                inRespEmissGrupoRes = reserva.ResponsavelGrupo,
                inRespEmissOrgaoRes = reserva.ResponsavelOrgao,


                inIdentContratoANORes = reserva.Contrato == null ? "" : reserva.Contrato.ToString().Substring(0, 2),
                inIdentContratoORGAORes = reserva.Contrato == null ? "" : reserva.Contrato.ToString().Substring(2, 2),
                inIdentContratoNUMRes = reserva.Contrato == null ? "" : reserva.Contrato.ToString().Substring(4, 5),
                inIdentContratoDCRes = reserva.Contrato == null ? "" : reserva.Contrato.ToString().Substring(9, 1),

                inImprimirRes = "N",
                inQuotaReserva_1 = reservaMes.Where(x => x.Descricao.Contains("01") || x.Descricao.Contains("02") || x.Descricao.Contains("03")).Sum(y => y.ValorMes).ToString(),
                inQuotaReserva_2 = reservaMes.Where(x => x.Descricao.Contains("04") || x.Descricao.Contains("05") || x.Descricao.Contains("06")).Sum(y => y.ValorMes).ToString(),
                inQuotaReserva_3 = reservaMes.Where(x => x.Descricao.Contains("07") || x.Descricao.Contains("08") || x.Descricao.Contains("09")).Sum(y => y.ValorMes).ToString(),
                inQuotaReserva_4 = reservaMes.Where(x => x.Descricao.Contains("10") || x.Descricao.Contains("11") || x.Descricao.Contains("12")).Sum(y => y.ValorMes).ToString(),
                inTotalReserva = reservaMes.Sum(x => x.ValorMes).ToString()
            };

            return reservaFiltersType;
        }

        private static Procedure_InclusaoDocSIAFEMFiltersType GerarDocSiafemFiltersType()
        {
            return new Procedure_InclusaoDocSIAFEMFiltersType
            {
                inOperador = "SIDS000100",
                inChave = "DERSIAFEM22716",
                inDocSIAFEM = "2016NR00023",
                inNumeroProdesp = "169900452",
                inTipo = "07"
            };
        }

        private static Procedure_ConsultaContratoFiltersType GerarContratoFiltersType()
        {
            return new Procedure_ConsultaContratoFiltersType
            {
                inOperador = "roque   00",
                inChave = "manurafa2016",
                inIdentContratoAno = "12",
                inIdentContratoNum = "17777",
                inIdentContratoOrgao = "99"
            };
        }

        private static Procedure_ConsultaReservaEstruturaFiltersType GerarReservaEstruturaFiltersType()
        {
            return new Procedure_ConsultaReservaEstruturaFiltersType
            {
                inOperador = "roque   00",
                inChave = "manurafa_2016",
                inAno = "16",
                inCED_1 = "3",
                inCED_2 = "1",
                inCED_3 = "9",
                inCED_4 = "0",
                inCED_5 = "11",
                inCFP_1 = "26",
                inCFP_2 = "122",
                inCFP_3 = "0100",
                inCFP_4 = "4908",
                inCFP_5 = "0400",
                inOrgao = "01",
                inOrigemRecurso = "04"
            };
        }

        private static Procedure_ConsultaReservaFiltersType GerarReservaFiltersType()
        {
            return new Procedure_ConsultaReservaFiltersType
            {
                inOperador = "roque   00",
                inChave = "manurafa_2016",
                inNumReserva = "169900112"
            };
        }

        private static Procedure_ConsultaEspecificacoesFiltersType GerarEspecificacaoFiltersType()
        {
            return new Procedure_ConsultaEspecificacoesFiltersType
            {
                inOperador = "roque   00",
                inChave = "manurafa_2016",
                inConsultaEspecificDesp = "123"
            };
        }

        private static Procedure_ConsultaEmpenhoFiltersType GerarEmpenhoFiltersType()
        {
            return new Procedure_ConsultaEmpenhoFiltersType
            {
                inOperador = "roque   00",
                inChave = "manurafa_2016",
                inNumEmpenho = "149900001"
            };
        }

        private static Procedure_ReforcoReservaFiltersType GerarReforcoFiltersType(string chave, string senha, ReservaReforco reforco, List<ReservaReforcoMes> reforcoMese)
        {
            var reforcoFiltersType = new Procedure_ReforcoReservaFiltersType
            {
                inChave = senha,
                inOperador = chave,
                inNumReserva = reforco.Reserva.ToString(),
                inQuotaReforco_1 = reforcoMese.Where(x => x.Descricao.Contains("01") || x.Descricao.Contains("02") || x.Descricao.Contains("03")).Sum(y => y.ValorMes).ToString(),
                inQuotaReforco_2 = reforcoMese.Where(x => x.Descricao.Contains("04") || x.Descricao.Contains("05") || x.Descricao.Contains("06")).Sum(y => y.ValorMes).ToString(),
                inQuotaReforco_3 = reforcoMese.Where(x => x.Descricao.Contains("07") || x.Descricao.Contains("08") || x.Descricao.Contains("09")).Sum(y => y.ValorMes).ToString(),
                inQuotaReforco_4 = reforcoMese.Where(x => x.Descricao.Contains("10") || x.Descricao.Contains("11") || x.Descricao.Contains("12")).Sum(y => y.ValorMes).ToString(),
                inTotalReforco = reforcoMese.Sum(x => x.ValorMes).ToString(),
                inAutoPorAssRef = reforco.AutorizadoAssinatura,
                inAutoPorGrupoRef = reforco.AutorizadoGrupo,
                inAutoPorOrgaoRef = reforco.AutorizadoOrgao,
                inAutoProcFolhasRef = reforco.AutorizadoSupraFolha,
                inDestinoRecursoRef = reforco.DestinoRecurso,
                inExamPorAssRef = reforco.ExaminadoAssinatura,
                inExamPorGrupoRef = reforco.ExaminadoGrupo,
                inExamPorOrgaoRef = reforco.ExaminadoOrgao,
                inNumProcessoRef = reforco.Processo,
                InOrigemRecursoRef = reforco.OrigemRecurso,
                inCodEspecDespesaRef = reforco.EspecificacaoDespesa,
                inEspecifiDespesaRef = reforco.DescEspecificacaoDespesa,
                inRespEmissGrupoRef = reforco.ResponsavelGrupo,
                inRespEmissaoAssRef = reforco.ResponsavelAssinatura,
                inRespEmissOrgaoRef = reforco.ResponsavelOrgao
            };

            return reforcoFiltersType;
        }

        private static Procedure_AnulacaoReservaFiltersType GerarAnulacaoFiltersType(string chave, string senha, ReservaCancelamento cancelamento, List<ReservaCancelamentoMes> cancelamentoMese)
        {
            var reforcoFiltersType = new Procedure_AnulacaoReservaFiltersType
            {
                inChave = senha,
                inOperador = chave,
                inNumReserva = cancelamento.Reserva.ToString(),
                inQuotaAnulacao_1 = cancelamentoMese.Where(x => x.Descricao.Contains("01") || x.Descricao.Contains("02") || x.Descricao.Contains("03")).Sum(y => y.ValorMes).ToString(),
                inQuotaAnulacao_2 = cancelamentoMese.Where(x => x.Descricao.Contains("04") || x.Descricao.Contains("05") || x.Descricao.Contains("06")).Sum(y => y.ValorMes).ToString(),
                inQuotaAnulacao_3 = cancelamentoMese.Where(x => x.Descricao.Contains("07") || x.Descricao.Contains("08") || x.Descricao.Contains("09")).Sum(y => y.ValorMes).ToString(),
                inQuotaAnulacao_4 = cancelamentoMese.Where(x => x.Descricao.Contains("10") || x.Descricao.Contains("11") || x.Descricao.Contains("12")).Sum(y => y.ValorMes).ToString(),
                inTotalAnulacao = cancelamentoMese.Sum(x => x.ValorMes).ToString(),
                inAutoPorAssAnu = cancelamento.AutorizadoAssinatura,
                inAutoPorGrupoAnu = cancelamento.AutorizadoGrupo,
                inAutoPorOrgaoAnu = cancelamento.AutorizadoOrgao,
                inAutoProcFolhasAnu = cancelamento.AutorizadoSupraFolha,
                inDestinoRecursoAnu = cancelamento.DestinoRecurso,
                inExamPorAssAnu = cancelamento.ExaminadoAssinatura,
                inExamPorGrupoAnu = cancelamento.ExaminadoGrupo,
                inExamPorOrgaoAnu = cancelamento.ExaminadoOrgao,
                inNumProcessoAnu = cancelamento.Processo,
                InOrigemRecursoAnu = cancelamento.OrigemRecurso,
                inCodEspecDespesaAnu = cancelamento.EspecificacaoDespesa,
                inEspecifiDespesaRef = cancelamento.DescEspecificacaoDespesa,
                inRespEmissGrupoAnu = cancelamento.ResponsavelGrupo,
                inRespEmissaoAssAnu = cancelamento.ResponsavelAssinatura,
                inRespEmissOrgaoAnu = cancelamento.ResponsavelOrgao
            };

            return reforcoFiltersType;
        }
        #endregion
    }
}
