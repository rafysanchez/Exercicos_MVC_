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
using Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer;
using Sids.Prodesp.Model.Exceptions;

namespace Sids.Prodesp.Infrastructure.Services.PagamentoContaDer
{
    public class ProdespPagamentoContaDerWs : IProdespPagamentoContaDer
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
                var MantemDataConfirmacao = entrada.DataConfirmacao;
                List<ConfirmacaoPagamento> resultreturn = new List<ConfirmacaoPagamento>();
                if(entrada.TipoConfirmacao == 1)
                {
                    entrada.DataPreparacao = null;
                    entrada.DataConfirmacao = null;
                }

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

                        //Campos vindo da tela - rumo a tabela Pai
                        confpag.ContaProdesp = entrada.NumeroConta;
                        confpag.TipoConfirmacao = entrada.TipoConfirmacao;
                        confpag.NumeroDocumento = !string.IsNullOrEmpty(entrada.NumeroDocumento) ? entrada.NumeroDocumento : string.Empty;
                        confpag.TipoDocumento = !string.IsNullOrEmpty(entrada.TipoDocumento) ? entrada.TipoDocumento : string.Empty;
                        confpag.TipoPagamento = entrada.TipoPagamento;
                        confpag.DataPreparacao =  entrada.DataPreparacao;
                        //confpag.DataPreparacao = !string.IsNullOrEmpty(item.outDataPreparacao) ? Convert.ToDateTime(item.outDataPreparacao) : DateTime.MinValue;
                        confpag.DataConfirmacao = MantemDataConfirmacao;
                        confpag.DataCadastro = DateTime.Now;
                        confpag.AnoReferencia = !string.IsNullOrEmpty(item.outNumOP) ? Convert.ToInt16(item.outNumOP.Substring(0, 2)) : 0;

                        //Campos vindo do mainframe - rumo a tabela filho

                        confpag.TipoDocumentoItem = !string.IsNullOrEmpty(item.outNumDoc) ? item.outNumDoc.Substring(0, 2) : string.Empty; //antigo 
                        confpag.NumeroDocumentoItem = !string.IsNullOrEmpty(item.outNumDoc) ? item.outNumDoc : string.Empty; //antigo

                        confpag.NumeroOP = !string.IsNullOrEmpty(item.outNumOP) ? item.outNumOP : string.Empty; 

                        confpag.numeroEmpenhoSiafem = !string.IsNullOrEmpty(item.outNE) ? item.outNE : string.Empty;
                        confpag.Totalizador = index == result.Length - 1;

                        confpag.NLDocumento = !string.IsNullOrEmpty(item.outNL) ? item.outNL : string.Empty;
                        confpag.NumeroContrato = !string.IsNullOrEmpty(item.outContrato) ? item.outContrato : string.Empty;
                        confpag.CodigoObra = !string.IsNullOrEmpty(item.outCodObra) ? item.outCodObra : string.Empty;
                        
                        confpag.NumeroProcesso = !string.IsNullOrEmpty(item.outProcesso) ? item.outProcesso : string.Empty;
                        confpag.NotaFiscal = !string.IsNullOrEmpty(item.outNotaFiscal) ? item.outNotaFiscal : string.Empty;
                        confpag.NaturezaDespesa = !string.IsNullOrEmpty(item.outNaturezaDesp) ? item.outNaturezaDesp : string.Empty;
 
                        confpag.DataVencimento = !string.IsNullOrEmpty(item.outVencimento) ? item.outVencimento : string.Empty;
                          
                        confpag.Orgao = !string.IsNullOrEmpty(item.outOrgao) ? item.outOrgao : string.Empty;
                        confpag.TipoDespesa = !string.IsNullOrEmpty(item.outDespesa) ? item.outDespesa : string.Empty;
                             
                        confpag.Referencia = !string.IsNullOrEmpty(item.outReferencia) ? item.outReferencia : string.Empty;
                        confpag.FonteSIAFEM = !string.IsNullOrEmpty(item.outFonteSiafem) ? item.outFonteSiafem : string.Empty;

                        confpag.Banco = !string.IsNullOrEmpty(item.outContaPagtoBanco) ? item.outContaPagtoBanco : string.Empty;
                        confpag.Agencia = !string.IsNullOrEmpty(item.outContaPagtoAgencia) ? item.outContaPagtoAgencia : string.Empty;
                        confpag.Conta = !string.IsNullOrEmpty(item.outContaPagtoConta) ? item.outContaPagtoConta : string.Empty;
                        
                        confpag.BancoFavorecido = !string.IsNullOrEmpty(item.outContaFavBanco) ? item.outContaFavBanco : string.Empty;
                        confpag.AgenciaFavorecido = !string.IsNullOrEmpty(item.outContaFavAgencia) ? item.outContaFavAgencia : string.Empty;
                        confpag.ContaFavorecido = !string.IsNullOrEmpty(item.outContaFavConta) ? item.outContaFavConta : string.Empty;


                        //CREDOR
                        confpag.NomeReduzidoCredor = !string.IsNullOrEmpty(item.outEmpresa) ? item.outEmpresa : string.Empty;
                        confpag.CPF_CNPJ_Credor = !string.IsNullOrEmpty(item.outCodCredor) ? item.outCodCredor : string.Empty;
                        confpag.CredorOrganizacaoCredorDocto = !string.IsNullOrEmpty(item.outOrganiz) ? item.outOrganiz : string.Empty;


                        //CREDOR DO DOCUMENTO
                        confpag.NomeReduzidoCredorDocto = !string.IsNullOrEmpty(item.outCredorOriginalReduz) ? item.outCredorOriginalReduz : string.Empty;
                        confpag.CPFCNPJCredorOriginal = !string.IsNullOrEmpty(item.outCodCredorDocumento) ? item.outCodCredorDocumento : string.Empty;
                        confpag.CredorOrganizacaoCredorOriginal = !string.IsNullOrEmpty(item.outOrganizDocumento) ? item.outOrganizDocumento : string.Empty;                        
                        //confpag.CredorOriginal = !string.IsNullOrEmpty(item.outCodCredorDocumento) ? item.outCodCredorDocumento : string.Empty;
                        confpag.ValorConfirmacao = !string.IsNullOrEmpty(item.outValorDocumento) ? Convert.ToDecimal(item.outValorDocumento) : 0;
                        confpag.ValorDocumento = !string.IsNullOrEmpty(item.outValorDocumento) ? item.outValorDocumento : string.Empty;
                        confpag.ValorDesdobradoCredor = !string.IsNullOrEmpty(item.outValorDesdobrado) ? item.outValorDesdobrado : string.Empty;
                        
                        confpag.ValorTotalConfirmado = !string.IsNullOrEmpty(item.outTotalConfirmar) ? Convert.ToDecimal(item.outTotalConfirmar) : 0;
                        //confpag.ValorDesdobradoCredor = !string.IsNullOrEmpty(item.outValorDesdobrado) ? item.outValorDesdobrado : string.Empty;
                        confpag.DataRealizacao = !string.IsNullOrEmpty(item.outDataRealizacao) ? item.outDataRealizacao : string.Empty;
                                              
                        //ACESSO
                        confpag.Chave = key;
                        confpag.Senha = password;
                        confpag.Impressora = impressora;

                        //VALORES (FONTE)
                        confpag.FonteLista = !string.IsNullOrEmpty(item.FonteLista) ? item.FonteLista : string.Empty;
                        confpag.ValorTotalConfirmarISSQN = !string.IsNullOrEmpty(item.TotalISSQNLista) ? item.TotalISSQNLista : string.Empty;
                        confpag.ValorTotalConfirmarIR = !string.IsNullOrEmpty(item.TotalIRLista) ? item.TotalIRLista : string.Empty;


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
                var entityProgramacaoDesembolso = new ProgramacaoDesembolso();
                var result = DataHelperProdespPagementoContaDer.Procedure_ConsultaOP(key, password, numeroDocumentoGerador);
                var resultItem = result[0];

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                {
                    var error = resultItem?.outErro;
                    if (error.Contains("FCFIN4 - PAGAMENTO NAO PREPARADO"))
                    {
                        entityProgramacaoDesembolso.OP = resultItem?.outNumeroOP + "@" + resultItem?.outErro;
                        //entityProgramacaoDesembolso.ProdespConsultaOPMensagemErro = resultItem?.outErro;
                    }
                    else
                    {
                        throw new ProdespException(error);
                    }

                }
                else
                {
                    entityProgramacaoDesembolso.OP = resultItem?.outNumeroOP;
                }

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


                string patters = @"^(\w+.{11})\s+(\w+.{21,22})\s+((\d{1,3}\.?)?(\d{1,3}\.?)?\d{1,3},\d{2})\s?(\w.+)?";
                string pattersOP = @"^(\d{5}.{7}.{4})";
                string pattersOcorrencia = @"(\w.+)";

                ArquivoRemessa arquivoRemessa = new ArquivoRemessa();

                arquivoRemessa.NumeroGeracao = !string.IsNullOrEmpty(resultItem.outNGA) ? Convert.ToInt32(resultItem.outNGA) : 0;
                arquivoRemessa.Orgao = resultItem.outOrgao;
                arquivoRemessa.Situacao = resultItem.outSituacao;
                arquivoRemessa.CodigoConta = !string.IsNullOrEmpty(resultItem.outCodConta) ? Convert.ToInt32(resultItem.outCodConta) : 0;
                arquivoRemessa.Banco = resultItem.outConta.Substring(0, 3);
                arquivoRemessa.Agencia = resultItem.outConta.Substring(4, 5);
                arquivoRemessa.NumeroConta = resultItem.outConta.Substring(12, 12);

                arquivoRemessa.ResultadoPreparacao = resultItem.outDataPrepArq + " " + resultItem.outHoraPrepArq;


                arquivoRemessa.DataPagamento = !string.IsNullOrEmpty(resultItem.outDataPagto) ? Convert.ToDateTime(resultItem.outDataPagto) : default(DateTime);


                arquivoRemessa.DataGeracao = resultItem.outDataGeracao + " " + resultItem.outHoraGeracaoArq;
                arquivoRemessa.dataPrevia = resultItem.outDataRetornoPrevia + " " + resultItem.outHoraRetornoPrevia;
                arquivoRemessa.ResultadoPrevia = resultItem.outResultadoPrevia;
                arquivoRemessa.dataProcessamento = resultItem.outDataRetornoProces + " " + resultItem.outHoraRetornoProces;
                arquivoRemessa.ResultadoProcessamento = resultItem.outResultadoProces;
                arquivoRemessa.dataConsolidado = resultItem.outDataRetornoConsol + " " + resultItem.outHoraRetornoConsol;
                arquivoRemessa.ResultadoConsolidado = resultItem.outResultadoConsol;

                arquivoRemessa.QtOpArquivo = !string.IsNullOrEmpty(resultItem.outQtdadeOPArq) ? Convert.ToInt32(resultItem.outQtdadeOPArq.Replace(",", "").Replace(".", "")) : 0;
                arquivoRemessa.ValorTotal = !string.IsNullOrEmpty(resultItem.outValorTotal) ? Convert.ToInt32(resultItem.outValorTotal.Replace(",", "").Replace(".", "")) : 0;
                arquivoRemessa.QtDeposito = !string.IsNullOrEmpty(resultItem.outQtdadeDepositos) ? Convert.ToInt32(resultItem.outQtdadeDepositos.Replace(",", "").Replace(".", "")) : 0;
                arquivoRemessa.ValorDep = "R$ " + resultItem.outValorDeposito;
                arquivoRemessa.QtDocTed = !string.IsNullOrEmpty(resultItem.outQtdadeDocTed) ? Convert.ToInt32(resultItem.outQtdadeDocTed.Replace(",", "").Replace(".", "")) : 0;
                arquivoRemessa.ValorDocTed = "R$ " + resultItem.outValorDocTed;
                arquivoRemessa.ValorCreditado = "R$ " + resultItem.outValorTotalCreditado;
                arquivoRemessa.ValorNaoCreditado = "R$ " + resultItem.outValorNaoCreditado;

                arquivoRemessa.ListOps = new System.Collections.Generic.List<ArquivoRemessaOP>();


                ArquivoRemessaOP arquivoOP = new ArquivoRemessaOP();
                string valida = "N";
                int contador = 0;

                string op = "";
                string conta = "";
                string valor = "";
                string ocorrencia = "";

                foreach (var item in result)
                {



                    if (Regex.IsMatch(item.outOP, pattersOP))
                    {

                        if (valida == "S")
                        {
                            ArquivoRemessaOP arquivoOPAUX = new ArquivoRemessaOP();
                            arquivoOPAUX.OP = op;
                            arquivoOPAUX.ContaCredito = conta;
                            arquivoOPAUX.Valor = valor;
                            arquivoOPAUX.Ocorrencia = ocorrencia;
                            arquivoOPAUX.indice = contador + 1;
                            arquivoRemessa.ListOps.Add(arquivoOPAUX);

                            contador++;


                        }


                        foreach (Match match in Regex.Matches(item.outOP, patters))
                        {

                            op = match.Groups[1].Value;
                            conta = match.Groups[2].Value;
                            valor = match.Groups[3].Value;
                            ocorrencia = match.Groups[6].Value;
                            valida = "S";

                        }


                    }
                    else //ocorrencia
                    {



                        foreach (Match match in Regex.Matches(item.outOP, pattersOcorrencia))
                        {

                            //arquivoOPAUX.Ocorrencia = match.Groups[1].Value;
                            ocorrencia = match.Groups[1].Value;
                        }

                        ArquivoRemessaOP arquivoOPAUX = new ArquivoRemessaOP();
                        arquivoOPAUX.OP = op;
                        arquivoOPAUX.ContaCredito = conta;
                        arquivoOPAUX.Valor = valor;
                        arquivoOPAUX.Ocorrencia = ocorrencia;
                        arquivoOPAUX.indice = contador + 1;
                        arquivoRemessa.ListOps.Add(arquivoOPAUX);
                        contador++;
                        valida = "N";

                    }





                }


                if (valida == "S")
                {
                    ArquivoRemessaOP arquivoOPAUX = new ArquivoRemessaOP();
                    arquivoOPAUX.OP = op;
                    arquivoOPAUX.ContaCredito = conta;
                    arquivoOPAUX.Valor = valor;
                    arquivoOPAUX.Ocorrencia = ocorrencia;
                    arquivoOPAUX.indice = contador + 1;
                    arquivoRemessa.ListOps.Add(arquivoOPAUX);

                }



                arquivoRemessa.ListOps = arquivoRemessa.ListOps.OrderBy(c => c.indice).ToList();




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

        public object ConsultarArquivoTipoDataVenc2(string key, string password, ArquivoRemessa objModel, string impressora)
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
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora") ? "Erro na comunicação com WebService Prodesp." : ex.Message);
            }
        }
    }
}
