using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProductById;

//public record GetProductByIdRequest();

public class GetProductByIdEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new GetProductByIdQuery(id));


      return Results.Ok(result);
    })
    .WithName("GetProductById")
    .Produces<CreateProductResult>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Product By Id")
    .WithDescription("Get Product By Id");
  }
}
