# Fizz Phone Stock Alert System

This project is a simple application that monitors the availability of a specific phone on Fizz (or any other web store, really) website and sends email alerts if the item becomes available. It utilizes Docker Compose, a worker service, Selenium, and email notifications.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- Monitors the availability of a specific phone on e-commerce websites.
- Sends email notifications if the item becomes available.
- Provides crash notifications via email if the worker service fails.

## Prerequisites

Before you begin, ensure you have the following installed:

- Docker
- Docker Compose

## Getting Started

1. Clone this repository:
   git clone https://github.com/gbourcier/FizzStockAlert
   cd FizzStockAlert


2. Build the Docker images:
    docker-compose build

3. Start the Docker containers
    docker-compose up -d

## Configuration

Create (under FizzStockAlert/appsettings.json) and edit the appsettings.json file to set your specific configuration parameters:

{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "Configuration": {
    "SMTPHost": "smtp.host.com",
    "SMTPUser": "username,
    "SMTPPassword": "password",
    "SMTPFromEmail": "from@gmail.com",
    "SMTPPort": 1234,
    "Target": "https://someitem.someonlinestore.com",
    "ToEmail": [ "to@gmail.com" ],
    "OOSElementSpanText": "Out of Stock",
    "SMTPEmailBody": "Some email body",
    "SMTPEmailSubject" :  "Some email subject"
  }
}

OOSElementSpanText will be monitored by the service every 5-10 secondes. Typically, this would represent a button that would normally says "Add to cart" but says "Out of stock". In order to find what to use exactly, you should use the inspect tool from your browser. The exact selenium call is the following : IWebElement? oosElement = _chromeDriver.FindElementSafe(By.XPath($"//span[text()='{_appSettings.OOSElementSpanText}']"));
If the Out of stock element is null, the system will assume that the item is in stock and it will trigger an email notification.

## Usage
The worker service will periodically make requests to the Fizz website to check if the specific phone is in stock. If the phone is available, email notifications will be sent to the specified recipients. If the worker service crashes, crash notifications will also be sent via email.

## Contributing
Contributions are welcome! Please email me at gmelanson.bourcier@gmail.com for any inquiries.

## License
This project is licensed under the MIT License.

Docker Compose configuration and image built based on the Dockerfile in the FizzStockAlert directory.

For more information, contact gmelanson.bourcier@gmail.com.