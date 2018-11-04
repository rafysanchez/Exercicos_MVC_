
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sids.Prodesp.Application;
using Sids.Prodesp.Model.Entity.Reserva;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Infrastructure;

namespace Sids.Prodesp.Text.Services.Reserva
{

    //using Model.Entity.Reserva;
    //using Model.Enum;
    //using System.Collections;

    [TestClass()]
    public class ReforcoServiceTests
    {
        private readonly ReservaReforco _reforco;
        private readonly List<ReservaReforcoMes> _reforcoMes;
        private readonly Usuario _usuario;

        public ReforcoServiceTests()
        {
            _usuario = new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword };
            _reforcoMes = GerarNewReforco(out _reforco);
        }

        [TestMethod()]
        public void BuscarTest()
        {
            var reforcos = App.ReservaReforcoService.Buscar(new ReservaReforco { Contrato = "1600123455" });

            Assert.IsNotNull(reforcos);
        }

        [TestMethod()]
        public void BuscarGridTest()
        {
            var reforcos = App.ReservaReforcoService.BuscarGrid(new ReservaReforco { Contrato = "1600123455" });

            Assert.IsNotNull(reforcos);
        }

        [TestMethod()]
        public void ExcluirTest()
        {
            var reforcos = App.ReservaReforcoService.Buscar(new ReservaReforco { Contrato = "1600123455" }).FirstOrDefault();
            var result = App.ReservaReforcoService.Excluir(reforcos, 0, 2);

            Assert.IsNotNull(result);
        }



        [TestMethod()]
        public void TransmitirTest()
        {
            var id = App.ReservaReforcoService.Salvar(_reforco, _reforcoMes, 0, 1);

            App.ReservaReforcoService.Transmitir(id, _usuario, 0);

            Assert.Fail("False");
        }

        [TestMethod()]
        public void ReTransmitirTest()
        {
            List<int> Codigo = new List<int> { 1, 2, 3 };
            App.ReservaReforcoService.Transmitir(Codigo, _usuario, 0);

            Assert.Fail("False");
        }

        [TestMethod()]
        public void SalvarTest()
        {
            var id = App.ReservaReforcoService.Salvar(_reforco, _reforcoMes, 0, 1);
            Assert.IsTrue(id > 0);
        }

        static Usuario CriarInstanciaUsuario()
        {
            return new Usuario { CPF = AppConfig.WsSiafemUser, SenhaSiafem = App.UsuarioService.Encrypt(AppConfig.WsPassword) };
        }
        static ReservaReforco CriarInstanciaReservaReforco()
        {
            return new ReservaReforco
            {
                Estrutura = 1,
                Fonte = 1,
                Programa = 1,
                Contrato = "1217777991",
                AnoExercicio = 2016,
                Regional = 3,
                AutorizadoAssinatura = "11111",
                AutorizadoGrupo = "1",
                AutorizadoOrgao = "99",
                AutorizadoSupraFolha = "fl01",
                DestinoRecurso = "04",
                ExaminadoAssinatura = "12345",
                ExaminadoGrupo = "1",
                ExaminadoOrgao = "99",
                Processo = "processo1",
                OrigemRecurso = "004001004",
                ResponsavelAssinatura = "88888",
                ResponsavelGrupo = "1",
                ResponsavelOrgao = "00",
                EspecificacaoDespesa = "001",
                DescEspecificacaoDespesa = "TESTE",
                DataEmissao = DateTime.Parse("15/02/2016"),
                Uo = "16055",
                Ugo = "122101",
                Oc = "00259",
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23",
                TransmitirProdesp = true,
                TransmitirSiafem = true
            };
        }
        static List<ReservaReforcoMes> CriarInstanciaReforcoMes()
        {
            return new List<ReservaReforcoMes>
            {
                new ReservaReforcoMes {ValorMes = (decimal) 1, Descricao = "01"},
                new ReservaReforcoMes {ValorMes = (decimal) 2, Descricao = "04"},
                new ReservaReforcoMes {ValorMes = (decimal) 3, Descricao = "07"},
                new ReservaReforcoMes {ValorMes = (decimal) 4, Descricao = "10"}
            };
        }
        private static List<ReservaReforcoMes> GerarNewReforco(out Model.Entity.Reserva.ReservaReforco reforco)
        {
            var reforcoMes = CriarInstanciaReforcoMes();
            reforco = new Model.Entity.Reserva.ReservaReforco
            {

                Estrutura = 1,
                Fonte = 1,
                Programa = 1,
                Contrato = "1217777991",
                AnoExercicio = 2016,
                Regional = 3,
                AutorizadoAssinatura = "11111",
                AutorizadoGrupo = "1",
                AutorizadoOrgao = "99",
                AutorizadoSupraFolha = "fl01",
                DestinoRecurso = "04",
                ExaminadoAssinatura = "12345",
                ExaminadoGrupo = "1",
                ExaminadoOrgao = "99",
                Processo = "processo1",
                OrigemRecurso = "004001004",
                ResponsavelAssinatura = "88888",
                ResponsavelGrupo = "1",
                ResponsavelOrgao = "00",
                EspecificacaoDespesa = "001",
                DescEspecificacaoDespesa = "TESTE",
                DataEmissao = DateTime.Parse("15/02/2016"),
                Uo = "16055",
                Ugo = "122101",
                Oc = "00259",
                Observacao = "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23",
                TransmitirProdesp = true,
                TransmitirSiafem = true
            };
            return reforcoMes;
        }
    }
}
