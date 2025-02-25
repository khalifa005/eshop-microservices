namespace Catalog.API.Products.GetProducts;

//public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
//public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/products", async ([AsParameters] GetProductsQuery request, ISender sender) =>
    {
      //mapper
      //var query = request.Adapt<GetProductsQuery>();

      //var result = await sender.Send(query);
      var result = await sender.Send(request);

      var response = result.Adapt<GetProductsResult>();

      return Results.Ok(response);
    })
    .WithName("GetProducts")
    .Produces<GetProductsResult>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Products")
    .WithDescription("Get Products");
  }
}
