using System;
using System.Linq;
using Sids.Prodesp.Model.Enum;
using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.PagamentoContaDer;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Infrastructure.Services.Seguranca;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoDer;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaDer;
using Sids.Prodesp.Model.Interface.Service.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.AutorizacaoDeOB;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using System.Text.RegularExpressions;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure.Helpers;

namespace Sids.Prodesp.Core.Services.PagamentoDer
{
    public class ConfirmacaoPagamentoService : Base.BaseService
    {
        #region Proprierties
        private static UsuarioService _uService;
        private readonly ChaveCicsmoService _chave;
        private readonly ProdespPagamentoContaDerWs _prodespContaDer;
        private readonly ProdespPagamentoContaUnicaService _prodespContaUnica;
        private readonly ICrudConfirmacaoPagamento _confirmacaoPgtoRepository;
        private readonly ConfirmacaoPagamentoItemDal _confirmacaoPgtoItemRepository;

        private readonly ICrudProgramacaoDesembolsoExecucaoItem _repositoryExecucaoPdItem;
        private readonly ICrudAutorizacaoDeOBItem _repositoryAutorizacaoObItem;
        #endregion

        #region ExecucaoPD
        public ConfirmacaoPagamentoService(ILogError log, IChaveCicsmo chave, ICrudConfirmacaoPagamento confirmacaoPgtoRepository, ConfirmacaoPagamentoItemDal confirmacaoPgtoItemRepository, IProdespPagamentoContaUnica prodespContaUnica, ProdespPagamentoContaDerWs prodespContaDer, ICrudProgramacaoDesembolsoExecucaoItem repositoryExecucaoPdItem, ICrudAutorizacaoDeOBItem repositoryAutorizacaoObItem)
            : base(log)
        {
            this._confirmacaoPgtoRepository = confirmacaoPgtoRepository;
            this._confirmacaoPgtoItemRepository = confirmacaoPgtoItemRepository;
            this._prodespContaUnica = new ProdespPagamentoContaUnicaService(new LogErrorDal(), new ProdespPagamentoContaUnicaWs());
            _chave = new ChaveCicsmoService(log, chave);
            this._prodespContaDer = prodespContaDer;
            _uService = _uService ?? new UsuarioService(log, new UsuarioDal(), new PerfilUsuarioDal(), new PerfilDal(), new SiafemSegurancaWs(), new RegionalDal());

            this._repositoryExecucaoPdItem = repositoryExecucaoPdItem;
            this._repositoryAutorizacaoObItem = repositoryAutorizacaoObItem;
        }

        public AcaoEfetuada Salvar(ConfirmacaoPagamento entity, int? recursoId)
        {
            try
            {
                if (entity.Id == 0)
                {
                    var id = _confirmacaoPgtoRepository.Add(entity);
                    entity.Id = id;
                }
                else
                {
                    ExcluirItemsNaoListados(entity);
                    _confirmacaoPgtoRepository.Edit(entity);
                }

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, 1, recursoId);
            }
        }

        public AcaoEfetuada Salvar(PDExecucao entity, int? recursoId)
        {
            try
            {
                if (entity.Id == 0)
                {
                    var id = _confirmacaoPgtoRepository.Add(entity);
                    entity.Id = id;
                }

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, 1, recursoId);
            }
        }

        public ConfirmacaoPagamento Selecionar(int id)
        {
            var filtro = new ConfirmacaoPagamento();
            filtro.Id = id;

            return Selecionar(filtro);
        }
        public ConfirmacaoPagamento Selecionar(ConfirmacaoPagamento filtro)
        {
            var entity = this._confirmacaoPgtoRepository.Fetch(filtro).FirstOrDefault();

            if (entity == null)
                return null;

            var items = this._confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = filtro.Id });
            entity.Items = items;

            return entity;
        }

        public ConfirmacaoPagamento Selecionar(int? idExecucaoPd, int? idAutorizacaoOB)
        {
            var entity = _confirmacaoPgtoRepository.Get(idExecucaoPd, idAutorizacaoOB);

            if (entity == null)
                return null;

            var items = _confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = entity.Id });
            entity.Items = items;

            return entity;
        }

        public List<ConfirmacaoPagamento> BuscarGrid(ConfirmacaoPagamento filtro)
        {
            var entities = this._confirmacaoPgtoRepository.FetchForGrid(filtro);

            List<ConfirmacaoPagamento> ListaPesquisa = new List<ConfirmacaoPagamento>();

            foreach (var item in entities)
            {
                ConfirmacaoPagamento itemPesquisa = new ConfirmacaoPagamento();
                itemPesquisa.acao = item.acao;
                itemPesquisa.Agencia = item.Agencia;
                itemPesquisa.AgenciaFavorecido = item.AgenciaFavorecido;
                itemPesquisa.AnoReferencia = item.AnoReferencia;
                itemPesquisa.Banco = item.Banco;
                itemPesquisa.BancoFavorecido = item.BancoFavorecido;
                itemPesquisa.Chave = item.Chave;
                itemPesquisa.CodigoAgrupamentoConfirmacaoPagamento = item.CodigoAgrupamentoConfirmacaoPagamento;
                itemPesquisa.CodigoObra = item.CodigoObra;
                itemPesquisa.Conta = item.Conta;
                itemPesquisa.ContaFavorecido = item.ContaFavorecido;
                itemPesquisa.CPFCNPJCredorOriginal = item.CPFCNPJCredorOriginal;
                itemPesquisa.CPF_CNPJ = item.CPF_CNPJ;
                itemPesquisa.CPF_CNPJ_Credor = item.CPF_CNPJ_Credor;
                itemPesquisa.CredorOrganizacao = item.CredorOrganizacao;
                itemPesquisa.CredorOrganizacaoCredorDocto = item.CredorOrganizacaoCredorDocto;
                itemPesquisa.CredorOrganizacaoCredorOriginal = item.CredorOrganizacaoCredorOriginal;
                itemPesquisa.CredorOriginal = item.CredorOriginal;
                itemPesquisa.DataCadastro = item.DataCadastro;
                itemPesquisa.DataCadastroAte = item.DataCadastroAte;
                itemPesquisa.DataCadastroDe = item.DataCadastroDe;
                itemPesquisa.DataConfirmacao = item.DataConfirmacao;
                itemPesquisa.DataModificacao = item.DataModificacao;
                itemPesquisa.DataPreparacao = item.DataPreparacao;
                itemPesquisa.DataTransmitidoProdesp = item.DataTransmitidoProdesp;
                itemPesquisa.DataVencimento = item.DataVencimento;
                itemPesquisa.Fonte = item.Fonte;
                itemPesquisa.FonteSIAFEM = item.FonteSIAFEM;
                itemPesquisa.Id = item.Id;
                itemPesquisa.IdAutorizacaoOB = item.IdAutorizacaoOB;
                itemPesquisa.IdExecucaoPD = item.IdExecucaoPD;
                itemPesquisa.IdTipoDocumento = item.IdTipoDocumento;
                itemPesquisa.id_confirmacao_pagamento = item.id_confirmacao_pagamento;
                itemPesquisa.id_confirmacao_pagamento_item = item.id_confirmacao_pagamento_item;
                itemPesquisa.Impressora = item.Impressora;
                itemPesquisa.Items = item.Items;
                itemPesquisa.MensagemErroRetornadaTransmissaoConfirmacaoPagamento = item.MensagemErroRetornadaTransmissaoConfirmacaoPagamento;
                itemPesquisa.MensagemServicoProdesp = item.MensagemServicoProdesp;
                itemPesquisa.NaturezaDespesa = item.NaturezaDespesa;
                itemPesquisa.NLDocumento = item.NLDocumento;
                itemPesquisa.NomeReduzidoCredor = item.NomeReduzidoCredor;
                itemPesquisa.NotaFiscal = item.NotaFiscal;
                itemPesquisa.NumeroBaixaRepasse = item.NumeroBaixaRepasse;
                itemPesquisa.NumeroConta = item.NumeroConta;
                itemPesquisa.NumeroContrato = item.NumeroContrato;
                itemPesquisa.NumeroDocumento = item.NumeroDocumento;
                itemPesquisa.NumeroEmpenho = item.NumeroEmpenho;
                itemPesquisa.numeroEmpenhoSiafem = item.numeroEmpenhoSiafem;
                itemPesquisa.NumeroNLBaixaRepasse = item.NumeroNLBaixaRepasse;
                itemPesquisa.NumeroOP = item.NumeroOP;
                itemPesquisa.NumeroProcesso = item.NumeroProcesso;
                itemPesquisa.Orgao = item.Orgao;
                itemPesquisa.OrigemConfirmacao = item.OrigemConfirmacao;
                itemPesquisa.Referencia = item.Referencia;
                itemPesquisa.RegionalId = item.RegionalId;
                itemPesquisa.Senha = item.Senha;
                itemPesquisa.StatusProdesp = item.StatusProdesp;
                itemPesquisa.StatusTransmissaoConfirmacao = item.StatusTransmissaoConfirmacao;
                itemPesquisa.TipoConfirmacao = item.TipoConfirmacao;
                itemPesquisa.TipoDespesa = item.TipoDespesa;
                itemPesquisa.TipoDocumento = item.TipoDocumento;
                itemPesquisa.TipoPagamento = item.TipoPagamento;
                itemPesquisa.TipoSistema = item.TipoSistema;
                itemPesquisa.Totalizador = item.Totalizador;
                itemPesquisa.TransmissaoConfirmacao = item.TransmissaoConfirmacao;
                itemPesquisa.TransmitidoProdesp = item.TransmitidoProdesp;
                itemPesquisa.ValorDocumento = item.ValorDocumento;
                itemPesquisa.DespesaTipo = item.DespesaTipo;
                itemPesquisa.ValorConfirmacao = item.ValorConfirmacao;
                itemPesquisa.ValorDesdobradoCredor = item.ValorDesdobradoCredor;
                itemPesquisa.ValorTotalConfirmado = item.ValorTotalConfirmado;
                itemPesquisa.ValorTotalConfirmarIR = item.ValorTotalConfirmarIR;
                itemPesquisa.ValorTotalConfirmarISSQN = item.ValorTotalConfirmarISSQN;
                itemPesquisa.Origem = item.Origem;
                itemPesquisa.ValorDocumentoDecimal = item.ValorDocumentoDecimal;

                ListaPesquisa.Add(itemPesquisa);
            }




            //if (entities == null)
            //    return null;

            //foreach (var entity in entities)
            //{
            //  var items = this._confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = entity.id_confirmacao_pagamento });
            //    entity.Items = items;
            //}

            return ListaPesquisa;
        }

        private void ExcluirItemsNaoListados(ConfirmacaoPagamento entity)
        {
            var items = _confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdConfirmacaoPagamento = entity.Id });
            foreach (var item in items)
            {
                if (!entity.Items.Any(x => x.Id == item.Id))
                {
                    _confirmacaoPgtoItemRepository.Remove(item.Id);
                }
            }
        }

        private void SalvarItem(ConfirmacaoPagamentoItem item)
        {
            if (item.Id == 0)
            {
                _confirmacaoPgtoItemRepository.Add(item);
            }
            else
            {
                _confirmacaoPgtoItemRepository.Edit(item);
            }
        }

        private void SalvarItem(PDExecucao entity, PDExecucaoItem item)
        {
            var confirmacaoPagamento = new ConfirmacaoPagamento();
            confirmacaoPagamento.IdTipoDocumento = item.DocumentoTipoId;
            confirmacaoPagamento.AnoReferencia = Convert.ToInt32(item.AnoAserpaga);
            confirmacaoPagamento.DataConfirmacao = item.Dt_confirmacao;
            confirmacaoPagamento.TipoPagamento = item.TipoPagamento;
            confirmacaoPagamento.IdExecucaoPD = item.id_execucao_pd;
            confirmacaoPagamento.ValorTotalConfirmado = Convert.ToDecimal(item.Valor);

            if (entity.IdConfirmacaoPagamento == 0)
            {
                entity.IdConfirmacaoPagamento = _confirmacaoPgtoRepository.Add(confirmacaoPagamento);
            }
            else
            {
                confirmacaoPagamento.Id = entity.IdConfirmacaoPagamento;
                confirmacaoPagamento.StatusProdesp = item.cd_transmissao_status_prodesp;
                confirmacaoPagamento.MensagemServicoProdesp = item.ds_transmissao_mensagem_prodesp;
                _confirmacaoPgtoRepository.Edit(confirmacaoPagamento);
            }

            item.id_confirmacao_pagamento = entity.IdConfirmacaoPagamento;

            //var items = this._confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdPDExecucaoItem = item.Codigo });

            if (item.IdConfirmacaoPagamentoItem != 0)
            {
                _confirmacaoPgtoItemRepository.Edit(item);
            }
            else
            {
                _confirmacaoPgtoItemRepository.Add(item); // ATUALIZA REGISTRO EXISTENTE OU ADICIONA INEXISTENTE
            }
        }

        private void SalvarItem(OBAutorizacao entity, OBAutorizacaoItem item)
        {
            var confirmacaoPagamento = new ConfirmacaoPagamento();
            confirmacaoPagamento.IdTipoDocumento = item.DocumentoTipoId;
            confirmacaoPagamento.AnoReferencia = Convert.ToInt32(item.AnoAserpaga);
            confirmacaoPagamento.DataConfirmacao = item.DataConfirmacaoItem;
            confirmacaoPagamento.TipoPagamento = item.TipoPagamento;
            confirmacaoPagamento.IdAutorizacaoOB = item.IdAutorizacaoOB;
            confirmacaoPagamento.IdExecucaoPD = item.IdExecucaoPD;
            confirmacaoPagamento.IdAutorizacaoOB = item.IdAutorizacaoOB;
            confirmacaoPagamento.ValorTotalConfirmado = Convert.ToDecimal(item.ValorItem);

            if (entity.IdConfirmacaoPagamento == 0)
            {
                entity.IdConfirmacaoPagamento = _confirmacaoPgtoRepository.Add(confirmacaoPagamento);
            }
            else
            {
                confirmacaoPagamento.Id = entity.IdConfirmacaoPagamento;
                confirmacaoPagamento.StatusProdesp = item.TransmissaoItemStatusProdesp;
                _confirmacaoPgtoRepository.Edit(confirmacaoPagamento);
            }

            item.id_confirmacao_pagamento = entity.IdConfirmacaoPagamento;

            //var items = this._confirmacaoPgtoItemRepository.Fetch(new ConfirmacaoPagamentoItem() { IdAutorizacaoOBItem = item.IdAutorizacaoOBItem });

            //if (items.Any())
            //{
            //    _confirmacaoPgtoItemRepository.Edit(item);
            //}
            //else
            //{
            _confirmacaoPgtoItemRepository.Add(item);
            //}
        }

        public void RelacionarAutorizacaoComPagamentoDesdobrado(OBAutorizacao entity, IEnumerable<OBAutorizacaoItem> entityItem, List<string> checados)
        {
            try
            {
                int contador = 0;
                string itemChecado = string.Empty;

                foreach (var item in entityItem)
                {
                    if (contador < checados.Count)
                    {
                        if (item.NumOB == checados[contador])
                        {
                            itemChecado = checados[contador];
                            contador++;
                        }
                    }

                    if (itemChecado != string.Empty)
                    {
                        _confirmacaoPgtoItemRepository.RelacionarExecucaoComPagamentoDesdobrado(item);
                        itemChecado = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RelacionarExecucaoComPagamentoDesdobrado(PDExecucao entity, IEnumerable<PDExecucaoItem> entityItem, List<string> checados, bool primeiraVez)
        {
            try
            {
                int contador = 0;
                string itemChecado = string.Empty;

                foreach (var item in entityItem)
                {
                    if (contador < checados.Count)
                    {
                        if (item.NumPD == checados[contador])
                        {
                            itemChecado = checados[contador];
                            contador++;
                        }
                    }

                    if (itemChecado != string.Empty)
                    {
                        _confirmacaoPgtoItemRepository.RelacionarExecucaoComPagamentoDesdobrado(item);
                        itemChecado = string.Empty;
                    }
                }

                RemoverItensNaoPertencentes(entity, primeiraVez);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RemoverItensNaoPertencentes(PDExecucao entity, bool primeiraVez)
        {
            foreach (var item in entity.Items)
            {
                var agrupado = item.AgrupamentoItemPD == 0;

                if (!agrupado && primeiraVez)
                {
                    _repositoryExecucaoPdItem.Remove(item.Codigo.GetValueOrDefault());
                }
            }
        }

        public void RemoverItensNaoPertencentesOb(OBAutorizacao entity, bool primeiraVez)
        {
            foreach (var item in entity.Items)
            {
                var agrupado = item.AgrupamentoItemOB == 0;

                if (!agrupado && primeiraVez)
                {
                    _repositoryAutorizacaoObItem.Remove(item.Codigo.GetValueOrDefault());
                }
            }
        }
        public void RemoverItensNaoPertencentesOb(OBAutorizacao entity)
        {
            foreach (var item in entity.Items)
            {
                var agrupado = item.AgrupamentoItemOB == 0;

                if (!agrupado)
                {
                    _repositoryAutorizacaoObItem.Remove(item.Codigo.GetValueOrDefault());
                }
            }
        }

        public void Transmitir(int entityId, Usuario user, int recursoId)
        {
            ConfirmacaoPagamento entity = Selecionar(entityId);

            try
            {
                Transmissao(user, entity, recursoId);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
            finally
            {
                Salvar(entity, recursoId);
            }
        }

        private void Transmissao(Usuario user, ConfirmacaoPagamento entity, int recursoId)
        {
            try
            {
                if (entity.StatusProdesp == "N" || entity.StatusProdesp == "E" || string.IsNullOrWhiteSpace(entity.StatusProdesp))
                {
                    TransmitirProdesp(entity, recursoId);
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
        }

        private void TransmitirProdesp(ConfirmacaoPagamento entity, int recursoId)
        {
            string verificaNumeroGerador = string.Empty;

            try
            {
                var itens = entity.Items.OrderBy(p => p.NumeroDocumentoGerador);

                //Transmitir confirmação Prodesp
                Model.Entity.Seguranca.Regional orgao = new Model.Entity.Seguranca.Regional();

                foreach (var item in itens)
                {
                    if (string.IsNullOrWhiteSpace(entity.FonteLista)) // remover, não necessita quando usamos o selecionar
                    {
                        if (item.NumeroDocumentoGerador.Substring(0, 17) != verificaNumeroGerador)
                        {

                            ConfirmacaoPagamento confirmarPagamentoUnico = new ConfirmacaoPagamento();
                            confirmarPagamentoUnico.Senha = entity.Senha;
                            confirmarPagamentoUnico.Chave = entity.Chave;
                            confirmarPagamentoUnico.DataConfirmacao = entity.DataConfirmacao;
                            confirmarPagamentoUnico.AnoReferencia = entity.AnoReferencia;
                            confirmarPagamentoUnico.IdTipoDocumento = Convert.ToInt16(item.NumeroDocumentoGerador.Substring(0, 2));
                            confirmarPagamentoUnico.NumeroOP = item.NumeroOp;
                            confirmarPagamentoUnico.TipoPagamento = entity.TipoPagamento;
                            orgao.Id = Convert.ToInt16(item.NumeroOp.Substring(2, 2));

                            var result = DataHelperProdespPagamentoContaUnica.Procedure_ConfirmacaoPagtoOPApoio(entity.Chave, entity.Senha, confirmarPagamentoUnico, orgao);

                            verificaNumeroGerador = item.NumeroDocumentoGerador.Substring(0, 17);


                            if (result[0].outErro == "" || result[0].outErro == null)
                            {
                                confirmarPagamentoUnico.DataTransmitidoProdesp = DateTime.Now;
                                confirmarPagamentoUnico.StatusProdesp = "S";
                                confirmarPagamentoUnico.TransmitidoProdesp = true;
                                confirmarPagamentoUnico.NumeroDocumento = entity.NumeroDocumento;
                                confirmarPagamentoUnico.id_confirmacao_pagamento = entity.Id;
                                confirmarPagamentoUnico.NumeroDocumentoItem = item.NumeroDocumentoGerador;
                            }
                            else
                            {
                                confirmarPagamentoUnico.DataTransmitidoProdesp = DateTime.Now;
                                confirmarPagamentoUnico.StatusProdesp = "E";
                                confirmarPagamentoUnico.TransmitidoProdesp = false;
                                confirmarPagamentoUnico.NumeroDocumento = item.NumeroNlDocumento;
                                confirmarPagamentoUnico.id_confirmacao_pagamento = entity.Id;
                                confirmarPagamentoUnico.MensagemServicoProdesp = result[0].outErro;

                                confirmarPagamentoUnico.NumeroDocumentoItem = item.NumeroDocumentoGerador;
                            }

                            var update = this.AtualizarPagamento(confirmarPagamentoUnico);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
        }

        public void TransmitirProdesp(PDExecucao entity, List<string> pdsMarcados, DateTime? dataConfirmacao, int? tipoPagamento, bool primeiraVez, int recursoId)
        {
            try
            {
                dataConfirmacao = dataConfirmacao ?? entity.DataConfirmacao;
                tipoPagamento = tipoPagamento ?? entity.TipoPagamento;
                bool jaConfirmado = false;
                List<string> opsJaConfirmadas = new List<string>();

                foreach (var item in entity.Items)
                {
                    if (((item.NouP == "N" && !item.Prioritario) || item.NumeroCnpjcpfPagto.Length <= 6) && (item.NumOP != string.Empty && item.NumOP != null))
                    {
                        var op = item.NumOP.Substring(5, 6);

                        jaConfirmado = opsJaConfirmadas.Contains(op);
                        var foiExecutado = pdsMarcados.Contains(item.NumPD);

                        if (item.Executar || (item.IsDesdobramento && jaConfirmado))
                        {
                            TransmitirConfirmacaoPagamentoItem(recursoId, entity, item, dataConfirmacao, tipoPagamento, (item.Executar && !jaConfirmado), foiExecutado);

                            if (!opsJaConfirmadas.Contains(op))
                            {
                                opsJaConfirmadas.Add(op);
                            }
                        }
                    }
                }

                RemoverItensNaoPertencentes(entity, primeiraVez);
            }
            catch (Exception ex)
            {
                entity.StatusProdesp = "E";
                entity.MensagemServicoProdesp = ex.Message;
            }
        }
        public void TransmitirProdesp(OBAutorizacao entity, List<string> marcados, DateTime? dataConfirmacao, int? tipoPagamento, bool primeiraVez, int recursoId)
        {
            try
            {
                bool jaConfirmado = false;
                List<string> opsJaConfirmadas = new List<string>();

                foreach (var item in entity.Items)
                {
                    if (!string.IsNullOrWhiteSpace(item.NumOP))
                    {
                        var op = item.NumOP.Substring(5, 6);

                        jaConfirmado = opsJaConfirmadas.Contains(op);
                        var foiExecutado = marcados.Contains(item.NumOB);

                        if (item.Executar || (item.IsDesdobramento && jaConfirmado))
                        {
                            TransmitirConfirmacaoPagamentoItem(recursoId, entity, item, dataConfirmacao, tipoPagamento, (item.Executar && !jaConfirmado), foiExecutado);

                            if (!opsJaConfirmadas.Contains(op))
                            {
                                opsJaConfirmadas.Add(op);
                            }
                        }
                    }
                }

                RemoverItensNaoPertencentesOb(entity, primeiraVez);
            }
            catch (Exception ex)
            {
                entity.StatusProdesp = "E";
                entity.MensagemServicoProdesp = ex.Message;
            }
        }

        public void TransmitirConfirmacaoPagamentoItem(int recursoId, PDExecucao pd, PDExecucaoItem item, DateTime? dataConfirmacao, int? tipoPagamento, bool transmitir, bool foiExecutado)
        {
            var cicsmo = new ChaveCicsmo();

            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                item.AnoAserpaga = dataConfirmacao.ToString().Substring(6, 4);
                item.Dt_confirmacao = dataConfirmacao;
                item.DocumentoTipoId = item.IdTipoDocumento;
                item.NumeroDocumento = item?.NumeroDocumento;
                item.TipoPagamento = tipoPagamento;
                item.DataCadastro = DateTime.Now;

                if (transmitir)
                {
                    _prodespContaUnica.Inserir_ConfirmacaoPagamento(cicsmo.Chave, cicsmo.Senha, ref item);
                }

                item.Dt_confirmacao = dataConfirmacao;
                item.fl_transmissao_transmitido_prodesp = true;
                item.cd_transmissao_status_prodesp = "S";
                item.dt_transmissao_transmitido_prodesp = DateTime.Now;
                item.ds_transmissao_mensagem_prodesp = null;
            }
            catch (Exception ex)
            {
                item.Dt_confirmacao = null;
                item.cd_transmissao_status_prodesp = "E";
                item.fl_transmissao_transmitido_prodesp = false;
                item.dt_transmissao_transmitido_prodesp = DateTime.Now;
                item.ds_transmissao_mensagem_prodesp = ex.Message;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

                var codigoEpdi = item.Codigo;
                if (!foiExecutado)
                {
                    item.Codigo = null;
                }
                SalvarItem(pd, item);
                item.Codigo = codigoEpdi;
            }
        }

        public void TransmitirConfirmacaoPagamentoItem(int recursoId, OBAutorizacao ob, OBAutorizacaoItem item, DateTime? dataConfirmacao, int? tipoPagamento, bool transmitir, bool foiExecutado)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                item.AnoAserpaga = dataConfirmacao.ToString().Substring(6, 4);
                item.DataConfirmacaoItem = dataConfirmacao;
                item.DocumentoTipoId = item.IdTipoDocumento;
                item.NumeroDocumento = item?.NumeroDocumento;
                item.TipoPagamento = tipoPagamento;
                item.DataCadastro = DateTime.Now;

                if (transmitir)
                {
                    _prodespContaUnica.Inserir_ConfirmacaoPagamento(cicsmo.Chave, cicsmo.Senha, ref item);
                }

                item.DataConfirmacaoItem = dataConfirmacao;
                item.TransmissaoItemTransmitidoProdesp = true;
                item.TransmissaoItemStatusProdesp = "S";
                item.TransmissaoItemDataProdesp = DateTime.Now;
                item.TransmissaoItemMensagemProdesp = null;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FCFG404 - PAGAMENTO CONFIRMADO EM"))
                {
                    var dataTexto = new Regex(@".+\s(\d{2}\/\d{2}\/\d{2,4})").Match(ex.Message);
                    if (dataTexto.Success)
                    {
                        var grupo = dataTexto.Groups[1];
                        var data = DateTime.Parse(grupo.Value);

                        item.DataConfirmacaoItem = data;
                        item.TransmissaoItemTransmitidoProdesp = true;
                        item.TransmissaoItemStatusProdesp = "S";
                        item.TransmissaoItemDataProdesp = DateTime.Now;
                        item.TransmissaoItemMensagemProdesp = null;
                    }
                }
                else
                {
                    item.DataConfirmacaoItem = null;
                    item.TransmissaoItemTransmitidoProdesp = false;
                    item.TransmissaoItemStatusProdesp = "E";
                    item.TransmissaoItemDataProdesp = DateTime.Now;
                    item.TransmissaoItemMensagemProdesp = ex.Message;
                }
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);

                var codigoEpdi = item.Codigo;
                if (!foiExecutado)
                {
                    item.Codigo = null;
                }
                SalvarItem(ob, item);
                item.Codigo = codigoEpdi;
            }
        }
        #endregion


        #region Confirmacao Pagamento
        public int SalvarPagamento(ConfirmacaoPagamento entrada, string tipo)
        {
            try
            {
                int salvarPagamento = new ConfirmacaoPagamentoDal().SavePayment(entrada, tipo);

                return salvarPagamento;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        public int AtualizarPagamento(ConfirmacaoPagamento entrada)
        {
            int salvarPagamento = new ConfirmacaoPagamentoDal().UpdatePayment(entrada);

            return salvarPagamento;
        }

        public int SalvarFonte(ConfirmacaoPagamento entrada)
        {
            int salvarPagamento = new ConfirmacaoPagamentoDal().SaveFonte(entrada);

            return salvarPagamento;
        }

        public int SalvarValorConfirmado(ConfirmacaoPagamento entrada)
        {

            int alterarValorConfirmado = new ConfirmacaoPagamentoDal().AlteraValorTotal(entrada);

            return alterarValorConfirmado;
        }

        public int ApagarPagamento(int? id, int? item)
        {
            return new ConfirmacaoPagamentoDal().DeletePayment(id, item);
        }

        public List<ConfirmacaoPagamento> ConsultarPagamento(ConfirmacaoPagamento entrada)
        {
            var cicsmo = new ChaveCicsmo();
            var usuario = _uService.Buscar(new Usuario { Codigo = GetUserIdLogado() }).FirstOrDefault();
            try
            {
                cicsmo = _chave.ObterChave();
                var result = _prodespContaDer.ConsultaPagtosConfirmarSIDS(cicsmo.Chave, cicsmo.Senha, usuario?.Impressora132, entrada);
                return result;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
            }
        }

        public List<ConfirmacaoPagamento> ConsultarPagamentoPorId(int? id, int? item)
        {
            return new ConfirmacaoPagamentoDal().FetchForId(id, item).ToList();
        }
        #endregion
    }
}