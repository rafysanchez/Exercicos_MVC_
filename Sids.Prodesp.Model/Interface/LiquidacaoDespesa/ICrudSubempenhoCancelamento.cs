namespace Sids.Prodesp.Model.Interface.LiquidacaoDespesa
{
    using Base;
    using Model.Entity.LiquidacaoDespesa;
    using System;
    using System.Collections.Generic;

    public interface ICrudSubempenhoCancelamento : ICrudBase<SubempenhoCancelamento>
    {
        IEnumerable<SubempenhoCancelamento> BuscarGrid(SubempenhoCancelamento entity, DateTime since, DateTime until);

        SubempenhoCancelamento GetLastSignatures(SubempenhoCancelamento entity);

        /// <summary>
        /// Caso não exista insere um único Subempenho no repositório, caso contrário o atualiza
        /// </summary>
        /// <param name="entity">O Subemepnho para inserir ou alterar</param>
        /// <returns>A chave de sistema (Id) do subemepnho</returns>
        int Save(SubempenhoCancelamento entity);

        /// <summary>
        /// Seleciona um único Subempenho do repositório de dados
        /// </summary>
        /// <param name="Id">A Chave de sistema (Id) do Subempenho que se deseja selecionar</param>
        /// <returns>O Subempenho, caso encontrado no banco, caso contrário um objeto padrão (default)</returns>
        SubempenhoCancelamento Get(int Id);
    }
}
