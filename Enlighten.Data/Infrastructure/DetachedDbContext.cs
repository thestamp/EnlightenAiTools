using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enlighten.Data.Infrastructure
{
    public class DetachedDbContext : DbContext
    {
        public DetachedDbContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        public void AddEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set<T>().Add(entity);
        }

        public void UpdateEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set<T>().Attach(entity);
            Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set<T>().Remove(entity);
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            Reset();  // or Clear(), Complete(), FinalizeUoW(), or whatever name you chose
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            Reset();  // or Clear(), Complete(), FinalizeUoW(), or whatever name you chose
            return result;
        }

        public void Reset()
        {
            var entries = ChangeTracker.Entries().ToList();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
