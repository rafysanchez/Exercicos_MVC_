using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Log
{
    public class LogResultado
    {
        public LogResultado()
        {
            
        }

        [Column("id_resultado")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("ds_resultado")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
