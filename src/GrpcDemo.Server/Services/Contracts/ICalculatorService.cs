﻿using System.ServiceModel;
using GrpcDemo.Server.Models;
using ProtoBuf.Grpc;

namespace GrpcDemo.Server.Services.Contracts;

[ServiceContract]
public interface ICalculatorService
{
    ValueTask<MultiplyResult> MultiplyAsync(MultiplyRequest request, CancellationToken cancellationToken = default);

    ValueTask<MultiplyResult> Multiply2Async(MultiplyRequest request, CallContext callContext = default);
}