namespace IDTExpress.NET
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string apiKey = "test";
            string apiSecret = "Test";

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                Console.WriteLine("API key and secret are not set.");
                return;
            }

            var client = new IdtExpressApiClient(apiKey, apiSecret);

            var countries = await GetCountryCoverage(client);

            var regions = await GetRegions(client);

            // Example 2: Get DID Groups
            await GetDidGroups(client, regions);

            await BrowseAvailableNumbers(client);

            var orderId = await CreateOrder(client);

            if (!string.IsNullOrEmpty(orderId))
            {
                await GetOrder(client, orderId);
            }
        }

        private static async Task<CountryCoverage[]?> GetCountryCoverage(IdtExpressApiClient client)
        {
            try
            {
                Console.WriteLine("Fetching country coverage...");
                var countries = await client.GetCountryCoverageAsync();

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

        private static async Task GetDidGroups(IdtExpressApiClient client, List<Region>? regions)
        {
            foreach (var region in regions)
            {
                try
                {

                    Console.WriteLine($"Fetching DID groups for '{region.Name}' - '{region.Code}'...");

                    var didGroupsResponse = await client.GetDidGroupsAsync(region.Code.Split('-')[0], region.Code);

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
                            Console.WriteLine(
                                $"  Fees - Setup: {group.Fees.SetupFee}, Monthly: {group.Fees.MonthlyFee}, Per Minute: {group.Fees.PerMinuteRate}");
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

        private static async Task<List<Region>?> GetRegions(IdtExpressApiClient client)
        {
            try
            {
                string countryIsoCode = "US";
                Console.WriteLine($"Fetching regions for country ISO code {countryIsoCode}...");

                // Send the request to get regions
                var regionsResponse = await client.GetRegionsAsync(countryIsoCode);

                // Check if the response has regions and print them
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


        private static async Task BrowseAvailableNumbers(IdtExpressApiClient client)
        {
            try
            {
                string didGroupId = "3054"; // Replace with a valid DID group ID
                if (string.IsNullOrWhiteSpace(didGroupId))
                {
                    Console.WriteLine("Invalid DID group ID.");
                    return;
                }

                Console.WriteLine($"Browsing available numbers for DID group ID: {didGroupId}...");

                var browseResponse = await client.BrowseAvailableNumbersAsync(didGroupId);
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
                    DidGroupId = 3054,
                    Quantity = 1,
                    DidSkus = new List<string> { "example-sku" }
                }
            }
                };

                if (createOrderRequest.OrderItems == null || !createOrderRequest.OrderItems.Any())
                {
                    Console.WriteLine("No items to create an order.");
                    return null;
                }

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
    }
}
