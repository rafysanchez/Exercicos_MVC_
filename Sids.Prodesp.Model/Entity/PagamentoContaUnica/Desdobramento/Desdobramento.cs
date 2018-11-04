using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento
{
    [Table("tb_desdobramento")]
    public class Desdobramento: Base.PagamentoContaUnica, IPagamentoContaUnicaProdesp
    {
        public Desdobramento()
        {
            this.TransmitirProdesp = true;
            this.IdentificacaoDesdobramentos = new List<IdentificacaoDesdobramento>();
        }

        [Key]
        [Column("id_desdobramento")]
        public override int Id { get; set; }

        [Display(Name = "PRODESP")]
        [Column("bl_transmitir_prodesp")]
        public bool TransmitirProdesp { get; set; }

        [Column("bl_transmitido_prodesp")]
        public bool TransmitidoProdesp { get; set; }


        [Display(Name = "Data de Transmissão")]
        [Column("dt_transmitido_prodesp")]
        public DateTime DataTransmitidoProdesp { get; set; }

        [Column("ds_transmissao_mensagem_prodesp")]
        public string MensagemServicoProdesp { get; set; }

        [Column("ds_status_prodesp")]
        public string StatusProdesp { get; set; }


        [Column("ds_status_documento")]
        public bool StatusDocumento { get; set; }

        [Column("bl_situacao_desdobramento")]
        public string SituacaoDesdobramento { get; set; }
        
        
        [Display(Name = "Cod. Serviço")]
        [Column("cd_servico")]
        public string CodigoServico { get; set; }

        [Display(Name = "Valor Distribuição")]
        [Column("vr_distribuicao")]
        public decimal ValorDistribuido { get; set; }

        [Display(Name = "Descrição de Serviço")]
        [Column("ds_servico")]
        public string DescricaoServico { get; set; }

        
        [Display(Name = "Credor")]
        [Column("ds_credor")]
        public string DescricaoCredor { get; set; }

        
        [Display(Name = "Nome Reduzido do Credor")]
        [Column("nm_reduzido_credor")]
        public string NomeReduzidoCredor { get; set; }



        [Display(Name = "Tipo Despesa")]
        [Column("cd_tipo_despesa")]
        public string TipoDespesa { get; set; }


        [Display(Name = "Total ISSQN")]
        [Column("vr_total_issqn")]
        public decimal ValorIssqn { get; set; }


        [Display(Name = "Total IR")]
        [Column("vr_total_ir")]
        public decimal ValorIr { get; set; }


        [Display(Name = "Total INSS")]
        [Column("vr_total_inss")]
        public decimal ValorInss { get; set; }

        [Display(Name = "Aceita credor de desdrobramento diferente.")]
        [Column("bl_aceitar_credor")]
        public bool AceitaCredor { get; set; }
        

        [Column("id_tipo_desdobramento")]
        public int DesdobramentoTipoId { get; set; }

        [ForeignKey("DesdobramentoTipoId")]
        public DesdobramentoTipo DesdobramentoTipo { get; set; }
        

        [Column("nr_tipo_desdobramento")]
        public int NumeroDesdobramentoTipoId { get; set; }

        [ForeignKey("DesdobramentoTipoId")]
        public DesdobramentoTipo NumeroDesdobramentoTipo { get; set; }

        [Column("id_tipo_documento")]
        public int DocumentoTipoId { get; set; }

        [ForeignKey("DocumentoTipoId")]
        public DocumentoTipo DocumentoTipo { get; set; }


        public virtual IEnumerable<IdentificacaoDesdobramento> IdentificacaoDesdobramentos { get; set; }
    }
}
