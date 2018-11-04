using System.ComponentModel.DataAnnotations;
using Sids.Prodesp.Model.Entity.PagamentoContaUnica.Desdobramento;

namespace Sids.Prodesp.UI.Areas.PagamentoContaUnica.Models
{
    public class DadoCredorGridViewModel
    {

        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "CNPJ / CPF / UG Credor")]
        public string CpfcnpjugCredor { get; set; }

        [Display(Name = "Nome Credor")]
        public string  Prefeitura { get; set; }

        [Display(Name = "Conveniado")]
        public string Conveniado { get; set; }

        [Display(Name = "Com Base de Cálculo")]
        public string BaseCalculo { get; set; }

        [Display(Name = "Nome Reduzido")]
        public string NomeReduzidoCredor { get; set; }

        public DadoCredorGridViewModel CreateInstance(Credor entity)
        {
            return new DadoCredorGridViewModel()
            {
                Id = entity.Id,
                CpfcnpjugCredor = entity.CpfCnpjUgCredor,
                Prefeitura = entity.Prefeitura,
                Conveniado = entity.Conveniado?"Sim":"Não",
                BaseCalculo = entity.BaseCalculo ? "Sim" : "Não",
                NomeReduzidoCredor = entity.NomeReduzidoCredor
            };
        }

    }
}