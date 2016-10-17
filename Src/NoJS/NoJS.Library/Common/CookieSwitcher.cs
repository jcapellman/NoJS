using NoJS.Library.Interfaces;

using Microsoft.AspNetCore.Http;

namespace NoJS.Library.Common {
    public class CookieSwitcher : IDeviceSwitcher {
        private const string DevicePreferenceCookieKey = ".MvcDeviceDetector.Preference";
        private const string LegacyPreferenceKey = "Legacy";
        private const string NormalPreferenceKey = "Normal";

        private readonly IDeviceFactory _deviceFactory;
        private readonly IDeviceRedirector _deviceRedirector;

        public CookieSwitcher(IDeviceFactory deviceFactory, IDeviceRedirector deviceRedirector) {
            _deviceFactory = deviceFactory;
            _deviceRedirector = deviceRedirector;
        }

        public int Priority => 1;

        public IDevice LoadPreference(HttpContext context) {
            if (!context.Request.Cookies.ContainsKey(DevicePreferenceCookieKey)) return null;

            switch (context.Request.Cookies[DevicePreferenceCookieKey]) {
                case LegacyPreferenceKey:
                    return _deviceFactory.Legacy();
                case NormalPreferenceKey:
                    return _deviceFactory.Normal();
            }

            return null;
        }

        public void StoreDevice(HttpContext context, IDevice device) {
            if (device.IsLegacy) {
                context.Response.Cookies.Append(DevicePreferenceCookieKey, LegacyPreferenceKey);
            } else if (device.IsNormal) {
                context.Response.Cookies.Append(DevicePreferenceCookieKey, NormalPreferenceKey);
            }

            _deviceRedirector.RedirectToDevice(context);
        }

        public void ResetStore(HttpContext context) {
            context.Response.Cookies.Delete(DevicePreferenceCookieKey);

            _deviceRedirector.RedirectToDevice(context);
        }
    }
}