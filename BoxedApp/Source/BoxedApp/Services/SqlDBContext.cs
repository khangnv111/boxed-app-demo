namespace BoxedApp.Services;

using BoxedApp.Models;
using Microsoft.EntityFrameworkCore;

public class SqlDBContext : DbContext
{
    public DbSet<BookDB> Books { get; set; } = null!;

    public SqlDBContext(DbContextOptions<SqlDBContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<BookDB>().ToTable("Books");
}
