using Enlighten.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Enlighten.Data.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //dotnet-ef migrations [SUMMARY] --project pupilpal.data --startup-project pupilpal.web

            //dotnet-ef database update  --project pupilpal.data --startup-project pupilpal.web
        }

        public DbSet<Course> Schools { get; set; }
        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<TextbookChapter> TextbookChapters { get; set; }

    }
}
