using System.Threading.Tasks;

namespace MoneyManager.DAL.Migrations.DataSeeding
{
    public interface IDataSeeding
    {
        Task SeedData();
    }
}
