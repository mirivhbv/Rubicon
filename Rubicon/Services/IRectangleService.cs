namespace Rubicon.Services;

public interface IRectangleService
{
    /// <summary>
    /// Determines whether rectangle intersects with given segment points.
    /// </summary>
    /// <param name="rectangle"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    bool Intersects(Rectangle rectangle, SegmentViewModel model);
}