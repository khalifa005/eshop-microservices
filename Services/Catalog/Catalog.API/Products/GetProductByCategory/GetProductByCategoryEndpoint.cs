﻿namespace Catalog.API.Products.GetProductByCategory;

public class GetProductByCategoryEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapGet("/products/category/{category}",
        async (string category, ISender sender) =>
        {
          var result = await sender.Send(new GetProductByCategoryQuery(category));

          var response = result.Adapt<GetProductByCategoryResult>();

          return Results.Ok(response);
        })
    .WithName("GetProductByCategory")
    .Produces<GetProductByCategoryResult>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .WithSummary("Get Product By Category")
    .WithDescription("Get Product By Category");
  }
}
