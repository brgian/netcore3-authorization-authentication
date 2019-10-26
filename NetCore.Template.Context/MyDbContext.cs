using NetCore.Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace NetCore.Template.Context
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyEntity>()
              .HasIndex(x => x.Key);
        }

        public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}
