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


        public async Task UpdateEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Check if the entity is already being tracked
            var local = Set<T>().Local
                .FirstOrDefault(entry => entry.Equals(entity));

            // If the entity is found locally (is tracked), then detach it.
            if (local != null)
            {
                Entry(local).State = EntityState.Detached;
            }

            // Attach the provided entity to the context.
            Set<T>().Attach(entity);

            // Set the entity's state to Modified.
            Entry(entity).State = EntityState.Modified;

            // NOTE: This method doesn't call SaveChangesAsync by design.
            // Remember to call SaveChanges or SaveChangesAsync separately.
        }

        public async Task AddEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            Set<T>().Add(entity);

            // NOTE: This method doesn't call SaveChangesAsync by design.
            // Remember to call SaveChanges or SaveChangesAsync separately.
        }

        public async Task DeleteEntity<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var local = Set<T>().Local.FirstOrDefault(entry => entry.Equals(entity));

            // If the entity is not tracked locally, attach it
            if (local == null)
            {
                Set<T>().Attach(entity);
            }

            Set<T>().Remove(entity);

            // NOTE: This method doesn't call SaveChangesAsync by design.
            // Remember to call SaveChanges or SaveChangesAsync separately.
        }

    }
}
