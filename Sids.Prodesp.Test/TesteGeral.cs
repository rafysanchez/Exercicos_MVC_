using Microsoft.VisualStudio.TestTools.UnitTesting;
using PdfSharp.Drawing;
using Sids.Prodesp.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.Reserva;
using PageSize = PdfSharp.PageSize;
using Sids.Prodesp.Infrastructure;

namespace Sids.Prodesp.Test
{
    [TestClass]
    public class TesteGeral
    {

        [TestMethod]
        public void GerarPdf()
        {
            var fileName = @"C:\Users\810235\Documents\TestePDF.pdf";
            int inicio = 30;
            int linha = 68;
            int pularLinha = 13;
            try
            {

                var consulta = ConsultaNr();

                var document = new PdfSharp.Pdf.PdfDocument();
                var page = document.AddPage();
                page.Size = PageSize.A4;
                var graphics = XGraphics.FromPdfPage(page);
                var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);

                double val = 0;
                double espaco = 25;

                // Imagem.
                graphics.DrawImage(XImage.FromFile(GerarImagem()), inicio - 20, 20, 60, 72);

                // Textos.
                //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
                textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold), XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

                linha += 32;
                textFormatter.DrawString("Nota de Reserva", new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

                // Figuras geométricas.
                linha += 20;
                graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


                //Data Emissão
                linha += 30;
                textFormatter.DrawString("Data da Emissão:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data da Emissão:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.DataEmissao, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Usuário
                linha += pularLinha;
                textFormatter.DrawString("Usuário:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Usuário:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.Usuario, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Data de Lançamento
                linha += (pularLinha * 2);
                textFormatter.DrawString("Data de Lançamento:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data de Lançamento:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.DataLancamento, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Unidade Gestora
                linha += (pularLinha * 2);
                textFormatter.DrawString("Unidade Gestora:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Unidade Gestorao:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.UnidadeGestora + " " + consulta.DescricaoUG, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Gestão
                linha += (pularLinha);
                textFormatter.DrawString("Gestão:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.DescricaoGestao, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Número NR
                linha += (pularLinha * 2);
                textFormatter.DrawString("Número NR:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Número NR:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.NumeroNR, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Evento
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Evento:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.Evento.ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Processo
                linha += (pularLinha);
                textFormatter.DrawString("Processo:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Processo:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.NumeroProcesso, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                //Número OC
                linha += (pularLinha);
                textFormatter.DrawString("Número OC:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Número OC:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.NumOC, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                //Ptres
                linha += (pularLinha);
                textFormatter.DrawString("Ptres:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Ptres:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.Ptres.ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val = 0;
                espaco = 25;

                //UO
                linha += (pularLinha * 2);
                textFormatter.DrawString("UO", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("UO", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Programa de Trabalho", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Programa de Trabalho", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Fonte de Recurso", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Fonte de Recurso", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Natureza de Despesa", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Natureza de Despesa", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("UGR", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("UGR", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Plano Interno", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Plano Interno", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString("Valor", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val = 0;

                linha += (pularLinha);
                textFormatter.DrawString(consulta.Uo.ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("UO", new XFont("Calibri", 10, XFontStyle.Regular)).Width + espaco;
                textFormatter.DrawString(consulta.Pt.ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Programa de Trabalho", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString(consulta.Fonte, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Fonte de Recurso", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString(consulta.Despesa, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Natureza de Despesa", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString(consulta.UGR.ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("UGR", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString(consulta.PlanoInterno, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Plano Interno", new XFont("Calibri", 10, XFontStyle.Bold)).Width + espaco;
                textFormatter.DrawString(consulta.Valor, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += (pularLinha * 3);
                textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
                textFormatter.DrawString("Cronograma", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Cronograma", new XFont("Calibri", 10, XFontStyle.Bold)).Width * 2;
                graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio + 217, linha + 11.8, val, 0.1));


                textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
                linha += (pularLinha * 2);
                textFormatter.DrawString("Mês", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + 217, linha, page.Width - 60, page.Height - 60));

                val -= graphics.MeasureString("Valor", new XFont("Calibri", 10, XFontStyle.Bold)).Width;
                textFormatter.DrawString("Valor", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + 217 + val, linha, page.Width - 60, page.Height - 60));

                var properties = consulta.GetType().GetProperties().ToList();
                
                int i = 0;
                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Name.Contains("Mes"))
                    {

                        linha += (pularLinha);
                        textFormatter.DrawString(propertyInfo.GetValue(consulta).ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + 217, linha, page.Width - 60, page.Height - 60));

                        i += 1;
                    }
                }

                linha -= (pularLinha * i);
                i = 0;
                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Name.Contains("Valor") && propertyInfo.Name != "Valor")
                    {
                        linha += (pularLinha);
                        textFormatter.DrawString(propertyInfo.GetValue(consulta).ToString(), new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + 217 + val, linha, page.Width - 60, page.Height - 60));
                        i += 1;
                    }
                }

                var text = "";

                linha += (pularLinha * 2);
                textFormatter.DrawString("Observações", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                linha += (pularLinha);
                textFormatter.DrawString(consulta.Observacao1, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                linha += (pularLinha);
                textFormatter.DrawString(consulta.Observacao2, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                linha += (pularLinha);
                textFormatter.DrawString(consulta.Observacao3, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                linha += (pularLinha);

                linha += (pularLinha * 3);
                textFormatter.DrawString("Lançado Por:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Lançado Por:", new XFont("Calibri", 10, XFontStyle.Bold)).Width;
                textFormatter.DrawString(consulta.Lancadopor, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


                textFormatter.DrawString("Em:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Em:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.DataLancamento, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


                textFormatter.DrawString("Às:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Às:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString(consulta.HoraLancamento, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


                text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() + " e hora " + consulta.HoraConsulta;

                linha += (pularLinha * 9);
                textFormatter.DrawString(text, new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));


                linha += (pularLinha);
                textFormatter.DrawString("nº FCO:", new XFont("Calibri", 10, XFontStyle.Bold), XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("nº FCO:", new XFont("Calibri", 10, XFontStyle.Bold)).Width + 2;
                textFormatter.DrawString("169900001", new XFont("Calibri", 10, XFontStyle.Regular), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                document.Save(fileName);
                System.Diagnostics.Process.Start(fileName);
            }
            catch
            {
                //handle exception
            }
        }
        
        [TestMethod]
        public void TesteListarString()
        {
            var teste = ListaString(25, "Luis Fernando Dias Campos Santps", 30)[0];
        }


        [TestMethod]
        public void ReplcaceTest()
        {
            Regex regex = new Regex(@"\-");
            var teste = "1-2-3-4-5-6";
            teste = regex.Replace(teste,"");
            Assert.IsFalse(teste.Contains("-"));
        }

        private static ConsultaNr ConsultaNr()
        {
            var reserva = new Model.Entity.Reserva.Reserva
            {
                DataEmissao = DateTime.Now,
                Uo = "16055",
                Processo = "processo1",
                Ugo = "162101",
                Oc = "00259",
                Observacao =
                    "asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdasd7;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas15;asdasdasdasdasdasdasdasdaasdasdasdasdasdasdasdasdassdasdasdasdaasdasdasdas23",
                NumSiafemSiafisico = "2016NR00012"
            };
            
            var consulta = App.SiafemReservaService.ConsultaNr(AppConfig.WsSiafemUser /*AppConfig.WsSiafisicoUser*/, AppConfig.WsPassword, reserva, "162101");
            return consulta;
        }

        public string GerarImagem()
        {
            string img = @"C:\Projetos\DER - Integracao SIAFEM\Fontes\Desenvolvimento\main\G&P\sids.prodesp1\Source\Reserva\Sids.Prodesp.Test\Brasão_do_estado_de_São_Paulo.jpg";
            return img;
        }

        private static List<string> ListaString(int dist, string texto, int qtd)
        {
            texto += texto.Length % 2 > 0 ? " " : "";
            texto = texto.Replace(";", "").Replace(";", "").Replace(";", "");

            var resultado = new List<string>();
            
            for (var x = 0; x < qtd; x++)
            {
                var need =((x + 1)*dist);
                var fim = texto.Length >= need ? dist : texto.Length - (0 + x * dist);

                if (texto.Length >= 0 + x*dist)
                    resultado.Add(texto.Substring(0 + x*dist, fim ));
                else
                    resultado.Add(null);
            }

            return resultado;
        }
    }

}
