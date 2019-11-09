using Microsoft.EntityFrameworkCore;
using WebApplication.Shared;
using WebApplication.Models;

namespace WebApplication.Database
{
    public class {{Name}}DbContext : DbContext
    {
        public {{Name}}DbContext(DbContextOptions<{{Name}}DbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(GlobalSettings.ConnectionString);
        public DbSet<{{Name}}> {{Name}} { get; set; }
    }
}


