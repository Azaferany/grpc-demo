using GrpcDemo.Server.Services;
using GrpcDemo.Server.Services.Contracts;
using IdentityModel.AspNetCore.AccessTokenValidation;
using ProtoBuf.Grpc.Server;

namespace GrpcDemo.Server;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        // 
        ////
        //////
        services.AddCodeFirstGrpc();
        services.AddCodeFirstGrpcReflection();
        //////
        ////
        // 

        #region Authorization

        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["PayPing_Identity_Address"];
                options.MapInboundClaims = false;
                // if token does not contain a dot, it is a reference token
                options.ForwardDefaultSelector = Selector.ForwardReferenceToken("Introspection");
            })
            .AddOAuth2Introspection("Introspection" ,options =>
            {
                options.Authority = configuration["PayPing_Identity_Address"];
                options.ClientId = configuration["PayPing_ApiName"];
                options.ClientSecret = configuration["PayPing_ApiSecret"];
            });

        services.AddScopeTransformation();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("read",
                policy => policy.RequireScope("user:read"));
        });

        #endregion
    }
    
    public static void Configure(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpointBuilder =>
        {
            endpointBuilder.MapControllers();

            // 
            ////
            //////
            endpointBuilder.MapGrpcService<CalculatorService>();
            endpointBuilder.MapCodeFirstGrpcReflectionService();
            //////
            ////
            // 
        });
    }
}