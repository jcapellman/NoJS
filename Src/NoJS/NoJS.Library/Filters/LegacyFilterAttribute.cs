using System.IO;
using System.Text;

using Microsoft.AspNetCore.Mvc.Filters;

namespace NoJS.Library.Filters {
    public class LegacyFilterAttribute : ActionFilterAttribute {
        public override async void OnResultExecuted(ResultExecutedContext context) {
            var httpResponse = context.HttpContext.Response;
            var responseStream = httpResponse.Body;
            
            using (var streamReader = new StreamReader(responseStream, Encoding.UTF8, true, 512, true)) {
                var toCache = streamReader.ReadToEnd();
                var contentType = httpResponse.ContentType;
                var statusCode = httpResponse.StatusCode.ToString();

                while (!streamReader.EndOfStream) {
                    var line = await streamReader.ReadLineAsync();
                }                
            }

            base.OnResultExecuted(context);
        }
    }
}