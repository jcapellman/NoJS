using NoJS.Library.Enums;
using NoJS.Library.Interfaces;

namespace NoJS.Library.Common {
    public class LiteDevice : IDevice {
        private readonly DeviceType _deviceType;

        public LiteDevice(DeviceType deviceType, string code = "") {
            _deviceType = deviceType;
            DeviceCode = code;
        }

        public bool IsLegacy => _deviceType == DeviceType.Legacy;
        public bool IsNormal => _deviceType == DeviceType.Normal;
        public string DeviceCode { get; }
        public override string ToString() {
            return $"{_deviceType}";
        }
    }
}