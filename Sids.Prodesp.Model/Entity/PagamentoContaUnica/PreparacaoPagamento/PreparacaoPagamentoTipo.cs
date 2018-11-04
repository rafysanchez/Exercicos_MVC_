using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.PreparacaoPagamento
{
    public class PreparacaoPagamentoTipo: ITipoPagamentoContaUnica
    {
        [Column("id_tipo_preparacao_pagamento")]
        public int Id { get; set; }
        [Column("ds_tipo_preparacao_pagamento")]
        public string Descricao { get; set; }
    }
}
