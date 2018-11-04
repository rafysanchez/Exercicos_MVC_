namespace Sids.Prodesp.Model.Entity.Reserva
{
    using Interface;
    using Interface.Base;

    public abstract class AbstractMes : IMes
    {
        public abstract int Codigo { get; set; }
        public abstract string Descricao { get; set; }
        public abstract decimal ValorMes { get; set; }
        public abstract int Id { get; set; }
    }
}