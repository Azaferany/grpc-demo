using GrpcDemo.Server.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.ClientFactory;

var builder = WebApplication.CreateBuilder(args);

// 
////
//////
builder.Services.AddCodeFirstGrpcClient<ICalculatorService>(options => options.Address = new Uri(builder.Configuration["GRPC_SERVER"]))
    .AddClientAccessTokenHandler();
//////
////
// 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1",new OpenApiInfo()
{
    Title = "client"
}));

var app = builder.Build();
app.MapSwagger();
app.UseSwaggerUI();

app.MapGet("/GrpcTest", ([FromServices] ICalculatorService calculatorService) => calculatorService.MultiplyAsync(
    new MultiplyRequest()
    {
        X = 50,
        Y = 10
    }));

app.Run("http://*:7590");