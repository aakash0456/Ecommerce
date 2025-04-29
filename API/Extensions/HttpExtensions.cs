



namespace API.Extensions;

using System.Text.Json;
using Microsoft.Net.Http.Headers;

public static class HttpExtensions
{
    public static void AddPageinationHeader(this HttpResponse response, PaginationMetadata metadata)
    {
        var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        response.Headers.Append("Pagination", JsonSerializer.Serialize(metadata, options));
        response.Headers.Append(HeaderNames.AccessControlExposeHeaders, "Pagination");
        
    }
}