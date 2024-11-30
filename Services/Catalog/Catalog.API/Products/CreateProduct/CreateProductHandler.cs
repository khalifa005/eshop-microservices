

using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.CreateProduct
{
  public record CreateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<CreateProductResult>;

  public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
  {
    public CreateProductCommandValidator()
    {
      RuleFor(x => x.Name).NotEmpty().WithMessage("name is required");
      RuleFor(x => x.ImageFile).NotEmpty().WithMessage("image is required");
      RuleFor(x => x.Category).NotEmpty().WithMessage("category is required");
      RuleFor(x => x.Price).GreaterThan(0).WithMessage("price is required");
    }
  }

  public record CreateProductResult(Guid Id);

  internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
  {
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
      var entity = new Product()
      {
        Name = command.Name,
        Category = command.Category,
        Description = command.Description,
        ImageFile = command.ImageFile,
        Price = command.Price,
      };


      session.Store(entity);
      await session.SaveChangesAsync(cancellationToken);

      return new CreateProductResult(entity.Id);
    }
  }
}
