using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ideo.Core.App.Extensions.Middlewares
{
    public class ContentSecurityPolicyMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        #region SecurityMiddleware()
        public ContentSecurityPolicyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            this.next = next;
            this.configuration = configuration;
        }
        #endregion

        #region Invoke()
        public async Task Invoke(HttpContext context)
        {
            var allowOrigin = String.Empty;
            var allowedOrigins = configuration.GetSection("Security:Access-Control-Allow-Origin").GetChildren().ToArray().Select(c => c.Value.TrimEnd('/')).ToArray();

            var origin = context.Request.Headers.GetCommaSeparatedValues("Origin").FirstOrDefault();
            var host = !String.IsNullOrEmpty(origin) ? origin : $"{context.Request.Scheme}://{context.Request.Host.Value}";

            if (allowedOrigins.Contains("*"))
            {
                allowOrigin = host;
            }
            else
            {
                allowOrigin = allowedOrigins.FirstOrDefault();

                if (allowedOrigins.Any(o => String.Compare(o, host, true) == 0))
                {
                    allowOrigin = host;
                }
            }

            context.Response.Headers.Add("Access-Control-Allow-Origin", allowOrigin);
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
      /*      context.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Content-Type, Request-Token, X-Requested-With, Impersonate");*/
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, HEAD, POST, PUT, PATCH, DELETE, OPTIONS");

            if (!String.IsNullOrEmpty(configuration["Security:Strict-Transport-Security"]))
            {
                context.Response.Headers.Add("Strict-Transport-Security", configuration["Security:Strict-Transport-Security"]);
            }

            if (!String.IsNullOrEmpty(configuration["Security:Content-Security-Policy"]))
            {
                context.Response.Headers.Add("Content-Security-Policy", configuration["Security:Content-Security-Policy"]);
            }

            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");

            if (!String.IsNullOrEmpty(configuration["Security:Feature-Policy"]))
            {
                context.Response.Headers.Add("Feature-Policy", configuration["Security:Feature-Policy"]);
            }

            if (context.Request.Method == "OPTIONS")
            {
                context.Response.Headers.Add("Access-Control-Max-Age", new[] { "604800" });
                context.Response.StatusCode = 204;
            }
            else
            {
                await next(context);
            }
        }
        #endregion
    }
}
