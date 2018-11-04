using System.Globalization;
using System.Text.RegularExpressions;
using PdfSharp.Drawing.Layout;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ReclassificacaoRetencao;
using Sids.Prodesp.Model.Interface.LiquidacaoDespesa;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using Sids.Prodesp.Model.ValueObject.Service.Prodesp.Common;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica;
using System.Linq;

namespace Sids.Prodesp.UI.Report
{
    using PdfSharp;
    using PdfSharp.Drawing;
    using PdfSharp.Pdf;
    using Model.Interface.Empenho;
    using Model.Interface.Reserva;
    using Model.ValueObject.Service.Siafem.Empenho;
    using Model.ValueObject.Service.Siafem.Reserva;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Model.ValueObject.Service.Siafem.LiquidacaoDespesa;
    using Model.Extension;
    using Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao;
    using Microsoft.Reporting.WebForms;
    using System.Web;
    using PdfSharp.Pdf.IO;
    using Model.ValueObject.Service.Siafem.Movimentacao;

    public static class HelperReport
    {
        private const string strTitloNrDocumentoGerador = "Nº do Documento Gerador.";
        private const string strTituloNomeReduzidoCredor = "Nome Reduzido do Credor.";
        private const string strTituloCpfCnpjCredor = "CPF/CNPJ do Credor.";
        private const string strTituloDataVencimento = "Vencimento.";
        private const string strTituloValor = "Valor.";
        private const string strTituloNumeroPd = "Nº da PD.";
        private const string strTituloMensagemErro = "Mensagem de Erro.";

        //public static readonly dynamic PDF = new { FileType="PDF", ContentType= "application/pdf" };

        //public static readonly string Excel = "Excel";

        static XFont fonteNegrito = new XFont("Calibri", 10, XFontStyle.Bold);
        static XFont fonteNormal = new XFont("Calibri", 10, XFontStyle.Regular);

        public static FileStreamResult GerarPdfReserva(ConsultaNr consulta, string tipo, IReserva reserva)
        {
            var fileName = consulta.NumeroNR + ".pdf";

            string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

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
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio = HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold), XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            tipo += !string.IsNullOrEmpty(consulta.NumOC) ? " Pregão Eletrônico" : "";

            linha += 32;
            textFormatter.DrawString("Nota de " + tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black, new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha + 1, page.Width - 20, 0.1));


            //Data Emissão
            linha += 30;
            textFormatter.DrawString("Data da Emissão:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data da Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataEmissao, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Usuário
            linha += pularLinha;
            textFormatter.DrawString("Usuário:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Usuário:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Usuario, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data de Lançamento
            linha += (pularLinha * 2);
            textFormatter.DrawString("Data de Lançamento:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data de Lançamento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Unidade Gestora
            linha += (pularLinha * 2);
            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestorao:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.UnidadeGestora + " " + consulta.DescricaoUG, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão
            linha += (pularLinha);
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DescricaoGestao, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Número NR
            linha += (pularLinha * 2);
            textFormatter.DrawString("Número NR:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Número NR:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumeroNR, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Evento
            linha += (pularLinha * 2);
            textFormatter.DrawString("Evento:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Evento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Evento.ToString(), fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Processo
            linha += (pularLinha);
            textFormatter.DrawString("Processo:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Processo:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumeroProcesso, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Número OC
            linha += (pularLinha);
            textFormatter.DrawString("Número OC:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Número OC:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumOC, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Ptres
            linha += (pularLinha);
            textFormatter.DrawString("Ptres:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Ptres:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Ptres.ToString(), fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val = 0;
            espaco = 25;

            //UO
            linha += (pularLinha * 2);
            textFormatter.DrawString("UO", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("UO", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Programa de Trabalho", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Programa de Trabalho", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Fonte de Recurso", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Fonte de Recurso", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Natureza de Despesa", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Natureza de Despesa", fonteNegrito).Width + espaco;
            textFormatter.DrawString("UGR", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Plano Interno", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Plano Interno", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val = 0;

            linha += (pularLinha);
            textFormatter.DrawString(consulta.Uo.ToString(), fonteNormal, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("UO", fonteNormal).Width + espaco;
            textFormatter.DrawString(consulta.Pt.ToString(), fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Programa de Trabalho", fonteNegrito).Width + espaco;
            textFormatter.DrawString(consulta.Fonte, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Fonte de Recurso", fonteNegrito).Width + espaco;
            textFormatter.DrawString(consulta.Despesa, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Natureza de Despesa", fonteNegrito).Width +
                   espaco;
            textFormatter.DrawString(consulta.UGR.ToString(), fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
            textFormatter.DrawString(consulta.PlanoInterno, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Plano Interno", fonteNegrito).Width + espaco;
            textFormatter.DrawString(consulta.Valor, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 3);
            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
            textFormatter.DrawString("Cronograma", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Cronograma", fonteNegrito).Width * 2;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio + 217, linha + 11.8, val, 0.1));


            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            linha += (pularLinha * 2);
            textFormatter.DrawString("Mês", fonteNegrito, XBrushes.Black, new XRect(inicio + 217, linha, page.Width - 60, page.Height - 60));

            val -= graphics.MeasureString("Valor", fonteNegrito).Width;
            textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + 217 + val, linha, page.Width - 60, page.Height - 60));

            List<MesValor> mesvList = new List<MesValor>();

            for (int index = 1; index <= 12; index++)
            {
                var mes = new MesValor
                {
                    Mes =
                        Convert.ToString(
                            consulta.GetType()
                                .GetProperties()
                                .ToList()
                                .First(x => x.Name == "Mes" + index.ToString("D2"))
                                .GetValue(consulta)),
                    Valor =
                        Convert.ToString(
                            consulta.GetType()
                                .GetProperties()
                                .ToList()
                                .First(x => x.Name == "Valor" + index.ToString("D2"))
                                .GetValue(consulta))
                };
                mesvList.Add(mes);
            }
            mesvList.Where(x => x.Mes == "").ToList().ForEach(x => x.Mes = "13");
            foreach (var mesValor in mesvList.OrderBy(x => x.Mes))
            {
                linha += (pularLinha);
                textFormatter.DrawString(mesValor.Mes == "13" ? "" : mesValor.Mes,
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + 217, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(mesValor.Valor, fonteNormal, XBrushes.Black,
                    new XRect(inicio + 217 + val, linha, page.Width - 60, page.Height - 60));
            }

            var text = "";

            linha += (pularLinha * 2);
            textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao1, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao2, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao3, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);

            linha += (pularLinha * 3);
            textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consulta.Lancadopor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.HoraLancamento, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                   " e hora " + DateTime.Now.ToShortTimeString();

            linha += (pularLinha * 9);
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            linha += (pularLinha);
            textFormatter.DrawString("nº FCO:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            text = reserva.NumProdesp ?? "";

            val = graphics.MeasureString("nº FCO:", fonteNegrito).Width + 2;
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);
            stream.Position = 0;

            var fileDownloadName = $"{consulta.NumeroNR}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;
        }

        public static FileStreamResult GerarPdfLiquidacaoDespesa(ConsultaNL consulta, string tipo, ILiquidacaoDespesa subEmpenho)
        {

            #region Configuraçao


            var fileName = consulta.NumeroNL + ".pdf";
            string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

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
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio =
                HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Nota de " + tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1

            string cenarioTipo = string.Empty;

            switch (subEmpenho.CenarioSiafemSiafisico)
            {
                case 1:
                    cenarioTipo = "SIAFISICO";
                    break;
                case 2:
                    cenarioTipo = "NLPREGAO";
                    break;
                case 3:
                    cenarioTipo = "NLBEC";
                    break;
                case 4:
                    cenarioTipo = "NLCONTRATO";
                    break;
                case 5:
                    cenarioTipo = "NLEMLIQ";
                    break;
                case 6:
                    cenarioTipo = "SIAFEM";
                    break;
                case 7:
                    cenarioTipo = "NLOBRAS";
                    break;
                case 8:
                    cenarioTipo = "NLCTOBRAS";
                    break;
                case 9:
                    cenarioTipo = "";
                    break;
                case 10:
                    cenarioTipo = "";
                    break;
                case 11:
                    cenarioTipo = "";
                    break;
                default:
                    cenarioTipo = "";
                    break;
            }


            //Nº do Documento
            linha += 30;
            if (tipo == "Anulação de Subempenho")
            {
                textFormatter.DrawString("Nº da NL de Apropriação:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            }
            else
            {
                textFormatter.DrawString("Nº da NL de Apropriação:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            }

            val = graphics.MeasureString("Nº da NL de Apropriação:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumeroNL, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Data da Emissão
            linha += pularLinha;
            textFormatter.DrawString("Data da Emissão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data da Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataEmissao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("", fonteNegrito).Width + 220;
            textFormatter.DrawString(cenarioTipo, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Unidade Gestora
            linha += pularLinha;
            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.UG, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Gestão
            val +=
                graphics.MeasureString("Unidade Gestora:" + consulta.UG, fonteNegrito)
                    .Width + espaco;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Gestao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



            #endregion

            #region Blobo Nota de Lançamento – NL (SIAFEM) 


            if ((subEmpenho.CenarioSiafemSiafisico == 6 || subEmpenho.CenarioSiafemSiafisico == 7 || subEmpenho.CenarioSiafemSiafisico == 8) && subEmpenho.TransmitirSiafem)
            {
                linha += pularLinha * 2;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;

                linha += pularLinha;
                textFormatter.DrawString("CNPJ / CPF / UG Favorecido:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("CNPJ / CPF / UG Favorecido:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.CgcCpf ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Evento1 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito,
                    XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.InscEvento1 ?? string.Empty, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Classificacao1 ?? string.Empty, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Fonte1 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Reclassificação Despesa
                val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Rec/Desp", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.RecDesp1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));



                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Valor1 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));

                if (!string.IsNullOrWhiteSpace(consulta.Evento2))
                {
                    linha += pularLinha;

                    val = 0;
                    //Unidade de Medida
                    //textFormatter.DrawString("Evento", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Evento2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Item Serviço
                    val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Inscrição do Evento", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.InscEvento2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Quantidade do Item
                    val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Classificação", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Classificacao2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Valor Unitario
                    val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Fonte", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Fonte2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Reclassificação Despesa
                    val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Rec/Desp", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.RecDesp1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Preço Total
                    val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Valor", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Valor2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
                }
            }

            #endregion

            #region Liquidação de Medição de Obras – NLObras

            //if (subEmpenho.CenarioSiafemSiafisico == 7)
            //{
            //    linha += pularLinha;
            //    textFormatter.DrawString("Valor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Valor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Valor1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //}

            #endregion

            #region Contrato de Obras - NLCTObras 

            //if (subEmpenho.CenarioSiafemSiafisico == 8)
            //{
            //    linha += pularLinha;
            //    textFormatter.DrawString("CNPJ / CPF / UG Credor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("CNPJ / CPF / UG Credor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.CgcCpf, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Gestão Credor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Gestão Credor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.GestaoFavorecido, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Valor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Valor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Valor1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //}

            #endregion

            #region Liquidação de Empenho Tradicional – NL(SIAFISICO)

            if ((subEmpenho.CenarioSiafemSiafisico == 1 || subEmpenho.CenarioSiafemSiafisico == 2 || subEmpenho.CenarioSiafemSiafisico == 4 || subEmpenho.CenarioSiafemSiafisico == 5 || subEmpenho.CenarioSiafemSiafisico == 3) && subEmpenho.TransmitirSiafisico)
            {
                //linha += pularLinha;
                //textFormatter.DrawString("Evento:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                //val = graphics.MeasureString("Evento:", fonteCalibri10).Width + 2;
                //textFormatter.DrawString(consulta.Evento1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                linha += pularLinha * 2;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;

                linha += pularLinha;
                textFormatter.DrawString("CNPJ / CPF / UG Favorecido:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("CNPJ / CPF / UG Favorecido:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.CgcCpf ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Evento1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.InscEvento1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Classificacao1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Fonte1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Reclassificação Despesa
                val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Rec/Desp", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.RecDesp1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Valor1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));

                if (!string.IsNullOrWhiteSpace(consulta.Evento2))
                {
                    linha += pularLinha;

                    val = 0;
                    //Unidade de Medida
                    //textFormatter.DrawString("Evento", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Evento2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Item Serviço
                    val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Inscrição do Evento", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.InscEvento2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Quantidade do Item
                    val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Classificação", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Classificacao2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Valor Unitario
                    val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Fonte", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Fonte2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Reclassificação Despesa
                    val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Rec/Desp", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.RecDesp2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Preço Total
                    val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Valor", fonteCalibri10, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consulta.Valor2 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
                }

            }

            #endregion

            #region Liquidação de Pregão Eletrônico – NLPREGAO (SIAFISICO)

            if (subEmpenho.CenarioSiafemSiafisico == 10 && subEmpenho.TransmitirSiafisico)
            {
                linha += pularLinha * 2;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;

                linha += pularLinha;
                textFormatter.DrawString("CNPJ / CPF / UG Credor:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("CNPJ / CPF / UG Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.CgcCpf ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Evento1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.InscEvento1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Classificacao1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Fonte1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Reclassificação Despesa
                val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Rec/Desp", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.RecDesp1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Valor1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));

            }

            #endregion

            #region Nota de Lançamento BEC – NLBEC (SIAFISICO)

            if (subEmpenho.CenarioSiafemSiafisico == 11 && subEmpenho.TransmitirSiafisico)
            {
                linha += pularLinha * 2;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;


                linha += pularLinha;
                textFormatter.DrawString("CNPJ / CPF / UG Credor:", fonteNegrito,
                    XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val =
                    graphics.MeasureString("CNPJ / CPF / UG Credor:", fonteNegrito).Width +
                    2;
                textFormatter.DrawString(consulta.CgcCpf ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                linha += pularLinha;
                textFormatter.DrawString("Gestão Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.GestaoFavorecido ?? string.Empty, fonteNormal,
                    XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Evento1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.InscEvento1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Classificacao1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Fonte1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Reclassificação Despesa
                val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Rec/Desp", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.RecDesp1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Valor1 ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));


            }

            #endregion

            #region Liquidação de Pregão Eletrônico – NLPREGAO (SIAFISICO)
            //if (subEmpenho.CenarioSiafemSiafisico == 2 && subEmpenho.TransmitirSiafisico)
            //{
            //    linha += pularLinha;
            //    textFormatter.DrawString("CNPJ / CPF / UG Credor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("CNPJ / CPF / UG Credor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.CgcCpf, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Gestão Credor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Gestão Credor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.GestaoFavorecido, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Evento:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Evento:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Evento1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Valor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Valor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Valor1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            //}
            #endregion

            #region Nota Lançamento de Contrato - NLCONTRATO (SIAFISICO) 

            //if (subEmpenho.CenarioSiafemSiafisico == 4 && subEmpenho.TransmitirSiafisico)
            //{
            //    linha += pularLinha;
            //    textFormatter.DrawString("CNPJ / CPF / UG Credor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("CNPJ / CPF / UG Credor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.CgcCpf, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Gestão Credor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Gestão Credor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.GestaoFavorecido, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Evento:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Evento:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Evento1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //    linha += pularLinha;
            //    textFormatter.DrawString("Valor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Valor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Valor1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //}

            #endregion

            #region Nota de Lançamento de Liquidação de Empenho – NLEMLIQ (SIAFISICO)
            //if (subEmpenho.CenarioSiafemSiafisico == 5 && subEmpenho.TransmitirSiafisico)
            //{
            //    linha += pularLinha;
            //    textFormatter.DrawString("Valor:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //    val = graphics.MeasureString("Valor:", fonteCalibri10).Width + 2;
            //    textFormatter.DrawString(consulta.Valor1, fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //}

            #endregion

            #region Rodapé

            linha += (pularLinha * 5);
            textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao1 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao2 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao3 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);

            linha += (pularLinha * 3);
            textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consulta.Lancadopor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));

            textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento ?? string.Empty, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.LancadoHora ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                       " e hora " + DateTime.Now.ToShortTimeString();

            linha += (pularLinha * 2);
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            linha += (pularLinha);
            textFormatter.DrawString("nº FCO:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            text = subEmpenho.NumeroProdesp ?? "";

            val = graphics.MeasureString("nº FCO:", fonteNegrito).Width + 2;
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            #endregion

            #region Salvar

            //document.Save(file);

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);
            stream.Position = 0;

            var fileDownloadName = $"{consulta.NumeroNL}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;

            #endregion
        }

        public static FileStreamResult GerarPdfEmpenho(ConsultaNe consulta, string tipo, IEmpenho empenho)
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

            string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

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
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio =
                HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Nota de " + tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1


            //Nº do Documento
            linha += 30;
            textFormatter.DrawString("Nº do Documento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº do Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumeroNe, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data Emissão
            val += graphics.MeasureString("Nº do Documento:", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Data da Emissão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Data da Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataEmissao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            ////Nº do Documento Prodesp
            //linha += (pularLinha);
            //textFormatter.DrawString("Nº do Empenho Prodesp:", fonteCalibri10, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //val = graphics.MeasureString("Nº do Empenho Prodesp:", fonteCalibri10).Width + 2;
            //textFormatter.DrawString(empenho.NumeroEmpenhoProdesp ?? "", fonteCalibri10Regular, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



            //Gestão
            linha += pularLinha;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Gestao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //UG
            linha += pularLinha;
            textFormatter.DrawString("UG:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("UG:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.UnidadeGestora, fonteNormal,
                XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Nº Processo
            linha += (pularLinha);
            textFormatter.DrawString("Nº Processo:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº Processo:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumeroProcesso, fonteNormal,
                XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco2

            //Credor
            linha += (pularLinha * 2);
            textFormatter.DrawString("Credor:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Credor:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.GestaoCredor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            //CPF / CNPJ
            textFormatter.DrawString("CPF / CNPJ:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("CPF / CNPJ:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.CgcCpf, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Origem do Material
            linha += (pularLinha * 2);
            textFormatter.DrawString("Origem do Material:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Origem do Material:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.OrigemMaterial, fonteNormal,
                XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Local Entrega
            linha += (pularLinha);
            textFormatter.DrawString("Local de Entrega:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Local de Entrega:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Local, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Origem do Material
            linha += (pularLinha);
            textFormatter.DrawString("Data de Entrega:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data de Entrega:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataEntrega, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco3

            //Evento
            linha += (pularLinha * 2);
            textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
            textFormatter.DrawString(consulta.Evento, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val = 0;
            //UO
            linha += (pularLinha * 2);
            textFormatter.DrawString("UO", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Uo, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            //PT
            val += graphics.MeasureString("UO", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Programa de Trabalho", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Pt, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //Fonte
            val += graphics.MeasureString("Programa de Trabalho", fonteNegrito).Width +
                   espaco;
            textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Fonte, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            //Natureza Despesa
            val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Nat. Desp.", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Despesa, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //UGR
            val += graphics.MeasureString("Nat. Desp.", fonteNegrito).Width + espaco;
            textFormatter.DrawString("UGR", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.Ugo, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //Plano Interno
            val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
            textFormatter.DrawString("PI", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consulta.PlanoInterno, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            #endregion

            #region Bloco3

            //ReferenciaLegal
            linha += (pularLinha * 3);
            textFormatter.DrawString("Refer. Legal:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Refer. Legal:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.ReferenciaLegal, fonteNormal,
                XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //EmpenhoOriginal
            textFormatter.DrawString("Empenho Orig:", fonteNegrito, XBrushes.Black,
                new XRect((page.Width * 0.45), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Empenho Orig:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.EmpenhoOriginal, fonteNormal,
                XBrushes.Black, new XRect((page.Width * 0.45) + val, linha, page.Width - 60, page.Height - 60));

            //Licitação
            linha += (pularLinha);
            textFormatter.DrawString("Licitação:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Licitação:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Licitacao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Modalidade
            textFormatter.DrawString("Modalidade:", fonteNegrito, XBrushes.Black,
                new XRect((page.Width * 0.45), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Modalidade:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Modalidade, fonteNormal, XBrushes.Black,
                new XRect((page.Width * 0.45) + val, linha, page.Width - 60, page.Height - 60));

            //Tipo Empenho
            linha += (pularLinha);
            textFormatter.DrawString("Tipo Empenho:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Tipo Empenho:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.TipoEmpenho, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Nº do Contrato
            textFormatter.DrawString("Acordo:", fonteNegrito, XBrushes.Black, new XRect((page.Width * 0.45), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Acordo:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Acordo, fonteNormal, XBrushes.Black, new XRect((page.Width * 0.45) + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco4




            linha += (pularLinha * 2);
            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
            textFormatter.DrawString("Cronograma de Desembolso Previsto", fonteNegrito,
                XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val =
                graphics.MeasureString("Cronograma de Desembolso Previsto", fonteNegrito)
                    .Width / 2;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White,
                new XRect(inicio - 20, linha + 11.8, page.Width - 20, 0.1));



            linha += (pularLinha * 2);
            textFormatter.DrawString("Janeiro", fonteNegrito, XBrushes.Black,
                new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "01")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Fevereiro", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "02")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Março", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "03")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 2);
            textFormatter.DrawString("Abril", fonteNegrito, XBrushes.Black,
                new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "04")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Maio", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "05")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Junho", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "06")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 2);
            textFormatter.DrawString("Julho", fonteNegrito, XBrushes.Black,
                new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "07")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Agosto", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "08")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Setembro", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "09")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            linha += (pularLinha * 2);
            textFormatter.DrawString("Outubro", fonteNegrito, XBrushes.Black,
                new XRect(inicio - val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "10")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio - val, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Novembro", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "11")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Dezembro", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(meses.FirstOrDefault(x => x.Mes == "12")?.Valor ?? "",
                fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;

            #endregion

            #region Bloco5

            linha += (pularLinha);
            if (!string.IsNullOrEmpty(empenho.NumeroEmpenhoSiafem))
            {
                //Obra
                textFormatter.DrawString("Nº da Obra:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Nº da Obra:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.IdentificadorObra, fonteNormal,
                    XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Unidade de Medida:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.unidade, fonteNormal,
                    XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Preço Unitario
                val = graphics.MeasureString("Unidade de Medida:", fonteNegrito).Width +
                      espaco;
                textFormatter.DrawString("Preço Unitario:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.valorunitario,
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Preço Total
                val += graphics.MeasureString("Preço Unitario:", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Preço Total:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.precototal, fonteNormal,
                    XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Descrição Item
                linha += (pularLinha * 2);
                textFormatter.DrawString("Descrição:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                var ListDesc = ListaString(77, consulta.Repete.tabela.descricao, 10);

                foreach (var item in ListDesc.Where(x => x != ""))
                {
                    textFormatter.DrawString(item, fonteNormal, XBrushes.Black,
                        new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));
                    linha += (pularLinha);
                }

                linha += pularLinha;

            }

            #endregion

            #region Bloco6

            linha += (pularLinha);
            if (!string.IsNullOrEmpty(empenho.NumeroEmpenhoSiafisico))
            {
                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Item Seq.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.sequencia, fonteNormal,
                    XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Item Seq.", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Item Serviço", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.material, fonteNormal,
                    XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Unidade Fornecimento
                val += graphics.MeasureString("Item Serviço.", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Unidade Forn.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.unidade, fonteNormal,
                    XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Unidade Forn.", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Quantidade do Item", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.qtdeitem, fonteNormal,
                    XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Quantidade do Item:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Preço Unitario", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.valorunitario, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Preço Total
                val += graphics.MeasureString("Preço Unitario", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Preço Total", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.precototal, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Descrição Item
                linha += (pularLinha * 2);
                textFormatter.DrawString("Descrição:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consulta.Repete.tabela.descricao, fonteNormal,
                    XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));
                if (!string.IsNullOrWhiteSpace(consulta.Repete.tabela.descricao1))
                {
                    linha += (pularLinha);
                    textFormatter.DrawString(consulta.Repete.tabela.descricao1,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));
                }
                if (!string.IsNullOrWhiteSpace(consulta.Repete.tabela.descricao2))
                {
                    linha += (pularLinha);
                    textFormatter.DrawString(consulta.Repete.tabela.descricao2,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));
                }
                if (!string.IsNullOrWhiteSpace(consulta.Repete.tabela.descricao3))
                {
                    linha += (pularLinha);
                    textFormatter.DrawString(consulta.Repete.tabela.descricao3,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));
                }

                linha += pularLinha * 6;
            }

            #endregion

            #region Bloco7


            //Total de Itens
            textFormatter.DrawString("Total de Itens:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Total de Itens:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Valor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            #endregion

            #region bloco8


            linha += (pularLinha * 4);
            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consulta.Lancadopor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.HoraLancamento, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                       " e hora " + DateTime.Now.ToShortTimeString();

            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, page.Height - 40, page.Width - 60, page.Height - 60));

            textFormatter.DrawString("Nº FCO:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, page.Height - 40 + pularLinha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº FCO:", fonteNegrito).Width + 2;
            textFormatter.DrawString(empenho.NumeroEmpenhoProdesp ?? "", fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, page.Height - 40 + pularLinha, page.Width - 60, page.Height - 60));

            textFormatter.DrawString("Contrato FCO:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, page.Height - 40 + (pularLinha * 2), page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Contrato FCO:", fonteNegrito).Width + 2;
            textFormatter.DrawString(empenho.NumeroContrato.Formatar("00 00 00000-0") ?? "", fonteNormal, XBrushes.Black, new XRect(inicio + val, page.Height - 40 + (pularLinha * 2), page.Width - 60, page.Height - 60));


            #endregion


            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);
            stream.Position = 0;

            var fileDownloadName = $"{consulta.NumeroNe}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;
        }

        public static FileStreamResult GerarPdfEmpenhoDireto(ConsultaPdfEmpenho consulta, string tipo, IEmpenho empenho)
        {
            int xPos = 45;
            int yPos = 790;
            int pularLinha = 13;
            var data = DateTime.Now;
            var texto = string.Empty;

            byte[] resultConvertido = Convert.FromBase64String(consulta.PdfBase64);

            using (Stream stream2 = new MemoryStream(resultConvertido))
            {

                PdfDocument document = PdfReader.Open(stream2);

                var page = document.Pages[document.PageCount - 1];

                page.Size = PageSize.A4;

                var graphics = XGraphics.FromPdfPage(page);
                var textFormatter = new XTextFormatter(graphics);

                texto = String.Format("Impresso através de consulta Web Service no SIAFEM na data {0} e hora {1}", data.ToShortDateString(), data.ToString("HH:mm"));
                textFormatter.DrawString(texto, fonteNormal, XBrushes.Black, new XRect(xPos, yPos, page.Width - 60, page.Height - 60));

                yPos += pularLinha;
                texto = String.Format("Nº. FCO: {0} Contrato FCO: {1}", (empenho.NumeroEmpenhoProdesp != null ? empenho.NumeroEmpenhoProdesp : string.Empty), empenho.NumeroContrato.Formatar("00 00 00000-4"));
                textFormatter.DrawString(texto, fonteNormal, XBrushes.Black, new XRect(xPos, yPos, page.Width - 60, page.Height - 60));

                //yPos += pularLinha;
                //texto = String.Format("", );
                //textFormatter.DrawString(texto, fonteNormal, XBrushes.Black, new XRect(xPos, yPos, page.Width - 60, page.Height - 60));
                MemoryStream stream = new MemoryStream { Position = 0 };

                document.Save(stream, false);
                stream.Position = 0;


                var fileDownloadName = $"{consulta.NumeroNE}.pdf";

                var fsr = new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = fileDownloadName
                };

                return fsr;
            }
        }

        public static FileStreamResult GerarPdfDesdobramento(IEnumerable<ConsultaDesdobramento> consulta, string tipo, Desdobramento desdobramento)
        {
            #region Configuraçao



            var fileName = $"{desdobramento.NumeroDocumento}.pdf";

            string file = HttpContext.Current.Server.MapPath($"~/Uploads/{fileName}");
            //string file = @"C:\Users\810235\Documents\TestePDF.pdf";

            FileInfo newFile = new FileInfo(file);

            if (newFile.Exists)
            {
                newFile.Delete();
            }

            int inicio = 30;
            int linha = 68;
            int pularLinha = 13;
            int numPage = 1;

            var document = new PdfDocument();
            var page = document.AddPage();
            page.Size = PageSize.A4;
            var graphics = XGraphics.FromPdfPage(page);
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio = HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");
            // var diretorio = @"C:\Projetos\DER - Integracao SIAFEM\Fontes\Desenvolvimento\main\G&P\sids.prodesp1\Source\Main\Sids.Prodesp.Test\Brasão_do_estado_de_São_Paulo.jpg";

            #endregion

            linha = CreatePageDesdobramento(consulta, tipo, desdobramento, graphics, diretorio, inicio, textFormatter, linha, page, pularLinha, espaco);

            #region Bloco3

            #region ISSQN
            if (desdobramento.DesdobramentoTipoId == 1)
            {

                foreach (var consultaDesdobramento in consulta.Where(x => !x.outCredor.Contains("TOTAL ISSQN")))
                {
                    linha += (pularLinha);
                    textFormatter.DrawString(consultaDesdobramento.outCredor, fonteNormal, XBrushes.Black,
                        new XRect(inicio + 8, linha, page.Width - 60, page.Height - 60));

                    val = graphics.MeasureString("Credor.", fonteNegrito).Width + espaco + 60;
                    textFormatter.DrawString(consultaDesdobramento.outValorDistribuicao, fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                    val += graphics.MeasureString("Valor Distribuição.", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(consultaDesdobramento.outBaseCalc, fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                    val += graphics.MeasureString("%Base Calc.", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(consultaDesdobramento.outValorBaseCalc, fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                    val += graphics.MeasureString("Valor Base Calc.", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(consultaDesdobramento.outAliq, fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                    val += graphics.MeasureString("Aliq.", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(consultaDesdobramento.outValor, fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                    if (linha >= page.Height - 100)
                    {

                        CreateFooter(textFormatter, inicio, page, numPage);
                        var newPage = CreatePage(document, PageOrientation.Portrait);
                        textFormatter = newPage.XTextFormatter;
                        graphics = newPage.Graphics;
                        numPage += 1;
                        linha = CreatePageDesdobramento(consulta, tipo, desdobramento, graphics, diretorio, inicio, textFormatter, 68, page, pularLinha, espaco);
                    }
                }


            }
            #endregion

            #region Outros
            if (desdobramento.DesdobramentoTipoId == 2)
            {

                foreach (var consultaDesdobramento in consulta.OrderBy(x => x.outDesdob))
                {
                    linha += (pularLinha);
                    textFormatter.DrawString(consultaDesdobramento.outDesdob,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio + 8, linha, page.Width - 60, page.Height - 60));

                    val = graphics.MeasureString("Desdobramento.", fonteNegrito).Width +
                          espaco + 5;
                    textFormatter.DrawString(consultaDesdobramento.outNomeReduzCred,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                    val +=
                        graphics.MeasureString("Nome Reduzido do Credor.", fonteNegrito)
                            .Width + espaco + 5;
                    textFormatter.DrawString(consultaDesdobramento.outValor,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                    val += graphics.MeasureString("Valor.", fonteNegrito).Width + espaco +
                           5;
                    textFormatter.DrawString(consultaDesdobramento.outTipoBloqueioDoc,
                        fonteNormal, XBrushes.Black,
                        new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                    if (linha >= page.Height - 50)
                    {
                        CreateFooter(textFormatter, inicio, page, numPage + 1);
                        var newPage = CreatePage(document, PageOrientation.Portrait);
                        textFormatter = newPage.XTextFormatter;
                        graphics = newPage.Graphics;
                        linha = CreatePageDesdobramento(consulta, tipo, desdobramento, graphics, diretorio, inicio, textFormatter, 68, page, pularLinha, espaco);
                    }
                }


            }
            #endregion

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));
            #endregion

            #region Bloco4

            #region ISSQN
            Regex regex = new Regex(@"\-");
            if (desdobramento.DesdobramentoTipoId == 1)
            {
                var outTotalISSQN = consulta.FirstOrDefault(x => x.outCredor.Contains("TOTAL ISSQN"));

                linha += (pularLinha);
                textFormatter.DrawString("Total ISSQN.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + 8, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Credor.", fonteNegrito).Width + espaco + 60;

                textFormatter.DrawString(outTotalISSQN.outValorDistribuicao, fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Valor Distribuição.", fonteNegrito).Width + espaco;
                val += graphics.MeasureString("%Base Calc.", fonteNegrito).Width + espaco;

                textFormatter.DrawString(outTotalISSQN.outValorBaseCalc, fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Valor Base Calc.", fonteNegrito).Width + espaco;

                val += graphics.MeasureString("Aliq.", fonteNegrito).Width + espaco;

                textFormatter.DrawString(outTotalISSQN.outValor, fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            }
            #endregion

            #region Outros

            if (desdobramento.DesdobramentoTipoId == 2)
            {
                linha += (pularLinha);
                textFormatter.DrawString("Total", fonteNegrito, XBrushes.Black, new XRect(inicio + 8, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Desdobramento.", fonteNegrito).Width + espaco + 5;

                val += graphics.MeasureString("Nome Reduzido do Credor.", fonteNegrito).Width + espaco + 5;

                textFormatter.DrawString(consulta.Sum(x => x.outValor == "" ? 0 : Convert.ToDecimal(x.outValor)).ToString(),
                    fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            }
            #endregion

            #endregion

            CreateFooter(textFormatter, inicio, page, numPage);

            #region Final



            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);

            //document.Save(file);
            stream.Position = 0;

            var fileDownloadName = $"{desdobramento.NumeroDocumento}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;
            #endregion
        }

        public static FileStreamResult GerarPdfReclassificacaoRetencao(ConsultaNL consultaNl, string tipo, ReclassificacaoRetencao reclassificacaoRetencao)
        {

            #region Configuraçao



            var fileName = $"{reclassificacaoRetencao.NumeroSiafem}.pdf";

            string file = HttpContext.Current.Server.MapPath($"~/Uploads/{fileName}");
            //string file = @"C:\Users\810235\Documents\TestePDF.pdf";

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
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio =
                HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");
            //var diretorio = @"C:\Projetos\DER - Integracao SIAFEM\Fontes\Desenvolvimento\main\G&P\sids.prodesp1\Source\Main\Sids.Prodesp.Test\Brasão_do_estado_de_São_Paulo.jpg";

            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Nota de " + tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1


            //Nº do Documento
            linha += 30;
            textFormatter.DrawString("Nº da NL de Reclassificação / Retenção:", fonteNegrito,
                XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val =
                graphics.MeasureString("Nº da NL de Reclassificação / Retenção:",
                    fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaNl.NumeroNL, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Data da Emissão
            linha += pularLinha;
            textFormatter.DrawString("Data da Emissão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data da Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaNl.DataEmissao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Unidade Gestora
            linha += pularLinha;
            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaNl.UG, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Gestão
            val += graphics.MeasureString("Unidade Gestora:" + consultaNl.UG, fonteNegrito)
                    .Width + espaco;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaNl.Gestao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            #endregion

            #region Blobo Nota de Lançamento – NL (SIAFEM) 


            if (reclassificacaoRetencao.ReclassificacaoRetencaoTipoId == 2)
            {



                //CNPJ / CPF /UG Credor
                linha += pularLinha;
                textFormatter.DrawString("CNPJ / CPF /UG Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("CNPJ / CPF /UG Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaNl.CgcCpf, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //CNPJ / CPF /UG Credor
                linha += pularLinha;
                textFormatter.DrawString("Gestão Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaNl.GestaoFavorecido, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                linha += pularLinha * 2;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;

                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Evento1, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito,
                    XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.InscEvento1, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Classificacao1, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Fonte1, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Valor1, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));

                if (!string.IsNullOrWhiteSpace(consultaNl.Evento2))
                {
                    linha += pularLinha;

                    val = 0;
                    //Unidade de Medida
                    //textFormatter.DrawString("Evento", fonteCalibri10Negrito, XBrushes.Black,
                    //  new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consultaNl.Evento2, fonteNormal,
                        XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Item Serviço
                    val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Inscrição do Evento", fonteCalibri10Negrito,
                    //  XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consultaNl.InscEvento2, fonteNormal,
                        XBrushes.Black,
                        new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Quantidade do Item
                    val +=
                        graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                        espaco;
                    //textFormatter.DrawString("Classificação", fonteCalibri10Negrito, XBrushes.Black,
                    //  new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consultaNl.Classificacao2, fonteNormal,
                        XBrushes.Black,
                        new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Valor Unitario
                    val += graphics.MeasureString("Classificação", fonteNegrito).Width +
                           espaco;
                    //textFormatter.DrawString("Fonte", fonteCalibri10Negrito, XBrushes.Black,
                    //  new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consultaNl.Fonte2, fonteNormal,
                        XBrushes.Black,
                        new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                    //Preço Total
                    val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                    //textFormatter.DrawString("Valor", fonteCalibri10Negrito, XBrushes.Black,
                    //  new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                    textFormatter.DrawString(consultaNl.Valor2, fonteNormal,
                        XBrushes.Black,
                        new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
                }
            }

            #endregion

            #region Pagamento de Obras sem OB – NLPGObras (Retenções)

            if (reclassificacaoRetencao.ReclassificacaoRetencaoTipoId == 3)
            {

                linha += pularLinha * 2;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;

                val = 0;
                //Unidade de Medida
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Evento1, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito,
                    XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.InscEvento1, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Quantidade do Item
                val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Classificacao1, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Fonte1, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaNl.Valor1, fonteNormal, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
            }

            #endregion

            #region Rodapé

            linha += (pularLinha * 5);
            textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            textFormatter.DrawString(consultaNl.Observacao1, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consultaNl.Observacao2, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consultaNl.Observacao3, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);

            linha += (pularLinha * 3);
            textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consultaNl.Lancadopor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaNl.DataLancamento, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaNl.LancadoHora, fonteNormal, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                       " e hora " + DateTime.Now.ToShortTimeString();

            linha += (pularLinha * 2);
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            linha += (pularLinha);
            textFormatter.DrawString("Nº do Contrato:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            text = reclassificacaoRetencao.NumeroContrato ?? "";

            val = graphics.MeasureString("Nº do Contrato:", fonteNegrito).Width + 2;
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            text = reclassificacaoRetencao.DocumentoTipo?.Descricao ?? "";

            linha += (pularLinha);
            textFormatter.DrawString($"N° do {text} Desdobrado:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            val = graphics.MeasureString($"N° do {text} Desdobrado:", fonteNegrito).Width + 2;
            textFormatter.DrawString(reclassificacaoRetencao.NumeroDocumento, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Final

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);

            //document.Save(file);
            stream.Position = 0;

            var fileDownloadName = $"{reclassificacaoRetencao.NumeroSiafem}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;

            #endregion
        }

        public static FileStreamResult GerarPdfProgramacaoDesembolso(ConsultaPd consultaPd, string tipo, IProgramacaoDesembolso pd)
        {
            #region Configuraçao



            var fileName = $"{pd.NumeroSiafem}.pdf";
            string file = HttpContext.Current.Server.MapPath($"~/Uploads/{fileName}");

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
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 15;


            var diretorio = HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");




            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString(tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1


            //Nº do Documento
            linha += 30;
            textFormatter.DrawString("Nº da Programação Desembolso:", fonteNegrito,
                XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val =
                graphics.MeasureString("Nº da Programação Desembolso:", fonteNegrito).Width +
                2;
            textFormatter.DrawString(consultaPd.NumPD, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Unidade Gestora
            linha += pularLinha;
            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.UG, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão
            val += graphics.MeasureString("Unida" + consultaPd.UG, fonteNegrito)
                       .Width + 10;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.Gestao, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Data da Emissão
            val += graphics.MeasureString("Gestão:" + consultaPd.Gestao, fonteNegrito)
                       .Width + 10;
            textFormatter.DrawString("Data da Emissão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Data da Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.DataEmissao, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data da Vencimento
            val += graphics.MeasureString("Data:" + consultaPd.DataEmissao, fonteNegrito)
                       .Width + 10;

            textFormatter.DrawString("Data da Vencimento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Data da Vencimento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.DataVencimento, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));



            #endregion

            #region Bloco2

            espaco = 25;
            //N° da Lista ou Anexo
            linha += pularLinha;
            textFormatter.DrawString("N° da Lista ou Anexo:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("N° da Lista ou Anexo:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.Lista, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Processo
            val += graphics.MeasureString("N° da Lista ou Anexo:Proce" + consultaPd.UG,
                           fonteNegrito)
                       .Width + espaco - 2;

            textFormatter.DrawString("Processo:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Processo:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.Processo, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            //Finalidade
            linha += pularLinha;
            textFormatter.DrawString("Finalidade:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            var val1 = graphics.MeasureString("Finalidade:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.Finalidade, fonteNormal, XBrushes.Black,
                new XRect(inicio + val1, linha, page.Width - 60, page.Height - 60));

            //NL de Referência
            textFormatter.DrawString("NL de Referência:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("NL de Referência:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.NLRef, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Bloco3
            if (!string.IsNullOrWhiteSpace(consultaPd.OB))
            {
                linha += (pularLinha * 2);
                textFormatter.DrawString("**PAGA**", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                //Data Pagamento
                val = graphics.MeasureString("**PAGA**" + consultaPd.UG, fonteNegrito)
                          .Width + espaco;

                textFormatter.DrawString("Data Pagamento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Data Pagamento:", fonteNegrito).Width + 2;
                textFormatter.DrawString("", fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Nº OB Paga
                val += graphics.MeasureString("Data Pagamento" + consultaPd.UG, fonteNegrito)
                           .Width + espaco - 2;

                textFormatter.DrawString("Nº da OB Paga:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Nº da OB Paga:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.OB, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            }
            #endregion

            #region Dados da Conta Pagadora

            linha += (pularLinha * 2);
            textFormatter.DrawString("Dados da Conta Pagadora", fonteNegrito,
                XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString("______________________", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //Unidade Gestora
            linha += (pularLinha * 2);

            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.UGPagadora, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão
            val +=
                graphics.MeasureString("Unidade Gestora:" + consultaPd.UGPagadora,
                    fonteNegrito).Width + espaco;


            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.GestaoPagadora, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Banco
            linha += (pularLinha);

            textFormatter.DrawString("Banco:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Banco:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.BancoPagador, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Agencia
            val +=
                graphics.MeasureString("Banco:" + consultaPd.BancoPagador, fonteNegrito)
                    .Width + espaco;


            textFormatter.DrawString("Agência:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Agência:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.AgenciaPagadora, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Agencia
            val +=
                graphics.MeasureString("Agência:" + consultaPd.AgenciaPagadora,
                    fonteNegrito).Width + espaco;


            textFormatter.DrawString("Conta:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Conta:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.ContaCorrentePagadora, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Dados da Conta Favorecido

            linha += (pularLinha * 2);
            textFormatter.DrawString("Dados da Conta Favorecido", fonteNegrito,
                XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString("______________________", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //Unidade Gestora
            linha += (pularLinha * 2);

            textFormatter.DrawString("CPF/CNPJ Favorecido:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("CPF/CNPJ Favorecido:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.CGC_CPF_UG_Favorecida, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão
            val = 408.3623046875;
            //+= graphics.MeasureString("CPF/CNPJ Favorecido:" + consultaPd.CGC_CPF_UG_Favorecida, fonteCalibri10).Width + espaco;


            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.GestaoFavorecido, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Banco
            linha += (pularLinha);

            textFormatter.DrawString("Banco:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Banco:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.BancoFavorecido, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Agencia
            val +=
                graphics.MeasureString("Banco:" + consultaPd.BancoFavorecido, fonteNegrito)
                    .Width + espaco;


            textFormatter.DrawString("Agência:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Agência:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.AgenciaFavorecido, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Agencia
            val +=
                graphics.MeasureString("Agência:" + consultaPd.AgenciaFavorecido,
                    fonteNegrito).Width + espaco;


            textFormatter.DrawString("Conta:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Conta:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.ContaCorrenteFavorecido, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Eventos
            linha += pularLinha * 2;
            var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;

            val = 0;
            //Evento
            linha += (pularLinha * 2);
            textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consultaPd.Evento1, fonteNormal, XBrushes.Black,
                new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

            //Inscrição do Evento
            val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Inscrição do Evento", fonteNegrito,
                XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consultaPd.InscricaoEvento1, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            //Classificação
            val += graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                   espaco;
            textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black,
                new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consultaPd.Classificacao1, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

            //Fonte
            val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consultaPd.Fonte1, fonteNormal, XBrushes.Black,
                new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


            //Valor
            val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black,
                new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
            textFormatter.DrawString(consultaPd.Valor1, fonteNormal, XBrushes.Black,
                new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));

            if (!string.IsNullOrWhiteSpace(consultaPd.Evento2))
            {
                linha += pularLinha;

                val = 0;
                //Unidade de Medida
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Evento2, fonteNormal,
                    XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito,
                    XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.InscricaoEvento2, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Quantidade do Item
                val +=
                    graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                    espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Classificacao2, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Fonte2, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Valor2, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
            }
            if (!string.IsNullOrWhiteSpace(consultaPd.Evento3))
            {
                linha += pularLinha;

                val = 0;
                //Unidade de Medida
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Evento3, fonteNormal,
                    XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Item Serviço
                val += graphics.MeasureString("Evento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito,
                    XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.InscricaoEvento3, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Quantidade do Item
                val +=
                    graphics.MeasureString("Inscrição do Evento:", fonteNegrito).Width +
                    espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Classificacao3, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));



                //Valor Unitario
                val += graphics.MeasureString("Classificação", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Fonte3, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));


                //Preço Total
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));
                textFormatter.DrawString(consultaPd.Valor3, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
            }
            #endregion

            #region Obra

            if (!string.IsNullOrWhiteSpace(consultaPd.NumObra))
            {
                espaco = 1;

                linha += pularLinha * 4;

                //Nº da Obra
                textFormatter.DrawString("Nº da Obra:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                val = graphics.MeasureString("Nº da Obra:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.NumObra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                espaco = 1;

                val += graphics.MeasureString("Nº da Obra" + consultaPd.NumObra, fonteNegrito).Width - 20;

                //Ano de Medição
                textFormatter.DrawString("Ano de Medição:", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                val += graphics.MeasureString("Ano de Medição:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.AnoMedicaoObra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Ano de Medição:" + consultaPd.AnoMedicaoObra, fonteNegrito).Width - 40;

                //Ano da Obra
                textFormatter.DrawString("Ano da Obra:", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                val += graphics.MeasureString("Ano da Obra:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.AnoObra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Ano da Obra:" + consultaPd.AnoObra, fonteNegrito).Width - 40;

                //Mês
                textFormatter.DrawString("Mês:", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                val += graphics.MeasureString("Mês:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.MesObra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Mês:" + consultaPd.MesObra, fonteNegrito).Width - 10;


                //Tipo de Obra
                textFormatter.DrawString("Tipo de Obra:", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                val += graphics.MeasureString("Tipo de Obra:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.TipoObra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Tipo da Obra:" + consultaPd.TipoObra, fonteNegrito).Width - 40;

                //UG da Obra
                textFormatter.DrawString("UG da Obra:", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
                val += graphics.MeasureString("UG da Obra:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.UGObra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                linha += pularLinha;

                //OC
                textFormatter.DrawString("OC:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                val = graphics.MeasureString("OC:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consultaPd.OfertaCompra, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            }


            #endregion

            #region Rodapé

            linha = Convert.ToInt32(page.Height) - 80;

            textFormatter.DrawString("Cadastrado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Cadastrado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consultaPd.Lancadopor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Data de Cadastramento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.4), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data de Cadastramento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.DataLanc, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.4) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Hora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Hora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.HoraLanc, fonteNormal, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = $"Impresso através de consulta Web Service ao SIAFEM na data {DateTime.Now.ToShortDateString()} e hora {DateTime.Now.ToShortTimeString()}";

            linha += (pularLinha * 2);
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            text = $"{pd.DocumentoTipo?.Id:D2} - {pd.DocumentoTipo?.Descricao}";

            linha += (pularLinha);
            textFormatter.DrawString("Tipo de Documento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            val = graphics.MeasureString("Tipo de Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Tipo de Documento:" + text, fonteNegrito).Width + 2;

            textFormatter.DrawString("Nº Documento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            val += graphics.MeasureString("Nº Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.NumeroDocumento ?? pd.NumeroSiafem, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Final

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);

            //document.Save(file);
            stream.Position = 0;

            var fileDownloadName = $"{pd.NumeroSiafem}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;

            #endregion
        }

        public static FileStreamResult GerarPdfProgramacaoDesembolsoAgrupamento(ConsultaPd consultaPd, string tipo, ProgramacaoDesembolso pd)
        {

            #region Configuraçao

            var fileName = $"{consultaPd.NumPD}.pdf";
            string file = HttpContext.Current.Server.MapPath($"~/Uploads/{fileName}");

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
            page.Orientation = PageOrientation.Landscape;
            var graphics = XGraphics.FromPdfPage(page);
            var textFormatter = new XTextFormatter(graphics);

            double xPos = 0;
            double espaco = 15;
            double larguraMaximaValor = CacularEspacamento(graphics, strTituloDataVencimento, strTituloDataVencimento, espaco);

            var diretorio = HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

            larguraMaximaValor = CalcularMaiorLarguraValor(graphics, pd.Agrupamentos, espaco, larguraMaximaValor);

            #endregion

            linha = CreatePageProgramacaoDesembolsoAgrupamento(consultaPd, tipo, pd, graphics, diretorio, inicio, textFormatter, linha, page, pularLinha, larguraMaximaValor, espaco);

            #region ListaAgrupamento
            var numPage = 1;
            var msg = string.Empty;
            var msgWidth = 40;
            foreach (var item in pd.Agrupamentos)
            {
                linha += (pularLinha);
                textFormatter.DrawString(item.NumeroDocumentoGerador, fonteNormal, XBrushes.Black, new XRect(inicio + 8, linha, page.Width, page.Height));

                xPos = CacularEspacamento(graphics, strTitloNrDocumentoGerador, item.NumeroDocumentoGerador, espaco);
                textFormatter.DrawString(item.NomeCredorReduzido, fonteNormal, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

                xPos += CacularEspacamento(graphics, strTituloNomeReduzidoCredor, item.NomeCredorReduzido, espaco);
                textFormatter.DrawString(item.NumeroCnpjcpfCredor, fonteNormal, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

                xPos += CacularEspacamento(graphics, strTituloCpfCnpjCredor, item.NumeroCnpjcpfCredor, espaco);
                var strDataVencimento = item.DataVencimento.ToShortDateString();
                textFormatter.DrawString(strDataVencimento, fonteNormal, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

                xPos += CacularEspacamento(graphics, strTituloDataVencimento, strDataVencimento, espaco);
                var stringValor = $"R${string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", (Convert.ToDecimal(item.Valor)).ToString("N2"))}";
                textFormatter.DrawString(stringValor, fonteNormal, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

                xPos += larguraMaximaValor;
                var strNumeroPd = item.NumeroSiafem ?? "";
                textFormatter.DrawString(strNumeroPd, fonteNormal, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

                xPos += CacularEspacamento(graphics, strTituloNumeroPd, "2018PD00000", espaco);
                msg = item.MensagemServicoSiafem ?? "";
                var qtdLinhas = Math.Ceiling(msg.Length / (float)msgWidth);
                var msgInicio = 0;
                for (int i = 0; i < qtdLinhas; i++)
                {
                    var msgLinha = msg.Substring(msgInicio, msgWidth);
                    textFormatter.DrawString(msgLinha, fonteNormal, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));
                    msgInicio += msgWidth;
                    msgWidth = msg.Substring(msgInicio).Length > msgWidth ? msgWidth : msg.Substring(msgInicio).Length;
                    if (qtdLinhas > 1)
                    {
                        linha += (pularLinha);
                    }
                }
                msgWidth = 40;

                if (linha >= page.Height - 80)
                {

                    CreateFooterAgrupamento(consultaPd, page, textFormatter, inicio, graphics, numPage);
                    var newPage = CreatePage(document, PageOrientation.Landscape);
                    textFormatter = newPage.XTextFormatter;
                    graphics = newPage.Graphics;
                    numPage += 1;
                    linha = 68;
                    linha = CreatePageProgramacaoDesembolsoAgrupamento(consultaPd, tipo, pd, graphics, diretorio, inicio, textFormatter, linha, page, pularLinha, larguraMaximaValor, espaco);
                }
            }

            #endregion

            CreateFooterAgrupamento(consultaPd, page, textFormatter, inicio, graphics, numPage);

            #region Final

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);

            stream.Position = 0;

            var fileDownloadName = $"{consultaPd.NumPD}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;

            #endregion
        }

        public static FileStreamResult GerarPdfMovimentacaoOrcamentariaNL(RespostaConsultaNL consulta, string nrReducaoSuplementacao, string tipo)
        {
            #region Configuraçao


            var fileName = consulta.NumeroNL + ".pdf";
            string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

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
            var textFormatter = new XTextFormatter(graphics);
            double val = 0;
            double espaco = 25;

            var diretorio =
                HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

            string tipoReduçãoSuplementacao = tipo == "Cancelamento" ? "Redução: " : "Suplementação: ";

            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Movimentação Orçamentária", new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1

            //Nº Documento
            linha += 30;
            textFormatter.DrawString("Nº Documento:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Nº Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.NumeroNL, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data Emissão
            linha += pularLinha;
            textFormatter.DrawString("Data Emissão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataEmissao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data Lançamento
            linha += pularLinha;
            textFormatter.DrawString("Data Lançamento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data Lançamento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            linha += pularLinha * 1;

            //Unidade Gestora
            linha += pularLinha;
            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.UG, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão
            linha += pularLinha;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Gestao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //UG Favorecida
            linha += pularLinha;
            textFormatter.DrawString("UG Favorecida:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("UG Favorecida:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.CgcCpf, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão Favorecido
            linha += pularLinha;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.GestaoFavorecido ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Blobo Nota de Lançamento – NL (SIAFEM) 

            linha += pularLinha;
            var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;
            val = 0;

            //Evento
            linha += (pularLinha * 2);
            textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));

            //Inscrição do Evento
            val += graphics.MeasureString("Evento", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Inscrição do Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

            //Rec/Desp
            val += graphics.MeasureString("Inscrição do Evento", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Rec/Desp", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

            //Classificação
            val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

            //Fonte
            val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

            //Valor
            val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));

            foreach (var item in consulta.ListaEventosNL)
            {
                linha += pularLinha;
                val = 0;

                //Evento
                textFormatter.DrawString(item.Evento ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Inscrição do Evento
                val += graphics.MeasureString("Evento", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.InscEvento ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Rec/Desp
                val += graphics.MeasureString("Inscrição do Evento", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.RecDesp ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Classificação
                val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Classificacao ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Fonte
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Fonte ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Valor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
            }

            #endregion

            #region Rodapé

            linha += (pularLinha * 5);
            textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao1 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao2 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Observacao3 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha * 7);
            textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consulta.Lancadopor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));

            textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataLancamento ?? string.Empty, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.LancadoHora ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                       " e hora " + DateTime.Now.ToShortTimeString();

            linha += (pularLinha * 2);
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            linha += (pularLinha);
            textFormatter.DrawString(tipoReduçãoSuplementacao, fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString(tipoReduçãoSuplementacao, fonteNegrito).Width + 2;
            textFormatter.DrawString(nrReducaoSuplementacao ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


            #endregion

            #region Salvar

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);
            stream.Position = 0;

            var fileDownloadName = $"{consulta.NumeroNL}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;

            #endregion
        }

        public static FileStreamResult GerarPdfMovimentacaoOrcamentariaNC(RespostaConsultaNC consulta, string nrReducao, string nrSuplementacao, string tipo, string gestaoEmitente)
        {
            #region Configuraçao


            var fileName = consulta.Numero + ".pdf";
            string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

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
            var textFormatter = new XTextFormatter(graphics);

            double val = 0;
            double espaco = 25;

            var diretorio =
                HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

            #endregion

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Movimentação Orçamentária", new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


            #endregion

            #region Bloco1

            //Nº Documento
            linha += 30;
            textFormatter.DrawString("Nº Documento:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Nº Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Numero, new XFont("Calibri", 12, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            linha += pularLinha * 1;

            //Unidade Gestora
            linha += pularLinha;
            textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.UGEmitente + " - " + consulta.UgEmitenteDesc, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão
            linha += pularLinha;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(gestaoEmitente + " - " + consulta.GestaoDesc ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //UG Favorecida
            linha += pularLinha;
            textFormatter.DrawString("UG Favorecida:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("UG Favorecida:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.UGFavorecido + " - " + consulta.UgFavorecidoDesc, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Gestão Favorecido
            linha += pularLinha;
            textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.GestaoFavorecido + " - " + consulta.GestaoFavorecidoDesc ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data Emissão
            linha += pularLinha;
            textFormatter.DrawString("Data Emissão:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data Emissão:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.DataEmissao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Data Lançamento
            linha += pularLinha;
            textFormatter.DrawString("Data Lançamento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data Lançamento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.LancadoPorEm ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Evento
            linha += pularLinha;
            textFormatter.DrawString("Evento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Evento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.Evento + " - " + consulta.EventoDesc ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Blobo Nota de Crédito – NC (SIAFEM) 

            linha += pularLinha;
            val = 0;

            //UO
            linha += (pularLinha * 2);
            textFormatter.DrawString("UO", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            //Programa Trabalho
            val += graphics.MeasureString("UO:", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Programa Trabalho", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Fonte Recurso
            val += graphics.MeasureString("Programa Trabalho", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Fonte Recurso", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Natureza Despesa
            val += graphics.MeasureString("Fonte Recurso", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Natureza Despesa", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //UGR
            val += graphics.MeasureString("Natureza Despesa", fonteNegrito).Width + espaco;
            textFormatter.DrawString("UGR", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Plano Interno
            val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Plano Interno", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            //Valor
            val += graphics.MeasureString("Plano Interno", fonteNegrito).Width + espaco;
            textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + val + espaco, linha, page.Width - 60, page.Height - 60));

            foreach (var item in consulta.ListaEventosNC)
            {
                linha += pularLinha;
                val = 0;

                //UO
                textFormatter.DrawString(item.UO ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Programa Trabalho
                val += graphics.MeasureString("UO", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.PT ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Fonte Recurso
                val += graphics.MeasureString("Programa Trabalho", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Fonte ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Natureza Despesa
                val += graphics.MeasureString("Fonte Recurso", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Despesa ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //UGR
                val += graphics.MeasureString("Natureza Despesa", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Ugr ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Plano Interno
                val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.PlanoInterno ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                //Valor
                val += graphics.MeasureString("Plano Interno", fonteNegrito).Width + espaco;
                textFormatter.DrawString(item.Valor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
            }

            #endregion

            #region Rodapé

            linha += (pularLinha * 5);
            textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha);
            textFormatter.DrawString(consulta.Obs1 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));
            linha += (pularLinha);
            textFormatter.DrawString(consulta.Obs2 ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            linha += (pularLinha * 7);
            textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consulta.LancadoPor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));

            textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.LancadoPorEm ?? string.Empty, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consulta.LancadoPorAs ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


            var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                       " e hora " + DateTime.Now.ToShortTimeString();

            linha += (pularLinha * 2);
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            linha += (pularLinha);
            textFormatter.DrawString("Redução:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Redução:", fonteNegrito).Width + 2;
            textFormatter.DrawString(nrReducao ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Suplementação:", fonteNegrito).Width + 40;
            textFormatter.DrawString("Suplementação:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            val += graphics.MeasureString("Suplementação:", fonteNegrito).Width + 2;
            textFormatter.DrawString(nrSuplementacao ?? string.Empty, fonteNormal, XBrushes.Black,
                new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Salvar

            var contentType = "application/pdf";

            MemoryStream stream = new MemoryStream { Position = 0 };
            document.Save(stream, false);
            stream.Position = 0;

            var fileDownloadName = $"{consulta.Numero}.pdf";

            var fsr = new FileStreamResult(stream, contentType)
            {
                FileDownloadName = fileDownloadName
            };

            return fsr;

            #endregion
        }

        public static FileStreamResult GerarPdfMovimentacaoAgrupamento(List<RespostaConsultaNL> listaNL, List<RespostaConsultaNC> listaNC, string gestaoEmitente, int numAgrupamento)
        {
            var document = new PdfDocument();

            foreach (var consulta in listaNL)
            {

                #region Configuraçao

                var fileName = "Agrupamento_" + numAgrupamento + ".pdf";
                string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

                FileInfo newFile = new FileInfo(file);

                if (newFile.Exists)
                {
                    newFile.Delete();
                }

                int inicio = 30;
                int linha = 68;
                int pularLinha = 13;

                var page = document.AddPage();
                page.Size = PageSize.A4;
                var graphics = XGraphics.FromPdfPage(page);
                var textFormatter = new XTextFormatter(graphics);
                double val = 0;
                double espaco = 25;

                var diretorio =
                    HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

                #endregion

                #region Cabeçario

                // Imagem.
                graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

                // Textos.
                textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                    XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

                linha += 32;
                textFormatter.DrawString("Movimentação Orçamentária", new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                    new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

                // Figuras geométricas.
                linha += 20;
                graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


                #endregion

                #region Bloco

                //Nº Documento
                linha += 30;
                textFormatter.DrawString("Nº Documento:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Nº Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.NumeroNL, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Data Emissão
                linha += pularLinha;
                textFormatter.DrawString("Data Emissão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data Emissão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.DataEmissao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Data Lançamento
                linha += pularLinha;
                textFormatter.DrawString("Data Lançamento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data Lançamento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.DataLancamento ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                linha += pularLinha * 1;

                //Unidade Gestora
                linha += pularLinha;
                textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.UG, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Gestão
                linha += pularLinha;
                textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.Gestao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //UG Favorecida
                linha += pularLinha;
                textFormatter.DrawString("UG Favorecida:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("UG Favorecida:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.CgcCpf, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Gestão Favorecido
                linha += pularLinha;
                textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.GestaoFavorecido ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

                #region Blobo Nota de Lançamento – NL (SIAFEM) 

                linha += pularLinha;
                var recuo = graphics.MeasureString("Unidade Ges:", fonteNegrito).Width;
                val = 0;

                //Evento
                linha += (pularLinha * 2);
                textFormatter.DrawString("Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo, linha, page.Width - 60, page.Height - 60));

                //Inscrição do Evento
                val += graphics.MeasureString("Evento", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Inscrição do Evento", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

                //Rec/Desp
                val += graphics.MeasureString("Inscrição do Evento", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Rec/Desp", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

                //Classificação
                val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Classificação", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

                //Fonte
                val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val, linha, page.Width - 60, page.Height - 60));

                //Valor
                val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha, page.Width - 60, page.Height - 60));

                var listaEvento = listaNL.Where(a => a.NumeroNL == consulta.NumeroNL).Select(b => b.ListaEventosNL)?.FirstOrDefault();

                foreach (var item in listaEvento)
                {
                    linha += pularLinha;
                    val = 0;

                    //Evento
                    textFormatter.DrawString(item.Evento ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Inscrição do Evento
                    val += graphics.MeasureString("Evento", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.InscEvento ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Rec/Desp
                    val += graphics.MeasureString("Inscrição do Evento", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.RecDesp ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Classificação
                    val += graphics.MeasureString("Rec/Desp", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Classificacao ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Fonte
                    val += graphics.MeasureString("Classificação", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Fonte ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Valor
                    val += graphics.MeasureString("Fonte", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Valor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + recuo + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
                }

                #endregion

                #region Rodapé

                linha += (pularLinha * 5);
                textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                linha += (pularLinha);
                textFormatter.DrawString(consulta.Observacao1 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                linha += (pularLinha);
                textFormatter.DrawString(consulta.Observacao2 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                linha += (pularLinha);
                textFormatter.DrawString(consulta.Observacao3 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                linha += (pularLinha * 7);
                textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
                textFormatter.DrawString(consulta.Lancadopor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));

                textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.DataLancamento ?? string.Empty, fonteNormal,
                    XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


                textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.LancadoHora ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


                var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                           " e hora " + DateTime.Now.ToShortTimeString();

                linha += (pularLinha * 2);
                textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));


                linha += (pularLinha);

                var tipoReduçãoSuplementacao = consulta.TipoDocumento == "Cancelamento" ? "Redução: " : "Suplementação: ";

                textFormatter.DrawString(tipoReduçãoSuplementacao, fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                var nrReducaoSuplementacao = consulta.TipoDocumento == "Cancelamento" ? consulta.NumeroReducao : consulta.NumeroSuplementacao;

                val = graphics.MeasureString(tipoReduçãoSuplementacao, fonteNegrito).Width + 2;
                textFormatter.DrawString(nrReducaoSuplementacao ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                #endregion

            }

            foreach (var consulta in listaNC)
            {

                #region Configuraçao


                var fileName = "Agrupamento_" + numAgrupamento + ".pdf";
                string file = HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);

                FileInfo newFile = new FileInfo(file);

                if (newFile.Exists)
                {
                    newFile.Delete();
                }

                int inicio = 30;
                int linha = 68;
                int pularLinha = 13;

                var page = document.AddPage();
                page.Size = PageSize.A4;
                var graphics = XGraphics.FromPdfPage(page);
                var textFormatter = new XTextFormatter(graphics);

                double val = 0;
                double espaco = 25;

                var diretorio =
                    HttpContext.Current.Server.MapPath("~/Content/images/Brasao_do_estado_de_Sao_Paulo.jpg");

                #endregion

                #region Cabeçario

                // Imagem.
                graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

                // Textos.
                textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                    XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

                linha += 32;
                textFormatter.DrawString("Movimentação Orçamentária", new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                    new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

                // Figuras geométricas.
                linha += 20;
                graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));


                #endregion

                #region Bloco

                //Nº Documento
                linha += 30;
                textFormatter.DrawString("Nº Documento:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Nº Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.Numero, new XFont("Calibri", 12, XFontStyle.Bold), XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                linha += pularLinha * 1;

                //Unidade Gestora
                linha += pularLinha;
                textFormatter.DrawString("Unidade Gestora:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Unidade Gestora:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.UGEmitente + " - " + consulta.UgEmitenteDesc, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Gestão
                linha += pularLinha;
                textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(gestaoEmitente + " - " + consulta.GestaoDesc ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //UG Favorecida
                linha += pularLinha;
                textFormatter.DrawString("UG Favorecida:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("UG Favorecida:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.UGFavorecido + " - " + consulta.UgFavorecidoDesc, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Gestão Favorecido
                linha += pularLinha;
                textFormatter.DrawString("Gestão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Gestão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.GestaoFavorecido + " - " + consulta.GestaoFavorecidoDesc ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Data Emissão
                linha += pularLinha;
                textFormatter.DrawString("Data Emissão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data Emissão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.DataEmissao ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Data Lançamento
                linha += pularLinha;
                textFormatter.DrawString("Data Lançamento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data Lançamento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.LancadoPorEm ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Evento
                linha += pularLinha;
                textFormatter.DrawString("Evento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Evento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.Evento + " - " + consulta.EventoDesc ?? DateTime.Now.ToShortDateString(), fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

                #region Blobo Nota de Crédito – NC (SIAFEM) 

                linha += pularLinha;
                val = 0;

                //UO
                linha += (pularLinha * 2);
                textFormatter.DrawString("UO", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                //Programa Trabalho
                val += graphics.MeasureString("UO:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Programa Trabalho", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Fonte Recurso
                val += graphics.MeasureString("Programa Trabalho", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Fonte Recurso", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Natureza Despesa
                val += graphics.MeasureString("Fonte Recurso", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Natureza Despesa", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //UGR
                val += graphics.MeasureString("Natureza Despesa", fonteNegrito).Width + espaco;
                textFormatter.DrawString("UGR", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Plano Interno
                val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Plano Interno", fonteNegrito, XBrushes.Black, new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //Valor
                val += graphics.MeasureString("Plano Interno", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor", fonteNegrito, XBrushes.Black, new XRect(inicio + val + espaco, linha, page.Width - 60, page.Height - 60));

                var listaEvento = listaNC.Where(a => a.Numero == consulta.Numero).Select(b => b.ListaEventosNC)?.FirstOrDefault();

                foreach (var item in listaEvento)
                {
                    linha += pularLinha;
                    val = 0;

                    //UO
                    textFormatter.DrawString(item.UO ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Programa Trabalho
                    val += graphics.MeasureString("UO", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.PT ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Fonte Recurso
                    val += graphics.MeasureString("Programa Trabalho", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Fonte ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Natureza Despesa
                    val += graphics.MeasureString("Fonte Recurso", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Despesa ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //UGR
                    val += graphics.MeasureString("Natureza Despesa", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Ugr ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Plano Interno
                    val += graphics.MeasureString("UGR", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.PlanoInterno ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val, linha + pularLinha, page.Width - 60, page.Height - 60));

                    //Valor
                    val += graphics.MeasureString("Plano Interno", fonteNegrito).Width + espaco;
                    textFormatter.DrawString(item.Valor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + espaco, linha + pularLinha, page.Width - 60, page.Height - 60));
                }

                #endregion

                #region Rodapé

                linha += (pularLinha * 5);
                textFormatter.DrawString("Observações", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                linha += (pularLinha);
                textFormatter.DrawString(consulta.Obs1 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));
                linha += (pularLinha);
                textFormatter.DrawString(consulta.Obs2 ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                linha += (pularLinha * 7);
                textFormatter.DrawString("Lançado Por:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Lançado Por:", fonteNegrito).Width;
                textFormatter.DrawString(consulta.LancadoPor ?? string.Empty, fonteNormal, XBrushes.Black, new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));

                textFormatter.DrawString("Em:", fonteNegrito, XBrushes.Black, new XRect(inicio + (page.Width * 0.6), linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Em:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.LancadoPorEm ?? string.Empty, fonteNormal,
                    XBrushes.Black, new XRect(inicio + (page.Width * 0.6) + val, linha, page.Width - 60, page.Height - 60));


                textFormatter.DrawString("Às:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Às:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.LancadoPorAs ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));


                var text = "Impresso através de consulta Web Service ao SIAFEM na data " + DateTime.Now.ToShortDateString() +
                           " e hora " + DateTime.Now.ToShortTimeString();

                linha += (pularLinha * 2);
                textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));


                linha += (pularLinha);
                textFormatter.DrawString("Redução:", fonteNegrito, XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Redução:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.NumeroReducao ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Suplementação:", fonteNegrito).Width + 40;
                textFormatter.DrawString("Suplementação:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Suplementação:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.NumeroSuplementacao ?? string.Empty, fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

            }

            #region Salvar

            var contentType = "application/pdf";

                MemoryStream stream = new MemoryStream { Position = 0 };
                document.Save(stream, false);
                stream.Position = 0;

                var fileDownloadName = $"Agrupamento_" + numAgrupamento + ".pdf";

                var fsr = new FileStreamResult(stream, contentType)
                {
                    FileDownloadName = fileDownloadName
                };

                return fsr;

                #endregion
        }

        /// <summary>
        /// Exportar o Relatorio RDLC para um formato Especifico.
        /// </summary>
        /// <param name="localReport">Relatório RDLC</param>
        /// <param name="FileType">Tipo do Arquivo (PDF, EXCEL...)</param>
        /// <param name="ContentType">ContentType do Arquivo "application/pdf", "application/vnd.ms-excel" </param>
        /// <returns></returns>
        public static FileResult ExportReport(LocalReport localReport, string FileType, string ContentType)
        {
            string reportType = FileType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>" + FileType + "</OutputFormat>" +
                //"  <PageWidth>8.5in</PageWidth>" +
                //"  <PageHeight>11in</PageHeight>" +
                //"  <MarginTop>0.5in</MarginTop>" +
                //"  <MarginLeft>1in</MarginLeft>" +
                //"  <MarginRight>1in</MarginRight>" +
                //"  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            return new FileContentResult(renderedBytes, ContentType);
        }

        #region Metodos Privados
        private static void CreateFooter(XTextFormatter textFormatter, int inicio, PdfPage page, int numPage)
        {
            #region Rodapé

            var text =
                $"Impresso através de consulta Web Service ao Prodesp na data {DateTime.Now.ToShortDateString()} e hora {DateTime.Now.ToShortTimeString()}";

            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio, page.Height - 40, page.Width - 60, page.Height - 60));

            text = $"Pagina {numPage}";
            textFormatter.Alignment = XParagraphAlignment.Right;

            textFormatter.DrawString(text, fonteNormal, XBrushes.Black, new XRect(inicio, page.Height - 40, page.Width - 60, page.Height - 60));

            textFormatter.Alignment = XParagraphAlignment.Left;

            //textFormatter.DrawString("Nº FCO:", fonteCalibri10, XBrushes.Black, new XRect(inicio, page.Height - 40 + pularLinha, page.Width - 60, page.Height - 60));

            #endregion
        }

        private static int CreatePageDesdobramento(IEnumerable<ConsultaDesdobramento> consulta, string tipo, Desdobramento desdobramento,
            XGraphics graphics, string diretorio, int inicio, XTextFormatter textFormatter, int linha, PdfPage page,
            int pularLinha, double espaco)
        {
            double val;

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold), XBrushes.Black,
                new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString("Nota de " + tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));

            #endregion

            #region Bloco1

            #region ISSQN

            if (desdobramento.DesdobramentoTipoId == 1)
            {
                #region Linha1

                //Nº do Documento
                linha += 30;
                textFormatter.DrawString("Documento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(desdobramento.DocumentoTipo.Descricao, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Nº do Documento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Nº do Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(desdobramento.NumeroDocumento, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Serviço:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Serviço:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outServico, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Data Emissão:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Data Emissão:", fonteNegrito).Width + 2;
                textFormatter.DrawString(desdobramento.DataTransmitidoProdesp.ToShortDateString(),
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outCredorDocumento, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Cód Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Cód Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outCodCredor, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Tipo Despesa:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Tipo Despesa:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outDespesa, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                linha += pularLinha;
                textFormatter.DrawString("Valor Documento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Valor Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outValorDocOriginal,
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion
            }

            #endregion

            #region Outros

            if (desdobramento.DesdobramentoTipoId == 2)
            {
                #region Linha1

                //Nº do Documento
                linha += 30;
                textFormatter.DrawString("Documento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(desdobramento.DocumentoTipo.Descricao, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("Documento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Nº do Documento:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Nº do Documento:", fonteNegrito).Width + 2;
                textFormatter.DrawString(desdobramento.NumeroDocumento, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("Nº do Documento:", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Tipo de Bloqueio:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Tipo de Bloqueio:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outTipoBloqueioDoc,
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

                #region Linha2

                //Nº do Documento
                linha += pularLinha;
                textFormatter.DrawString("Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outCredorDocumento,
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                //val = graphics.MeasureString("Documento:", fonteCalibri10).Width + 2;
                //val += graphics.MeasureString("Documento:", fonteCalibri10).Width + espaco;
                //textFormatter.DrawString("Tipo Desdobramento:", fonteCalibri10, XBrushes.Black,
                //    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                //val += graphics.MeasureString("Tipo Desdobramento:", fonteCalibri10).Width + 2;
                //textFormatter.DrawString(desdobramento.DocumentoTipoId.ToString(), fonteCalibri10Regular, XBrushes.Black,
                //    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

                #region Linha3

                //Nº do Documento
                linha += pularLinha;
                textFormatter.DrawString("Cód Credor:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Cód Credor:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outCodCredor, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

                #region Linha4

                //Nº do Documento
                linha += pularLinha;
                textFormatter.DrawString("Tipo Despesa:", fonteNegrito, XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Tipo Despesa:", fonteNegrito).Width + 2;
                textFormatter.DrawString(consulta.FirstOrDefault().outDespesa, fonteNormal,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion

                #region Linha5

                //Nº do Documento
                linha += pularLinha;
                textFormatter.DrawString("Valor do Documento Original:", fonteNegrito,
                    XBrushes.Black,
                    new XRect(inicio, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Valor do Documento Original:", fonteNegrito).Width +
                      2;
                textFormatter.DrawString(consulta.FirstOrDefault().outValorDocOriginal,
                    fonteNormal, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                #endregion
            }

            #endregion

            #endregion

            #region Bloco2

            #region ISSQN

            if (desdobramento.DesdobramentoTipoId == 1)
            {
                linha += (pularLinha * 2);
                textFormatter.DrawString("Credor.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + 8, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Credor.", fonteNegrito).Width + espaco + 60;
                textFormatter.DrawString("Valor Distribuição.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("Valor Distribuição.", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("%Base Calc.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("%Base Calc.", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor Base Calc.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("Valor Base Calc.", fonteNegrito).Width +
                       espaco;
                textFormatter.DrawString("Aliq.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));

                val += graphics.MeasureString("Aliq.", fonteNegrito).Width + espaco;
                textFormatter.DrawString("Valor.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            }

            #endregion

            #region Outros

            if (desdobramento.DesdobramentoTipoId == 2)
            {
                linha += (pularLinha * 2);
                textFormatter.DrawString("Desdobramento.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + 8, linha, page.Width - 60, page.Height - 60));

                val = graphics.MeasureString("Desdobramento.", fonteNegrito).Width + espaco +
                      5;
                textFormatter.DrawString("Nome Reduzido do Credor.", fonteNegrito,
                    XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("Nome Reduzido do Credor.", fonteNegrito).Width +
                       espaco + 5;
                textFormatter.DrawString("Valor.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));


                val += graphics.MeasureString("Valor.", fonteNegrito).Width + espaco + 5;
                textFormatter.DrawString("Tipo de Bloqueio.", fonteNegrito, XBrushes.Black,
                    new XRect(inicio + val, linha, page.Width - 60, page.Height - 60));
            }

            #endregion

            #endregion

            return linha;
        }

        private static CreatedPage CreatePage(PdfDocument document, PageOrientation orientation)
        {

            var page = document.AddPage();
            page.Size = PageSize.A4;
            page.Orientation = orientation;
            var graphics = XGraphics.FromPdfPage(page);
            return new CreatedPage { XTextFormatter = new XTextFormatter(graphics), Graphics = graphics };
        }

        private static List<string> ListaString(int dist, string texto, int qtd)
        {

            var resultado = new List<string>();
            if (texto == null)
            {
                for (var x = 0; x < qtd; x++)
                {
                    resultado.Add(null);
                }
                return resultado;
            }
            texto += texto.Length % 2 > 0 ? " " : "";
            texto = texto.Replace(";", "").Replace(";", "").Replace(";", "");

            for (var x = 0; x < qtd; x++)
            {
                var need = ((x + 1) * dist);
                var fim = texto.Length >= need ? dist : texto.Length - (0 + x * dist);

                if (texto.Length >= (0 + x * dist) && texto.Length > 0)
                    resultado.Add(texto.Substring(0 + x * dist, fim));
                else
                    resultado.Add("");
            }

            return resultado;
        }
        private static void CreateFooterAgrupamento(ConsultaPd consultaPd, PdfPage page, XTextFormatter textFormatter, int inicio, XGraphics graphics, int numPage)
        {
            int linha;
            double val;

            #region Rodapé

            linha = Convert.ToInt32(page.Height) - 60;

            textFormatter.DrawString("Cadastrado Por:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Height - 60, page.Height - 60));

            val = graphics.MeasureString("Cadastrado Por:", fonteNegrito).Width;
            textFormatter.DrawString(consultaPd.Lancadopor, fonteNormal, XBrushes.Black,
                new XRect(inicio + val + 5, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Data de Cadastramento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.4), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Data de Cadastramento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.DataLanc, fonteNormal,
                XBrushes.Black, new XRect(inicio + (page.Width * 0.4) + val, linha, page.Width - 60, page.Height - 60));


            textFormatter.DrawString("Hora:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715), linha, page.Width - 60, page.Height - 60));

            val = graphics.MeasureString("Hora:", fonteNegrito).Width + 2;
            textFormatter.DrawString(consultaPd.HoraLanc, fonteNormal, XBrushes.Black,
                new XRect(inicio + (page.Width * 0.715) + val, linha, page.Width - 60, page.Height - 60));

            var text = $"Pagina {numPage}";
            textFormatter.Alignment = XParagraphAlignment.Right;

            textFormatter.DrawString(text, fonteNormal, XBrushes.Black, new XRect(inicio, page.Height - 40, page.Width - 60, page.Height - 60));

            textFormatter.Alignment = XParagraphAlignment.Left;

            #endregion
        }

        private static int CreatePageProgramacaoDesembolsoAgrupamento(ConsultaPd consultaPd, string tipo, ProgramacaoDesembolso pd, XGraphics graphics,
            string diretorio, int inicio, XTextFormatter textFormatter, int linha, PdfPage page, int pularLinha, double larguraMaximaValor, double espaco)
        {
            double xPos;

            #region Cabeçario

            // Imagem.
            graphics.DrawImage(XImage.FromFile(diretorio), inicio - 20, 20, 60, 72);

            // Textos.
            //textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;
            textFormatter.DrawString("GOVERNO DO ESTADO DE SÃO PAULO", new XFont("Calibri", 12, XFontStyle.Bold),
                XBrushes.Black, new XRect(85, linha, page.Width - 60, page.Height - 60));

            linha += 32;
            textFormatter.DrawString(tipo, new XFont("Calibri", 18.5, XFontStyle.Regular), XBrushes.Black,
                new XRect(inicio - 20, linha, page.Width - 60, page.Height - 60));

            // Figuras geométricas.
            linha += 20;
            graphics.DrawRectangle(XPens.Silver, XBrushes.White, new XRect(inicio - 20, linha, page.Width - 20, 0.1));

            #endregion

            #region Bloco1

            //Tipo Programação Desembolso
            linha += 30;
            textFormatter.DrawString("Nº Agrupamento:", fonteNegrito,
                XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            xPos = graphics.MeasureString("Nº Agrupamento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.NumeroAgrupamento.ToString("D5"), fonteNormal,
                XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));
            linha += pularLinha;
            textFormatter.DrawString("Tipo Programação Desembolso:", fonteNegrito,
                XBrushes.Black, new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            xPos = graphics.MeasureString("Tipo Programação Desembolso:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.ProgramacaoDesembolsoTipo.Descricao, fonteNormal,
                XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));

            //Órgão Regional
            linha += pularLinha;
            textFormatter.DrawString("Órgão Regional:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));

            xPos = graphics.MeasureString("Órgão Regional:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.Regional.Descricao, fonteNormal, XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));

            //Tipo de Despesa
            xPos += graphics.MeasureString("Órgão Regional:" + pd.Regional.Descricao,
                           fonteNegrito)
                       .Width + espaco;
            textFormatter.DrawString("Tipo de Despesa:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));

            xPos += graphics.MeasureString("Tipo de Despesa:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.CodigoDespesa ?? "", fonteNormal, XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));


            //Data da Vencimento
            xPos += graphics.MeasureString("Tipo de Despesa:" + pd.CodigoDespesa ?? "", fonteNegrito)
                       .Width + espaco;
            textFormatter.DrawString("Data da Vencimento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));

            xPos += graphics.MeasureString("Data da Vencimento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.DataVencimento.ToShortDateString(), fonteNormal,
                XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));

            //Data da Vencimento
            var text = $"{pd.DocumentoTipo?.Id:D2} - {pd.DocumentoTipo?.Descricao}";

            linha += (pularLinha);
            textFormatter.DrawString("Tipo de Documento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio, linha, page.Width - 60, page.Height - 60));


            var val1 = graphics.MeasureString("Tipo de Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(text, fonteNormal, XBrushes.Black,
                new XRect(inicio + val1, linha, page.Width - 60, page.Height - 60));

            xPos -= graphics.MeasureString("Data da Vencimento:", fonteNegrito).Width + 2;

            textFormatter.DrawString("Nº Documento:", fonteNegrito, XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));


            xPos += graphics.MeasureString("Nº Documento:", fonteNegrito).Width + 2;
            textFormatter.DrawString(pd.NumeroDocumento ?? "", fonteNormal, XBrushes.Black,
                new XRect(inicio + xPos, linha, page.Width - 60, page.Height - 60));

            #endregion

            #region Agrupamento

            linha += (pularLinha * 2);
            textFormatter.DrawString(strTitloNrDocumentoGerador, fonteNegrito, XBrushes.Black, new XRect(inicio + 8, linha, page.Width, page.Height));

            xPos = CacularEspacamentoTitulo(graphics, strTitloNrDocumentoGerador, espaco);
            textFormatter.DrawString(strTituloNomeReduzidoCredor, fonteNegrito, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

            xPos += CacularEspacamentoTitulo(graphics, strTituloNomeReduzidoCredor, espaco);
            textFormatter.DrawString(strTituloCpfCnpjCredor, fonteNegrito, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

            xPos += CacularEspacamentoTitulo(graphics, strTituloCpfCnpjCredor, espaco);
            textFormatter.DrawString(strTituloDataVencimento, fonteNegrito, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

            xPos += CacularEspacamentoTitulo(graphics, strTituloDataVencimento, espaco);
            textFormatter.DrawString(strTituloValor, fonteNegrito, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

            xPos += larguraMaximaValor;
            textFormatter.DrawString(strTituloNumeroPd, fonteNegrito, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

            xPos += CacularEspacamento(graphics, fonteNegrito, strTituloNumeroPd, "2018PD00000", espaco);
            textFormatter.DrawString(strTituloMensagemErro, fonteNegrito, XBrushes.Black, new XRect(inicio + xPos, linha, page.Width, page.Height));

            #endregion

            return linha;
        }



        #endregion

        private static double CacularEspacamento(XGraphics graphics, string tituloAnterior, string valorAnterior, double espaco)
        {
            return CacularEspacamento(graphics, fonteNormal, tituloAnterior, valorAnterior, espaco);
        }
        private static double CacularEspacamentoTitulo(XGraphics graphics, string tituloAnterior, double espaco)
        {
            return CacularEspacamento(graphics, fonteNormal, tituloAnterior, tituloAnterior, espaco);
        }
        private static double CacularEspacamento(XGraphics graphics, XFont fonte, string tituloAnterior, string valorAnterior, double espaco)
        {
            var larguraTituloAnterior = graphics.MeasureString(tituloAnterior, fonte).Width + espaco;
            var larguraValorAtual = graphics.MeasureString(valorAnterior, fonte).Width + espaco;
            var val = larguraTituloAnterior > larguraValorAtual ? larguraTituloAnterior : larguraValorAtual;
            return val;
        }

        private static double CalcularMaiorLarguraValor(XGraphics graphics, IEnumerable<ProgramacaoDesembolsoAgrupamento> agrupamentos, double espaco, double larguraValor)
        {
            var valores = agrupamentos.Select(x => $"R${string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", Convert.ToDecimal(x.Valor).ToString("N2"))}");
            return CalcularMaiorLarguraValor(graphics, fonteNormal, valores, espaco, larguraValor);
        }
        private static double CalcularMaiorLarguraValor(XGraphics graphics, XFont fonte, IEnumerable<string> valores, double espaco, double larguraValor)
        {
            foreach (var valor in valores)
            {
                var largura = graphics.MeasureString(valor, fonteNormal).Width + espaco;
                larguraValor = larguraValor > largura ? larguraValor : largura;
            }

            return larguraValor;
        }

        #region Classes internas

        internal class mes
        {
            public string Mes { get; set; }
            public string Valor { get; set; }
        }

        internal class CreatedPage
        {
            public XGraphics Graphics { get; set; }
            public XTextFormatter XTextFormatter { get; set; }
        }

        internal class MesValor
        {
            public string Mes { get; set; }
            public string Valor { get; set; }
        }

        #endregion

    }
}