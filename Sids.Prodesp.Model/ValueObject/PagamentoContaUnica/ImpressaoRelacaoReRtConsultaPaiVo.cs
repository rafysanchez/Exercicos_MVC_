using Sids.Prodesp.Model.Entity.PagamentoContaUnica.ImpressaoRelacaoRERT;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using System;

namespace Sids.Prodesp.Model.ValueObject.PagamentoContaUnica
{
    public class ImpressaoRelacaoReRtConsultaPaiVo : IPagamentoContaUnica
    {
        public bool CadastroCompleto { get; set; }

        public string CodigoAplicacaoObra { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        public DateTime DataEmissao { get; set; }

        public DocumentoTipo DocumentoTipo { get; set; }

        public int DocumentoTipoId { get; set; }

        public int Id { get; set; }

        public string NumeroContrato { get; set; }

        public string NumeroDocumento { get; set; }

        public int RegionalId { get; set; }

        [Display(Name = "Mensagem Retornada da Transmissão")]
        public string MsgRetornoTransmissaoSiafem { get; set; }

        [Display(Name = "Unidade Gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Display(Name = "Gestão")]
        public string CodigoGestao { get; set; }

        [Display(Name = "Banco")]
        public string CodigoBanco { get; set; }

        public bool FlagTransmitidoSiafem { get; set; }

        public DateTime DataTransmissaoSiafem { get; set; }
        
        public IEnumerable<ImpressaoRelacaoReRtConsultaVo> Filhos { get; set; }

        public ImpressaoRelacaoReRtConsultaPaiVo()
        {
            Filhos = new List<ImpressaoRelacaoReRtConsultaVo>();
        }
    }
}
