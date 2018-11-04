using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Area
    {

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public Area() { }

        public Area(
            int _id,
            string _descricao)
        {
            Id = _id;
            Descricao = _descricao;
        }

        [Column("id_area")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_area")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
