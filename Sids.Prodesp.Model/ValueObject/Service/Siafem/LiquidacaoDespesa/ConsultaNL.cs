namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.LiquidacaoDespesa
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Serialization;

    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "documento", IsNullable = false, Namespace = "")]
    public class ConsultaNL
    {

        [XmlElement(ElementName = "UG")]
        public string UG { get; set; }

        [XmlElement(ElementName = "CgcCpf")]
        public string CgcCpf { get; set; }

        [XmlElement(ElementName = "GestaoFavorecido")]
        public string GestaoFavorecido { get; set; }

        [XmlElement(ElementName = "Gestao")]
        public string Gestao { get; set; }

        [XmlElement(ElementName = "DataLancamento")]
        public string DataLancamento { get; set; }

        [XmlElement(ElementName = "DataEmissao")]
        public string DataEmissao { get; set; }

        [XmlElement(ElementName = "NumeroNL")]
        public string NumeroNL { get; set; }

        [XmlElement(ElementName = "Classificacao1")]
        public string Classificacao1 { get; set; }

        [XmlElement(ElementName = "Evento1")]
        public string Evento1 { get; set; }

        [XmlElement(ElementName = "Fonte1")]
        public string Fonte1 { get; set; }

        [XmlElement(ElementName = "RecDesp1")]
        public string RecDesp1 { get; set; }

        [XmlElement(ElementName = "InscEvento1")]
        public string InscEvento1 { get; set; }

        [XmlElement(ElementName = "Valor1")]
        public string Valor1 { get; set; }

        [XmlElement(ElementName = "Classificacao2")]
        public string Classificacao2 { get; set; }

        [XmlElement(ElementName = "Evento2")]
        public string Evento2 { get; set; }

        [XmlElement(ElementName = "Fonte2")]
        public string Fonte2 { get; set; }

        [XmlElement(ElementName = "RecDesp2")]
        public string RecDesp2 { get; set; }

        [XmlElement(ElementName = "InscEvento2")]
        public string InscEvento2 { get; set; }

        [XmlElement(ElementName = "Valor2")]
        public string Valor2 { get; set; }

        [XmlElement(ElementName = "Observacao1")]
        public string Observacao1 { get; set; }

        [XmlElement(ElementName = "Observacao2")]
        public string Observacao2 { get; set; }

        [XmlElement(ElementName = "Observacao3")]
        public string Observacao3 { get; set; }

        [XmlElement(ElementName = "Lancadopor")]
        public string Lancadopor { get; set; }

        [XmlElement(ElementName = "LancadoHora")]
        public string LancadoHora { get; set; }

        [XmlElement(ElementName = "LancadoData")]
        public string LancadoData { get; set; }

        //[XmlElement(ElementName = "ItensLiquidados")]
        [XmlArray("ItensLiquidados")]
        [XmlArrayItem("Item")]
        public List<ConsultaNLItem> ItensLiquidados { get; set; }


        [Serializable]
        //[XmlType(AnonymousType = true)]
        [XmlRoot(Namespace = "", IsNullable = false, ElementName = "Item")]
        public class ConsultaNLItem
        {
            [XmlElement(ElementName = "Codigo")]
            public int Codigo { get; set; }

            [XmlElement(ElementName = "Quantidade")]
            public string Quantidade { get; set; }

            [XmlElement(ElementName = "Valor")]
            public string Valor { get; set; }

            public decimal ValorFormatadoDecimal
            {
                get
                {
                    return Convert.ToDecimal(this.Valor, new CultureInfo("pt-br")) * 100;
                }
            }

            public decimal QuantidadeFormatadoDecimal
            {
                get
                {
                    return Convert.ToDecimal(this.Quantidade, new CultureInfo("pt-br")) * 1000;
                }
            }

            [XmlElement(ElementName = "Seq")]
            public string Sequencia { get; set; }
        }
    }
}

