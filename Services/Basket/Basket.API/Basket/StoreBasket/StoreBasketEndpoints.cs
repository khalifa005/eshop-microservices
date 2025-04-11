using Carter;
using MediatR;

namespace Basket.API.Basket.StoreBasket;

public class StoreBasketEndpoints : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPost("/basket", async (StoreBasketCommand request, ISender sender) =>
    {
      var result = await sender.Send(request);
      return Results.Created($"/basket/{result.Username}", result);
    })
      .WithName("CreateProduct")
      .Produces<StoreBasketResult>(StatusCodes.Status201Created)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Create Product")
    ;
  }
}
