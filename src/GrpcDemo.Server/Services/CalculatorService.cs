using GrpcDemo.Server.Models;
using GrpcDemo.Server.Services.Contracts;
using ProtoBuf.Grpc;

namespace GrpcDemo.Server.Services;

public class CalculatorService : ICalculatorService
{
    private readonly ILogger<CalculatorService> _logger;

    public CalculatorService(ILogger<CalculatorService> logger)
    {
        _logger = logger;
    }

    public async ValueTask<MultiplyResult> MultiplyAsync(MultiplyRequest request, CancellationToken cancellationToken = default)
    {
        var result = new MultiplyResult { Result = request.X * request.Y };
        _logger.LogInformation("{X} Multiply {Y} = {result}", request.X, request.Y, result.Result);
        return result;
        
    }

    public ValueTask<MultiplyResult> Multiply2Async(MultiplyRequest request, CallContext callContext = default)
    {
        throw new NotImplementedException();
    }
}