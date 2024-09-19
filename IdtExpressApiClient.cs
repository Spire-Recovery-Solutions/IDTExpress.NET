using IDTExpress.NET.Models.Requests;
using IDTExpress.NET.Models.Responses;
using System.Net;
using System.Text;
using System.Text.Json;

namespace IDTExpress.NET;

/// <summary>
/// A client for interacting with the IDT Express API, providing functionality for managing DIDs, orders, and phone numbers.
/// </summary>
public class IdtExpressApiClient
{
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdtExpressApiClient"/> class.
    /// </summary>
    /// <param name="apiKey">The API key for authenticating requests.</param>
    /// <param name="apiSecret">The API secret for authenticating requests.</param>
    /// <param name="baseUrl">The base URL for the API. Defaults to the sandbox environment.</param>
    public IdtExpressApiClient(string apiKey, string apiSecret, string baseUrl = "https://sandbox-api.idtexpress.com/v1/")
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        _httpClient.DefaultRequestHeaders.Add("x-api-secret", apiSecret);
    }

    /// <summary>
    /// Sends an HTTP request to the API and returns a response of the specified type.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <typeparam name="TResponse">The type of the response object.</typeparam>
    /// <param name="method">The HTTP method (GET, POST, PUT, DELETE).</param>
    /// <param name="endpoint">The API endpoint to send the request to.</param>
    /// <param name="request">The request object to send (optional).</param>
    /// <returns>The response from the API.</returns>
    private async Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod method, string endpoint,
        TRequest? request = default)
    {
        using var content = request != null
            ? new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            : null;

        try
        {
            HttpResponseMessage response = method switch
            {
                { } m when m == HttpMethod.Get => await _httpClient.GetAsync(endpoint),
                { } m when m == HttpMethod.Post => await _httpClient.PostAsync(endpoint, content),
                { } m when m == HttpMethod.Put => await _httpClient.PutAsync(endpoint, content),
                { } m when m == HttpMethod.Delete => await _httpClient.DeleteAsync(endpoint),
                _ => throw new NotSupportedException($"HTTP method {method} is not supported.")
            };

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<TResponse>(responseContent);

                if (apiResponse == null)
                {
                    throw new IdtExpressApiException("Failed to deserialize the response or response data is null.");
                }

                return apiResponse;
            }

            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseContent);

            throw errorResponse != null
                ? new IdtExpressApiException(errorResponse, response.StatusCode)
                : new IdtExpressApiException(response.StatusCode.ToString(), responseContent ?? "No response content",
                    response.StatusCode);
        }
        catch (IdtExpressApiException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new IdtExpressApiException("An unexpected error occurred", ex.Message,
                HttpStatusCode.InternalServerError);
        }
    }

    /// <summary>
    /// Retrieves the country coverage for DIDs.
    /// </summary>
    /// <returns>An array of countries with available DID coverage.</returns>
    public async Task<CountryCoverage[]> GetCountryCoverageAsync()
    {
        var response = await SendRequestAsync<object, CountryCoverageResponse>(HttpMethod.Get, "dids/coverage/countries");
        return response.Countries.ToArray();
    }

    /// <summary>
    /// Retrieves regions for a specific country.
    /// </summary>
    /// <param name="countryIso">The ISO 3166 country code.</param>
    /// <returns>A response containing the regions available for the specified country.</returns>
    public async Task<RegionsResponse> GetRegionsAsync(string countryIso)
    {
        var endpoint = $"dids/coverage/countries/{countryIso}/regions";
        return await SendRequestAsync<object, RegionsResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Retrieves available DID groups based on country, region, and toll-free options.
    /// </summary>
    /// <param name="countryIso">The ISO country code (e.g., "US").</param>
    /// <param name="regionCode">Optional region code (required for some countries, except Toll-Free numbers).</param>
    /// <param name="tollFree">Optional flag to specify whether to retrieve Toll-Free numbers.</param>
    /// <returns>A response containing the available DID groups.</returns>
    public async Task<DidGroupsResponse> GetDidGroupsAsync(string countryIso, string? regionCode = null, bool? tollFree = null)
    {
        var queryParams = new List<string> { $"country_iso={countryIso}" };
        if (!string.IsNullOrEmpty(regionCode))
        {
            queryParams.Add($"region_code={regionCode}");
        }
        if (tollFree.HasValue)
        {
            queryParams.Add($"toll_free={tollFree.Value.ToString().ToLower()}");
        }

        var endpoint = $"dids/coverage/did_groups?{string.Join("&", queryParams)}";
        return await SendRequestAsync<object, DidGroupsResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Browses available phone numbers for a specific DID group.
    /// </summary>
    /// <param name="didGroupId">The ID of the DID group.</param>
    /// <returns>A response containing the available phone numbers and their SKUs.</returns>
    public async Task<BrowseAvailableNumbersResponse> BrowseAvailableNumbersAsync(string didGroupId)
    {
        var endpoint = $"dids/coverage/did_groups/{didGroupId}/browse_numbers";
        return await SendRequestAsync<object, BrowseAvailableNumbersResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Creates an order for DID numbers.
    /// </summary>
    /// <param name="request">The order request details.</param>
    /// <returns>A response containing the details of the created order.</returns>
    public async Task<OrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        const string endpoint = "dids/orders";
        return await SendRequestAsync<CreateOrderRequest, OrderResponse>(HttpMethod.Post, endpoint, request);
    }

    /// <summary>
    /// Retrieves the details of a specific order.
    /// </summary>
    /// <param name="orderId">The ID of the order.</param>
    /// <returns>A response containing the details of the order.</returns>
    public async Task<OrderResponse> GetOrderAsync(string orderId)
    {
        var endpoint = $"dids/orders/{orderId}";
        return await SendRequestAsync<object, OrderResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Retrieves a list of DID orders with optional filtering.
    /// </summary>
    /// <param name="page">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of records per page (default is 10).</param>
    /// <param name="filterByStatus">Optional filter by order status.</param>
    /// <returns>A response containing the list of DID orders.</returns>
    public async Task<OrdersResponse> GetOrdersAsync(int page = 1, int pageSize = 10, string? filterByStatus = null)
    {
        var queryParams = new List<string>
        {
            $"page={page}",
            $"page_size={pageSize}"
        };

        if (!string.IsNullOrEmpty(filterByStatus))
        {
            queryParams.Add($"filter_by_status={filterByStatus}");
        }

        var endpoint = $"dids/orders?{string.Join("&", queryParams)}";
        return await SendRequestAsync<object, OrdersResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Retrieves a list of phone numbers with pagination.
    /// </summary>
    /// <param name="page">The page number to retrieve (default is 1).</param>
    /// <param name="pageSize">The number of records per page (default is 10).</param>
    /// <returns>A response containing the list of phone numbers.</returns>
    public async Task<NumberResponse> GetNumbersAsync(int page = 1, int pageSize = 10)
    {
        var endpoint = $"dids/numbers?page={page}&page_size={pageSize}";
        return await SendRequestAsync<object, NumberResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Deletes a phone number from the account.
    /// </summary>
    /// <param name="number">The phone number to delete.</param>
    /// <returns>A response indicating whether the deletion was successful.</returns>
    public async Task<DeleteNumberResponse> DeleteNumberAsync(string number)
    {
        var endpoint = $"dids/numbers/{number}";
        return await SendRequestAsync<object, DeleteNumberResponse>(HttpMethod.Delete, endpoint);
    }
}