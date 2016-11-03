using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using NoJS.Library.Objects;

namespace NoJS.Library.Filters {
    public class LegacyMiddleware {
        private readonly RequestDelegate _next;

        private readonly Options _option;

        public LegacyMiddleware(RequestDelegate next, Options option) {
            _next = next;

            _option = option;
        }

        public async Task Invoke(HttpContext context) {
            var sw = new Stopwatch();
            sw.Start();

            using (var memoryStream = new MemoryStream()) {
                var bodyStream = context.Response.Body;

                context.Response.Body = memoryStream;

                await _next(context);

                var isHtml = context.Response.ContentType?.ToLower().Contains("text/html");

                if (context.Response.StatusCode == 200 && isHtml.GetValueOrDefault()) {
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (var streamReader = new StreamReader(memoryStream)) {
                        var responseBody = await streamReader.ReadToEndAsync();

                        if (_option.EnableRenderTime) {
                            var updatedFooter = @"<footer><div>Page processed in {0} seconds.</div>";

                            responseBody = responseBody.Replace("<footer>", string.Format(updatedFooter, sw.ElapsedMilliseconds / 60));

                            context.Response.Headers.Add("X-ElapsedTime", new[] {sw.ElapsedMilliseconds.ToString()});
                        }

                        if (!_option.EnableJS) {
                            responseBody = Regex.Replace(responseBody, "<script[^<]*</script>", "");
                        }

                        if (!_option.EnableCSS) {
                            responseBody = Regex.Replace(responseBody, "<link[^<]*>", "");
                        }

                        using (var amendedBody = new MemoryStream()) {
                            using (var streamWriter = new StreamWriter(amendedBody)) {
                                streamWriter.Write(responseBody);
                                amendedBody.Seek(0, SeekOrigin.Begin);
                                await amendedBody.CopyToAsync(bodyStream);
                            }
                        }
                    }
                }
            }
        }
    }

    public static class LegacyMiddlewareExtensions {
        public static IApplicationBuilder UseLegacyMiddleware(this IApplicationBuilder builder, Options option) {
            return builder.UseMiddleware<LegacyMiddleware>(option);
        }
    }
}