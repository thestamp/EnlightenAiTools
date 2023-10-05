using Enlighten.Data.Models;
using Enlighten.Data.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Enlighten.Data.Infrastructure
{
    public class DataContext : DetachedDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //required LocalDB - https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16#install-localdb
            //dotnet-ef migrations add [SUMMARY] --project Enlighten.Data --startup-project Enlighten.Study.Web --context DataContext
            //dotnet-ef database update --project Enlighten.Data --startup-project Enlighten.Study.Web --context DataContext
        }

        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<TextbookUnit> TextbookUnits { get; set; }
        public DbSet<GptDataSettingsModel> GptDataSettings { get; set; }

    }
}
