using Microsoft.AspNetCore.Http;

namespace StartSportStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            string x = request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
            return x;
        }
    }
}
