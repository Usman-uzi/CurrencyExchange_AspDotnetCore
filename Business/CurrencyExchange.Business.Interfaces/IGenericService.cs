using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Business.Interfaces
{
    public interface IGenericService<TBusinessModel, TKey>
    {
        Task<List<TBusinessModel>> Get();
        Task<TBusinessModel> Get(TKey id);
        Task<TBusinessModel> Add(TBusinessModel entity);
        Task Update(TBusinessModel entity);
        Task Delete(TBusinessModel entity);
    }
}
