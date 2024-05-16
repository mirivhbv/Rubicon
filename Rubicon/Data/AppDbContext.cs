namespace Rubicon.Data;

public class AppDbContext : DbContext
{
    public DbSet<Rectangle> Rectangles { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}