using System.Net;
using System.Text;
using System.Text.Json;

namespace IDTExpress.NET;

public class IdtExpressApiClient
{
    private readonly HttpClient _httpClient;

    public IdtExpressApiClient(string apiKey, string apiSecret, string baseUrl = "https://sandbox-api.idtexpress.com/v1/")
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        _httpClient.DefaultRequestHeaders.Add("x-api-secret", apiSecret);
    }

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
        catch (IdtExpressApiException ex)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new IdtExpressApiException("An unexpected error occurred", ex.Message,
                HttpStatusCode.InternalServerError);
        }
    }


    public async Task<CountryCoverage[]> GetCountryCoverageAsync()
    {
        var response = await SendRequestAsync<object, CountryCoverageResponse>(HttpMethod.Get, "dids/coverage/countries");
        return response.Countries.ToArray();
    }

    public async Task<RegionsResponse> GetRegionsAsync(string countryIso)
    {
        var endpoint = $"dids/coverage/countries/{countryIso}/regions";
        return await SendRequestAsync<object, RegionsResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Get DID Groups based on the specified parameters.
    /// </summary>
    /// <param name="countryIso">2 letter country code.</param>
    /// <param name="regionCode">Region code (example format: "US-AK"). Required for countries that support regions, except for Toll-Free numbers.</param>
    /// <param name="tollFree">Indicates if looking for Toll-Free numbers. Do not pass region code for Toll-Free numbers in US or Canada.</param>
    /// <returns>A DidGroupsResponse containing the list of DID Groups and metadata.</returns>
    public async Task<DidGroupsResponse> GetDidGroupsAsync(string countryIso, string? regionCode = null, bool? tollFree = null)
    {
        var queryParams = new List<string> { $"country_iso={countryIso}" };
        if (!string.IsNullOrEmpty(regionCode))
        {
            queryParams.Add($"region_code={regionCode}");
        }
        if (tollFree.HasValue)
        {
            // queryParams.Add($"toll_free={tollFree.Value.ToString().ToLower()}");
        }

        var endpoint = $"dids/coverage/did_groups?{string.Join("&", queryParams)}";
        return await SendRequestAsync<object, DidGroupsResponse>(HttpMethod.Get, endpoint);
    }


    /// <summary>
    /// Browse available DID numbers for a specific DID Group.
    /// </summary>
    /// <param name="didGroupId">The ID of the DID Group to browse for available numbers.</param>
    /// <returns>A BrowseAvailableNumbersResponse containing the list of available numbers and their SKUs.</returns>
    public async Task<BrowseAvailableNumbersResponse> BrowseAvailableNumbersAsync(string didGroupId)
    {
        var endpoint = $"dids/coverage/did_groups/{didGroupId}/browse_numbers";
        return await SendRequestAsync<object, BrowseAvailableNumbersResponse>(HttpMethod.Get, endpoint);
    }

    /// <summary>
    /// Creates an order for phone numbers.
    /// </summary>
    /// <param name="request">The order request details.</param>
    /// <returns>A CreateOrderResponse containing the order details.</returns>
    public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        const string endpoint = "dids/orders";
        return await SendRequestAsync<CreateOrderRequest, CreateOrderResponse>(HttpMethod.Post, endpoint, request);
    }
}