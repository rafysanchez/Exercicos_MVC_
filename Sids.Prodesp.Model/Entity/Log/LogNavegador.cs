using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Log
{
    public class LogNavegador
    {
        [Column("id_navegador")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_navegador")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
