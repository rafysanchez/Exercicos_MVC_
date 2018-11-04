using Sids.Prodesp.Model.ValueObject.Service.Siafem.Base;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{
    public class SFCODOC
    {
        public string cdMsg { get; set; }
        public SiafemDocConsultaEmpenhos SiafemDocConsultaEmpenhos { get; set; }

        public SiafisicoDocCT SiafisicoDocCT { get; set; }
        public SiafisicoDocDescCT SiafisicoDocDescCT { get; set; }
        public SiafisicoDocContNe2 SiafisicoDocContNe2 { get; set; }
        public SiafisicoDocContNeBec2 SiafisicoDocContNeBec2 { get; set; }
        
        public SiafisicoDocAltDescCT SiafisicoDocAltDescCT { get; set; }
        public SiafisicoDocAltContNe SiafisicoDocAltContNe { get; set; }
        public SiafisicoDocIncContRE SiafisicoDocIncContRE { get; set; }
        public SiafisicoConsultaCT SiafisicoConsultaCT { get; set; }


        public SiafisicoDocCanCTVinc SiafisicoDocCanCTVinc { get; set; }
        public SiafisicoDocCanNeVin SiafisicoDocCanNeVin { get; set; }
        public SiafisicoDocCanNeBecTes SiafisicoDocCanNeBecTes { get; set; }
        public SiafisicoDocCanNeBecVin SiafisicoDocCanNeBecVin { get; set; }
        public SiafisicoConPrecoNE SiafisicoConPrecoNE { get; set; }

        [XmlElement("SiafemDocImpNe2")]
        public SiafemDocObterPdfEmpenho SiafemDocPdfEmpenho { get; set; }
        [XmlElement("SiafisicoDocImpNe2")]
        public SiafemDocObterPdfEmpenho SiafisicoDocPdfEmpenho { get; set; }

    }

    public class SiafisicoConPrecoNE
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocCanNeBecTes
    {
        public documento documento { get; set; }
    }
    public class SiafisicoDocCanNeBecVin
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocContNeBec2
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocCanCTVinc
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocCanNeVin
    {
        public documento documento { get; set; }

        public cronograma cronograma { get; set; }
    }

    public class SiafisicoConsultaCT
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocIncContRE
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocCT
    {
        public documento documento { get; set; }
        public cronograma cronograma { get; set; }

        public LocalEntrega LocalEntrega { get; set; }
    }
    
    public class LocalEntrega
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        public string InformacoesAdicionais { get; set; }
    }

    public class SiafisicoDocDescCT
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocContNe2
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocAltDescCT
    {
        public documento documento { get; set; }
    }

    public class SiafisicoDocAltContNe
    {
        public documento documento { get; set; }
    }

    public class SiafemDocObterPdfEmpenho
    {
        public documento documento { get; set; }
    }
}

