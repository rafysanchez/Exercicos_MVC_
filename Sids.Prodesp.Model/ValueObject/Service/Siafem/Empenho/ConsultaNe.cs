namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "documento")]
    //[XmlType(AnonymousType = true)]
    //[XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ConsultaNe
    {
        /// <remarks/>
        public string Acordo { get; set; }
        public string CgcCpf { get; set; }
        public string DataConsulta { get; set; }
        public string DataEmissao { get; set; }
        public string DataEntrega { get; set; }
        public string DataLancamento { get; set; }
        public string Despesa { get; set; }
        public string Docto { get; set; }
        public string EmpenhoOriginal { get; set; }
        public string Evento { get; set; }
        public string Fonte { get; set; }
        public string Gestao { get; set; }
        public string GestaoCredor { get; set; }
        public string HoraConsulta { get; set; }
        public string HoraLancamento { get; set; }
        public string IdentificadorObra { get; set; }
        public string Lancadopor { get; set; }
        public string Licitacao { get; set; }
        public string Local { get; set; }
        public string Mes01 { get; set; }
        public string Mes02 { get; set; }
        public string Mes03 { get; set; }
        public string Mes04 { get; set; }
        public string Mes05 { get; set; }
        public string Mes06 { get; set; }
        public string Mes07 { get; set; }
        public string Mes08 { get; set; }
        public string Mes09 { get; set; }
        public string Mes10 { get; set; }
        public string Mes11 { get; set; }
        public string Mes12 { get; set; }
        public string Mes13 { get; set; }
        public string Modalidade { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroNe { get; set; }
        public string NumeroProcesso { get; set; }
        public string Oc { get; set; }
        public string OrigemMaterial { get; set; }
        public string PlanoInterno { get; set; }
        public string Pt { get; set; }
        public string Ptres { get; set; }
        public string ReferenciaLegal { get; set; }
        public documentoRepete Repete { get; set; }
        public string ServicoouMaterial { get; set; }
        public string TipoEmpenho { get; set; }
        public string Ugo { get; set; }
        public string UnidadeGestora { get; set; }
        public string Uo { get; set; }
        public string Usuario { get; set; }
        public string Valor { get; set; }
        public string Valor01 { get; set; }
        public string Valor02 { get; set; }
        public string Valor03 { get; set; }
        public string Valor04 { get; set; }
        public string Valor05 { get; set; }
        public string Valor06 { get; set; }
        public string Valor07 { get; set; }
        public string Valor08 { get; set; }
        public string Valor09 { get; set; }
        public string Valor10 { get; set; }
        public string Valor11 { get; set; }
        public string Valor12 { get; set; }
        public string Valor13 { get; set; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class documentoRepete
    {

        private documentoRepeteTabela tabelaField;

        /// <remarks/>
        public documentoRepeteTabela tabela
        {
            get
            {
                return this.tabelaField;
            }
            set
            {
                this.tabelaField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class documentoRepeteTabela
    {

        private string sequenciaField;

        private string itemField;

        private string materialField;

        private string unidadeField;

        private string qtdeitemField;

        private string valorunitarioField;

        private string precototalField;

        private string descricaoField;

        private string descricao1Field;

        private string descricao2Field;

        private string descricao3Field;

        /// <remarks/>
        public string sequencia
        {
            get
            {
                return this.sequenciaField;
            }
            set
            {
                this.sequenciaField = value;
            }
        }

        /// <remarks/>
        public string item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        public string material
        {
            get
            {
                return this.materialField;
            }
            set
            {
                this.materialField = value;
            }
        }

        /// <remarks/>
        public string unidade
        {
            get
            {
                return this.unidadeField;
            }
            set
            {
                this.unidadeField = value;
            }
        }

        /// <remarks/>
        public string qtdeitem
        {
            get
            {
                return this.qtdeitemField;
            }
            set
            {
                this.qtdeitemField = value;
            }
        }

        /// <remarks/>
        public string valorunitario
        {
            get
            {
                return this.valorunitarioField;
            }
            set
            {
                this.valorunitarioField = value;
            }
        }

        /// <remarks/>
        public string precototal
        {
            get
            {
                return this.precototalField;
            }
            set
            {
                this.precototalField = value;
            }
        }

        /// <remarks/>
        public string descricao
        {
            get
            {
                return this.descricaoField;
            }
            set
            {
                this.descricaoField = value;
            }
        }

        /// <remarks/>
        public string descricao1
        {
            get
            {
                return this.descricao1Field;
            }
            set
            {
                this.descricao1Field = value;
            }
        }

        /// <remarks/>
        public string descricao2
        {
            get
            {
                return this.descricao2Field;
            }
            set
            {
                this.descricao2Field = value;
            }
        }

        /// <remarks/>
        public string descricao3
        {
            get
            {
                return this.descricao3Field;
            }
            set
            {
                this.descricao3Field = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        CgcCpf,

        /// <remarks/>
        DataConsulta,

        /// <remarks/>
        DataEmissao,

        /// <remarks/>
        DataEntrega,

        /// <remarks/>
        DataLancamento,

        /// <remarks/>
        Despesa,

        /// <remarks/>
        Docto,

        /// <remarks/>
        EmpenhoOriginal,

        /// <remarks/>
        Evento,

        /// <remarks/>
        Fonte,

        /// <remarks/>
        Gestao,

        /// <remarks/>
        GestaoCredor,

        /// <remarks/>
        HoraConsulta,

        /// <remarks/>
        HoraLancamento,

        /// <remarks/>
        IdentificadorObra,

        /// <remarks/>
        Lancadopor,

        /// <remarks/>
        Licitacao,

        /// <remarks/>
        Local,

        /// <remarks/>
        Mes01,

        /// <remarks/>
        Mes02,

        /// <remarks/>
        Mes03,

        /// <remarks/>
        Mes04,

        /// <remarks/>
        Mes05,

        /// <remarks/>
        Mes06,

        /// <remarks/>
        Mes07,

        /// <remarks/>
        Mes08,

        /// <remarks/>
        Mes09,

        /// <remarks/>
        Mes10,

        /// <remarks/>
        Mes11,

        /// <remarks/>
        Mes12,

        /// <remarks/>
        Mes13,

        /// <remarks/>
        Modalidade,

        /// <remarks/>
        NumeroContrato,

        /// <remarks/>
        NumeroNe,

        /// <remarks/>
        NumeroProcesso,

        /// <remarks/>
        Oc,

        /// <remarks/>
        OrigemMaterial,

        /// <remarks/>
        PlanoInterno,

        /// <remarks/>
        Pt,

        /// <remarks/>
        Ptres,

        /// <remarks/>
        ReferenciaLegal,

        /// <remarks/>
        Repete,

        /// <remarks/>
        ServicoouMaterial,

        /// <remarks/>
        TipoEmpenho,

        /// <remarks/>
        Ugo,

        /// <remarks/>
        UnidadeGestora,

        /// <remarks/>
        Uo,

        /// <remarks/>
        Usuario,

        /// <remarks/>
        Valor,

        /// <remarks/>
        Valor01,

        /// <remarks/>
        Valor02,

        /// <remarks/>
        Valor03,

        /// <remarks/>
        Valor04,

        /// <remarks/>
        Valor05,

        /// <remarks/>
        Valor06,

        /// <remarks/>
        Valor07,

        /// <remarks/>
        Valor08,

        /// <remarks/>
        Valor09,

        /// <remarks/>
        Valor10,

        /// <remarks/>
        Valor11,

        /// <remarks/>
        Valor12,

        /// <remarks/>
        Valor13,
    }


}
