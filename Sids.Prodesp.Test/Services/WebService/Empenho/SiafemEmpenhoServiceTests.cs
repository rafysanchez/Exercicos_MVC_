namespace Sids.Prodesp.Test.Services.WebService.Empenho
{
    using Application;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model.Entity.Empenho;
    using Model.Entity.Seguranca;
    using Model.Interface.Base;
    using Model.Interface.Empenho;
    using Model.ValueObject.Service.Siafem.Empenho;
    using PdfSharp;
    using PdfSharp.Drawing;
    using PdfSharp.Pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Sids.Prodesp.Infrastructure;
    [TestClass()]
    public class SiafemEmpenhoServiceTests
    {
        private Model.Entity.Empenho.Empenho _model;
        private IEnumerable<IMes> _mes;
        private IEnumerable<IEmpenhoItem> _item;
        private readonly Usuario _usuario;
        private EmpenhoReforco _modelReforco;

        public SiafemEmpenhoServiceTests()
        {
            _usuario = new Usuario { Codigo = 1, CPF = AppConfig.WsSiafemUser, SenhaSiafem = AppConfig.WsPassword };
            CreateInstance();
        }

        #region Empenho

        
        [TestMethod()]
        public void InserirEmpenhoSiafemTest()
        {

            var result = App.SiafemEmpenhoService.InserirEmpenhoSiafem(_usuario.CPF, _usuario.SenhaSiafem,ref _model, _mes, _item, "162101");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirEmpenhoDescricaoSiafemTest()
        {
            _modelReforco.NumeroEmpenhoSiafem = "2016NE00016";
                var result = App.SiafemEmpenhoService.InserirEmpenhoReforcoSiafem(_usuario.CPF, _usuario.SenhaSiafem,ref _modelReforco, _item, "162101");
       
            Assert.IsNotNull(result);
        }

        
        [TestMethod()]
        public void InserirEmpenhoSiafisicoTest()
        {
            var result = App.SiafemEmpenhoService.InserirEmpenhoSiafisico(AppConfig.WsSiafisicoUser, _usuario.SenhaSiafem, _model, _mes, _item, "162101");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirEmpenhoDescricaoSiafisicoTest()
        {
            foreach (var empenhoItem in _item)
            {
                _model.NumeroCT = "2016CT00007";
                 App.SiafemEmpenhoService.InserirEmpenhoSiafisico(AppConfig.WsSiafisicoUser, _usuario.SenhaSiafem, _model, empenhoItem, "162101");
            }

            Assert.Fail();
        }



        [TestMethod()]
        public void InserirEmpenhoDescricaoSiafisicoAlteracaoTest()
        {
            foreach (var empenhoItem in _item)
            {
                _model.NumeroCT = "2016CT00006";
                App.SiafemEmpenhoService.InserirEmpenhoSiafisicoAlteracao(AppConfig.WsSiafisicoUser, _usuario.SenhaSiafem, _model, empenhoItem, "162101");
            }

            Assert.Fail();
        }


        [TestMethod()]
        public void ContabilizarEmpenhoSiafisicoTest()
        {
                _model.NumeroCT = "2016CT00006";
            var result = App.SiafemEmpenhoService.ContablizarEmpenho(AppConfig.WsSiafisicoUser, _usuario.SenhaSiafem, _model, "162101");
            Assert.IsNotNull(result);
        }
        #endregion


        #region Empenho Reforço

        [TestMethod()]
        public void InserirEmpenhoReforcoSiafemTest()
        {

            var result = App.SiafemEmpenhoService.InserirEmpenhoReforcoSiafem(_usuario.CPF, _usuario.SenhaSiafem, ref _modelReforco, _mes, _item, "162101");
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void InserirEmpenhoReforcoDescricaoSiafemTest()
        {
            _modelReforco.NumeroEmpenhoSiafem = "2016NE00013";
            var result = App.SiafemEmpenhoService.InserirEmpenhoReforcoSiafem(_usuario.CPF, _usuario.SenhaSiafem, ref _modelReforco, _item, "162101");
            Assert.IsNotNull(result);
        }



        #endregion


        #region Empenho Cancelamento
        [TestMethod()]
        public void InserirEmpenhoCancelamentoTest()
        {

        }
        #endregion


        #region Outros

        [TestMethod()]
        public void ConsultaCtTest()
        {

            var result = App.SiafemEmpenhoService.ConsultaCt(AppConfig.WsSiafisicoUser /*AppConfig.WsSiafisicoUser*/,_usuario.SenhaSiafem, "2016CT00008", "162101","16055");
            
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void ConsultaEmpenhos()

        {

            var result = App.SiafemEmpenhoService.ConsultaEmpenhos(AppConfig.WsSiafemUser, _usuario.SenhaSiafem, "", "", "", "16055", "162101", "", "", "", "", "2016NE00008", "", "162101");

            Assert.IsTrue(typeof(ConsultaEmpenhos) == result.GetType());

        }

        [TestMethod()]
        public void ConsultaNeTest()
        {
            var empenho = new Model.Entity.Empenho.Empenho
            {
                DataEmissao = DateTime.Now,
                CodigoGestao = "16055",
                CodigoUnidadeGestora = "162101",
                NumeroProcesso = "processo1",
                CodigoUGO = 162101,
                NumeroEmpenhoSiafem = "2016NE00016",
                //NumeroEmpenhoSiafisico = "2016NE00012"
            };

            var result = App.SiafemEmpenhoService.ConsultaNe(AppConfig.WsSiafemUser /*AppConfig.WsSiafisicoUser*/, AppConfig.WsPassword, empenho, "162101");

            var teste = GerarPdfEmpenho(result, "Empenho", empenho);

            Assert.IsNotNull(teste);

        }

        public static FileStreamResult GerarPdfEmpenho(ConsultaNe consulta, string tipo, Model.Entity.Empenho.Empenho empenho)
        {
            #region Configuraçao

            var meses = new List<mes>();

            var properties = consulta.GetType().GetProperties().ToList();

            int i = 0;
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name.Contains("Mes"))
                {
                    if (propertyInfo.GetValue(consulta).ToString() != "")
                    {
                        mes mes = new mes { Mes = propertyInfo.GetValue(consulta).ToString(), Valor = "" };
                        meses.Add(mes);
                    }

                    i += 1;
                }
            }

            i = 0;
            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.Name.Contains("Valor") && propertyInfo.Name != "Valor")
                {

                    if (propertyInfo.GetValue(consulta).ToString() != "")
                    {
                        meses[i].Valor = propertyInfo.GetValue(consulta).ToString();
                        i += 1;
                    }
                }
            }


            var fileName = consulta.NumeroNe + ".pdf";

            string file = @"C:\Users\810235\Documents/" + fileName;

            FileInfo newFile = new FileInfo(file);

            if (newFile.Exists)
            {
                newFile.Delete();
            }

            int inicio = 30;
            int linha = 68;
            int pularLinha = 13;

            var document = new PdfDocument();
            var page = document.AddPage();
            page.Size = PageSize.A4;
            var graphics = XGraphics.FromPdfPage(page);
            var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio = GerarImagem();

            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold), XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Nota de " + tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1


            //Nº do Documento
            linha += 30;
            textFormatter.DrawString("Nº do Documento:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº do Documento:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.NumeroNe, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data Emissão
            val += graphics.MeasureString("Nº do Documento:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString("Data da Emissão:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Data da Emissão:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.DataEmissao, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Nº do Documento Prodesp
            linha += (pularLinha);
            textFormatter.DrawString("Nº do Empenho Prodesp:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº do Empenho Prodesp:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(empenho.NumeroEmpenhoProdesp ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



            //Gestão
            linha += pularLinha;
            textFormatter.DrawString("Gestão:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.Gestao, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Nº Processo
            linha += (pularLinha);
            textFormatter.DrawString("Nº Processo:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº Processo:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.NumeroProcesso, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco2
            //Credor
            linha += (pularLinha * 2);
            textFormatter.DrawString("Credor:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Credor:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.GestaoCredor, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            //CPF / CNPJ
            textFormatter.DrawString("CPF / CNPJ:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("CPF / CNPJ:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.CgcCpf, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Origem do Material
            linha += (pularLinha * 2);
            textFormatter.DrawString("Origem do Material:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Origem do Material:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.OrigemMaterial, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco3

            //Evento
            linha += (pularLinha * 2);
            textFormatter.DrawString("Evento", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Evento:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString(consulta.Evento, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val = 0;
            //UO
            linha += (pularLinha * 2);
            textFormatter.DrawString("UO", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Uo, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            //PT
            val += graphics.MeasureString("UO", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString("Programa de Trabalho", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Pt, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //Fonte
            val += graphics.MeasureString("Programa de Trabalho", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString("Fonte", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Fonte, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            //Natureza Despesa
            val += graphics.MeasureString("Fonte", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString("Nat. Desp.", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Despesa, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //UGR
            val += graphics.MeasureString("Nat. Desp.", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString("UGR", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Ugo, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //Plano Interno
            val += graphics.MeasureString("UGR", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
            textFormatter.DrawString("PI", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.PlanoInterno, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            #endregion

            #region Bloco3
            //ReferenciaLegal
            linha += (pularLinha * 3);
            textFormatter.DrawString("Refer. Legal:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Refer. Legal:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.ReferenciaLegal, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //EmpenhoOriginal
            textFormatter.DrawString("Empenho Orig:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect((page.Width * 0.45), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Empenho Orig:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.EmpenhoOriginal, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect((page.Width * 0.45) + val, linha, page.Width - 60, page.Height - 60));

            //Licitação
            linha += (pularLinha);
            textFormatter.DrawString("Licitação:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Licitação:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.Licitacao, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Modalidade
            textFormatter.DrawString("Modalidade:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect((page.Width * 0.45), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Modalidade:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.Modalidade, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect((page.Width * 0.45) + val, linha, page.Width - 60, page.Height - 60));

            //Tipo Empenho
            linha += (pularLinha);
            textFormatter.DrawString("Tipo Empenho:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Tipo Empenho:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.TipoEmpenho, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Modalidade
            textFormatter.DrawString("Nº do Contrato:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect((page.Width * 0.45), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº do Contrato:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.NumeroContrato, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect((page.Width * 0.45) + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco4
            linha += (pularLinha);
            if (!string.IsNullOrEmpty(empenho.NumeroEmpenhoSiafem))
            {
                //Obra
                textFormatter.DrawString("Nº da Obra:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Nº da Obra:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.IdentificadorObra, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Unidade de Medida:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.unidade, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val = graphics.MeasureString("Unidade de Medida:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Valor Unitario:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.valorunitario, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Preço Total
                val += graphics.MeasureString("Valor Unitario:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Preço Total:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.precototal, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            }

            #endregion

            #region Bloco5
            linha += (pularLinha);
            if (!string.IsNullOrEmpty(empenho.NumeroEmpenhoSiafisico))
            {
                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Item Seq.", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.sequencia, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Item Seq.", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Item Serviço", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.item, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Item Serviço", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Quantidade do Item", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.qtdeitem, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Quantidade do Item:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Valor Unitario", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.valorunitario, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Preço Total
                val += graphics.MeasureString("Valor Unitario", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Preço Total", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.precototal, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            }

            #endregion

            #region Bloco6

            linha += pularLinha * 3;

            //Valor Empenho
            textFormatter.DrawString("Valor Empenho:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Valor Empenho:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.Valor, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            #endregion


            #region Bloco7




            linha += (pularLinha * 3);
            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
            textFormatter.DrawString("Cronograma de Desembolso Previsto", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Cronograma de Desembolso Previsto", new XFont("Calibri", 10, XFontStyle.Bold)).Width / 2;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha + 11.8, page.Width - 20, 0.1));



            linha += (pularLinha * 2);
            textFormatter.DrawString("Janeiro", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "01")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Fevereiro", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "02")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Março", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "03")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 2);
            textFormatter.DrawString("Abril", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "04")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Maio", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "05")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Junho", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "06")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 2);
            textFormatter.DrawString("Julho", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "07")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Agosto", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "08")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Setembro", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "09")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 2);
            textFormatter.DrawString("Outubro", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "10")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Novembro", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "11")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Dezembro", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "12")?.Valor ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            #endregion


            #region bloco8



            linha += (pularLinha * 9);
            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("Lançado Por:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", new XFont("Calibri", 10, XFontStyle.Bold)).Width;
            textFormatter.DrawString(consulta.Lancadopor, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Em:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(consulta.HoraLancamento, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() + " e hora " + consulta.HoraConsulta;

            textFormatter.DrawString(text, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, page.Height - 40, page.Width - 60, page.Height - 60));

            textFormatter.DrawString("Nº FCO:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, page.Height - 40 + pularLinha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº FCO:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
            textFormatter.DrawString(empenho.NumeroEmpenhoProdesp ?? "", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, page.Height - 40 + pularLinha, page.Width - 60, page.Height - 60));


            #endregion

            document.Save(fileName);
            System.Diagnostics.Process.Start(fileName);

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);
            stream.Position = 0;

            var fileDownloadName = string.Format("{0}.pdf", consulta.NumeroNe);

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;
        }

        #endregion


        #region Classes

        internal class mes
        {
            public string Mes { get; set; }
            public string Valor { get; set; }
        }

        #endregion


        #region Metodos Privados
        private static string GerarImagem()
        {
            string img = @"C:\Projetos\DER - Integracao SIAFEM\Fontes\Desenvolvimento\main\G&P\sids.prodesp1\Source\Reserva\Sids.Prodesp.Test\Brasão_do_estado_de_São_Paulo.jpg";
            return img;
        }

        private void CreateInstance()
        {
            _model = CreateInstanceEmpenho();
            _modelReforco = CreateInstanceEmpenhoRefoco();
            _mes = CreateInsatanceEmpenhoMes(_model);
            _item = CreateInsatanceEmpenhoItem(_model);
        }

        private static Model.Entity.Empenho.Empenho CreateInstanceEmpenho()
        {
            return new Model.Entity.Empenho.Empenho
            {
                NumeroAnoExercicio = 2016,
                RegionalId = 16,
                ProgramaId = 210,
                FonteId = 6,
                NaturezaId = 1683,
                NumeroEmpenhoSiafem = "",
                NumeroEmpenhoProdesp = "",
                DestinoId = "24",
                CodigoGestao = "16055",
                CodigoEvento = 400051,
                CodigoGestaoCredor = "",
                DataEmissao = Convert.ToDateTime("31/12/2016"),
                CodigoUnidadeGestora = "162101",
                DescricaoAutorizadoSupraFolha = "teste",
                CodigoEspecificacaoDespesa = "001",
                DescricaoEspecificacaoDespesa = "1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE",
                CodigoNaturezaItem = "30",
                CodigoUnidadeGestoraFornecedora = "162101",
                NumeroContrato = "1399179024",
                NumeroCNPJCPFFornecedor = "00000028000129",
                NumeroCNPJCPFUGCredor = "00000028000129",
                NumeroProcesso = "PROCESSO1",
                CodigoAutorizadoAssinatura = 11111.ToString(),
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                NomeAutorizadoAssinatura = "<====NOME DA ASSINATURA 11111=====>",
                DescricaoAutorizadoCargo = "<==CARGO DA ASSINATURA HUMHUMHUM==>",
                CodigoExaminadoAssinatura = 12345.ToString(),
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                NomeExaminadoAssinatura = "<======UMDOISTRESQUATROCINCO======>",
                DescricaoExaminadoCargo = "<========DIRETOR EXECUTIVO========>",
                CodigoResponsavelAssinatura = 88888.ToString(),
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NomeResponsavelAssinatura = "ANALISTA SETOR",
                DescricaoResponsavelCargo = "KAMIYA",
                TransmitirProdesp = false,
                TransmitirSiafem = true,
                TransmitirSiafisico = false,
                EmpenhoTipoId = 9,
                DescricaoBairroEntrega = "G",
                ModalidadeId = 1,
                DataEntregaMaterial = DateTime.Now,
                DescricaoLogradouroEntrega = "Teste",
                CodigoCredorOrganizacao = 2,
                NumeroCEPEntrega = "01000000",
                LicitacaoId = "6",
                CodigoAplicacaoObra = "0042663",
                NumeroProcessoSiafisico = "processo1",
                NumeroEmpenhoSiafisico = "2016NE00001",
                OrigemMaterialId = 1,
                CodigoUGO = 162101,
                DescricaoReferenciaLegal = "Lei 866693",
                TipoAquisicaoId = 1,
                CodigoGestaoFornecedora = "16055",
                CodigoMunicipio = "0100",
                DescricaoLocalEntregaSiafem = "Rua X",
                DescricaoCidadeEntrega = "Ferraz",
                CodigoUnidadeFornecimento = "3077",

            };
        }

        private static EmpenhoReforco CreateInstanceEmpenhoRefoco()
        {
            return new EmpenhoReforco
            {

                NumeroAnoExercicio = 2016,
                RegionalId = 16,
                ProgramaId = 210,
                FonteId = 6,
                NaturezaId = 1683,
                NumeroEmpenhoSiafem = "",
                NumeroEmpenhoProdesp = "",
                DestinoId = "24",
                CodigoGestao = "16055",
                CodigoEvento = 400052,
                CodigoGestaoCredor = " ",
                DataEmissao = Convert.ToDateTime("31/12/2016"),
                CodigoUnidadeGestora = "162101",
                DescricaoAutorizadoSupraFolha = "teste",
                CodigoEspecificacaoDespesa = "001",
                DescricaoEspecificacaoDespesa = "1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE;1RESRE",
                CodigoUnidadeGestoraFornecedora = "162101",
                NumeroContrato = "1399179024",
                NumeroCNPJCPFFornecedor = "00000028000129",
                NumeroCNPJCPFUGCredor = "00000028000129",
                NumeroProcesso = "PROCESSO1",
                CodigoAutorizadoAssinatura = 11111.ToString(),
                CodigoAutorizadoGrupo = 1,
                CodigoAutorizadoOrgao = "99",
                NomeAutorizadoAssinatura = "<====NOME DA ASSINATURA 11111=====>",
                DescricaoAutorizadoCargo = "<==CARGO DA ASSINATURA HUMHUMHUM==>",
                CodigoExaminadoAssinatura = 12345.ToString(),
                CodigoExaminadoGrupo = 1,
                CodigoExaminadoOrgao = "99",
                NomeExaminadoAssinatura = "<======UMDOISTRESQUATROCINCO======>",
                DescricaoExaminadoCargo = "<========DIRETOR EXECUTIVO========>",
                CodigoResponsavelAssinatura = 88888.ToString(),
                CodigoResponsavelGrupo = 1,
                CodigoResponsavelOrgao = "00",
                NomeResponsavelAssinatura = "ANALISTA SETOR",
                DescricaoResponsavelCargo = "KAMIYA",
                TransmitirProdesp = false,
                TransmitirSiafem = true,
                TransmitirSiafisico = false,
                ModalidadeId = 3,
                CodigoCredorOrganizacao = 2,
                LicitacaoId = "6",
                NumeroProcessoSiafisico = "processo1",
                NumeroEmpenhoSiafisico = "2016NE00001",
                TipoObraId = 1,
                CodigoEmpenhoOriginal = "2016NE00005",
                CodigoNaturezaItem = "30"

            };
        }

        private IList<IMes> CreateInsatanceEmpenhoMes(IEmpenho objModel)
        {
            return new List<IMes>
            {
                new EmpenhoMes {ValorMes = 20, Id = objModel.Id, Descricao= "12" }
            };
        }

        private List<EmpenhoItem> CreateInsatanceEmpenhoItem(IEmpenho objModel)
        {
            return new List<EmpenhoItem>
            {
                new EmpenhoItem { ValorUnitario = 04, EmpenhoId = objModel.Id, CodigoUnidadeFornecimentoItem = "16055", QuantidadeMaterialServico = 1, DescricaoItemSiafem = "Teste", CodigoItemServico = "3360", DescricaoJustificativaPreco = "teste", DescricaoUnidadeMedida = "Test", ValorTotal = 04 },
                //new EmpenhoItem { ValorUnitario = 04, EmpenhoId = objModel.Id, CodigoUnidadeFornecimento = "16055", QuantidadeMaterialServico = 1, DescricaoItemSiafem = "Teste", CodigoItemServico = "134392", DescricaoJustificativaPreco = "teste", DescricaoUnidadeMedida = "Test", ValorTotal = 04 },
                //new EmpenhoItem { ValorUnitario = 04, EmpenhoId = objModel.Id, CodigoUnidadeFornecimento = "16055", QuantidadeMaterialServico = 1, DescricaoItemSiafem = "Teste", CodigoItemServico = "134392", DescricaoJustificativaPreco = "teste", DescricaoUnidadeMedida = "Test", ValorTotal = 04 },
                //new EmpenhoItem { ValorUnitario = 04, EmpenhoId = objModel.Id, CodigoUnidadeFornecimento = "16055", QuantidadeMaterialServico = 1, DescricaoItemSiafem = "Teste", CodigoItemServico = "134392", DescricaoJustificativaPreco = "teste", DescricaoUnidadeMedida = "Test", ValorTotal = 04 },
                //new EmpenhoItem { ValorUnitario = 04, EmpenhoId = objModel.Id, CodigoUnidadeFornecimento = "16055", QuantidadeMaterialServico = 1, DescricaoItemSiafem = "Teste", CodigoItemServico = "134392", DescricaoJustificativaPreco = "teste", DescricaoUnidadeMedida = "Test", ValorTotal = 04 }
            };
        }

        #endregion
    }
}