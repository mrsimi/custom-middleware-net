using System.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware.Middlewares
{
    public class HeadersCheckers
    {
        private readonly RequestDelegate _next;
        private static string CLIENTID = "CK_Live1xzhgR0nbLlH0ecHBHzO";
        private static string CLIENTKEY = "2938c842-0dd9-49a4-9dd2";

        public HeadersCheckers(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var requestUrl = httpContext.Request.Path;
            string securedPath = "/api_s/";
           

            if(requestUrl.ToString().Contains(securedPath))
            {
                string clientKey = httpContext.Request.Headers["ClientKey"];
                string clientId = httpContext.Request.Headers["ClientId"];
                
                if((string.IsNullOrEmpty(clientId) && string.IsNullOrEmpty(clientKey)) ||
                   (!clientId.Equals(CLIENTID, StringComparison.InvariantCultureIgnoreCase) && 
                   !(clientKey.Equals(CLIENTKEY, StringComparison.InvariantCultureIgnoreCase))))
                {
                    httpContext.Response.StatusCode = 401;
                    httpContext.Response.Redirect("/api/error/401");
                }
            }
            
            await _next(httpContext);
        }

    }

    public static class HeadersCheckersExtensions
    {
        public static IApplicationBuilder UseHeaderCheckerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeadersCheckers>();
        }
    }
}