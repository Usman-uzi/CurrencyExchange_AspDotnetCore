using CurrencyExchange.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchange.Business.Interfaces
{
    public interface IAuditService : IGenericService<AuditModel, int>
    {
        Task<List<AuditModel>> GetFiltered(DateTime? startdate, DateTime? enddate);
    }
}
