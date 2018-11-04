using System;
using System.Linq;
using Sids.Prodesp.Infrastructure.Helpers;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text.RegularExpressions;
using Sids.Prodesp.Model.Interface.Service.Movimentacao;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Entity.Configuracao;

namespace Sids.Prodesp.Infrastructure.Services.Movimentacao
{
    public class ProdespMovimentacaoWs : IProdespMovimentacao
    {
        public object PreparacaoArquivoRemessa(string key, string password, PreparacaoPagamento objModel, string impressora)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_PreparacaoArquiRemessa(key, password, objModel, impressora);
                var resultItem = result.FirstOrDefault();





                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }

        }
        public object ImpressaoRelacaoOD(string key, string password, PreparacaoPagamento objModel, string impressora)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_ImpressaoRelacaoOD(key, password, objModel, impressora);
                var resultItem = result.FirstOrDefault();
                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }

        public List<ConfirmacaoPagamento> ConsultaPagtosConfirmarSIDS(string key, string password, string impressora, ConfirmacaoPagamento entrada)
        {
            try
            {

                List<ConfirmacaoPagamento> resultreturn = new List<ConfirmacaoPagamento>();
                var result = DataHelperProdespPagementoContaDer.Procedure_ConsultaPagtosConfirmarSIDS(key, password, impressora, entrada);
                var resultItem = result.FirstOrDefault();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new FaultException($"Prodesp - {resultItem?.outErro}");
                else
                {
                    var index = 0;
                    foreach (var item in result)
                    {
                        ConfirmacaoPagamento confpag = new ConfirmacaoPagamento();
                        confpag.NumeroOP = !string.IsNullOrEmpty(item.outNumOP) ? item.outNumOP : string.Empty;
                        confpag.Orgao = !string.IsNullOrEmpty(item.outOrgao) ? item.outOrgao : string.Empty;
                        confpag.TipoDespesa = !string.IsNullOrEmpty(item.outDespesa) ? item.outDespesa : string.Empty;
                        confpag.DataVencimento = !string.IsNullOrEmpty(item.outVencimento) ? item.outVencimento : string.Empty;
                        confpag.DataPreparacao = !string.IsNullOrEmpty(item.outDataPreparacao) ? Convert.ToDateTime(item.outDataPreparacao) : DateTime.MinValue;
                        confpag.TipoDocumento = entrada.IdTipoDocumento.HasValue ? entrada.IdTipoDocumento.Value.ToString() : string.Empty;
                        confpag.NumeroDocumento = !string.IsNullOrEmpty(item.outNumDoc) ? item.outNumDoc : string.Empty;
                        confpag.NLDocumento = !string.IsNullOrEmpty(item.outNL) ? item.outNL : string.Empty;
                        confpag.NumeroContrato = !string.IsNullOrEmpty(item.outContrato) ? item.outContrato : string.Empty;
                        confpag.CodigoObra = !string.IsNullOrEmpty(item.outCodObra) ? item.outCodObra : string.Empty;
                        confpag.NumeroOP = !string.IsNullOrEmpty(item.outNumOP) ? item.outNumOP : string.Empty;
                        confpag.Banco = !string.IsNullOrEmpty(item.outContaPagtoBanco) ? item.outContaPagtoBanco : string.Empty;
                        confpag.Agencia = !string.IsNullOrEmpty(item.outContaPagtoAgencia) ? item.outContaPagtoAgencia : string.Empty;
                        confpag.Conta = !string.IsNullOrEmpty(item.outContaPagtoConta) ? item.outContaPagtoConta : string.Empty;
                        confpag.FonteSIAFEM = !string.IsNullOrEmpty(item.outFonteSiafem) ? item.outFonteSiafem : string.Empty;
                        confpag.NumeroEmpenho = !string.IsNullOrEmpty(item.outNE) ? item.outNE : string.Empty;
                        confpag.NumeroProcesso = !string.IsNullOrEmpty(item.outProcesso) ? item.outProcesso : string.Empty;
                        confpag.NotaFiscal = !string.IsNullOrEmpty(item.outNotaFiscal) ? item.outNotaFiscal : string.Empty;
                        confpag.ValorDocumento = !string.IsNullOrEmpty(item.outValorDocumento) ? item.outValorDocumento : string.Empty;
                        confpag.NaturezaDespesa = !string.IsNullOrEmpty(item.outNaturezaDesp) ? item.outNaturezaDesp : string.Empty;
                        confpag.CredorOrganizacaoCredorOriginal = !string.IsNullOrEmpty(item.outCredorOriginalReduz) ? item.outCredorOriginalReduz : string.Empty;
                        confpag.CPFCNPJCredorOriginal = !string.IsNullOrEmpty(item.outNumDoc) ? item.outNumDoc : string.Empty;
                        confpag.CredorOriginal = !string.IsNullOrEmpty(item.outCodCredor) ? item.outCodCredor : string.Empty;
                        confpag.Referencia = !string.IsNullOrEmpty(item.outReferencia) ? item.outReferencia : string.Empty;
                        confpag.CredorOrganizacao = !string.IsNullOrEmpty(item.outCodCredorDocumento) ? item.outCodCredorDocumento : string.Empty;
                        confpag.CPF_CNPJ_Credor = !string.IsNullOrEmpty(item.outCodCredor) ? item.outCodCredor : string.Empty;
                        confpag.NomeReduzidoCredor = !string.IsNullOrEmpty(item.outEmpresa) ? item.outEmpresa : string.Empty;
                        confpag.ValorDesdobradoCredor = !string.IsNullOrEmpty(item.outValorDesdobrado) ? item.outValorDesdobrado : string.Empty;
                        confpag.BancoFavorecido = !string.IsNullOrEmpty(item.outContaFavBanco) ? item.outContaFavBanco : string.Empty;
                        confpag.AgenciaFavorecido = !string.IsNullOrEmpty(item.outContaFavAgencia) ? item.outContaFavAgencia : string.Empty;
                        confpag.ContaFavorecido = !string.IsNullOrEmpty(item.outContaFavConta) ? item.outContaFavConta : string.Empty;

                        confpag.ValorConfirmacao = !string.IsNullOrEmpty(item.outValorDocumento) ? Convert.ToDecimal(item.outValorDocumento) : 0;
                        confpag.Fonte = !string.IsNullOrEmpty(item.outFonte) ? item.outFonte : string.Empty;
                        confpag.ValorTotalConfirmarISSQN = !string.IsNullOrEmpty(item.outTotalConfirmar) ? item.outTotalConfirmar : string.Empty;
                        confpag.ValorTotalConfirmarIR = !string.IsNullOrEmpty(item.outValorDocumento) ? item.outValorDocumento : string.Empty;
                        confpag.ValorTotalConfirmado = !string.IsNullOrEmpty(item.outTotalConfirmar) ? Convert.ToDecimal(item.outTotalConfirmar) : 0;


                        //confpag.NumeroNLBaixaRepasse = !string.IsNullOrEmpty(item.outNL) ? item.outNL : string.Empty;
                        //confpag.CPF_CNPJ = !string.IsNullOrEmpty(item.out) ? item.outDespesa : string.Empty;
                        //confpag.NumeroEmpenho = !string.IsNullOrEmpty(item.out) ? item.outContaPagtoBanco : string.Empty;
                        //confpag.StatusProdesp = !string.IsNullOrEmpty(item.outs) ? item.outValorDesdobrado : string.Empty;
                        //confpag.TransmissaoConfirmacao = !string.IsNullOrEmpty(item.out) ? item.outContaPagtoConta : string.Empty;

                        confpag.Totalizador = index == result.Length - 1;

                        resultreturn.Add(confpag);
                        index++;
                    }
                }


                return resultreturn;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }

        public string ConsultaOP(string key, string password, string numeroDocumentoGerador)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_ConsultaOP(key, password, numeroDocumentoGerador);
                var resultItem = result[0];

                //if (!string.IsNullOrEmpty(resultItem?.outErro))
                //  throw new Exception($"Prodesp - {resultItem?.outErro}");

                var entityProgramacaoDesembolso = new ProgramacaoDesembolso();
                entityProgramacaoDesembolso.OP = resultItem?.outNumeroOP + "@" + resultItem?.outErro;
                //entityProgramacaoDesembolso.ProdespConsultaOPMensagemErro = resultItem?.outErro;

                return entityProgramacaoDesembolso.OP;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }

        public object PreparacaoArquivoRemessa(string key, string password, ArquivoRemessa objModel, string impressora)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_PreparacaoArquiRemessa2(key, password, objModel, impressora);
                var resultItem = result.FirstOrDefault();


                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public object CancelamentoArquivoRemessa(string key, string password, ArquivoRemessa objModel, string impressora)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_CancelamentoArquiRemessa(key, password, objModel, impressora);
                var resultItem = result.FirstOrDefault();





                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        //public object ConsultaArquivoPreparado(string key, string password, ArquivoRemessa objModel, string impressora)
        //{
        //    try
        //    {

        //        var result = DataHelperProdespPagementoContaDer.Procedure_ConsultaArquivoPreparado(key, password, objModel, impressora);
        //        var resultItem = result.FirstOrDefault();


        //        if (!string.IsNullOrEmpty(resultItem?.outErro))
        //            throw new Exception($"Prodesp - {resultItem?.outErro}");
        //        return resultItem;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
        //            ? "Erro na comunicação com WebService Prodesp."
        //            : ex.Message);
        //    }
        //}

        public object ConsultaArquivoPreparado(string key, string password, ArquivoRemessa objModel, string impressora)
        {
            try
            {

                var result = DataHelperProdespPagementoContaDer.Procedure_ConsultaArquivoPreparado(key, password, objModel, impressora);
                var resultItem = result.FirstOrDefault();


                string patters = @"^(\w+.{11})\s+(\w+.{21})\s+(\d.+)";

                ArquivoRemessa arquivoRemessa = new ArquivoRemessa();

                arquivoRemessa.NumeroGeracao = Convert.ToInt32(resultItem.outNGA);
                arquivoRemessa.Orgao = resultItem.outOrgao;
                arquivoRemessa.Situacao = resultItem.outSituacao;
                arquivoRemessa.CodigoConta = Convert.ToInt32(resultItem.outCodConta);
                arquivoRemessa.Banco = resultItem.outConta.Substring(0, 3);
                arquivoRemessa.Agencia = resultItem.outConta.Substring(4, 5);
                arquivoRemessa.NumeroConta = resultItem.outConta.Substring(12, 12);

                arquivoRemessa.DataPreparacao = Convert.ToDateTime(resultItem.outDataPrepArq);
                arquivoRemessa.DataPagamento = Convert.ToDateTime(resultItem.outDataPagto);
                arquivoRemessa.DataGeracao = resultItem.outDataGeracao;

                arquivoRemessa.dataPrevia = resultItem.outDataRetornoPrevia;
                arquivoRemessa.ResultadoPrevia = resultItem.outResultadoPrev;
                arquivoRemessa.dataProcessamento = resultItem.outDataRetornoProces;
                arquivoRemessa.ResultadoProcessamento = resultItem.outResultadoProces;
                arquivoRemessa.dataConsolidado = resultItem.outDataRetornoConsol;
                arquivoRemessa.ResultadoConsolidado = resultItem.outResultadoConsol;

                arquivoRemessa.QtOpArquivo = Convert.ToInt32(resultItem.outQtdadeOPArq.Replace(",", "").Replace(".", ""));
                arquivoRemessa.ValorTotal = Convert.ToInt32(resultItem.outValorTotal.Replace(",", "").Replace(".", ""));
                arquivoRemessa.QtDeposito = Convert.ToInt32(resultItem.outQtdadeDepositos.Replace(",", "").Replace(".", ""));
                arquivoRemessa.ValorDep = "R$ " + resultItem.outValorDeposito;
                arquivoRemessa.QtDocTed = Convert.ToInt32(resultItem.outQtdadeDocTed.Replace(",", "").Replace(".", ""));
                arquivoRemessa.ValorDocTed = "R$ " + resultItem.outValorDocTed;
                arquivoRemessa.ValorCreditado = "R$ " + resultItem.outValorTotalCreditado;
                arquivoRemessa.ValorNaoCreditado = "R$ " + resultItem.outValorNaoCreditado;

                arquivoRemessa.ListOps = new System.Collections.Generic.List<ArquivoRemessaOP>();



                foreach (var item in result)
                {
                    foreach (Match match in Regex.Matches(item.outOP, patters))
                    {
                        ArquivoRemessaOP arquivoOP = new ArquivoRemessaOP();
                        arquivoOP.OP = match.Groups[1].Value;
                        arquivoOP.ContaCredito = match.Groups[2].Value;
                        arquivoOP.Valor = match.Groups[3].Value;


                        arquivoRemessa.ListOps.Add(arquivoOP);

                    }





                }




                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");
                return arquivoRemessa;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public object ImpressaoReemissaoRelacaoOD(string key, string password, ArquivoRemessa objModel, string impressora)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_ReemissaoRelacaoOD(key, password, objModel, impressora);
                var resultItem = result.FirstOrDefault();
                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }

        public object PreparacaoMovimentacaoInterna(string key, string password, string impressora, Programa programa,Estrutura estrutura, List<MovimentacaoReducaoSuplementacao> items)
        {
            try
            {
                var result = DataHelperProdespMovimentacao.Procedure_MovOrcamentariaInterna(key, password, impressora, programa, estrutura, items);
                var resultItem = result.FirstOrDefault();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }
    }
}
