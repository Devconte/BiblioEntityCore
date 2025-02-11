using Microsoft.EntityFrameworkCore;

namespace BiblioEntityCore.Class;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Constants.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ajout d'une relation one to many.
        modelBuilder.Entity<Book>()
            .HasOne(a => a.Author)
            .WithMany(b => b.Books)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}