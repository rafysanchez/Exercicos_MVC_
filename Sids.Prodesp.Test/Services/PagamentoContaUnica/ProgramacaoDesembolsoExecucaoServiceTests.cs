using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Core.Base;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using Sids.Prodesp.UI.Report;

namespace Sids.Prodesp.Test.Services.PagamentoContaUnica
{
    [TestClass()]
    public class ProgramacaoDesembolsoExecucaoServiceTests
    {

        private readonly PDExecucao _entity;
        private List<PDExecucao> _entities;
        private readonly Usuario _user;

        public ProgramacaoDesembolsoExecucaoServiceTests()
        {
            _user = new Usuario { Codigo = 1, RegionalId = 16 };
            _entities = new List<PDExecucao>();
            _entity = CreateEntityFactory();
        }

        private PDExecucao CreateEntityFactory()
        {
            return null;
        }
    }
}
