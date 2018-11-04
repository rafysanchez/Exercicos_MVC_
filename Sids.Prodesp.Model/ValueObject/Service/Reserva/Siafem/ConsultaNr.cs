using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sids.Prodesp.Model.ValueObject.Service.Reserva.Siafem
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false,ElementName = "documento")]
    public partial class ConsultaNr
    {

        private bool statusOperacaoField;

        private string dataConsultaField;

        private string horaConsultaField;

        private string usuarioField;

        private string dataEmissaoField;

        private string numeroNRField;

        private uint unidadeGestoraField;

        private string descricaoUGField;

        private ushort gestaoField;

        private string descricaoGestaoField;

        private uint eventoField;

        private uint ptresField;

        private ushort uoField;

        private ulong ptField;

        private string fonteField;

        private string despesaField;

        private uint uGRField;

        private string planoInternoField;

        private string numeroProcessoField;

        private string valorField;

        private string lancadoporField;

        private string dataLancamentoField;

        private string horaLancamentoField;

        private string mes01Field;

        private string mes02Field;

        private string mes03Field;

        private string mes04Field;

        private string mes05Field;

        private string mes06Field;

        private string mes07Field;

        private string mes08Field;

        private string mes09Field;

        private string mes10Field;

        private string mes11Field;

        private string mes12Field;

        private string valor01Field;

        private string valor02Field;

        private string valor03Field;

        private string valor04Field;

        private string valor05Field;

        private string valor06Field;

        private string valor07Field;

        private string valor08Field;

        private string valor09Field;

        private string valor10Field;

        private string valor11Field;

        private string valor12Field;

        private string observacao1Field;

        private string observacao2Field;

        private string observacao3Field;

        private string numOCField;

        private string msgRetornoField;

        /// <remarks/>
        public bool StatusOperacao
        {
            get
            {
                return this.statusOperacaoField;
            }
            set
            {
                this.statusOperacaoField = value;
            }
        }

        /// <remarks/>
        public string DataConsulta
        {
            get
            {
                return this.dataConsultaField;
            }
            set
            {
                this.dataConsultaField = value;
            }
        }

        /// <remarks/>
        public string HoraConsulta
        {
            get
            {
                return this.horaConsultaField;
            }
            set
            {
                this.horaConsultaField = value;
            }
        }

        /// <remarks/>
        public string Usuario
        {
            get
            {
                return this.usuarioField;
            }
            set
            {
                this.usuarioField = value;
            }
        }

        /// <remarks/>
        public string DataEmissao
        {
            get
            {
                return this.dataEmissaoField;
            }
            set
            {
                this.dataEmissaoField = value;
            }
        }

        /// <remarks/>
        public string NumeroNR
        {
            get
            {
                return this.numeroNRField;
            }
            set
            {
                this.numeroNRField = value;
            }
        }

        /// <remarks/>
        public uint UnidadeGestora
        {
            get
            {
                return this.unidadeGestoraField;
            }
            set
            {
                this.unidadeGestoraField = value;
            }
        }

        /// <remarks/>
        public string DescricaoUG
        {
            get
            {
                return this.descricaoUGField;
            }
            set
            {
                this.descricaoUGField = value;
            }
        }

        /// <remarks/>
        public ushort Gestao
        {
            get
            {
                return this.gestaoField;
            }
            set
            {
                this.gestaoField = value;
            }
        }

        /// <remarks/>
        public string DescricaoGestao
        {
            get
            {
                return this.descricaoGestaoField;
            }
            set
            {
                this.descricaoGestaoField = value;
            }
        }

        /// <remarks/>
        public uint Evento
        {
            get
            {
                return this.eventoField;
            }
            set
            {
                this.eventoField = value;
            }
        }

        /// <remarks/>
        public uint Ptres
        {
            get
            {
                return this.ptresField;
            }
            set
            {
                this.ptresField = value;
            }
        }

        /// <remarks/>
        public ushort Uo
        {
            get
            {
                return this.uoField;
            }
            set
            {
                this.uoField = value;
            }
        }

        /// <remarks/>
        public ulong Pt
        {
            get
            {
                return this.ptField;
            }
            set
            {
                this.ptField = value;
            }
        }

        /// <remarks/>
        public string Fonte
        {
            get
            {
                return this.fonteField;
            }
            set
            {
                this.fonteField = value;
            }
        }

        /// <remarks/>
        public string Despesa
        {
            get
            {
                return this.despesaField;
            }
            set
            {
                this.despesaField = value;
            }
        }

        /// <remarks/>
        public uint UGR
        {
            get
            {
                return this.uGRField;
            }
            set
            {
                this.uGRField = value;
            }
        }

        /// <remarks/>
        public string PlanoInterno
        {
            get
            {
                return this.planoInternoField;
            }
            set
            {
                this.planoInternoField = value;
            }
        }

        /// <remarks/>
        public string NumeroProcesso
        {
            get
            {
                return this.numeroProcessoField;
            }
            set
            {
                this.numeroProcessoField = value;
            }
        }

        /// <remarks/>
        public string Valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }

        /// <remarks/>
        public string Lancadopor
        {
            get
            {
                return this.lancadoporField;
            }
            set
            {
                this.lancadoporField = value;
            }
        }

        /// <remarks/>
        public string DataLancamento
        {
            get
            {
                return this.dataLancamentoField;
            }
            set
            {
                this.dataLancamentoField = value;
            }
        }

        /// <remarks/>
        public string HoraLancamento
        {
            get
            {
                return this.horaLancamentoField;
            }
            set
            {
                this.horaLancamentoField = value;
            }
        }

        /// <remarks/>
        public string Mes01
        {
            get
            {
                return this.mes01Field;
            }
            set
            {
                this.mes01Field = value;
            }
        }

        /// <remarks/>
        public string Mes02
        {
            get
            {
                return this.mes02Field;
            }
            set
            {
                this.mes02Field = value;
            }
        }

        /// <remarks/>
        public string Mes03
        {
            get
            {
                return this.mes03Field;
            }
            set
            {
                this.mes03Field = value;
            }
        }

        /// <remarks/>
        public string Mes04
        {
            get
            {
                return this.mes04Field;
            }
            set
            {
                this.mes04Field = value;
            }
        }

        /// <remarks/>
        public string Mes05
        {
            get
            {
                return this.mes05Field;
            }
            set
            {
                this.mes05Field = value;
            }
        }

        /// <remarks/>
        public string Mes06
        {
            get
            {
                return this.mes06Field;
            }
            set
            {
                this.mes06Field = value;
            }
        }

        /// <remarks/>
        public string Mes07
        {
            get
            {
                return this.mes07Field;
            }
            set
            {
                this.mes07Field = value;
            }
        }

        /// <remarks/>
        public string Mes08
        {
            get
            {
                return this.mes08Field;
            }
            set
            {
                this.mes08Field = value;
            }
        }

        /// <remarks/>
        public string Mes09
        {
            get
            {
                return this.mes09Field;
            }
            set
            {
                this.mes09Field = value;
            }
        }

        /// <remarks/>
        public string Mes10
        {
            get
            {
                return this.mes10Field;
            }
            set
            {
                this.mes10Field = value;
            }
        }

        /// <remarks/>
        public string Mes11
        {
            get
            {
                return this.mes11Field;
            }
            set
            {
                this.mes11Field = value;
            }
        }

        /// <remarks/>
        public string Mes12
        {
            get
            {
                return this.mes12Field;
            }
            set
            {
                this.mes12Field = value;
            }
        }

        /// <remarks/>
        public string Valor01
        {
            get
            {
                return this.valor01Field;
            }
            set
            {
                this.valor01Field = value;
            }
        }

        /// <remarks/>
        public string Valor02
        {
            get
            {
                return this.valor02Field;
            }
            set
            {
                this.valor02Field = value;
            }
        }

        /// <remarks/>
        public string Valor03
        {
            get
            {
                return this.valor03Field;
            }
            set
            {
                this.valor03Field = value;
            }
        }

        /// <remarks/>
        public string Valor04
        {
            get
            {
                return this.valor04Field;
            }
            set
            {
                this.valor04Field = value;
            }
        }

        /// <remarks/>
        public string Valor05
        {
            get
            {
                return this.valor05Field;
            }
            set
            {
                this.valor05Field = value;
            }
        }

        /// <remarks/>
        public string Valor06
        {
            get
            {
                return this.valor06Field;
            }
            set
            {
                this.valor06Field = value;
            }
        }

        /// <remarks/>
        public string Valor07
        {
            get
            {
                return this.valor07Field;
            }
            set
            {
                this.valor07Field = value;
            }
        }

        /// <remarks/>
        public string Valor08
        {
            get
            {
                return this.valor08Field;
            }
            set
            {
                this.valor08Field = value;
            }
        }

        /// <remarks/>
        public string Valor09
        {
            get
            {
                return this.valor09Field;
            }
            set
            {
                this.valor09Field = value;
            }
        }

        /// <remarks/>
        public string Valor10
        {
            get
            {
                return this.valor10Field;
            }
            set
            {
                this.valor10Field = value;
            }
        }

        /// <remarks/>
        public string Valor11
        {
            get
            {
                return this.valor11Field;
            }
            set
            {
                this.valor11Field = value;
            }
        }

        /// <remarks/>
        public string Valor12
        {
            get
            {
                return this.valor12Field;
            }
            set
            {
                this.valor12Field = value;
            }
        }

        /// <remarks/>
        public string Observacao1
        {
            get
            {
                return this.observacao1Field;
            }
            set
            {
                this.observacao1Field = value;
            }
        }

        /// <remarks/>
        public string Observacao2
        {
            get
            {
                return this.observacao2Field;
            }
            set
            {
                this.observacao2Field = value;
            }
        }

        /// <remarks/>
        public string Observacao3
        {
            get
            {
                return this.observacao3Field;
            }
            set
            {
                this.observacao3Field = value;
            }
        }

        /// <remarks/>
        public string NumOC
        {
            get
            {
                return this.numOCField;
            }
            set
            {
                this.numOCField = value;
            }
        }

        /// <remarks/>
        public string MsgRetorno
        {
            get
            {
                return this.msgRetornoField;
            }
            set
            {
                this.msgRetornoField = value;
            }
        }
    }
    //public sealed class ConsultaNr
    //{
    //    //[XmlAttribute("StatusOperacao")]
    //    //public string StatusOperacao { get; set; }

    //    //[XmlAttribute("DataConsulta")]
    //    //public string DataConsulta { get; set; }

    //    //[XmlAttribute("HoraConsulta")]
    //    //public string HoraConsulta { get; set; }

    //    //[XmlAttribute("Usuario")]
    //    //public string Usuario { get; set; }

    //    //[XmlAttribute("DataEmissao")]
    //    //public string DataEmissao { get; set; }

    //    //[XmlAttribute("NumeroNR")]
    //    //public string NumeroNR { get; set; }

    //    //[XmlAttribute("UnidadeGestora")]
    //    //public string UnidadeGestora { get; set; }

    //    //[XmlAttribute("DescricaoUG")]
    //    //public string DescricaoUG { get; set; }

    //    //[XmlAttribute("Gestao")]
    //    //public string Gestao { get; set; }

    //    //[XmlAttribute("DescricaoGestao")]
    //    //public string DescricaoGestao { get; set; }

    //    //[XmlAttribute("Evento")]
    //    //public string Evento { get; set; }

    //    //[XmlAttribute("Ptres")]
    //    //public string Ptres { get; set; }

    //    //[XmlAttribute("Uo")]
    //    //public string Uo { get; set; }

    //    //[XmlAttribute("Pt")]
    //    //public string Pt { get; set; }

    //    //[XmlAttribute("Fonte")]
    //    //public string Fonte { get; set; }

    //    //[XmlAttribute("Despesa")]
    //    //public string Despesa { get; set; }

    //    //[XmlAttribute("UGR")]
    //    //public string UGR { get; set; }

    //    //[XmlAttribute("PlanoInterno")]
    //    //public string PlanoInterno { get; set; }

    //    //[XmlAttribute("NumeroProcesso")]
    //    //public string NumeroProcesso { get; set; }

    //    //[XmlAttribute("Valor")]
    //    //public string Valor { get; set; }

    //    //[XmlAttribute("Lancadopor")]
    //    //public string Lancadopor { get; set; }

    //    //[XmlAttribute("DataLancamento")]
    //    //public string DataLancamento { get; set; }

    //    //[XmlAttribute("HoraLancamento")]
    //    //public string HoraLancamento { get; set; }

    //    //[XmlAttribute("Mes01")]
    //    //public string Mes01 { get; set; }

    //    //[XmlAttribute("Mes02")]
    //    //public string Mes02 { get; set; }

    //    //[XmlAttribute("Mes03")]
    //    //public string Mes03 { get; set; }

    //    //[XmlAttribute("Mes04")]
    //    //public string Mes04 { get; set; }

    //    //[XmlAttribute("Mes05")]
    //    //public string Mes05 { get; set; }

    //    //[XmlAttribute("Mes06")]
    //    //public string Mes06 { get; set; }

    //    //[XmlAttribute("Mes07")]
    //    //public string Mes07 { get; set; }

    //    //[XmlAttribute("Mes08")]
    //    //public string Mes08 { get; set; }

    //    //[XmlAttribute("Mes09")]
    //    //public string Mes09 { get; set; }

    //    //[XmlAttribute("Mes10")]
    //    //public string Mes10 { get; set; }

    //    //[XmlAttribute("Mes11")]
    //    //public string Mes11 { get; set; }

    //    //[XmlAttribute("Mes12")]
    //    //public string Mes12 { get; set; }

    //    //[XmlAttribute("Valor01")]
    //    //public string Valor01 { get; set; }

    //    //[XmlAttribute("Valor02")]
    //    //public string Valor02 { get; set; }

    //    //[XmlAttribute("Valor03")]
    //    //public string Valor03 { get; set; }

    //    //[XmlAttribute("Valor04")]
    //    //public string Valor04 { get; set; }

    //    //[XmlAttribute("Valor05")]
    //    //public string Valor05 { get; set; }

    //    //[XmlAttribute("Valor06")]
    //    //public string Valor06 { get; set; }

    //    //[XmlAttribute("Valor07")]
    //    //public string Valor07 { get; set; }

    //    //[XmlAttribute("Valor08")]
    //    //public string Valor08 { get; set; }

    //    //[XmlAttribute("Valor09")]
    //    //public string Valor09 { get; set; }

    //    //[XmlAttribute("Valor10")]
    //    //public string Valor10 { get; set; }

    //    //[XmlAttribute("Valor11")]
    //    //public string Valor11 { get; set; }

    //    //[XmlAttribute("Valor12")]
    //    //public string Valor12 { get; set; }

    //    //[XmlAttribute("Observacao1")]
    //    //public string Observacao1 { get; set; }

    //    //[XmlAttribute("Observacao2")]
    //    //public string Observacao2 { get; set; }

    //    //[XmlAttribute("Observacao3")]
    //    //public string Observacao3 { get; set; }

    //    //[XmlAttribute("NumOC")]
    //    //public string NumOC { get; set; }

    //    //[XmlAttribute("MsgRetorno")]
    //    //public string MsgRetorno { get; set; }

    //}
}



