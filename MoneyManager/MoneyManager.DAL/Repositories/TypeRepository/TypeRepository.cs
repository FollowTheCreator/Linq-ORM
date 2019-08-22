using MoneyManager.DAL.Interfaces.Models;
using MoneyManager.DAL.Interfaces.Repositories.TypeRepository;
using MoneyManager.DAL.Models.Contexts;

namespace MoneyManager.DAL.Repositories.TypeRepository
{
    public class TypeRepository : Repository<Type, int>, ITypeRepository
    {
        public TypeRepository(MoneyManagerContext context)
            :base(context)
        { }
    }
}
