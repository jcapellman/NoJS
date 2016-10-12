using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NoJS.Library {
    public class NoJSViewEngine : RazorViewEngine {
        public NoJSViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, ILoggerFactory loggerFactory)
            : base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, loggerFactory)
        {
        }        
      
    }
}