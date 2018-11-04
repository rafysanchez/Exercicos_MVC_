
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;

namespace Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica
{
    public interface IProdespPagamentoContaUnica
    {
        object ConsultaDesdobramentoApoio(string key, string password, Desdobramento objModel);

        object ConsultaPreparacaoPagamentoApoio(string key, string password, PreparacaoPagamento objModel);

        object ConsultarPreparacaoPgtoTipoDespesaDataVenc(string key, string password, PreparacaoPagamento objModel);

        object ConsultarPreparacaoPgtoTipoDespesaDataVenc2(string key, string password, ArquivoRemessa objModel);

        IEnumerable<Credor> ConsultaCredorReduzido(string key, string password,string organizacao);

        void Inserir_DesdobramentoISSQN(string key, string password, ref Desdobramento entity);

        void Inserir_DesdobramentoOutros(string key, string password, ref Desdobramento entity);

        void Inserir_PreparacaoPagamento(string key, string password, ref PreparacaoPagamento entity, Regional orgao);

        void Inserir_ConfirmacaoPagamento(string key, string password, ref PDExecucaoItem entity, Regional orgao);

        void Inserir_ConfirmacaoPagamento(string key, string password, ref OBAutorizacaoItem entity, Regional orgao);

        void AnulacaoDesdobramento(string key, string password, ref Desdobramento entity);

        IEnumerable<ConsultaDesdobramento> ConsultaDesdobramento(string key, string password, string number, int type);

        IEnumerable<ProgramacaoDesembolsoAgrupamento> ConsultaDocumentoGerador(string key, string password, ProgramacaoDesembolso programacaoDesembolso, Regional orgao);

        void CancelamentoOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso);

        void BloqueioOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso);
        void DesbloqueioOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso);
        IEnumerable<object> BloqueioOpApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso);
        IEnumerable<object> CancelamentoOpApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso);

        void InserirDoc(ListaBoletos entity, string chave, string senha, string tipo);
    }

}
