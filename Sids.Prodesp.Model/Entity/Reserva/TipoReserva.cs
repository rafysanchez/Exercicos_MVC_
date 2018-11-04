using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Reserva
{
    public class TipoReserva
    {
        public TipoReserva()
        {
        }

        /// <summary>
        /// Código de tipo de reserva
        /// </summary>
        [Display(Name = "Código")]
        [Column("id_tipo_reserva")]
        public int Codigo { get; set; }


        /// <summary>
        /// Descrição do tipo da reserva
        /// </summary>
        [Display(Name = "Descrição tipo de reserva")]
        [Column("ds_tipo_reserva")]
        public string Descricao { get; set; }
        
    }
}
