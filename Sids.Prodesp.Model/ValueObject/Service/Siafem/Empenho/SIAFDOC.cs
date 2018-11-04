using System;
using System.Collections.Generic;
using Sids.Prodesp.Model.ValueObject.Service.Siafem.Base;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{
    public class SIAFDOC
    {
        public string cdMsg { get; set; }

        public SiafemDocConsultaEmpenhos SiafemDocConsultaEmpenhos { get; set; }
        public SiafemDocNE SiafemDocNE { get; set; }
        public SiafemDocRENE SiafemDocRENE { get; set; }
        public SiafemDocIncDescNE SiafemDocIncDescNE { get; set; }
        public SiafemDocListaEmpenhos SiafemDocListaEmpenhos { get; set; }

        public SiafemDocCanNeTes SiafemDocCanNeTes { get; set; }

        public SiafemDocCanNeVin SiafemDocCanNeVin { get; set; }

        public SiafemDocCanNeAdTes SiafemDocCanNeAdTes { get; set; }

        public SiafemDocCanNeAdVin SiafemDocCanNeAdVin { get; set; }

        public SiafemDocCanNeAdVnp SiafemDocCanNeAdVnp { get; set; }

        public SiafemDocCanNePess SiafemDocCanNePess { get; set; }

        public SiafemDocCanNeTesBL SiafemDocCanNeTesBL { get; set; }



        [XmlElement("SiafemDocImpNe2")]
        public SiafemDocObterPdfEmpenho SiafemDocPdfEmpenho { get; set; }
        [XmlElement("SiafisicoDocImpNe2")]
        public SiafemDocObterPdfEmpenho SiafisicoDocPdfEmpenho { get; set; }
    }


    public class SiafemDocCanNeTes
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNeVin
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNeAdTes
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNeAdVin
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNeAdVnp
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNePess
    {
        public documento documento { get; set; }
    }

    public class SiafemDocCanNeTesBL
    {
        public documento documento { get; set; }
    }


    public class SiafemDocIncDescNE
    {
        public documento documento { get; set; }
        public descricao descricao { get; set; }
    }

    public class SiafemDocListaEmpenhos
    {
        public documento documento { get; set; }
    }

    public class SiafemDocRENE
    {
        public documento documento { get; set; }
        public cronograma cronograma { get; set; }
        public descricao descricao { get; set; }
    }

    public class SiafemDocConsultaEmpenhos
    {
        public documento documento { get; set; }
    }

    public class SiafemDocNE
    {
        public documento documento { get; set; }
        public cronograma cronograma { get; set; }
        public descricao descricao { get; set; }
    }

    public class cronograma
    {
        public Repeticao Repeticao { get; set; }
        public List<repeticao> repeticao { get; set; }
    }

    public class descricao
    {
        public Repeticao Repeticao { get; set; }

        public Repeticao repeticao { get; set; }
    }
    public class Repeticao
    {
        public List<desc> desc { get; set; }
        public List<des> des { get; set; }
    }


    public class desc
    {
        public string Mes { get; set; }
        public string Valor { get; set; }

        public string mes { get; set; }
        public string valor { get; set; }
    }

    public class repeticao
    {
        public string mes { get; set; }
        public string valor { get; set; }
    }

    public class des
    {

        public string UnidadeMedida { get; set; }

        public string Quantidade { get; set; }

        public string PrecoUnitario { get; set; }

        public string PrecoTotal { get; set; }

        public string DescricaoParte1 { get; set; }

        public string DescricaoParte2 { get; set; }

        public string DescricaoParte3 { get; set; }

        public string DescricaoParte4 { get; set; }

        public string DescricaoParte5 { get; set; }

        public string DescricaoParte6 { get; set; }

        public string DescricaoParte7 { get; set; }

        public string DescricaoParte8 { get; set; }

        public string DescricaoParte9 { get; set; }

        public string DescricaoParte10 { get; set; }

        public string DescricaoParteB1 { get; set; }

        public string DescricaoParteB2 { get; set; }

        public string DescricaoParteB3 { get; set; }

        public string DescricaoParteB4 { get; set; }

        public string DescricaoParteB5 { get; set; }

        public string DescricaoParteB6 { get; set; }

        public string DescricaoParteB7 { get; set; }

        public string DescricaoParteB8 { get; set; }

        public string DescricaoParteB9 { get; set; }

        public string DescricaoParteB10 { get; set; }

        public string DescricaoParteC1 { get; set; }

        public string DescricaoParteC2 { get; set; }

        public string DescricaoParteC3 { get; set; }

        public string DescricaoParteC4 { get; set; }

        public string DescricaoParteC5 { get; set; }

        public string DescricaoParteC6 { get; set; }

        public string DescricaoParteC7 { get; set; }

        public string DescricaoParteC8 { get; set; }

        public string DescricaoParteC9 { get; set; }

        public string DescricaoParteC10 { get; set; }

        public string NumPD { get; set; }
        public string Causa1 { get; set; }
        public string Causa2 { get; set; }
    }

        
  
}


