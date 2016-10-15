namespace NoJS.Library.Interfaces {
    public interface IDeviceAccessor {
        IDevice Device { get; }

        IDevice Preference { get; }
    }
}