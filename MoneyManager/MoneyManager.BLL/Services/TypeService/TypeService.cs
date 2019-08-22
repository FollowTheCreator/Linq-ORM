using AutoMapper;
using MoneyManager.BLL.Interfaces.Models.Type;
using MoneyManager.BLL.Interfaces.Services.TypeService;
using MoneyManager.DAL.Interfaces.Repositories.TypeRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyManager.BLL.Services.TypeService
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _repository;

        private readonly IMapper _mapper;

        public TypeService(ITypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(Type item)
        {
            var convertedItem = _mapper.Map<Type, DAL.Interfaces.Models.Type>(item);
            await _repository.CreateAsync(convertedItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Type>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            var convertedItems = _mapper.Map<IEnumerable<DAL.Interfaces.Models.Type>, IEnumerable<Type>>(items);

            return convertedItems;
        }

        public async Task<Type> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            var convertedItem = _mapper.Map<DAL.Interfaces.Models.Type, Type>(item);

            return convertedItem;
        }

        public async Task UpdateAsync(Type item)
        {
            var convertedItem = _mapper.Map<Type, DAL.Interfaces.Models.Type>(item);
            await _repository.UpdateAsync(convertedItem);
        }
    }
}
