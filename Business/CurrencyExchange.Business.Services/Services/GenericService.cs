using AutoMapper;
using CurrencyExchange.Business.Interfaces;
using CurrencyExchange.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Business.Services
{
    public class GenericService<TDataModel, TBusinessModel, TKey> : IGenericService<TBusinessModel, TKey>
        where TBusinessModel : class where TDataModel : class
    {
        public IGenericRepository<TDataModel, TKey> Repository { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }

        public GenericService(IMapper mapper, IGenericRepository<TDataModel, TKey> genericRepository, IUnitOfWork unitOfWork)
        {
            Repository = genericRepository;
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<List<TBusinessModel>> Get()
        {
            var dataEntities = await Repository.GetAsync();
            var businessEntities = Mapper.Map<List<TDataModel>, List<TBusinessModel>>(dataEntities);
            return businessEntities;
        }

        public async Task<TBusinessModel> Get(TKey id)
        {
            var dataEntity = await Repository.GetAsync(id);
            var businessEntity = Mapper.Map<TDataModel, TBusinessModel>(dataEntity);
            return businessEntity;
        }
        public async virtual Task<TBusinessModel> Add(TBusinessModel businessEntity)
        {
            var dataEntity = Mapper.Map<TBusinessModel, TDataModel>(businessEntity);
            dataEntity = await Repository.AddAsync(dataEntity);
            if (UnitOfWork != null)
            {
                await UnitOfWork.SaveChangesAsync();
            }
            businessEntity = Mapper.Map<TDataModel, TBusinessModel>(dataEntity);
            return businessEntity;
        }

        public async Task Update(TBusinessModel businessEntity)
        {
            var dataEntity = Mapper.Map<TBusinessModel, TDataModel>(businessEntity);
            await Repository.UpdateAsync(dataEntity);
            if (UnitOfWork != null)
            {
                await UnitOfWork.SaveChangesAsync();
            }
            //test
        }

        public async Task Delete(TBusinessModel businessEntity)
        {
            var dataEntity = Mapper.Map<TBusinessModel, TDataModel>(businessEntity);
            await Repository.DeleteAsync(dataEntity);
            if (UnitOfWork != null)
            {
                await UnitOfWork.SaveChangesAsync();
            }
        }
    }
}
