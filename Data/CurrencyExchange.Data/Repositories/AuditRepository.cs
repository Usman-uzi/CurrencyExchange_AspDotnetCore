using CurrencyExchange.Data.Interfaces;
using CurrencyExchange.Data.Models;

namespace CurrencyExchange.Data
{
    public class AuditRepository : GenericRepository<Audit, int>, IAuditRepository
    {
        public AuditRepository(IDatabaseContext dbContext) : base(dbContext)
        { }
    }
}
