﻿namespace Rubicon.Data;

public class AppDbContext : DbContext
{
    public DbSet<Rectangle> Rectangles { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rectangle>()
            .HasIndex(r => r.Geometry)
            .HasMethod("GIST");
    }
}