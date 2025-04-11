using Basket.API.Models;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
  public StoreBasketCommandValidator()
  {
    RuleFor(x => x.Cart)
        .NotNull()
        .WithMessage("Cart cannot be null");

    RuleFor(x => x.Cart.UserName)
        .NotEmpty()
        .WithMessage("UserName cannot be empty");
  }
}
public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
  public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
  {
    ShoppingCart cart = command.Cart;


    //TODO: Logic to store the basket
    //TODO:update cache 
    return new StoreBasketResult("khalifa");
  }
}

