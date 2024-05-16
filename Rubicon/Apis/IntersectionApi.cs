namespace Rubicon.Apis;

public static class IntersectionApi
{
    public static RouteGroupBuilder MapIntersectionApi(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/intersection");

        api.MapGet("/", GetIntersectingRectangles);

        return api;
    }

    private static Task GetIntersectingRectangles([AsParameters] RectangleService rectangleService)
    {
        throw new NotImplementedException();
    }
}