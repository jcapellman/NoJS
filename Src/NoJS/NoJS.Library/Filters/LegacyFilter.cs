using Microsoft.AspNetCore.Mvc.Filters;

namespace NoJS.Library.Filters {
    public class LegacyFilter : ActionFilterAttribute {
        public override void OnResultExecuted(ResultExecutedContext context) {
            base.OnResultExecuted(context);
        }
    }
}