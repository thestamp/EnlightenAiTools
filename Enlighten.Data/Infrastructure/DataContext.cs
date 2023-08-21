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

        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<TextbookChapter> TextbookChapters { get; set; }
        public DbSet<GptDataSettingsModel> GptDataSettings { get; set; }

    }
}
