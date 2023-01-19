using Microsoft.Net.Http.Headers;

namespace DevSecOps.Template.API.DotNet.Extensions;

public static class HeadersExtensions
{
    public static string AccessToken(this IHeaderDictionary headers)
    {
        return headers[HeaderNames.Authorization].First().Replace("Bearer", "", StringComparison.InvariantCultureIgnoreCase).Trim();
    }
}