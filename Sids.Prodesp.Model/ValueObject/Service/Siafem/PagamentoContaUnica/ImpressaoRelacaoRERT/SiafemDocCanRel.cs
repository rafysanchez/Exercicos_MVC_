﻿using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.PagamentoContaUnica.ImpressaoRelacaoRERT
{
    public class SiafemDocCanRel
    {
        [XmlElement("documento")]
        public DocumentoImpressaoRelacaoReRt Documento { get; set; }
    }
}
