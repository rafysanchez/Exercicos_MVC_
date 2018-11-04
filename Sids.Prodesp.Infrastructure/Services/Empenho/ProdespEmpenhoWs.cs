namespace Sids.Prodesp.Infrastructure.Services.Empenho
{
    using Helpers;
    using Model.Entity.Configuracao;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Interface;
    using Model.Interface.Base;
    using Model.Interface.Empenho;
    using Model.Interface.Service.Empenho;
    using Model.ValueObject.Service.Prodesp.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ProdespEmpenhoWs : IProdespEmpenho
    {
        public ConsultaEmpenhoEstrutura ConsultaEmpenhoEstrutura(string key, string password, int year, string regional, string cfp, string nature, int program, string resourceSource, string process)
        {
            var result = DataHelperProdespEmpenho.Procedure_ConsultaEmpenhoEstrutura(
                key, password, year, regional, cfp, nature, resourceSource, process)
                ?? new ProdespEmpenho.Procedure_ConsultaEmpenhoEstruturaRecordType[] { };

            var resultItem = result.FirstOrDefault();

            if (!string.IsNullOrEmpty(resultItem?.outErro))
                throw new Exception($"Prodesp - {resultItem?.outErro}");

            return new ConsultaEmpenhoEstrutura
            {
                OutCodAplicacao = resultItem?.outCodAplicacao,
                OutCED = resultItem?.outCED,
                OutErro = resultItem?.outErro,
                OutTerminal = resultItem?.outTerminal,
                OutSucesso = resultItem?.outSucesso,
                OutDispSubEmpenhar = resultItem?.outDispSubEmpenhar,
                OutNrEmpenho = resultItem?.outNrEmpenho,
                OutTipoEmpenho = resultItem?.outTipoEmpenho,
                OutValorAtual = resultItem?.outValorAtual,
                OutValorSubEmpenhado = resultItem?.outValorSubEmpenhado,

                ListConsultaEstrutura = result.Select(x => new ListConsultaEstrutura
                {
                    OutCodAplicacao = x.outCodAplicacao,
                    OutCED = x.outCED,
                    OutErro = x.outErro,
                    OutTerminal = x.outTerminal,
                    OutSucesso = x.outSucesso,
                    OutDispSubEmpenhar = x.outDispSubEmpenhar,
                    OutNrEmpenho = x.outNrEmpenho,
                    OutTipoEmpenho = x.outTipoEmpenho,
                    OutValorAtual = x.outValorAtual,
                    OutValorSubEmpenhado = x.outValorSubEmpenhado,

                }).ToList()
            };
        }

        public bool InserirDoc(string key, string password, IEmpenho entity, string type)
        {
            try
            {
                var siafemSiafisicoNumber = $"{entity.NumeroEmpenhoSiafem}{entity.NumeroEmpenhoSiafisico}";

                var result = DataHelperProdespReserva.Procedure_InclusaoDocSIAFEM(
                    key, password, siafemSiafisicoNumber, entity.NumeroEmpenhoProdesp, type)
                    ?? new ProdespReserva.Procedure_InclusaoDocSIAFEMRecordType[] { };

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

        public string InserirEmpenho(string key, string password, Empenho entity, IEnumerable<IMes> months, Programa program, Estrutura structure, Fonte source, Regional regional)
        {
            try
            {
                var result = DataHelperProdespEmpenho.Procedure_InclusaoEmpenho(
                    key, password, entity, months, program, structure, source, regional)
                    ?? new ProdespEmpenho.Procedure_InclusaoEmpenhoRecordType[] { };

                var resultItem = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outNumEmpenho;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public string InserirEmpenhoCancelamento(string key, string password, EmpenhoCancelamento entity, IEnumerable<IMes> months, Fonte source)
        {
            try
            {
                var result = DataHelperProdespEmpenho.Procedure_AnulacaoEmpenho(
                    key, password, entity, months, source)
                    ?? new ProdespEmpenho.Procedure_AnulacaoEmpenhoRecordType[] { };

                var resultItem = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItem?.outTerminal;

                if (!string.IsNullOrEmpty(resultItem?.outErro))
                    throw new Exception($"Prodesp - {resultItem?.outErro}");

                return resultItem?.outNumAnulacaoEmp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.Contains("EntradaCICS_Fora")
                    ? "Erro na comunicação com WebService Prodesp."
                    : ex.Message);
            }
        }

        public string InserirEmpenhoReforco(string key, string password, EmpenhoReforco entity, IEnumerable<IMes> months, Fonte source)
        {
            try
            {
                var result = DataHelperProdespEmpenho.Procedure_EmpenhoReforco(
                    key, password, entity, months, source)
                    ?? new ProdespEmpenho.Procedure_ReforcoEmpenhoRecordType[] { };


                var resultItems = result.FirstOrDefault();

                if (HttpContext.Current != null)
                    HttpContext.Current.Session["terminal"] = resultItems?.outTerminal;

                if (!string.IsNullOrEmpty(resultItems?.outErro))
                    throw new Exception($"Prodesp - {resultItems?.outErro}");

                return resultItems?.outNumReforcoEmp;
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
