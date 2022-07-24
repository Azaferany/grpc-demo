## Grpc Demo 
### for add grpc client follow steps  

1.`dotnet tool install --global dotnet-grpc-cli --version 0.6.0`

2.`dotnet grpc-cli ls {Grpc Server Address}`(this will print list of grpc protos for Grpc Server)

3.`dotnet grpc-cli dump {Grpc Server Address} {Target Proto Name}`(this will print proto in terminal)

4.paste proto in `https://protogen.marcgravell.com` or pass it to `protogen` cli

5.paste Generated Contracts into file on your project

6.add 

    services.AddCodeFirstGrpcClient<ICalculatorService>(options =>options.Address = new Uri(GRPC SERVER ADDRESS))
to startup
(`ICalculatorService` is on Generated Contracts)


7.inject `ICalculatorService`

### for client if server need Authentication 

1.add

    services.AddClientAccessTokenManagement(options =>
    {
        options.Clients.Add("default", new ClientCredentialsTokenRequest
        {
            Address = $"{Configuration["TokenAddress"]}/connect/token",
            ClientId = Configuration["ClientId"],
            ClientSecret = Configuration["ClientSecret"],
        });
        options.CacheKeyPrefix = "Token_";
    });

2.use 

    services.AddCodeFirstGrpcClient<ICalculatorService>(options => options.Address = new Uri(GRPC SERVER ADDRESS))
    .AddClientAccessTokenHandler();
insted of step 6

for more information see:

    https://github.com/protobuf-net/protobuf-net.Grpc
    https://github.com/protobuf-net/protobuf-net
 
