using System.Collections.Generic;

namespace Sids.Prodesp.UI.Areas.Seguranca.Models.Perfil
{

    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public List<FuncionalidadeAcaoViewModel> FuncionalidadeAcoes { get; set; }

    }
    public class FuncionalidadeAcaoViewModel
    {
        public int Id { get; set; }
        public int Menu { get; set; }
        public string Funcionalidade { get; set; }
        public List<AcaoViewModel> Acoes { get; set; }
    }
    public class AcaoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int FuncionalidadeAcaoId { get; set; }
        public int FuncionalidadeId { get; set; }
        public bool Associado { get; set; }

    }
}
