using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento
{
    [Table("tb_tipo_desdobramento")]
    public class DesdobramentoTipo: ITipoPagamentoContaUnica
    {
        [Key]
        [Column("id_tipo_desdobramento")]
        public int Id { get; set; }

        [Column("ds_tipo_desdobramento")]
        public string Descricao { get; set; }
    }
}
