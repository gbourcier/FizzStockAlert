using FizzStockAlert.Configuration;
using FizzStockAlert.Extensions;
using FizzStockAlert.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Net.Mail;
using System.Xml.Linq;

namespace FizzStockAlert
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;
        private readonly EmailService _emailService;
        private readonly RemoteWebDriver _chromeDriver;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, EmailService emailService, RemoteWebDriver chromeDriver)
        {
            _logger = logger;
            _configuration = configuration;
            _appSettings = configuration.GetSection("Configuration").Get<AppSettings>();
            _emailService = emailService;
            _chromeDriver = chromeDriver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(10000, stoppingToken);
            _logger.LogDebug($"Navigating to {_appSettings.Target}.");
            _chromeDriver.Navigate().GoToUrl(_appSettings.Target);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Starting new execution loop.");

                try
                {
                    _logger.LogDebug($"Refreshing page :  {_appSettings.Target}.");
                    _chromeDriver.Navigate().Refresh();
                    await Task.Delay(10000, stoppingToken);
                    IWebElement? oosElement = _chromeDriver.FindElementSafe(By.XPath($"//span[text()='{_appSettings.OOSElementSpanText}']"));

                    if (oosElement is null)
                    {
                        _logger.LogInformation($"Phone is possibly in stock. Emailing {_appSettings.ToEmail}.");
                        _emailService.SendSuccessEmail(_appSettings.ToEmail);
                        await Task.Delay(900000, stoppingToken);
                    }

                }
                catch (Exception e)
                {
                    _logger.LogError("Error during execution loop. Waiting 5min. ", e);
                    _emailService.SendCrashEmail(_appSettings.ToEmail, e);

                    await Task.Delay(300000, stoppingToken);
                }
   
            }
        }
    }
}
