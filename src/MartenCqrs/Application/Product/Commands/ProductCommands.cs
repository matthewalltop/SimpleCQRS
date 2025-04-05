namespace SimpleCqrs.Command;

/// <summary>
/// Command for Adding a Product
/// </summary>
/// <param name="Name">The name of the product.</param>
/// <param name="Category">Category of the product.</param>
/// <param name="Quantity">The initial quantity of the product.</param>
public record AddProductRequest(string Name, string Category = "", uint Quantity = 0) : CommandBase;

/// <summary>
/// Command to adjust product quantity
/// </summary>
/// <param name="Id">The Id of the product.</param>
/// <param name="Quantity">The updated quantity for the given product.</param>
public record AdjustProductQuantityRequest(Guid Id, uint Quantity) : CommandBase;

/// <summary>
/// Command to remove a product.
/// </summary>
/// <param name="Id">The Id of the product.</param>
public record RemoveProductRequest(Guid Id) : CommandBase;