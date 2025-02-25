﻿using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapPut("/products",
        async (UpdateProductCommand request, ISender sender) =>
        {
          var command = request.Adapt<UpdateProductCommand>();

          var result = await sender.Send(command);

          var response = result.Adapt<UpdateProductResult>();

          return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
  }
}
