using Basket.API.Data;
using BuildingBlocks.CQRS;
using FluentValidation;
using System.Windows.Input;

namespace Basket.API.Basket.DeleteBasket;


public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
  public DeleteBasketCommandValidator()
  {
      RuleFor(x => x.Username)
          .NotEmpty()
          .WithMessage("Username is required.");
  }
}

public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
  public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
  {
    //TODO delete basket from database and cache
    var result = await basketRepository.DeleteBasket(command.Username, cancellationToken);
    return new DeleteBasketResult(result);


  }
}
