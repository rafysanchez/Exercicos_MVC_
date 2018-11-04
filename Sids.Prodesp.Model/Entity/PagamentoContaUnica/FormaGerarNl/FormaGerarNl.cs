using Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer;
using Sids.Prodesp.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.FormaGerarNl
{
    [Table("tb_parametrizacao_forma_gerar_nl")]
    public class FormaGerarNl
    {

        public FormaGerarNl()
        {
            tipoDespesa = new NlParametrizacaoDespesaTipo();
        }

        [Key]
        [Column("id_parametrizacao_forma_gerar_nl")]
        public int Id { get; set; }
        [Column("ds_gerar_nl")]
        public string Descricao { get; set; }
        [Column("id_despesa_tipo")]
        public int IdDespesaTipo { get; set; }
        [Column("cd_despesa_tipo")]
        public int CodigoTipoDespesa { get; set; }
        [NotMapped]
        public EnumFormaGerarNl FormaGerar { get { return (EnumFormaGerarNl)Id; } }
        [NotMapped]
        public NlParametrizacaoDespesaTipo tipoDespesa { get; set; }
        [Column("id_nl_parametrizacao")]
        public int IdNlParametrizacao { get; set; }
        [Column("id_nl_tipo")]
        public int numeroTipoNl { get; set; }
    }
}
