using Carter;
using MediatR;

namespace Basket.API.Basket.DeleteBasket;

public class DeleteBasketEndpoints : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
    {
      var result = await sender.Send(new DeleteBasketCommand(userName));
      var response = result.IsSuccess;

      return Results.Ok(response);

    })
      .WithName("DeleteProductFromBasket")
      .Produces<DeleteBasketResult>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .ProducesProblem(StatusCodes.Status404NotFound)
      .WithSummary("Delete a product from basket")
      .WithDescription("Deletes a product from the user's basket by username.");
  }
}
