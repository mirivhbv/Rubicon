namespace Rubicon.Apis;

public static class IntersectionApi
{
    public static RouteGroupBuilder MapIntersectionApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/intersection");

        api.MapPost("/", GetIntersectingRectangles);

        return api;
    }

    private static async Task<Ok<List<RectangleViewModel>>> GetIntersectingRectangles(IRepository<Rectangle> rectangleRepository, [FromBody] SegmentViewModel model)
    {
        var segmentLine = new LineString(new[]
        {
            new Coordinate(model.X1, model.Y1),
            new Coordinate(model.X2, model.Y2)
        });

        var rectangles = await rectangleRepository.Query()
            .Where(r => r.Geometry.Intersects(segmentLine))
            .Select(x => RectangleViewModel.GetFromEntity(x))
            .ToListAsync();

        return TypedResults.Ok(rectangles);
    }
}