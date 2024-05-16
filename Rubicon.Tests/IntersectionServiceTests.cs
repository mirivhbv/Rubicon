using Microsoft.EntityFrameworkCore;
using Rubicon.Data;
using Rubicon.Entities;
using Rubicon.Services;

namespace Rubicon.Tests;

public class IntersectionServiceTests
{
    [Fact]
    public async Task GetIntersectingRectangles_ReturnsIntersectingRectangles()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase1")
            .Options;
        await using (var context = new AppDbContext(contextOptions))
        {
            context.Rectangles.Add(Rectangle.FromCoordinates(0, 0, 2, 2));
            context.Rectangles.Add(Rectangle.FromCoordinates(1, 1, 3, 3));
            context.Rectangles.Add(Rectangle.FromCoordinates(3, 3, 5, 5));
            await context.SaveChangesAsync();
        }

        await using (var context = new AppDbContext(contextOptions))
        {
            var repo = new Repository<Rectangle>(context);
            var sut = new IntersectionService(repo);

            // Act
            var result = await sut.GetIntersectedRectangles(3, 3, 4, 4);

            // Assert
            Assert.Equal(2, result.ToList().Count);
        }
    }

    [Fact]
    public async Task GetIntersectingRectangles_NoIntersections_ReturnsEmptyList()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDatabase2")
            .Options;
        // Arrange
        await using (var context = new AppDbContext(contextOptions))
        {
            context.Rectangles.Add(Rectangle.FromCoordinates(0, 0, 2, 2));
            await context.SaveChangesAsync();
        }

        await using (var context = new AppDbContext(contextOptions))
        {
            var repo = new Repository<Rectangle>(context);
            var sut = new IntersectionService(repo);

            // Act
            var result = await sut.GetIntersectedRectangles(3, 3, 4, 4);

            // Assert
            Assert.Empty(result); // Should return an empty list
        }
    }
}