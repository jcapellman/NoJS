using NoJS.Library;
using NoJS.Library.Interfaces;
using System.Collections.Generic;

using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MvcDeviceDetector.Device {
    public class AgentResolver : IDeviceResolver {
        private static readonly string[] KnownLegacyUserAgentKeywords = { "netscape", "mosiac" };
        private readonly IDeviceFactory _deviceFactory;
        private readonly IOptions<DeviceOptions> _options;

        public AgentResolver(IOptions<DeviceOptions> options, IDeviceFactory deviceFactory) {
            _options = options;
            _deviceFactory = deviceFactory;
        }

        protected virtual IEnumerable<string> NormalUserAgentKeywords
            => _options.Value.NormalUserAgentKeywords;

        public IDevice ResolveDevice(HttpContext context) {
            var agent = context.Request.Headers["User-Agent"].FirstOrDefault()?.ToLowerInvariant();

            if (agent != null && NormalUserAgentKeywords.Any(normalKeyword => agent.Contains(normalKeyword))) {
                return _deviceFactory.Normal();
            }

            if (agent != null && agent.Length >= 4 && KnownLegacyUserAgentKeywords.Any(prefix => agent.StartsWith(prefix))) {
                return _deviceFactory.Legacy();
            }

            return _deviceFactory.Normal();
        }
    }
}