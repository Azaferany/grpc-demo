## Grpc Demo 
### for add grpc client follow steps  

1.dotnet tool install --global dotnet-grpc-cli --version 0.6.0

2.dotnet grpc-cli ls {Grpc Server Address}(this will print list of grpc protos for Grpc Server)

3.dotnet grpc-cli dump {Grpc Server Address} {Target Proto Name}(this will print proto in terminal)

4.paste proto in 'https://protogen.marcgravell.com' or pass it to 'protogen' cli

5.paste Generated Contracts into file on your project

6.add `services.AddCodeFirstGrpcClient<ICalculatorService>(options =>options.Address = new Uri(GRPC SERVER ADDRESS))` to startup
(`ICalculatorService` is on Generated Contracts)

7.inject `ICalculatorService`

### for client if server need Authentication 

1.add
   `services.AddClientAccessTokenManagement(options =>
   {
     options.Clients.Add("default", new ClientCredentialsTokenRequest
   {
   Address = $"{Configuration["TokenAddress"]}/connect/token",
   ClientId = Configuration["ClientId"],
   ClientSecret = Configuration["ClientSecret"],
   });
   options.CacheKeyPrefix = "Payping_Token_";
   });`

2.use 
  `services.AddCodeFirstGrpcClient<ICalculatorService>(options =>
   options.Address = new Uri(GRPC SERVER ADDRESS))
  .AddClientAccessTokenHandler();`
insted of step 6




### this template will add :

- Clean architecture structure 
  - read https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture for more information (Principle  - place of stuff - examples - philosophy etc)


- MassTransit (fill "ConnectionStrings__RabbitMq" && "RabbitMq_Username" && "RabbitMq_Password")


    - keep in mind if you use integration sdk this needed

