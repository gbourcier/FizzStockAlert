# Fizz Phone Stock Alert System

This project is a simple application that monitors the availability of a specific phone on the Fizz website and sends email alerts if the phone becomes available. It utilizes Docker Compose, a worker service, Selenium, and email notifications.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- Monitors the availability of a specific phone on the Fizz website.
- Sends email notifications if the phone becomes available.
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

Edit the appsettings.json file to set your specific configuration parameters:

{
  "Logging": {
    // ...
  },
  "Configuration": {
    "SMTPHost": "email-smtp.us-east-1.amazonaws.com",
    "SMTPUser": "your-smtp-username",
    "SMTPPassword": "your-smtp-password",
    "SMTPFromEmail": "your-email@example.com",
    "SMTPPort": 587,
    "Target": "https://fizz.ca/en/product/URLToProductThatYouWantToMonitor",
    "ToEmail": [ "recipient1@example.com", "recipient2@example.com", ... ]
  }
}


## Usage
The worker service will periodically make requests to the Fizz website to check if the specific phone is in stock. If the phone is available, email notifications will be sent to the specified recipients. If the worker service crashes, crash notifications will also be sent via email.

## Contributing
Contributions are welcome! Please email me at gmelanson.bourcier@gmail.com for any inquiries.

## License
This project is licensed under the MIT License.

Docker Compose configuration and image built based on the Dockerfile in the FizzStockAlert directory.

For more information, contact gmelanson.bourcier@gmail.com.