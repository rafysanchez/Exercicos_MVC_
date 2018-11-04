using Sids.Prodesp.Model.Interface.PagamentoContaUnica.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sids.Prodesp.Model.Entity.Movimentacao
{
    public class MovimentacaoReducaoSuplementacao : MovimentacaoItemBase
    {
        [Column("id_reducao_suplementacao")]
        public int Id { get; set; }


        [Column("tb_credito_movimentacao_id_nota_credito")]
        public int IdNotaCredito { get; set; }


        [Column("tb_distribuicao_movimentacao_id_distribuicao_movimentacao")]
        public int IdDistribuicao { get; set; }

        [Column("tb_cancelamento_movimentacao_id_cancelamento_movimentacao")]
        public int IdCancelamento { get; set; }

        [Column("tb_programa_id_programa")]
        public int IdPrograma { get; set; }

        [Column("tb_estrutura_id_estrutura")]
        public int IdEstrutura { get; set; }

        [Column("tb_fonte_id_fonte")]
        public int IdFonte { get; set; }

        [Column("tb_movimentacao_orcamentaria_id_movimentacao_orcamentaria")]
        public int IdMovimentacao { get; set; }

        [Column("nr_agrupamento")]
        public int NrAgrupamento { get; set; }


        [Column("nr_seq")]
        public int NrSequencia { get; set; }

        [Column("nr_suplementacao_reducao")]
        public string NrSuplementacaoReducao { get; set; }

        [Column("fl_proc")]
        public string FlProc { get; set; }

        [Column("nr_processo")]
        public string NrProcesso { get; set; }

        [Column("nr_orgao")]
        public string NrOrgao { get; set; }


        [Column("nr_obra")]
        public string NrObra { get; set; }



        [Column("flag_red_sup")]
        public string RedSup { get; set; }



        [Column("nr_cnpj_cpf_ug_credor")]
        public string NrCnpjCpf { get; set; }

        [Column("ds_autorizado_supra_folha")]
        public string AutorizadoSupraFolha { get; set; }

        [Column("cd_origem_recurso")]
        public string OrigemRecurso { get; set; }

        [Column("cd_destino_recurso")]
        public string DestinoRecurso { get; set; }

        [Column("cd_especificacao_despesa")]
        public string EspecDespesa { get; set; }

        [Column("ds_especificacao_despesa")]
        public string DescEspecDespesa { get; set; }


        [Display(Name = "Código da Assinatura")]
        [Column("cd_autorizado_assinatura")]
        public string CodigoAutorizadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_autorizado_grupo")]
        public int CodigoAutorizadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_autorizado_orgao")]
        public string CodigoAutorizadoOrgao { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_autorizado_cargo")]
        public string DescricaoAutorizadoCargo { get; set; }


        [Display(Name = "Nome")]
        [Column("nm_autorizado_assinatura")]
        public string NomeAutorizadoAssinatura { get; set; }



        [Display(Name = "Código da Assinatura")]
        [Column("cd_examinado_assinatura")]
        public string CodigoExaminadoAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_examinado_grupo")]
        public int CodigoExaminadoGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_examinado_orgao")]
        public string CodigoExaminadoOrgao { get; set; }


        [Display(Name = "Cargo")]
        [Column("ds_examinado_cargo")]
        public string DescricaoExaminadoCargo { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_examinado_assinatura")]
        public string NomeExaminadoAssinatura { get; set; }
        


        [Display(Name = "Código da Assinatura")]
        [Column("cd_responsavel_assinatura")]
        public string CodigoResponsavelAssinatura { get; set; }

        [Display(Name = "Grupo")]
        [Column("cd_responsavel_grupo")]
        public int CodigoResponsavelGrupo { get; set; }

        [Display(Name = "Órgão")]
        [Column("cd_responsavel_orgao")]
        public string CodigoResponsavelOrgao { get; set; }

        [Display(Name = "Cargo")]
        [Column("ds_responsavel_cargo")]
        public string DescricaoResponsavelCargo { get; set; }

        [Display(Name = "Nome")]
        [Column("nm_responsavel_assinatura")]
        public string NomeResponsavelAssinatura { get; set; }

        [Column("valor")]
        public decimal Valor { get; set; }

        [Column("cd_unidade_gestora")]
        public string UnidadeGestora { get; set; }

        [Column("cd_gestao_favorecido")]
        public string GestaoFavorecido { get; set; }

        [Column("TotalQ1")]
        public decimal TotalQ1 { get; set; }

        [Column("TotalQ2")]
        public decimal TotalQ2 { get; set; }

        [Column("TotalQ3")]
        public decimal TotalQ3 { get; set; }

        [Column("TotalQ4")]
        public decimal TotalQ4 { get; set; }

        public decimal TotalGeral { get; set; }
        
        public int IdTipoMovimentacao { get; set; }
        public int AnoExercicio { get; set; }
        public int IdTipoDocumento { get; set; }
    }
}
