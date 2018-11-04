using System.Collections.Generic;
using System.Threading.Tasks;
using Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;

namespace Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica
{
    using System;
    using Model.Interface.Service.PagamentoContaUnica;
    using Helpers;
    using ProdespPagtoContaUnica;
    using System.Linq;
    using Model.Entity.PagamentoContaUnica.ListaBoletos;
    using System.Text.RegularExpressions;
    using Model.Extension;
    using Model.Exceptions;
    using DataBase.PagamentoContaUnica;
    using Model.Entity.PagamentoContaUnica.PagamentoDer;
    using Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
    using Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;

    public class ProdespPagamentoContaUnicaWs : IProdespPagamentoContaUnica
    {

        public object ConsultaDesdobramentoApoio(string key, string password, Desdobramento objModel)
        {
            try
            {


                if (objModel.DesdobramentoTipoId == 1)
                {
                    var result = DataHelperProdespPagamentoContaUnica.Procedure_DesdobramentoISSQNApoio(
                        key, password, objModel)
                        ?? new Procedure_DesdobramentoISSQNApoioRecordType[] { };
                    var resultItem = result.FirstOrDefault();
                    if (!string.IsNullOrEmpty(resultItem?.outErro))
                        throw new SidsException($"Prodesp - {resultItem?.outErro}");
                    return resultItem;
                }
                else
                {
                    var result = DataHelperProdespPagamentoContaUnica.Procedure_DesdobramentoOutrosApoio(key, password, objModel) ?? new Procedure_DesdobramentoOutrosApoioRecordType[] { };
                    var resultItem = result.FirstOrDefault();
                    if (!string.IsNullOrEmpty(resultItem?.outErro))
                        throw new SidsException($"Prodesp - {resultItem?.outErro}");
                    return resultItem;
                }

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public object ConsultaPreparacaoPagamentoApoio(string key, string password, PreparacaoPagamento objModel)
        {
            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_PreparacaoPagtoDocGeradorApoioRecordType(key, password, objModel) ?? new Procedure_PreparacaoPagtoDocGeradorApoioRecordType[] { };
                var resultItem = result.FirstOrDefault();
                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }

        }



        public object ConsultarPreparacaoPgtoTipoDespesaDataVenc(string key, string password, PreparacaoPagamento objModel)
        {
            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_PreparacaoPagtoOrgaoApoioRecordType(key, password, objModel) ?? new Procedure_PreparacaoPagtoOrgaoApoioRecordType[] { };
                var resultItem = result.FirstOrDefault();
                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }

        }


        public object ConsultarPreparacaoPgtoTipoDespesaDataVenc2(string key, string password, ArquivoRemessa objModel)
        {
            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_PreparacaoPagtoOrgaoApoioRecordType2(key, password, objModel) ?? new Procedure_PreparacaoPagtoOrgaoApoioRecordType[] { };
                var resultItem = result.FirstOrDefault();
                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }

        }


        public object ConsultarPreparacaoPgtoTipoDespesaDataVenc3(string key, string password, PreparacaoPagamento objModel)
        {
            try
            {
                var result = DataHelperProdespPagementoContaDer.Procedure_PreparacaoArquiRemessa(key, password, objModel,"") ?? new Procedure_PreparacaoArquiRemessaRecordType[] { };
                var resultItem = result.FirstOrDefault();
                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");
                return resultItem;

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }

        }




        public IEnumerable<Credor> ConsultaCredorReduzido(string key, string password, string organizacao)
        {

            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_ConsultaCredorReduzido(key, password, organizacao);

                var resultItem = result.ToList();

                if (!string.IsNullOrEmpty(resultItem[0]?.outErro))
                    throw new SidsException($"Prodesp - {resultItem[0]?.outErro}");

                return resultItem.Select(x => new Credor
                {
                    Prefeitura = x.outNome,
                    Conveniado = !string.IsNullOrWhiteSpace(x.outConveniado),
                    BaseCalculo = !string.IsNullOrWhiteSpace(x.outBaseCalc),
                    NomeReduzidoCredor = x.outNomeReduzido,
                    CpfCnpjUgCredor = x.outCodCredor.Replace("/", "").Replace(".", "").Replace("-", "")
                });
            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public void Inserir_DesdobramentoISSQN(string key, string password, ref Desdobramento entity)
        {
            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_DesdobramentoISSQN(key, password, entity) ?? new Procedure_DesdobramentoISSQNRecordType[] { };

                var resultItem = result[0] ?? new Procedure_DesdobramentoISSQNRecordType();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");

                entity.ValorInss = Convert.ToDecimal(resultItem?.outTotalINSS);
                entity.ValorIr = Convert.ToDecimal(resultItem?.outTotalIR);
                entity.ValorIssqn = Convert.ToDecimal(resultItem?.outTotalISSQN);
                entity.DescricaoCredor = resultItem?.outCredor;
                entity.NomeReduzidoCredor = resultItem?.outCredorReduz;
                entity.DescricaoServico = resultItem?.outDescricaoServico;

                var filteProperties = resultItem.GetType().GetProperties().Where(x => x.Name.Contains("outValorDesdobrado_") || x.Name.Contains("outCredorReduzido_") || x.Name.Contains("outPorcentBaseCalc_")).ToList();

                foreach (var identificacaoDesdobramento in entity.IdentificacaoDesdobramentos)
                {

                    foreach (var filterPropriety in filteProperties)
                    {
                        if (identificacaoDesdobramento.NomeReduzidoCredor.ToUpper() == filterPropriety.GetValue(resultItem).ToString().ToUpper() && filterPropriety.Name != "outCredorReduz")
                        {
                            var name = filterPropriety.Name;
                            var index = name.Substring(name.Length - 2, 2);
                            var valor = filteProperties.FirstOrDefault(x => x.Name == $"outValorDesdobrado_{index}")?.GetValue(resultItem).ToString();
                            var baseCalc = filteProperties.FirstOrDefault(x => x.Name == $"outPorcentBaseCalc_{index}")?.GetValue(resultItem).ToString();
                            identificacaoDesdobramento.ValorDesdobrado = Convert.ToDecimal((valor == "" ? "0" : valor));
                            identificacaoDesdobramento.ValorPercentual = Convert.ToDecimal(baseCalc == "" ? "0" : baseCalc);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public void Inserir_DesdobramentoOutros(string key, string password, ref Desdobramento entity)
        {
            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_DesdobramentoOutros(key, password, entity) ?? new Procedure_DesdobramentoOutrosRecordType[] { };

                var resultItem = result[0] ?? new Procedure_DesdobramentoOutrosRecordType();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");

                entity.DescricaoCredor = resultItem?.outCredor01;
                entity.NomeReduzidoCredor = resultItem?.outCredorReduz;
                entity.TipoDespesa = resultItem?.outTipoDespesa;
                entity.ValorDistribuido = Convert.ToDecimal(resultItem?.outValorOriginal);

                var filteProperties = resultItem.GetType().GetProperties().ToList();

                foreach (var identificacaoDesdobramento in entity.IdentificacaoDesdobramentos)
                {

                    foreach (var filterPropriety in filteProperties)
                    {
                        if (identificacaoDesdobramento.NomeReduzidoCredor.ToUpper() == filterPropriety.GetValue(resultItem).ToString().ToUpper() && filterPropriety.Name != "outCredorReduz")
                        {
                            var name = filterPropriety.Name;
                            var index = name.Substring(name.Length - 2, 2);

                            var valor = filteProperties.FirstOrDefault(x => x.Name == $"outValor_{index}")?.GetValue(resultItem).ToString();
                            var baseCalc = filteProperties.FirstOrDefault(x => x.Name == $"outPorcentagem_{index}")?.GetValue(resultItem).ToString();

                            identificacaoDesdobramento.TipoBloqueio = filteProperties.FirstOrDefault(x => x.Name == $"outTipoBloqueio_{index}")?.GetValue(resultItem).ToString();
                            identificacaoDesdobramento.ValorDesdobrado = Convert.ToDecimal((valor == "" ? "0" : valor));
                            identificacaoDesdobramento.ValorPercentual = Convert.ToDecimal(baseCalc == "" ? "0" : baseCalc);
                        }
                    }
                }



            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public void Inserir_PreparacaoPagamento(string key, string password, ref PreparacaoPagamento entity, Regional orgao)
        {

            if (entity.PreparacaoPagamentoTipoId == 1)
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_PreparacaoPagtoOrgao(key, password, entity, orgao) ?? new Procedure_PreparacaoPagtoOrgaoRecordType[] { };

                var resultItem = result[0] ?? new Procedure_PreparacaoPagtoOrgaoRecordType();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");

                entity.ValorTotal = Convert.ToDecimal(resultItem.outTotal);
                entity.NumeroOpFinal = resultItem.outNumOPFinal.Replace("-", string.Empty);
                entity.NumeroOpInicial = resultItem.outNumOPInicial.Replace("-", string.Empty);
                entity.QuantidadeOpPreparada = Convert.ToInt32(resultItem.outQuantidadeOPs.Replace(",", "").Replace(".", ""));
            }
            else
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_PreparacaoPagtoDocGerador(key, password, entity) ?? new Procedure_PreparacaoPagtoDocGeradorRecordType[] { };

                var resultItem = result[0] ?? new Procedure_PreparacaoPagtoDocGeradorRecordType();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new SidsException($"Prodesp - {resultItem?.outErro}");

                entity.ValorTotal = Convert.ToDecimal(resultItem.outTotal);
                entity.NumeroOpFinal = resultItem.outNumeroOPFinal.Replace("-", "");
                entity.NumeroOpInicial = resultItem.outNumeroOPInicial.Replace("-", "");
                entity.QuantidadeOpPreparada =  Convert.ToInt32(resultItem.outQtdadeOP.Replace(",", "").Replace(".", ""));
            }



        }

        public void Inserir_ConfirmacaoPagamento(string key, string password, ref ConfirmacaoPagamento entity, Regional orgao)
        {

            //var result = DataHelperProdespPagamentoContaUnica.Procedure_PreparacaoPagtoOrgao(key, password, entity, orgao) ?? new Procedure_PreparacaoPagtoOrgaoRecordType[] { };
            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConfirmacaoPagtoOPApoio(key, password, entity, orgao) ?? new Procedure_ConfirmacaoPagtoOPRecordType[] { };

            var resultItem = result[0] ?? new Procedure_ConfirmacaoPagtoOPRecordType();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new SidsException($"Prodesp - {resultItem?.outErro}");

            //entity.ValorTotal = Convert.ToDecimal(resultItem.outTotal);
            //entity.NumeroOpFinal = resultItem.outNumOPFinal.Replace("-", string.Empty);
            //entity.NumeroOpInicial = resultItem.outNumOPInicial.Replace("-", string.Empty);
            //entity.QuantidadeOpPreparada = Convert.ToInt32(resultItem.outQuantidadeOPs);
        }

        public void AnulacaoDesdobramento(string key, string password, ref Desdobramento entity)
        {
            try
            {
                var result = DataHelperProdespPagamentoContaUnica.Procedure_AnulacaoDesdobramento(key, password, entity);

                var resultItem = result.ToList();

                if (!string.IsNullOrEmpty(resultItem[0]?.outErro))
                    throw new SidsException($"Prodesp - {resultItem[0]?.outErro}");

            }
            catch (Exception ex)
            {

                throw new SidsException(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public IEnumerable<ConsultaDesdobramento> ConsultaDesdobramento(string key, string password, string number, int type)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConsultaDesdobramento(key, password, number, type) ?? new Procedure_ConsultaDesdobramentoRecordType[] { };



            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");

            var consultaDesdobramentos = new List<ConsultaDesdobramento>();

            foreach (var resultItem in result)
            {
                var consultaDesdobramento = new ConsultaDesdobramento();
                var subempenhoProprietys = consultaDesdobramento.GetType().GetProperties().ToList();

                var propriedade = resultItem.GetType().GetProperties().ToList();

                foreach (var subempenhoPropriety in subempenhoProprietys)
                {
                    subempenhoPropriety.SetValue(consultaDesdobramento, propriedade.FirstOrDefault(x => x.Name == subempenhoPropriety.Name)?.GetValue(resultItem).ToString());

                }
                consultaDesdobramentos.Add(consultaDesdobramento);
            }


            return consultaDesdobramentos;
        }

        public IEnumerable<ProgramacaoDesembolsoAgrupamento> ConsultaDocumentoGerador(string key, string password, ProgramacaoDesembolso programacaoDesembolso, Regional orgao)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConsultaPagtosPrepararSDFF(key, password, programacaoDesembolso, orgao) ?? new Procedure_ConsultaPagtosPrepararSDFFRecordType[] { };




            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");

            if (string.IsNullOrEmpty(result[0]?.outNumDoc))
                throw new SidsException($"Prodesp - Nenhum pagamento a preparar encontrado");

            var documentoGeradorList = ProgramacaoDesembolsoAgrupamentosFactory(result);

            documentoGeradorList.Where(x => string.IsNullOrWhiteSpace(x.NumeroDocumento)).ToList().ForEach(x => x.NumeroDocumento = programacaoDesembolso.NumeroDocumento);
            documentoGeradorList.Where(x => x.DocumentoTipoId == 0).ToList().ForEach(x => x.DocumentoTipoId = programacaoDesembolso.DocumentoTipoId);

            return documentoGeradorList;
        }

        public void CancelamentoOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_CancelamentoOP(key, password, programacaoDesembolso) ?? new Procedure_CancelamentoOPRecordType[] { };


            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");
        }

        public IEnumerable<object> CancelamentoOpApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_CancelamentoOPApoio(key, password, programacaoDesembolso) ?? new Procedure_CancelamentoOPApoioRecordType[] { };

            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");

            return result;
        }

        public IEnumerable<object> BloqueioOpApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_BloqueioPagtoDocApoio(key, password, programacaoDesembolso) ?? new Procedure_BloqueioPagtoDocApoioRecordType[] { };

            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");

            return result;
        }

        public void BloqueioOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_BloqueioPagtoDoc(key, password, programacaoDesembolso) ?? new Procedure_BloqueioPagtoDocRecordType[] { };

            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");
        }

        public void DesbloqueioOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_DesbloqueioPagtoDoc(key, password, programacaoDesembolso) ?? new Procedure_DesbloqueioPagtoDocRecordType[] { };

            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new SidsException($"Prodesp - {result[0]?.outErro}");
        }

        private static List<ProgramacaoDesembolsoAgrupamento> ProgramacaoDesembolsoAgrupamentosFactory(Procedure_ConsultaPagtosPrepararSDFFRecordType[] result)
        {
            var documentoGeradorList = new List<ProgramacaoDesembolsoAgrupamento>();
            int seq = 0;

            var parametros = new ProgramacaoDesembolsoAgrupamentoDal().BuscaParametros();


            foreach (var selectedItem in result)
            {
                var documentoGerador = new ProgramacaoDesembolsoAgrupamento();

                if (selectedItem.outDataRealizacao.Equals("00/00/2000"))
                {
                    selectedItem.outDataRealizacao = "01/01/2000";
                }

                if (selectedItem.outVencimento.Equals("00/00/2000"))
                {
                    selectedItem.outVencimento = "01/01/2000";
                }

                //if (!string.IsNullOrWhiteSpace(selectedItem.outDataRealizacao))
                   documentoGerador.DataEmissao = DateTime.Now;

                if (!string.IsNullOrWhiteSpace(selectedItem.outVencimento))
                {
                    if (Convert.ToDateTime(selectedItem.outVencimento) < DateTime.Now)
                    {
                        documentoGerador.DataVencimento = DateTime.Now;
                    }
                    else
                    {
                        documentoGerador.DataVencimento = Convert.ToDateTime(selectedItem.outVencimento);
                    }
                }

                documentoGerador.DocumentoTipoId = string.IsNullOrWhiteSpace(selectedItem.outNumDoc) ? 0 : Convert.ToInt32(selectedItem.outNumDoc.Substring(0, 2));

                switch (documentoGerador.DocumentoTipoId)
                {
                    case 5:
                        documentoGerador.NumeroDocumento = $"{selectedItem.outNumDoc.Substring(5, 9)}/{selectedItem.outNumDoc.Substring(14, 3)}"; 
                        break;
                    case 11:
                        documentoGerador.NumeroDocumento = $"{selectedItem.outNumDoc.Substring(2, 9)}/{selectedItem.outNumDoc.Substring(11, 3)}/{selectedItem.outNumDoc.Substring(14, 3)}";
                        break;
                }

                documentoGerador.Sequencia = seq += 1;
                documentoGerador.MensagemServicoSiafem = "";
                documentoGerador.Regional = selectedItem.outOrgao;
                documentoGerador.CodigoUnidadeGestora = "162101";
                documentoGerador.CodigoGestao = "16055";
                documentoGerador.NumeroListaAnexo = "";
                documentoGerador.NumeroNLReferencia = NLFactory(selectedItem); ;
                documentoGerador.NumeroProcesso = selectedItem.outProcesso;

                if (!string.IsNullOrWhiteSpace(selectedItem.outContrato))
                    documentoGerador.NumeroContrato = $"{selectedItem.outContrato.Substring(0, 2)}.{selectedItem.outContrato.Substring(2, 2)}.{selectedItem.outContrato.Substring(4, 5)}-{selectedItem.outContrato.Substring(9, 1)}";

                documentoGerador.CodigoAplicacaoObra = selectedItem.outCodObra;

                if (!string.IsNullOrWhiteSpace(selectedItem.outVencimento))
                {
                    decimal valorConvertido;

                    if (decimal.TryParse(selectedItem.outValorDoc.Replace(",",""), out valorConvertido))
                    {
                        documentoGerador.Valor = valorConvertido;
                    }
                }

                documentoGerador.NumeroDocumentoGerador = selectedItem.outNumDoc;
                documentoGerador.NomeCredorReduzido = selectedItem.outEmpresa;


                if (string.IsNullOrWhiteSpace(selectedItem.outFonte))
                {
                    documentoGerador.NumeroCnpjcpfCredor = "";
                }
                else if ((selectedItem.outEmpresa == "ISS/SAO PAULO") || (selectedItem.outEmpresa == "INSS-11%"))
                {
                    documentoGerador.NumeroCnpjcpfCredor = "162184";
                }
                else if (selectedItem.outFonte == "01" || selectedItem.outFonte == "41")
                {
                    documentoGerador.NumeroCnpjcpfCredor = "162181";
                }
                else
                {
                    documentoGerador.NumeroCnpjcpfCredor = "162184";
                }


                documentoGerador.GestaoCredor = "16055";
                documentoGerador.NumeroBancoCredor = "";
                documentoGerador.NumeroAgenciaCredor = "";
                documentoGerador.NumeroContaCredor = "UNICA";
                documentoGerador.NumeroNE = (!string.IsNullOrWhiteSpace(selectedItem.outNE)) ? selectedItem.outNE : "";
                documentoGerador.numNLRef = selectedItem.outNL;

                var vNumContrato = "00.000-0/00";

                if (!string.IsNullOrWhiteSpace(selectedItem.outContrato))
                    vNumContrato = $"{selectedItem.outContrato.Substring(4, 2)}.{selectedItem.outContrato.Substring(6, 3)}-{selectedItem.outContrato.Substring(9, 1)}/{selectedItem.outContrato.Substring(0, 2)}";



                if (selectedItem.outOrganiz == "8")
                {
                    documentoGerador.Finalidade = string.IsNullOrEmpty(selectedItem.outNotaFiscal) ? $"PAGTO ISSQN CONTR {vNumContrato}" : $"PAGTO ISSQN CONTR {vNumContrato} N.F. {selectedItem.outNotaFiscal}";

                }
                else
                {
                    switch (selectedItem.outEmpresa)
                    {
                        case "ISS/SAO PAULO":
                            documentoGerador.Finalidade = string.IsNullOrEmpty(selectedItem.outNotaFiscal) ? $"PAGTO ISS/SP CONT {vNumContrato}" : $"PAGTO ISS/SP CONT {vNumContrato} N.F. {selectedItem.outNotaFiscal}";
                            break;
                        case "INSS-11%":
                            documentoGerador.Finalidade = string.IsNullOrEmpty(selectedItem.outNotaFiscal) ? $"PAGTO INSS CONTR {vNumContrato}" : $"PAGTO INSS CONTR {vNumContrato} N.F. {selectedItem.outNotaFiscal}";
                            break;
                        default:
                            documentoGerador.Finalidade = string.IsNullOrEmpty(selectedItem.outNotaFiscal) ? $"PAGTO CONTRATO {vNumContrato}" : $"PAGTO CONTRATO {vNumContrato} N.F. {selectedItem.outNotaFiscal}";
                            break;

                    }

                }


                if (string.IsNullOrEmpty(selectedItem.outListaSiafem))
                {
                    documentoGerador.NumeroListaAnexo = "";
                }
                else
                {
                    if (selectedItem.outEmpresa == "INSS-11%" || selectedItem.outEmpresa == "ISS/SAO PAULO")
                        documentoGerador.NumeroListaAnexo = selectedItem.outListaSiafem;
                }


                documentoGerador.Classificacao = ClassificacaoFactory(selectedItem);
                documentoGerador.NumeroEvento = EventoFactory(selectedItem);
                documentoGerador.InscricaoEvento = InscricaoEventoFactory(selectedItem);
                documentoGerador.CodigoDespesa = selectedItem.outDespesa;
                documentoGerador.RecDespesa = EventoDespesaFactory(selectedItem);


                switch (selectedItem.outEmpresa)
                {
                    case "ISS/SAO PAULO":
                        var iss = parametros.FirstOrDefault(i => i.NomeCredorReduzido == "ISS/SAO PAULO");

                        documentoGerador.NumeroCnpjcpfPagto = iss.NumeroCnpjcpfCredor;
                        documentoGerador.NumeroBancoPagto = iss.NumeroBancoCredor;
                        documentoGerador.NumeroAgenciaPagto = iss.NumeroAgenciaCredor;
                        documentoGerador.NumeroContaPagto = iss.NumeroContaCredor;

                        break;
                    case "INSS-11%":
                        var inss = parametros.FirstOrDefault(i => i.NomeCredorReduzido == "INSS-11%");

                        documentoGerador.NumeroCnpjcpfPagto = inss.NumeroCnpjcpfCredor;
                        documentoGerador.NumeroBancoPagto = inss.NumeroBancoCredor;
                        documentoGerador.NumeroAgenciaPagto = inss.NumeroAgenciaCredor;
                        documentoGerador.NumeroContaPagto = inss.NumeroContaCredor;

                        break;
                    default:
                        documentoGerador.NumeroCnpjcpfPagto = selectedItem.outCodCredor;
                        documentoGerador.NumeroBancoPagto = selectedItem.outContaFavBanco;
                        documentoGerador.NumeroAgenciaPagto = selectedItem.outContaFavAgencia;
                        documentoGerador.NumeroContaPagto = selectedItem.ouContaFavConta;
                        break;
                }



                documentoGerador.Fonte = selectedItem.outFonteSiafem;
                documentoGerador.GestaoPagto = "";

                documentoGeradorList.Add(documentoGerador);
            }

            return documentoGeradorList;
        }


        private static string NLFactory(Procedure_ConsultaPagtosPrepararSDFFRecordType selectedItem)
        {

            if (string.IsNullOrWhiteSpace(selectedItem.outOrganiz) || (selectedItem.outOrganiz == "8" || (selectedItem.outEmpresa == "ISS/SAO PAULO")))
            {
                return "";
            }
            else if (selectedItem.outEmpresa == "INSS-11%")
            {
                if (string.IsNullOrWhiteSpace(selectedItem.outNLInss))
                {
                    return "";
                }
                else
                {
                    return selectedItem.outNLInss;
                }
            }
            else if (string.IsNullOrWhiteSpace(selectedItem.outNL))
            {
                return "";
            }
            else
            {
                return selectedItem.outNL;
            }
        }




        private static string ClassificacaoFactory(Procedure_ConsultaPagtosPrepararSDFFRecordType selectedItem)
        {
            var parametros = new ProgramacaoDesembolsoAgrupamentoDal().BuscaParametros();
            var iss = parametros.FirstOrDefault(i => i.NomeCredorReduzido == "ISS/SAO PAULO");

            if (selectedItem.outOrganiz == "8" || (selectedItem.outOrganiz == "7" && (selectedItem.outEmpresa == "ISS/SAO PAULO")))
                return iss.Classificacao;

            return "";
        }



        private static string EventoDespesaFactory(Procedure_ConsultaPagtosPrepararSDFFRecordType selectedItem)
        {
            if (selectedItem.outNumDoc.Substring(0, 2) != "11" || selectedItem.outOrganiz == "8" ||
                    selectedItem.outEmpresa == "INSS-11%" || selectedItem.outEmpresa == "ISS/SAO PAULO")
                return "";
            else if (selectedItem.outNaturezaDesp.Length == 0 || selectedItem.outNaturezaDesp == "00000000")

                return "";
            else
                return selectedItem.outNaturezaDesp;
        }

        private static string InscricaoEventoFactory(Procedure_ConsultaPagtosPrepararSDFFRecordType selectedItem)
        {

            if (selectedItem.outOrganiz == "8" || (selectedItem.outOrganiz == "7" && (selectedItem.outEmpresa == "ISS/SAO PAULO")))
            {
                return "";
            }
            else if (selectedItem.outEmpresa == "INSS-11%")
            {
                return (string.IsNullOrWhiteSpace(selectedItem.RField_25)) ? "" : selectedItem.RField_25;
            }
            else
            {
                return (string.IsNullOrWhiteSpace(selectedItem.outNE)) ? "" : selectedItem.outNE;

            }


        }

        private static string EventoFactory(Procedure_ConsultaPagtosPrepararSDFFRecordType selectedItem)
        {
            var vAnoDocGer = "";
            if (string.IsNullOrWhiteSpace(selectedItem.outNumDoc))
                return "";
            // Evento
            switch (selectedItem.outNumDoc?.SafeSubstring(0, 2))
            {
                case "05":
                    vAnoDocGer = selectedItem.outNumDoc?.SafeSubstring(5, 2);
                    break;
                case "11":
                    vAnoDocGer = selectedItem.outNumDoc?.SafeSubstring(2, 2);
                    break;
            }

            var varSsaa = "20" + vAnoDocGer;

            var varDataAux = default(DateTime);

            if (!string.IsNullOrWhiteSpace(selectedItem.outDataRealizacao))
                varDataAux = Convert.ToDateTime(selectedItem.outDataRealizacao);


            if (selectedItem.outOrganiz == "8" || (selectedItem.outOrganiz == "7" && (selectedItem.outEmpresa == "ISS/SAO PAULO")))
            {
                return "700215";
            }
            else if (selectedItem.outEmpresa == "INSS-11%")
            {
                return "700552";
            }
            else if (selectedItem.outNumDoc?.SafeSubstring(0, 2) == "05")
            {
                return "700601";
            }
            else if ((selectedItem.outNumDoc?.SafeSubstring(0, 2) == "11")
                && (Convert.ToInt32(varSsaa) == (DateTime.Now.Year - 1))
                && (selectedItem.outNL.SafeSubstring(0, 4) == (DateTime.Now.Year - 1).ToString())
                && varDataAux == default(DateTime))
            {
                return "700625";
            }
            else if (selectedItem.outNumDoc?.Substring(0, 2) == "11"
                && (Convert.ToInt32(varSsaa) == (DateTime.Now.Year - 1))
                && varDataAux == default(DateTime))
            {
                return "700630";
            }
            else if (selectedItem.outNumDoc?.SafeSubstring(0, 2) == "11"
                && (Convert.ToInt32(varSsaa) == (DateTime.Now.Year - 1)) && varDataAux != default(DateTime))
            {
                return "700625";
            }
            else if (selectedItem.outNumDoc?.SafeSubstring(0, 2) == "11" && (Convert.ToInt32(varSsaa) < (DateTime.Now.Year - 1)))
            {
                return "700634";
            }

            return "";

 
        }

        public void InserirDoc(ListaBoletos entity, string chave, string senha, string tipo)
        {
            var result = entity.NumeroDocumento != null ? DataHelperProdespReserva.Procedure_InclusaoDocSIAFEM(chave, senha, entity.NumeroSiafem, entity.NumeroDocumento, tipo) : null;
            var resultItem = entity.NumeroDocumento != null ? result.FirstOrDefault() : null;

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new SidsException(resultItem?.outErro);
        }

        public void Inserir_ConfirmacaoPagamento(string key, string password, ref PDExecucaoItem entity, Regional orgao)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConfirmacaoPagtoOPApoio(key, password, entity, orgao) ?? new Procedure_ConfirmacaoPagtoOPRecordType[] { };

            var resultItem = result[0] ?? new Procedure_ConfirmacaoPagtoOPRecordType();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new SidsException($"Prodesp - {resultItem?.outErro}");

            //entity.ValorTotal = Convert.ToDecimal(resultItem.outTotal);
            //entity.NumeroOpFinal = resultItem.outNumOPFinal.Replace("-", string.Empty);
            //entity.NumeroOpInicial = resultItem.outNumOPInicial.Replace("-", string.Empty);
            //entity.QuantidadeOpPreparada = Convert.ToInt32(resultItem.outQuantidadeOPs);
        }

        public void Inserir_ConfirmacaoPagamento(string key, string password, ref OBAutorizacaoItem entity, Regional orgao)
        {
            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConfirmacaoPagtoOPApoio(key, password, entity, orgao) ?? new Procedure_ConfirmacaoPagtoOPRecordType[] { };

            var resultItem = result[0] ?? new Procedure_ConfirmacaoPagtoOPRecordType();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new SidsException($"Prodesp - {resultItem?.outErro}");

            //entity.ValorTotal = Convert.ToDecimal(resultItem.outTotal);
            //entity.NumeroOpFinal = resultItem.outNumOPFinal.Replace("-", string.Empty);
            //entity.NumeroOpInicial = resultItem.outNumOPInicial.Replace("-", string.Empty);
            //entity.QuantidadeOpPreparada = Convert.ToInt32(resultItem.outQuantidadeOPs);
        }

    }
}
