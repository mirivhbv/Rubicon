namespace Rubicon.Services;

public interface IIntersectionService
{
    Task<IEnumerable<Rectangle>> GetIntersectedRectangles(double x1, double y1, double x2, double y2);
}