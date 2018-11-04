namespace Sids.Prodesp.Infrastructure.Helpers
{
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Prodesp;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class DataHelperProdesp
    {
        public static Procedure_InclusaoReservaRecordType[] Procedure_InclusaoReserva(string chave, string senha, Reserva reserva, List<IMes> reservaMes, Programa programa, Estrutura estrutura, Fonte fonte, Regional regional)
        {
            var reservaFiltersType = GerarReservaFiltersType(chave, senha, reserva, reservaMes, programa, estrutura, fonte, regional);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_InclusaoReserva(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ReforcoReservaRecordType[] Procedure_ReservaReforco(string chave, string senha, ReservaReforco reforco, List<IMes> reforcoMes)
        {
            var reforcoFiltersType = GerarReforcoFiltersType(chave, senha, reforco, reforcoMes);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_ReforcoReserva(reforcoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_AnulacaoReservaRecordType[] Procedure_AnulacaoReserva(string chave, string senha, ReservaCancelamento cancelamento, List<IMes> cancelamentoMes)
        {
            var reservaFiltersType = GerarAnulacaoFiltersType(chave, senha, cancelamento, cancelamentoMes);
            var prodespWs = new Integracao_DER_SIAFEM_ReservaService();

            return prodespWs.Procedure_AnulacaoReserva(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_InclusaoDocSIAFEMRecordType[] Procedure_InclusaoDocSIAFEM(
            string chave, string senha, IReserva reserva, string tipo)
        {
            var reservaFiltersType = GerarDocSiafemFiltersType(chave, senha, reserva, tipo);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_InclusaoDocSIAFEM(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaContratoRecordType[] Procedure_ConsultaContrato(
            string chave, string senha, string contrato)
        {
            var contratoFiltersType = GerarContratoFiltersType(chave, senha, contrato);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_ConsultaContrato(contratoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaReservaEstruturaRecordType[] Procedure_ConsultaReservaEstrutura(int anoExercicio, string regional, string cfp, string natureza, int programa, string origemRecurso, string processo, string chave, string senha)
        {
            var reservaEstruturaFiltesType = GerarReservaEstruturaFiltersType(anoExercicio, regional, cfp, natureza, programa, origemRecurso, processo, chave, senha);

            var prodesp = new Integracao_DER_SIAFEM_ReservaService();

            return prodesp.Procedure_ConsultaReservaEstrutura(reservaEstruturaFiltesType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaReservaRecordType[] Procedure_ConsultaReserva(string chave, string senha, string reserva)
        {
            var reservaFiltersType = GerarReservaFiltersType(chave, senha, reserva);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_ConsultaReserva(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaEmpenhoRecordType[] Procedure_ConsultaEmpenho(string chave, string senha, string empenho)
        {
            var reservaFiltersType = GerarEmpenhoFiltersType(chave, senha, empenho);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_ConsultaEmpenho(reservaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaEspecificacoesRecordType[] Procedure_ConsultaEspecificacaoDespesa(string chave, string senha, string especificacao)
        {
            var despesaFiltersType = GerarEspecificacoesFiltersType(chave, senha, especificacao);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_ConsultaEspecificacoes(despesaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaAssinaturasRecordType[] Procedure_ConsultaAssinaturas(string chave, string senha, string assinaturas, int tipo)
        {
            var assinaturasFiltersType = GerarAssinaturasFiltersType(chave, senha, assinaturas, tipo);
            var prodesp = new Integracao_DER_SIAFEM_ReservaService();
            return prodesp.Procedure_ConsultaAssinaturas(assinaturasFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }


        private static Procedure_ConsultaContratoFiltersType GerarContratoFiltersType(string chave, string senha, string contrato)
        {
            return new Procedure_ConsultaContratoFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inIdentContratoAno = contrato?.Substring(0, 2) ?? string.Empty,
                inIdentContratoNum = contrato?.Substring(4, 5) ?? string.Empty,
                inIdentContratoOrgao = contrato?.Substring(2, 2) ?? string.Empty
            };
        }

        private static Procedure_ConsultaReservaEstruturaFiltersType GerarReservaEstruturaFiltersType(int anoExercicio, string regional, string cfp, string natureza, int programa,string origemRecurso, string processo, string chave, string senha)
        {

            return new Procedure_ConsultaReservaEstruturaFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inAno = anoExercicio.ToString().Substring(2, 2),

                inCFP_1 = cfp?.Substring(0, 2),
                inCFP_2 = cfp?.Substring(2, 3),
                inCFP_3 = cfp?.Substring(5, 4),
                inCFP_4 = cfp?.Substring(9, 4),
                inCFP_5 = origemRecurso?.Substring(1, 2) + "00",

                inCED_1 = natureza?.Substring(0, 1),
                inCED_2 = natureza?.Substring(1, 1),
                inCED_3 = natureza?.Substring(2, 1),
                inCED_4 = natureza?.Substring(3, 1),
                inCED_5 = natureza?.Substring(4, 2),

                inOrgao = regional.Substring(2, 2),
                inOrigemRecurso = origemRecurso?.Substring(1, 2)
            };
        }

        private static Procedure_ConsultaReservaFiltersType GerarReservaFiltersType(string chave, string senha, string reserva)
        {
            return new Procedure_ConsultaReservaFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inNumReserva = reserva
            };
        }

        private static Procedure_ConsultaEspecificacoesFiltersType GerarEspecificacoesFiltersType(string chave, string senha, string especificacao)
        {
            return new Procedure_ConsultaEspecificacoesFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inConsultaEspecificDesp = especificacao
            };
        }

        private static Procedure_ConsultaAssinaturasFiltersType GerarAssinaturasFiltersType(string chave, string senha, string assinaturas, int tipo)
        {
            if (tipo == 1)
            {
                return new Procedure_ConsultaAssinaturasFiltersType
                {
                    inOperador = chave,
                    inChave = senha,
                    inCodigoConsultAssinDE_01 = assinaturas,
                    inCodigoConsultAssinATE_01 = assinaturas
                };
            }
            else
            if (tipo == 2)
            {
                return new Procedure_ConsultaAssinaturasFiltersType
                {
                    inOperador = chave,
                    inChave = senha,
                    inCodigoConsultAssinDE_02 = assinaturas,
                    inCodigoConsultAssinATE_02 = assinaturas
                };
            }
            else
            if (tipo == 3)
            {
                return new Procedure_ConsultaAssinaturasFiltersType
                {
                    inOperador = chave,
                    inChave = senha,
                    inCodigoConsultAssinDE_03 = assinaturas,
                    inCodigoConsultAssinATE_03 = assinaturas
                };
            }
            else
                return new Procedure_ConsultaAssinaturasFiltersType
                {
                    inOperador = chave,
                    inChave = senha,
                };

        }

        private static Procedure_ConsultaEmpenhoFiltersType GerarEmpenhoFiltersType(string chave, string senha, string empenho)
        {
            return new Procedure_ConsultaEmpenhoFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inNumEmpenho = empenho
            };
        }

        private static Procedure_InclusaoDocSIAFEMFiltersType GerarDocSiafemFiltersType(string chave, string senha, IReserva reserva, string tipo)
        {
            return new Procedure_InclusaoDocSIAFEMFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inDocSIAFEM = reserva?.NumSiafemSiafisico,
                inNumeroProdesp = reserva?.NumProdesp.Replace("/", ""),
                inTipo = tipo
            };
        }

        private static Procedure_InclusaoReservaFiltersType GerarReservaFiltersType(string chave, string senha, Reserva reserva, List<IMes> mes, Programa programa, Estrutura estrutura, Fonte fonte, Regional regional)
        {
            var bim1 = new string[] { "01", "02", "03" };
            var bim2 = new string[] { "04", "05", "06" };
            var bim3 = new string[] { "07", "08", "09" };
            var bim4 = new string[] { "10", "11", "12" };

            var inQuotaReserva1 = mes?.Where(x => bim1.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString();
            var inQuotaReserva2 = mes?.Where(x => bim2.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString();
            var inQuotaReserva3 = mes?.Where(x => bim3.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString();
            var inQuotaReserva4 = mes?.Where(x => bim4.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString();
            var inTotalReserva = mes?.Sum(x => x.ValorMes).ToString();

            Procedure_InclusaoReservaFiltersType reservaFiltersType = new Procedure_InclusaoReservaFiltersType
            {
                inOperador = chave,
                inChave = senha,
                inAnoRefRes = reserva?.AnoExercicio.ToString().Substring(2, 2),
                inAnoExercicio = DateTime.Now.Month <= 2 ? reserva?.AnoExercicio.ToString().Substring(2, 2) : null,
                inCFPRes_1 = programa?.Cfp?.Substring(0, 2),
                inCFPRes_2 = programa?.Cfp?.Substring(2, 3),
                inCFPRes_3 = programa?.Cfp?.Substring(5, 4),
                inCFPRes_4 = programa?.Cfp?.Substring(9, 4),
                inCFPRes_5 = reserva?.OrigemRecurso?.Substring(1, 2) + "00" ?? string.Empty,
                inCEDRes_1 = estrutura?.Natureza?.Substring(0, 1),
                inCEDRes_2 = estrutura?.Natureza?.Substring(1, 1),
                inCEDRes_3 = estrutura?.Natureza?.Substring(2, 1),
                inCEDRes_4 = estrutura?.Natureza?.Substring(3, 1),
                inCEDRes_5 = estrutura?.Natureza?.Substring(4, 2),
                inOrgao = regional?.Descricao?.Substring(2, 2),
                inCodAplicacaoRes = reserva?.Obra?.ToString(),
                inOrigemRecursoRes = reserva?.OrigemRecurso?.Substring(1, 2),
                inDestinoRecursoRes = reserva?.DestinoRecurso,
                inNumProcessoRes = reserva?.Processo,
                inAutoProcFolhasRes = reserva?.AutorizadoSupraFolha,
                inCodEspecDespesaRes = reserva?.EspecificacaoDespesa,
                inEspecifDespesaRes = reserva?.DescEspecificacaoDespesa.Replace(";", "").Replace(";", ""),
                inAutoPorAssRes = reserva?.AutorizadoAssinatura,
                inAutoPorGrupoRes = reserva?.AutorizadoGrupo,
                inAutoPorOrgaoRes = reserva?.AutorizadoOrgao,
                inExamPorAssRes = reserva?.ExaminadoAssinatura,
                inExamPorGrupoRes = reserva?.ExaminadoGrupo,
                inExamPorOrgaoRes = reserva?.ExaminadoOrgao,
                inRespEmissaoAssRes = reserva?.ResponsavelAssinatura,
                inRespEmissGrupoRes = reserva?.ResponsavelGrupo,
                inRespEmissOrgaoRes = reserva?.ResponsavelOrgao,
                inIdentContratoANORes = reserva?.Contrato?.Substring(0, 2),
                inIdentContratoORGAORes = reserva?.Contrato?.Substring(2, 2),
                inIdentContratoNUMRes = reserva?.Contrato?.Substring(4, 5),
                inIdentContratoDCRes = reserva?.Contrato?.Substring(9, 1),
                inQuotaReserva_1 = inQuotaReserva1?.Length < 3 ? "0" + inQuotaReserva1 : inQuotaReserva1,
                inQuotaReserva_2 = inQuotaReserva2?.Length < 3 ? "0" + inQuotaReserva2 : inQuotaReserva2,
                inQuotaReserva_3 = inQuotaReserva3?.Length < 3 ? "0" + inQuotaReserva3 : inQuotaReserva3,
                inQuotaReserva_4 = inQuotaReserva4?.Length < 3 ? "0" + inQuotaReserva4 : inQuotaReserva4,
                inTotalReserva = inTotalReserva?.Length < 3 ? "0" + inTotalReserva : inTotalReserva,
                inImprimirRes = "A"
            };

            return reservaFiltersType;
        }

        private static Procedure_ReforcoReservaFiltersType GerarReforcoFiltersType(string chave, string senha, ReservaReforco reforco, List<IMes> mes)
        {
            var bim1 = new string[] { "01", "02", "03" };
            var bim2 = new string[] { "04", "05", "06" };
            var bim3 = new string[] { "07", "08", "09" };
            var bim4 = new string[] { "10", "11", "12" };

            Procedure_ReforcoReservaFiltersType reforcoFiltersType = new Procedure_ReforcoReservaFiltersType
            {
                inChave = senha,
                inOperador = chave,
                inNumReserva = reforco?.Reserva.ToString(),
                inQuotaReforco_1 = mes?.Where(x => bim1.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuotaReforco_2 = mes?.Where(x => bim2.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuotaReforco_3 = mes?.Where(x => bim3.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuotaReforco_4 = mes?.Where(x => bim4.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inTotalReforco = mes?.Sum(x => x.ValorMes).ToString(),
                inAutoPorAssRef = reforco?.AutorizadoAssinatura,
                inAutoPorGrupoRef = reforco?.AutorizadoGrupo,
                inAutoPorOrgaoRef = reforco?.AutorizadoOrgao,
                inAutoProcFolhasRef = reforco?.AutorizadoSupraFolha,
                inDestinoRecursoRef = reforco?.DestinoRecurso,
                inExamPorAssRef = reforco?.ExaminadoAssinatura,
                inExamPorGrupoRef = reforco?.ExaminadoGrupo,
                inExamPorOrgaoRef = reforco?.ExaminadoOrgao,
                inNumProcessoRef = reforco?.Processo,
                InOrigemRecursoRef = reforco?.OrigemRecurso?.Substring(1, 2),
                inCodEspecDespesaRef = reforco?.EspecificacaoDespesa,
                inEspecifiDespesaRef = reforco?.DescEspecificacaoDespesa.Replace(";", "").Replace(";", ""),
                inRespEmissGrupoRef = reforco?.ResponsavelGrupo,
                inRespEmissaoAssRef = reforco?.ResponsavelAssinatura,
                inRespEmissOrgaoRef = reforco?.ResponsavelOrgao,
                inImprimirRef = "A"
            };

            return reforcoFiltersType;
        }

        private static Procedure_AnulacaoReservaFiltersType GerarAnulacaoFiltersType(string chave, string senha, ReservaCancelamento cancelamento, List<IMes> mes)
        {
            var bim1 = new string[] { "01", "02", "03" };
            var bim2 = new string[] { "04", "05", "06" };
            var bim3 = new string[] { "07", "08", "09" };
            var bim4 = new string[] { "10", "11", "12" };

            Procedure_AnulacaoReservaFiltersType reforcoFiltersType = new Procedure_AnulacaoReservaFiltersType
            {
                inChave = senha,
                //  inImpressora = 
                inOperador = chave,
                inNumReserva = cancelamento?.Reserva.ToString(),
                inQuotaAnulacao_1 = mes?.Where(x => bim1.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuotaAnulacao_2 = mes?.Where(x => bim2.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuotaAnulacao_3 = mes?.Where(x => bim3.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuotaAnulacao_4 = mes?.Where(x => bim4.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inTotalAnulacao = mes?.Sum(x => x.ValorMes).ToString(),
                inAutoPorAssAnu = cancelamento?.AutorizadoAssinatura,
                inAutoPorGrupoAnu = cancelamento?.AutorizadoGrupo,
                inAutoPorOrgaoAnu = cancelamento?.AutorizadoOrgao,
                inAutoProcFolhasAnu = cancelamento?.AutorizadoSupraFolha,
                inDestinoRecursoAnu = cancelamento?.DestinoRecurso,
                inExamPorAssAnu = cancelamento?.ExaminadoAssinatura,
                inExamPorGrupoAnu = cancelamento?.ExaminadoGrupo,
                inExamPorOrgaoAnu = cancelamento?.ExaminadoOrgao,
                inNumProcessoAnu = cancelamento?.Processo,
                InOrigemRecursoAnu = cancelamento?.OrigemRecurso?.Substring(1, 2),
                inCodEspecDespesaAnu = cancelamento?.EspecificacaoDespesa,
                inEspecifiDespesaRef = cancelamento?.DescEspecificacaoDespesa.Replace(";", "").Replace(";", ""),
                inRespEmissGrupoAnu = cancelamento?.ResponsavelGrupo,
                inRespEmissaoAssAnu = cancelamento?.ResponsavelAssinatura,
                inRespEmissOrgaoAnu = cancelamento?.ResponsavelOrgao,
                inImprimirAnu = "A"
            };

            return reforcoFiltersType;
        }
    }
}
