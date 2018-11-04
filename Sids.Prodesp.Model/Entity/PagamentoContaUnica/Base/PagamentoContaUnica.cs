using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Base
{
    public class PagamentoContaUnica: IPagamentoContaUnica
    {
        
        public virtual int Id { get; set; }

        [Column("bl_cadastro_completo")]
        public bool CadastroCompleto { get; set; }

        [Display(Name = "Cód. Aplicação")]
        [Column("cd_aplicacao_obra")]
        public virtual string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Data Emissão")]
        [Column("dt_emissao")]
        public DateTime DataEmissao { get; set; }

        [Display(Name = "Regional")]
        [Column("id_regional")]
        public int RegionalId { get; set; }

        [Display(Name = "N° do Contrato")]
        [Column("nr_contrato")]
        public string NumeroContrato { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Column("dt_cadastro")]
        public DateTime DataCadastro { get; set; }
        
        [Column("id_tipo_documento")]
        public int DocumentoTipoId { get; set; }

        [ForeignKey("DocumentoTipoId")]
        public DocumentoTipo DocumentoTipo { get; set; }

        [Display(Name = "Nº do Documento")]
        [Column("nr_documento")]
        public string NumeroDocumento { get; set; }
    }
}
