using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaDer;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using System;
using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Core.Services.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ProgramacaoDesembolsoExecucaoService : CommonService
    {

        private readonly ICrudProgramacaoDesembolsoExecucao _repository;
        private readonly ICrudProgramacaoDesembolsoExecucaoItem _repositoryItem;

        private readonly SiafemPagamentoContaUnicaService _siafem;
        private readonly ProdespPagamentoContaDerService _prodespContaDer;

        private string USUARIO = AppConfig.WsSiafemUser;
        private string SENHA = AppConfig.WsPassword;
        private string UNIDADE_GESTORA = "162101";

        private readonly ChaveCicsmoService _chave;

        #region Constructor(s)
        public ProgramacaoDesembolsoExecucaoService(ILogError log, ICommon common, IChaveCicsmo chave, ICrudProgramacaoDesembolsoExecucao repository, ICrudProgramacaoDesembolsoExecucaoItem repositoryItem, SiafemPagamentoContaUnicaService siafem, ProdespPagamentoContaDerService prodespContaDer) : base(log, common, chave)
        {
            this._repository = repository;
            this._repositoryItem = repositoryItem;

            this._siafem = siafem;
            this._prodespContaDer = prodespContaDer;

            _chave = new ChaveCicsmoService(log, chave);

        }
        #endregion Constructor(s)

        #region Consulta Dependencias
        public List<PDExecucaoTipoExecucao> TiposExecucao()
        {
            return _repository.FetchTiposExecucao().ToList();
        }

        public List<PDExecucaoTipoPagamento> TiposPagamento()
        {
            return _repository.FetchTiposPagamento().ToList();
        }
        #endregion Consulta Dependencias

        #region Execucao PD
        public PDExecucao Selecionar(int Id, int recursoId, short action)
        {
            try
            {
                var retorno = _repository.Get(Id);
                retorno.Items = _repositoryItem.Fetch(new PDExecucaoItem() { id_execucao_pd = retorno.IdExecucaoPD.GetValueOrDefault() }).ToList();

                foreach (var item in retorno.Items)
                {
                    item.Prioritario = item.NouP != null && item.NouP.Equals("P");
                    item.AgrupamentoItemPD = item.id_execucao_pd;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public PDExecucaoItem SelecionarItem(int Id, int recursoId, short action)
        {
            try
            {
                var retorno = _repositoryItem.Get(Id);
                return retorno;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }

        public IEnumerable<PDExecucao> Consultar(PDExecucao entity, DateTime De, DateTime Ate)
        {
            return _repository.FetchForGrid(entity, De, Ate);
        }
        public IEnumerable<PDExecucaoItem> ConsultarItems(PDExecucaoItem entity, int? tipoExecucao, DateTime? De, DateTime? Ate)
        {
            return _repositoryItem.FetchForGrid(entity, tipoExecucao, De, Ate);
        }

        public int Salvar(ref PDExecucao entity, List<string> checados, int recursoId, short action)
        {
            try
            {
                entity.Id = entity.IdConfirmacaoPagamento;
                var id = _repository.Save(entity);
                entity.IdExecucaoPD = id;

                int numeroAgrupamentoPD = action == (int)EnumAcao.Inserir ? _repository.GetNumeroAgrupamento() : 0;

                foreach (var item in entity.Items)
                {
                    if (action == 1)
                    {
                        item.AgrupamentoItemPD = numeroAgrupamentoPD;
                    }

                    item.id_execucao_pd = id;
                    item.AgrupamentoItemPD = item.Codigo.HasValue && item.Codigo != 0 ? 0 : item.AgrupamentoItemPD;
                    var iditem = SalvarItem(item);
                    item.Codigo = iditem;
                }

                return id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }
        public int SalvarItem(PDExecucaoItem entity)
        {
            entity.NouP = entity.Prioritario ? "P" : "N";
            entity.fl_transmissao_transmitido_siafem = (entity.fl_transmissao_transmitido_siafem.HasValue && entity.fl_transmissao_transmitido_siafem.Value) || (entity.cd_transmissao_status_siafem != null && entity.cd_transmissao_status_siafem.Equals("S"));
            return _repositoryItem.Save(entity);
        }

        public void AtualizarDataConfirmacaoItem(IEnumerable<PDExecucaoItem> entity)
        {
            foreach (var item in entity)
            {
                _repositoryItem.Save(item);
            }
        }

        public bool Deletar(int Id, int recursoId, short action)
        {
            var execucaoPD = Selecionar(Id, recursoId, action);

            if (execucaoPD == null)
            {
                throw new SidsException("Não foi possivel excluir pois o item selecionado não existe.");
            }

            var itensTransmitidos = execucaoPD.Items.Where(x => x.cd_transmissao_status_siafem == "S").ToList();

            if (itensTransmitidos.Any())
            {
                throw new SidsException("Não foi possivel excluir a execução da pd pois existem items já trasmitidos.");
            }

            try
            {
                _repository.Remove(execucaoPD.IdExecucaoPD.GetValueOrDefault());
                return true;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }
        public bool DeletarItem(int Id, int recursoId, short action)
        {
            var item = _repositoryItem.Get(Id);
            if (item.cd_transmissao_status_siafem == "S")
            {
                throw new SidsException("Não foi possivel excluir a execução da pd pois o item já foi trasmitido.");
            }

            try
            {
                _repositoryItem.Remove(item.Codigo.GetValueOrDefault());
                return true;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
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

        #endregion Execucao PD

        #region SiafemWS
        private bool ExecucaoPDExiste(string NumeroPD, int agrupamento)
        {
            var pd_existe = _repositoryItem.FetchForGrid(new PDExecucaoItem() { NumPD = NumeroPD }, null, null, null);
            if (pd_existe.Any())
            {
                var pd = pd_existe.FirstOrDefault();
                if (pd.id_execucao_pd != agrupamento && pd.cd_transmissao_status_siafem == "S")
                {
                    return true;
                }
            }

            return false;
        }
        private bool ExecucaoPDExiste(string numeroPD, int agrupamento, IEnumerable<PDExecucaoItem> itemsExistentes)
        {

            if (itemsExistentes.Any(x => x.NumPD.Equals(numeroPD)))
            {
                var pd = itemsExistentes.FirstOrDefault(x => x.NumPD.Equals(numeroPD));
                if (pd.id_execucao_pd != agrupamento && pd.cd_transmissao_status_siafem == "S")
                {
                    return true;
                }
            }

            return false;
        }
        private bool PdTransmitidaSemAgrupamento(ConsultaPd item, int agrupamentoPd, List<ConsultaPd> itemsExistentes)
        {

            if (itemsExistentes.Any(x => x.NumPD.Equals(item.NumPD)))
            {
                var pd = itemsExistentes.FirstOrDefault(x => x.NumPD.Equals(item.NumPD));
                if (pd.IdExecucaoPD != agrupamentoPd && pd.TransmissaoItemStatusSiafem == "S")
                {
                    return true;
                }
            }

            return false;
        }
        public ConsultaPd ComplementarDadosPd(Usuario user, string unidadeGestoraLiquidante = "", string gestaoLiquidante = "", string unidadeGestora = "", string NumeroPD = "", int agrupamento = 0, TIPO_CONSULTA_PD origem = TIPO_CONSULTA_PD.CONSULTAR, string dsNumOB = "")
        {
            if (origem == TIPO_CONSULTA_PD.ADICIONAR && ExecucaoPDExiste(NumeroPD, agrupamento))
            {
                throw new SidsException("A Execução da PD já existe em outro agrupamento.");
            }

            var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

            USUARIO = dadosUsuarioSiafem.usuario;
            SENHA = dadosUsuarioSiafem.senha;

            var data = new ConsultaPd();
            if (dsNumOB == "" || dsNumOB == null)
            {
                data = _siafem.ConsultaPD(USUARIO, Decrypt(SENHA), unidadeGestoraLiquidante, gestaoLiquidante, unidadeGestora, NumeroPD);
            }

            var dataSids = SelecionarDadosApoio(1, NumeroPD);

            if (dataSids != null)
            {
                data.NumeroAgrupamentoProgramacaoDesembolso = dataSids.NumeroAgrupamento;
                data.NumeroContrato = dataSids?.NumeroContrato;
                data.NumeroDocumentoGerador = dataSids?.NumeroDocumentoGerador;
                data.IdTipoDocumento = dataSids.DocumentoTipoId;
                data.NumeroDocumento = dataSids?.NumeroDocumento;
                data.TransmissaoItemStatusSiafem = dataSids?.StatusSiafem;
                data.TransmissaoItemMensagemSiafem = dataSids?.MensagemServicoSiafem;
                data.TransmissaoItemStatusProdesp = dataSids?.StatusConfirmacaoPagtoProdesp;
                data.TransmissaoItemMensagemProdesp = dataSids?.MensagemConfirmacaoPagtoProdesp;
                data.NumeroCnpjCpfPgto = dataSids.NumeroCnpjcpfPagto;

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
                data.MensagemRetornoConsultaOP = "PD não existe na base SIDS";
            }

            return data;
        }

        public List<ConsultaPd> AdicionarPDDesdobrada(string numeroPD)
        {
            try
            {

                List<ConsultaPd> retorno = new List<ConsultaPd>();

                //data = _siafem.AdicionarPD(USUARIO, SENHA, unidadeGestoraLiquidante, gestaoLiquidante, unidadeGestora, NumeroPD);


                //var dataSids = SelecionarDadosApoio(1, NumeroPD);
                var dataSids = ListarPDSDesdobradas(1, numeroPD);

                foreach (var itemLista in dataSids)
                {
                    var data = new ConsultaPd();

                    data.NumPD = itemLista.NumeroSiafem;
                    data.NumeroContrato = itemLista?.NumeroContrato;
                    data.NumeroDocumentoGerador = itemLista?.NumeroDocumentoGerador;
                    data.IdTipoDocumento = itemLista.DocumentoTipoId;
                    data.NumeroDocumento = itemLista?.NumeroDocumento;
                    data.TransmissaoItemStatusSiafem = itemLista?.StatusSiafem;
                    data.TransmissaoItemMensagemSiafem = itemLista?.MensagemServicoSiafem;
                    data.TransmissaoItemStatusProdesp = itemLista?.StatusConfirmacaoPagtoProdesp;
                    data.TransmissaoItemMensagemProdesp = itemLista?.MensagemConfirmacaoPagtoProdesp;
                    data.NumeroCnpjCpfPgto = itemLista.NumeroCnpjcpfPagto;

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

                    retorno.Add(data);
                }

                //return data;
                return retorno;
            }
            catch
            {
                throw;
            }
        }

        public ProgramacaoDesembolso Selecionar(string dsNumPD)
        {
            var entity = _repository.Get(dsNumPD);
            return entity;
        }

        public ProgramacaoDesembolsoDadosApoio SelecionarDadosApoio(int tipo, string dsNumPD)
        {
            var entity = _repository.GetDadosApoio(tipo, dsNumPD);
            return entity;
        }

        public IEnumerable<ProgramacaoDesembolso> ListarPDSDesdobradas(int tipo, string dsNumPD)
        {
            var entity = _repository.ListDesdobradas(dsNumPD);
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

        public List<ConsultaPd> ConsultarPD(Usuario user, int agrupamento, string UgLiquidante = "", string GestaoLiquidante = "", string UgPagadora = "", string GestaoPagadora = "", string Favorecido = "", string DataInicial = "", string DataFinal = "", string AnoInicial = "", string AnoFinal = "", string Opcao = "", string TipoOB = "", string PdCanceladTotal = "")
        {
            try
            {
                var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

                USUARIO = dadosUsuarioSiafem.usuario;
                SENHA = dadosUsuarioSiafem.senha;

                List<ConsultaPd> itensRetorno = new List<ConsultaPd>();
                List<ConsultaPd> pdsSiafem = _siafem.ListarPD(USUARIO, Decrypt(SENHA), UNIDADE_GESTORA, UgLiquidante, GestaoLiquidante, UgPagadora, GestaoPagadora, Favorecido, DataInicial, DataFinal, AnoInicial, AnoFinal, Opcao, TipoOB, PdCanceladTotal);

                pdsSiafem.RemoveAll(item => ExecucaoPDExiste(item.NPD, agrupamento));

                List<string> opsConfirmadas = new List<string>();

                foreach (var itemSiafem in pdsSiafem)
                {
                    var itemSids = ComplementarDadosPd(user, UgLiquidante, GestaoLiquidante, UgPagadora, itemSiafem.PD ?? itemSiafem.NPD, 0, TIPO_CONSULTA_PD.CONSULTAR, itemSiafem.NumOB);

                    if (!string.IsNullOrWhiteSpace(itemSids.NumOP))
                    {
                        var op = itemSids.NumOP?.Substring(5, 6);
                        if (itemSids.TransmissaoItemStatusProdesp != null && itemSids.TransmissaoItemStatusProdesp.Equals("S") && !opsConfirmadas.Contains(op))
                        {
                            opsConfirmadas.Add(op);
                        }
                    }

                    //if (itemSids.TransmissaoItemStatusSiafem != "S")
                    //{
                    itemSids.TransmissaoItemStatusSiafem = "N";
                    itemSids.TransmissaoItemMensagemSiafem = null;
                    //}

                    //if (itemSids.TransmissaoItemStatusProdesp != "S")
                    //{
                    itemSids.TransmissaoItemStatusProdesp = "N";
                    itemSids.TransmissaoItemMensagemProdesp = null;
                    //}

                    itensRetorno.Add(itemSids);
                }

                //foreach (var item in itensRetorno)
                //{
                //    if (!string.IsNullOrWhiteSpace(item.NumOP))
                //    {
                //        var op = item.NumOP?.Substring(5, 6);
                //        if (opsConfirmadas.Contains(op))
                //        {
                //            item.TransmissaoItemStatusProdesp = "S";
                //        }
                //    }
                //}

                return itensRetorno;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ExecutarPD(PDExecucao model, string UGMudapah, List<string> checados, Usuario user, int recursoId)
        {
            bool executar = false;

            if (model.TransmitirSiafem == null)
                model.TransmitirSiafem = false;

            var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, model.TransmitirSiafem.HasValue);

            USUARIO = dadosUsuarioSiafem.usuario;
            SENHA = dadosUsuarioSiafem.senha;

            foreach (var item in model.Items)
            {
                executar = checados.Contains(item.NumPD);

                if (item.cd_transmissao_status_siafem != "S" && executar)
                {
                    ExecutarItemPD(user, item, UGMudapah);
                }
            }
        }

        public void ExecutarItemPD(Usuario user, PDExecucaoItem item, string UGMudapah)
        {
            try
            {
                var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

                USUARIO = dadosUsuarioSiafem.usuario;
                SENHA = dadosUsuarioSiafem.senha;

                var pd = item.NumPD.ToUpper().Split(new string[] { "PD" }, StringSplitOptions.None);

                var retornoSiafem = _siafem.ExecutarPD(USUARIO, Decrypt(SENHA), UGMudapah, item.UGPagadora, item.GestaoPagadora, item.UGLiquidante, item.GestaoLiquidante, pd[0], item.NouP, pd[1]);

                item.NumOBItem = retornoSiafem.NumOB;

                item.fl_transmissao_transmitido_siafem = true;
                item.cd_transmissao_status_siafem = "S";
                item.dt_transmissao_transmitido_siafem = DateTime.Now;
                item.ds_transmissao_mensagem_siafem = null;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("(3706) ESTA PROGRAMACAO DE DESEMBOLSO JA\" FOI PAGA"))
                {

                    item.fl_transmissao_transmitido_siafem = true;
                    item.cd_transmissao_status_siafem = "E";
                    //item.dt_transmissao_transmitido_siafem = DateTime.Now;
                    item.ds_transmissao_mensagem_siafem = ex.Message;
                }
                else
                {
                    item.cd_transmissao_status_siafem = "E";
                    item.fl_transmissao_transmitido_siafem = false;
                    item.ds_transmissao_mensagem_siafem = ex.Message;
                }
            }
            finally
            {
                item.NouP = item.Prioritario ? "P" : "N";
                item.AgrupamentoItemPD = 0;
                _repositoryItem.Save(item);
            }
        }



        //public void ExcluirItensNaoAgrupados(ref PDExecucao entity, int recursoId, short action)
        //{
        //    try
        //    {
        //        foreach (var item in entity.Items)
        //        {
        //            //item.id_execucao_pd = id;
        //            //item.AgrupamentoItemPD = NumeroAgrupamentoPD != 0 ? NumeroAgrupamentoPD : item.AgrupamentoItemPD;
        //            //var iditem = SalvarItem(item);
        //            //item.Codigo = iditem;
        //            if(item.AgrupamentoItemPD == 0) { 
        //                DeletarItem(item.Id, )
        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;               
        //    }
        //}

        public ConsultaOB ConsultarOB(Usuario user, string unidadeGestora = "", string Gestao = "", string NumeroOB = "", int agrupamento = 0)
        {
            try
            {
                var partes = NumeroOB.ToUpper().Split(new string[] { "OB" }, StringSplitOptions.None);

                var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

                USUARIO = dadosUsuarioSiafem.usuario;
                SENHA = dadosUsuarioSiafem.senha;

                var data = _siafem.ConsultaOB(USUARIO, Decrypt(SENHA), unidadeGestora, Gestao, partes[1]);
                return data;
            }
            catch
            {
                throw;
            }
        }
        public CancelarOB CancelarOB(string unidadeGestora, string Gestao, string OB, string Causa1, string Causa2, Usuario user)
        {
            var partes = OB.Split(new string[] { "OB" }, StringSplitOptions.None);

            var dadosUsuarioSiafem = _siafem.ConsultarUsuarioHomologacaoProducao(user.CPF, user.Senha, true);

            USUARIO = dadosUsuarioSiafem.usuario;
            SENHA = dadosUsuarioSiafem.senha;

            var data = _siafem.CancelarOB(USUARIO, Decrypt(SENHA), unidadeGestora, Gestao, partes[1], Causa1, Causa2);
            return data;
        }
        #endregion SiafemWS

        #region Enums
        public enum TIPO_CONSULTA_PD
        {
            CONSULTAR = 1,
            ADICIONAR = 2
        }
        #endregion Enums

    }
}