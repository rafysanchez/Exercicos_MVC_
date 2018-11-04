using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoDocumentoTipo
    {
        /// <summary>
        /// Construtor Padrão.
        /// </summary>
        public MovimentacaoDocumentoTipo() { }

        public MovimentacaoDocumentoTipo(
            int _id,
            string _descricao)
        {
            Id = _id;
            Descricao = _descricao;
        }

        [Column("id_tipo_documento_movimentacao")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_tipo_documento_movimentacao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

   
    }
}
