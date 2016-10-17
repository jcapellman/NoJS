using Microsoft.Extensions.Options;

using NoJS.Library.Enums;
using NoJS.Library.Interfaces;

namespace NoJS.Library.Common {
    public class DefaultDeviceFactory : IDeviceFactory {
        private readonly IOptions<DeviceOptions> _options;

        public DefaultDeviceFactory(IOptions<DeviceOptions> options) {
            _options = options;
        }

        public IDevice Normal() => new LiteDevice(DeviceType.Normal, string.Empty);

        public IDevice Legacy() => new LiteDevice(DeviceType.Legacy, _options.Value.LegacyCode);
    }
}