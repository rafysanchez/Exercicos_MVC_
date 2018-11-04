namespace Sids.Prodesp.Model.Entity.Empenho
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmpenhoCancelamentoTipo
    {
        [Column("id_empenho_cancelamento_tipo")]
        public int Id { get; set; }

        [Column("ds_empenho_cancelamento_tipo")]
        public string Descricao { get; set; }

        [Column("nm_web_service")]
        public string ServicoWeb { get; set; }

        [Column("fl_siafem")]
        public bool Siafem { get; set; }
    }
}
