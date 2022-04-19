## Usage : for use this template run below  commands

`dotnet new -i PayPing.Template.WebApi`

`dotnet new PayPing.Template.WebApi -n {prog name}`

### this template will add :

- Clean architecture structure 
  - read https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture for more information (Principle  - place of stuff - examples - philosophy etc)



- sentry (please fill SENTRY_DSN)
  - log unhandled errors and _logger.LogError to sentry 
  - log some Trace aside errors (https://docs.sentry.io/platforms/dotnet/guides/serilog/performance/instrumentation/automatic-instrumentation/)


- serilog 
  - log events by the provided level in "LogEventLevel" env
  - log to Console
  - log to Errors to Sentry
  - log to Elasticsearch (fill "ConnectionStrings__ElasticsearchConnection")
  - logs Enriched with trace id and span id and can mach with tracing


- MassTransit (fill "ConnectionStrings__RabbitMq" && "RabbitMq_Username" && "RabbitMq_Password")


    - keep in mind if you use integration sdk this needed

- Ef DbContext for Npgsql (fill "ConnectionStrings__DefaultConnection")
  - DbContextBase have support for `ISoftDeletable` and `ITimeable` out of the box
  - SecondLevelCache also added (find more information at https://github.com/VahidN/EFCoreSecondLevelCacheInterceptor)
  

- Localization : inject `IMessageLocalizer` and use it as `IStringLocalizer`
  MessagesResource.resx and MessagesResource.*.resx will contain all resource
  Culture will be select by request own preferred language(`app.UseRequestLocalization()`)
  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-6.0


- redis (fill "ConnectionStrings__Redis") (Use It By Inject IDistributedCache(https://docs.microsoft.com/en-us/aspnet/core/performance/caching/distributed?view=aspnetcore-6.0))


- Mechanism for fill Authorization header in internal Sdk Calls(common base & identity model base sdks)


    - provide "PayPing_Identity_Address"
    - provide "PayPing_ClientId"
    - provide "PayPing_ClientSecret"
    - keep in mind for identity model base sdks `AddClientAccessTokenHandler` should add to http client handlres (find example in startup.cs line 194)
    - keep in mind common base & identity model base Authorization Mechanism Is Added In 'AddSdksAuthenticationMechanism' (startup.cs line 219)

- OpenTelemetryTracing 
  - Export with Jaeger (but acording to https://opentelemetry.io/docs/collector/configuration/#receivers still can do export on OpenTelemetry Collector(also in collector can export into multiple collector such as jaeger and elastic amp(for integrating tracing and logs in elastic and kibana)(https://opentelemetry.io/docs/collector/configuration/#exporters)))
  - EntityFrameworkCore Instrumentation
  - AspNetCore Instrumentation
  - Redis Instrumentation
  - Elasticsearch Client Instrumentation
  - HttpClient Instrumentation
  - MassTransit Instrumentation
  - Npgsql Instrumentation


- Authentication


    - provide "PayPing_Identity_Address"
    - provide "PayPing_ApiName"
    - provide "PayPing_ApiSecret

- Authorization (startup.cs line 82)


- AddSwagger in "api-docs/{documentName}/swagger.json" endpoint


- HttpLogging 
  - LoggingFields as default is None
  - LoggingFields can change by provide "HttpLoggingFields" (values from https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.httplogging.httploggingfields?view=aspnetcore-6.0)
  

- metrics (prometheus) 
  - collect app metrics and expose it at "/admin/metrics" endpoint
  - Collect metrics for all HttpClient instances created using IHttpClientFactory
  - Collect metrics for all HTTP request 
  - Collect metrics for gRPC request  
  - Collect health check status metrics
  - Collect dotnet runtime stats
  - Collect dotnet event counters
  - Collect dotnet .NET 6 Meters (https://docs.microsoft.com/en-us/dotnet/core/diagnostics/metrics-instrumentation)


- AddHealthChecks and expose it at "/v1/healthcheck/index" endpoint

- `ExternalApiWraperService` : its example of proper use of http client factory for api calls (find more information at https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)

- `IDateTimeProvider` : pleas use this instead of `DateTime.Now` or `DateTimeOffset.Now` its more flexible and testable


- ### identity model base sdk example (`GrpcDemo.Sdk` & `GrpcDemo.WebApi.ViewModels`)

  - this sdk use `netstandard2.0` as `TargetFramework`
  - this sdk use HttpClientFactory 
  - this sdk is more flexible (ability config sdk httpclient independently(eg you can add retry,cache,Circuit-breaker,Rate-Limit handlers ))
  - its use `IdentityModel.AspNetCore` for Client Access Token Management instead of helpers in `PayPing.Common.Tools` So its dont need maintenance & documentation (read more in https://identitymodel.readthedocs.io/en/latest/aspnetcore/worker.html)
  - its wont generate package conflict
  - `GrpcDemo.Sdk` will keep Contracts and implementation of api cals
  - for every method of api call an example exist
  - use `System.Net.Http.Json` helper methods for make http call instead of helpers in `PayPing.Common.Tools` So its dont need maintenance & documentation(read more in https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json)
  - share Api `Request` & `Response` Classes With WebApi Project cross the `YYY.WebApi.ViewModels` project 
  - reference `YYY.WebApi.ViewModels` only in `WebApi` And `Sdk` Project(its Not Match with **_Clean architecture structure_** )
  - invoke `httpClientBuilderAction` on Every `AddHttpClient` as its done in example (its used to add Client Access Token Management and to ability config sdk httpclient independently) 


- in the end use below documents principals in Api designs
  - https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-implementation
  - https://docs.microsoft.com/en-us/azure/architecture/best-practices/api-design