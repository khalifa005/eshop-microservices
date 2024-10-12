using FluentValidation;

namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
  public DeleteProductCommandValidator()
  {
    RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
  }
}

internal class DeleteProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
  public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
  {
    session.Delete<Product>(command.Id);
    await session.SaveChangesAsync(cancellationToken);

    return new DeleteProductResult(true);
  }
}



public class DeleteProductEndpoint : ICarterModule
{
  public void AddRoutes(IEndpointRouteBuilder app)
  {
    app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
    {
      var result = await sender.Send(new DeleteProductCommand(id));

      var response = result.Adapt<DeleteProductResult>();

      return Results.Ok(response);
    })
    .WithName("DeleteProduct")
    .Produces<DeleteProductResult>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("Delete Product")
    .WithDescription("Delete Product");
  }
}
