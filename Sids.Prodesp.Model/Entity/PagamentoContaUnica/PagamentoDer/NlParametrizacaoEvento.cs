using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PagamentoDer
{
    public class NlParametrizacaoEvento
    {
        public NlParametrizacaoEvento()
        {
            ValidaCampos();
        }

        [Column("id_evento")]
        public int Id { get; set; }

        [Column("id_nl_parametrizacao")]
        public int IdNlParametrizacao { get; set; }

        [Column("id_rap_tipo")]
        public string IdRapTipo { get; set; }

        [Column("id_documento_tipo")]
        public int IdDocumentoTipo { get; set; }

        [Column("nr_evento")]
        public string NumeroEvento { get; set; }

        [Column("nr_classificacao")]
        public string NumeroClassificacao { get; set; }

        [Column("ds_entrada_saida")]
        public string EntradaSaidaDescricao { get; set; }

        [Column("nr_fonte")]
        public string NumeroFonte { get; set; }

        public void ValidaCampos() {
            this.NumeroEvento = string.IsNullOrEmpty(NumeroFonte) ? string.Empty : this.NumeroFonte;
           this.NumeroClassificacao = string.IsNullOrEmpty(NumeroClassificacao) ? string.Empty : this.NumeroClassificacao;
            this.NumeroFonte = string.IsNullOrEmpty(NumeroFonte) ? string.Empty : this.NumeroFonte;
        }
    }
}
