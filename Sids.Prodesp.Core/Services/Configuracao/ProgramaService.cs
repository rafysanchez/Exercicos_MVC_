using Sids.Prodesp.Core.Base;
namespace Sids.Prodesp.Core.Services.Configuracao
{
    using Model.Enum;
    using Model.Exceptions;
    using Model.Interface.Log;
    using Sids.Prodesp.Model.Entity.Configuracao;
    using Sids.Prodesp.Model.Interface.Configuracao;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class ProgramaService : BaseService
    {
        private ICrudPrograma _programa;
        private ICrudEstrutura _estrutura;

        public ProgramaService(ILogError l, ICrudPrograma p, ICrudEstrutura e) : base(l)
        {
            _estrutura = e;
            _programa = p;
        }

        public AcaoEfetuada Excluir(Programa programa, int recursoId, short actionId)
        {
            try
            {
                var estruturas = _estrutura.Fetch(new Estrutura { Programa = programa.Codigo }).Count();

                if (estruturas > 0)
                    throw new SidsException("Existem itens de estrutura associados ao programa, não é permitida a exclusão.");

                if (Buscar(new Programa { Ano = (programa.Ano + 1) }).Any())
                    throw new SidsException($"Não é permitida a exclusão do programa. Existe estrutura de programas cadastradas no ano {programa.Ano + 1}.");

                _programa.Remove(programa.Codigo);

                var arg = $"Programa {programa.Descricao}, Codigo {programa.Codigo}";


                var programas = (IEnumerator<Programa>)_programa.Fetch(new Programa());
                SetCurrentCache(programas, "Programa");

            return LogSucesso(actionId, recursoId, arg);
        }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        public AcaoEfetuada Salvar(Programa objModel, int recursoId, short actionId)
        {

            try
            {
                ValidarPrograma(objModel);

                if (objModel.Codigo == 0)
                {
                    objModel.Cfp = objModel.Cfp.Replace(".", "");

                    objModel.Codigo = _programa.Add(objModel);
                }
                else
                {
                    objModel.Cfp = objModel.Cfp.Replace(".", "");
                    _programa.Edit(objModel);
                }

                var programas = (IEnumerator<Programa>)_programa.Fetch(new Programa());
                SetCurrentCache(programas, "Programa");

                var arg = $"Programa {objModel.Descricao}, Codigo {objModel.Codigo}";

                return LogSucesso(actionId, recursoId, arg);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId, recursoId);
            }
        }

        private void ValidarPrograma(Programa objModel)
        {
            var mensagem = "Não é possível realizar cadastro, já existe programa com o ";
            bool ptres = false;
            bool cfp = false;
            bool descricao = false;

            List<Programa> programa;

            programa = _programa.Fetch(new Programa { Ano = objModel.Ano, Ptres = objModel.Ptres }).ToList();

            if (programa.Count(x => x.Codigo != objModel.Codigo) > 0)
                mensagem = mensagem + "Ptres: " + objModel.Ptres;
            else
            ptres = true;

            objModel.Cfp = objModel.Cfp.Replace(".", "");

            programa = _programa.Fetch(new Programa { Ano = objModel.Ano, Cfp = objModel.Cfp }).ToList();

            if (programa.Count(x => x.Codigo != objModel.Codigo) > 0)
                mensagem = mensagem + (ptres ? "" : ",") + " Cfp: " + objModel.Cfp;
            else
                cfp = true;

            programa = _programa.Fetch(new Programa { Ano = objModel.Ano, Descricao = objModel.Descricao }).ToList();

            if (programa.Count(x => x.Codigo != objModel.Codigo && x.Descricao == objModel.Descricao) > 0)
               mensagem = mensagem + (cfp ? "" : " e") + " Descricao: " + objModel.Descricao;
            else
                descricao = true;

            mensagem = mensagem + " cadastrado para o ano referência selecionado.";


            if (!descricao || !ptres || !cfp)
                throw new SidsException(mensagem);

        }

        public IEnumerable<Programa> Buscar(Programa objModel)
        {
            try
            {
                return _programa.Fetch(objModel);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public IEnumerable<Programa> Listar(Programa objModel)
        {
            try
            {
                var lista = (IEnumerable<Programa>)GetCurrentCache<Programa>("Programa");

                if (lista != null)
                {
                    return lista;
                }

                var programas = (IEnumerator<Programa>)_programa.Fetch(new Programa());
                SetCurrentCache(programas, "Programa");

                return (IEnumerable<Programa>)programas;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public IEnumerable<int> GetAnosPrograma()
        {
            try
            {
                return Buscar(new Programa()).GroupBy(g => g.Ano).Select(s => s.Key).Distinct();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<SelectListItem> ObterAnos()
        {
            var anos = GetAnosPrograma().ToList();

            if (anos.All(x => x != DateTime.Now.Year))
                anos.Add(DateTime.Now.Year);

            return anos.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString()
            }).ToList();
        }

        public int GerarEstruturaAnoAtual(int recursoId, short actionId)
        {
            try
            {

                int ano = GetAnosPrograma().ToList().Max();

                ano = Buscar(new Programa()).Select(x => x.Ano).Distinct().Max();

                ano += 1;

                _programa.CopyProgramStructureFromYear(ano);

                var arg = String.Format("Gerar nova tabela de estrutura ano {0}", ano.ToString());

                LogSucesso(actionId, recursoId, arg);

                return ano;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
}
