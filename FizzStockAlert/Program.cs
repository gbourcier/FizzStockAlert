using FizzStockAlert;
using FizzStockAlert.Services;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

   

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<EmailService>();
        services.AddSingleton<RemoteWebDriver>(provider =>
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless", "--verbose", "--disable-gpu", "--no-sandbox", "--disable-dev-shm-usage");
            string? chromeHost = Environment.GetEnvironmentVariable("CHROME_HOST");
            RemoteWebDriver chromeDriver = new RemoteWebDriver(new Uri($"http://{chromeHost}:4444/wd/hub"), chromeOptions);

            return chromeDriver;
        });

        services.AddHostedService<Worker>();
    })
    .Build();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


await host.RunAsync();

