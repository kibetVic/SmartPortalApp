using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SmartPortalApp.Models;
namespace SmartPortalApp.Middleware
{
    public class CookiePolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private CookiePolicyOptions _options;

        public CookiePolicyMiddleware(RequestDelegate next, IOptions<CookiePolicyOptions> options, ILoggerFactory factory)
        {
            Options = options.Value;
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = factory.CreateLogger<CookiePolicyMiddleware>();
        }
        public CookiePolicyMiddleware(RequestDelegate next, IOptions<CookiePolicyOptions> options)
        {
            Options = options.Value;
            _next = next;
            _logger = NullLogger.Instance;
        }
        public CookiePolicyOptions Options { get; set; }

        public Task Invoke(HttpContext context)
        {
            var feature = context.Features.Get<IResponseCookiesFeature>() ?? new ResponseCookiesFeature(context.Features);
            var wrapper = new ResponseCookiesWrapper(feature.Cookies, Options, _logger);
            context.Features.Set<IResponseCookiesFeature>(new CookiesWrapperFeature(wrapper));
            //context.Features.Set<ITrackingConsentFeature>((ITrackingConsentFeature?)wrapper);

            return _next(context);
        }


        //public Task Invoke(HttpContext context)
        //{
        //    var feature = context.Features.Get<IResponseCookiesFeature>() ?? new ResponseCookiesFeature(context.Features);
        //    var wrapper = new ResponseCookiesWrapper(context, Options, feature, _logger);
        //    context.Features.Set<IResponseCookiesFeature>(new CookiesWrapperFeature(wrapper));
        //    context.Features.Set((ITrackingConsentFeature?)wrapper);

        //    return _next(context);
        //}

        private sealed class CookiesWrapperFeature : IResponseCookiesFeature
        {
            public CookiesWrapperFeature(ResponseCookiesWrapper wrapper)
            {
                Cookies = (IResponseCookies?)wrapper;
            }

            public IResponseCookies Cookies { get; }
        }
    }
}
