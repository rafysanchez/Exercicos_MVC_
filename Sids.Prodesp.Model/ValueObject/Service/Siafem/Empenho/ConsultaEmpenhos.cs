namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false,ElementName = "documento")]
    public partial class ConsultaEmpenhos
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("tabela", IsNullable = false)]
        public documentoTabela[] Repete { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class documentoTabela
    {
        /// <remarks/>
        public string sequencia { get; set; }

        /// <remarks/>
        public string numerone { get; set; }

        /// <remarks/>
        public uint natureza { get; set; }

        /// <remarks/>
        public string evento { get; set; }

        /// <remarks/>
        public string credor { get; set; }

        /// <remarks/>
        public string valor { get; set; }

        /// <remarks/>
        public string tipo { get; set; }
    }




}
