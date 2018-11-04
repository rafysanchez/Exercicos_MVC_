using System.Linq;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    using Base;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;

    public class ProgramacaoDesembolsoAgrupamentoService : BaseService
    {
        private readonly ICrudProgramacaoDesembolsoAgrupamento _repository;
        private readonly ProgramacaoDesembolsoAgrupamentoService _agrupamento;

        public ProgramacaoDesembolsoAgrupamentoService(ILogError log, ICrudProgramacaoDesembolsoAgrupamento repository) : base(log)
        {
            _repository = repository;
        }


        public AcaoEfetuada Excluir(ProgramacaoDesembolsoAgrupamento entity, int resource, short action)
        {
            try
            {
                _repository.Remove(entity.Id);

                return AcaoEfetuada.Sucesso;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: action, functionalityId: resource);
            }
        }

        public AcaoEfetuada Excluir(IEnumerable<ProgramacaoDesembolsoAgrupamento> entities, int resource, short action)
        {
            int valida = 0;
            try
            {
                foreach (var aux in entities)
                {
                    if (aux.TransmitidoSiafem)
                        valida++;
                }


                if (valida == 0)
                {
                    foreach (var item in entities)
                        Excluir(item, resource, action);

                    return AcaoEfetuada.Sucesso;
                }
                else
                {
                    throw new SidsException("Não é permitido excluir lista de PD, existem PD’s já transmitidas");
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }


        public int SalvarOuAlterar(ProgramacaoDesembolsoAgrupamento entity, int resource, short action)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, action, resource);
            }
        }

        public IEnumerable<ProgramacaoDesembolsoAgrupamento> Buscar(ProgramacaoDesembolsoAgrupamento entity)
        {
            return _repository.Fetch(entity);
        }

        public ProgramacaoDesembolsoAgrupamento Selecionar(int id)
        {
            return _repository.Fetch(new ProgramacaoDesembolsoAgrupamento { Id = id }).FirstOrDefault();
        }

        public int BuscarUltimoAgupamento()
        {
            return _repository.GetLastGroup();
        }

        public ProgramacaoDesembolso ProgramacaoDesembolsoFactory(ProgramacaoDesembolsoAgrupamento agrupamento)
        {
            return new ProgramacaoDesembolso
            {
                Id = agrupamento.Id,
                CadastroCompleto = true,
                TransmitirSiafem = true,
                MensagemServicoSiafem = agrupamento.MensagemServicoSiafem,
                NumeroDocumentoGerador = agrupamento.NumeroDocumentoGerador,
                DataCadastro = agrupamento.DataCadastro,
                DataEmissao = agrupamento.DataEmissao,
                DataTransmitidoSiafem = agrupamento.DataTransmitidoSiafem,
                DocumentoTipoId = agrupamento.DocumentoTipoId,
                NumeroDocumento = agrupamento.NumeroDocumento,
                TransmitidoSiafem = agrupamento.TransmitidoSiafem,
                RegionalId = agrupamento.RegionalId,
                CodigoDespesa = agrupamento.CodigoDespesa,
                ProgramacaoDesembolsoTipoId = agrupamento.ProgramacaoDesembolsoTipoId,
                CodigoUnidadeGestora = agrupamento.CodigoUnidadeGestora,
                CodigoGestao = agrupamento.CodigoGestao,
                DataVencimento = agrupamento.DataVencimento,
                NumeroListaAnexo = agrupamento.NumeroListaAnexo,
                NumeroNLReferencia = agrupamento.NumeroNLReferencia ?? string.Empty,
                NumeroCnpjcpfPagto = agrupamento.NumeroCnpjcpfPagto,
                GestaoPagto = agrupamento.GestaoPagto,
                NumeroBancoPagto = agrupamento.NumeroBancoPagto,
                NumeroAgenciaPagto = agrupamento.NumeroAgenciaPagto,
                NumeroContaPagto = agrupamento.NumeroContaPagto,
                NumeroCnpjcpfCredor = agrupamento.NumeroCnpjcpfCredor,
                GestaoCredor = agrupamento.GestaoCredor,
                NumeroBancoCredor = agrupamento.NumeroBancoCredor,
                NumeroAgenciaCredor = agrupamento.NumeroAgenciaCredor,
                NumeroContaCredor = agrupamento.NumeroContaCredor,
                NumeroProcesso = agrupamento.NumeroProcesso,
                Valor = agrupamento.Valor,
                Finalidade = agrupamento.Finalidade,
                Agrupamentos = new List<ProgramacaoDesembolsoAgrupamento>
                                {
                                    new ProgramacaoDesembolsoAgrupamento
                                    {
                                        NumeroDocumentoGerador = agrupamento.NumeroDocumentoGerador,
                                        NomeCredorReduzido = agrupamento.NomeCredorReduzido,
                                        NumeroCnpjcpfPagto = agrupamento.NumeroCnpjcpfPagto,
                                        Classificacao = agrupamento.Classificacao,
                                        DataVencimento = agrupamento.DataVencimento,
                                        Valor = agrupamento.Valor,
                                        NumeroSiafem = agrupamento.NumeroSiafem,
                                        MensagemServicoSiafem = agrupamento.MensagemServicoSiafem
                                    }
                                },
                Eventos = new List<ProgramacaoDesembolsoEvento>
                            {
                                new ProgramacaoDesembolsoEvento
                                {
                                    NumeroEvento = agrupamento.NumeroEvento,
                                    InscricaoEvento = agrupamento.InscricaoEvento,
                                    Classificacao = agrupamento.Classificacao,
                                    ValorUnitario = (int)agrupamento.Valor,
                                    Fonte = agrupamento.Fonte
                                }
                            }
            };
        }
    }
}
