using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso
{
    public class ProgramacaoDesembolsoTipo : ITipoPagamentoContaUnica
    {
        [Column("id_tipo_programacao_desembolso")]
        public int Id { get; set; }
        [Column("ds_tipo_programacao_desembolso")]
        public string Descricao { get; set; }
    }
}
