using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;
using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.ListaBoletos
{
    public class ListaBoletos : Base.PagamentoContaUnica, IPagamentoContaUnicaSiafem
    {
        public ListaBoletos()
        {
            ListaCodigoBarras = new List<ListaCodigoBarras>();
            DocumentoTipo = new DocumentoTipo();
        }

        [Key]
        [Column("id_lista_de_boletos")]
        public override int Id { get; set; }

        [Column("nr_siafem_siafisico")]
        public string NumeroSiafem { get; set; }

        [Column("fl_sistema_siafem_siafisico")]
        public bool TransmitirSiafem { get; set; }

        [Column("fl_transmissao_transmitido_siafem_siafisico")]
        public bool TransmitidoSiafem { get; set; }
        [Column("dt_transmissao_transmitido_siafem_siafisico")]
        public DateTime DataTransmitidoSiafem { get; set; }

        [Column("ds_transmissao_mensagem_siafem_siafisico")]
        public string MensagemServicoSiafem { get; set; }

        [Column("cd_transmissao_status_siafem_siafisico")]
        public string StatusSiafem { get; set; }

        [Column("nr_cnpj_favorecido")]
        public string NumeroCnpjcpfFavorecido{ get; set; }

        [Column("ds_nome_da_lista")]
        public string NomeLista { get; set; }

        [Column("ds_copiar_da_lista")]
        public string CopiarLista { get; set; }

        [Column("nr_total_de_credores")]
        public int TotalCredores { get; set; }

        [Column("vl_total_da_lista")]
        public decimal ValorTotalLista { get; set; }

        [Column("cd_unidade_gestora")]
        public string CodigoUnidadeGestora { get; set; }

        [Column("cd_gestao")]
        public string CodigoGestao { get; set; }

        public IEnumerable<ListaCodigoBarras> ListaCodigoBarras { get; set; }

        [NotMapped]
        public string CodigoDeBarras { get; set; }

        [NotMapped]
        public int TipoBoletoId { get; set; }

    }
}
