using System.Text.Json;
using SimpleCqrs.Command.Application.UseCases;

namespace SimpleCqrs.Command.Api;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

public class ProductFunction
{
    private readonly ILogger<ProductFunction> _logger;
    private readonly ProductUseCaseHandler _useCase;

    public ProductFunction(ILogger<ProductFunction> logger, ProductUseCaseHandler useCase) {
        _logger = logger;
        _useCase = useCase;
    }

    [Function("AddProduct")]
    public async Task<IActionResult> AddProduct(
        [HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequest req) {
        
        // Happy Path.
        var request = await JsonSerializer.DeserializeAsync<AddProductRequest>(req.Body);

        await this._useCase.Handle(request);
        
        return new OkObjectResult("Product Added!");
    }

}