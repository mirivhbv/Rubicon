namespace Rubicon.Services
{
    public class IntersectionService(IRepository<Rectangle> rectangleRepository) : IIntersectionService
    {
        public async Task<IEnumerable<Rectangle>> GetIntersectedRectangles(double x1, double y1, double x2, double y2)
        {
            var segmentLine = new LineString(new[]
            {
                new Coordinate(x1, y1),
                new Coordinate(x2, y2)
            });

            var rectangles = await rectangleRepository.Query()
                .Where(r => r.Geometry.Intersects(segmentLine))
                .ToListAsync();

            return rectangles;
        }
    }
}
