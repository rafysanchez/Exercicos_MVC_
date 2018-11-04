using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Core.Services.Seguranca;
using Sids.Prodesp.Model.Entity.Seguranca;
using System.Collections.Generic;
using Sids.Prodesp.Infrastructure.Log;
using Sids.Prodesp.Infrastructure.DataBase.Seguranca;
using System.Linq;
using Sids.Prodesp.Model.Enum;

namespace Sids.Prodesp.Test
{
    [TestClass]
    public class PerfilTest
    {
        PerfilService perfilService;
        Perfil obj;
        List<Perfil> listObj;     

        [TestMethod]
        public void TesteIncluirPerfil()
        {
            perfilService = new PerfilService(new LogErrorDal(), new PerfilDal(), new PerfilAcaoDal());
            
            obj = new Perfil();
            obj.Descricao = "Teste Perfil Luis";
            obj.Status = true;
            obj.DataCriacao = DateTime.Parse("01/01/2016 00:00:00");
            obj.UsuarioCriacao = 7;

            var listRecursos = new List<PerfilAcao>();
            listRecursos.Add(new PerfilAcao(0,2, obj.Codigo,1));
            

            Assert.AreEqual(AcaoEfetuada.Sucesso, perfilService.Salvar(obj, listRecursos,1,1));
        }

        [TestMethod]
        public void TesteAlterarPerfil()
        {
            perfilService = new PerfilService(new LogErrorDal(), new PerfilDal(), new PerfilAcaoDal());

            obj = new Perfil();
            obj.Codigo = 3;
            obj.Descricao = "Teste Perfil";
            obj.Status = true;
            var listRecursos = new List<PerfilAcao>();
            listRecursos.Add(new PerfilAcao(1,2, obj.Codigo, 1));


            Assert.AreEqual(AcaoEfetuada.Sucesso, perfilService.Salvar(obj, listRecursos,1,1));
        }

        [TestMethod]
        public void TesteListarPerfil()
        {
            perfilService = new PerfilService(new LogErrorDal(), new PerfilDal(), new PerfilAcaoDal());

            obj = new Perfil();
            listObj = new List<Perfil>();

            obj.Codigo = 3;
            obj.Descricao = "Teste Perfil";
            obj.Status = true;
            obj.DataCriacao = DateTime.Parse("01/01/2016 00:00:00");
            obj.UsuarioCriacao = 1;
            listObj.Add(obj);

            Assert.AreEqual(Util.Comparador(listObj.FirstOrDefault(), perfilService.Buscar(obj).FirstOrDefault()), true);
        }
    }
}
