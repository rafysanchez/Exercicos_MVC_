using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaDer;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class AutorizacaoDeOBService : CommonService
    {
        private readonly ICrudProgramacaoDesembolsoExecucao _repositoryExecucao;
        private readonly ICrudProgramacaoDesembolsoExecucaoItem _repositoryExecucaoItem;

        private readonly ICrudAutorizacaoDeOB _repository;
        private readonly ICrudAutorizacaoDeOBItem _repositoryItem;

        private readonly SiafemPagamentoContaUnicaService _siafem;
        private readonly ProdespPagamentoContaDerService _prodespContaDer;

        private string USUARIO = AppConfig.WsSiafemUser;
        private string SENHA = AppConfig.WsPassword;
        private string UNIDADE_GESTORA = "162101";

        private readonly ChaveCicsmoService _chave;

        #region Constructor(s)
        public AutorizacaoDeOBService(ILogError log, ICommon common, IChaveCicsmo chave, ICrudAutorizacaoDeOB repository, ICrudAutorizacaoDeOBItem repositoryItem, SiafemPagamentoContaUnicaService siafem, ICrudProgramacaoDesembolsoExecucao repositoryExecucao, ICrudProgramacaoDesembolsoExecucaoItem repositoryExecucaoItem, ProdespPagamentoContaDerService prodespContaDer) : base(log, common, chave)
        {
            this._repository = repository;
            this._repositoryItem = repositoryItem;
            this._siafem = siafem;
            this._repositoryExecucao = repositoryExecucao;
            this._repositoryExecucaoItem = repositoryExecucaoItem;
            this._prodespContaDer = prodespContaDer;

            _chave = new ChaveCicsmoService(log, chave);

        }
        #endregion Constructor(s)

        #region Consulta Dependencias
        public List<PDExecucaoTipoPagamento> TiposPagamento()
        {
            return _repository.FetchTiposPagamento().ToList();
        }
        #endregion Consulta Dependencias

        public IEnumerable<OBAutorizacaoItem> ConsultarItensDaOB(int id)
        {
            return _repositoryItem.Fetch(id);
        }

        public IEnumerable<OBAutorizacaoItem> ConsultarItems(OBAutorizacaoItem entity, int? tipoExecucao, DateTime? De, DateTime? Ate)
        {
            return _repositoryItem.FetchForGrid(entity, tipoExecucao, De, Ate);
        }

        public IEnumerable<OBAutorizacao> ConsultarItensAgrupadosPorOB(OBAutorizacao entity, DateTime? De, DateTime? Ate)
        {
            return _repository.FetchForGrid(entity, De, Ate);
        }

        public OBAutorizacao Selecionar(int Id, int recursoId, short action)
        {
            try
            {
                var retorno = _repository.Get(Id);
                retorno.Items = _repositoryItem.Fetch(new OBAutorizacaoItem() { IdAutorizacaoOB = retorno.IdAutorizacaoOB.GetValueOrDefault() }).ToList();
                //retorno.Items = _repositoryItem.Fetch(new OBAutorizacaoItem() { Agrupamento = retorno.Agrupamento.GetValueOrDefault() }).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public OBAutorizacao Selecionar(int Id, string numOB, int recursoId, short action)
        {
            try
            {
                var retorno = _repository.Get(Id);
                retorno.Items = _repositoryItem.Fetch(new OBAutorizacaoItem() { IdAutorizacaoOB = retorno.IdAutorizacaoOB.GetValueOrDefault(), NumOB = numOB }).ToList();
                //retorno.Items = _repositoryItem.Fetch(new OBAutorizacaoItem() { Agrupamento = retorno.Agrupamento.GetValueOrDefault() }).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        //public ProgramacaoDesembolso SelecionarDadosApoio(string dsNumPD)
        //{
        //    var entity = _repositoryExecucao.GetDadosApoio(dsNumPD);
        //    return entity;
        //}

        public OBAutorizacaoItem SelecionarDadosApoio(int tipo, string dsNumPD)
        {
            var entity = _repositoryItem.GetDadosApoio(2, dsNumPD);
            return entity;
        }

        public string ConsultaOP(string numeroDocumentoGerador)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave();

                var NumeroOP = _prodespContaDer.ConsultaOP(cicsmo.Chave, cicsmo.Senha, numeroDocumentoGerador);
                return NumeroOP;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        public List<ConsultaOB> ConsultarOB(Usuario user, string unidadeGestora = "", string Gestao = "", string NumeroOB = "", int agrupamento = 0)
        {
            try
            {
                var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

                USUARIO = dadosUsuarioSiafem.usuario;
                SENHA = dadosUsuarioSiafem.senha;

                //List<ConsultaOB> retorno = new List<ConsultaOB>();
                var data = _siafem.ListarOB(USUARIO, Decrypt(SENHA), unidadeGestora, Gestao, null);
                //data.RemoveAll(item => AutorizacaoOBExiste(item.NoOB ?? item.NumeroOB, agrupamento));

                foreach (var item in data)
                {
                    ComplementarDadosOb(item);
                }

                return data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ComplementarDadosOb(ConsultaOB item)
        {
            var dataSids = SelecionarDadosApoio(2, item.NoOB);

            if (dataSids != null)
            {
                item.AgrupamentoItemOB = dataSids.AgrupamentoItemOB ?? 0;
                item.NumeroContrato = dataSids?.NumeroContrato;
                item.NumeroDocumentoGerador = dataSids?.NumeroDocumentoGerador;
                item.IdTipoDocumento = dataSids.IdTipoDocumento;
                item.NumeroDocumento = dataSids?.NumeroDocumento;
                item.TransmissaoItemStatusSiafem = dataSids?.TransmissaoItemStatusSiafem;
                item.TransmissaoItemMensagemSiafem = dataSids?.TransmissaoItemMensagemSiafem;
                item.TransmissaoItemStatusProdesp = dataSids?.TransmissaoItemStatusProdesp;
                item.TransmissaoItemMensagemProdesp = dataSids?.TransmissaoItemMensagemProdesp;
                item.NumOP = dataSids?.NumOP;
                item.IdExecucaoPD = dataSids.IdExecucaoPD;
                item.IdExecucaoPDItem = dataSids.IdExecucaoPDItem;
                item.IdAutorizacaoOB = dataSids.IdAutorizacaoOB;
                item.IdAutorizacaoOBItem = dataSids.IdAutorizacaoOBItem;
                item.MensagemRetornoConsultaOP = dataSids.MensagemRetornoConsultaOP;
                item.Despesa = (item.Despesa == string.Empty || item.Despesa == "00000000") ? dataSids.CodigoDespesa : item.Despesa.Substring(6, 2);
                item.Banco = item.Banco.PadLeft(3, '0');
                item.CodigoAplicacaoObra = dataSids.CodigoAplicacaoObra;
            }
            else
            {
                item.MensagemRetornoConsultaOP = "OB não existe na base SIDS";
            }
        }

        private bool AutorizacaoOBExiste(string NumeroOB, int agrupamento)
        {
            var ob_existe = _repositoryItem.FetchForGrid(new OBAutorizacaoItem() { NumOB = NumeroOB }, null, null, null);
            if (ob_existe.Any())
            {
                var ob = ob_existe.FirstOrDefault();
                if (ob.IdAutorizacaoOB != agrupamento && ob.TransmissaoItemStatusSiafem == "S")
                {
                    return true;
                }
            }

            return false;
        }

        public ConsultaOB ConsultarOBForReport(Usuario user, string unidadeGestora = "", string Gestao = "", string NumeroOB = "", int agrupamento = 0)
        {
            try
            {
                var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

                USUARIO = dadosUsuarioSiafem.usuario;
                SENHA = dadosUsuarioSiafem.senha;

                var partes = NumeroOB.Split(new string[] { "OB" }, StringSplitOptions.None);
                var data = _siafem.ConsultaOB(USUARIO, Decrypt(SENHA), unidadeGestora, Gestao, partes[1]);
                return data;
            }
            catch
            {
                throw;
            }
        }

        public OBAutorizacaoItem SelecionarItem(int id, int recursoId, short action)
        {
            try
            {
                var retorno = _repositoryItem.Get(id);
                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public OBAutorizacaoItem SelecionarItem(int id, string numOb, int recursoId, short action)
        {
            try
            {
                var retorno = _repositoryItem.Get(id, numOb);
                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public int Salvar(ref OBAutorizacao entity, List<string> checados, int recursoId, short action)
        {
            try
            {
                var id = _repository.Save(entity);
                entity.IdAutorizacaoOB = id;

                //int numeroAgrupamentoOb = action == (int)EnumAcao.Inserir ? _repository.GetNumeroAgrupamento() : 0;

                foreach (var item in entity.Items)
                {
                    //if (action == 1)
                    //{
                    //    item.AgrupamentoItemOB = numeroAgrupamentoOb;
                    //}

                    item.IdAutorizacaoOB = id;
                    item.AgrupamentoItemOB = item.Codigo.HasValue && item.Codigo != 0 ? 0 : item.AgrupamentoItemOB;
                    var iditem = SalvarItem(item);
                    item.Codigo = iditem;
                    item.IdAutorizacaoOBItem = iditem;
                }

                return id;

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public int SalvarItem(OBAutorizacaoItem entity)
        {
            return _repositoryItem.Save(entity);
        }

        public void AutorizarOB(OBAutorizacao model, List<string> checados, object viewModel, string UGMudapah, int recursoId, short action, Usuario user)
        {
            bool executar = false;

            var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

            USUARIO = dadosUsuarioSiafem.usuario;
            SENHA = dadosUsuarioSiafem.senha;

            try
            {
                foreach (var item in model.Items)
                {
                    executar = checados.Contains(item.NumOB);

                    if (item.TransmissaoItemStatusSiafem != "S" && executar)
                    {
                        AutorizarItemOB(user, item, UGMudapah);
                    }
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public void AutorizarItemOB(Usuario user, OBAutorizacaoItem item, string UGMudapah)
        {
            try
            {
                //var ob = new string[2];
                //if (item.NumOB.Length > 5)
                //{
                //    ob = item.NumOB.Split(new string[] { "OB" }, StringSplitOptions.None);
                //}
                //else
                //{
                //    ob[0] = DateTime.Now.Year.ToString();
                //    ob[1] = item.NumOB;
                //}

                var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

                USUARIO = dadosUsuarioSiafem.usuario;
                SENHA = dadosUsuarioSiafem.senha;

                var retornoSiafem = _siafem.AutorizarOB(USUARIO, Decrypt(SENHA), UGMudapah, item.GestaoPagadora, item.NumOB, item.ValorItem.ToString());

                item.NumOB = item.NumOB.Length < 6 ? DateTime.Now.Year.ToString() + "OB" + item.NumOB : item.NumOB;
                item.TransmissaoItemTransmitidoSiafem = true;
                item.TransmissaoItemStatusSiafem = "S";
                item.TransmissaoItemDataSiafem = DateTime.Now;
                item.TransmissaoItemMensagemSiafem = null;
            }
            catch (Exception ex)
            {
                item.TransmissaoItemStatusSiafem = "E";
                item.TransmissaoItemTransmitidoSiafem = false;
                item.TransmissaoItemMensagemSiafem = ex.Message;
            }
            finally
            {
                item.AgrupamentoItemOB = 0;
                _repositoryItem.Save(item);
            }
        }

        public void AtualizarDataConfirmacaoItem(IEnumerable<OBAutorizacaoItem> entity)
        {
            foreach (var item in entity)
            {
                _repositoryItem.Save(item);
            }
        }

        public bool DeletarNaoAgrupados(int Id, int recursoId, short action)
        {
            try
            {
                _repositoryItem.DeletarNaoAgrupados(Id);
                return true;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public bool Deletar(int Id, int recursoId, short action)
        {
            var autorizacaoOB = Selecionar(Id, recursoId, action);

            if (autorizacaoOB == null)
            {
                throw new SidsException("Não foi possivel excluir pois o item selecionado não existe.");
            }

            var itensTransmitidos = autorizacaoOB.Items.Where(x => x.TransmissaoItemStatusSiafem == "S").ToList();

            if (itensTransmitidos.Any())
            {
                throw new SidsException("Não foi possivel excluir a autorização de OB pois existem items já trasmitidos.");
            }

            try
            {
                _repository.Remove(autorizacaoOB.IdAutorizacaoOB.GetValueOrDefault());
                return true;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public ConsultaOB ComplementarDadosOb(Usuario user, string unidadeGestoraLiquidante, string gestaoLiquidante, string unidadeGestora, string numeroPD, int agrupamento, TIPO_CONSULTA_PD origem = TIPO_CONSULTA_PD.CONSULTAR)
        {
            if (origem == TIPO_CONSULTA_PD.ADICIONAR && AutorizacaoOBExiste(numeroPD, agrupamento))
            {
                throw new SidsException("A Autorizacao de OB já existe em outro agrupamento.");
            }

            var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

            USUARIO = dadosUsuarioSiafem.usuario;
            SENHA = dadosUsuarioSiafem.senha;

            ConsultaOB data = _siafem.ConsultaOB(USUARIO, Decrypt(SENHA), unidadeGestoraLiquidante, gestaoLiquidante, numeroPD);

            var dataSids = SelecionarDadosApoio(1, numeroPD);

            if (dataSids != null)
            {
                data.AgrupamentoItemOB = dataSids.AgrupamentoItemOB ?? 0;
                data.NumeroContrato = dataSids?.NumeroContrato;
                data.NumeroDocumentoGerador = dataSids?.NumeroDocumentoGerador;
                data.IdTipoDocumento = dataSids.DocumentoTipoId;
                data.NumeroDocumento = dataSids?.NumeroDocumento;
                data.TransmissaoItemStatusSiafem = dataSids?.TransmissaoItemStatusSiafem;
                data.TransmissaoItemMensagemSiafem = dataSids?.TransmissaoItemMensagemSiafem;
                data.TransmissaoItemStatusProdesp = dataSids?.TransmissaoItemStatusProdesp;
                data.TransmissaoItemMensagemProdesp = dataSids?.TransmissaoItemStatusProdesp;
                data.CgcCpfUG = dataSids.Favorecido;

                if (data.NumeroDocumentoGerador != null)
                {
                    var numeroOP = ConsultaOP(data.NumeroDocumentoGerador);
                    var partes = numeroOP.Split(new string[] { "@" }, StringSplitOptions.None);
                    if (partes[0].Any())
                    {
                        data.NumOP = partes[0];
                    }
                    else
                    {
                        data.MensagemRetornoConsultaOP = partes[1];
                    }
                }

            }
            else
            {
                data.MensagemRetornoConsultaOP = "OB não existe na base SIDS";
            }

            return data;
        }

        #region Enums
        public enum TIPO_CONSULTA_PD
        {
            CONSULTAR = 1,
            ADICIONAR = 2
        }
        #endregion Enums
    }

}