using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento
{
    public class DocumentoTipo : ITipoPagamentoContaUnica
    {
        [Key]
        [Column("id_tipo_documento")]
        public int Id { get; set; }

        [Column("ds_tipo_documento")]
        public string Descricao { get; set; }
    }
}
