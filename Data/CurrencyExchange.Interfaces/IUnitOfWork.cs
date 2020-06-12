using System.Threading.Tasks;

namespace CurrencyExchange.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task ReloadEntities();
    }
}
