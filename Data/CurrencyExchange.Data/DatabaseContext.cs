using CurrencyExchange.Data.Interfaces;
using CurrencyExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class DatabaseContext : DbContext, IDatabaseContext, IDisposable
    {
        public DbSet<Audit> Audits { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        public DatabaseContext()
        { }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public async Task ReloadEntities()
        {
            List<EntityEntry> entries = ChangeTracker.Entries().ToList();
            for (int i = 0; i < entries.Count; ++i)
                await entries[i].ReloadAsync(new CancellationToken());
        }
    }
}
