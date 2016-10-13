namespace NoJS.Library.Interfaces {
    public interface IDevice {

        bool IsMobile { get; }

        bool IsTablet { get; }

        bool IsNormal { get; }

        string DeviceCode { get; }
    }
}