namespace MoneyManager.DAL.Interfaces.Models
{
    public interface IId<TId>
    {
        TId Id { get; set; }
    }
}
