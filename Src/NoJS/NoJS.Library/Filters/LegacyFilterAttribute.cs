using System.IO;
using System.Text;

using Microsoft.AspNetCore.Mvc.Filters;

namespace NoJS.Library.Filters {
    public class LegacyFilterAttribute : ActionFilterAttribute {
        public override void OnResultExecuted(ResultExecutedContext context) {
            var httpResponse = context.HttpContext.Response;
            var responseStream = httpResponse.Body;

            responseStream.Seek(0, SeekOrigin.Begin);

            using (var streamReader = new StreamReader(responseStream, Encoding.UTF8, true, 512, true)) {
                var toCache = streamReader.ReadToEnd();
                var contentType = httpResponse.ContentType;
                var statusCode = httpResponse.StatusCode.ToString();
                
            }

            base.OnResultExecuted(context);
        }
    }
}