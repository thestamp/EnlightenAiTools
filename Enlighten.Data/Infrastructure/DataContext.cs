using Enlighten.Data.Models;
using Enlighten.Data.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Enlighten.Data.Infrastructure
{
    public class DataContext : DetachedDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
          
        }

        public DbSet<Textbook> Textbooks { get; set; }
        public DbSet<TextbookUnit> TextbookUnits { get; set; }
        public DbSet<GptDataSettingsModel> GptDataSettings { get; set; }

    }
}
