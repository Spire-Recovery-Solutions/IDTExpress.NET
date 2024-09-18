using IDTExpress.NET.Models.Requests;
using IDTExpress.NET.Models.Responses;

namespace IDTExpress.NET
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // API key and secret should be securely stored and fetched.
            string apiKey = "test";
            string apiSecret = "Test";

            // Check if API key and secret are provided
            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                Console.WriteLine("API key and secret are not set.");
                return;
            }

            // Initialize API client
            var client = new IdtExpressApiClient(apiKey, apiSecret);

            // Fetch and display country coverage
            var countries = await GetCountryCoverage(client);

            // Fetch and display regions for a specific country
            var regions = await GetRegions(client);

            // Fetch DID groups based on regions
            await GetDidGroups(client, regions);

            // Browse available numbers for a specific DID group
            await BrowseAvailableNumbers(client);

            // Create an order and retrieve the order ID
            var orderId = await CreateOrder(client);

            // Fetch details of the created order if the order ID exists
            if (!string.IsNullOrEmpty(orderId))
            {
                await GetOrder(client, orderId);
            }

            // Fetch and display all orders
            await GetOrders(client);

            // Get a number from the API and delete it if found
            var number = await GetNumbers(client);
            if (!string.IsNullOrEmpty(number))
            {
                await DeleteNumber(client, number);
            }
        }

        /// <summary>
        /// Fetches the country coverage from the API.
        /// </summary>
        private static async Task<CountryCoverage[]?> GetCountryCoverage(IdtExpressApiClient client)
        {
            try
            {
                Console.WriteLine("Fetching country coverage...");
                var countries = await client.GetCountryCoverageAsync();

                // Display the list of countries
                if (countries != null && countries.Any())
                {
                    foreach (var country in countries)
                    {
                        Console.WriteLine($"Country: {country.Name}, ISO Code: {country.Iso}");
                    }
                }
                else
                {
                    Console.WriteLine("No countries found.");
                }

                return countries;
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching country coverage: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Fetches DID groups for the specified regions.
        /// </summary>
        private static async Task GetDidGroups(IdtExpressApiClient client, List<Region>? regions)
        {
            if (regions == null) return;

            foreach (var region in regions)
            {
                try
                {
                    Console.WriteLine($"Fetching DID groups for '{region.Name}' - '{region.Code}'...");

                    // Fetch DID groups for each region
                    var didGroupsResponse = await client.GetDidGroupsAsync(region.Code.Split('-')[0], region.Code);

                    // Display DID group details
                    if (didGroupsResponse?.DidGroups != null && didGroupsResponse.DidGroups.Any())
                    {
                        Console.WriteLine($"Fetched {didGroupsResponse.DidGroups.Count} DID groups.");
                        foreach (var group in didGroupsResponse.DidGroups)
                        {
                            Console.WriteLine($"DID Group: {group.Name}, ID: {group.Id}");
                            Console.WriteLine($"  Area Code: {group.AreaCode}");
                            Console.WriteLine($"  NXX: {group.Nxx}");
                            Console.WriteLine($"  Toll-Free: {group.TollFree}");
                            Console.WriteLine($"  Supports Browse: {group.SupportsBrowse}");
                            Console.WriteLine($"  Quantity: {group.Quantity}");
                            Console.WriteLine($"  Fees - Setup: {group.Fees.SetupFee}, Monthly: {group.Fees.MonthlyFee}, Per Minute: {group.Fees.PerMinuteRate}");
                            Console.WriteLine($"  Country: {group.Country.Name} ({group.Country.Iso})");
                            Console.WriteLine($"  Region: {group.Region?.Name} ({group.Region?.Code})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No DID groups found.");
                    }
                }
                catch (IdtExpressApiException ex)
                {
                    Console.WriteLine($"Error fetching DID groups: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Fetches regions for a specific country.
        /// </summary>
        private static async Task<List<Region>?> GetRegions(IdtExpressApiClient client)
        {
            try
            {
                string countryIsoCode = "US"; // Specify the country code
                Console.WriteLine($"Fetching regions for country ISO code {countryIsoCode}...");

                var regionsResponse = await client.GetRegionsAsync(countryIsoCode);

                if (regionsResponse?.Regions != null && regionsResponse.Regions.Any())
                {
                    Console.WriteLine($"Fetched {regionsResponse.Regions.Count} regions.");
                    foreach (var region in regionsResponse.Regions)
                    {
                        Console.WriteLine($"Region: {region.Name}, Code: {region.Code}");
                    }
                }
                else
                {
                    Console.WriteLine("No regions found.");
                }

                return regionsResponse?.Regions;
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching regions: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Browses available numbers for a specific DID group.
        /// </summary>
        private static async Task BrowseAvailableNumbers(IdtExpressApiClient client)
        {
            try
            {
                string didGroupId = "3054"; // Replace with a valid DID group ID

                Console.WriteLine($"Browsing available numbers for DID group ID: {didGroupId}...");

                var browseResponse = await client.BrowseAvailableNumbersAsync(didGroupId);

                // Display available numbers
                if (browseResponse?.Numbers != null && browseResponse.Numbers.Any())
                {
                    foreach (var number in browseResponse.Numbers)
                    {
                        Console.WriteLine($"Number: {number.Number}, SKU: {number.Sku}");
                    }
                }
                else
                {
                    Console.WriteLine("No available numbers found.");
                }
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error browsing available numbers: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new order for numbers.
        /// </summary>
        private static async Task<string?> CreateOrder(IdtExpressApiClient client)
        {
            try
            {
                var createOrderRequest = new CreateOrderRequest
                {
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            DidGroupId = 3054, // Replace with a valid group ID
                            Quantity = 1,
                            DidSkus = new List<string> { "example-sku" } // Replace with a valid SKU
                        }
                    }
                };

                Console.WriteLine("Creating an order...");
                var createOrderResponse = await client.CreateOrderAsync(createOrderRequest);

                if (createOrderResponse != null)
                {
                    var orderId = createOrderResponse.Order.Id;
                    Console.WriteLine($"Order created successfully. Order ID: {orderId}, Status: {createOrderResponse.Order.Status}");
                    return orderId;
                }
                else
                {
                    Console.WriteLine("Order creation failed, no response received.");
                    return null;
                }
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error creating order: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Fetches details of an order by ID.
        /// </summary>
        private static async Task GetOrder(IdtExpressApiClient client, string orderId)
        {
            try
            {
                Console.WriteLine($"Fetching details for order ID: {orderId}...");

                var orderResponse = await client.GetOrderAsync(orderId);

                if (orderResponse?.Order != null)
                {
                    var order = orderResponse.Order;
                    Console.WriteLine($"Order ID: {order.Id}");
                    Console.WriteLine($"Status: {order.Status}");
                    Console.WriteLine($"Total Quantity: {order.Ordered.Quantity}");
                    Console.WriteLine($"Created At: {order.CreatedAt}");
                }
                else
                {
                    Console.WriteLine("Order details not found.");
                }
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching order details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Fetches and displays all orders.
        /// </summary>
        private static async Task GetOrders(IdtExpressApiClient client)
        {
            try
            {
                Console.WriteLine("Fetching orders...");

                var ordersResponse = await client.GetOrdersAsync(page: 1, pageSize: 10);

                if (ordersResponse?.Orders != null && ordersResponse.Orders.Any())
                {
                    Console.WriteLine($"Fetched {ordersResponse.Orders.Count} orders.");
                    foreach (var order in ordersResponse.Orders)
                    {
                        Console.WriteLine($"Order ID: {order.Id}, Status: {order.Status}, Created At: {order.CreatedAt}");
                    }
                }
                else
                {
                    Console.WriteLine("No orders found.");
                }
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching orders: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Fetches numbers from the API.
        /// </summary>
        private static async Task<string?> GetNumbers(IdtExpressApiClient client, int page = 1, int pageSize = 10)
        {
            try
            {
                Console.WriteLine("Fetching numbers...");

                var numbersResponse = await client.GetNumbersAsync();

                if (numbersResponse?.Numbers != null && numbersResponse.Numbers.Any())
                {
                    foreach (var number in numbersResponse.Numbers)
                    {
                        Console.WriteLine($"Number: {number.NumberValue}, DudGroup: {number.DidGroup.Id}, Status: {number.Status}");
                    }

                    return numbersResponse.Numbers.FirstOrDefault().NumberValue;
                }
                else
                {
                    Console.WriteLine("No numbers found.");
                    return null;
                }
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching numbers: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Deletes a number from the account.
        /// </summary>
        private static async Task DeleteNumber(IdtExpressApiClient client, string number)
        {
            try
            {
                Console.WriteLine($"Deleting number: {number}...");

                var deleteResponse = await client.DeleteNumberAsync(number);
                if (deleteResponse != null)
                {
                    Console.WriteLine($"Number: {deleteResponse.Number} has been {deleteResponse.Status}.");
                }
                else
                {
                    Console.WriteLine("Failed to delete the number, no response received.");
                }
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error deleting number: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}