using Sids.Prodesp.Infrastructure.ProdespPagtoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Extension;
using System.Linq;
using System.Threading.Tasks;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Globalization;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;


namespace Sids.Prodesp.Infrastructure.Helpers
{
    public class DataHelperProdespPagamentoContaUnica
    {
        public static Procedure_DesdobramentoISSQNRecordType[] Procedure_DesdobramentoISSQN(string key, string password, Desdobramento entity)
        {
            Procedure_DesdobramentoISSQNFiltersType desdobramentoIssqnFiltersType = DesdobramentoIssqnFiltersType(key, password, entity);

            return new WSProdespPagamentoContaUnica().Procedure_DesdobramentoISSQN(desdobramentoIssqnFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_AnulacaoDesdobramentoRecordType[] Procedure_AnulacaoDesdobramento(string key, string password, Desdobramento entity)
        {
            Procedure_AnulacaoDesdobramentoFiltersType anulacaoDesdobramentoFiltersType = AnulacaoDesdobramentoFiltersType(key, password, entity);

            return new WSProdespPagamentoContaUnica().Procedure_AnulacaoDesdobramento(anulacaoDesdobramentoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_DesdobramentoOutrosRecordType[] Procedure_DesdobramentoOutros(string key, string password, Desdobramento entity)
        {
            var desdobramentoOutrosFiltersType = DesdobramentoOutrosFiltersType(key, password, entity);

            return new WSProdespPagamentoContaUnica().Procedure_DesdobramentoOutros(desdobramentoOutrosFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_PreparacaoPagtoOrgaoRecordType[] Procedure_PreparacaoPagtoOrgao(string key, string password, PreparacaoPagamento entity, Regional orgao)
        {
            var preparacaoPagtoOrgaoFiltersType = PreparacaoPagtoOrgaoFiltersType(key, password, entity, orgao);

            return new WSProdespPagamentoContaUnica().Procedure_PreparacaoPagtoOrgao(preparacaoPagtoOrgaoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_PreparacaoPagtoDocGeradorRecordType[] Procedure_PreparacaoPagtoDocGerador(string key, string password, PreparacaoPagamento entity)
        {
            var preparacaoPagtoDocGeradorFiltersType = PreparacaoPagtoDocGeradorFiltersType(key, password, entity);

            return new WSProdespPagamentoContaUnica().Procedure_PreparacaoPagtoDocGerador(preparacaoPagtoDocGeradorFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_PreparacaoPagtoDocGeradorApoioRecordType[] Procedure_PreparacaoPagtoDocGeradorApoioRecordType(string key, string password, PreparacaoPagamento entity)
        {
            var preparacaoPagtoDocGeradorApoioFiltersType = PreparacaoPagtoDocGeradorApoioFiltersType(key, password, entity);
            return new WSProdespPagamentoContaUnica().Procedure_PreparacaoPagtoDocGeradorApoio(preparacaoPagtoDocGeradorApoioFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }



        public static Procedure_PreparacaoPagtoOrgaoApoioRecordType[] Procedure_PreparacaoPagtoOrgaoApoioRecordType(string key, string password, PreparacaoPagamento entity)
        {
            var preparacaoPagtoOrgaoApoioFiltersType = Procedure_PreparacaoPagtoOrgaoApoioFiltersType(key, password, entity);
            return new WSProdespPagamentoContaUnica().Procedure_PreparacaoPagtoOrgaoApoio(preparacaoPagtoOrgaoApoioFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_PreparacaoPagtoOrgaoApoioRecordType[] Procedure_PreparacaoPagtoOrgaoApoioRecordType2(string key, string password, ArquivoRemessa entity)
        {
            var preparacaoPagtoOrgaoApoioFiltersType = Procedure_PreparacaoPagtoOrgaoApoioFiltersType2(key, password, entity);
            return new WSProdespPagamentoContaUnica().Procedure_PreparacaoPagtoOrgaoApoio(preparacaoPagtoOrgaoApoioFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }




        public static Procedure_DesdobramentoISSQNApoioRecordType[] Procedure_DesdobramentoISSQNApoio(string key, string password, Desdobramento entity)
        {
            return new WSProdespPagamentoContaUnica().Procedure_DesdobramentoISSQNApoio(DesdobramentoIssqnApoioFiltersType(key, password, entity), new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_DesdobramentoOutrosApoioRecordType[] Procedure_DesdobramentoOutrosApoio(string key, string password, Desdobramento entity)
        {
            return new WSProdespPagamentoContaUnica().Procedure_DesdobramentoOutrosApoio(DesdobramentoOutrosApoioFiltersType(key, password, entity), new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaCredorReduzidoRecordType[] Procedure_ConsultaCredorReduzido(string key, string password, string organizacao)
        {
            var consultaCredorReduzidoFiltersType = ConsultaCredorReduzidoFiltersType(key, password, organizacao);
            return new WSProdespPagamentoContaUnica().Procedure_ConsultaCredorReduzido(consultaCredorReduzidoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaDesdobramentoRecordType[] Procedure_ConsultaDesdobramento(string key, string password, string number, int type)
        {
            Procedure_ConsultaDesdobramentoFiltersType consultaDesdobramentoFiltersType = ConsultaDesdobramentoFiltersType(key, password, number, type);
            return new WSProdespPagamentoContaUnica().Procedure_ConsultaDesdobramento(consultaDesdobramentoFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConsultaPagtosPrepararSDFFRecordType[] Procedure_ConsultaPagtosPrepararSDFF(string key, string password, ProgramacaoDesembolso programacaoDesembolso, Regional orgao)
        {
            var consultaPagtosPrepararSdffFiltersType = ConsultaPagtosPrepararSdffFiltersType(key, password, programacaoDesembolso, orgao);
            return new WSProdespPagamentoContaUnica().Procedure_ConsultaPagtosPrepararSDFF(consultaPagtosPrepararSdffFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_CancelamentoOPRecordType[] Procedure_CancelamentoOP(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var cancelamentoOpFiltersType = CancelamentoOPFiltersType(key, password, programacaoDesembolso);
            return new WSProdespPagamentoContaUnica().Procedure_CancelamentoOP(cancelamentoOpFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_BloqueioPagtoDocRecordType[] Procedure_BloqueioPagtoDoc(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var bloqueioPagtoDocFiltersType = BloqueioPagtoDocFiltersType(key, password, programacaoDesembolso);
            return new WSProdespPagamentoContaUnica().Procedure_BloqueioPagtoDoc(bloqueioPagtoDocFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_DesbloqueioPagtoDocRecordType[] Procedure_DesbloqueioPagtoDoc(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var desbloqueioPagtoDocFiltersType = DesbloqueioPagtoDocFiltersType(key, password, programacaoDesembolso);
            return new WSProdespPagamentoContaUnica().Procedure_DesbloqueioPagtoDoc(desbloqueioPagtoDocFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_BloqueioPagtoDocApoioRecordType[] Procedure_BloqueioPagtoDocApoio(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var bloqueioPagtoDocApoioFiltersType = BloqueioPagtoDocApoioFiltersType(key, password, programacaoDesembolso);
            return new WSProdespPagamentoContaUnica().Procedure_BloqueioPagtoDocApoio(bloqueioPagtoDocApoioFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_CancelamentoOPApoioRecordType[] Procedure_CancelamentoOPApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            var cancelamentoOpApoioFiltersType = CancelamentoOpApoioFiltersType(key, password, programacaoDesembolso);
            return new WSProdespPagamentoContaUnica().Procedure_CancelamentoOPApoio(cancelamentoOpApoioFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConfirmacaoPagtoOPRecordType[] Procedure_ConfirmacaoPagtoOPApoio(string key, string password, ConfirmacaoPagamento confirmacaoPagamento, Regional orgao)
        {
            Procedure_ConfirmacaoPagtoOPFiltersType confirmacaoPagtoOPFiltersType = new Procedure_ConfirmacaoPagtoOPFiltersType()
            {
                inChave = password,
                inOperador = key,
                inDataDIA = confirmacaoPagamento.DataConfirmacao.Value.ToString("dd"),
                inDataMES = confirmacaoPagamento.DataConfirmacao.Value.ToString("MM"),
                inDataANO = confirmacaoPagamento.DataConfirmacao.Value.ToString("yy"),
                inDocPagtoANO = confirmacaoPagamento.AnoReferencia?.ToString(),
                inDocPagtoORGAO = orgao.Id.ToString(),
                inDocPagtoTP = confirmacaoPagamento.IdTipoDocumento.ToString(),
                inDocPagtoOP = confirmacaoPagamento.NumeroOP.Substring(5,6),
                inTipPagto = confirmacaoPagamento.TipoPagamento?.ToString(),
                inConfirmacao = ""
            };

            return new WSProdespPagamentoContaUnica().Procedure_ConfirmacaoPagtoOP(confirmacaoPagtoOPFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConfirmacaoPagtoOPRecordType[] Procedure_ConfirmacaoPagtoOPApoio(string key, string password, PDExecucaoItem confirmacaoPagamento, Regional orgao)
        {
            Procedure_ConfirmacaoPagtoOPFiltersType confirmacaoPagtoOPFiltersType = new Procedure_ConfirmacaoPagtoOPFiltersType()
            {
                inChave = password,
                inOperador = key,
                inDataDIA = confirmacaoPagamento.Dt_confirmacao.Value.ToString("dd"),
                inDataMES = confirmacaoPagamento.Dt_confirmacao.Value.ToString("MM"),
                inDataANO = confirmacaoPagamento.Dt_confirmacao.Value.ToString("yy"),
                inDocPagtoANO = confirmacaoPagamento.AnoAserpaga?.ToString().Substring(2, 2),
                //inDocPagtoORGAO = orgao.Descricao?.Substring(2, 2),
                inDocPagtoORGAO = (confirmacaoPagamento.NumeroDocumento?.ToString() != "") ? confirmacaoPagamento.NumeroDocumento?.ToString().Substring(2, 2) : "",
                inDocPagtoTP = confirmacaoPagamento.IdTipoDocumento.ToString(),
                //inDocPagtoOP = (confirmacaoPagamento.NumeroDocumento?.ToString() != "") ? confirmacaoPagamento.NumeroDocumento?.ToString().Substring(0, 6) : "",
                inDocPagtoOP = confirmacaoPagamento.NumOP.Substring(5, 6),
                inTipPagto = confirmacaoPagamento.TipoPagamento?.ToString(),
                inConfirmacao = ""
            };

            return new WSProdespPagamentoContaUnica().Procedure_ConfirmacaoPagtoOP(confirmacaoPagtoOPFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConfirmacaoPagtoOPRecordType[] Procedure_ConfirmacaoPagtoOPApoio(string key, string password, PDExecucao confirmacaoPagamento, Regional orgao)
        {
            Procedure_ConfirmacaoPagtoOPFiltersType confirmacaoPagtoOPFiltersType = new Procedure_ConfirmacaoPagtoOPFiltersType()
            {
                inChave = password,
                inOperador = key,
                inDataDIA = confirmacaoPagamento.DataConfirmacao.Value.ToString("dd"),
                inDataMES = confirmacaoPagamento.DataConfirmacao.Value.ToString("MM"),
                inDataANO = confirmacaoPagamento.DataConfirmacao.Value.ToString("yy"),
                inDocPagtoANO = confirmacaoPagamento.Ano?.Substring(2, 2),
                //inDocPagtoORGAO = orgao.Descricao?.Substring(2, 2),
                inDocPagtoORGAO = (confirmacaoPagamento.NumeroDocumento?.ToString() != "") ? confirmacaoPagamento.NumeroDocumento?.ToString().Substring(2, 2) : "",
                inDocPagtoTP = confirmacaoPagamento.DocumentoTipoId.ToString(),
                //inDocPagtoOP = (confirmacaoPagamento.NumeroDocumento != "") ? confirmacaoPagamento.NumeroDocumento?.Substring(0, 6) : "",
                inDocPagtoOP = confirmacaoPagamento.NumOP.Substring(5, 6),
                inTipPagto = confirmacaoPagamento.TipoPagamento?.ToString(),
                inConfirmacao = ""
            };

            return new WSProdespPagamentoContaUnica().Procedure_ConfirmacaoPagtoOP(confirmacaoPagtoOPFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        public static Procedure_ConfirmacaoPagtoOPRecordType[] Procedure_ConfirmacaoPagtoOPApoio(string key, string password, OBAutorizacaoItem confirmacaoPagamento, Regional orgao)
        {
            Procedure_ConfirmacaoPagtoOPFiltersType confirmacaoPagtoOPFiltersType = new Procedure_ConfirmacaoPagtoOPFiltersType()
            {
                inChave = password,
                inOperador = key,
                inDataDIA = confirmacaoPagamento.DataConfirmacaoItem.Value.ToString("dd"),
                inDataMES = confirmacaoPagamento.DataConfirmacaoItem.Value.ToString("MM"),
                inDataANO = confirmacaoPagamento.DataConfirmacaoItem.Value.ToString("yy"),
                inDocPagtoANO = confirmacaoPagamento.AnoAserpaga?.Substring(2, 2),
                inDocPagtoORGAO = (confirmacaoPagamento.NumeroDocumento?.ToString() != "") ? confirmacaoPagamento.NumeroDocumento?.ToString().Substring(2, 2) : "",
                inDocPagtoTP = confirmacaoPagamento.DocumentoTipoId.ToString(),
                inDocPagtoOP = confirmacaoPagamento.NumOP.Length == 6 ? confirmacaoPagamento.NumOP : confirmacaoPagamento.NumOP.Substring(5, 6),
                inTipPagto = confirmacaoPagamento.TipoPagamento.ToString(),
                inConfirmacao = ""
            };

            return new WSProdespPagamentoContaUnica().Procedure_ConfirmacaoPagtoOP(confirmacaoPagtoOPFiltersType, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        private static Procedure_CancelamentoOPApoioFiltersType CancelamentoOpApoioFiltersType(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            return new Procedure_CancelamentoOPApoioFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipo = programacaoDesembolso.DocumentoTipoId == 0 ? null : programacaoDesembolso.DocumentoTipoId.ToString("D2"),
                inNumero = programacaoDesembolso.NumeroDocumento?.Replace("/", "")
            };
        }

        #region Metodos Privados

        private static Procedure_DesbloqueioPagtoDocFiltersType DesbloqueioPagtoDocFiltersType(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            return new Procedure_DesbloqueioPagtoDocFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipoDoc = programacaoDesembolso.DocumentoTipoId == 0 ? null : programacaoDesembolso.DocumentoTipoId.ToString("D2"),
                inNumeroDoc = programacaoDesembolso.NumeroDocumento?.Replace("/", ""),
                inConfirmacao = "Sim"
            };
        }

        private static Procedure_BloqueioPagtoDocApoioFiltersType BloqueioPagtoDocApoioFiltersType(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            return new Procedure_BloqueioPagtoDocApoioFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipoDoc = programacaoDesembolso.DocumentoTipoId == 0 ? null : programacaoDesembolso.DocumentoTipoId.ToString("D2"),
                inNumeroDoc = programacaoDesembolso.NumeroDocumento?.Replace("/", ""),
                inTipoBloqueio = "97"
            };
        }

        private static Procedure_BloqueioPagtoDocFiltersType BloqueioPagtoDocFiltersType(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            return new Procedure_BloqueioPagtoDocFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipoDoc = programacaoDesembolso.DocumentoTipoId == 0 ? null : programacaoDesembolso.DocumentoTipoId.ToString("D2"),
                inNumeroDoc = programacaoDesembolso.NumeroDocumento?.Replace("/", "").Replace(" ", ""),
                inTipoBloqueio = programacaoDesembolso.TipoBloqueio.ToString()
            };
        }

        private static Procedure_CancelamentoOPFiltersType CancelamentoOPFiltersType(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            return new Procedure_CancelamentoOPFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipo = programacaoDesembolso.DocumentoTipoId == 0 ? null : programacaoDesembolso.DocumentoTipoId.ToString("D2"),
                inNumero = programacaoDesembolso.NumeroDocumento?.Replace("/", "")
            };
        }

        private static Procedure_ConsultaPagtosPrepararSDFFFiltersType ConsultaPagtosPrepararSdffFiltersType(string key, string password, ProgramacaoDesembolso programacaoDesembolso, Regional orgao)
        {
            return new Procedure_ConsultaPagtosPrepararSDFFFiltersType
            {
                inChave = password,
                inOperador = key,
                inDocumento = programacaoDesembolso.NumeroDocumento?.Replace("/", ""),
                inOrgao = orgao.Descricao?.Substring(2, 2),
                inTipo = programacaoDesembolso.DocumentoTipoId == 0 ? null : programacaoDesembolso.DocumentoTipoId.ToString("D2"),
                inTipoDespesa = programacaoDesembolso.CodigoDespesa,
                inVencimentoATE = programacaoDesembolso.DataVencimento == default(DateTime) ? null : programacaoDesembolso.DataVencimento.ToString("ddMMyy"),
                inVencimentoDE = programacaoDesembolso.DataVencimento == default(DateTime) ? null : new DateTime(2000,01,01).ToString("ddMMyy")
            };
        }

        private static Procedure_AnulacaoDesdobramentoFiltersType AnulacaoDesdobramentoFiltersType(string key, string password, Desdobramento entity)
        {
            return new Procedure_AnulacaoDesdobramentoFiltersType
            {
                inChave = password,
                inOperador = key,
                inNumeroDocumento = entity.NumeroDocumento.Replace("/", ""),
                inTipoDocumento = entity.DocumentoTipoId.ToString("D2"),
                inConfirmacao = "SIM"
            };
        }

        private static Procedure_ConsultaDesdobramentoFiltersType ConsultaDesdobramentoFiltersType(string key, string password, string number, int type)
        {
            return new Procedure_ConsultaDesdobramentoFiltersType
            {
                inChave = password,
                inOperador = key,
                inNumero = number.Replace("/", ""),
                inTipoDoc = type.ToString("D2")
            };
        }

        private static Procedure_DesdobramentoOutrosFiltersType DesdobramentoOutrosFiltersType(string key, string password, Desdobramento entity)
        {
            var filter = new Procedure_DesdobramentoOutrosFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipoDocumento = entity.DocumentoTipoId.ToString("D2"),
                inNumeroDocumento = entity.NumeroDocumento.Replace("/", ""),
                inContinuar = entity.AceitaCredor ? "sim" : null
            };

            Parallel.ForEach(entity.IdentificacaoDesdobramentos, identificacaoDesdobramento =>
            {
                int index = entity.IdentificacaoDesdobramentos.ToList().IndexOf(identificacaoDesdobramento) + 1;

                SetValueISSQN("inDesdobramento_", filter, index, index.ToString("D3"));
                SetValueISSQN("inNomeReduzCredor_", filter, index, identificacaoDesdobramento.NomeReduzidoCredor);
                SetValueISSQN("inPorcentagem_", filter, index, (identificacaoDesdobramento.ValorPercentual).ZeroParaNulo()?.Replace(".", ","));
                SetValueISSQN("inValor_", filter, index, identificacaoDesdobramento.ValorDesdobrado.ZeroParaNulo()?.Replace(".", ","));
            });

            return filter;
        }

        private static Procedure_DesdobramentoISSQNApoioFiltersType DesdobramentoIssqnApoioFiltersType(string key, string password, Desdobramento entity)
        {
            var filter = new Procedure_DesdobramentoISSQNApoioFiltersType();

            filter.inChave = password;
            filter.inOperador = key;
            filter.inCodServico = entity.CodigoServico;
            filter.inNumeroDocumento = entity.NumeroDocumento.Replace("/", "");
            filter.inTipoDocumento = entity.DocumentoTipoId.ToString("D2");
            filter.inValorDistribuicao = entity.ValorDistribuido.ToString();

            return filter;
        }

        private static Procedure_DesdobramentoOutrosApoioFiltersType DesdobramentoOutrosApoioFiltersType(string key, string password, Desdobramento entity)
        {
            var filter = new Procedure_DesdobramentoOutrosApoioFiltersType();
            filter.inChave = password;
            filter.inOperador = key;
            filter.inNumeroDocumento = entity.NumeroDocumento.Replace("/", "");
            filter.inTipoDocumento = entity.DocumentoTipoId.ToString("D2");

            return filter;
        }

        private static Procedure_ConsultaCredorReduzidoFiltersType ConsultaCredorReduzidoFiltersType(string key, string password, string organizacao)
        {
            var filter = new Procedure_ConsultaCredorReduzidoFiltersType();

            filter.inChave = password;
            filter.inOperador = key;
            filter.inOrganizacao = organizacao;

            return filter;
        }

        private static Procedure_DesdobramentoISSQNFiltersType DesdobramentoIssqnFiltersType(string key, string password, Desdobramento entity)
        {
            var filter = new Procedure_DesdobramentoISSQNFiltersType
            {
                inChave = password,
                inOperador = key,
                inTipoDocumento = entity.DocumentoTipoId.ToString("D2"),
                inNumeroDocumento = entity.NumeroDocumento.Replace("/", ""),
                inCodServico = entity.CodigoServico,
                inValorDistribuicao = (entity.ValorDistribuido).ZeroParaNulo()?.Replace(".", ","),
                inContinuar = entity.AceitaCredor ? "sim" : null
            };

            foreach (var identificacaoDesdobramento in entity.IdentificacaoDesdobramentos.OrderBy(x => x.Sequencia))
            {
                int index = entity.IdentificacaoDesdobramentos.OrderBy(x => x.Sequencia).ToList().IndexOf(identificacaoDesdobramento) + 1;

                SetValueISSQN("inTipoDesd_", filter, index, identificacaoDesdobramento.DesdobramentoTipoId.ToString());
                SetValueISSQN("inCredorReduzido_", filter, index, identificacaoDesdobramento.NomeReduzidoCredor);
                SetValueISSQN("inValorDistribuicao_", filter, index, (identificacaoDesdobramento.ValorDistribuicao).ZeroParaNulo()?.Replace(".", ","));
                SetValueISSQN("inValorDesdobrado_", filter, index, (identificacaoDesdobramento.ValorDesdobrado).ZeroParaNulo()?.Replace(".", ","));
                SetValueISSQN("inNaoReter_", filter, index, identificacaoDesdobramento.ReterId.ZeroParaNulo()?.Replace(".", ","));
                SetValueISSQN("inBaseCalc_", filter, index, (identificacaoDesdobramento.ValorPercentual).ZeroParaNulo()?.Replace(".", ","));
            };

            return filter;
        }

        private static Procedure_PreparacaoPagtoOrgaoFiltersType PreparacaoPagtoOrgaoFiltersType(string key, string password, PreparacaoPagamento entity, Regional orgao)
        {
            return new Procedure_PreparacaoPagtoOrgaoFiltersType
            {
                inChave = password,
                inOperador = key,
                inAnoVencimento = entity.DataVencimento.Year.ToString().Substring(2, 2),
                inMesVencimento = entity.DataVencimento.Month.ToString("D2"),
                inDataVencimento = entity.DataVencimento.Day.ToString("D2"),
                inExercicio = entity.AnoExercicio.ToString().Substring(2, 2),
                inOrgao = orgao.Descricao.Substring(2, 2),
                inTipoDespesa = entity.CodigoDespesa,
                inAssinNumero = entity.CodigoAutorizadoAssinatura,
                inAssinGrupo = entity.CodigoAutorizadoGrupo,
                inAssinOrgao = entity.CodigoAutorizadoOrgao,
                inContraAssinNumero = entity.CodigoExaminadoAssinatura,
                inContraAssinGrupo = entity.CodigoExaminadoGrupo,
                inContraAssinOrgao = entity.CodigoExaminadoOrgao,
                inCodConta = entity.CodigoConta,
                inExibirDocs = "N"
            };
        }

        private static Procedure_PreparacaoPagtoDocGeradorFiltersType PreparacaoPagtoDocGeradorFiltersType(string key, string password, PreparacaoPagamento entity)
        {
            return new Procedure_PreparacaoPagtoDocGeradorFiltersType
            {
                inChave = password,
                inOperador = key,
                inExercicio = entity.AnoExercicio.ToString().Substring(2, 2),
                inAssinNumero = entity.CodigoAutorizadoAssinatura,
                inAssinGrupo = entity.CodigoAutorizadoGrupo,
                inAssinOrgao = entity.CodigoAutorizadoOrgao,
                inContraAssinNumero = entity.CodigoExaminadoAssinatura,
                inContraAssinGrupo = entity.CodigoExaminadoGrupo,
                inContraAssinOrgao = entity.CodigoExaminadoOrgao,
                inCodConta = entity.CodigoConta,
                inReferencia = entity.Referencia,
                inTipoDoc = entity.DocumentoTipoId.ToString("D2"),
                inNumeroDoc = entity.NumeroDocumento.Replace("/", ""),
                inValorDoc = entity.ValorDocumento.ToString(CultureInfo.CurrentCulture).Replace(".", ",")
            };
        }

        private static Procedure_PreparacaoPagtoDocGeradorApoioFiltersType PreparacaoPagtoDocGeradorApoioFiltersType(string key, string password, PreparacaoPagamento entity)
        {
            return new Procedure_PreparacaoPagtoDocGeradorApoioFiltersType
            {
                inChave = password,
                inOperador = key,
                inExercicio = entity.AnoExercicio.ToString().Substring(2, 2),
                inAssinNumero = entity.CodigoAutorizadoAssinatura,
                inAssinGrupo = entity.CodigoAutorizadoGrupo,
                inAssinOrgao = entity.CodigoAutorizadoOrgao,
                inContraAssinNumero = entity.CodigoExaminadoAssinatura,
                inContraAssinGrupo = entity.CodigoExaminadoGrupo,
                inContraAssinOrgao = entity.CodigoExaminadoOrgao,
                inCodConta = entity.CodigoConta,
                inTipoDoc = entity.DocumentoTipoId.ToString("D2"),
                inNumeroDoc = entity.NumeroDocumento.Replace("/", "").Replace("/", ""),
                inValorDoc = entity.ValorDocumento.ToString(CultureInfo.CurrentCulture).Replace(".", ",")
            };
        }

        private static Procedure_PreparacaoPagtoOrgaoApoioFiltersType Procedure_PreparacaoPagtoOrgaoApoioFiltersType(string key, string password, PreparacaoPagamento entity)
        {
            return new Procedure_PreparacaoPagtoOrgaoApoioFiltersType
            {
                
                inOperador = key,
                inChave = password,
                inExercicio = entity.AnoExercicio.ToString().Substring(2, 2),
                inCodConta = entity.CodigoConta,
                inAssinGrupo = entity.CodigoAutorizadoGrupo,
                inAssinNumero = entity.CodigoAutorizadoAssinatura,
                inAssinOrgao = entity.CodigoAutorizadoOrgao,
                inContraAssinGrupo = entity.CodigoExaminadoGrupo,
                inContraAssinNumero = entity.CodigoExaminadoAssinatura,
                inContraAssinOrgao = entity.CodigoExaminadoOrgao,
                            
            };
        }

        private static Procedure_PreparacaoPagtoOrgaoApoioFiltersType Procedure_PreparacaoPagtoOrgaoApoioFiltersType2(string key, string password, ArquivoRemessa entity)
        {
            return new Procedure_PreparacaoPagtoOrgaoApoioFiltersType
            {

                inOperador = key,
                inChave = password,
                inExercicio = entity.DataPagamento.Value.Year.ToString().Substring(2, 2),
                inCodConta = entity.CodigoConta.ToString(),
                inAssinGrupo = entity.CodigoGrupoAssinatura.ToString(),
                inAssinNumero = entity.CodigoAssinatura.ToString(),
                inAssinOrgao = entity.CodigoOrgaoAssinatura.ToString(),
                inContraAssinGrupo = entity.CodigoContraGrupoAssinatura.ToString(),
                inContraAssinNumero = entity.CodigoContraAssinatura.ToString(),
                inContraAssinOrgao = entity.CodigoContraOrgaoAssinatura.ToString(),

            };
        }



        private static void SetValueISSQN<T>(string campo, T filter, int index, string valor)
        {
            var filteProperties = filter.GetType().GetProperties().Where(x => x.Name == campo + index.ToString("D2"));

            Parallel.ForEach(filteProperties, filterPropriety =>
            {
                filterPropriety.SetValue(filter, valor);
            });
        }

        #endregion Metodos Privados
    }


    public class WSProdespPagamentoContaUnica : Integracao_DER_SIAFEM_PagtoContaUnicaService
    {
        public WSProdespPagamentoContaUnica() : base()
        {
            var ambiente = AppConfig.WsUrl;

            if (ambiente == "siafemDes")
                this.Url = AppConfig.WsPgtoContaUnicaUrlDes;
            if (ambiente == "siafemHom")
                this.Url = AppConfig.WsPgtoContaUnicaUrlHom;
            if (ambiente == "siafemProd")
                this.Url = AppConfig.WsPgtoContaUnicaUrlProd;
        }
    }
}