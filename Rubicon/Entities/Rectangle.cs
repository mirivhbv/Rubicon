namespace Rubicon.Entities;

/// <summary>
/// Model for rectangle domain entity.
/// </summary>
public class Rectangle : BaseEntity
{
    public Polygon Geometry { get; set; } = default!;

    public static Rectangle FromCoordinates(double x1, double y1, double x2, double y2)
    {
        return new Rectangle
        {
            Geometry = new Polygon(new LinearRing(new[]
            {
                new Coordinate(x1, y1), 
                new Coordinate(x2, y1), 
                new Coordinate(x2, y2), 
                new Coordinate(x1, y2),
                new Coordinate(x1, y1)
            }))
        };
    }

    public (double X1, double Y1, double X2, double Y2) ToCoordinates()
    {
        if (Geometry == null || Geometry.Coordinates.Length < 4)
        {
            throw new InvalidOperationException("Invalid polygon geometry");
        }

        var x1 = Geometry.Coordinates[0].X;
        var y1 = Geometry.Coordinates[0].Y;
        var x2 = Geometry.Coordinates[2].X;
        var y2 = Geometry.Coordinates[2].Y;

        return (X1: x1, Y1: y1, X2: x2, Y2: y2);
    }
}