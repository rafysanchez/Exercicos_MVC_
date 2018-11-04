
namespace Sids.Prodesp.Core.Services.Configuracao
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Interface.Configuracao;
    using Model.Interface.Log;
    using Model.Interface.Reserva;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EstruturaService : BaseService
    {
        private readonly ICrudEstrutura _estrutura;
        private readonly ICrudReserva _reserva;
        private readonly ICrudPrograma _programa;

        public EstruturaService(ILogError l,ICrudEstrutura e, ICrudReserva r,ICrudPrograma p) : base(l)
        {
            _programa = p;
            _reserva = r;
            _estrutura = e;
        }
        

        public IEnumerable<Estrutura> Buscar(Estrutura objModel)
        {
            return _estrutura.Fetch(objModel);
        }
        
        public IEnumerable<Estrutura> Listar(Estrutura objModel)
        {
            try
            {
                var lista = (IEnumerable<Estrutura>)GetCurrentCache<Estrutura>("Estrutura");

                if (lista != null)
                {
                    return lista;
                }

                var estruturas = (IEnumerator<Estrutura>)_estrutura.Fetch(new Estrutura());
                SetCurrentCache(estruturas, "Estrutura");

                return (IEnumerable<Estrutura>)estruturas;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }


        public AcaoEfetuada Excluir(Estrutura estrutura, int recursoId, short actionId)
        {
            try
            {

                var reservars = _reserva.Fetch(new Model.Entity.Reserva.Reserva { Estrutura = estrutura.Codigo }).Count();

                if (reservars > 0)
                    throw new SidsException("Existem reservas cadastradas com o item selecionado na estrutura, não é permitida a exclusão.");

                _estrutura.Remove(estrutura.Codigo);

                var arg = String.Format("Estrutura {0}, Codigo {1}", estrutura.Nomenclatura, estrutura.Codigo);

                var estruturas = (IEnumerator<Estrutura>)_estrutura.Fetch(new Estrutura());
                SetCurrentCache(estruturas, "Estrutura");

                return LogSucesso(actionId, recursoId, arg);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        public AcaoEfetuada Salvar(Estrutura objModel, int recursoId, short actionId)
        {
            try
            {
                objModel.Natureza = objModel.Natureza.Replace(".", "");

                ValidadrEstrutura(objModel);

                if (objModel.Codigo == 0)
                {

                    objModel.Codigo = _estrutura.Add(objModel);
                }
                else
                {
                    _estrutura.Edit(objModel);
                }

                var estruturas = (IEnumerator<Estrutura>)_estrutura.Fetch(new Estrutura());
                SetCurrentCache(estruturas, "Estrutura");

                var arg = String.Format("Estrutura {0}, Codigo {1}", objModel.Nomenclatura, objModel.Codigo);

                return LogSucesso(actionId, recursoId, arg);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        private void ValidadrEstrutura(Estrutura objModel)
        {
            var buscar = new Estrutura
            {
                Natureza = objModel.Natureza,
                Aplicacao = objModel.Aplicacao,
                Fonte = objModel.Fonte,
                Programa = objModel.Programa,
                Macro = objModel.Macro
            };
            
            var estrutura = _estrutura.Fetch(buscar).ToList();

            if (estrutura.Count(x => x.Codigo != objModel.Codigo) <= 0) return;

            var cfp = _programa.Fetch(new Programa {Codigo = (int) buscar.Programa}).FirstOrDefault().Cfp;

            throw new SidsException("Não é possível realizar cadastro, para o programa " + cfp +
                                ", já existe cadastrado Natureza " + objModel.Natureza + ", Macro " +
                                objModel.Macro + ", Fonte " + objModel.Fonte + " e Código da Aplicação" +
                                objModel.Aplicacao + ".");
        }

        public IEnumerable<Estrutura> ObterPorPrograma(Programa objModel)
        {
            return _estrutura.FetchByProgram(objModel);
        }
        

    }
}
