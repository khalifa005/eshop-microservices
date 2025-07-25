using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Basket.API.Basket.GetBasket;

public class GetBasketEndpoints : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
    {
      var result = await sender.Send(new GetBasketQuery(userName));
      //var response = result.Adapt<GetBasketResult>();
      return Results.Ok(result);
    })
    .WithName("GetBasket")
    .Produces<GetBasketResult>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Basket")
    .WithDescription("Get Basket");
  }
}
