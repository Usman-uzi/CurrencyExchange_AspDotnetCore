
using AutoMapper;
using CurrencyExchange.Business.Interfaces;
using CurrencyExchange.Business.Models;
using CurrencyExchange.Data.Interfaces;
using CurrencyExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Business.Services
{
    public class AuditService : GenericService<Data.Models.Audit, AuditModel, int>, IAuditService
    {

        public AuditService(IMapper mapper, IAuditRepository auditRepository, IUnitOfWork unitOfWork)
            : base(mapper, auditRepository, unitOfWork)
        {
        }

        public async Task<List<AuditModel>> GetFiltered(DateTime? startdate, DateTime? enddate)
        {
            var query = Repository.Get();
            if (startdate != null)
                query = query.Where(x => x.AddedOn >= startdate);
            if (enddate != null)
            {
                enddate = enddate.Value.Date.AddDays(1);
                query = query.Where(x => x.AddedOn < enddate);
            }

            var dataEntities = await query.ToListAsync();
            var businessEntities = Mapper.Map<List<Audit>, List<AuditModel>>(dataEntities);
            return businessEntities;
        }
    }
}
