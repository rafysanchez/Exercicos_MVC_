using System;
using Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;

namespace Sids.Prodesp.Infrastructure.Helpers
{
    public class DataHelperProdespPagementoContaDer
    {
        public static Procedure_PreparacaoArquiRemessaRecordType[] Procedure_PreparacaoArquiRemessa(string key, string password, PreparacaoPagamento entity, string impressora)
        {
            Procedure_PreparacaoArquiRemessaFiltersType preparacaoArquiRemessaFiltersType = PreparacaoArquiRemessaFiltersType(key, password, entity, impressora);

            return new WSPagamentoContaDer().Procedure_PreparacaoArquiRemessa(preparacaoArquiRemessaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }


        public static Procedure_CancelamentoArquiRemessaRecordType[] Procedure_CancelamentoArquiRemessa(string key, string password, ArquivoRemessa entity, string impressora)
        {
            Procedure_CancelamentoArquiRemessaFiltersType cancelamentoArquiRemessaFiltersType = CancelamentoArquiRemessaFiltersType(key, password, entity, impressora);

            return new WSPagamentoContaDer().Procedure_CancelamentoArquiRemessa(cancelamentoArquiRemessaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ReemissaoRelacaoODRecordType[] Procedure_ReemissaoRelacaoOD(string key, string password, ArquivoRemessa entity, string impressora)
        {
            Procedure_ReemissaoRelacaoODFiltersType filtertype = ImpressaoReemissaoRelacaoODRecordType(key, password, entity, impressora);

            var result = new WSPagamentoContaDer().Procedure_ReemissaoRelacaoOD(filtertype, new ModelVariablesType(), new EnvironmentVariablesType());

            return result;
        }




        public static Procedure_ImpressaoRelacaoODRecordType[] Procedure_ImpressaoRelacaoOD(string key, string password, PreparacaoPagamento entity, string impressora)
        {
            Procedure_ImpressaoRelacaoODFiltersType filtertype = ImpressaoRelacaoODRecordType(key, password, entity, impressora);

            var result = new WSPagamentoContaDer().Procedure_ImpressaoRelacaoOD(filtertype, new ModelVariablesType(), new EnvironmentVariablesType());

            return result;
        }

        public static Procedure_ConsultaOPDocGeradorRecordType[] Procedure_ConsultaOP(string key, string password, string numeroDocumentoGerador)
        {
            Procedure_ConsultaOPDocGeradorFiltersType filtertype = ConsultaOPDocGeradorRecordType(key, password, numeroDocumentoGerador);

            var result = new WSPagamentoContaDer().Procedure_ConsultaOPDocGerador(filtertype, new ModelVariablesType(), new EnvironmentVariablesType());

            return result;
        }

        public static Procedure_ConsultaPagtosConfirmarSIDSRecordType[] Procedure_ConsultaPagtosConfirmarSIDS(string key, string password, string impressora, ConfirmacaoPagamento entrada)
        {
            Procedure_ConsultaPagtosConfirmarSIDSFiltersType filtertype = Procedure_ConsultaPagtosConfirmarSIDSFiltersType(key, password, impressora, entrada);
            return new WSPagamentoContaDer().Procedure_ConsultaPagtosConfirmarSIDS(filtertype, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        private static Procedure_PreparacaoArquiRemessaFiltersType PreparacaoArquiRemessaFiltersType(string key, string password, PreparacaoPagamento entity, string impressora)
        {
            return new Procedure_PreparacaoArquiRemessaFiltersType
            {
                inChave = password,
                inOperador = key,
                inAssinGrupo = entity.CodigoAutorizadoGrupo,
                inAssinNumero = entity.CodigoAutorizadoAssinatura,
                inAssinOrgao = entity.CodigoAutorizadoOrgao,
                inContraAssinGrupo = entity.CodigoExaminadoGrupo,
                inContraAssinNumero = entity.CodigoExaminadoAssinatura,
                inContraAssinOrgao = entity.CodigoExaminadoOrgao,
                inCodConta = entity.CodigoConta,
                inImpressora = impressora,
                inDataPagamentoANO = DateTime.Now.Year.ToString().Substring(2, 2),
                inDataPagamentoDIA = DateTime.Now.Day.ToString("D2"),
                inDataPagamentoMES = DateTime.Now.Month.ToString("D2"),
                inDataPreparacaoANO = entity.DataTransmitidoProdesp.Year.ToString().Substring(2, 2),
                inDataPreparacaoMES = entity.DataTransmitidoProdesp.Month.ToString("D2"),
                inDataPreparacaoDIA = entity.DataTransmitidoProdesp.Day.ToString("D2")
            };
        }


        private static Procedure_CancelamentoArquiRemessaFiltersType CancelamentoArquiRemessaFiltersType(string key, string password, ArquivoRemessa entity, string impressora)
        {
            return new Procedure_CancelamentoArquiRemessaFiltersType
            {
                inChave = password,
                inOperador = key,
                inImpressora = impressora,
                inCodConta = entity.CodigoConta.ToString(),
                inNumGerArquivo = entity.NumeroGeracao.ToString(),
                InConfirmacao = "SIM"

            };
        }

        private static Procedure_ImpressaoRelacaoODFiltersType ImpressaoRelacaoODRecordType(string operador, string password, PreparacaoPagamento entity, string impressora)
        {
            var obj = new Procedure_ImpressaoRelacaoODFiltersType();

            obj.inOperador = operador;
            obj.inChave = password;
            obj.inImpressora = impressora;

            var dataPrep = entity.DataTransmitidoProdesp;
            obj.inCodigoConta = entity.CodigoConta;
            obj.inDataPrepOPDIA = dataPrep.Day.ToString();
            obj.inDataPrepOPMES = dataPrep.Month.ToString();
            obj.inDataPrepOPANO = dataPrep.Year.ToString().Substring(2, 2);
            obj.inAssinGrupo = entity.CodigoAutorizadoGrupo;
            obj.inAssinNumero = entity.CodigoAutorizadoAssinatura;
            obj.inAssinOrgao = entity.CodigoAutorizadoOrgao;
            obj.inContraAssinGrupo = entity.CodigoExaminadoGrupo;
            obj.inContraAssinNumero = entity.CodigoExaminadoAssinatura;
            obj.inContraAssinOrgao = entity.CodigoExaminadoOrgao;

            return obj;
        }

        private static Procedure_ConsultaOPDocGeradorFiltersType ConsultaOPDocGeradorRecordType(string operador, string password, string numeroDocumentoGerador)
        {
            var obj = new Procedure_ConsultaOPDocGeradorFiltersType();

            obj.inOperador = operador;
            obj.inChave = password;
            obj.inNumeroDoc = numeroDocumentoGerador;

            return obj;
        }

        private static Procedure_ConsultaPagtosConfirmarSIDSFiltersType Procedure_ConsultaPagtosConfirmarSIDSFiltersType(string operador, string password, string impressora, ConfirmacaoPagamento entrada)
        {
            var obj = new Procedure_ConsultaPagtosConfirmarSIDSFiltersType();

            obj.inOperador = operador;
            obj.inChave = password;
            obj.inImpressora = impressora;
            obj.inDocumento = string.IsNullOrWhiteSpace(entrada.NumeroDocumento) ? null : entrada.NumeroDocumento.Replace("/", string.Empty);
            obj.inTipo = entrada.TipoDocumento.ToString();
            obj.inConta = entrada.NumeroConta;
            obj.inDataPreparacao = entrada.DataPreparacao.HasValue ? entrada.DataPreparacao.Value.ToString("ddMMyy") : null;
            obj.inDataConfirmacao = entrada.DataConfirmacao.HasValue? entrada.DataConfirmacao.Value.ToString("ddMMyy") : null;

            return obj;
        }


        private static Procedure_PreparacaoArquiRemessaFiltersType PreparacaoArquiRemessaFiltersType(string key, string password, ArquivoRemessa entity, string impressora)
        {
            return new Procedure_PreparacaoArquiRemessaFiltersType
            {
                inChave = password,
                inImpressora = impressora,
                inOperador = key,

                inAssinGrupo = entity.CodigoGrupoAssinatura,
                inAssinNumero = entity.CodigoAssinatura,
                inAssinOrgao = entity.CodigoOrgaoAssinatura,
                inCodConta = entity.CodigoConta.ToString(),

                inContraAssinGrupo = entity.CodigoContraGrupoAssinatura,
                inContraAssinNumero = entity.CodigoContraAssinatura,
                inContraAssinOrgao = entity.CodigoContraOrgaoAssinatura,

                inDataPagamentoANO = entity.DataPagamento.Value.Year.ToString().Substring(2, 2),
                inDataPagamentoDIA = entity.DataPagamento.Value.Day.ToString("D2"),
                inDataPagamentoMES = entity.DataPagamento.Value.Month.ToString("D2"),
                inDataPreparacaoANO = entity.DataPreparacao.Value.Year.ToString().Substring(2, 2),
                inDataPreparacaoDIA = entity.DataPreparacao.Value.Day.ToString("D2"),
                inDataPreparacaoMES = entity.DataPreparacao.Value.Month.ToString("D2")


            };
        }





        private static Procedure_PreparacaoArquiRemessaApoioFiltersType PreparacaoArquiRemessaFiltersType2(string key, string password, ArquivoRemessa entity, string impressora)
        {
            return new Procedure_PreparacaoArquiRemessaApoioFiltersType
            {
                inChave = password,
                inImpressora = impressora,
                inOperador = key,

                inAssinGrupo = entity.CodigoGrupoAssinatura,
                inAssinNumero = entity.CodigoAssinatura,
                inAssinOrgao = entity.CodigoOrgaoAssinatura,
                inCodConta = entity.CodigoConta.ToString(),

                inContraAssinGrupo = entity.CodigoContraGrupoAssinatura,
                inContraAssinNumero = entity.CodigoContraAssinatura,
                inContraAssinOrgao = entity.CodigoContraOrgaoAssinatura,

                inDataPagamentoANO = entity.DataPagamento.Value.Year.ToString().Substring(2, 2),
                inDataPagamentoDIA = entity.DataPagamento.Value.Day.ToString("D2"),
                inDataPagamentoMES = entity.DataPagamento.Value.Month.ToString("D2"),
                inDataPreparacaoANO = entity.DataPreparacao.Value.Year.ToString().Substring(2, 2),
                inDataPreparacaoDIA = entity.DataPreparacao.Value.Day.ToString("D2"),
                inDataPreparacaoMES = entity.DataPreparacao.Value.Month.ToString("D2")


            };
        }


        private static Procedure_ReemissaoRelacaoODFiltersType ImpressaoReemissaoRelacaoODRecordType(string operador, string password, ArquivoRemessa entity, string impressora)
        {
            var obj = new Procedure_ReemissaoRelacaoODFiltersType();

            obj.inOperador = operador;
            obj.inChave = password;
            obj.inImpressora = impressora;

            obj.inCodigoConta = entity.CodigoConta.ToString();
            obj.inDataPrepArquivoDIA = entity.DataTrasmitido.Value.Day.ToString("D2");
            obj.inDataPrepArquivoMES = entity.DataTrasmitido.Value.Month.ToString("D2");
            obj.inDataPrepArquivoANO = entity.DataTrasmitido.Value.Year.ToString().Substring(2, 2);

            obj.inAssinGrupo = entity.CodigoGrupoAssinatura.ToString();
            obj.inAssinNumero = entity.CodigoAssinatura.ToString();
            obj.inAssinOrgao = entity.CodigoOrgaoAssinatura.ToString();
            obj.inContraAssinGrupo = entity.CodigoContraGrupoAssinatura.ToString();
            obj.inContraAssinNumero = entity.CodigoContraAssinatura.ToString();
            obj.inContraAssinOrgao = entity.CodigoContraOrgaoAssinatura.ToString();

            obj.inNGA = entity.NumeroGeracao.ToString().PadLeft(6, '0');
            obj.inSel = entity.SelArquivo;

            return obj;
        }




        public static Procedure_PreparacaoArquiRemessaApoioRecordType[] Procedure_PreparacaoArquiRemessa2(string key, string password, ArquivoRemessa entity, string impressora)
        {
            Procedure_PreparacaoArquiRemessaApoioFiltersType preparacaoArquiRemessaFiltersType = PreparacaoArquiRemessaFiltersType2(key, password, entity, impressora);

            return new WSPagamentoContaDer().Procedure_PreparacaoArquiRemessaApoio(preparacaoArquiRemessaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }


        public static Procedure_PreparacaoArquiRemessaRecordType[] Procedure_PreparacaoArquiRemessa(string key, string password, ArquivoRemessa entity, string impressora)
        {
            Procedure_PreparacaoArquiRemessaFiltersType preparacaoArquiRemessaFiltersType = PreparacaoArquiRemessaFiltersType(key, password, entity, impressora);

            return new WSPagamentoContaDer().Procedure_PreparacaoArquiRemessa(preparacaoArquiRemessaFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }















        public static Procedure_ConsultaArquivoPreparadoRecordType[] Procedure_ConsultaArquivoPreparado(string key, string password, ArquivoRemessa entity, string impressora)
        {
            Procedure_ConsultaArquivoPreparadoFiltersType consultaArquivoPreparado = ConsultaArquivoPreparadoFiltersType(key, password, entity, impressora);

            return new WSPagamentoContaDer().Procedure_ConsultaArquivoPreparado(consultaArquivoPreparado, new ModelVariablesType(), new EnvironmentVariablesType());
        }





        private static Procedure_ConsultaArquivoPreparadoFiltersType ConsultaArquivoPreparadoFiltersType(string key, string password, ArquivoRemessa entity, string impressora)
        {
            return new Procedure_ConsultaArquivoPreparadoFiltersType
            {
                inChave = password,
                inImpressora = impressora,
                inOperador = key,

                inCodConta = entity.CodigoConta.ToString(),

                inNGA = entity.NumeroGeracao.ToString().PadLeft(5,'0'),

                //inDataPrepDE_dia = entity.DataPreparacao.Value.Day.ToString("D2"),
                //inDataPrepDE_mes= entity.DataPreparacao.Value.Month.ToString("D2"),
                //inDataPrepDE_ano = entity.DataPreparacao.Value.Year.ToString().Substring(2, 2),

                //inDataPrepATE_dia = entity.DataPreparacao.Value.Day.ToString("D2"),
                //inDataPrepATE_mes = entity.DataPreparacao.Value.Month.ToString("D2"),
                //inDataPrepATE_ano = entity.DataPreparacao.Value.Year.ToString().Substring(2, 2),


                inSelecao = "x"

            };
        }



    }










    public class WSPagamentoContaDer : Integracao_DER_SIAFEM_PagtoContaDERService
    {
        public WSPagamentoContaDer() : base()
        {
            var ambiente = AppConfig.WsUrl;

            if (ambiente == "siafemDes")
                this.Url = AppConfig.WsPgtoContaDerUrlDes;
            if (ambiente == "siafemHom")
                this.Url = AppConfig.WsPgtoContaDerUrlHom;
            if (ambiente == "siafemProd")
                this.Url = AppConfig.WsPgtoContaDerUrlProd;

        }
    }
}
