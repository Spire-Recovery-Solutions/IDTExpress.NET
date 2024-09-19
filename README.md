# IDT Express .NET SDK

<p align="center">
  <a href="https://www.idtexpress.com/"><img src="https://docs.idtexpress.com/images/theme/logo-idtpro-aggregator-glow-94464ec3.png" alt="IDT Express Logo" width="200"/></a>
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/IDTExpress.NET"><img src="https://img.shields.io/nuget/v/IDTExpress.NET.svg" alt="NuGet"></a>
</p>

## 📱 About

The IDT Express .NET SDK simplifies interaction with IDT's VoIP services, making it easy to integrate communication features into your .NET applications.

## 🚀 Features

- 🌍 Fetch country coverage and regions
- 📞 Manage DID groups and browse available numbers
- 📊 Create and manage orders
- 🔢 Retrieve and delete numbers
- 🔒 Secure API communication
- ⚡ Efficient JSON serialization via System.Text.Json

## 📦 Installation

Install the Solutions By Text SDK via NuGet:

    dotnet add package IDTExpress.NET

Or via the NuGet Package Manager Console:

    Install-Package IDTExpress.NET

## 🏗 Quick Start

```csharp
using IDTExpress.NET;

// Initialize API client
string apiKey = "YOUR_API_KEY";
string apiSecret = "YOUR_API_SECRET";
var client = new IdtExpressApiClient(apiKey, apiSecret);

// Fetch country coverage
var countries = await client.GetCountryCoverageAsync();

// Fetch regions for a specific country
var regions = await client.GetRegionsAsync(" "your-country-code",");

// Browse available numbers for a specific DID group
var numbers = await client.BrowseAvailableNumbersAsync("DID_GROUP_ID");

// Create an order
var createOrderRequest = new CreateOrderRequest
{
    OrderItems = new List<OrderItem>
    {
        new OrderItem
        {
            DidGroupId = did-groupi-id,
            Quantity = 1
        }
    }
};
var order = await client.CreateOrderAsync(createOrderRequest);

// Fetch order details
var orderDetails = await client.GetOrderAsync(order.Order.Id);

// Get numbers
var numbers = await client.GetNumbersAsync();

// Delete a number
await client.DeleteNumberAsync("NUMBER_TO_DELETE");
```

## 📘 Documentation

For detailed documentation, please visit our [GitHub Wiki](https://github.com/Spire-Recovery-Solutions/IDTExpress.NET/wiki).

## 📊 Supported .NET Versions

- .NET 8.0+

## 🛠 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## 🤝 Support

If you encounter any issues or have questions, please [open an issue](https://github.com/Spire-Recovery-Solutions/IDTExpress.NET/issues) on our GitHub repository.
