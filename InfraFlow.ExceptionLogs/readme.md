# InfraFlow.ExceptionLogs

## Installation

You can install the package via NuGet Package Manager:
```bash
Install-Package InfraFlow.ExceptionLogs
```
Or via .NET CLI:
```bash
dotnet add package InfraFlow.ExceptionLogs
```

## Usage

```csharp
    //Context
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.ApplyConfiguration(new AppExceptionLogConfiguration(DatabaseProviderTypes.PostgreSql));
    }
    
    //Program.cs
    builder.Services.AddInfraFlowExceptionLogServices<ApplicationDbContext>();

    app.ConfigureInfraFlowExceptionMiddleware();
```

## Example

```csharp
    public class ExampleService
    {
        private readonly IAppExceptionLogService _appExceptionLogService;
        private readonly IAppLogger _appLogger;

        public ExampleService(IAppExceptionLogService appExceptionLogService, IAppLogger appLogger)
        {
            _appExceptionLogService = appExceptionLogService;
            _logger = logger;
        }

        public async Task ExampleMethod()
        {
            try
            {
                //Code
            }
            catch (Exception ex)
            {     
                await _logger.LogErrorAsync("Error", ex);
            }
        }
    }
```