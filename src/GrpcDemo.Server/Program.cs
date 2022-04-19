using GrpcDemo.Server;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5016, listenOptions =>
        listenOptions.Protocols = HttpProtocols.Http1);
            
    //https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-6.0#unable-to-start-aspnet-core-grpc-app-on-macos
    options.ListenLocalhost(5017, listenOptions =>
        listenOptions.Protocols = HttpProtocols.Http2);
});

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.Configure(app.Environment);

app.Run();


