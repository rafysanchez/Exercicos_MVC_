using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sids.Prodesp.Model.ValueObject
{
    public class AssinaturasVo
    {
        [Column("cd_autorizado_assinatura")]
        public string CodigoAutorizadoAssinatura { get; set; }
        
        [Column("cd_autorizado_grupo")]
        public int CodigoAutorizadoGrupo { get; set; }
        
        [Column("cd_autorizado_orgao")]
        public string CodigoAutorizadoOrgao { get; set; }
        
        [Column("ds_autorizado_cargo")]
        public string DescricaoAutorizadoCargo { get; set; }
        
        [Column("nm_autorizado_assinatura")]
        public string NomeAutorizadoAssinatura { get; set; }
        


        [Column("cd_examinado_assinatura")]
        public string CodigoExaminadoAssinatura { get; set; }
        
        [Column("cd_examinado_grupo")]
        public int CodigoExaminadoGrupo { get; set; }
        
        [Column("cd_examinado_orgao")]
        public string CodigoExaminadoOrgao { get; set; }
        
        [Column("ds_examinado_cargo")]
        public string DescricaoExaminadoCargo { get; set; }
        
        [Column("nm_examinado_assinatura")]
        public string NomeExaminadoAssinatura { get; set; }
        


        [Column("cd_responsavel_assinatura")]
        public string CodigoResponsavelAssinatura { get; set; }
        
        [Column("cd_responsavel_grupo")]
        public int CodigoResponsavelGrupo { get; set; }
        
        [Column("cd_responsavel_orgao")]
        public string CodigoResponsavelOrgao { get; set; }
        
        [Column("ds_responsavel_cargo")]
        public string DescricaoResponsavelCargo { get; set; }
        
        [Column("nm_responsavel_assinatura")]
        public string NomeResponsavelAssinatura { get; set; }
    }
}
