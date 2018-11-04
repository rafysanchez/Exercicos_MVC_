using System.Reflection;

namespace Sids.Prodesp.Infrastructure.Services.Reserva
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.Interface.Base;
    using Model.Interface.Reserva;
    using Model.Interface.Service.Reserva;
    using Model.ValueObject.Service.Prodesp.Common;
    using Model.ValueObject.Service.Prodesp.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Caching;

    public class ProdespReservaWs : IProdespReserva
    {
        public string InserirReserva(string key, string password, Reserva entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional)
        {
            try
            {
                var result = DataHelperProdespReserva.Procedure_InclusaoReserva(
                    key, password, entity, months, program, structure, source, regional)
                    ?? new ProdespReserva.Procedure_InclusaoReservaRecordType[] { };

                var resultItem = result.FirstOrDefault();

                HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception("Prodesp - " + resultItem?.outErro);

                return resultItem?.outDocsNumeroReserva;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }

        public bool InserirDoc(string key, string password, IReserva entity, string type)
        {
            try
            {
                var result = DataHelperProdespReserva.Procedure_InclusaoDocSIAFEM(
                    key, password, entity.NumSiafemSiafisico, entity.NumProdesp, type)
                    ?? new ProdespReserva.Procedure_InclusaoDocSIAFEMRecordType[] { };

                var resultItem = result.FirstOrDefault();

                HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception(resultItem?.outErro);

                return !string.IsNullOrEmpty(resultItem?.outSucesso);
            }
            catch
            {
                throw;
            }
        }

        public string InserirReservaReforco(string key, string password, ReservaReforco entity, IEnumerable<IMes> months, Regional regional)
        {
            try
            {
                var result = DataHelperProdespReserva.Procedure_ReservaReforco(
                    key, password, entity, months)
                    ?? new ProdespReserva.Procedure_ReforcoReservaRecordType[] { };

                var resultItem = result.FirstOrDefault();

                HttpContext.Current.Cache.Insert(
                    "terminal", resultItem?.outTerminal, null,
                    Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(1));

                HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outSucesso;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public string InserirReservaCancelamento(string key, string password, ReservaCancelamento entity, IEnumerable<IMes> months)
        {
            try
            {
                var result = DataHelperProdespReserva.Procedure_AnulacaoReserva(
                    key, password, entity, months)
                    ?? new ProdespReserva.Procedure_AnulacaoReservaRecordType[] { };

                var resultItem = result.FirstOrDefault();

                HttpContext.Current.Cache.Insert(
                    "terminal", resultItem?.outTerminal, null,
                    Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(1));

                HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outSucesso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public ConsultaContrato ConsultaContrato(string key, string password, string contract, string type = null)
        {
            object[] result;

            result = ConsultarContrato(key, password, contract, type);

            var resultItem = result.FirstOrDefault();

            var propriedades = resultItem.GetType().GetProperties().ToList();

            ValidarContrato(propriedades, resultItem);

            return new ConsultaContrato
            {
                OutContrato = propriedades.FirstOrDefault(x => x.Name == "outContrato")?.GetValue(resultItem).ToString().Replace(" ","."),
                OutCpfcnpj = propriedades.FirstOrDefault(x => x.Name == "outCPFCNPJ")?.GetValue(resultItem).ToString(),
                OutCodObra = propriedades.FirstOrDefault(x => x.Name == "outCodObra")?.GetValue(resultItem).ToString(),
                OutContratada = propriedades.FirstOrDefault(x => x.Name == "outContratada")?.GetValue(resultItem).ToString(),
                OutObjeto = propriedades.FirstOrDefault(x => x.Name == "outObjeto")?.GetValue(resultItem).ToString(),
                OutProcesSiafem = propriedades.FirstOrDefault(x => x.Name == "outProcesSiafem")?.GetValue(resultItem).ToString(),
                OutPrograma = propriedades.FirstOrDefault(x => x.Name == "outPrograma")?.GetValue(resultItem).ToString(),
                OutCED = propriedades.FirstOrDefault(x => x.Name == "outCED")?.GetValue(resultItem).ToString(),
                OutTipo = propriedades.FirstOrDefault(x=>x.Name == "outTipo")?.GetValue(resultItem).ToString(),

                ListConsultaContrato = result.Select(x => new InfoConsultaContrato
                {
                    OutData = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outData")?.GetValue(x).ToString(),
                    OutEvento = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outEvento")?.GetValue(x).ToString(),
                    OutNumero = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outNumero")?.GetValue(x).ToString(),
                    OutValor = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outValor")?.GetValue(x).ToString()
                }).ToList()
            };
        }

        private static void ValidarContrato(List<PropertyInfo> propriedades, object resultItem)
        {
            if (!string.IsNullOrEmpty(propriedades.FirstOrDefault(x => x.Name == "outErro")?.GetValue(resultItem).ToString()))
                throw new Exception($"Prodesp - {propriedades.FirstOrDefault(x => x.Name == "outErro")?.GetValue(resultItem)}");
        }

        private static object[] ConsultarContrato(string key, string password, string contract, string type)
        {
            object[] result;
            object[] resulRap;
           

            switch (type)
            {
                case null:
                case "0":
                    result = DataHelperProdespReserva.Procedure_ConsultaContrato(key, password, contract);
                    break;
                case "0304":
                    type = "3";
                    result = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaContrato(key, password, contract, type);

                    type = "4";
                    resulRap = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaContrato(key, password, contract, type);

                    result = result.Concat(resulRap).ToArray();
                    break;
                default:
                    result = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaContrato(key, password, contract, type);
                    break;
            }
            return result;
        }

        public ConsultaReservaEstrutura ConsultaReservaEstrutura(string key, string password, int year, string regional, string cfp, string nature, int program, string resourceSource, string process)
        {
            var result = DataHelperProdespReserva.Procedure_ConsultaReservaEstrutura(
                key, password, year, regional, cfp, nature, program, resourceSource, process)
                ?? new ProdespReserva.Procedure_ConsultaReservaEstruturaRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new Exception($"Prodesp - {resultItem?.outErro}");

            return new ConsultaReservaEstrutura
            {
                OutCodAplicacao = resultItem?.outCodAplicacao,
                OutDataInic = resultItem?.outDataInic,
                OutDispEmpenhar = resultItem?.outDispEmpenhar,
                OutNrReserva = resultItem?.outNrReserva,
                OutValorAtual = resultItem?.outValorAtual,
                OutValorEmpenhado = resultItem?.outValorEmpenhado,
                OutErro = resultItem?.outErro,
                OutCED = resultItem?.outCED,

                ListConsultaEstrutura = result.Select(x => new InfoConsultaReservaEstrutura
                {
                    OutNrReserva = x.outNrReserva,
                    OutCED = x.outCED,
                    OutCodAplicacao = x.outCodAplicacao,
                    OutDataInic = x.outDataInic,
                    OutValorAtual = x.outValorAtual,
                    OutValorEmpenhado = x.outValorEmpenhado,
                    OutDispEmpenhar = x.outDispEmpenhar

                }).ToList()
            };
        }

        public ConsultaReserva ConsultaReserva(string key, string password, string entity)
        {
            var result = DataHelperProdespReserva.Procedure_ConsultaReserva(
                key, password, entity)
                ?? new ProdespReserva.Procedure_ConsultaReservaRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (!string.IsNullOrEmpty(resultItem.outErro))
                throw new Exception("Prodesp - " + resultItem.outErro);

            return new ConsultaReserva
            {
                OutNumReserva = entity,
                OutCed1 = resultItem?.outCED_1,
                OutCed2 = resultItem?.outCED_2,
                OutCed3 = resultItem?.outCED_3,
                OutCed4 = resultItem?.outCED_4,
                OutCed5 = resultItem?.outCED_5,
                OutCfp1 = resultItem?.outCFP_1,
                OutCfp2 = resultItem?.outCFP_2,
                OutCfp3 = resultItem?.outCFP_3,
                OutCfp4 = resultItem?.outCFP_4,
                OutCfp5 = resultItem?.outCFP_5,
                OutCodAplicacao = resultItem?.outCodAplicacao,
                OutCodObra = resultItem?.outCodObra,
                OutDataEmissao = resultItem?.outDataEmissao,
                OutDestRecurso = resultItem?.outDestRecurso,
                OutErro = resultItem?.outErro,
                OutEspecDespesa1 = resultItem?.outEspecDespesa_1,
                OutEspecDespesa2 = resultItem?.outEspecDespesa_2,
                OutEspecDespesa3 = resultItem?.outEspecDespesa_3,
                OutEspecDespesa4 = resultItem?.outEspecDespesa_4,
                OutEspecDespesa5 = resultItem?.outEspecDespesa_5,
                OutEspecDespesa6 = resultItem?.outEspecDespesa_6,
                OutEspecDespesa7 = resultItem?.outEspecDespesa_7,
                OutEspecDespesa8 = resultItem?.outEspecDespesa_8,
                OutEspecDespesa9 = resultItem?.outEspecDespesa_9,
                OutIdentContrato = resultItem?.outIdentContrato,
                OutNumProcesso = resultItem?.outNumProcesso,
                OutPrevInic = resultItem?.outPrevInic,
                OutSaldoQ1 = resultItem?.outSaldoQ1,
                OutSaldoQ2 = resultItem?.outSaldoQ2,
                OutSaldoQ3 = resultItem?.outSaldoQ3,
                OutSaldoQ4 = resultItem?.outSaldoQ4,
                OutSaldoReserva = resultItem?.outSaldoReserva,
                OutSeqTam = resultItem?.outSeqTAM,
                OutSucesso = resultItem?.outSucesso,
                OutValorAnulado = resultItem?.outValorAnulado,
                OutValorEmpenhado = resultItem?.outValorEmpenhado,
                OutValorReforco = resultItem?.outValorReforco,
                OutValorReserva = resultItem?.outValorReserva
            };
        }

        public ConsultaEmpenho ConsultaEmpenho(string key, string password, string entity)
        {
            var result = DataHelperProdespReserva.Procedure_ConsultaEmpenho(
                key, password, entity)
                ?? new ProdespReserva.Procedure_ConsultaEmpenhoRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new Exception($"Prodesp - { resultItem?.outErro}");

            return new ConsultaEmpenho
            {
                NumEmpenho = entity,
                OutAutorizadorCargo = resultItem?.outAutorizadorCargo,
                OutAutorizadorNome = resultItem?.outAutorizadorNome,
                OutBairro = resultItem?.outBairro,
                OutCed1 = resultItem?.outCED_1,
                OutCed2 = resultItem?.outCED_2,
                OutCed3 = resultItem?.outCED_3,
                OutCed4 = resultItem?.outCED_4,
                OutCed5 = resultItem?.outCED_5,
                OutCep = resultItem?.outCEP,
                OutCfp1 = resultItem?.outCFP_1,
                OutCfp2 = resultItem?.outCFP_2,
                OutCfp3 = resultItem?.outCFP_3,
                OutCfp4 = resultItem?.outCFP_4,
                OutCfp5 = resultItem?.outCFP_5,
                OutCnpjCpf = resultItem?.outCnpjCpf,
                OutCnpjCpfTipo = resultItem?.outCnpjCpf_tipo,
                OutCodAplicacao = resultItem?.outCodAplicacao,
                OutCodObra = resultItem?.outCodObra,
                OutCredor = resultItem?.outCredor,
                OutDataEmissao = resultItem?.outDataEmissao,
                OutDestRecurso = resultItem?.outDestRecurso,
                OutEndereco = resultItem?.outEndereco,
                OutErro = resultItem?.outErro,
                OutEspecDespesa1 = resultItem?.outEspecDespesa_1,
                OutEspecDespesa2 = resultItem?.outEspecDespesa_2,
                OutEspecDespesa3 = resultItem?.outEspecDespesa_3,
                OutEspecDespesa4 = resultItem?.outEspecDespesa_4,
                OutEspecDespesa5 = resultItem?.outEspecDespesa_5,
                OutEspecDespesa6 = resultItem?.outEspecDespesa_6,
                OutEspecDespesa7 = resultItem?.outEspecDespesa_7,
                OutEspecDespesa8 = resultItem?.outEspecDespesa_8,
                OutEspecDespesa9 = resultItem?.outEspecDespesa_9,
                OutEstado = resultItem?.outEstado,
                OutExaminadorCargo = resultItem?.outExaminadorCargo,
                OutExaminadorNome = resultItem?.outExaminadorNome,
                OutFolhas = resultItem?.outFolhas,
                OutFonteSiafem = resultItem?.outFonteSIAFEM,
                OutMunicipio = resultItem?.outMunicipio,
                OutNeSiafem = resultItem?.outNESiafem,
                OutNumContrato = resultItem?.outNumContrato,
                OutNumProcesso = resultItem?.outNumProcesso,
                OutOrigemRecurso = resultItem?.outOrigemRecurso,
                OutPrevInic = resultItem?.outPrevInic,
                OutSaldoEmpenho = resultItem?.outSaldoEmpenho,
                OutSaldoQ1 = resultItem?.outSaldoQ1,
                OutSaldoQ2 = resultItem?.outSaldoQ2,
                OutSaldoQ3 = resultItem?.outSaldoQ3,
                OutSaldoQ4 = resultItem?.outSaldoQ4,
                OutSeqTam = resultItem?.outSeqTAM,
                OutSucesso = resultItem?.outSucesso,
                OutTerminal = resultItem?.outTerminal,
                OutValorAnulado = resultItem?.outValorAnulado,
                OutValorEmpenho = resultItem?.outValorEmpenho,
                OutValorReforco = resultItem?.outValorReforco,
                OutValorSubEmpenhado = resultItem?.outValorSubEmpenhado
            };
        }

        public ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string key, string password, string specification)
        {
            var result = DataHelperProdespReserva.Procedure_ConsultaEspecificacaoDespesa(
                key, password, specification)
                ?? new ProdespReserva.Procedure_ConsultaEspecificacoesRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (resultItem?.outErro != "")
                throw new Exception($"Prodesp - {resultItem?.outErro}");

            if (resultItem?.outCodigoEspecificacaoDesp == specification)
            {
                return new ConsultaEspecificacaoDespesa
                {
                    outFimTransacao = resultItem?.outFimTransacao,
                    outErro = resultItem?.outErro,
                    outCodigoEspecificacaoDesp = resultItem?.outCodigoEspecificacaoDesp,
                    outEspecDespesa = resultItem?.outEspecDespesa,
                    outSucesso = resultItem?.outSucesso,
                    outEspecDespesa_02 = resultItem?.outEspecDespesa_02,
                    outEspecDespesa_03 = resultItem?.outEspecDespesa_03,
                    outEspecDespesa_04 = resultItem?.outEspecDespesa_04,
                    outEspecDespesa_05 = resultItem?.outEspecDespesa_05,
                    outEspecDespesa_06 = resultItem?.outEspecDespesa_06,
                    outEspecDespesa_07 = resultItem?.outEspecDespesa_07,
                    outEspecDespesa_08 = resultItem?.outEspecDespesa_08,
                    outEspecDespesa_09 = resultItem?.outEspecDespesa_09
                };
            }

            throw new Exception("Código de Especificação não Cadastrado.");
        }

        public ConsultaAssinatura ConsultaAssinatura(string key, string password, string signatures, int type)
        {
            var result = DataHelperProdespReserva.Procedure_ConsultaAssinaturas(
                key, password, signatures, type)
                ?? new ProdespReserva.Procedure_ConsultaAssinaturasRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new Exception($"Prodesp - {resultItem?.outErro}");

            return new ConsultaAssinatura
            {
                outGrupoAutorizador = resultItem?.outGrupoAutorizador,
                outOrgaoAutorizador = resultItem?.outOrgaoAutorizador,
                outNomeAutorizador = resultItem?.outNomeAutorizador,
                outCargoAutorizador = resultItem?.outCargoAutorizador,

                outGrupoExaminador = resultItem?.outGrupoExaminador,
                outOrgaoExaminador = resultItem?.outOrgaoExaminador,
                outNomeExaminador = resultItem?.outNomeExaminador,
                outCargoExaminador = resultItem?.outCargoExaminador,

                outGrupoResponsavel = resultItem?.outGrupoResponsavel,
                outOrgaoResponsavel = resultItem?.outOrgaoResponsavel,
                outNomeResponsavel = resultItem?.outNomeResponsavel,
                outCargoResponsavel = resultItem?.outCargoResponsavel
            };
        }
    }
}
