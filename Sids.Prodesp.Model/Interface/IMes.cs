namespace Sids.Prodesp.Model.Interface.Base
{
    public interface IMes
    {
        int Codigo { get; set; }
        int Id { get; set; }
        string Descricao { get; set; }
        decimal ValorMes { get; set; }
    }
}
