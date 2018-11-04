
namespace Sids.Prodesp.Model.Entity.LiquidacaoDespesa
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ServicoTipo
    {
        [Column("id_servico_tipo")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Serviço")]
        [Column("ds_servico_tipo")]
        public string Descricao { get; set; }

        [Column("cd_rap_tipo")]
        public string TipoRap { get; set; }
    }
}
