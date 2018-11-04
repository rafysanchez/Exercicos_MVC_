
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Model.ValueObject.PagamentoContaUnica;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ImpressaoRelacaoRERTService : CommonService
    {
        private readonly ICrudImpressaoRelacaoRERT _repository;
        private readonly ICrudListaRe _repositoryListaRe;
        private readonly ICrudListaRt _repositoryListaRt;
        private readonly SiafemPagamentoContaUnicaService _siafem;
        private readonly ChaveCicsmoService _chave;

        private string USUARIO = AppConfig.WsSiafemUser;
        private string SENHA = AppConfig.WsPassword;
        private string UNIDADE_GESTORA = "162101";

        public ImpressaoRelacaoRERTService(ILogError log, ICommon common, IChaveCicsmo chave, ISiafemPagamentoContaUnica siafem,
            ICrudImpressaoRelacaoRERT repository, ICrudListaRe repositoryListaRe, ICrudListaRt repositoryListaRt)
            : base(log, common, chave)
        {
            _repository = repository;

            _repositoryListaRe = repositoryListaRe;

            _repositoryListaRt = repositoryListaRt;

            _siafem = new SiafemPagamentoContaUnicaService(log, siafem);

            if (AppConfig.WsUrl == "siafemProd")
            {
                SENHA = Encrypt(AppConfig.WsPassword);
            }

            _chave = new ChaveCicsmoService(log, chave);
        }

        public AcaoEfetuada Excluir(int id, int recursoId, short action)
        {
            try
            {
                //a procedure se encarrega de excluir uma RE ou RT desta relação de impressão
                _repository.Remove(id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public IEnumerable<ImpressaoRelacaoRERT> Listar(ImpressaoRelacaoRERT entity)
        {
            return _repository.Fetch(entity);
        }

        public ImpressaoRelacaoRERT GetPorId(int id, int recursoId, short action)
        {
            try
            {
                return _repository.Get(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public int GetNumeroAgrupamento()
        {
            return _repository.GetNumeroAgrupamento();
        }

        public IEnumerable<ImpressaoRelacaoRERT> BuscarGrid(ImpressaoRelacaoRERT entity,
            DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            return _repository.FetchForGrid(entity, de, ate);
        }

        public ImpressaoRelacaoReRtConsultaPaiVo Selecionar(int id, int recursoId, short action)
        {
            try
            {
                var entity = new ImpressaoRelacaoReRtConsultaVo
                {
                    Id = id,
                };

                var listaFilhos = _repository.Fetch(entity);

                var retorno = new ImpressaoRelacaoReRtConsultaPaiVo
                {
                    Id = id,
                    Filhos = listaFilhos
                };

                var objFilhos = listaFilhos.FirstOrDefault();

                if (objFilhos != null)
                {
                    retorno.DataCadastro = objFilhos.DataCadastro;
                    retorno.DataTransmissaoSiafem = objFilhos.DataTransmissaoSiafem;
                    retorno.FlagTransmitidoSiafem = objFilhos.FlagTransmitidoSiafem;
                    retorno.CodigoUnidadeGestora = objFilhos.CodigoUnidadeGestora;
                    retorno.CodigoGestao = objFilhos.CodigoGestao;
                    retorno.CodigoBanco = objFilhos.CodigoBanco;
                    retorno.MsgRetornoTransmissaoSiafem = objFilhos.MsgRetornoTransmissaoSiafem;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public ImpressaoRelacaoReRtConsultaPaiVo SelecionarPorAgrupamento(int idAgrupamento, string ug, string gestao, string banco, 
                                                                                                        int recursoId, short action)
        {
            try
            {
                ImpressaoRelacaoReRtConsultaPaiVo resultado = new ImpressaoRelacaoReRtConsultaPaiVo();

                var listaReRtMaisFilhos = _repository.Fetch(idAgrupamento);

                resultado.CodigoUnidadeGestora = ug;
                resultado.CodigoGestao = gestao;
                resultado.CodigoBanco = banco;

                resultado.Filhos = listaReRtMaisFilhos;

                return resultado;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public List<string> ListarCodigoReRtPorAgrupamento(int idAgrupamento, int recursoId, short action)
        {
            try
            {
                var listaReRt = _repository.Fetch(idAgrupamento);

                var lista = listaReRt.Select(a => a.CodigoRelacaoRERT).OrderBy(x => x).Distinct().ToList();

                return lista;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public CancelarImpressaoRelacaoReRt CancelarImpressaoRelacaoReRt(string unidadeGestora, string Gestao, string prefixoREouRT, string numREouRT, int recursoId, short action)
        {
            try
            {
                var data = _siafem.CancelarImpressaoRelacaoReRt(USUARIO, SENHA, unidadeGestora, Gestao, prefixoREouRT, numREouRT);
                return data;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

    public int SalvarOuAlterar(ImpressaoRelacaoRERT entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                if (recursoId > 0) LogSucesso(action, recursoId, $"Impressão Relação RE e RT : Codigo {entity.Id}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public int SalvarOuAlterarRe(ListaRE entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repositoryListaRe.Save(entity);

                if (recursoId > 0) LogSucesso(action, recursoId, $"Lista RE : Codigo {entity.Id}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public int SalvarOuAlterarRt(ListaRT entity, int recursoId, short action)
        {
            try
            {
                entity.Id = _repositoryListaRt.Save(entity);

                if (recursoId > 0) LogSucesso(action, recursoId, $"Lista RT : Codigo {entity.Id}");

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public RespostaImpressaoRelacaoReRt TransmitirImpressaoRelacaoReRt(string unidadeGestora, string Gestao, string banco, string dataSolicitacao, string numeroRelatorio, int recursoId, short action, string chamarApenasRT)
        {
            try
            {
                if (chamarApenasRT == "" || chamarApenasRT == "RE")
                {
                    var data = _siafem.TransmitirImpressaoRelacaoRE(USUARIO, SENHA, unidadeGestora, Gestao, banco, dataSolicitacao, numeroRelatorio);
                    return data;
                }
                else
                {
                    var data = _siafem.TransmitirImpressaoRelacaoRT(USUARIO, SENHA, unidadeGestora, Gestao, banco, dataSolicitacao, numeroRelatorio);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }
    }
}