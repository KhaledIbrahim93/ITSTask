using Microsoft.EntityFrameworkCore;
namespace ItemsDAL.Model
{
    public class Context : DbContext
    {
        public Context()
        { }

        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ProductDb;Trusted_Connection=True;");
            }
       }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(e => {
                e.ToTable("Item");
                e.HasKey(s => s.Id);
                e.Property(p => p.Title).HasColumnName("Title");
                e.Property(p => p.Description).HasColumnName("Description");


            });
        }
        public DbSet<Item> Items { get; set; }

    }
}
