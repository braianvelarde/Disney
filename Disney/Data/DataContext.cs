using Disney.Models;
using Microsoft.EntityFrameworkCore;

namespace Disney.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
