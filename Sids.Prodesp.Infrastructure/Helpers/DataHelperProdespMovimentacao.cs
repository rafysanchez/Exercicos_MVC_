using System;
//using Sids.Prodesp.Infrastructure.ProdespPagamentoContaDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Infrastructure.ProdespMovimentacao;
using Sids.Prodesp.Model.Entity.Configuracao;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sids.Prodesp.Model.Extension;

namespace Sids.Prodesp.Infrastructure.Helpers
{
    public class DataHelperProdespMovimentacao
    {
        public static Procedure_MovOrcamentariaInternaRecordType[] Procedure_MovOrcamentariaInterna(string key, string password, string impressora, Programa programa, Estrutura estrutura, List<MovimentacaoReducaoSuplementacao> items)
        {
            var filter = new Procedure_MovOrcamentariaInternaFiltersType();

            if (items.Any())
            {
                filter.inChave = password;
                filter.inImpressora = impressora;
                filter.inOperador = key;

                filter.inCFP_1 = programa.Cfp.Substring(0, 2);
                filter.inCFP_2 = programa.Cfp.Substring(2, 3);
                filter.inCFP_3 = programa.Cfp.Substring(5, 4);
                filter.inCFP_4 = programa.Cfp.Substring(9, 4);

                filter.inCFP_5 = estrutura.Fonte.PadRight(4, '0');

                filter.inCED_1 = estrutura.Natureza.Substring(0, 1);
                filter.inCED_2 = estrutura.Natureza.Substring(1, 1);
                filter.inCED_3 = estrutura.Natureza.Substring(2, 1);
                filter.inCED_4 = estrutura.Natureza.Substring(3, 1);
                filter.inCED_5 = estrutura.Natureza.Substring(4, 2);
                

                for (int i = 0; i < 15; i++)
                {
                    var item = i < items.Count ? items[i] : new MovimentacaoReducaoSuplementacao();
                    
                    MovOrcamentariaInternaFiltersType(i+1, item, filter);
                }
            }            

            return new WSMovimentacao().Procedure_MovOrcamentariaInterna(filter, new ModelVariablesType(), new EnvironmentVariablesType());
        }

        private static Procedure_MovOrcamentariaInternaFiltersType MovOrcamentariaInternaFiltersType(int posicao, MovimentacaoReducaoSuplementacao item, Procedure_MovOrcamentariaInternaFiltersType filter)
        {
            var totalGeral = item.TotalQ1 + item.TotalQ2 + item.TotalQ3 + item.TotalQ4;

            filter.GetType().GetProperty("inAplicObra_1_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.NrObra) ? string.Empty : item.NrObra.Substring(0, 6));
            filter.GetType().GetProperty("inAplicObra_2_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.NrObra) ? string.Empty : item.NrObra.Substring(6, 1));
            filter.GetType().GetProperty("inDestinoRec_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.DestinoRecurso) ? string.Empty : item.DestinoRecurso);

            filter.GetType().GetProperty("inFLProc_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.FlProc) ? string.Empty : item.FlProc);
            filter.GetType().GetProperty("inCodAssinAUTO_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.CodigoAutorizadoAssinatura) ? string.Empty : item.CodigoAutorizadoAssinatura);
            filter.GetType().GetProperty("inCodAssinEXAM_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.CodigoExaminadoAssinatura) ? string.Empty : item.CodigoExaminadoAssinatura);
            filter.GetType().GetProperty("inCodAssinRESP_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.CodigoResponsavelAssinatura) ? string.Empty : item.CodigoResponsavelAssinatura);
            filter.GetType().GetProperty("inGrupoAssinAUTO_Tela" + posicao).SetValue(filter, item.CodigoAutorizadoGrupo == 0 ? string.Empty : item.CodigoAutorizadoGrupo.ToString());
            filter.GetType().GetProperty("inGrupoAssinEXAM_Tela" + posicao).SetValue(filter, item.CodigoExaminadoGrupo == 0 ? string.Empty : item.CodigoExaminadoGrupo.ToString());
            filter.GetType().GetProperty("inGrupoAssinRESP_Tela" + posicao).SetValue(filter, item.CodigoResponsavelGrupo == 0 ? string.Empty : item.CodigoResponsavelGrupo.ToString());
            filter.GetType().GetProperty("inOrgaoAssinAUTO_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.CodigoAutorizadoOrgao) ? string.Empty : item.CodigoAutorizadoOrgao);
            filter.GetType().GetProperty("inOrgaoAssinEXAM_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.CodigoExaminadoOrgao) ? string.Empty : item.CodigoExaminadoOrgao);
            filter.GetType().GetProperty("inOrgaoAssinRESP_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.CodigoResponsavelOrgao) ? string.Empty : item.CodigoResponsavelOrgao);
            filter.GetType().GetProperty("inOrgao_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.NrOrgao) ? string.Empty : item.NrOrgao);
            filter.GetType().GetProperty("inOrigemRec_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.OrigemRecurso) ? string.Empty : item.OrigemRecurso);
            filter.GetType().GetProperty("inProcesso_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.NrProcesso) ? string.Empty : item.NrProcesso);

            filter.GetType().GetProperty("inTotal_Tela" + posicao).SetValue(filter, totalGeral == 0 ? string.Empty : totalGeral.ToString());
            filter.GetType().GetProperty("inS_R_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.RedSup) ? string.Empty : item.RedSup);
            filter.GetType().GetProperty("inCodEspecificacao_Tela" + posicao).SetValue(filter, string.IsNullOrEmpty(item.EspecDespesa) ? string.Empty : item.EspecDespesa);

            var observacoes = (item.DescEspecDespesa ?? string.Empty).Split(';');

            for (int i = 0; i < 5; i++)
            {
                var prop = filter.GetType().GetProperty("inEspecificacao_" + (i + 1) + "_Tela" + posicao);
                if (prop != null)
                {
                    var obs = i < observacoes.Length ? observacoes[i] : string.Empty;
                    prop.SetValue(filter, obs);
                }
            }


            filter.GetType().GetProperty("inQuota1_Tela" + posicao).SetValue(filter, string.Empty);
            filter.GetType().GetProperty("inQuota2_Tela" + posicao).SetValue(filter, string.Empty);
            filter.GetType().GetProperty("inQuota3_Tela" + posicao).SetValue(filter, string.Empty);
            filter.GetType().GetProperty("inQuota4_Tela" + posicao).SetValue(filter, string.Empty);

            var quarter = DateTime.Now.GetQuarter();
            if (quarter <= 2)
            {
                filter.GetType().GetProperty("inQuota1_Tela" + posicao).SetValue(filter, item.TotalQ1 == 0 ? string.Empty : item.TotalQ1.ToString());
            }

            if (quarter <= 3)
            {
                filter.GetType().GetProperty("inQuota2_Tela" + posicao).SetValue(filter, item.TotalQ2 == 0 ? string.Empty : item.TotalQ2.ToString());
            }

            if (quarter <= 4)
            {
                filter.GetType().GetProperty("inQuota3_Tela" + posicao).SetValue(filter, item.TotalQ3 == 0 ? string.Empty : item.TotalQ3.ToString());
            }

            if (quarter <= 4)
            {
                filter.GetType().GetProperty("inQuota4_Tela" + posicao).SetValue(filter, item.TotalQ4 == 0 ? string.Empty : item.TotalQ4.ToString());
            }

            filter.GetType().GetProperty("inQuota5_Tela" + posicao).SetValue(filter, string.Empty);

            return filter;
        }

    }

    public class WSMovimentacao : Integracao_DER_SIAFEM_MovOrcamentariaService
    {
        public WSMovimentacao() : base()
        {
            var ambiente = AppConfig.WsUrl;

            if (ambiente == "siafemDes")
                this.Url = AppConfig.UrlMovimentacaoDes;
            if (ambiente == "siafemHom")
                this.Url = AppConfig.UrlMovimentacaoHom;
            if (ambiente == "siafemProd")
                this.Url = AppConfig.UrlMovimentacaoProd;

        }
    }
}
