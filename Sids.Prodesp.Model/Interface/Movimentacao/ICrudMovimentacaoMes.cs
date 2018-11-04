namespace Sids.Prodesp.Model.Interface.Movimentacao
{
    using Base;
    using Interface.Base;
    using Model.Entity.Movimentacao;
    using System.Collections.Generic;

    public interface ICrudMovimentacaoMes : ICrudBase<MovimentacaoMes>
    {

        IEnumerable<MovimentacaoMes> FetchCancelamento(MovimentacaoMes entity);

        IEnumerable<MovimentacaoMes> FetchDistribuicao(MovimentacaoMes entity);

        IEnumerable<MovimentacaoMes> FetchReducaoSuplementacao(MovimentacaoMes entity);

      

    }
}
