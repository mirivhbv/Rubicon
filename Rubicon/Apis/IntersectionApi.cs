namespace Rubicon.Apis;

public static class IntersectionApi
{
    public static RouteGroupBuilder MapIntersectionApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/intersection");

        api.MapPost("/", GetIntersectingRectangles);

        return api;
    }

    private static async Task<Ok<IEnumerable<RectangleViewModel>>> GetIntersectingRectangles(IIntersectionService intersectionService, IMemoryCache cache, [FromBody] SegmentViewModel model)
    {
        var cacheKey = $"IntersectingRectangles_{model.X1}_{model.Y1}_{model.X2}_{model.Y2}";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<Rectangle>? rectangles))
        {
            rectangles = await intersectionService.GetIntersectedRectangles(model.X1, model.Y1, model.X2, model.Y2);
            cache.Set(cacheKey, rectangles, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }
        return TypedResults.Ok(rectangles.Select(RectangleViewModel.GetFromEntity));
    }
}