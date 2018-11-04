using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento
{
    public class Credor
    {
        [Key]
        [Column("id_credor")]
        public int Id { get; set; }

        [Column("nm_prefeitura")]
        public string Prefeitura { get; set; }

        [Column("cd_cpf_cnpj_ug_credor")]
        public string CpfCnpjUgCredor { get; set; }

        [Column("bl_conveniado")]
        public bool Conveniado { get; set; }

        [Column("bl_base_calculo")]
        public bool BaseCalculo { get; set; }

        [Column("nm_reduzido_credor")]
        public string NomeReduzidoCredor { get; set; }

        [Column("nr_cnpj_credor")]
        public string CnpjCredor { get; set; }

    }
}
