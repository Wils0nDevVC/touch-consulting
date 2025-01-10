using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Persistence 
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }

}