using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Url
    {
        [Column("id_menu_url")]
        public int Id { get; set; }

        [Column("ds_area")]
        public string Area { get; set; }

        [Column("ds_controller")]
        public string Controler { get; set; }

        [Column("ds_action")]
        public string Action { get; set; }

        [Column("ds_url")]
        public string DescricaoUrl { get; set; }
    }
}
