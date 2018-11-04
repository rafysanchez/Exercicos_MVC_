using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class NlParametrizacao
    {
        [Column("id_nl_parametrizacao")]
        public int Id { get; set; }

        [Column("id_nl_tipo")]
        public int Tipo { get; set; }

        [Column("id_parametrizacao_forma_gerar_nl")]
        public int IdFormaGerarNl { get; set; }

        [Column("ds_observacao")]
        public string Observacao { get; set; }

        [Column("bl_transmitir")]
        public bool Transmitir { get; set; }

        [Column("nr_favorecida_cnpjcpfug")]
        public string FavorecidaCnpjCpfUg { get; set; }

        [Column("nr_favorecida_gestao")]
        public int FavorecidaNumeroGestao { get; set; }

        [Column("nr_unidade_gestora")]
        public int? UnidadeGestora { get; set; }

        public IEnumerable<NlParametrizacaoEvento> Eventos { get; set; }
        public IEnumerable<NlParametrizacaoDespesa> Despesas { get; set; }
    }
}
