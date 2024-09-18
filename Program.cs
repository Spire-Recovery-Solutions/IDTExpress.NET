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

            await GetCountryCoverage(client);

            await GetRegions(client);

            // Example 2: Get DID Groups
            await GetDidGroups(client);

            //await BrowseAvailableNumbers(client);

            //await CreateOrder(client);
        }

        private static async Task GetCountryCoverage(IdtExpressApiClient client)
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
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching country coverage: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        private static async Task GetDidGroups(IdtExpressApiClient client)
        {
            try
            {
                string countryIsoCode = "PL";

                Console.WriteLine($"Fetching DID groups for {countryIsoCode}...");

                var didGroupsResponse = await client.GetDidGroupsAsync(countryIsoCode);

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

        private static async Task GetRegions(IdtExpressApiClient client)
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
            }
            catch (IdtExpressApiException ex)
            {
                Console.WriteLine($"Error fetching regions: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


        //private static async Task BrowseAvailableNumbers(IdtExpressApiClient client)
        //{
        //    try
        //    {
        //        string didGroupId = "example-did-group-id"; // Replace with a valid DID group ID
        //        if (string.IsNullOrWhiteSpace(didGroupId))
        //        {
        //            Console.WriteLine("Invalid DID group ID.");
        //            return;
        //        }

        //        Console.WriteLine($"Browsing available numbers for DID group ID: {didGroupId}...");

        //        var browseResponse = await client.BrowseAvailableNumbersAsync(didGroupId);
        //        if (browseResponse?.AvailableNumbers != null && browseResponse.AvailableNumbers.Any())
        //        {
        //            foreach (var number in browseResponse.AvailableNumbers)
        //            {
        //                Console.WriteLine($"Number: {number.Number}, SKU: {number.Sku}");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("No available numbers found.");
        //        }
        //    }
        //    catch (IdtExpressApiException ex)
        //    {
        //        Console.WriteLine($"Error browsing available numbers: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Unexpected error: {ex.Message}");
        //    }
        //}

        //private static async Task CreateOrder(IdtExpressApiClient client)
        //{
        //    try
        //    {
        //        var createOrderRequest = new CreateOrderRequest
        //        {
        //            Items = new[]
        //            {
        //        new OrderItem { Sku = "example-sku", Quantity = 1 }
        //    }
        //        };

        //        if (createOrderRequest.Items == null || !createOrderRequest.Items.Any())
        //        {
        //            Console.WriteLine("No items to create an order.");
        //            return;
        //        }

        //        Console.WriteLine("Creating an order...");
        //        var createOrderResponse = await client.CreateOrderAsync(createOrderRequest);

        //        if (createOrderResponse != null)
        //        {
        //            Console.WriteLine($"Order ID: {createOrderResponse.OrderId}, Status: {createOrderResponse.Status}");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Order creation failed, no response received.");
        //        }
        //    }
        //    catch (IdtExpressApiException ex)
        //    {
        //        Console.WriteLine($"Error creating order: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Unexpected error: {ex.Message}");
        //    }
        //}

    }
}
