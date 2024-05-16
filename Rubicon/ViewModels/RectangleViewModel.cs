namespace Rubicon.ViewModels;

public class RectangleViewModel
{
    public string Id { get; set; } = default!;

    public double X1 { get; set; }
    public double Y1 { get; set; }
    public double X2 { get; set; }
    public double Y2 { get; set; }


    public static RectangleViewModel GetFromEntity(Rectangle rectangle)
    {
        var (x1, y1, x2, y2) = rectangle.ToCoordinates();
        return new RectangleViewModel
        {
            X1 = x1,
            Y1 = y1,
            X2 = x2,
            Y2 = y2
        };
    }
}