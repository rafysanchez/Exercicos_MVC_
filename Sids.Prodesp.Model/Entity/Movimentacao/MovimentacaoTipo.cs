using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoTipo
    {
        public MovimentacaoTipo() { }

        public MovimentacaoTipo(
            int _id,
            string _descricao)
        {
            Id = _id;
            Descricao = _descricao;
        }

        [Column("id_tipo_movimentacao_orcamentaria")]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Column("ds_tipo_movimentacao_orcamentariao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


    }
}
