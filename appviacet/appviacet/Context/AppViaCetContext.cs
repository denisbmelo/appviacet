using appviacet.Context.Entitys;
using Microsoft.EntityFrameworkCore;

namespace appviacet.Context
{
    public class AppViaCetContext : DbContext
    {
        public AppViaCetContext(DbContextOptions<AppViaCetContext> options) : base (options) 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseInMemoryDatabase("InMemoryTestDatabase");
        }

        public DbSet<Conta> Contas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Conta>().ToTable("Contas");
            modelBuilder.Entity<Conta>().HasKey(c => c.Id);
            
        }


    }
}
