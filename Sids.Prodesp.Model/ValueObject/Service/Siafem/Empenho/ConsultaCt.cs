using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Siafem.Empenho
{
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "CT")]
    public class ConsultaCt
    {
        public ConsultaCt()
        {
            RepeteDescricao = new List<Descricao>();
        }

        [XmlElement(ElementName = "CGCCpf")]
        public string CGCCpf { get; set; }

        [XmlElement(ElementName = "Contrato")]
        public string Contrato { get; set; }

        [XmlElement(ElementName = "ContratoEmpenhado")]
        public string ContratoEmpenhado { get; set; }
        
        [XmlElement(ElementName = "ContratoOriginal")]
        public string ContratoOriginal { get; set; }
         
        [XmlElement(ElementName = "Credor")]
        public string Credor { get; set; }
            
        [XmlElement(ElementName = "DataEmissao")]
        public string DataEmissao { get; set; }

        [XmlElement(ElementName = "DataEntregaPrevista")]
        public string DataEntregaPrevista { get; set; }
 
        [XmlElement(ElementName = "DataEmpenhoOriginal")]
        public string DataEmpenhoOriginal { get; set; }

        [XmlElement(ElementName = "Evento")]
        public string Evento { get; set; }
            
        [XmlElement(ElementName = "Fonte")]
        public string Fonte { get; set; }

        public string OrigemRecurso { get; set; }


        [XmlElement(ElementName = "Fornecedor")]
        public string Fornecedor { get; set; }
       
        [XmlElement(ElementName = "Gestao")]
        public string Gestao { get; set; }
            
        [XmlElement(ElementName = "LocalEntrega")]
        public string LocalEntrega { get; set; }
          
        [XmlElement(ElementName = "Bairro")]
        public string Bairro { get; set; }
         
        [XmlElement(ElementName = "Cidade")]
        public string Cidade { get; set; }
          
        [XmlElement(ElementName = "CEP")]
        public string CEP { get; set; }
          
        [XmlElement(ElementName = "Pedido")]
        public string Pedido { get; set; }
           
        [XmlElement(ElementName = "InformacoesAdicionais")]
        public string InformacoesAdicionais { get; set; }
        
        [XmlElement(ElementName = "ModalidadeEmpenho")]
        public string ModalidadeEmpenho { get; set; }
         
        [XmlElement(ElementName = "NaturezaDespesa")]
        public string NaturezaDespesa { get; set; }
         
        [XmlElement(ElementName = "NumeroContratoForn")]
        public string NumeroContratoForn { get; set; }
          
        [XmlElement(ElementName = "NumeroEdital")]
        public string NumeroEdital { get; set; }
           
        [XmlElement(ElementName = "Processo")]
        public string Processo { get; set; }
         
        [XmlElement(ElementName = "OrigemMaterial")]
        public string OrigemMaterial { get; set; }
     
        [XmlElement(ElementName = "PlanoInterno")]
        public string PlanoInterno { get; set; }

        //[XmlElement(ElementName = "PTRES")]
        //public string PlanoIPTRESnterno { get; set; }

        [XmlElement(ElementName = "PTRES")]
        public string PTRES { get; set; }

        [XmlElement(ElementName = "ReferenciaLegal")]
        public string ReferenciaLegal { get; set; }
       
        [XmlElement(ElementName = "ServicoMaterial")]
        public string ServicoMaterial { get; set; }
         
        [XmlElement(ElementName = "TipoCompraLicitacao")]
        public string TipoCompraLicitacao { get; set; }
          
        [XmlElement(ElementName = "ValorEmpenhar")]
        public string ValorEmpenhar { get; set; }
        
        [XmlElement(ElementName = "Uasg")]
        public string Uasg { get; set; }
  
        [XmlElement(ElementName = "UG")]
        public int UG { get; set; }
        
        [XmlElement(ElementName = "UGR")]
        public int UGR { get; set; }
       
        [XmlElement(ElementName = "Hora")]
        public DateTime Hora { get; set; }
        
        [XmlElement(ElementName = "Usuario")]
        public string Usuario { get; set; }
      
        [XmlElement(ElementName = "Mes1")]
        public string Mes1 { get; set; }
         
        [XmlElement(ElementName = "Mes2")]
        public string Mes2 { get; set; }
           
        [XmlElement(ElementName = "Mes3")]
        public string Mes3 { get; set; }
           
        [XmlElement(ElementName = "Mes4")]
        public string Mes4 { get; set; }
         
        [XmlElement(ElementName = "Mes5")]
        public string Mes5 { get; set; }
           
        [XmlElement(ElementName = "Mes6")]
        public string Mes6 { get; set; }
          
        [XmlElement(ElementName = "Mes7")]
        public string Mes7 { get; set; }

        [XmlElement(ElementName = "Mes8")]
        public string Mes8 { get; set; }
        
        [XmlElement(ElementName = "Mes9")]
        public string Mes9 { get; set; }

        [XmlElement(ElementName = "Mes10")]
        public string Mes10 { get; set; }
          
        [XmlElement(ElementName = "Mes11")]
        public string Mes11 { get; set; }
         
        [XmlElement(ElementName = "Mes12")]
        public string Mes12 { get; set; }
          
        [XmlElement(ElementName = "Mes13")]
        public string Mes13 { get; set; }


        [XmlElement(ElementName = "Valor1")]
        public string Valor1 { get; set; }
          
        [XmlElement(ElementName = "Valor2")]
        public string Valor2 { get; set; }
          
        [XmlElement(ElementName = "Valor3")]
        public string Valor3 { get; set; }
          
        [XmlElement(ElementName = "Valor4")]
        public string Valor4 { get; set; }
          
        [XmlElement(ElementName = "Valor5")]
        public string Valor5 { get; set; }
         
        [XmlElement(ElementName = "Valor6")]
        public string Valor6 { get; set; }
         
        [XmlElement(ElementName = "Valor7")]
        public string Valor7 { get; set; }
         
        [XmlElement(ElementName = "Valor8")]
        public string Valor8 { get; set; }
           
        [XmlElement(ElementName = "Valor9")]
        public string Valor9 { get; set; }
          
        [XmlElement(ElementName = "Valor10")]
        public string Valor10 { get; set; }
           
        [XmlElement(ElementName = "Valor11")]
        public string Valor11 { get; set; }
            
        [XmlElement(ElementName = "Valor12")]
        public string Valor12 { get; set; }

        [XmlElement(ElementName = "Valor13")]
        public string Valor13 { get; set; }
          


        [XmlElement(ElementName = "CentroCusto")]
        public CentroCusto CentroCusto { get; set; }

        public List<Descricao> RepeteDescricao { get; set; }

    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "CentroCusto")]
    public class CentroCusto
    {

        [XmlElement(ElementName = "Nivel1")]
        public string Nivel1 { get; set; }

        [XmlElement(ElementName = "Nivel2")]
        public string Nivel2 { get; set; }

        [XmlElement(ElementName = "Nivel3")]
        public string Nivel3 { get; set; }

        [XmlElement(ElementName = "Nivel4")]
        public string Nivel4 { get; set; }
    }

    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "Descricao")]
    public class Descricao
    {
        [XmlElement(ElementName = "Seq")]
        public string Seq { get; set; }

        [XmlElement(ElementName = "Item")]
        public string Item { get; set; }

        [XmlElement(ElementName = "UFor")]
        public string UFor { get; set; }

        [XmlElement(ElementName = "Quantidade")]
        public string Quantidade { get; set; }

        [XmlElement(ElementName = "Descricao")]
        public string descricao { get; set; }

        [XmlElement(ElementName = "Justificativa")]
        public string Justificativa { get; set; }

        [XmlElement(ElementName = "PrecoTotal")]
        public string PrecoTotal { get; set; }

        [XmlElement(ElementName = "ValorUnitario")]
        public string ValorUnitario { get; set; }

        [XmlElement(ElementName = "Municipio")]
        public string Municipio { get; set; }

        [XmlElement(ElementName = "ProPPais")]
        public string ProPPais { get; set; }

        
    }

}
