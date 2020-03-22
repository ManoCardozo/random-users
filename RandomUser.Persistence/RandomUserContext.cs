using RandomUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace RandomUser.Persistence
{
    public class RandomUserContext : DbContext
    {
        public RandomUserContext(DbContextOptions<RandomUserContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RandomUserContext).Assembly);
        }
    }
}
