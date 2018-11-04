using System.Threading.Tasks;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.Configuracao;
using Sids.Prodesp.Model.Interface.Seguranca;

namespace Sids.Prodesp.Infrastructure.Services.LiquidacaoDespesa
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Entity.LiquidacaoDespesa;
    using Model.Interface.LiquidacaoDespesa;
    using Model.Interface.Service.LiquidacaoDespesa;
    using Model.ValueObject.Service.Prodesp.Common;
    using Sids.Prodesp.Infrastructure.ProdespLiquidacaoDespesa;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    public class ProdespLiquidacaoDespesaWs : IProdespLiquidacaoDespesa
    {
        public string InserirSubEmpenho(string chave, string senha, Subempenho subempenho, Estrutura estrutura)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_InclusaoSubEmpenho(
                    chave, senha, subempenho, estrutura)
                    ?? new Procedure_InclusaoSubEmpenhoRecordType[] { };

                var resultItem = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                subempenho.DataVencimento = Convert.ToDateTime(resultItem?.outDataVencimento);
                return (resultItem?.outNumSubEmpenho + ";" + resultItem?.outReferencia);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public string InserirSubEmpenhoCancelamento(string chave, string senha, SubempenhoCancelamento entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_AnulacaoSubEmpenho(
                    chave, senha, entity)
                    ?? new ProdespLiquidacaoDespesa.Procedure_AnulacaoSubEmpenhoRecordType[] { };

                var resultItem = result.FirstOrDefault();

                HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outNumeroAnulacao;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public object InserirSubEmpenhoApoio(string chave, string password, Subempenho entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_InclusaoSubEmpenhoApoio(
                    chave, password, entity)
                    ?? new Procedure_InclusaoSubEmpenhoApoioRecordType[] { };

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

        public object ConsultarSubEmpenhoApoio(string chave, string senha, SubempenhoCancelamento entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_AnulacaoSubEmpenhoApoio(
                    chave, senha, entity)
                    ?? new Procedure_AnulacaoSubEmpenhoApoioRecordType[] { };

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

        public string InserirRapInscricao(string key, string password, IRap entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_InclusaoRapInscricao(key, password, entity) ?? new Procedure_InscricaoRAPRecordType[] { };

                var resultItem = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outNumero;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public object RapRequisicaoApoio(string key, string password, IRap entity, ICrudPrograma programa, ICrudEstrutura estrutura, IRegional regional)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_RapRequisicaoApoio(key, password, entity) ?? new Procedure_RequisicaoRAPApoioRecordType[] { };

                var resultItem = result.FirstOrDefault() ?? new Procedure_RequisicaoRAPApoioRecordType();


                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                var outCFP = resultItem?.outCFP.Replace(" ", "").Replace(".", "").Replace("/", "").Substring(0, 13);
                var outCED = resultItem?.outCED.Replace(".", "").Replace(" ", "").Replace("/", "");

                var prog = programa.Fetch(new Programa { Cfp = outCFP, Ano = DateTime.Now.Year }).FirstOrDefault();
                var natureza = estrutura.Fetch(new Estrutura { Natureza = outCED, Programa = prog?.Codigo }).FirstOrDefault();
                var orgao = regional.Fetch(new Regional { Orgao = resultItem?.outOrgao }).FirstOrDefault();

                resultItem.outCED = natureza?.Codigo.ToString();
                resultItem.outCFP = prog?.Codigo.ToString();
                //resultItem.outCGC = resultItem.outCGC;
                //resultItem.outContrato = resultItem.outContrato;
                //resultItem.outCredor1 = resultItem.outCredor1;
                //resultItem.outCredor2 = resultItem.outCredor2;
                //resultItem.outDataRealizacao = resultItem.outDataRealizacao;
                //resultItem.outErro = resultItem.outErro;
                //resultItem.outInfoTransacao = resultItem.outInfoTransacao;
                //resultItem.outMedicao = resultItem.outMedicao;
                //resultItem.outNFF = resultItem.outNFF;
                //resultItem.outNatureza = resultItem.outNatureza;
                //resultItem.outOrganiz = resultItem.outOrganiz;
                //resultItem.outPrazoPagto = resultItem.outPrazoPagto;
                //resultItem.outSucesso = resultItem.outSucesso;
                resultItem.outOrgao = orgao?.Id.ToString();


                return resultItem;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }


        public object ConsultaEmpenhoRap(string key, string password, IRap entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaEmpenhoInscrito(key, password, entity) ?? new Procedure_ConsultaEmpenhoInscritoRecordType[] { };

                var resultItem = result.FirstOrDefault() ?? new Procedure_ConsultaEmpenhoInscritoRecordType();

                string patters = @"^(\d{3})\s+(\d{2})\s+(\d{2}\/\d{2}\/\d{2})?\s+(.+)\s(\d+?\,\d{2})(.+)";
                string patters2 = @"\s+(\d{2}\/\d{2}\/\d{2})?(  )?(\d{6}|.+)?(  )?(\d{2}\/\d{2}\/\d{2})?";
                string patters3 = @"REQ\W";




            ConsultaEmpenhoRap consultaEmpenhoRap = new ConsultaEmpenhoRap();
                consultaEmpenhoRap.outValorIniRef_EMP = resultItem.outValorIniRef_EMP;
                consultaEmpenhoRap.outValorAnul_EMP = resultItem.outValorAnul_EMP;
                consultaEmpenhoRap.outValorLiqEmp_EMP = resultItem.outValorLiqEmp_EMP;
                consultaEmpenhoRap.outValorpagoExerc_SUB = resultItem.outValorpagoExerc_SUB;
                consultaEmpenhoRap.outValorInscRAP_SUB = resultItem.outValorInscRAP_SUB;
                consultaEmpenhoRap.outValorReqRAP_SUB = resultItem.outValorReqRAP_SUB;
                consultaEmpenhoRap.outValorAnulRAP_SUB = resultItem.outValorAnulRAP_SUB;
                consultaEmpenhoRap.outSaldoReq_SUB = resultItem.outSaldoReq_SUB;
                consultaEmpenhoRap.outTotalUtilizado_SUB = resultItem.outTotalUtilizado_SUB;
                consultaEmpenhoRap.outValorSubEmp_PAG = resultItem.outValorSubEmp_PAG;
                consultaEmpenhoRap.outValorReq_PAG = resultItem.outValorReq_PAG;
                consultaEmpenhoRap.outValorPagar_PAG = resultItem.outValorPagar_PAG;
                consultaEmpenhoRap.ListConsultarEmpenhoRap = new System.Collections.Generic.List<ListConsultaEmpenhoRap>();



                foreach (var item in result)
                {

                    if (Regex.Match(item.outLinhaDados, patters3).Success)
                        break;
               
                    


                    foreach (Match match in Regex.Matches(item.outLinhaDados, patters))
                    {
                        ListConsultaEmpenhoRap listconsulta = new ListConsultaEmpenhoRap();
                        listconsulta.outNrSubEmpenho = match.Groups[1].Value;
                        listconsulta.outCodDespesa = match.Groups[2].Value;
                        listconsulta.outDtRealizacao = match.Groups[3].Value;
                        listconsulta.outNomeCredor = match.Groups[4].Value;
                        listconsulta.outLiqSubEmpenhado = match.Groups[5].Value;

                        if (!string.IsNullOrEmpty(match.Groups[4].Value))
                            consultaEmpenhoRap.outCredor = match.Groups[4].Value;

                        string final = match.Groups[6].Value;


                        foreach (Match match2 in Regex.Matches(final, patters2))
                        {
                            listconsulta.outDtVencimento = match2.Groups[1].Value;
                            listconsulta.outOD = match2.Groups[3].Value;
                            listconsulta.outDtPagto = match2.Groups[5].Value;


                        }


                        consultaEmpenhoRap.ListConsultarEmpenhoRap.Add(listconsulta);
                   

                    }

                }



                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return consultaEmpenhoRap;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }


        public object RapAnulacaoApoio(string key, string password, string numRequisicaoRap, ICrudPrograma programa, ICrudEstrutura estrutura, IRegional regional)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_RapAnulacaoApoio(key, password, numRequisicaoRap) ?? new Procedure_anulacaoRequisicaoRAPApoioRecordType[] { };

                var resultItem = result.FirstOrDefault() ?? new Procedure_anulacaoRequisicaoRAPApoioRecordType();


                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");


                var outCFP = resultItem?.outCFP.Replace(" ", "").Replace(".", "").Replace("/", "").Substring(0, 13);
                var outCED = resultItem?.outCED.Replace(".", "").Replace(" ", "").Replace("/", "");

                var prog = programa.Fetch(new Programa { Cfp = outCFP, Ano = DateTime.Now.Year }).FirstOrDefault();
                var natureza = estrutura.Fetch(new Estrutura { Natureza = outCED, Programa = prog?.Codigo }).FirstOrDefault();
                var orgao = regional.Fetch(new Regional { Orgao = resultItem?.outOrgao }).FirstOrDefault();

                resultItem.outCED = natureza?.Codigo.ToString();
                resultItem.outCFP = prog?.Codigo.ToString();
                resultItem.outOrgao = orgao?.Id.ToString();


                return resultItem;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public string InserirRapRequisicao(string key, string password, IRap entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_RequisicaoRAP(key, password, entity);

                var resultItem = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                entity.DataVencimento = Convert.ToDateTime(resultItem?.outDataVencimento);

                return resultItem?.outNumRAP;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public ConsultaSubempenho ConsultaSubempenho(string chave, string senha, string subempenho)
        {
            var result = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaSubEmpenho(
               chave, senha, subempenho) ?? new Procedure_ConsultaSubEmpenhoRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new Exception($"Prodesp - {resultItem?.outErro}");

            var ConsultaSubempenho = new ConsultaSubempenho();

            var subempenhoProprietys = ConsultaSubempenho.GetType().GetProperties().ToList();

            var propriedade = resultItem.GetType().GetProperties().ToList();

            Parallel.ForEach(subempenhoProprietys, subempenhoPropriety =>
            {
                subempenhoPropriety.SetValue(ConsultaSubempenho, propriedade.FirstOrDefault(x => x.Name == subempenhoPropriety.Name)?.GetValue(resultItem).ToString());

            });

            return ConsultaSubempenho;
        }

        public object ConsultaInclusaoEmpenhoCredor(string chave, string senha, Subempenho subempenho)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaEmpenhoCredor(
                   chave, senha, subempenho)
                   ?? new Procedure_ConsultaEmpenhoCredorRecordType[] { };

                var resultItem = result.FirstOrDefault();

                var propriedade = resultItem.GetType().GetProperties().ToList();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");


                return new ConsultaEmpenhoCredor
                {
                    outCGC = propriedade.FirstOrDefault(x => x.Name == "outCGC").GetValue(resultItem).ToString(),
                    outContrato = propriedade.FirstOrDefault(x => x.Name == "outContrato").GetValue(resultItem).ToString().Replace("/", ""),
                    //outNotaFiscal = propriedade.FirstOrDefault(x => x.Name == "outNotaFiscal").GetValue(resultItem).ToString(),
                    outDisponivelSubEmpenhar = propriedade.FirstOrDefault(x => x.Name == "outDisponivelSubEmpenhar").GetValue(resultItem).ToString(),
                    outErro = propriedade.FirstOrDefault(x => x.Name == "outErro").GetValue(resultItem).ToString(),
                    outLiqEmpenhado = propriedade.FirstOrDefault(x => x.Name == "outLiqEmpenhado").GetValue(resultItem).ToString(),
                    outLiqSubEmpenhado = propriedade.FirstOrDefault(x => x.Name == "outLiqSubEmpenhado").GetValue(resultItem).ToString(),
                    outNrEmpenho = propriedade.FirstOrDefault(x => x.Name == "outNrEmpenho").GetValue(resultItem).ToString(),
                    outOrganiz = propriedade.FirstOrDefault(x => x.Name == "outOrganiz").GetValue(resultItem).ToString(),
                    outCredorReduzido = propriedade.FirstOrDefault(x => x.Name == "outCredorReduzido").GetValue(resultItem).ToString(),
                    outSucesso = propriedade.FirstOrDefault(x => x.Name == "outSucesso").GetValue(resultItem).ToString(),
                    ListConsultarEmpenhoCredor = result.Select(x => new ListConsultaEmpenhoCredor
                    {
                        outNrEmpenho = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outNrEmpenho").GetValue(x).ToString(),
                        outContrato = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outContrato").GetValue(x).ToString(),
                        outLiqEmpenhado = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outLiqEmpenhado").GetValue(x).ToString(),
                        outLiqSubEmpenhado = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outLiqSubEmpenhado").GetValue(x).ToString(),
                        outDisponivelSubEmpenhar = x.GetType().GetProperties().ToList().FirstOrDefault(y => y.Name == "outDisponivelSubEmpenhar").GetValue(x).ToString()

                    }).ToList()
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public object ConsultaEmpenhoSaldo(string key, string password, IRap entity, Regional regional)
        {
            var result = DataHelperProdespLiquidacaoDespesa.Procedure_ConsultaEmpenhoSaldoRAP(key, password, entity, regional) ?? new Procedure_ConsultaEmpenhoSaldoRAPRecordType[] { };

            if (!string.IsNullOrEmpty(result[0]?.outErro))
                throw new Exception($"Prodesp - {result[0]?.outErro}");
            return result;
        }

        public string InserirRapAnulacao(string key, string password, RapAnulacao entity)
        {
            try
            {
                var result = DataHelperProdespLiquidacaoDespesa.Procedure_AnulacaoRequisicaoRAP(key, password, entity);

                var resultItem = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outNumeroAnulacao;
            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }

        }

        public bool InserirDoc(string chave, string senha, ILiquidacaoDespesa subempenho, string tipo)
        {
            try
            {
                var result = DataHelperProdespReserva.Procedure_InclusaoDocSIAFEM(chave, senha, subempenho.NumeroSiafemSiafisico, subempenho.NumeroProdesp, tipo);

                var resultItem = result.FirstOrDefault();

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception(resultItem?.outErro);

                return !string.IsNullOrEmpty(resultItem?.outSucesso);
            }
            catch
            {
                throw;
            }
        }


    }
}
