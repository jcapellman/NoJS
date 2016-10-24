using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NoJS.Library.Filters {
    public class LegacyFilterAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);

            var httpResponse = filterContext.HttpContext.Response;
            var responseStream = httpResponse.Body;

            filterContext.Result = new JsonResult(httpResponse.Body.Read(null, 0, 0));
        }
    }
}