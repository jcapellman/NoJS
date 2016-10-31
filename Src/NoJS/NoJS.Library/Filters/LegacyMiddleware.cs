using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NoJS.Library.Filters {
    public class LegacyMiddleware {
        private readonly RequestDelegate _next;
        private readonly bool _enableJS;

        public LegacyMiddleware(RequestDelegate next, bool enableJS = false) {
            _next = next;
            _enableJS = enableJS;
        }

        public async Task Invoke(HttpContext context) {
            var response = await GenerateResponse(context);

            await context.Response.WriteAsync(response);
        }

        private async Task<string> GenerateResponse(HttpContext context) {
            var content = string.Empty;

            await _next(context);

            if (context.Request.Body.CanSeek) {
                context.Request.Body.Position = 0;
            }

            content = new StreamReader(context.Request.Body).ReadToEnd();
                
            if (!_enableJS) {
                content = Regex.Replace(content, "<script[^<]*</script>", "");
            }

            return content;
            
        }
    }

    public static class LegacyMiddlewareExtensions {
        public static IApplicationBuilder UseLegacyMiddleware(this IApplicationBuilder builder, bool enableJS = false) {
            return builder.UseMiddleware<LegacyMiddleware>(enableJS);
        }
    }
}