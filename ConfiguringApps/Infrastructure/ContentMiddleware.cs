using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguringApps.Infrastructure
{
    public class ContentMiddleware
    {
        private RequestDelegate nextDelegate;
        private UptimeService uptime;

        public ContentMiddleware(RequestDelegate next, UptimeService up)
        {
            this.nextDelegate = next;
            this.uptime = up;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //Erik - 5/8/2018 This check to see if sent to the /middleware url -> then just sends through text
            // otherwise passes the httpContext through directly
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
            {
                await httpContext.Response.WriteAsync(
                    "This is from the content middleware " +
                        $"(uptime: {uptime.Uptime}ms)", Encoding.UTF8);
            }
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }

    }
}
