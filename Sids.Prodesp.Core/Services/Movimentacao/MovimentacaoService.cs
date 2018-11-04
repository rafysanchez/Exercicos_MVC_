using Sids.Prodesp.Core.Services.Configuracao;
using Sids.Prodesp.Core.Services.Movimentacao;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Core.Services.WebService.Movimentacao;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaDer;
using Sids.Prodesp.Core.Services.WebService.PagamentoContaUnica;
using Sids.Prodesp.Infrastructure;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Infrastructure.Services.Movimentacao;
using Sids.Prodesp.Infrastructure.Services.PagamentoContaUnica;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Entity.Movimentacao;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Exceptions;
using Sids.Prodesp.Model.Interface.Configuracao;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.Movimentacao;
using Sids.Prodesp.Model.Interface.PagamentoContaDer;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Service.Movimentacao;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.Movimentacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Sids.Prodesp.Model.ValueObject;

namespace Sids.Prodesp.Core.Services.PagamentoDer
{
    public class MovimentacaoService : Base.BaseService
    {
        private readonly ICrudMovimentacao _repository;
        private readonly ICrudMovimentacaoTipo _repositoryTipo;
        private readonly ICrudMovimentacaoDocumentoTipo _repositoryDoc;
        private readonly ICrudMovimentacaoMes _repositoryMes;
        private readonly MovimentacaoMesService _serviceMes;
        private readonly ProdespMovimentacaoService _prodespContaDer;
        private readonly ChaveCicsmoService _chave;
        private readonly SiafemMovimentacaoService _siafemMov;
        private readonly RegionalService _regional;
        private readonly ICrudPrograma _programa2;
        private readonly ICrudFonte _fonte;
        private readonly ICrudEstrutura _estutura;

        List<MovimentacaoCancelamento> listCancelamento = new List<MovimentacaoCancelamento>();
        List<MovimentacaoNotaDeCredito> listCredito = new List<MovimentacaoNotaDeCredito>();
        List<MovimentacaoDistribuicao> listDistribuicao = new List<MovimentacaoDistribuicao>();
        List<MovimentacaoReducaoSuplementacao> listReducaoSuplementacao = new List<MovimentacaoReducaoSuplementacao>();

        int countErro;

        MovimentacaoOrcamentaria entity2 = new MovimentacaoOrcamentaria();

        public MovimentacaoService(ILogError log, IChaveCicsmo chave, ICrudMovimentacao repository, ICrudMovimentacaoTipo repositoryTipo, ICrudMovimentacaoDocumentoTipo repositoryDoc, ICrudMovimentacaoMes repositoryMes, IProdespMovimentacao prodespContaDer, ICrudPrograma programa,
            ICrudFonte fonte, ICrudEstrutura estrutura, ISiafemMovimentacao siafemMov)
            : base(log)
        {
            this._repository = repository;
            this._repositoryTipo = repositoryTipo;
            this._repositoryDoc = repositoryDoc;
            this._repositoryMes = repositoryMes;
            this._serviceMes = new MovimentacaoMesService(new LogErrorDal(), repositoryMes);
            this._prodespContaDer = new ProdespMovimentacaoService(new LogErrorDal(), new ProdespMovimentacaoWs());
            this._regional = new RegionalService(log, new RegionalDal());
            this._siafemMov = new SiafemMovimentacaoService(log, siafemMov, programa, fonte, estrutura);
            this._estutura = estrutura;
            this._programa2 = programa;
            this._fonte = fonte;

            _chave = new ChaveCicsmoService(log, chave);
        }

        public int SalvarOuAlterar(MovimentacaoOrcamentaria entity, int recursoId, short action)
        {
            listCancelamento = new List<MovimentacaoCancelamento>();
            listCredito = new List<MovimentacaoNotaDeCredito>();
            listDistribuicao = new List<MovimentacaoDistribuicao>();
            listReducaoSuplementacao = new List<MovimentacaoReducaoSuplementacao>();


            try
            {
                if (entity.Id == 0)
                {
                    entity.DataCadastro = DateTime.Now;

                    entity.NrAgrupamento = _repository.GetLastGroup() + 1;

                }
                else
                {
                    entity.NrAgrupamento = entity.NrAgrupamento;
                }

                entity.Id = _repository.Save(entity);

                if (entity.Cancelamento != null && entity.Cancelamento.Any())
                {
                    entity.Cancelamento.ForEach(f => f.NrAgrupamento = entity.NrAgrupamento);

                    foreach (var cancelamento in entity.Cancelamento)
                    {
                        var isTextoPadraoCancelamento = cancelamento.Observacao?.Contains("Cancelamento para atender regionais") ?? false;
                        var isTextoPadraoDistribuicao = cancelamento.Observacao?.Contains("Distribuição conforme Nota de Crédito") ?? false;

                        if (isTextoPadraoCancelamento || isTextoPadraoDistribuicao)
                        {
                            var sb = new StringBuilder(cancelamento.Observacao.Replace("...", string.Empty));

                            if (isTextoPadraoCancelamento)
                            {
                                sb = new StringBuilder("Cancelamento para atender regionais");
                                MontarObservacao(entity, cancelamento, sb);
                            }
                            else if (isTextoPadraoDistribuicao && !String.IsNullOrWhiteSpace(entity.NrNotaCredito))
                            {
                                sb.Append(" ");
                                sb.Append(entity.NrNotaCredito);
                            }
                            cancelamento.Observacao = sb.ToString();
                        }

                        PreencherCancelamento(entity, cancelamento);
                    }

                    SalvarOuAlterarCancelamentos(entity, listCancelamento, recursoId, action);
                }

                if (entity.Distribuicao != null && entity.Distribuicao.Any())
                {
                    entity.Distribuicao.ForEach(f => f.NrAgrupamento = entity.NrAgrupamento);

                    foreach (MovimentacaoDistribuicao item in entity.Distribuicao)
                    {
                        PreencherDistribuicao(entity, item);
                    }

                    SalvarOuAlterarDistribuicao(entity, listDistribuicao, recursoId, action);
                }

                if (entity.NotasCreditos != null && entity.NotasCreditos.Any())
                {
                    entity.NotasCreditos.ForEach(f => f.NrAgrupamento = entity.NrAgrupamento);

                    foreach (MovimentacaoNotaDeCredito item in entity.NotasCreditos)
                    {
                        PreencherNotaDeCredito(entity, item);
                    }

                    SalvarOuAlterarCredito(entity, listCredito, recursoId, action);
                }

                if (entity.ReducaoSuplementacao != null && entity.ReducaoSuplementacao.Any())
                {
                    entity.ReducaoSuplementacao.ForEach(f => f.NrAgrupamento = entity.NrAgrupamento);

                    foreach (MovimentacaoReducaoSuplementacao item in entity.ReducaoSuplementacao)
                    {
                        PreencherReducaoSuplementacao(entity, item);
                    }

                    SalvarOuAlterarReducaoSuplementacao(entity, listReducaoSuplementacao, recursoId, action);
                }


                List<MovimentacaoMes> listaTemporariaMes;

                if (entity.Meses != null && entity.Meses.Any())
                {
                    listaTemporariaMes = new List<MovimentacaoMes>();
                    foreach (MovimentacaoMes item in entity.Meses)
                    {
                        var mesFormatado = GenerateClassModelMes(entity, item, new MovimentacaoMes(), entity);
                        listaTemporariaMes.Add(mesFormatado);
                    }
                    entity.Meses = listaTemporariaMes;

                    _serviceMes.Salvar(entity.Meses, recursoId, action);
                }

                if (recursoId > 0 && entity.StatusProdesp == "S")
                {
                    LogSucesso(action, recursoId, $"Movimentação Orçamentária : Codigo {entity.Id}");
                }

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, recursoId);
            }
        }


        public int BuscarUltimoAgupamento()
        {
            return _repository.GetLastGroup();
        }


        public IEnumerable<MovimentacaoOrcamentaria> Listar(MovimentacaoOrcamentaria entity)
        {
            return _repository.Fetch(entity);
        }

        public MovimentacaoOrcamentaria Selecionar(int id)
        {
            var listaMeses = new List<MovimentacaoMes>();
            var entity = _repository.Get(id);

            entity.Cancelamento = Buscar(new MovimentacaoCancelamento() { IdMovimentacao = entity.Id, NrAgrupamento = entity.NrAgrupamento }).ToList();
            entity.NotasCreditos = Buscar(new MovimentacaoNotaDeCredito() { IdMovimentacao = entity.Id, NrAgrupamento = entity.NrAgrupamento }).ToList();
            entity.Distribuicao = Buscar(new MovimentacaoDistribuicao() { IdMovimentacao = entity.Id, NrAgrupamento = entity.NrAgrupamento });
            entity.ReducaoSuplementacao = Buscar(new MovimentacaoReducaoSuplementacao() { IdMovimentacao = entity.Id, NrAgrupamento = entity.NrAgrupamento });

            foreach (MovimentacaoCancelamento item in entity.Cancelamento)
            {
                var meses = _serviceMes.BuscarCancelamento(new MovimentacaoMes() { IdMovimentacao = item.IdMovimentacao, IdCancelamento = item.Id, NrAgrupamento = item.NrAgrupamento, NrSequencia = item.NrSequencia }).ToList();

                meses.ForEach(x => x.tab = "C");

                listaMeses.AddRange(meses);
            }

            foreach (MovimentacaoDistribuicao item in entity.Distribuicao)
            {
                var meses = _serviceMes.BuscarDistribuicao(new MovimentacaoMes() { IdMovimentacao = item.IdMovimentacao, IdDistribuicao = item.Id, NrAgrupamento = item.NrAgrupamento, NrSequencia = item.NrSequencia }).ToList();

                meses.ForEach(x => x.tab = "D");

                listaMeses.AddRange(meses);
            }

            foreach (MovimentacaoReducaoSuplementacao item in entity.ReducaoSuplementacao)
            {
                if (item.RedSup == "R")
                {
                    var meses = _serviceMes.BuscarReducaoSuplementacao(new MovimentacaoMes() { IdMovimentacao = item.IdMovimentacao, IdReducaoSuplementacao = item.Id, NrAgrupamento = item.NrAgrupamento, NrSequencia = item.NrSequencia });

                    ((List<MovimentacaoMes>)meses).ForEach(x => x.tab = "R");

                    listaMeses.AddRange(meses);
                }
                if (item.RedSup == "S")
                {
                    var meses = _serviceMes.BuscarReducaoSuplementacao(new MovimentacaoMes() { IdMovimentacao = item.IdMovimentacao, IdReducaoSuplementacao = item.Id, NrAgrupamento = item.NrAgrupamento, NrSequencia = item.NrSequencia });

                    ((List<MovimentacaoMes>)meses).ForEach(x => x.tab = "S");

                    listaMeses.AddRange(meses);
                }
            }

            entity.Meses = listaMeses;

            foreach (var item in entity.Cancelamento)
            {
                item.IdTipoDocumento = (int)EnumTipoDocumentoMovimentacao.CancelamentoReducao;
            }

            foreach (var item in entity.Distribuicao)
            {
                item.IdTipoDocumento = (int)EnumTipoDocumentoMovimentacao.DistribuicaoSuplementacao;
            }

            foreach (var item in entity.ReducaoSuplementacao)
            {
                item.IdTipoMovimentacao = entity.IdTipoMovimentacao;
                item.AnoExercicio = entity.AnoExercicio;
                item.IdTipoDocumento = item.RedSup.Equals("R") ? (int)EnumTipoDocumentoMovimentacao.CancelamentoReducao : (int)EnumTipoDocumentoMovimentacao.DistribuicaoSuplementacao;
            }

            entity.TransmitidoProdesp = entity.ReducaoSuplementacao.All(x => x.StatusProdesp.Equals("S"));

            var cancelamentosTransmitidos = entity.Cancelamento.All(x => x.StatusSiafem.Equals("S"));
            var distribuicoesTransmitidas = entity.Distribuicao.All(x => x.StatusSiafem.Equals("S"));
            var notasDeCreditoTransmitidas = entity.NotasCreditos.All(x => x.StatusSiafem.Equals("S"));

            entity.TransmitidoSiafem = cancelamentosTransmitidos && distribuicoesTransmitidas && notasDeCreditoTransmitidas;

            PreencherAssinaturaDeFilhoParaPai(entity);

            return entity;
        }

        private static void PreencherAssinaturaDeFilhoParaPai(MovimentacaoOrcamentaria entity)
        {
            var rs = entity.ReducaoSuplementacao.LastOrDefault();

            if (rs != null)
            {
                entity.CodigoAutorizadoAssinatura = rs.CodigoAutorizadoAssinatura;
                entity.CodigoAutorizadoGrupo = rs.CodigoAutorizadoGrupo;
                entity.CodigoAutorizadoOrgao = rs.CodigoAutorizadoOrgao;
                entity.DescricaoAutorizadoCargo = rs.DescricaoAutorizadoCargo;
                entity.CodigoAutorizadoAssinatura = rs.CodigoAutorizadoAssinatura;
                entity.NomeAutorizadoAssinatura = rs.NomeAutorizadoAssinatura;

                entity.CodigoExaminadoAssinatura = rs.CodigoExaminadoAssinatura;
                entity.CodigoExaminadoGrupo = rs.CodigoExaminadoGrupo;
                entity.CodigoExaminadoOrgao = rs.CodigoExaminadoOrgao;
                entity.DescricaoExaminadoCargo = rs.DescricaoExaminadoCargo;
                entity.CodigoExaminadoAssinatura = rs.CodigoExaminadoAssinatura;
                entity.NomeExaminadoAssinatura = rs.NomeExaminadoAssinatura;

                entity.CodigoResponsavelAssinatura = rs.CodigoResponsavelAssinatura;
                entity.CodigoResponsavelGrupo = rs.CodigoResponsavelGrupo;
                entity.CodigoResponsavelOrgao = rs.CodigoResponsavelOrgao;
                entity.DescricaoResponsavelCargo = rs.DescricaoResponsavelCargo;
                entity.CodigoResponsavelAssinatura = rs.CodigoResponsavelAssinatura;
                entity.NomeResponsavelAssinatura = rs.NomeResponsavelAssinatura;
            }
        }

        public IEnumerable<MovimentacaoOrcamentaria> BuscarGrid(MovimentacaoOrcamentaria entity, DateTime de = default(DateTime), DateTime ate = default(DateTime))
        {
            var result = _repository.FetchForGrid(entity, de, ate).ToList();

            return result;
        }

        public List<MovimentacaoTipo> Buscar(MovimentacaoTipo obj)
        {
            try
            {
                return _repositoryTipo.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        
        public IEnumerable<MovimentacaoCancelamento> Buscar(MovimentacaoCancelamento obj)
        {
            try
            {
                return _repository.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<MovimentacaoDistribuicao> Buscar(MovimentacaoDistribuicao obj)
        {
            try
            {
                return _repository.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<MovimentacaoNotaDeCredito> Buscar(MovimentacaoNotaDeCredito obj)
        {
            try
            {
                return _repository.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<MovimentacaoReducaoSuplementacao> Buscar(MovimentacaoReducaoSuplementacao obj)
        {
            try
            {
                return _repository.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<MovimentacaoDocumentoTipo> Buscar(MovimentacaoDocumentoTipo obj)
        {
            try
            {
                return _repositoryDoc.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }


        public void Transmitir(int entityId, Usuario user, int recursoId)
        {
            countErro = 0;

            var entity = Selecionar(entityId);
            if (AppConfig.WsUrl != "siafemProd")
            {
                user.CPF = entity.TransmitirSiafem ? AppConfig.WsSiafemUser : AppConfig.WsSiafisicoUser;
                user.SenhaSiafem = Encrypt(AppConfig.WsPassword);
            }

            try
            {
                Transmissao(user, entity, recursoId);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, (short)EnumAcao.Transmitir, recursoId);
            }
        }


        private void TransmitirProdesp(MovimentacaoOrcamentaria entity, int recursoId)
        {
            var cicsmo = new ChaveCicsmo();
            try
            {
                cicsmo = _chave.ObterChave(recursoId);

                var programa = _programa2.Fetch(new Programa { Codigo = entity.IdPrograma }).FirstOrDefault();
                var estrutura = _estutura.Fetch(new Estrutura { Codigo = entity.IdEstrutura }).FirstOrDefault();

                var reducoesSuplementacoes = entity.ReducaoSuplementacao.OrderBy(x => x.NrSequencia).ToList();

                var obj = _prodespContaDer.PreparacaoMovimentacaoInterna(cicsmo.Chave, cicsmo.Senha, programa, estrutura, reducoesSuplementacoes);

                for (int i = 0; i < 15; i++)
                {
                    var prop = obj.GetType().GetProperty("outNumero_" + (i + 1).ToString().PadLeft(2, '0'));

                    var valor = prop.GetValue(obj)?.ToString();

                    if (!string.IsNullOrEmpty(valor))
                    {
                        reducoesSuplementacoes[i].NrSuplementacaoReducao = valor;

                        reducoesSuplementacoes[i].StatusProdesp = "S";
                        reducoesSuplementacoes[i].MensagemProdesp = null;
                    }
                }

                entity.StatusProdesp = "S";
                entity.DataProdesp = DateTime.Now;
                entity.MensagemProdesp = null;
                entity.DataCadastro = DateTime.Now;
                entity.TransmitidoProdesp = true;
            }
            catch (Exception ex)
            {
                entity.StatusProdesp = "E";
                entity.MensagemProdesp = ex.Message;

                foreach (var item in entity.ReducaoSuplementacao)
                {
                    item.StatusProdesp = "E";
                    item.MensagemProdesp = ex.Message;
                }

                throw;
            }
            finally
            {
                _chave.LiberarChave(cicsmo.Codigo);
                SalvarOuAlterar(entity, 0, (short)EnumAcao.Transmitir);
            }
        }


        protected MovimentacaoCancelamento PreencherCancelamento(MovimentacaoOrcamentaria movimentacao, MovimentacaoCancelamento cancelamento)
        {
            cancelamento.IdMovimentacao = movimentacao.Id;
            cancelamento.NrAgrupamento = movimentacao.NrAgrupamento;
            cancelamento.StatusProdesp = string.IsNullOrEmpty(cancelamento.StatusProdesp) ? movimentacao.StatusProdesp : cancelamento.StatusProdesp;
            cancelamento.StatusSiafem = string.IsNullOrEmpty(cancelamento.StatusSiafem) ? movimentacao.StatusSiafem : cancelamento.StatusSiafem;

            listCancelamento.Add(cancelamento);

            return cancelamento;
        }

        protected MovimentacaoDistribuicao PreencherDistribuicao(MovimentacaoOrcamentaria movPai, MovimentacaoDistribuicao distribuicao)
        {
            distribuicao.IdMovimentacao = movPai.Id;
            distribuicao.NrAgrupamento = movPai.NrAgrupamento;
            distribuicao.StatusProdesp = string.IsNullOrEmpty(distribuicao.StatusProdesp) ? movPai.StatusProdesp : distribuicao.StatusProdesp;
            distribuicao.StatusSiafem = string.IsNullOrEmpty(distribuicao.StatusSiafem) ? movPai.StatusSiafem : distribuicao.StatusSiafem;

            listDistribuicao.Add(distribuicao);

            return distribuicao;
        }

        protected MovimentacaoNotaDeCredito PreencherNotaDeCredito(MovimentacaoOrcamentaria movimentacao, MovimentacaoNotaDeCredito nc)
        {
            nc.IdMovimentacao = movimentacao.Id;
            nc.NrAgrupamento = movimentacao.NrAgrupamento;
            nc.StatusProdesp = string.IsNullOrEmpty(nc.StatusProdesp) ? movimentacao.StatusProdesp : nc.StatusProdesp;
            nc.StatusSiafem = string.IsNullOrEmpty(nc.StatusSiafem) ? movimentacao.StatusSiafem : nc.StatusSiafem;

            listCredito.Add(nc);

            return nc;
        }

        protected MovimentacaoReducaoSuplementacao PreencherReducaoSuplementacao(MovimentacaoOrcamentaria movimentacao, MovimentacaoReducaoSuplementacao rs)
        {
            rs.IdMovimentacao = movimentacao.Id;
            rs.NrAgrupamento = movimentacao.NrAgrupamento;
            rs.StatusProdesp = string.IsNullOrEmpty(rs.StatusProdesp) ? movimentacao.StatusProdesp : rs.StatusProdesp;
            rs.StatusSiafem = string.IsNullOrEmpty(rs.StatusSiafem) ? movimentacao.StatusSiafem : rs.StatusSiafem;

            PreencherAssinaturaReducaoSuplementacao(movimentacao, rs);

            listReducaoSuplementacao.Add(rs);

            return rs;
        }

        private static void PreencherAssinaturaReducaoSuplementacao(MovimentacaoOrcamentaria movimentacao, MovimentacaoReducaoSuplementacao rs)
        {
            //rs.CodigoAutorizadoAssinatura = string.IsNullOrWhiteSpace(rs.CodigoAutorizadoAssinatura) ? movimentacao.CodigoAutorizadoAssinatura : rs.CodigoAutorizadoAssinatura;
            //rs.CodigoAutorizadoGrupo = rs.CodigoAutorizadoGrupo == 0 ? movimentacao.CodigoAutorizadoGrupo : rs.CodigoAutorizadoGrupo;
            //rs.CodigoAutorizadoOrgao = string.IsNullOrWhiteSpace(rs.CodigoAutorizadoOrgao) ? movimentacao.CodigoAutorizadoOrgao : rs.CodigoAutorizadoOrgao;
            //rs.DescricaoAutorizadoCargo = string.IsNullOrWhiteSpace(rs.DescricaoAutorizadoCargo) ? movimentacao.DescricaoAutorizadoCargo : rs.DescricaoAutorizadoCargo;
            //rs.CodigoAutorizadoAssinatura = string.IsNullOrWhiteSpace(rs.CodigoAutorizadoAssinatura) ? movimentacao.CodigoAutorizadoAssinatura : rs.CodigoAutorizadoAssinatura;
            //rs.NomeAutorizadoAssinatura = string.IsNullOrWhiteSpace(rs.NomeAutorizadoAssinatura) ? movimentacao.NomeAutorizadoAssinatura : rs.NomeAutorizadoAssinatura;

            //rs.CodigoExaminadoAssinatura = string.IsNullOrWhiteSpace(rs.CodigoExaminadoAssinatura) ? movimentacao.CodigoExaminadoAssinatura : rs.CodigoExaminadoAssinatura;
            //rs.CodigoExaminadoGrupo = rs.CodigoExaminadoGrupo == 0 ? movimentacao.CodigoExaminadoGrupo : rs.CodigoExaminadoGrupo;
            //rs.CodigoExaminadoOrgao = string.IsNullOrWhiteSpace(rs.CodigoExaminadoOrgao) ? movimentacao.CodigoExaminadoOrgao : rs.CodigoExaminadoOrgao;
            //rs.DescricaoExaminadoCargo = string.IsNullOrWhiteSpace(rs.DescricaoExaminadoCargo) ? movimentacao.DescricaoExaminadoCargo : rs.DescricaoExaminadoCargo;
            //rs.CodigoExaminadoAssinatura = string.IsNullOrWhiteSpace(rs.CodigoExaminadoAssinatura) ? movimentacao.CodigoExaminadoAssinatura : rs.CodigoExaminadoAssinatura;
            //rs.NomeExaminadoAssinatura = string.IsNullOrWhiteSpace(rs.NomeExaminadoAssinatura) ? movimentacao.NomeExaminadoAssinatura : rs.NomeExaminadoAssinatura;

            //rs.CodigoResponsavelAssinatura = string.IsNullOrWhiteSpace(rs.CodigoResponsavelAssinatura) ? movimentacao.CodigoResponsavelAssinatura : rs.CodigoResponsavelAssinatura;
            //rs.CodigoResponsavelGrupo = rs.CodigoResponsavelGrupo == 0 ? movimentacao.CodigoResponsavelGrupo : rs.CodigoResponsavelGrupo;
            //rs.CodigoResponsavelOrgao = string.IsNullOrWhiteSpace(rs.CodigoResponsavelOrgao) ? movimentacao.CodigoResponsavelOrgao : rs.CodigoResponsavelOrgao;
            //rs.DescricaoResponsavelCargo = string.IsNullOrWhiteSpace(rs.DescricaoResponsavelCargo) ? movimentacao.DescricaoResponsavelCargo : rs.DescricaoResponsavelCargo;
            //rs.CodigoResponsavelAssinatura = string.IsNullOrWhiteSpace(rs.CodigoResponsavelAssinatura) ? movimentacao.CodigoResponsavelAssinatura : rs.CodigoResponsavelAssinatura;
            //rs.NomeResponsavelAssinatura = string.IsNullOrWhiteSpace(rs.NomeResponsavelAssinatura) ? movimentacao.NomeResponsavelAssinatura : rs.NomeResponsavelAssinatura;


            rs.CodigoAutorizadoAssinatura = movimentacao.CodigoAutorizadoAssinatura;
            rs.CodigoAutorizadoGrupo = movimentacao.CodigoAutorizadoGrupo;
            rs.CodigoAutorizadoOrgao = movimentacao.CodigoAutorizadoOrgao;
            rs.DescricaoAutorizadoCargo = movimentacao.DescricaoAutorizadoCargo;
            rs.CodigoAutorizadoAssinatura = movimentacao.CodigoAutorizadoAssinatura;
            rs.NomeAutorizadoAssinatura = movimentacao.NomeAutorizadoAssinatura;

            rs.CodigoExaminadoAssinatura = movimentacao.CodigoExaminadoAssinatura;
            rs.CodigoExaminadoGrupo = movimentacao.CodigoExaminadoGrupo;
            rs.CodigoExaminadoOrgao = movimentacao.CodigoExaminadoOrgao;
            rs.DescricaoExaminadoCargo = movimentacao.DescricaoExaminadoCargo;
            rs.CodigoExaminadoAssinatura = movimentacao.CodigoExaminadoAssinatura;
            rs.NomeExaminadoAssinatura = movimentacao.NomeExaminadoAssinatura;

            rs.CodigoResponsavelAssinatura = movimentacao.CodigoResponsavelAssinatura;
            rs.CodigoResponsavelGrupo = movimentacao.CodigoResponsavelGrupo;
            rs.CodigoResponsavelOrgao = movimentacao.CodigoResponsavelOrgao;
            rs.DescricaoResponsavelCargo = movimentacao.DescricaoResponsavelCargo;
            rs.CodigoResponsavelAssinatura = movimentacao.CodigoResponsavelAssinatura;
            rs.NomeResponsavelAssinatura = movimentacao.NomeResponsavelAssinatura;
        }

        protected MovimentacaoMes GenerateClassModelMes(MovimentacaoOrcamentaria movPai, MovimentacaoMes item, MovimentacaoMes objModel, MovimentacaoOrcamentaria mov)
        {
            MovimentacaoCancelamento cancelamento = mov.Cancelamento.FirstOrDefault(x => x.NrSequencia == item.NrSequencia && x.NrAgrupamento == movPai.NrAgrupamento);
            MovimentacaoDistribuicao distribuicao = mov.Distribuicao.FirstOrDefault(x => x.NrSequencia == item.NrSequencia && x.NrAgrupamento == movPai.NrAgrupamento);
            MovimentacaoReducaoSuplementacao reducao = mov.ReducaoSuplementacao.FirstOrDefault(x => x.RedSup.Equals("R") && x.NrSequencia == item.NrSequencia && x.NrAgrupamento == movPai.NrAgrupamento);
            MovimentacaoReducaoSuplementacao suplementacao = mov.ReducaoSuplementacao.FirstOrDefault(x => x.RedSup.Equals("S") && x.NrSequencia == item.NrSequencia && x.NrAgrupamento == movPai.NrAgrupamento);

            item.IdMovimentacao = movPai.Id;
            item.IdCancelamento = 0;
            item.IdDistribuicao = 0;
            item.IdReducaoSuplementacao = 0;

            if (item.tab == "C")
            {
                item.IdCancelamento = cancelamento != null ? cancelamento.Id : item.IdCancelamento;
                item.NrAgrupamento = cancelamento != null ? cancelamento.NrAgrupamento : item.NrAgrupamento;
            }
            if (item.tab == "D")
            {
                item.IdDistribuicao = distribuicao != null ? distribuicao.Id : item.IdDistribuicao;
                item.NrAgrupamento = distribuicao != null ? distribuicao.NrAgrupamento : item.NrAgrupamento;
            }

            if (item.tab == "R")
            {
                item.IdReducaoSuplementacao = reducao != null ? reducao.Id : item.IdReducaoSuplementacao;
                item.NrAgrupamento = reducao != null ? reducao.NrAgrupamento : item.NrAgrupamento;
            }

            if (item.tab == "S")
            {
                item.IdReducaoSuplementacao = suplementacao != null ? suplementacao.Id : item.IdReducaoSuplementacao;
                item.NrAgrupamento = suplementacao != null ? suplementacao.NrAgrupamento : item.NrAgrupamento;
            }


            return item;
        }


        #region Cancelamento
        public AcaoEfetuada SalvarOuAlterarCancelamentos(MovimentacaoOrcamentaria entity, IEnumerable<MovimentacaoCancelamento> objModel, int recursoId, short acaoId)
        {
            try
            {
                if (objModel != null)
                {
                    var salvos = _repository.Fetch(new MovimentacaoCancelamento { IdMovimentacao = objModel.FirstOrDefault().IdMovimentacao, NrAgrupamento = objModel.FirstOrDefault().NrAgrupamento });

                    var deleta = salvos.Where(w => entity.Cancelamento.All(a => a.Id != w.Id));


                    foreach (var item in deleta)
                    {
                        Excluir(item, recursoId, acaoId);
                    }

                    foreach (var item in objModel)
                    {
                        item.Id = _repository.Save(item);
                    }

                    return AcaoEfetuada.Sucesso;
                }

                else
                {
                    return AcaoEfetuada.Sucesso;
                }

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada Excluir(IEnumerable<MovimentacaoCancelamento> entities, int resource, short action)
        {
            int valida = 0;
            try
            {
                foreach (var aux in entities)
                {
                    if (aux.StatusSiafem == "S" || aux.StatusProdesp == "S")
                        valida++;
                }


                if (valida == 0)
                {
                    foreach (var item in entities)
                        _repository.Remove(item);

                    return AcaoEfetuada.Sucesso;
                }
                else
                {
                    throw new SidsException("Não é permitido excluir Cancelamento já transmitido");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }


        public AcaoEfetuada Excluir(MovimentacaoCancelamento objModel, int recursoId, int acaoId)
        {
            try
            {
                _repository.Remove(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short?)acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada SalvarOuAlterarItemCancelamentos(MovimentacaoCancelamento entity, int recursoId, short acaoId)
        {
            try
            {
                _repository.Save(entity);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }

        #endregion


        #region Redução Suplementação
        public AcaoEfetuada SalvarOuAlterarReducaoSuplementacao(MovimentacaoOrcamentaria entity, IEnumerable<MovimentacaoReducaoSuplementacao> reducoesSuplementacoes, int recursoId, short acaoId)
        {
            try
            {
                //var reducoesSuplementacoesSeparadas = new List<MovimentacaoReducaoSuplementacao>();
                //reducoesSuplementacoesSeparadas.AddRange(entity.Cancelamento.Where(x => true).Select(x => new MovimentacaoReducaoSuplementacao())); // TODO inserir critério.

                if (reducoesSuplementacoes != null)
                {
                    var salvos = _repository.Fetch(new MovimentacaoReducaoSuplementacao { IdMovimentacao = reducoesSuplementacoes.FirstOrDefault().IdMovimentacao, NrAgrupamento = reducoesSuplementacoes.FirstOrDefault().NrAgrupamento, RedSup = reducoesSuplementacoes.FirstOrDefault().RedSup });

                    var deleta = salvos.Where(w => reducoesSuplementacoes.All(a => a.Id != w.Id));

                    foreach (var item in deleta)
                    {
                        Excluir(item, recursoId, acaoId);
                    }

                    foreach (var item in reducoesSuplementacoes)
                    {
                        item.Id = _repository.Save(item);
                    }

                    return AcaoEfetuada.Sucesso;
                }

                else
                {
                    return AcaoEfetuada.Sucesso;
                }

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }

        public AssinaturasVo BuscarUltimaAssinatura()
        {
            return _repository.BuscarUltimaAssinatura();
        }

        public AcaoEfetuada SalvarOuAlterarReducaoSuplementacaoTransmissao(MovimentacaoOrcamentaria entity, IEnumerable<MovimentacaoReducaoSuplementacao> objModel, int recursoId, short acaoId)
        {
            try
            {
                if (objModel != null)
                {
                    var salvos = _repository.Fetch(new MovimentacaoReducaoSuplementacao { IdMovimentacao = objModel.FirstOrDefault().IdMovimentacao, NrAgrupamento = objModel.FirstOrDefault().NrAgrupamento, NrSequencia = objModel.FirstOrDefault().NrSequencia });

                    var deleta = salvos.Where(w => entity.Distribuicao.All(a => a.Id != w.Id));

                    foreach (var item in deleta)
                    {
                        Excluir(item, recursoId, acaoId);
                    }


                    foreach (var item in objModel)
                    {
                        item.Id = _repository.Save(item);
                    }

                    return AcaoEfetuada.Sucesso;
                }

                else
                {
                    return AcaoEfetuada.Sucesso;
                }

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada Excluir(IEnumerable<MovimentacaoReducaoSuplementacao> entities, int resource, short action)
        {
            int valida = 0;
            try
            {
                foreach (var aux in entities)
                {
                    if (aux.StatusSiafem == "S" || aux.StatusProdesp == "S")
                        valida++;
                }


                if (valida == 0)
                {
                    foreach (var item in entities)
                        _repository.Remove(item);

                    return AcaoEfetuada.Sucesso;
                }
                else
                {
                    throw new SidsException("Não é permitido excluir Redução/Suplementação já transmitido");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }


        public AcaoEfetuada Excluir(MovimentacaoReducaoSuplementacao objModel, int recursoId, int acaoId)
        {
            try
            {
                _repository.Remove(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short?)acaoId, functionalityId: recursoId);
            }
        }
        #endregion


        #region Distribuição
        public AcaoEfetuada SalvarOuAlterarDistribuicao(MovimentacaoOrcamentaria entity, IEnumerable<MovimentacaoDistribuicao> objModel, int recursoId, short acaoId)
        {
            try
            {
                if (objModel != null)
                {
                    var salvos = _repository.Fetch(new MovimentacaoDistribuicao { IdMovimentacao = objModel.FirstOrDefault().IdMovimentacao, NrAgrupamento = objModel.FirstOrDefault().NrAgrupamento });

                    var deleta = salvos.Where(w => entity.Distribuicao.All(a => a.Id != w.Id));

                    foreach (var item in deleta)
                    {
                        Excluir(item, recursoId, acaoId);
                    }

                    foreach (var item in objModel)
                    {
                        item.Id = _repository.Save(item);
                    }

                    return AcaoEfetuada.Sucesso;
                }

                else
                {
                    return AcaoEfetuada.Sucesso;
                }

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada Excluir(IEnumerable<MovimentacaoDistribuicao> entities, int resource, short action)
        {
            int valida = 0;
            try
            {
                foreach (var aux in entities)
                {
                    if (aux.MensagemSiafem == "S" || aux.StatusProdesp == "S")
                        valida++;
                }


                if (valida == 0)
                {
                    foreach (var item in entities)
                        _repository.Remove(item);

                    return AcaoEfetuada.Sucesso;
                }
                else
                {
                    throw new SidsException("Não é permitido excluir Redução/Suplementação já transmitido");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }


        public AcaoEfetuada Excluir(MovimentacaoDistribuicao objModel, int recursoId, int acaoId)
        {
            try
            {
                _repository.Remove(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short?)acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada SalvarOuAlterarItemDistribuicao(MovimentacaoOrcamentaria movimentacao, MovimentacaoDistribuicao entity, int recursoId, short acaoId)
        {
            try
            {
                _repository.Save(entity);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }

        #endregion


        #region Nota Credito
        public AcaoEfetuada SalvarOuAlterarCredito(MovimentacaoOrcamentaria entity, IEnumerable<MovimentacaoNotaDeCredito> objModel, int recursoId, short acaoId)
        {
            try
            {
                if (objModel != null)
                {
                    var salvos = _repository.Fetch(new MovimentacaoNotaDeCredito { IdMovimentacao = objModel.FirstOrDefault().IdMovimentacao, NrAgrupamento = objModel.FirstOrDefault().NrAgrupamento });

                    var deleta = salvos.Where(w => entity.NotasCreditos.All(a => a.Id != w.Id));


                    foreach (var item in deleta)
                    {
                        Excluir(item, recursoId, acaoId);
                    }


                    foreach (var item in objModel)
                    {
                        item.Id = _repository.Save(item);
                    }

                    return AcaoEfetuada.Sucesso;
                }

                else
                {
                    return AcaoEfetuada.Sucesso;
                }

            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada Excluir(IEnumerable<MovimentacaoNotaDeCredito> entities, int resource, short action)
        {
            int valida = 0;
            try
            {
                foreach (var aux in entities)
                {
                    if (aux.StatusSiafem == "S" || aux.StatusProdesp == "S")
                        valida++;
                }


                if (valida == 0)
                {
                    foreach (var item in entities)
                        _repository.Remove(item);

                    return AcaoEfetuada.Sucesso;
                }
                else
                {
                    throw new SidsException("Não é permitido excluir Redução/Suplementação já transmitido");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }


        public AcaoEfetuada Excluir(MovimentacaoNotaDeCredito objModel, int recursoId, int acaoId)
        {
            try
            {
                _repository.Remove(objModel);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: (short?)acaoId, functionalityId: recursoId);
            }
        }


        public AcaoEfetuada SalvarOuAlterarItemNotas(MovimentacaoNotaDeCredito entity, int recursoId, short acaoId)
        {
            try
            {
                _repository.Save(entity);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: acaoId, functionalityId: recursoId);
            }
        }



        #endregion

        public AcaoEfetuada ExcluirMovimentacao(MovimentacaoOrcamentaria entity, int recursoId, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                if (recursoId > 0)
                {
                    return LogSucesso(action, recursoId, $"Movimentação Orçamentária : Codigo {entity.Id}");
                }

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: recursoId);
            }
        }

        private void Transmissao(Usuario usuario, MovimentacaoOrcamentaria objModel, int resource)
        {
            var key = new ChaveCicsmo();

            try
            {
                if (objModel.TransmitirSiafem && objModel.StatusSiafem != "S")
                {
                    TransmitirSiafem(objModel, usuario, resource);
                }


                if (countErro == 0)
                {

                    if (objModel.TransmitirProdesp && objModel.StatusProdesp != "S")
                    {
                        TransmitirProdesp(objModel, resource);
                    }
                }

                key = _chave.ObterChave();
            }
            catch
            {
                throw;
            }
            finally
            {
                _chave.LiberarChave(key.Codigo);
            }
        }

        private void TransmitirSiafem(MovimentacaoOrcamentaria entity, Usuario usuario, int resource)
        {
            countErro = 0;

            try
            {
                var isHasError = false;
                var regional = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First();
                var ug = regional.Uge;

                if (entity.Cancelamento.Any())
                {

                    foreach (var movimentacaoCancelamento in entity.Cancelamento.OrderBy(x => x.NrSequencia))
                    {
                        if (string.IsNullOrEmpty(movimentacaoCancelamento.NumeroSiafem) && countErro == 0)
                        {
                            isHasError = TransmitirSiafemRoboCancelamento(usuario, ug, entity, movimentacaoCancelamento, isHasError);
                        }
                    }

                    if (isHasError)
                    {
                        countErro++;
                        throw new SidsException("Uma ou mais movimentações não puderam ser transmitidas");
                    }

                }

                if (countErro == 0 && entity.NotasCreditos.Any())
                {
                    foreach (MovimentacaoNotaDeCredito notaDeCredito in entity.NotasCreditos.OrderBy(x => x.NrSequencia))
                    {
                        if (string.IsNullOrEmpty(notaDeCredito.NumeroSiafem) && countErro == 0)
                        {
                            notaDeCredito.UnidadeGestoraEmitente = string.IsNullOrEmpty(notaDeCredito.UnidadeGestoraEmitente) ? entity.UnidadeGestoraEmitente : notaDeCredito.UnidadeGestoraEmitente;
                            notaDeCredito.GestaoEmitente = entity.GestaoEmitente;
                            notaDeCredito.DataCadastro = entity.DataCadastro;
                            isHasError = TransmitirSiafemRoboNotaDeCredito(usuario, ug, notaDeCredito, isHasError);
                        }
                    }

                    if (isHasError)
                    {
                        countErro++;
                        throw new SidsException("Uma ou mais movimentações não puderam ser transmitidas");
                    }
                }


                if (countErro == 0 && entity.Distribuicao.Any())
                {
                    foreach (MovimentacaoDistribuicao dist in entity.Distribuicao.OrderBy(x => x.NrSequencia))
                    {
                        if (string.IsNullOrEmpty(dist.NumeroSiafem) && countErro == 0)
                        {
                            FormatarObservacao(dist, entity.NotasCreditos);

                            isHasError = TransmitirSiafemRoboDistribuicao(usuario, ug, entity, dist, isHasError);
                        }
                    }

                    if (isHasError)
                    {
                        countErro++;
                        throw new SidsException("Uma ou mais movimentações não puderam ser transmitidas");
                    }
                }
            }
            finally
            {
                //entity.Agrupamentos = _agrupamento.Buscar(new ProgramacaoDesembolsoAgrupamento { PagamentoContaUnicaId = entity.Id });
            }
        }

        private static void FormatarObservacao(MovimentacaoDistribuicao dist, List<MovimentacaoNotaDeCredito> notas)
        {
            var isTextoPadraoDistribuicao = dist.Observacao?.Contains("Distribuição conforme Nota de Crédito") ?? false;

            var sb = new StringBuilder();

            if (isTextoPadraoDistribuicao)
            {
                sb.Append(dist.Observacao.Replace("...", string.Empty));
            }
            else
            {
                sb.Append("Distribuição conforme Nota de Crédito");
            }

            var ncs = notas.Where(x => x.NrSequencia == dist.NrSequencia && x.Valor == dist.Valor);

            if (ncs.Any())
            {
                foreach (var nc in ncs)
                {
                    sb.Append(" ");
                    sb.Append(nc.NumeroSiafem);
                }
            }

            dist.Observacao = sb.ToString();
        }

        private bool TransmitirSiafemRoboCancelamento(Usuario user, string ug, MovimentacaoOrcamentaria movimentacao, MovimentacaoCancelamento cancelamento, bool isHasError)
        {
            try
            {
                var months = _repositoryMes.FetchCancelamento(new MovimentacaoMes { IdMovimentacao = cancelamento.IdMovimentacao, IdCancelamento = cancelamento.Id, NrAgrupamento = cancelamento.NrAgrupamento, NrSequencia = cancelamento.NrSequencia }).ToList();

                if (cancelamento.IdFonte.ToString().PadLeft(3, '0').Equals("001"))
                {

                    cancelamento.NumeroSiafem = _siafemMov.InserirCancelamentoTesouroSiafem(user.CPF, Decrypt(user.SenhaSiafem), movimentacao, ref cancelamento, months, ug);
                }
                else
                {
                    cancelamento.NumeroSiafem = _siafemMov.InserirCancelamentoNaoTesouroSiafem(user.CPF, Decrypt(user.SenhaSiafem), movimentacao, ref cancelamento, months, ug);
                }

                cancelamento.MensagemSiafem = null;
                cancelamento.StatusSiafem = "S";
                cancelamento.DataSiafem = DateTime.Now;
            }
            catch (Exception ex)
            {
                cancelamento.StatusSiafem = "E";
                cancelamento.MensagemSiafem = ex.Message;
                isHasError = true;
            }
            finally
            {
                SalvarOuAlterarItemCancelamentos(cancelamento, 0, 1);
            }

            return isHasError;
        }

        private bool TransmitirSiafemRoboNotaDeCredito(Usuario user, string ug, MovimentacaoNotaDeCredito notaDeCredito, bool isHasError)
        {
            try
            {
                notaDeCredito.NumeroSiafem = _siafemMov.InserirNotaCreditoSiafem(user.CPF, Decrypt(user.SenhaSiafem), ref notaDeCredito, ug);

                notaDeCredito.MensagemSiafem = null;
                notaDeCredito.StatusSiafem = "S";
                notaDeCredito.DataSiafem = DateTime.Now;
            }
            catch (Exception ex)
            {
                notaDeCredito.StatusSiafem = "E";
                notaDeCredito.MensagemSiafem = ex.Message;
                isHasError = true;
            }
            finally
            {
                SalvarOuAlterarItemNotas(notaDeCredito, 0, 1);
            }

            return isHasError;
        }

        private bool TransmitirSiafemRoboDistribuicao(Usuario user, string ug, MovimentacaoOrcamentaria movimentacao, MovimentacaoDistribuicao movimentacaoOrcamentaria, bool isHasError)
        {
            var months = _repositoryMes.FetchDistribuicao(new MovimentacaoMes { IdMovimentacao = movimentacaoOrcamentaria.IdMovimentacao, IdDistribuicao = movimentacaoOrcamentaria.Id, NrAgrupamento = movimentacaoOrcamentaria.NrAgrupamento, NrSequencia = movimentacaoOrcamentaria.NrSequencia }).ToList();


            try
            {

                if (movimentacaoOrcamentaria.IdFonte.PadLeft(3, '0').Equals("001"))
                {
                    movimentacaoOrcamentaria.NumeroSiafem = _siafemMov.InserirDistribuicaoTesouroSiafem(user.CPF, Decrypt(user.SenhaSiafem), movimentacao, ref movimentacaoOrcamentaria, months, ug);
                }
                else
                {
                    movimentacaoOrcamentaria.NumeroSiafem = _siafemMov.InserirDistribuicaoNaoTesouroSiafem(user.CPF, Decrypt(user.SenhaSiafem), movimentacao, ref movimentacaoOrcamentaria, months, ug);
                }


                movimentacaoOrcamentaria.MensagemSiafem = null;
                movimentacaoOrcamentaria.StatusSiafem = "S";
                movimentacaoOrcamentaria.DataSiafem = DateTime.Now;
            }
            catch (Exception ex)
            {
                movimentacaoOrcamentaria.StatusSiafem = "E";
                movimentacaoOrcamentaria.MensagemSiafem = ex.Message;
                isHasError = true;
            }
            finally
            {
                SalvarOuAlterarItemDistribuicao(movimentacao, movimentacaoOrcamentaria, 0, 1);
            }

            return isHasError;
        }
        
        private void MontarObservacao(MovimentacaoOrcamentaria entity, MovimentacaoCancelamento cancelamento, StringBuilder sb)
        {
            var ugfsJaAdicionadas = new List<string>();
            var regexUG = new Regex(@"\d{6}");
            var matches = regexUG.Matches(cancelamento.Observacao);
            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    ugfsJaAdicionadas.Add(match.Value);
                }
            }


            for (int i = 0; i < entity.Distribuicao.Count(); i++)
            {
                var item = entity.Distribuicao.ElementAt(i);

                var jaExiste = ugfsJaAdicionadas.Contains(item.UnidadeGestoraFavorecida);

                var regional = _regional.Buscar(new Regional { Uge = item.UnidadeGestoraFavorecida })?.LastOrDefault();

                if (regional != null)
                {
                    if (!jaExiste)
                    {
                        if (i == 0)
                        {
                            sb.Append(" ");
                        }
                        else
                        {
                            sb.Append(", ");
                        }

                        sb.Append(regional.Descricao?.Substring(0, 4));
                    }
                }
            }
        }

        #region Impressão NL NC

        public RespostaConsultaNL ConsultaNL(Usuario usuario, string unidadegestora, string gestao, string numeroNlNc, string tipoDocumento, string nrReducao, string nrSuplementacao)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var uge = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            if (string.IsNullOrWhiteSpace(unidadegestora))
            {
                unidadegestora = uge;
            }

            var nl = _siafemMov.ConsultaNL(usuario.CPF, Decrypt(usuario.SenhaSiafem), unidadegestora, gestao, numeroNlNc, tipoDocumento, nrReducao, nrSuplementacao);

            nl.DataEmissao = ConverterData(nl.DataEmissao);
            nl.DataLancamento = ConverterData(nl.DataLancamento);
            nl.LancadoData = ConverterData(nl.LancadoData);

            return nl;
        }

        public RespostaConsultaNC ConsultaNC(Usuario usuario, string unidadegestora, string gestao, string numeroNlNc, string tipoDocumento, string nrReducao, string nrSuplementacao)
        {
            if (AppConfig.WsUrl != "siafemProd")
                usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = Encrypt(AppConfig.WsPassword), RegionalId = 1 };

            var uge = _regional.Buscar(new Regional { Id = (int)usuario.RegionalId }).First().Uge;

            if (string.IsNullOrWhiteSpace(unidadegestora))
            {
                unidadegestora = uge;
            }

            var nc = _siafemMov.ConsultaNC(usuario.CPF, Decrypt(usuario.SenhaSiafem), unidadegestora, gestao, numeroNlNc, tipoDocumento, nrReducao, nrSuplementacao);

            nc.DataEmissao = ConverterData(nc.DataEmissao);
            nc.LancadoPorEm = ConverterData(nc.LancadoPorEm);

            return nc;
        }

        public string ConverterData(string data)
        {
            switch (data.Substring(2, 3))
            {
                case "JAN":
                    data = data.Replace("JAN", "01");
                    break;
                case "FEV":
                    data = data.Replace("FEV", "02");
                    break;
                case "MAR":
                    data = data.Replace("MAR", "03");
                    break;
                case "ABR":
                    data = data.Replace("ABR", "04");
                    break;
                case "MAI":
                    data = data.Replace("MAI", "05");
                    break;
                case "JUN":
                    data = data.Replace("JUN", "06");
                    break;
                case "JUL":
                    data = data.Replace("JUL", "07");
                    break;
                case "AGO":
                    data = data.Replace("AGO", "08");
                    break;
                case "SET":
                    data = data.Replace("SET", "09");
                    break;
                case "OUT":
                    data = data.Replace("OUT", "10");
                    break;
                case "NOV":
                    data = data.Replace("NOV", "11");
                    break;
                case "DEZ":
                    data = data.Replace("DEZ", "12");
                    break;
            }

            return data = data.Substring(0, 2) + "/" + data.Substring(2, 2) + "/" + data.Substring(4, 4);
        }

        #endregion
    }
}