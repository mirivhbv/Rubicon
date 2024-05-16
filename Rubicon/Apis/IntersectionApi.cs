namespace Rubicon.Apis;

public static class IntersectionApi
{
    public static RouteGroupBuilder MapIntersectionApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/intersection");

        api.MapPost("/", GetIntersectingRectangles);

        return api;
    }

    private static async Task<Ok<IEnumerable<RectangleViewModel>>> GetIntersectingRectangles(IIntersectionService intersectionService, [FromBody] SegmentViewModel model)
    {
        var intersectedRectangles = await intersectionService.GetIntersectedRectangles(model.X1, model.Y1, model.X2, model.Y2);
        return TypedResults.Ok(intersectedRectangles.Select(RectangleViewModel.GetFromEntity));
    }
}