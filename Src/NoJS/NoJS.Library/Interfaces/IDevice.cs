namespace NoJS.Library.Interfaces {
    public interface IDevice {
        bool IsLegacy { get; }
        
        bool IsNormal { get; }

        string DeviceCode { get; }
    }
}