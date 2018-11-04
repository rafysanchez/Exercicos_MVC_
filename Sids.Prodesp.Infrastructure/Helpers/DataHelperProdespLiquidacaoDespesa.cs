using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Infrastructure.Helpers
{
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Extension;
    using Model.Interface.LiquidacaoDespesa;
    using ProdespLiquidacaoDespesa;
    using System;

    internal static class DataHelperProdespLiquidacaoDespesa
    {
        public static Procedure_InclusaoSubEmpenhoRecordType[] Procedure_InclusaoSubEmpenho(string key, string password, Subempenho entity, Estrutura structure)
        {
            var subEmpenhoFilterType = CreateSubEmpenhoFilterType(key, password, entity, structure);
            return new WSLiquidacaoDespesa().Procedure_InclusaoSubEmpenho(subEmpenhoFilterType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_AnulacaoSubEmpenhoRecordType[] Procedure_AnulacaoSubEmpenho(string key, string password, SubempenhoCancelamento entity)
        {
            return new WSLiquidacaoDespesa().Procedure_AnulacaoSubEmpenho(
                CreateSubEmpenhoCancelamentoFilterType(key, password, entity),
                new ModelVariablesType(),
                new EnvironmentVariablesType());
        }

        public static Procedure_InclusaoSubEmpenhoApoioRecordType[] Procedure_InclusaoSubEmpenhoApoio(string key, string password, Subempenho entity)
        {
            return new WSLiquidacaoDespesa().Procedure_InclusaoSubEmpenhoApoio(CreateSubEmpenhoApoioFilterType(key, password, entity), new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_AnulacaoSubEmpenhoApoioRecordType[] Procedure_AnulacaoSubEmpenhoApoio(string key, string password, SubempenhoCancelamento entity)
        {
            return new WSLiquidacaoDespesa().Procedure_AnulacaoSubEmpenhoApoio(AnulacaoSubEmpenhoApoioFilterType(key, password, entity), new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaEmpenhoCredorRecordType[] Procedure_ConsultaEmpenhoCredor(string key, string password, Subempenho entity)
        {
            return new WSLiquidacaoDespesa().Procedure_ConsultaEmpenhoCredor(CreateConsultaEmpenhoCredorFilterType(key, password, entity), new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_InscricaoRAPRecordType[] Procedure_InclusaoRapInscricao(string key, string password, IRap entity)
        {
            var dadosEnviar = CreateRapInscricaoFilterType(key, password, entity);

            return new WSLiquidacaoDespesa().Procedure_InscricaoRAP(dadosEnviar, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_RequisicaoRAPRecordType[] Procedure_RequisicaoRAP(string key, string password, IRap entity)
        {
            var requisicaoRapFiltersType = CreateRapRequisicaoFilterType(key, password, entity);

            return new WSLiquidacaoDespesa().Procedure_RequisicaoRAP(requisicaoRapFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_AnulacaoRequisicaoRAPRecordType[] Procedure_AnulacaoRequisicaoRAP(string key, string password, RapAnulacao entity)
        {
            var anulacaoRequisicaoRapFiltersType = CreateAnulacaoRequisicaoRAPFiltersType(key, password, entity);

            return new WSLiquidacaoDespesa().Procedure_AnulacaoRequisicaoRAP(anulacaoRequisicaoRapFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_RequisicaoRAPApoioRecordType[] Procedure_RapRequisicaoApoio(string key, string password, IRap entity)
        {
            return new WSLiquidacaoDespesa().Procedure_RequisicaoRAPApoio(
                CreateRapRequisicaoApoioFilterType(key, password, entity),
                new ModelVariablesType(),
                new EnvironmentVariablesType());
        }



        public static Procedure_ConsultaEmpenhoInscritoRecordType[] Procedure_ConsultaEmpenhoInscrito(string key, string password, IRap entity)
        {
            return new WSLiquidacaoDespesa().Procedure_ConsultaEmpenhoInscrito(
                CreateConsultaEmpenhoInscritoFilterType(key, password, entity),
                new ModelVariablesType(),
                new EnvironmentVariablesType());
        }



        public static Procedure_anulacaoRequisicaoRAPApoioRecordType[] Procedure_RapAnulacaoApoio(string key, string password, string numRequisicaoRAP)
        {
            var rapAnulacaoFilterType = CreateRapAnulacaoFilterType(key, password, numRequisicaoRAP);

            return new WSLiquidacaoDespesa().Procedure_anulacaoRequisicaoRAPApoio(rapAnulacaoFilterType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaContratoTodosRecordType[] Procedure_ConsultaContrato(string key, string password, string contract, string type)
        {
            var obj = new WSLiquidacaoDespesa().Procedure_ConsultaContratoTodos(CreateContratoFilterType(key, password, contract, type), new ModelVariablesType(), new EnvironmentVariablesType());
            return obj;
        }

        public static Procedure_ConsultaEmpenhoSaldoRAPRecordType[] Procedure_ConsultaEmpenhoSaldoRAP(string key, string password, IRap entity, Regional regional)
        {
            return new WSLiquidacaoDespesa().Procedure_ConsultaEmpenhoSaldoRAP(CreateEmpenhoSaldoRAPFilters(key, password, entity, regional), new ModelVariablesType(), new EnvironmentVariablesType());
        }

        #region MetodosPrivados

        private static Procedure_RequisicaoRAPFiltersType CreateRapRequisicaoFilterType(string key, string password, IRap entity)
        {
            var filter = new Procedure_RequisicaoRAPFiltersType();

            GetRapRequisicaoFiltersCommonFactory(key, password, entity, ref filter);

            switch (entity.CenarioProdesp)
            {
                case "RAPSimples":
                    GetRequisicaoFiltersFactory(entity, ref filter);
                    break;

                case "RAPContrato":
                    GetRequisicaoContratoFiltersFactory(entity, ref filter);
                    break;

                case "RAPRecibo":
                    GetRequisicaoReciboFiltersFactory(entity, ref filter);
                    break;

                case "RAPOraganizao7":
                    GetRequisicaoOrganizacao7FiltersFactory(entity, ref filter);
                    break;
            }

            return filter;
        }

        private static Procedure_AnulacaoRequisicaoRAPFiltersType CreateAnulacaoRequisicaoRAPFiltersType(string key, string password, RapAnulacao entity)
        {
            var filter = new Procedure_AnulacaoRequisicaoRAPFiltersType();

            GetRapAnulacaoFiltersCommonFactory(key, password, entity, ref filter);

            switch (entity.CenarioProdesp)
            {
                case "RAPSimples":
                    GetAnulacaoFiltersFactory(entity, ref filter);
                    break;

                case "SemContrato":
                    GetAnulacaoFiltersFactory(entity, ref filter);
                    break;

                case "ComContrato":
                    GetAnulacaoContratoFiltersFactory(entity, ref filter);
                    break;
            }

            return filter;
        }

        private static Procedure_ConsultaEmpenhoSaldoRAPFiltersType CreateEmpenhoSaldoRAPFilters(string key, string password, IRap entity, Regional regional)
        {
            return new Procedure_ConsultaEmpenhoSaldoRAPFiltersType
            {
                inOperador = key,
                inChave = password,
                inAno = entity.NumeroAnoExercicio.ToString().Substring(2, 2),
                inOrgao = regional.Descricao?.Substring(2, 2)
            };
        }

        private static Procedure_anulacaoRequisicaoRAPApoioFiltersType CreateRapAnulacaoFilterType(string key, string password, string numRequisicaoRAP)
        {
            return new Procedure_anulacaoRequisicaoRAPApoioFiltersType
            {
                inOperador = key,
                inChave = password,
                inNumRequisicaoRAP = numRequisicaoRAP
            };
        }

        private static Procedure_RequisicaoRAPApoioFiltersType CreateRapRequisicaoApoioFilterType(string key, string password, IRap entity)
        {
            return new Procedure_RequisicaoRAPApoioFiltersType
            {
                inOperador = key,
                inChave = password,
                inNumSubEmpenho = entity.NumeroSubempenho,
                inRecibo = entity.NumeroRecibo
            };
        }



        private static Procedure_ConsultaEmpenhoInscritoFiltersType CreateConsultaEmpenhoInscritoFilterType(string key, string password, IRap entity)
        {
            return new Procedure_ConsultaEmpenhoInscritoFiltersType
            {
                inOperador = key,
                inChave = password,
                inEmpenho = entity.NumeroProdesp
            };
        }

        private static Procedure_InscricaoRAPFiltersType CreateRapInscricaoFilterType(string key, string password, IRap entity)
        {
            if (entity.CEDId == 3)
            {

                return new Procedure_InscricaoRAPFiltersType
                {
                    inOperador = key,
                    inChave = password,
                    inAno = entity.NumeroOriginalProdesp.Substring(0, 2),
                    inOrgao = entity.NumeroOriginalProdesp.Substring(2, 2),
                    inNumEmpenho = entity.NumeroOriginalProdesp.Substring(4, 5),
                    inAutorizadoPor = entity.DescricaoUsoAutorizadoPor,

                    inFolhasGrupo3 = entity.DescricaoAutorizadoSupraFolha,
                    inProcessoGrupo3 = entity.NumeroProcesso,

                    inAssinAUTOGrupo3 = Convert.ToString(entity.CodigoAutorizadoAssinatura),
                    inGrupoAUTOGrupo3 = Convert.ToString(entity.CodigoAutorizadoGrupo),
                    inOrgaoAUTOGrupo3 = entity.CodigoAutorizadoOrgao,

                    inAssinEXAMGrupo3 = Convert.ToString(entity.CodigoExaminadoAssinatura),
                    inGrupoEXAMGrupo3 = Convert.ToString(entity.CodigoExaminadoGrupo),
                    inOrgaoEXAMGrupo3 = entity.CodigoExaminadoOrgao,

                    inAssinRESPGrupo3 = Convert.ToString(entity.CodigoResponsavelAssinatura),
                    inGrupoRESPGrupo3 = Convert.ToString(entity.CodigoResponsavelGrupo),
                    inOrgaoRESPGrupo3 = entity.CodigoResponsavelOrgao,
                    inImprimir = "A"
                };

            }
            else
            {
                return new Procedure_InscricaoRAPFiltersType
                {
                    inOperador = key,
                    inChave = password,
                    inAno = entity.NumeroOriginalProdesp.Substring(0, 2),
                    inOrgao = entity.NumeroOriginalProdesp.Substring(2, 2),
                    inNumEmpenho = entity.NumeroOriginalProdesp.Substring(4, 5),
                    inAutorizadoPor = entity.DescricaoUsoAutorizadoPor,

                    inFolhasGrupo4 = entity.DescricaoAutorizadoSupraFolha,
                    inProcessoGrupo4 = entity.NumeroProcesso,

                    inAssinAUTOGrupo4 = Convert.ToString(entity.CodigoAutorizadoAssinatura),
                    inGrupoAUTOGrupo4 = Convert.ToString(entity.CodigoAutorizadoGrupo),
                    inOrgaoAUTOGrupo4 = entity.CodigoAutorizadoOrgao,

                    inAssinEXAMGrupo4 = Convert.ToString(entity.CodigoExaminadoAssinatura),
                    inGrupoEXAMGrupo4 = Convert.ToString(entity.CodigoExaminadoGrupo),
                    inOrgaoEXAMGrupo4 = entity.CodigoExaminadoOrgao,

                    inAssinRESPGrupo4 = Convert.ToString(entity.CodigoResponsavelAssinatura),
                    inGrupoRESPGrupo4 = Convert.ToString(entity.CodigoResponsavelGrupo),
                    inOrgaoRESPGrupo4 = entity.CodigoResponsavelOrgao,
                    inImprimir = "A"
                };
            }

        }

        public static Procedure_ConsultaSubEmpenhoRecordType[] Procedure_ConsultaSubEmpenho(string key, string password, string subempenho)
        {
            var subempenhoFiltersTypes = CreateContratoFilterType(key, password, subempenho);
            return new WSLiquidacaoDespesa().Procedure_ConsultaSubEmpenho(subempenhoFiltersTypes, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        private static Procedure_ConsultaSubEmpenhoFiltersType CreateContratoFilterType(string key, string password, string subempenho)
        {
            return new Procedure_ConsultaSubEmpenhoFiltersType
            {
                inOperador = key,
                inChave = password,
                inSubEmpenho = subempenho
            };
        }

        private static Procedure_ConsultaContratoTodosFiltersType CreateContratoFilterType(string key, string password, string contract, string type)
        {
            return new Procedure_ConsultaContratoTodosFiltersType
            {
                inOperador = key,
                inChave = password,
                inIdentContratoAno = contract?.Substring(0, 2),
                inIdentContratoNum = contract?.Substring(4, 5),
                inIdentContratoOrgao = contract?.Substring(2, 2),
                inTipoDocumento = type
            };
        }

        private static Procedure_InclusaoSubEmpenhoFiltersType CreateSubEmpenhoFilterType(string key, string password, Subempenho entity, Estrutura structure)
        {
            var filter = new Procedure_InclusaoSubEmpenhoFiltersType();

            GetSubEmpenhoFiltersCommonFactory(key, password, entity, ref filter);

            switch (entity.CenarioProdesp)
            {
                case "SubEmpenho":
                    GetSubEmpenhoFiltersFactory(entity, ref filter);
                    break;

                case "SubEmpenhoContrato":
                    GetSubEmpenhoContratoFiltersFactory(entity, ref filter);
                    break;

                case "SubEmpenhoRecibo":
                    GetSubEmpenhoReciboFiltersFactory(entity, ref filter);
                    break;

                case "SubEmpenhoOrganizacao7":
                    GetSubEmpenhoOrganizacao7FiltersFactory(entity, ref filter);
                    break;
            }

            return filter;
        }

        private static Procedure_AnulacaoSubEmpenhoFiltersType CreateSubEmpenhoCancelamentoFilterType(string key, string password, SubempenhoCancelamento entity)
        {
            var filter = new Procedure_AnulacaoSubEmpenhoFiltersType();

            filter.inChave = password;
            filter.inOperador = key;

            filter.inAutFls = entity.DescricaoAutorizadoSupraFolha;
            filter.inCodAssinAUTO = entity.CodigoAutorizadoAssinatura;
            filter.inGrupoAssinAUTO = entity.CodigoAutorizadoGrupo.ToString();
            filter.inOrgaoAssinAUTO = entity.CodigoAutorizadoOrgao;
            filter.inCodAssinEXAM = entity.CodigoExaminadoAssinatura;
            filter.inGrupoAssinEXAM = entity.CodigoExaminadoGrupo.ToString();
            filter.inOrgaoAssinEXAM = entity.CodigoExaminadoOrgao;
            filter.inCodAssinRESP = entity.CodigoResponsavelAssinatura;
            filter.inGrupoAssinRESP = entity.CodigoResponsavelGrupo.ToString();
            filter.inOrgaoAssinRESP = entity.CodigoResponsavelOrgao;

            if (entity.CenarioProdesp == "SubEmpenho")
                filter.inEspecificacaoDesp = entity.CodigoEspecificacaoDespesa;

            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;

            if (entity.CenarioProdesp == "SubEmpenho")
            {
                filter.inEspecificacaoDesp_04 = entity.DescricaoEspecificacaoDespesa4;
                filter.inEspecificacaoDesp_05 = entity.DescricaoEspecificacaoDespesa5;
                filter.inEspecificacaoDesp_06 = entity.DescricaoEspecificacaoDespesa6;
                filter.inEspecificacaoDesp_07 = entity.DescricaoEspecificacaoDespesa7;
                filter.inEspecificacaoDesp_08 = entity.DescricaoEspecificacaoDespesa8;
            }

            filter.inNumProcesso = entity.NumeroProcesso;
            filter.inNumeroSubEmpenho = entity.NumeroSubempenhoProdesp;
            filter.inValorAnular = entity.ValorAnular.ToString("D2");
            filter.inImprimir = "A";
            return filter;
        }

        private static Procedure_InclusaoSubEmpenhoApoioFiltersType CreateSubEmpenhoApoioFilterType(string key, string password, Subempenho entity)
        {
            var filter = new Procedure_InclusaoSubEmpenhoApoioFiltersType();

            filter.inChave = password;
            filter.inOperador = key;

            filter.inCodDespesa = entity.CodigoDespesa;
            filter.inCodTarefa = entity.CodigoTarefa;
            filter.inDataRealizacao = entity.DataRealizado == DateTime.MinValue ? null : entity.DataRealizado.ToString("ddMMyy");
            filter.inNumeroEmpenho = entity.NumeroOriginalProdesp;
            filter.inNumeroRecibo = entity.NumeroRecibo;
            filter.inPrazoPagto = entity.PrazoPagamento;
            filter.inValorRealizado = entity.ValorRealizado.ToString("D2");

            return filter;
        }

        private static Procedure_AnulacaoSubEmpenhoApoioFiltersType AnulacaoSubEmpenhoApoioFilterType(string key, string password, SubempenhoCancelamento entity)
        {
            var filter = new Procedure_AnulacaoSubEmpenhoApoioFiltersType();

            filter.inChave = password;
            filter.inOperador = key;
            filter.inNumeroSubEmpenho = entity.NumeroOriginalProdesp;
            filter.inValorAnular = entity.Valor.ToString();

            return filter;
        }

        private static Procedure_ConsultaEmpenhoCredorFiltersType CreateConsultaEmpenhoCredorFilterType(string key, string password, Subempenho entity)
        {
            var filter = new Procedure_ConsultaEmpenhoCredorFiltersType();
            filter.inChave = password;
            filter.inOperador = key;
            filter.inNomeCredor = entity.NomeCredor;
            filter.inOrganiz = entity.CodigoCredorOrganizacao > 0 ? entity.CodigoCredorOrganizacao.ToString() : null;
            filter.inCGC = entity.NumeroCNPJCPFFornecedor;
            filter.inAno = entity.NumeroAnoExercicio > 0 ? entity.NumeroAnoExercicio.ToString().Substring(2, 2) : null;

            return filter;
        }

        private static void GetSubEmpenhoOrganizacao7FiltersFactory(Subempenho entity, ref Procedure_InclusaoSubEmpenhoFiltersType filter)
        {
            filter.inOrganizacao = entity.CodigoCredorOrganizacao.ZeroParaNulo();
            filter.inCGC_CPF = entity.NumeroCNPJCPFFornecedor;
            filter.inCodEspDespesa = entity.CodigoEspecificacaoDespesa;
            filter.inReferencia = entity.Referencia;
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
            filter.inEspecificacaoDesp_04 = entity.DescricaoEspecificacaoDespesa4;
            filter.inEspecificacaoDesp_05 = entity.DescricaoEspecificacaoDespesa5;
            filter.inEspecificacaoDesp_06 = entity.DescricaoEspecificacaoDespesa6;
            filter.inEspecificacaoDesp_07 = entity.DescricaoEspecificacaoDespesa7;
            filter.inEspecificacaoDesp_08 = entity.DescricaoEspecificacaoDespesa8;
        }

        private static void GetSubEmpenhoReciboFiltersFactory(Subempenho entity, ref Procedure_InclusaoSubEmpenhoFiltersType filter)
        {
            filter.inCaucaoNumGuia = entity.NumeroGuia;
            filter.inValorCaucionado = entity.ValorCaucionado.ZeroParaNulo();
            filter.inQutoGeralAuto = entity.QuotaGeralAutorizadaPor;
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00001";
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
        }

        private static void GetSubEmpenhoContratoFiltersFactory(Subempenho entity, ref Procedure_InclusaoSubEmpenhoFiltersType filter)
        {
            filter.inNumMedicao = entity.NumeroMedicao;
            filter.inNatureza = entity.NaturezaSubempenhoId;
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00001";
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
        }

        private static void GetSubEmpenhoFiltersFactory(Subempenho entity, ref Procedure_InclusaoSubEmpenhoFiltersType filter)
        {
            filter.inCodEspDespesa = entity.CodigoEspecificacaoDespesa;
            filter.inReferencia = entity.Referencia;
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
            filter.inEspecificacaoDesp_04 = entity.DescricaoEspecificacaoDespesa4;
            filter.inEspecificacaoDesp_05 = entity.DescricaoEspecificacaoDespesa5;
            filter.inEspecificacaoDesp_06 = entity.DescricaoEspecificacaoDespesa6;
            filter.inEspecificacaoDesp_07 = entity.DescricaoEspecificacaoDespesa7;
            filter.inEspecificacaoDesp_08 = entity.DescricaoEspecificacaoDespesa8;
        }

        private static void GetSubEmpenhoFiltersCommonFactory(string key, string password, Subempenho entity, ref Procedure_InclusaoSubEmpenhoFiltersType filter)
        {
            filter.inChave = password;
            filter.inOperador = key;
            filter.inNumeroEmpenho = entity.NumeroOriginalProdesp;
            filter.inCodTarefa = entity.CodigoTarefa;
            filter.inCodDespesa = entity.CodigoDespesa;
            filter.inValorRealizado = entity.ValorRealizado.ToString("D2");
            filter.inNumeroRecibo = entity.NumeroRecibo;
            filter.inPrazoPagto = entity.PrazoPagamento;
            filter.inDataRealizacao = entity.DataRealizado == DateTime.MinValue ? null : entity.DataRealizado.ToString("ddMMyy");
            filter.inNotaFiscal = entity.CodigoNotaFiscalProdesp;
            filter.inNumProcesso = entity.NumeroProcesso;
            filter.inAutFls = entity.DescricaoAutorizadoSupraFolha;
            filter.inNLRetInss = entity.NlRetencaoInss;
            filter.inLista = entity.Lista;

            filter.inCodAssinAUTO = entity.CodigoAutorizadoAssinatura;
            filter.inGrupoAssinAUTO = entity.CodigoAutorizadoGrupo.ToString();
            filter.inOrgaoAssinAUTO = entity.CodigoAutorizadoOrgao;

            filter.inCodAssinEXAM = entity.CodigoExaminadoAssinatura;
            filter.inGrupoAssinEXAM = entity.CodigoExaminadoGrupo.ToString();
            filter.inOrgaoAssinEXAM = entity.CodigoExaminadoOrgao;

            filter.inCodAssinRESP = entity.CodigoResponsavelAssinatura;
            filter.inGrupoAssinRESP = entity.CodigoResponsavelGrupo.ToString();
            filter.inOrgaoAssinRESP = entity.CodigoResponsavelOrgao;
            filter.inImprimir = "A";
        }

        private static void GetRapRequisicaoFiltersCommonFactory(string key, string password, IRap entity, ref Procedure_RequisicaoRAPFiltersType filter)
        {
            filter.inChave = password;
            filter.inOperador = key;

            filter.inNumSubEmpenho = entity.NumeroSubempenho;
            filter.inRecibo = entity.NumeroRecibo;
            filter.inCodTarefa = entity.CodigoTarefa;
            filter.inCodDespesa = entity.CodigoDespesa;
            filter.inValorRealizado = entity.ValorRealizado.ToString("D2");

#if DEBUG
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00001";
#else
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00000";
#endif

            filter.inNumProcesso = entity.NumeroProcesso;
            filter.inAutFls = entity.DescricaoAutorizadoSupraFolha;
            filter.inNLRetInss = entity.NlRetencaoInss;
            filter.inLista = entity.Lista;
            filter.inCaucaoNumGuia = entity.NumeroGuia;
            filter.inValorCaucionado = entity.ValorCaucionado.ZeroParaNulo();

            filter.inCodAssinAUTO = entity.CodigoAutorizadoAssinatura;
            filter.inGrupoAssinAUTO = entity.CodigoAutorizadoGrupo.ToString();
            filter.inOrgaoAssinAUTO = entity.CodigoAutorizadoOrgao;
            filter.inCodAssinEXAM = entity.CodigoExaminadoAssinatura;
            filter.inGrupoAssinEXAM = entity.CodigoExaminadoGrupo.ToString();
            filter.inOrgaoAssinEXAM = entity.CodigoExaminadoOrgao;

            filter.inCodAssinRESP = entity.CodigoResponsavelAssinatura;
            filter.inGrupoAssinRESP = entity.CodigoResponsavelGrupo.ToString();
            filter.inOrgaoAssinRESP = entity.CodigoResponsavelOrgao;
            filter.inImprimir = "A";
            filter.inTarefa = entity.Tarefa;
        }

        private static void GetRequisicaoOrganizacao7FiltersFactory(IRap entity, ref Procedure_RequisicaoRAPFiltersType filter)
        {
            filter.inPrazoPagto = entity.DescricaoPrazoPagamento;
            filter.inDataRealizacao = entity.DataRealizado == DateTime.MinValue ? null : entity.DataRealizado.ToString("dd/MM/yy");
            filter.inOrganizacao = entity.CodigoCredorOrganizacao.ZeroParaNulo();
            filter.inGCGCPF = entity.NumeroCNPJCPFFornecedor;
            filter.inCodEspDespesa = entity.CodigoEspecificacaoDespesa;
            filter.inReferencia = entity.Referencia;
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
            filter.inEspecificacaoDesp_04 = entity.DescricaoEspecificacaoDespesa4;
            filter.inEspecificacaoDesp_05 = entity.DescricaoEspecificacaoDespesa5;
            filter.inEspecificacaoDesp_06 = entity.DescricaoEspecificacaoDespesa6;
            filter.inEspecificacaoDesp_07 = entity.DescricaoEspecificacaoDespesa7;
            filter.inEspecificacaoDesp_08 = entity.DescricaoEspecificacaoDespesa8;
        }

        private static void GetRequisicaoReciboFiltersFactory(IRap entity, ref Procedure_RequisicaoRAPFiltersType filter)
        {
            filter.inCaucaoNumGuia = entity.NumeroGuia;
            filter.inValorCaucionado = entity.ValorCaucionado.ZeroParaNulo();
            filter.inNumeroNFF = entity.CodigoNotaFiscalProdesp;

#if DEBUG
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00001";
#else
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00000";
#endif

            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
        }

        private static void GetRequisicaoContratoFiltersFactory(IRap entity, ref Procedure_RequisicaoRAPFiltersType filter)
        {
            filter.inPrazoPagto = entity.DescricaoPrazoPagamento;
            filter.inDataRealizacao = entity.DataRealizado == DateTime.MinValue ? null : entity.DataRealizado.ToString("dd/MM/yy");
            filter.inNumMedicao = entity.NumeroMedicao;
            filter.inNatureza = entity.NaturezaSubempenhoId;
            filter.inNumeroNFF = entity.CodigoNotaFiscalProdesp;

#if DEBUG
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00001";
#else
            filter.inReferencia = entity.NumeroSiafemSiafisico?.Substring(6, 5) ?? "00000";
#endif

            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
        }

        private static void GetRequisicaoFiltersFactory(IRap entity, ref Procedure_RequisicaoRAPFiltersType filter)
        {
            filter.inPrazoPagto = entity.DescricaoPrazoPagamento;
            filter.inDataRealizacao = entity.DataRealizado == DateTime.MinValue ? null : entity.DataRealizado.ToString("dd/MM/yy");
            filter.inCodEspDespesa = entity.CodigoEspecificacaoDespesa;
            filter.inCodTarefa = entity.CodigoTarefa;
            filter.inReferencia = entity.Referencia;
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
            filter.inEspecificacaoDesp_04 = entity.DescricaoEspecificacaoDespesa4;
            filter.inEspecificacaoDesp_05 = entity.DescricaoEspecificacaoDespesa5;
            filter.inEspecificacaoDesp_06 = entity.DescricaoEspecificacaoDespesa6;
            filter.inEspecificacaoDesp_07 = entity.DescricaoEspecificacaoDespesa7;
            filter.inEspecificacaoDesp_08 = entity.DescricaoEspecificacaoDespesa8;
        }

        private static void GetRapAnulacaoFiltersCommonFactory(string key, string password, RapAnulacao entity, ref Procedure_AnulacaoRequisicaoRAPFiltersType filter)
        {
            filter.inChave = password;
            filter.inOperador = key;

            filter.inNumRequisicaoRAP = entity.NumeroRequisicaoRap;

            filter.inSaldoAnteriorSubEmp = entity.ValorSaldoAnteriorSubempenho;
            filter.inValorAnulado = entity.ValorAnulado;
            filter.inSaldoAposAnulacao = entity.ValorSaldoAposAnulacao;

            filter.inNumProcesso = entity.NumeroProcesso;
            filter.inAutFls = entity.DescricaoAutorizadoSupraFolha;

            filter.inCodAssinAUTO = entity.CodigoAutorizadoAssinatura;
            filter.inGrupoAssinAUTO = entity.CodigoAutorizadoGrupo.ToString();
            filter.inOrgaoAssinAUTO = entity.CodigoAutorizadoOrgao;
            filter.inCodAssinEXAM = entity.CodigoExaminadoAssinatura;
            filter.inGrupoAssinEXAM = entity.CodigoExaminadoGrupo.ToString();
            filter.inOrgaoAssinEXAM = entity.CodigoExaminadoOrgao;
            filter.inCodAssinRESP = entity.CodigoResponsavelAssinatura;
            filter.inGrupoAssinRESP = entity.CodigoResponsavelGrupo.ToString();
            filter.inOrgaoAssinRESP = entity.CodigoResponsavelOrgao;
            filter.inImprimir = "A";
        }

        private static void GetAnulacaoContratoFiltersFactory(RapAnulacao entity, ref Procedure_AnulacaoRequisicaoRAPFiltersType filter)
        {
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
        }

        private static void GetAnulacaoFiltersFactory(RapAnulacao entity, ref Procedure_AnulacaoRequisicaoRAPFiltersType filter)
        {
            filter.inEspecificacaoDesp = entity.CodigoEspecificacaoDespesa;
            filter.inEspecificacaoDesp_01 = entity.DescricaoEspecificacaoDespesa1;
            filter.inEspecificacaoDesp_02 = entity.DescricaoEspecificacaoDespesa2;
            filter.inEspecificacaoDesp_03 = entity.DescricaoEspecificacaoDespesa3;
            filter.inEspecificacaoDesp_04 = entity.DescricaoEspecificacaoDespesa4;
            filter.inEspecificacaoDesp_05 = entity.DescricaoEspecificacaoDespesa5;
            filter.inEspecificacaoDesp_06 = entity.DescricaoEspecificacaoDespesa6;
            filter.inEspecificacaoDesp_07 = entity.DescricaoEspecificacaoDespesa7;
            filter.inEspecificacaoDesp_08 = entity.DescricaoEspecificacaoDespesa8;
        }

        #endregion MetodosPrivados
    }

    public class WSLiquidacaoDespesa : Integracao_DER_SIAFEM_LiqDespesasService
    {
        public WSLiquidacaoDespesa()
        {
            var ambiente = AppConfig.WsUrl;

            if (ambiente == "siafemDes")
                this.Url = AppConfig.WsSubEmpenhoUrlDes;
            if (ambiente == "siafemHom")
                this.Url = AppConfig.WsSubEmpenhoUrlHom;
            if (ambiente == "siafemProd")
                this.Url = AppConfig.WsSubEmpenhoUrlProd;
        }
    }
}