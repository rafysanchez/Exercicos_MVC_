using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ProgramacaoDesembolso
{
    public class ProgramacaoDesembolso : ProgramacaoDesembolsoBase
    {
        public ProgramacaoDesembolso()
        {
            ProgramacaoDesembolsoTipo = new ProgramacaoDesembolsoTipo();
            Eventos = new List<ProgramacaoDesembolsoEvento>();
            Agrupamentos = new List<ProgramacaoDesembolsoAgrupamento>();
            DataEmissao = DateTime.Now;
        }

        [Column("id_programacao_desembolso")]
        public override int Id { get; set; }

        [ForeignKey("ProgramacaoDesembolsoTipoId")]
        public ProgramacaoDesembolsoTipo ProgramacaoDesembolsoTipo { get; set; }
        public IEnumerable<ProgramacaoDesembolsoEvento> Eventos { get; set; }
        public IEnumerable<ProgramacaoDesembolsoAgrupamento> Agrupamentos { get; set; }
        [NotMapped]
        public Regional Regional { get; set; }
    }
}
