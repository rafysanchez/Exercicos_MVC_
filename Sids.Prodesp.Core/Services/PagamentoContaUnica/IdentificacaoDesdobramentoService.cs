using System;
using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System.Linq;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class IdentificacaoDesdobramentoService
    {
        private readonly ICrudIdentificacaoDesdobramento _repository;

        public IdentificacaoDesdobramentoService(ICrudIdentificacaoDesdobramento repository)
        {
            _repository = repository;
        }

        public void Excluir(IdentificacaoDesdobramento entity)
        {
            try
            {
                _repository.Remove(entity);

            }
            catch
            {
                throw;
            }
        }

        public int SalvarOuAlterar(IdentificacaoDesdobramento entity)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                return entity.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SalvarOuAlterar(IEnumerable<IdentificacaoDesdobramento> identificacoes)
        {
            var tipos2 = identificacoes.Where(x => x.DesdobramentoTipoId == 2);
            var zerados = tipos2.Where(x => x.ValorDistribuicao == 0 && x.ValorDesdobrado == 0 && x.ValorDesdobradoInicial == 0);

            tipos2 = tipos2.Except(zerados);

            identificacoes = identificacoes.Except(tipos2).Except(zerados);

            identificacoes = identificacoes.Union(tipos2).Union(zerados);

            var seq = 1;
            foreach (var idd in identificacoes)
            {
                idd.Sequencia = seq++;
            }

            foreach (var identificacaoDesdobramento in identificacoes)
            {
                identificacaoDesdobramento.Id = SalvarOuAlterar(identificacaoDesdobramento);
            }
        }

        public IEnumerable<IdentificacaoDesdobramento> Listar(IdentificacaoDesdobramento entity)
        {
            return _repository.Fetch(entity);
        }


        public IdentificacaoDesdobramento Selecionar(int Id)
        {
            var entity = _repository.Get(Id);

            return entity;
        }
    }
}
