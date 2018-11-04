using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "NE")]
    public class ConsultaPrecoNE
    {
        [XmlElement(ElementName = "cd_Data")]
        public string cd_Data { get; set; }

        [XmlElement(ElementName = "cd_DataEmissao")]
        public string cd_DataEmissao { get; set; }

        [XmlElement(ElementName = "cd_DoctoUgCred")]
        public string cd_DoctoUgCred { get; set; }

        [XmlElement(ElementName = "cd_Em")]
        public string cd_Em { get; set; }

        [XmlElement(ElementName = "cd_LocalEntrega")]
        public string cd_LocalEntrega { get; set; }

        [XmlElement(ElementName = "cd_Gestao")]
        public string cd_Gestao { get; set; }

        [XmlElement(ElementName = "cd_Hora")]
        public string cd_Hora { get; set; }

        [XmlElement(ElementName = "cd_Evento")]
        public string cd_Evento { get; set; }

        [XmlElement(ElementName = "cd_Fonte")]
        public string cd_Fonte { get; set; }

        [XmlElement(ElementName = "cd_LancadoPor")]
        public string cd_LancadoPor { get; set; }

        [XmlElement(ElementName = "cd_LancadoPorEm")]
        public string cd_LancadoPorEm { get; set; }

        [XmlElement(ElementName = "cd_Licitacao")]
        public string cd_Licitacao { get; set; }

        [XmlElement(ElementName = "cd_Modalidade")]
        public string cd_Modalidade { get; set; }

        [XmlElement(ElementName = "cd_ND")]
        public string cd_ND { get; set; }

        [XmlElement(ElementName = "cd_Referencia")]
        public string cd_Referencia { get; set; }

        [XmlElement(ElementName = "cd_NumContrato")]
        public string cd_NumContrato { get; set; }

        [XmlElement(ElementName = "cd_NumNE")]
        public string cd_NumNE { get; set; }

        [XmlElement(ElementName = "cd_OfertaCompra")]
        public string cd_OfertaCompra { get; set; }

        [XmlElement(ElementName = "cd_Origem")]
        public string cd_Origem { get; set; }

        [XmlElement(ElementName = "cd_PLInterno")]
        public string cd_PLInterno { get; set; }

        [XmlElement(ElementName = "cd_Processo")]
        public string cd_Processo { get; set; }

        [XmlElement(ElementName = "cd_ProgTrabalho")]
        public string cd_ProgTrabalho { get; set; }

        [XmlElement(ElementName = "cd_Ptres")]
        public string cd_Ptres { get; set; }

        [XmlElement(ElementName = "cd_Saldo")]
        public string cd_Saldo { get; set; }

        [XmlElement(ElementName = "cd_ServMaterial")]
        public string cd_ServMaterial { get; set; }

        [XmlElement(ElementName = "cd_TipoEmpenho")]
        public string cd_TipoEmpenho { get; set; }

        [XmlElement(ElementName = "cd_TipoNE")]
        public string cd_TipoNE { get; set; }

        [XmlElement(ElementName = "cd_UGO")]
        public string cd_UGO { get; set; }

        [XmlElement(ElementName = "cd_UnidadeGestora")]
        public string cd_UnidadeGestora { get; set; }

        [XmlElement(ElementName = "cd_Uo")]
        public string cd_Uo { get; set; }

        [XmlElement(ElementName = "cd_Usuario")]
        public string cd_Usuario { get; set; }

        [XmlElement(ElementName = "cd_Valor")]
        public string cd_Valor { get; set; }

        public List<Descricao> RepeteDescricao { get; set; }


        [XmlTypeAttribute(AnonymousType = true)]
        [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "Descricao")]
        public class Descricao
        {
            [XmlElement(ElementName = "Seq")]
            public string Seq { get; set; }

            [XmlElement(ElementName = "Item")]
            public string Item { get; set; }

            [XmlElement(ElementName = "ItMeEpp")]
            public string ItMeEpp { get; set; }

            [XmlElement(ElementName = "Quantidade")]
            public string Quantidade { get; set; }

            [XmlElement(ElementName = "Saldo")]
            public string Saldo { get; set; }

            public decimal SaldoCalculado
            {
                get
                {
                    return (Convert.ToDecimal(this.Saldo) - Convert.ToDecimal(this.VlrLiquidado));
                }
            }

            public decimal QuantidadeCalculada
            {
                get
                {
                    return (Convert.ToDecimal(this.Quantidade) - Convert.ToDecimal(this.QtdLiquidado));
                }
            }

            [XmlElement(ElementName = "UF")]
            public string UF { get; set; }

            [XmlElement(ElementName = "ValorUnitario")]
            public string ValorUnitario { get; set; }

            [XmlElement(ElementName = "DescricaoItem")]
            public string DescricaoItem { get; set; }

            [XmlElement(ElementName = "ProdutorPPais")]
            public string ProdutorPPais { get; set; }

            [XmlElement(ElementName = "QtdLiquidado")]
            public string QtdLiquidado { get; set; }

            [XmlElement(ElementName = "VlrLiquidado")]
            public string VlrLiquidado { get; set; }

        }
    }
}
