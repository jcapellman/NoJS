using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NoJS.Library.Filters {
    public class LegacyMiddleware {
        private readonly RequestDelegate _next;
        private bool _enableJS;

        public LegacyMiddleware(RequestDelegate next, bool enableJS = false) {
            _next = next;
            _enableJS = enableJS;
        }

        public async Task Invoke(HttpContext context) {
            var response = await GenerateResponse(context);x
            
            await context.Response.WriteAsync(response);
        }

        private async Task<string> GenerateResponse(HttpContext context) {
            var content = string.Empty;

            var existingBody = context.Response.Body;

            using (var newBody = new MemoryStream()) {                
                await _next.Invoke(context);
                
                context.Response.Body = existingBody;

                newBody.Seek(0, SeekOrigin.Begin);

                content = new StreamReader(newBody).ReadToEnd();

                if (!_enableJS) {
                   content = Regex.Replace(content, "<script[^<]*</script>", "");
                }
                
                return content;
            }
        }
    }

    public static class LegacyMiddlewareExtensions {
        public static IApplicationBuilder UseLegacyMiddleware(this IApplicationBuilder builder, bool enableJS = false) {
            return builder.UseMiddleware<LegacyMiddleware>(enableJS);
        }
    }
}