using CurrencyExchange.Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseContext _dbContext;

        public UnitOfWork(IDatabaseContext context)
        {
            _dbContext = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            int num = await _dbContext.SaveChangesAsync(new CancellationToken());
            return num;
        }

        public async Task ReloadEntities()
        {
            await _dbContext.ReloadEntities();
        }
    }
}
