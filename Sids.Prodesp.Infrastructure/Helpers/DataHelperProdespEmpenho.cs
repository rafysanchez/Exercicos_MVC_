namespace Sids.Prodesp.Infrastructure.Helpers
{
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.Interface.Base;
    using ProdespEmpenho;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class DataHelperProdespEmpenho
    {
        static readonly string[] B1 = new string[] { "01", "02", "03" };
        static readonly string[] B2 = new string[] { "04", "05", "06" };
        static readonly string[] B3 = new string[] { "07", "08", "09" };
        static readonly string[] B4 = new string[] { "10", "11", "12" };



        public static Procedure_InclusaoEmpenhoRecordType[] Procedure_InclusaoEmpenho(string key, string password, Empenho entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional)
        {
            return new WSProdespEmprenho().Procedure_InclusaoEmpenho(
                CreateEmpenhoFilterType(key, password, entity, months, program, structure, source, regional), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_ReforcoEmpenhoRecordType[] Procedure_EmpenhoReforco(string key, string password, EmpenhoReforco entity, IEnumerable<IMes> months, Fonte source)
        {
            return new WSProdespEmprenho().Procedure_ReforcoEmpenho(
                CreateReforcoFilterType(key, password, entity, months, source), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }
        public static Procedure_AnulacaoEmpenhoRecordType[] Procedure_AnulacaoEmpenho(string key, string password, EmpenhoCancelamento entity, IEnumerable<IMes> months, Fonte source)
        {
            return new WSProdespEmprenho().Procedure_AnulacaoEmpenho(CreateAnulacaoFilterType(key, password, entity, months, source),
                new ModelVariablesType(),
                new EnvironmentVariablesType());
        }
        public static Procedure_ConsultaEmpenhoEstruturaRecordType[] Procedure_ConsultaEmpenhoEstrutura(string key, string password, int year, string regional, string cfp, string nature, string resourceSource,string processo)
        {
            return new WSProdespEmprenho().Procedure_ConsultaEmpenhoEstrutura(
                CreateEmpenhoEstruturaFilterType(key, password, year, regional, cfp, nature, resourceSource, processo), 
                new ModelVariablesType(), 
                new EnvironmentVariablesType());
        }

        private static Procedure_InclusaoEmpenhoFiltersType CreateEmpenhoFilterType(string key, string password, Empenho entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional)
        {
            var NumeroOriginalSiafemSiafisico = $"{entity.NumeroEmpenhoSiafem}{entity.NumeroEmpenhoSiafisico}";
            var filter = new Procedure_InclusaoEmpenhoFiltersType()
            {
                inChave = password,
                inOperador = key
            };

            if (months != null)
            {
                filter.inQuota1 = months.Where(w => B1.Contains(w.Descricao)).Sum(z => z.ValorMes).ToString();
                filter.inQuota2 = months.Where(w => B2.Contains(w.Descricao)).Sum(z => z.ValorMes).ToString();
                filter.inQuota3 = months.Where(w => B3.Contains(w.Descricao)).Sum(z => z.ValorMes).ToString();
                filter.inQuota4 = months.Where(w => B4.Contains(w.Descricao)).Sum(z => z.ValorMes).ToString();
                filter.inTotal = months.Sum(z => z.ValorMes).ToString();
            };

            if (entity != null)
            {
                filter.inCodAssinAutorizacao = entity.CodigoAutorizadoAssinatura.ToString();
                filter.inGrupoAssinAutorizacao = entity.CodigoAutorizadoGrupo.ToString();
                filter.inOrgaoAssinAutorizacao = entity.CodigoAutorizadoOrgao;
                filter.inAutFLS = entity.DescricaoAutorizadoSupraFolha;
                filter.inDestinoRecurso = entity.DestinoId;
                filter.inCodAssinExaminado = entity.CodigoExaminadoAssinatura.ToString();
                filter.inGrupoAssinExaminado = entity.CodigoExaminadoGrupo.ToString();
                filter.inOrgaoAssinExaminado = entity.CodigoExaminadoOrgao;
                filter.inNumProcesso = $"{entity.NumeroProcesso}{entity.NumeroProcessoSiafisico}";
                filter.inCodEspecificaDesp = entity.CodigoEspecificacaoDespesa;
                filter.inGrupoAssinRespons = entity.CodigoResponsavelGrupo.ToString();
                filter.inCodAssinRespons = entity.CodigoResponsavelAssinatura.ToString();
                filter.InOrgaoAssinRespons = entity.CodigoResponsavelOrgao;

                filter.inTipoEmpenho = entity.ModalidadeId.ToString();
                filter.inCodNaturItem = entity.CodigoNaturezaItem;
                filter.inProcessoNE = entity.NumeroProcesso;
                filter.inCGC_CPF = entity.NumeroCNPJCPFFornecedor;
                filter.inCredorOrganTipo = entity.CodigoCredorOrganizacao.ToString();

                filter.inNumNE = string.IsNullOrWhiteSpace(NumeroOriginalSiafemSiafisico) ? "2016NE00001" : NumeroOriginalSiafemSiafisico;

                if (!string.IsNullOrWhiteSpace(entity.CodigoReserva))
                {
                    filter.inReserva = entity.CodigoReserva;
                }
                else
                {
                    filter.inAplicacao = (entity.CodigoAplicacaoObra ?? string.Empty).Replace("-", "");


                    // TODO implementar controle para não enviar ano a partir de primeiro de março
                    var numeroAnoExercicio = entity.NumeroAnoExercicio.ToString();
                    if (numeroAnoExercicio.Length > 3) filter.inExer = FormatarAnoExercicio(numeroAnoExercicio);

                    if (regional != null)
                    {
                        var descricao = (regional.Descricao ?? string.Empty);
                        if (descricao.Length > 3) filter.inOrgao = regional.Descricao.Substring(2, 2);
                    }

                    if (structure != null)
                    {
                        filter.inCED = structure.Natureza;
                    }

                    if (source != null)
                    {
                        var code = source.Codigo ?? string.Empty;
                        if (code.Length > 2)
                        {
                            code = code.Substring(1, 2);
                            if (program != null) filter.inCFP = $"{(program.Cfp)}{code}00";
                            filter.inOrigemRecurso = code;
                        }
                    }
                }


                var numeroContrato = entity.NumeroContrato ?? string.Empty;
                if (numeroContrato.Length > 1) filter.inContratoANO = numeroContrato.Substring(0, 2);
                if (numeroContrato.Length > 4) filter.inContratoORGAO = numeroContrato.Substring(2, 2);
                if (numeroContrato.Length > 9) filter.inContratoNUM = numeroContrato.Substring(4, 5);
                if (numeroContrato.Length >= 10) filter.inContratoDC = numeroContrato.Substring(9, 1);

                filter.inFonteNE = source?.Codigo;

                var origemRecurso = (source?.Codigo ?? string.Empty);
                origemRecurso = origemRecurso.Substring(1, 2);
                filter.inOrigemRecurso = origemRecurso;

                var observacoes = (entity.DescricaoEspecificacaoDespesa ?? string.Empty).Split(';');

                if (observacoes.Length > 0) filter.inEspecificaDesp_01 = observacoes[0];
                if (observacoes.Length > 1) filter.inEspecificaDesp_02 = observacoes[1];
                if (observacoes.Length > 2) filter.inEspecificaDesp_03 = observacoes[2];
                if (observacoes.Length > 3) filter.inEspecificaDesp_04 = observacoes[3];
                if (observacoes.Length > 4) filter.inEspecificaDesp_05 = observacoes[4];
                if (observacoes.Length > 5) filter.inEspecificaDesp_06 = observacoes[5];
                if (observacoes.Length > 6) filter.inEspecificaDesp_07 = observacoes[6];
                if (observacoes.Length > 7) filter.inEspecificaDesp_08 = observacoes[7];
                if (observacoes.Length > 8) filter.inEspecificaDesp_09 = observacoes[8];
            }
            filter.inImprimir = "A";

            return filter;
        }

        private static Procedure_ReforcoEmpenhoFiltersType CreateReforcoFilterType(string key, string password, EmpenhoReforco entity, IEnumerable<IMes> months, Fonte source)
        {

            var descriptions = new List<string>(entity.DescricaoEspecificacaoDespesa?.Split(';') ?? new string[] { });
            return new Procedure_ReforcoEmpenhoFiltersType
            {
                inChave = password,
                //  inImpressora = 
                inOperador = key,
                inEmpenho = entity?.CodigoEmpenho,
                inQuota1 = months?.Where(x => B1.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuota2 = months?.Where(x => B2.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuota3 = months?.Where(x => B3.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuota4 = months?.Where(x => B4.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inTotal = months?.Sum(x => x.ValorMes).ToString(),
                inCodAssinAutorizacao = entity?.CodigoAutorizadoAssinatura.ToString(),
                inGrupoAssinAutorizacao = entity?.CodigoAutorizadoGrupo.ToString(),
                inOrgaoAssinAutorizacao = entity?.CodigoAutorizadoOrgao,
                inAutFLS = entity?.DescricaoAutorizadoSupraFolha,
                inDestinoRecurso = entity.DestinoId,
                inCodAssinExaminado = entity?.CodigoExaminadoAssinatura.ToString(),
                inGrupoAssinExaminado = entity?.CodigoExaminadoGrupo.ToString(),
                inOrgaoAssinExaminado = entity?.CodigoExaminadoOrgao,
                inNumProcesso = entity?.NumeroProcesso,
                inOrigemRecurso = source.Codigo.Substring(1, 2),
                inEspecificaDesp_01 = descriptions.Count > 0 ? descriptions[0] : null,
                inEspecificaDesp_02 = descriptions.Count > 1 ? descriptions[1] : null,
                inEspecificaDesp_03 = descriptions.Count > 2 ? descriptions[2] : null,
                inEspecificaDesp_04 = descriptions.Count > 3 ? descriptions[3] : null,
                inEspecificaDesp_05 = descriptions.Count > 4 ? descriptions[4] : null,
                inEspecificaDesp_06 = descriptions.Count > 5 ? descriptions[5] : null,
                inEspecificaDesp_07 = descriptions.Count > 6 ? descriptions[6] : null,
                inEspecificaDesp_08 = descriptions.Count > 7 ? descriptions[7] : null,
                inEspecificaDesp_09 = descriptions.Count > 8 ? descriptions[8] : null,
                inCodEspecificaDesp = entity.CodigoEspecificacaoDespesa,
                inGrupoAssinRespons = entity?.CodigoResponsavelGrupo.ToString(),
                inCodAssinRespons = entity?.CodigoResponsavelAssinatura.ToString(),
                InOrgaoAssinRespons = entity?.CodigoResponsavelOrgao,
                inReforcoSIAFEM = $"{entity?.NumeroEmpenhoSiafem}{entity?.NumeroEmpenhoSiafisico}",
                inReserva = entity.CodigoReserva,
                inImprimir = "A"
            };
        }
        private static Procedure_AnulacaoEmpenhoFiltersType CreateAnulacaoFilterType(string key, string password, EmpenhoCancelamento entity, IEnumerable<IMes> months, Fonte source)
        {
            var descriptions = new List<string>(entity.DescricaoEspecificacaoDespesa?.Split(';') ?? new string[] { });
            return new Procedure_AnulacaoEmpenhoFiltersType
            {
                //  inImpressora = 
                inOperador = key,
                inChave = password,

                inNumProcesso = entity?.NumeroProcesso,
                inOrigemRecurso = source.Codigo.Substring(1, 2),
                inDestinoRecurso = entity.DestinoId,

                inEmpenho = entity?.CodigoEmpenho,
                inQuota1 = months?.Where(x => B1.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuota2 = months?.Where(x => B2.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuota3 = months?.Where(x => B3.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),
                inQuota4 = months?.Where(x => B4.Contains(x.Descricao)).Sum(y => y.ValorMes).ToString(),

                inTotal = months?.Sum(x => x.ValorMes).ToString(),

                inAutFLS = entity?.DescricaoAutorizadoSupraFolha,
                inCodEspecificaDesp = entity.CodigoEspecificacaoDespesa,
                inEspecificaDesp_01 = descriptions.Count > 0 ? descriptions[0] : null,
                inEspecificaDesp_02 = descriptions.Count > 1 ? descriptions[1] : null,
                inEspecificaDesp_03 = descriptions.Count > 2 ? descriptions[2] : null,
                inEspecificaDesp_04 = descriptions.Count > 3 ? descriptions[3] : null,
                inEspecificaDesp_05 = descriptions.Count > 4 ? descriptions[4] : null,
                inEspecificaDesp_06 = descriptions.Count > 5 ? descriptions[5] : null,
                inEspecificaDesp_07 = descriptions.Count > 6 ? descriptions[6] : null,
                inEspecificaDesp_08 = descriptions.Count > 7 ? descriptions[7] : null,
                inEspecificaDesp_09 = descriptions.Count > 8 ? descriptions[8] : null,

                inCodAssinAutorizacao = entity?.CodigoAutorizadoAssinatura.ToString(),
                inGrupoAssinAutorizacao = entity?.CodigoAutorizadoGrupo.ToString(),
                inOrgaoAssinAutorizacao = entity?.CodigoAutorizadoOrgao,

                inCodAssinExaminado = entity?.CodigoExaminadoAssinatura.ToString(),
                inGrupoAssinExaminado = entity?.CodigoExaminadoGrupo.ToString(),
                inOrgaoAssinExaminado = entity?.CodigoExaminadoOrgao,

                inCodAssinRespons = entity?.CodigoResponsavelAssinatura.ToString(),
                inGrupoAssinRespons = entity?.CodigoResponsavelGrupo.ToString(),
                InOrgaoAssinRespons = entity?.CodigoResponsavelOrgao,
                inImprimir = "A"
            };
        }
        private static Procedure_ConsultaEmpenhoEstruturaFiltersType CreateEmpenhoEstruturaFilterType(string key, string password, int year, string regional, string cfp, string nature, string resourceSource,string processo)
        {
            return new Procedure_ConsultaEmpenhoEstruturaFiltersType
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
                inProcesso = processo
            };
        }

        private static string FormatarAnoExercicio(string numeroAnoExercicio)
        {
            var hoje = DateTime.Today;
            if (hoje >= new DateTime(hoje.Year, 3, 1) && hoje <= new DateTime(hoje.Year, 12, 31))
            {
                return null;
            }
            else
            {
                return numeroAnoExercicio.Substring(2, 2);
            }
        }
    }

    public class WSProdespEmprenho : Integracao_DER_SIAFEM_EmpenhoService
    {
        public WSProdespEmprenho() : base()
        {
            var ambiente = AppConfig.WsUrl;

            if (ambiente == "siafemDes")
                this.Url = AppConfig.WsEmpenhoUrlDes;
            if (ambiente == "siafemHom")
                this.Url = AppConfig.WsEmpenhoUrlHom;
            if (ambiente == "siafemProd")
                this.Url = AppConfig.WsEmpenhoUrlProd;
        }
    }
}
