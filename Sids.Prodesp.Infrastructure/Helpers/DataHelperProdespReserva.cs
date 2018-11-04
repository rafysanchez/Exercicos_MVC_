namespace Sids.Prodesp.Infrastructure.Helpers
{
    using Model.Entity.Configuracao;
    using Model.Entity.Reserva;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.Interface.Base;
    using ProdespReserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class DataHelperProdespReserva
    {
        static readonly string[] B1 = new string[] { "01", "02", "03" };
        static readonly string[] B2 = new string[] { "04", "05", "06" };
        static readonly string[] B3 = new string[] { "07", "08", "09" };
        static readonly string[] B4 = new string[] { "10", "11", "12" };



        public static Procedure_InclusaoReservaRecordType[] Procedure_InclusaoReserva(string key, string password, Reserva entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional)
        {
            return new WSProdespReserva().Procedure_InclusaoReserva(
                CreateReservaFilterType(key, password, entity, months, program, structure, source, regional), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ReforcoReservaRecordType[] Procedure_ReservaReforco(string key, string password, ReservaReforco entity, IEnumerable<IMes> months)
        {
            return new WSProdespReserva().Procedure_ReforcoReserva(
                CreateReservaReforcoFilterType(key, password, entity, months), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_AnulacaoReservaRecordType[] Procedure_AnulacaoReserva(string key, string password, ReservaCancelamento entity, IEnumerable<IMes> months)
        {
            return new WSProdespReserva().Procedure_AnulacaoReserva(
                CreateReservaCancelamentoFilterType(key, password, entity, months), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_InclusaoDocSIAFEMRecordType[] Procedure_InclusaoDocSIAFEM(string key, string password, string numberSiafem, string numberProdesp, string type)
        {
            var doc = CreateSiafemDocumentFilterType(key, password, numberSiafem, numberProdesp, type);

            return new WSProdespReserva().Procedure_InclusaoDocSIAFEM(doc, new ModelVariablesType(), new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaContratoRecordType[] Procedure_ConsultaContrato(string key, string password, string contract)
        {
            return new WSProdespReserva().Procedure_ConsultaContrato(
                CreateContratoFilterType(key, password, contract), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaReservaEstruturaRecordType[] Procedure_ConsultaReservaEstrutura(string key, string password, int year, string regional, string cfp, string nature, int program, string resourceSource, string process)
        {
            return new WSProdespReserva().Procedure_ConsultaReservaEstrutura(
                CreateReservaEstruturaFilterType(key, password, year, regional, cfp, nature, program, resourceSource, process), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaReservaRecordType[] Procedure_ConsultaReserva(string key, string password, string numberReserva)
        {
            return new WSProdespReserva().Procedure_ConsultaReserva(
                CreateReservaFilterType(key, password, numberReserva), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaEmpenhoRecordType[] Procedure_ConsultaEmpenho(string key, string password, string numberEmpenho)
        {
            return new WSProdespReserva().Procedure_ConsultaEmpenho(
                CreateEmpenhoFilterType(key, password, numberEmpenho), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaEspecificacoesRecordType[] Procedure_ConsultaEspecificacaoDespesa(string key, string password, string specification)
        {
            return new WSProdespReserva().Procedure_ConsultaEspecificacoes(
                CreateEspecificacoesFilterType(key, password, specification), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaAssinaturasRecordType[] Procedure_ConsultaAssinaturas(string key, string password, string signatures, int type)
        {
            return new WSProdespReserva().Procedure_ConsultaAssinaturas(
                CreateAssinaturasFilterType(key, password, signatures, type), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }



        private static Procedure_ConsultaContratoFiltersType CreateContratoFilterType(string key, string password, string contract)
        {
            return new Procedure_ConsultaContratoFiltersType
            {
                inOperador = key,
                inChave = password,
                inIdentContratoAno = contract?.Substring(0, 2),
                inIdentContratoNum = contract?.Substring(4, 5),
                inIdentContratoOrgao = contract?.Substring(2, 2)
            };
        }
        private static Procedure_ConsultaReservaEstruturaFiltersType CreateReservaEstruturaFilterType(string key, string password, int year, string regional, string cfp, string nature, int program, string resourceSource, string process)
        {
            return new Procedure_ConsultaReservaEstruturaFiltersType
            {
                inOperador = key,
                inChave = password,
                inAno = year.ToString().Substring(2, 2),

                inCFP_1 = cfp?.Substring(0, 2),
                inCFP_2 = cfp?.Substring(2, 3),
                inCFP_3 = cfp?.Substring(5, 4),
                inCFP_4 = cfp?.Substring(9, 4),
                inCFP_5 = resourceSource?.Substring(1, 2) + "00",

                inCED_1 = nature?.Substring(0, 1),
                inCED_2 = nature?.Substring(1, 1),
                inCED_3 = nature?.Substring(2, 1),
                inCED_4 = nature?.Substring(3, 1),
                inCED_5 = nature?.Substring(4, 2),

                inOrgao = regional.Substring(2, 2),
                inOrigemRecurso = resourceSource?.Substring(1, 2),
                inProcesso = process
            };
        }
        private static Procedure_ConsultaReservaFiltersType CreateReservaFilterType(string key, string password, string reservaNumber)
        {
            return new Procedure_ConsultaReservaFiltersType
            {
                inOperador = key,
                inChave = password,
                inNumReserva = reservaNumber
            };
        }
        private static Procedure_ConsultaEspecificacoesFiltersType CreateEspecificacoesFilterType(string key, string password, string specification)
        {
            return new Procedure_ConsultaEspecificacoesFiltersType
            {
                inOperador = key,
                inChave = password,
                inConsultaEspecificDesp = specification
            };
        }
        private static Procedure_ConsultaAssinaturasFiltersType CreateAssinaturasFilterType(string key, string password, string signatures, int type)
        {
            switch (type)
            {
                case 1:
                    return new Procedure_ConsultaAssinaturasFiltersType
                    {
                        inOperador = key,
                        inChave = password,
                        inCodigoConsultAssinDE_01 = signatures,
                        inCodigoConsultAssinATE_01 = signatures
                    };
                case 2:
                    return new Procedure_ConsultaAssinaturasFiltersType
                    {
                        inOperador = key,
                        inChave = password,
                        inCodigoConsultAssinDE_02 = signatures,
                        inCodigoConsultAssinATE_02 = signatures
                    };
                case 3:
                    return new Procedure_ConsultaAssinaturasFiltersType
                    {
                        inOperador = key,
                        inChave = password,
                        inCodigoConsultAssinDE_03 = signatures,
                        inCodigoConsultAssinATE_03 = signatures
                    };
                default:
                    return new Procedure_ConsultaAssinaturasFiltersType
                    {
                        inOperador = key,
                        inChave = password,
                    };
            }
        }
        private static Procedure_ConsultaEmpenhoFiltersType CreateEmpenhoFilterType(string key, string password, string empenhoNumber)
        {
            return new Procedure_ConsultaEmpenhoFiltersType
            {
                inOperador = key,
                inChave = password,
                inNumEmpenho = empenhoNumber
            };
        }
        private static Procedure_InclusaoDocSIAFEMFiltersType CreateSiafemDocumentFilterType(string key, string password, string siafemNumber, string prodespNumber, string type)
        {
            return new Procedure_InclusaoDocSIAFEMFiltersType
            {
                inOperador = key,
                inChave = password,
                inDocSIAFEM = siafemNumber,
                inNumeroProdesp = prodespNumber.Replace("/", ""),
                inTipo = type
            };
        }
        private static Procedure_InclusaoReservaFiltersType CreateReservaFilterType(string key, string password, Reserva entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional)
        {
            var inQuotaReserva_1 = Convert.ToString(months?.Where(x => B1.Contains(x.Descricao)).Sum(y => y.ValorMes));
            var inQuotaReserva_2 = Convert.ToString(months?.Where(x => B2.Contains(x.Descricao)).Sum(y => y.ValorMes));
            var inQuotaReserva_3 = Convert.ToString(months?.Where(x => B3.Contains(x.Descricao)).Sum(y => y.ValorMes));
            var inQuotaReserva_4 = Convert.ToString(months?.Where(x => B4.Contains(x.Descricao)).Sum(y => y.ValorMes));
            var inTotalReserva = months?.Sum(x => x.ValorMes).ToString();

            return new Procedure_InclusaoReservaFiltersType
            {
                inOperador = key,
                inChave = password,
                inAnoRefRes = entity?.AnoExercicio.ToString().Substring(2, 2),
                inAnoExercicio = DateTime.Now.Month <= 2 ? entity?.AnoExercicio.ToString().Substring(2, 2) : default(string),
                inCFPRes_1 = program?.Cfp?.Substring(0, 2),
                inCFPRes_2 = program?.Cfp?.Substring(2, 3),
                inCFPRes_3 = program?.Cfp?.Substring(5, 4),
                inCFPRes_4 = program?.Cfp?.Substring(9, 4),
                inCFPRes_5 = entity?.OrigemRecurso?.Substring(1, 2) + "00" ?? default(string),
                inCEDRes_1 = structure?.Natureza?.Substring(0, 1),
                inCEDRes_2 = structure?.Natureza?.Substring(1, 1),
                inCEDRes_3 = structure?.Natureza?.Substring(2, 1),
                inCEDRes_4 = structure?.Natureza?.Substring(3, 1),
                inCEDRes_5 = structure?.Natureza?.Substring(4, 2),
                inOrgao = regional?.Descricao?.Substring(2, 2),
                inCodAplicacaoRes = entity?.Obra?.ToString(),
                inOrigemRecursoRes = entity?.OrigemRecurso?.Substring(1, 2),
                inDestinoRecursoRes = entity?.DestinoRecurso,
                inNumProcessoRes = entity?.Processo,
                inAutoProcFolhasRes = entity?.AutorizadoSupraFolha,
                inCodEspecDespesaRes = entity?.EspecificacaoDespesa,
                inEspecifDespesaRes = entity?.DescEspecificacaoDespesa.Replace(";", string.Empty),
                inAutoPorAssRes = entity?.AutorizadoAssinatura,
                inAutoPorGrupoRes = entity?.AutorizadoGrupo,
                inAutoPorOrgaoRes = entity?.AutorizadoOrgao,
                inExamPorAssRes = entity?.ExaminadoAssinatura,
                inExamPorGrupoRes = entity?.ExaminadoGrupo,
                inExamPorOrgaoRes = entity?.ExaminadoOrgao,
                inRespEmissaoAssRes = entity?.ResponsavelAssinatura,
                inRespEmissGrupoRes = entity?.ResponsavelGrupo,
                inRespEmissOrgaoRes = entity?.ResponsavelOrgao,
                inIdentContratoANORes = entity?.Contrato?.Substring(0, 2),
                inIdentContratoORGAORes = entity?.Contrato?.Substring(2, 2),
                inIdentContratoNUMRes = entity?.Contrato?.Substring(4, 5),
                inIdentContratoDCRes = entity?.Contrato?.Substring(9, 1),
                inQuotaReserva_1 = inQuotaReserva_1?.Length < 3 ? "0" + inQuotaReserva_1 : inQuotaReserva_1,
                inQuotaReserva_2 = inQuotaReserva_2?.Length < 3 ? "0" + inQuotaReserva_2 : inQuotaReserva_2,
                inQuotaReserva_3 = inQuotaReserva_3?.Length < 3 ? "0" + inQuotaReserva_3 : inQuotaReserva_3,
                inQuotaReserva_4 = inQuotaReserva_4?.Length < 3 ? "0" + inQuotaReserva_4 : inQuotaReserva_4,
                inTotalReserva = inTotalReserva?.Length < 3 ? "0" + inTotalReserva : inTotalReserva,
            };
        }
        private static Procedure_ReforcoReservaFiltersType CreateReservaReforcoFilterType(string key, string password, ReservaReforco entity, IEnumerable<IMes> months)
        {
            return new Procedure_ReforcoReservaFiltersType
            {
                inChave = password,
                inOperador = key,
                inNumReserva = entity?.Reserva.ToString(),
                inQuotaReforco_1 = Convert.ToString(months?.Where(x => B1.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inQuotaReforco_2 = Convert.ToString(months?.Where(x => B2.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inQuotaReforco_3 = Convert.ToString(months?.Where(x => B3.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inQuotaReforco_4 = Convert.ToString(months?.Where(x => B4.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inTotalReforco = months?.Sum(x => x.ValorMes).ToString(),
                inAutoPorAssRef = entity?.AutorizadoAssinatura,
                inAutoPorGrupoRef = entity?.AutorizadoGrupo,
                inAutoPorOrgaoRef = entity?.AutorizadoOrgao,
                inAutoProcFolhasRef = entity?.AutorizadoSupraFolha,
                inDestinoRecursoRef = entity?.DestinoRecurso,
                inExamPorAssRef = entity?.ExaminadoAssinatura,
                inExamPorGrupoRef = entity?.ExaminadoGrupo,
                inExamPorOrgaoRef = entity?.ExaminadoOrgao,
                inNumProcessoRef = entity?.Processo,
                InOrigemRecursoRef = entity?.OrigemRecurso?.Substring(1, 2),
                inCodEspecDespesaRef = entity?.EspecificacaoDespesa,
                inEspecifiDespesaRef = entity?.DescEspecificacaoDespesa.Replace(";", string.Empty),
                inRespEmissGrupoRef = entity?.ResponsavelGrupo,
                inRespEmissaoAssRef = entity?.ResponsavelAssinatura,
                inRespEmissOrgaoRef = entity?.ResponsavelOrgao
            };
        }
        private static Procedure_AnulacaoReservaFiltersType CreateReservaCancelamentoFilterType(string key, string password, ReservaCancelamento entity, IEnumerable<IMes> months)
        {
            return new Procedure_AnulacaoReservaFiltersType
            {
                inChave = password,
                //  inImpressora = 
                inOperador = key,
                inNumReserva = entity?.Reserva.ToString(),
                inQuotaAnulacao_1 = Convert.ToString(months?.Where(x => B1.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inQuotaAnulacao_2 = Convert.ToString(months?.Where(x => B2.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inQuotaAnulacao_3 = Convert.ToString(months?.Where(x => B3.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inQuotaAnulacao_4 = Convert.ToString(months?.Where(x => B4.Contains(x.Descricao)).Sum(y => y.ValorMes)),
                inTotalAnulacao = months?.Sum(x => x.ValorMes).ToString(),
                inAutoPorAssAnu = entity?.AutorizadoAssinatura,
                inAutoPorGrupoAnu = entity?.AutorizadoGrupo,
                inAutoPorOrgaoAnu = entity?.AutorizadoOrgao,
                inAutoProcFolhasAnu = entity?.AutorizadoSupraFolha,
                inDestinoRecursoAnu = entity?.DestinoRecurso,
                inExamPorAssAnu = entity?.ExaminadoAssinatura,
                inExamPorGrupoAnu = entity?.ExaminadoGrupo,
                inExamPorOrgaoAnu = entity?.ExaminadoOrgao,
                inNumProcessoAnu = entity?.Processo,
                InOrigemRecursoAnu = entity?.OrigemRecurso?.Substring(1, 2),
                inCodEspecDespesaAnu = entity?.EspecificacaoDespesa,
                inEspecifiDespesaRef = entity?.DescEspecificacaoDespesa.Replace(";", string.Empty),
                inRespEmissGrupoAnu = entity?.ResponsavelGrupo,
                inRespEmissaoAssAnu = entity?.ResponsavelAssinatura,
                inRespEmissOrgaoAnu = entity?.ResponsavelOrgao
            };
        }
    }

    public class WSProdespReserva : Integracao_DER_SIAFEM_ReservaService
    {
        public WSProdespReserva() : base()
        {
            var ambiente = AppConfig.WsUrl;

            if (ambiente == "siafemDes")
                this.Url = AppConfig.WsReservaUrlDes;
            if (ambiente == "siafemHom")
                this.Url = AppConfig.WsReservaUrlHom;
            if (ambiente == "siafemProd")
                this.Url = AppConfig.WsReservaUrlProd;
        }
    }
}
