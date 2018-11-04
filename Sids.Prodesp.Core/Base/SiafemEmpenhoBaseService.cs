using Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho;
using System.Collections.Generic;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.Base;
using Sids.Prodesp.Model.Extension;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Entity.Empenho;
using Sids.Prodesp.Model.Entity.Configuracao;
using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.Interface.Empenho;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface;
using System.Globalization;

namespace Sids.Prodesp.Core.Base
{
    public class SiafemEmpenhoBaseService : BaseService
    {
        private readonly ICrudEmpenhoReforcoItem _item;

        public SiafemEmpenhoBaseService(ILogError l) : base(l)
        {
        }
        public SiafemEmpenhoBaseService(ILogError l, ICrudEmpenhoReforcoItem item) : base(l)
        {
            _item = item;
        }

        public static SFCODOC GerarSiafisicoCtDescricao(Documento document, EnumAcaoSiaf acao, EnumTipoOperacaoEmpenho operacaoEmpenho)
        {
            var siafdoc = new SFCODOC();

            string mensagem = string.Empty;

            var qtdConvertida = document.EmpenhoItem.QuantidadeMaterialServico.ConverterQuantidade();

            string vlrDesc;
            string vlrInt;
            //if (document.EmpenhoItem.ValorUnitario.ToString().Length < 3)
            //{
            //    vlrDesc = int.Parse(document.EmpenhoItem.ValorUnitario.ToString()).ToString("D2");
            //    vlrInt = "0";
            //}
            //else
            //{
            //    vlrDesc = document.EmpenhoItem.ValorUnitario.ToString().Substring(document.EmpenhoItem.ValorUnitario.ToString().Length - 2, 2);
            //    vlrInt = document.EmpenhoItem.ValorUnitario.ToString().Substring(0, document.EmpenhoItem.ValorUnitario.ToString().Length - 2);
            //}
            var arrVlr = document.EmpenhoItem.ValorUnitario.ToString(new CultureInfo("pt-BR")).Split(',');
            vlrInt = arrVlr[0] ?? "0";
            vlrDesc= arrVlr.Length == 1 ? "0" : arrVlr[1] ?? "0";

            string numeroCt = string.Empty;
            string ug = string.Empty;
            string gestao = string.Empty;
            int dist = 0;
            string valorUnitario = string.Empty;

            switch (operacaoEmpenho)
            {
                case EnumTipoOperacaoEmpenho.Cancelamento:
                    numeroCt = document.EmpenhoCancelamento.NumeroCT ?? string.Empty;
                    ug = document.EmpenhoCancelamento.CodigoUnidadeGestora;
                    gestao = document.EmpenhoCancelamento.CodigoGestao;
                    dist = 70;
                    break;
                case EnumTipoOperacaoEmpenho.Reforco:
                case EnumTipoOperacaoEmpenho.Empenho:
                default:
                    numeroCt = document.Empenho.NumeroCT ?? string.Empty;
                    ug = document.Empenho.CodigoUnidadeGestora;
                    gestao = document.Empenho.CodigoGestao;
                    dist = 50;
                    break;
            }

            var doc = new documento
            {
                UG = ug,
                Gestao = gestao,
                NumeroCT = numeroCt,
                Item = document.EmpenhoItem.CodigoItemServico,                
                Quantidade = qtdConvertida.Key,
                QuantidadeDec = qtdConvertida.Value,
                JustificativaPreco1 = acao == EnumAcaoSiaf.Alterar ? document.EmpenhoItem.DescricaoJustificativaPreco : ListaString(dist, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0],
                JustificativaPreco2 = acao == EnumAcaoSiaf.Alterar ? document.EmpenhoItem.DescricaoJustificativaPreco : ListaString(dist, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1],
            };

            switch (acao)
            {
                case EnumAcaoSiaf.Alterar:
                    doc.Seq = document.EmpenhoItem.SequenciaItem.ToString();
                    doc.ConfirmaPreco = "S";
                    doc.JustificativaItemNao = document.EmpenhoItem.DescricaoItemSiafem;
                    doc.UnidadeFornecimento = document.EmpenhoItem.CodigoUnidadeFornecimentoItem;
                    doc.ValorParteInteira = vlrInt.Replace(",", "");
                    doc.ValorParteDecimal = vlrDesc;

                    siafdoc.SiafisicoDocAltDescCT = new SiafisicoDocAltDescCT();
                    siafdoc.cdMsg = "SFCODocAltDescCT";
                    siafdoc.SiafisicoDocAltDescCT.documento = doc;
                    break;
                case EnumAcaoSiaf.Inserir:
                default:
                    doc.UnidadeForn = document.EmpenhoItem.CodigoUnidadeFornecimentoItem;
                    doc.ValorUnitario = vlrInt.Replace(",", "");
                    doc.ValorUnitarioDec = vlrDesc;
                    doc.Validademinimadoprodutonaentregade50porcento = "x";
                    doc.Validademinimadoprodutonaentregade60porcento = " ";
                    doc.Validademinimadoprodutonaentregade75porcento = " ";
                    doc.Validademinimadoprodutonaentregade80porcento = " ";
                    doc.Validademinimadoprodutonaentregavideedital = " ";
                    doc.Validademinimadoprodutonaentregaconformelegislacaovigentemedicamentomanipulado = " ";
                    doc.JustificativaValorLancado1 = ListaString(dist, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[0];
                    doc.JustificativaValorLancado2 = ListaString(dist, document.EmpenhoItem.DescricaoJustificativaPreco, 2)[1];

                    siafdoc.SiafisicoDocDescCT = new SiafisicoDocDescCT();
                    siafdoc.cdMsg = "SFCOCTDESC001";
                    siafdoc.SiafisicoDocDescCT.documento = doc;
                    break;
            }

            return siafdoc;
        }

        #region Auxiliares
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
                    resultado.Add(" ");
            }

            return resultado;
        }
        #endregion
    }
    #region Classe Concreta Documento
    public class Documento
    {
        public Model.Entity.Empenho.Empenho Empenho { get; set; }
        public EmpenhoReforco EmpenhoReforco { get; set; }
        public EmpenhoCancelamento EmpenhoCancelamento { get; set; }
        public Programa Programa { get; set; }
        public Fonte Fonte { get; set; }
        public Estrutura Estutura { get; set; }

        public IEnumerable<IMes> ValorMes { get; set; }
        public IEnumerable<IEmpenhoItem> EmpenhoItens { get; set; }
        public IEmpenhoItem EmpenhoItem { get; set; }
    }
    #endregion
}
