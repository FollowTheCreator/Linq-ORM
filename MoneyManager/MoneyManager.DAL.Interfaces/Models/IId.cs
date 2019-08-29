namespace MoneyManager.DAL.Interfaces.Models
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
