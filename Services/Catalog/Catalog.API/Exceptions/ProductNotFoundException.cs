namespace Catalog.API.Exceptions;


public class ProductNotFoundException : Exception
{
  //public ProductNotFoundException(Guid Id) : base("Product", Id)
  //{
  //}
  public ProductNotFoundException(Guid Id) : base("Product noy found ")
  {
  }
}
