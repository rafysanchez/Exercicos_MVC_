namespace Sids.Prodesp.UI.Areas.PagamentoContaDer.Models
{
    using Model.ValueObject;
    using System.ComponentModel.DataAnnotations;

    public class DadoAssinaturaViewModel
    {
        [Display(Name = "Código da Assinatura")]
        public string CodigoAutorizadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        public string CodigoAutorizadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        public string CodigoAutorizadoOrgao { get; set; }

        [Display(Name = "Nome")]
        public string NomeAutorizadoAssinatura { get; set; }

        [Display(Name = "Cargo")]
        public string DescricaoAutorizadoCargo { get; set; }


        [Display(Name = "Código da Assinatura")]
        public string CodigoExaminadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        public string CodigoExaminadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        public string CodigoExaminadoOrgao { get; set; }

        [Display(Name = "Nome")]
        public string NomeExaminadoAssinatura { get; set; }

        [Display(Name = "Cargo")]
        public string DescricaoExaminadoCargo { get; set; }


        [Display(Name = "Código da Assinatura")]
        public string CodigoResponsavelAssinatura { get; set; }

        [Display(Name = "Grupo")]
        public string CodigoResponsavelGrupo { get; set; }

        [Display(Name = "Órgão")]
        public string CodigoResponsavelOrgao { get; set; }

        [Display(Name = "Nome")]
        public string NomeResponsavelAssinatura { get; set; }

        [Display(Name = "Cargo")]
        public string DescricaoResponsavelCargo { get; set; }
        
        
        public DadoAssinaturaViewModel CreateInstance(Assinatura autorizado, Assinatura examinado, Assinatura responsavel)
        {
            return new DadoAssinaturaViewModel()
            {
                CodigoAutorizadoAssinatura = autorizado.CodigoAssinatura,
                CodigoAutorizadoGrupo = autorizado.CodigoGrupo > 0 ? autorizado.CodigoGrupo.ToString() : default(string),
                CodigoAutorizadoOrgao = autorizado.CodigoOrgao,
                NomeAutorizadoAssinatura = autorizado.NomeAssinatura,
                DescricaoAutorizadoCargo = autorizado.DescricaoCargo,

                CodigoExaminadoAssinatura = examinado.CodigoAssinatura,
                CodigoExaminadoGrupo = examinado.CodigoGrupo > 0 ? examinado.CodigoGrupo.ToString() : default(string),
                CodigoExaminadoOrgao = examinado.CodigoOrgao,
                NomeExaminadoAssinatura = examinado.NomeAssinatura,
                DescricaoExaminadoCargo = examinado.DescricaoCargo,

                CodigoResponsavelAssinatura = responsavel.CodigoAssinatura,
                CodigoResponsavelGrupo = responsavel.CodigoGrupo > 0 ? responsavel.CodigoGrupo.ToString() : default(string),
                CodigoResponsavelOrgao = responsavel.CodigoOrgao,
                NomeResponsavelAssinatura = responsavel.NomeAssinatura,
                DescricaoResponsavelCargo = responsavel.DescricaoCargo,
            };
        }
    }
}