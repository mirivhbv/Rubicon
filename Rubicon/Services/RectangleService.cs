namespace Rubicon.Services;

[Obsolete("Switched to using NetTopologySuite library.")]
public class RectangleService(ILogger<RectangleService> logger) : IRectangleService
{
    /// <inheritdoc />
    public bool Intersects(Rectangle rectangle, SegmentViewModel segment)
    {
        return CheckIntersection(segment, rectangle.X1, rectangle.Y1, rectangle.X2, rectangle.Y1) ||
               CheckIntersection(segment, rectangle.X2, rectangle.Y1, rectangle.X2, rectangle.Y2) ||
               CheckIntersection(segment, rectangle.X2, rectangle.Y2, rectangle.X1, rectangle.Y2) ||
               CheckIntersection(segment, rectangle.X1, rectangle.Y2, rectangle.X1, rectangle.Y1);
    }

    private static bool CheckIntersection(SegmentViewModel segment, double x1, double y1, double x2, double y2)
    {
        double p1X = segment.X1, p1Y = segment.Y1, p2X = segment.X2, p2Y = segment.Y2;

        var o1 = GetOrientation(p1X, p1Y, p2X, p2Y, x1, y1);
        var o2 = GetOrientation(p1X, p1Y, p2X, p2Y, x2, y2);
        var o3 = GetOrientation(x1, y1, x2, y2, p1X, p1Y);
        var o4 = GetOrientation(x1, y1, x2, y2, p2X, p2Y);

        if (o1 != o2 && o3 != o4) return true;
        if (o1 == Orientation.Collinear && OnSegment(p1X, p1Y, x1, y1, p2X, p2Y)) return true;
        if (o2 == Orientation.Collinear && OnSegment(p1X, p1Y, x2, y2, p2X, p2Y)) return true;
        if (o3 == Orientation.Collinear && OnSegment(x1, y1, p1X, p1Y, x2, y2)) return true;
        return o4 == Orientation.Collinear && OnSegment(x1, y1, p2X, p2Y, x2, y2);
    }

    private static Orientation GetOrientation(double x1, double y1, double x2, double y2, double x3, double y3)
    {
        var val = (y2 - y1) * (x3 - x2) - (x2 - x1) * (y3 - y2);
        if (val == 0) return Orientation.Collinear;
        return val > 0 ? Orientation.Clockwise : Orientation.Counterclockwise;
    }

    private static bool OnSegment(double x1, double y1, double x2, double y2, double x3, double y3)
    {
        return x2 <= Math.Max(x1, x3) && x2 >= Math.Min(x1, x3) &&
               y2 <= Math.Max(y1, y3) && y2 >= Math.Min(y1, y3);
    }

    private enum Orientation
    {
        Collinear,
        Clockwise,
        Counterclockwise
    }
}