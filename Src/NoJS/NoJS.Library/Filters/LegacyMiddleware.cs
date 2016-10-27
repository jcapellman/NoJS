using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NoJS.Library.Filters {
    public class LegacyMiddleware {
        private readonly RequestDelegate _next;

        public LegacyMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            var response = await GenerateResponse(context);
            
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
             
                //todo implement filtering of legacy functionality
                   
                return content;
            }
        }
    }

    public static class LegacyMiddlewareExtensions {
        public static IApplicationBuilder UseLegacyMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<LegacyMiddleware>();
        }
    }
}