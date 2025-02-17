using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SmartPortalApp.Middleware
{
    public class ResponseCookiesWrapper : IResponseCookies
    {
        private readonly IResponseCookies _innerCookies;
        private readonly CookiePolicyOptions _options;
        private readonly ILogger _logger;

        public ResponseCookiesWrapper(IResponseCookies innerCookies, CookiePolicyOptions options, ILogger logger)
        {
            _innerCookies = innerCookies;
            _options = options;
            _logger = logger;
        }

        public void Append(string key, string value, CookieOptions options)
        {
            // Optionally enforce custom policy
            if (_options.MinimumSameSitePolicy != null)
            {
                options.SameSite = _options.MinimumSameSitePolicy;
                _logger.LogInformation($"Cookie {key} SameSite set to: {options.SameSite}");
            }

            _innerCookies.Append(key, value, options);
        }

        public void Append(string key, string value)
        {
            _innerCookies.Append(key, value);
        }

        public void Delete(string key, CookieOptions options)
        {
            _innerCookies.Delete(key, options);
        }

        public void Delete(string key)
        {
            _innerCookies.Delete(key);
        }
    }
}
