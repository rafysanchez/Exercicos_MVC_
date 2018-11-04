using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Reserva
{
    public class ChaveCicsmo
    {
        [Column("id_chave")]
        [Display(Name = "Código")]
        public int Codigo { get; set; }

        [Column("ds_chave")]
        [Display(Name = "Chave")]
        public string Chave { get; set; }

        [Column("ds_senha")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
