namespace Sids.Prodesp.Infrastructure.Services
{
    using Helpers;
    using Interface.Interface.Service;
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.ValueObject.Service.Reserva.Prodesp;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProdespWs : IProdesp
    {
        public string InserirReserva(
            string chave, string senha, Reserva reserva, 
            List<IMes> mes, Programa programa, Estrutura estrutura, 
            Fonte fonte, Regional regional)
        {
            try
            {
                var result = DataHelperProdesp.Procedure_InclusaoReserva(
                    chave, senha, reserva, mes, programa, estrutura, fonte, regional)[0];

                //HttpContext.Current.Session["terminal"] = result.outTerminal;
                HttpContext.Current.Session["terminal"] = result.outTerminal;

                if (result.outErro != "")
                    throw new Exception("Prodesp - " + result.outErro);

                return result.outDocsNumeroReserva;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }

        public bool InserirDoc(string chave, string senha, IReserva reserva, string tipo)
        {
            try
            {
                var result = DataHelperProdesp.Procedure_InclusaoDocSIAFEM(chave, senha, reserva, tipo)[0];
                if (result.outErro != "")
                    throw new Exception(result.outErro);

                return result.outSucesso != "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InserirReservaReforco(
            string chave, string senha, ReservaReforco reforco, 
            List<IMes> reforcoMes, Regional regional)
        {
            try
            {
                var result = DataHelperProdesp.Procedure_ReservaReforco(chave, senha, reforco, reforcoMes)[0];

                HttpContext.Current.Session["terminal"] = result.outTerminal; 

                if (result.outErro != "")
                    throw new Exception("Prodesp - " + result.outErro);

                return result.outSucesso;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") 
                    ? "Erro na comunicação com WebService Prodesp." 
                    : ex.Message);
            }
        }

        public string InserirReservaCancelamento(
            string chave, string senha, ReservaCancelamento cancelamento, 
            List<IMes> cancelamentoMeses)
        {
            try
            {
                var result = DataHelperProdesp.Procedure_AnulacaoReserva(
                    chave, senha, cancelamento, cancelamentoMeses)[0];

                HttpContext.Current.Session["terminal"] = result.outTerminal; 

                if (result.outErro != "")
                    throw new Exception("Prodesp - " + result.outErro);

                return result.outSucesso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") 
                    ? "Erro na comunicação com WebService Prodesp." 
                    : ex.Message);
            }
        }

        public ConsultaContrato ConsultaContrato(string chave, string senha, string contrato)
        {
            var result = DataHelperProdesp.Procedure_ConsultaContrato(
                chave, senha, contrato).ToList();

            if (result[0].outErro != "")
                throw new Exception("Prodesp - " + result[0].outErro);

            return new ConsultaContrato
            {
                OutContrato = result[0].outContrato,
                OutCpfcnpj = result[0].outCPFCNPJ,
                OutCodObra = result[0].outCodObra,
                OutContratada = result[0].outContratada,
                OutObjeto = result[0].outObjeto,
                OutProcesSiafem = result[0].outProcesSiafem,
                OutPrograma = result[0].outPrograma,
                OutCED = result[0].outCED,
                ListConsultaContrato = result.Select(x => new InfoConsultaContrato
                {
                    OutData = x.outData,
                    OutEvento = x.outEvento,
                    OutNumero = x.outNumero,
                    OutValor = x.outValor
                }).ToList()
            };
        }

        public ConsultaReservaEstrutura ConsultaReservaEstrutura(
                    int anoExercicio,
                    string regional,
                    string cfp,
                    string natureza,
                    int programa,
                    string origemRecurso,
                    string processo,
                    string chave,
                    string senha)
        {
            var result = DataHelperProdesp.Procedure_ConsultaReservaEstrutura(
                anoExercicio,
                regional,
                cfp,
                natureza,
                programa,
                origemRecurso,
                processo,
                chave,
                senha).ToList();


            if (result[0].outErro != "")
                throw new Exception("Prodesp - " + result[0].outErro);

            return new ConsultaReservaEstrutura
            {
                OutCodAplicacao = result[0].outCodAplicacao,
                OutDataInic = result[0].outDataInic,
                OutDispEmpenhar = result[0].outDispEmpenhar,
                OutNrReserva = result[0].outNrReserva,
                OutValorAtual = result[0].outValorAtual,
                OutValorEmpenhado = result[0].outValorEmpenhado,
                OutErro = result[0].outErro,
                OutCED = result[0].outCED,

                ListConsultaReservaEstrutura = result.Select(x => new InfoConsultaReservaEstrutura
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

        public ConsultaReserva ConsultaReserva(string chave, string senha, string reserva)
        {
            var result = DataHelperProdesp.Procedure_ConsultaReserva(chave, senha, reserva)[0];


            if (result.outErro != "")
                throw new Exception("Prodesp - "+result.outErro);

            return new ConsultaReserva
            {
                OutNumReserva = reserva,
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
                OutIdentContrato = result.outIdentContrato,
                OutNumProcesso = result.outNumProcesso,
                OutPrevInic = result.outPrevInic,
                OutSaldoQ1 = result.outSaldoQ1,
                OutSaldoQ2 = result.outSaldoQ2,
                OutSaldoQ3 = result.outSaldoQ3,
                OutSaldoQ4 = result.outSaldoQ4,
                OutSaldoReserva = result.outSaldoReserva,
                OutSeqTam = result.outSeqTAM,
                OutSucesso = result.outSucesso,
                OutValorAnulado = result.outValorAnulado,
                OutValorEmpenhado = result.outValorEmpenhado,
                OutValorReforco = result.outValorReforco,
                OutValorReserva = result.outValorReserva
            };
        }

        public ConsultaEmpenho ConsultaEmpenho(string chave, string senha, string empenho)
        {
            var result = DataHelperProdesp.Procedure_ConsultaEmpenho(chave, senha, empenho)[0];

            if (result.outErro != "")
                throw new Exception("Prodesp - " + result.outErro);

            return new ConsultaEmpenho
            {
                NumEmpenho = empenho,
                OutAutorizadorCargo = result.outAutorizadorCargo,
                OutAutorizadorNome = result.outAutorizadorNome,
                OutBairro = result.outBairro,
                OutCed1 = result.outCED_1,
                OutCed2 = result.outCED_2,
                OutCed3 = result.outCED_3,
                OutCed4 = result.outCED_4,
                OutCed5 = result.outCED_5,
                OutCep = result.outCEP,
                OutCfp1 = result.outCFP_1,
                OutCfp2 = result.outCFP_2,
                OutCfp3 = result.outCFP_3,
                OutCfp4 = result.outCFP_4,
                OutCfp5 = result.outCFP_5,
                OutCnpjCpf = result.outCnpjCpf,
                OutCnpjCpfTipo = result.outCnpjCpf_tipo,
                OutCodAplicacao = result.outCodAplicacao,
                OutCodObra = result.outCodObra,
                OutCredor = result.outCredor,
                OutDataEmissao = result.outDataEmissao,
                OutDestRecurso = result.outDestRecurso,
                OutEndereco = result.outEndereco,
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
                OutEstado = result.outEstado,
                OutExaminadorCargo = result.outExaminadorCargo,
                OutExaminadorNome = result.outExaminadorNome,
                OutFolhas = result.outFolhas,
                OutFonteSiafem = result.outFonteSIAFEM,
                OutMunicipio = result.outMunicipio,
                OutNeSiafem = result.outNESiafem,
                OutNumContrato = result.outNumContrato,
                OutNumProcesso = result.outNumProcesso,
                OutOrigemRecurso = result.outOrigemRecurso,
                OutPrevInic = result.outPrevInic,
                OutSaldoEmpenho = result.outSaldoEmpenho,
                OutSaldoQ1 = result.outSaldoQ1,
                OutSaldoQ2 = result.outSaldoQ2,
                OutSaldoQ3 = result.outSaldoQ3,
                OutSaldoQ4 = result.outSaldoQ4,
                OutSeqTam = result.outSeqTAM,
                OutSucesso = result.outSucesso,
                OutTerminal = result.outTerminal,
                OutValorAnulado = result.outValorAnulado,
                OutValorEmpenho = result.outValorEmpenho,
                OutValorReforco = result.outValorReforco,
                OutValorSubEmpenhado = result.outValorSubEmpenhado
            };
        }

        public ConsultaEspecificacaoDespesa ConsultaEspecificacaoDespesa(string chave, string senha, string especificacao)
        {
            var result = DataHelperProdesp.Procedure_ConsultaEspecificacaoDespesa(
                chave, senha, especificacao)[0];

            if (result.outErro != "")
                throw new Exception("Prodesp - " + result.outErro);

            if (result.outCodigoEspecificacaoDesp == especificacao)
            {
                return new ConsultaEspecificacaoDespesa
                {
                    outFimTransacao = result.outFimTransacao,
                    outErro = result.outErro,
                    outCodigoEspecificacaoDesp = result.outCodigoEspecificacaoDesp,
                    outEspecDespesa = result.outEspecDespesa,
                    outSucesso = result.outSucesso,
                    outEspecDespesa_02 = result.outEspecDespesa_02,
                    outEspecDespesa_03 = result.outEspecDespesa_03,
                    outEspecDespesa_04 = result.outEspecDespesa_04,
                    outEspecDespesa_05 = result.outEspecDespesa_05,
                    outEspecDespesa_06 = result.outEspecDespesa_06,
                    outEspecDespesa_07 = result.outEspecDespesa_07,
                    outEspecDespesa_08 = result.outEspecDespesa_08,
                    outEspecDespesa_09 = result.outEspecDespesa_09
                };
            }
            else
                throw new Exception(String.Format("Código de Especificação não Cadastrado."));
        }

        public ConsultaAssinatura ConsultaAssinatura(string chave, string senha, string assinatura, int tipo)
        {
            var result = DataHelperProdesp.Procedure_ConsultaAssinaturas(chave, senha, assinatura, tipo)[0];

            if (result.outErro != "")
                throw new Exception("Prodesp - " + result.outErro);

            return new ConsultaAssinatura
            {
                outGrupoAutorizador = result.outGrupoAutorizador,
                outOrgaoAutorizador = result.outOrgaoAutorizador,
                outNomeAutorizador = result.outNomeAutorizador,
                outCargoAutorizador = result.outCargoAutorizador,

                outGrupoExaminador = result.outGrupoExaminador,
                outOrgaoExaminador = result.outOrgaoExaminador,
                outNomeExaminador = result.outNomeExaminador,
                outCargoExaminador = result.outCargoExaminador,

                outGrupoResponsavel = result.outGrupoResponsavel,
                outOrgaoResponsavel = result.outOrgaoResponsavel,
                outNomeResponsavel = result.outNomeResponsavel,
                outCargoResponsavel = result.outCargoResponsavel
            };
        }
    }
}
