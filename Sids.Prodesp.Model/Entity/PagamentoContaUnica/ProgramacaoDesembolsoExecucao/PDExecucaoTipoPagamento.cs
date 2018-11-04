using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolsoExecucao
{
    public class PDExecucaoTipoPagamento
    {

        public PDExecucaoTipoPagamento() { }

        [Column("id_programacao_desembolso_execucao_tipo_pagamento")]
        public int Codigo { get; set; }

        [Column("ds_programacao_desembolso_execucao_tipo_pagamento")]
        public string Descricao { get; set; }
    }
}
