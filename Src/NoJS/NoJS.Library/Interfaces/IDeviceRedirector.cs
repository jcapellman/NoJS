using Microsoft.AspNetCore.Http;

namespace NoJS.Library.Interfaces {
    public interface IDeviceRedirector {
        void RedirectToDevice(HttpContext context, string code = "");
    }
}