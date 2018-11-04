namespace Sids.Prodesp.Core.Services.Configuracao
{
    using Base;
    using Model.Entity.Configuracao;
    using Model.Enum;
    using Model.Exceptions;
    using Model.Interface.Configuracao;
    using Model.Interface.Log;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class FonteService : BaseService
    {
        ICrudFonte _fonte;
        public FonteService(ILogError l, ICrudFonte f) : base(l)
        {
            _fonte = f;
        }

        public AcaoEfetuada Salvar(Fonte obj, int fontesId, short actionId)
        {
            try
            {
                if (obj.Id == 0) //se for insert
                {
                    if (_fonte.Fetch(new Fonte { Descricao = obj.Descricao , Codigo = obj.Codigo}).Any(x => x.Descricao == obj.Descricao))
                        throw new SidsException("Não é possível realizar cadastro, já existe fonte com a descrição e o código já cadastrada");


                    if (_fonte.Fetch(new Fonte { Codigo = obj.Codigo }).Any())
                        throw new SidsException("Não é possível realizar cadastro, já existe fonte com o código já cadastrado");

                    if (_fonte.Fetch(new Fonte { Descricao = obj.Descricao }).Any(x => x.Descricao == obj.Descricao))
                        throw new SidsException("Não é possível realizar cadastro, já existe fonte com a descrição já cadastrada");

                    obj.Id = _fonte.Add(obj);
                }
                else
                {
                    if (_fonte.DuplicateCheck(new Fonte { Id = obj.Id, Codigo = obj.Codigo, Descricao = obj.Descricao }).Any())
                        throw new SidsException("Registro já existente!");
                    _fonte.Edit(obj);
                }


                var fontes = (IEnumerator<Fonte>)_fonte.Fetch(new Fonte());
                SetCurrentCache(fontes, "Fonte");

                var arg = $"Fonte {obj.Descricao}, Codigo {obj.Id}";
                return LogSucesso(actionId, fontesId, arg);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: fontesId);
            }
        }


        public IEnumerable<Fonte> Buscar(Fonte obj)
        {
            try
            {
                return _fonte.Fetch(obj);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public IEnumerable<Fonte> Listar(Fonte objModel)
        {
            try
            {
                var lista = (IEnumerable<Fonte>)GetCurrentCache<Fonte>("Fonte");

                if (lista != null)
                {
                    return lista;
                }

                var fontes = (IEnumerator<Fonte>)_fonte.Fetch(new Fonte());
                SetCurrentCache(fontes, "Fonte");

                return (IEnumerable<Fonte>)fontes;
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }


        public AcaoEfetuada Excluir(Fonte fonte, int fontesId, short actionId)
        {
            try
            {
                _fonte.Remove(fonte.Id);
                var arg = string.Format("Fonte {0}, Id {1}", fonte.Descricao, fonte.Id.ToString());

                var programas = (IEnumerator<Fonte>)_fonte.Fetch(new Fonte());
                SetCurrentCache(programas, "Fonte");

                return LogSucesso(actionId, fontesId, arg);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex, actionId: actionId, functionalityId: fontesId);
            }
        }

        public IEnumerable<Fonte> VerificaDuplicado(Fonte obj)
        {
            try
            {
                return _fonte.Fetch(obj);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }

        public List<SelectListItem> ObterFontes()
        {
            var fontesCodigos = Buscar(new Fonte()).ToList().Select(x => new { Codigo = x.Codigo.Substring(1, 2)}).Distinct().ToList();

            return fontesCodigos.Select(x => new SelectListItem
            {
                Text = x.Codigo,
                Value = x.Codigo
            }).ToList();
        }
    }
}
