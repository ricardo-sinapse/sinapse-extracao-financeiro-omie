using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sinapse.Extracao.Financeiro.Omie;
using Sinapse.Extracao.Financeiro.Omie.ExternalServices;
using Sinapse.Extracao.Financeiro.Omie.Interfaces;
using Sinapse.Extracao.Financeiro.Omie.Models;
using System.Net.Http.Headers;

try
{
    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    environmentName = string.IsNullOrWhiteSpace(environmentName) ? "Production" : environmentName;

    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile(@"appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($@"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

    var servicesProvider = ConfigureService(config);
    using (servicesProvider as IDisposable)
    {
        var runner = servicesProvider.GetRequiredService<MovimentosFinanceirosRunner>();
        await runner.StartRunner();
        //await runner.StartRunner("payment_asaas");
    }
    Environment.Exit(0);
}
catch (Exception ex)
{
    Environment.Exit(-9);
}

static IServiceProvider ConfigureService(IConfiguration configuration)
{
    var appSettings = configuration.Get<AppSettings>();

    var services = new ServiceCollection();
    services.AddSingleton(configuration);
    services.AddSingleton(appSettings);

    services.AddTransient<IServiceBusApplication, ServiceBusApplication>();
    services.AddTransient<MovimentosFinanceirosRunner>();

    services.AddHttpClient("service-bus", client =>
    {
        client.BaseAddress = new Uri(appSettings.ExternalServicesUrl);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("Sinapse-Origin", "interno");
        client.Timeout = new TimeSpan(0, 20, 0);

    });

    return services.BuildServiceProvider();
}