using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.ProgramacaoDesembolso;

namespace Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica
{
    using System;
    using Base;
    using Infrastructure.DataBase.Configuracao;
    using Model.Interface.Log;
    using Model.Interface.Service.PagamentoContaUnica;
    using Model.Entity.PagamentoContaUnica.ListaBoletos;
    using Model.Entity.PagamentoContaUnica.PagamentoDer;
    using Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
    using Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;

    public class ProdespPagamentoContaUnicaService : BaseService
    {

        private readonly IProdespPagamentoContaUnica _contaUnica;
        private readonly RegionalService _regional;

        public ProdespPagamentoContaUnicaService(ILogError logError, IProdespPagamentoContaUnica contaUnica) : base(logError)
        {
            _contaUnica = contaUnica;
            _regional = new RegionalService(logError, new RegionalDal());
        }

        public object DesdobramentoApoio(Desdobramento desdobramento, string key, string password)
        {
            return _contaUnica.ConsultaDesdobramentoApoio(key, password, desdobramento);
        }
              
        public IEnumerable<Credor> ConsultaCredorReduzido(string key, string password)
        {
            var result = new List<Credor>();

            var cods = new List<string> { "7", "8" };

            foreach (var cod in cods)
            {
                result.AddRange(_contaUnica.ConsultaCredorReduzido(key, password, cod));
            }

            return result;
        }

        public void Inserir_DesdobramentoISSQN(string key, string password, ref Desdobramento entity)
        {
            _contaUnica.Inserir_DesdobramentoISSQN(key, password, ref entity);
        }

        public void Inserir_DesdobramentoOutros(string key, string password, ref Desdobramento entity)
        {
            _contaUnica.Inserir_DesdobramentoOutros(key, password, ref entity);
        }

        public void Inserir_PreparacaoPagamento(string key, string password, ref PreparacaoPagamento entity)
        {
            var orgao = _regional.Buscar(new Regional {Id = entity.RegionalId}).FirstOrDefault();
             _contaUnica.Inserir_PreparacaoPagamento(key, password, ref entity, orgao);
        }

        public void Inserir_ConfirmacaoPagamento(string key, string password, ref PDExecucaoItem entity)
        {
            var orgao = _regional.Buscar(new Regional { Id = entity.RegionalId }).FirstOrDefault();
            _contaUnica.Inserir_ConfirmacaoPagamento(key, password, ref entity, orgao);
        }

        public void Inserir_ConfirmacaoPagamento(string key, string password, ref OBAutorizacaoItem entity)
        {
            var orgao = _regional.Buscar(new Regional { Id = entity.RegionalId }).FirstOrDefault();
            _contaUnica.Inserir_ConfirmacaoPagamento(key, password, ref entity, orgao);
        }

        public void AnulacaoDesdobramento(string key, string password, ref Desdobramento entity)
        {
            _contaUnica.AnulacaoDesdobramento(key, password, ref entity);
        }

        public IEnumerable<ConsultaDesdobramento> ConsultaDesdobramento(string key, string password, string number, int type)
        {
            return _contaUnica.ConsultaDesdobramento(key, password, number, type);
        }

        public object PreparacaoPagamentoApoio(PreparacaoPagamento preparacaoPagamento ,string key, string password)
        {
            return _contaUnica.ConsultaPreparacaoPagamentoApoio(key, password, preparacaoPagamento);
        }
        

        public object PreparacaoPgtoTipoDespesaDataVenc(PreparacaoPagamento preparacaoPagamento, string key, string password)
        {
            return _contaUnica.ConsultarPreparacaoPgtoTipoDespesaDataVenc(key, password, preparacaoPagamento);
        }


        public object PreparacaoPgtoTipoDespesaDataVenc2(ArquivoRemessa arquivoRemessa, string key, string password)
        {
            return _contaUnica.ConsultarPreparacaoPgtoTipoDespesaDataVenc2(key, password, arquivoRemessa);
        }

        public IEnumerable<ProgramacaoDesembolsoAgrupamento> ConsultaDocumentoGerador(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            var orgao = _regional.Buscar(new Regional { Id = programacaoDesembolso.RegionalId }).FirstOrDefault();
            return _contaUnica.ConsultaDocumentoGerador(key, password, programacaoDesembolso, orgao);
        }

        public void CancelamentoOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            _contaUnica.CancelamentoOp(key, password, programacaoDesembolso);
        }

        public void BoqueioOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            _contaUnica.BloqueioOp(key, password, programacaoDesembolso);
        }
        public void DesbloqueioOp(string key, string password, IProgramacaoDesembolso programacaoDesembolso)
        {
            _contaUnica.DesbloqueioOp(key, password, programacaoDesembolso);
        }
        public IEnumerable<object> CancelamentoOpApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            return _contaUnica.CancelamentoOpApoio(key, password, programacaoDesembolso);
        }

        public IEnumerable<object> BloqueioOpApoio(string key, string password, ProgramacaoDesembolso programacaoDesembolso)
        {
            return _contaUnica.BloqueioOpApoio(key, password, programacaoDesembolso);
        }

        internal void InserirDoc(ListaBoletos entity, string chave, string senha, string tipo)
        {
            _contaUnica.InserirDoc(entity, chave, senha, tipo);
        }
    }
}
