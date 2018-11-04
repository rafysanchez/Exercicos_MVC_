using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Acao
    {

        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public Acao() { }

        public Acao(
            int _id,
            string _descricao)
        {
            Id = _id;
            Descricao = _descricao;
        }

        [Column("id_acao")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_acao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
    }
}
