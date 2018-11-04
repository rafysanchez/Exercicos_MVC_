namespace Sids.Prodesp.Core.Services.Seguranca
{
    using Model.Entity.Seguranca;
    using Model.Interface.Log;
    using Model.Interface.Seguranca;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FuncionalidadeAcaoService : Base.BaseService
    {
        ICrudFuncionalidadeAcao funcionalidadeAcao;

        public FuncionalidadeAcaoService(ILogError l, ICrudFuncionalidadeAcao r)
            : base(l)
        {
            funcionalidadeAcao = r;
        }

        public void Salvar(FuncionalidadeAcao obj)
        {
            try
            {
                if (obj.Codigo == 0)
                {
                    PreInsertModel(obj);
                    funcionalidadeAcao.Add(obj);
                }
                else
                {
                    funcionalidadeAcao.Edit(obj);
                }
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public void Excluir(int id)
        {
            try
            {
                funcionalidadeAcao.Remove(id);
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<FuncionalidadeAcao> Buscar(FuncionalidadeAcao obj)
        {
            try
            {
                return funcionalidadeAcao.Fetch(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
        public List<FuncionalidadeAcao> GetFuncionalidadeAcaoByFuncionalidade(Funcionalidade obj)
        {
            try
            {
                if (obj == null)
                    obj = new Funcionalidade();

                return funcionalidadeAcao.FetchByFunctionality(obj).ToList();
            }
            catch (Exception ex)
            {
                throw SaveLog(ex);
            }
        }
    }
        
}
