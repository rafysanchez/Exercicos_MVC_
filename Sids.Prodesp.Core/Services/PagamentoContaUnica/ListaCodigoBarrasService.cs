using System;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica;
using System.Collections.Generic;
using System.Linq;
using Sids.Prodesp.Infrastructure.DataBase.PagamentoContaUnica;

namespace Sids.Prodesp.Core.Services.PagamentoContaUnica
{
    public class ListaCodigoBarrasService
    {
        private readonly ICrudListaCodigoBarras _repository;
        private readonly CodigoBarraTaxaService _codigoBarraTaxa;
        private readonly CodigoBarraBoletoService _codigoBarraBoleto;

        public ListaCodigoBarrasService(ICrudListaCodigoBarras repository)
        {
            _repository = repository;
             _codigoBarraBoleto= new CodigoBarraBoletoService(new CodigoBarraBoletoDal());
            _codigoBarraTaxa = new CodigoBarraTaxaService(new CodigoBarraTaxaDal());
        }

        public IEnumerable<ListaCodigoBarras> Listar(ListaCodigoBarras entity)
        {
            var entities = _repository.Fetch(entity).ToList();
            entities.ForEach(x => x.CodigoBarraBoleto = _codigoBarraBoleto.Selecionar(new CodigoBarraBoleto { CodigoBarraId = x.Id }));
            entities.ForEach(x => x.CodigoBarraTaxa = _codigoBarraTaxa.Selecionar(new CodigoBarraTaxa { CodigoBarraId = x.Id }));
            return entities;
        }

        public void Excluir(ListaCodigoBarras entity)
        {
            try
            {
                _repository.Remove(entity.Id);

            }
            catch
            {
                throw;
            }
        }

        public int SalvarOuAlterar(ListaCodigoBarras entity)
        {
            try
            {
                entity.Id = _repository.Save(entity);

                return entity.Id;
            }
            catch
            {
                throw;
            }
        }


        public void SalvarOuAlterar(IEnumerable<ListaCodigoBarras> entity)
        {
            try
            {
                foreach (var codigoBarras in entity)
                {

                    codigoBarras.Id = SalvarOuAlterar(codigoBarras);

                    if (!string.IsNullOrWhiteSpace(codigoBarras.CodigoBarraTaxa?.NumeroConta1))
                    {
                        codigoBarras.CodigoBarraTaxa.CodigoBarraId = codigoBarras.Id;
                        codigoBarras.CodigoBarraTaxa.Id = _codigoBarraTaxa.SalvarOuAlterar(codigoBarras.CodigoBarraTaxa);
                    }

                    if (!string.IsNullOrWhiteSpace(codigoBarras.CodigoBarraBoleto?.NumeroConta1))
                    {
                        codigoBarras.CodigoBarraBoleto.CodigoBarraId = codigoBarras.Id;
                        codigoBarras.CodigoBarraBoleto.Id = _codigoBarraBoleto.SalvarOuAlterar(codigoBarras.CodigoBarraBoleto);
                    }

                }

            }
            catch
            {
                throw;
            }
        }

    }


}







