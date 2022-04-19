using GrpcDemo.Server.Services.Contracts;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ProtoBuf.Grpc.ClientFactory;

var builder = WebApplication.CreateBuilder(args);

// 
////
//////
builder.Services.AddCodeFirstGrpcClient<ICalculatorService>(options =>
    options.Address = new Uri(builder.Configuration["GRPC_SERVER"]));
    //.AddClientAccessTokenHandler();
//////
////
// 

/*builder.Services.AddClientAccessTokenManagement(options =>
{
    options.Clients.Add("default", new ClientCredentialsTokenRequest
    {
        Address = $"{builder.Configuration["TokenAddress"]}/connect/token",
        ClientId = builder.Configuration["ClientId"],
        ClientSecret = builder.Configuration["ClientSecret"],
    });
    options.CacheKeyPrefix = "Payping_Token_";
});*/

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