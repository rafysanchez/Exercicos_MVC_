using System.Collections.Generic;
using Sids.Prodesp.Model.Entity.Seguranca;

namespace Sids.Prodesp.UI.Areas.Seguranca.Models.Perfil
{
    public class DtoFuncionalidadeAcaoViewModel
    {
        public List<Sids.Prodesp.Model.Entity.Seguranca.Funcionalidade> Funcionalidade { get; set; }
        public List<Acao> Acao { get; set; }
        public List<FuncionalidadeAcao> FuncionalidadeAcao { get; set; }
    }
}