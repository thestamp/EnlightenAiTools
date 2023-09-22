using Enlighten.Data.Models;
using Enlighten.Data.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Enlighten.Data.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //required LocalDB - https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16#install-localdb
            //dotnet-ef migrations add [SUMMARY] --project Enlighten.Data --startup-project Enlighten.Study.Web
            //dotnet-ef database update --project Enlighten.Data --startup-project Enlighten.Study.Web
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<TextbookUnit> TextbookUnits { get; set; }
        public DbSet<GptDataSettingsModel> GptDataSettings { get; set; }


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
