using Sids.Prodesp.Core.Services.Configuracao;
using Sids.Prodesp.Core.Services.Reserva;
using Sids.Prodesp.Core.Services.WebService;
using Sids.Prodesp.Core.Services.WebService.Empenho;
using Sids.Prodesp.Infrastructure.Services.Empenho;
using Sids.Prodesp.Infrastructure.Services.Reserva;
using Sids.Prodesp.Model.Base.Empenho;
using Sids.Prodesp.Model.Entity.Seguranca;
using Sids.Prodesp.Model.Enum;
using Sids.Prodesp.Model.Interface;
using Sids.Prodesp.Model.Interface.Base;
using Sids.Prodesp.Model.Interface.Configuracao;
using Sids.Prodesp.Model.Interface.Log;
using Sids.Prodesp.Model.Interface.Reserva;
using Sids.Prodesp.Model.Interface.Seguranca;
using Sids.Prodesp.Model.Interface.Service;
using Sids.Prodesp.Model.Interface.Service.Empenho;
using System.Collections.Generic;

namespace Sids.Prodesp.Core.Services.Empenho
{
    public class EmpenhoBaseService<TEmpenho, TItem, TMes> : CommonService
        where TEmpenho : BaseEmpenho
        where TItem : BaseEmpenhoItem
    {
        private readonly ProdespEmpenhoService _prodesp;
        private readonly SiafemEmpenhoService _siafem;
        private readonly ChaveCicsmoService _chave;
        private readonly ProgramaService _programa;
        private readonly IEmpenhoItemService<TItem> _item;


        public EmpenhoBaseService(ILogError l,
            IProdespEmpenho prodesp, ISiafemEmpenho siafem, ICrudPrograma programa,
            ICrudFonte fonte, ICrudEstrutura estrutura, IRegional regional, IChaveCicsmo chave, ICommon c, IEmpenhoItemService<TItem> itemService)
            : base(l, c, new ProdespReservaWs(), new SiafemReservaWs(), new SiafemEmpenhoWs(), chave)
        {
            _prodesp = new ProdespEmpenhoService(l, prodesp, programa, fonte, estrutura, regional);
            _siafem = new SiafemEmpenhoService(l, siafem, programa, fonte, estrutura);
            _chave = new ChaveCicsmoService(l, chave);
            _programa = new ProgramaService(l, programa, estrutura);

            _item = itemService;
        }

        protected string TransmitirEmpenho(TEmpenho objModel, Usuario usuario, string ug, List<IMes> months, IEnumerable<TItem> items, EnumAcaoSiaf acaoSiaf, string senha)
        {
            var numeroCT = _siafem.TransmitirEmpenho(EnumTipoServicoFazenda.Siafisico, acaoSiaf, usuario.CPF, senha, objModel, months, items, ug);

            return numeroCT;
        }

        protected void TransmitirItens(EnumTipoServicoFazenda servico, TEmpenho objModel, IEnumerable<TItem> itens, Usuario usuario, string ug, int resource)
        {

            foreach (TItem empenhoItem in itens)
            {
                if (empenhoItem.StatusSiafisicoItem == "N" || empenhoItem.StatusSiafisicoItem == "E")
                {
                    TransmitirItem(servico, EnumAcaoSiaf.Inserir, objModel, usuario, empenhoItem, ug, resource);
                }
                else if (empenhoItem.StatusSiafisicoItem == "S")
                {
                    TransmitirItem(servico, EnumAcaoSiaf.Alterar, objModel, usuario, empenhoItem, ug, resource);
                }
            }
        }

        private void TransmitirItem(EnumTipoServicoFazenda servico, EnumAcaoSiaf acao, TEmpenho objModel, Usuario usuario, TItem item, string ug, int resource)
        {
            try
            {
                item.StatusSiafisicoItem = "E";

                item.SequenciaItem = _siafem.TransmitirEmpenhoItem(servico, acao, usuario.CPF, Decrypt(usuario.SenhaSiafem), objModel, item, ug);

                item.StatusSiafisicoItem = "S";
            }
            finally
            {
                _item.Salvar(item, resource, (int)EnumAcao.Transmitir);
            }
        }
    }
}
