using Microsoft.AspNetCore.Http.Features;

namespace SmartPortalApp.Models
{
    internal class ResponseCookiesWrapper
    {
        private HttpContext context;
        private CookiePolicyOptions options;
        private IResponseCookiesFeature feature;
        private ILogger logger;

        public ResponseCookiesWrapper(HttpContext context, CookiePolicyOptions options, IResponseCookiesFeature feature, ILogger logger)
        {
            this.context = context;
            this.options = options;
            this.feature = feature;
            this.logger = logger;
        }
    }
}