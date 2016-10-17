using NoJS.Library.Interfaces;

namespace NoJS.Library {
    public class SwitcherOptions {
        public string LegacyKey { get; set; } = "legacy";
        
        public string NormalKey { get; set; } = "normal";

        public string ResetKey { get; set; } = "reset";

        public IDeviceSwitcher DefaultSwitcher { get; set; }

        public string SwitchUrl { get; set; } = "choose";
    }
}