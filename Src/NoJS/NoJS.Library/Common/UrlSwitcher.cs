using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;

using NoJS.Library.Interfaces;

namespace NoJS.Library.Common {
    public class UrlSwitcher : IDeviceSwitcher {
        private readonly IDeviceFactory _deviceFactory;
        private readonly IDeviceRedirector _deviceRedirector;
        private readonly IOptions<DeviceOptions> _options;

        public UrlSwitcher(IOptions<DeviceOptions> options, IDeviceFactory deviceFactory, IDeviceRedirector deviceRedirector) {
            _options = options;
            _deviceFactory = deviceFactory;
            _deviceRedirector = deviceRedirector;
        }

        public int Priority => 2;

        public IDevice LoadPreference(HttpContext context) {
            var url = context.Request.GetDisplayUrl();

            return url.Contains($"//{_options.Value.LegacyCode}.") ? _deviceFactory.Legacy() : null;
        }

        public void StoreDevice(HttpContext context, IDevice device)
            => _deviceRedirector.RedirectToDevice(context, device.DeviceCode);

        public void ResetStore(HttpContext context)
            => _deviceRedirector.RedirectToDevice(context);
    }
}