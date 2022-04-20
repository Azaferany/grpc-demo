using System.Security.Claims;
using Grpc.Core;
using GrpcDemo.Server.Models;
using GrpcDemo.Server.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public async ValueTask<MultiplyResult> AuthorizeAndMultiplyAsync(MultiplyRequest request, CallContext callContext = default)
    {

        var clientId = callContext.ServerCallContext!.GetHttpContext().User.FindFirstValue("Client_Id");

        var result = new MultiplyResult { Result = request.X * request.Y };
        _logger.LogInformation("{X} Multiply {Y} = {result} By {ClientId}", request.X, request.Y, result.Result, clientId);
        return result;
        
    }
}