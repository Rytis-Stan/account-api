using System.Text.Json;

namespace AccountApi.Tests;

public static class HttpClientExtensions
{
    public static async Task<T> ContentAsJsonAsync<T>(this HttpResponseMessage response)
    {
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
