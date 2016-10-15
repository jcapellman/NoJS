using Microsoft.AspNetCore.Http;

namespace NoJS.Library.Interfaces {
    public interface IDeviceResolver {
        IDevice ResolveDevice(HttpContext context);
    }
}