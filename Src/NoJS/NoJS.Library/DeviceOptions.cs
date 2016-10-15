using System.Collections.Generic;

using NoJS.Library.Enums;

namespace NoJS.Library {
    public class DeviceOptions {
        public IEnumerable<string> MobileUserAgentPrefixes { get; set; } = new List<string>();

        public IEnumerable<string> MobileUserAgentKeywords { get; set; } = new List<string>();

        public IEnumerable<string> TabletUserAgentKeywords { get; set; } = new List<string>();

        public IEnumerable<string> NormalUserAgentKeywords { get; set; } = new List<string>();

        public string MobileCode { get; set; } = "m";

        public string TabletCode { get; set; } = "t";

        public DeviceLocationExpanderFormat Format { get; set; } = DeviceLocationExpanderFormat.Suffix;
    }
}