using System.ServiceModel;
using Grpc.Core;
using GrpcDemo.Server.Models;
using ProtoBuf.Grpc;

namespace GrpcDemo.Server.Services.Contracts;

[ServiceContract]
public interface ICalculatorService
{
    ValueTask<MultiplyResult> MultiplyAsync(MultiplyRequest request, CancellationToken cancellationToken = default);

    ValueTask<MultiplyResult> AuthorizeAndMultiplyAsync(MultiplyRequest request, CallContext callContext = default);
    
    
    IAsyncEnumerable<MultiplyResult> ServerStreaming(MultiplyRequest request, CallContext callContext = default);
    IAsyncEnumerable<MultiplyResult> BiDirectionalStreaming(IAsyncEnumerable<MultiplyRequest> request, CallContext callContext = default);
    ValueTask<MultiplyResult> ClientStreaming(IAsyncEnumerable<MultiplyRequest> request, CallContext callContext = default);
}