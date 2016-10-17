namespace NoJS.Library.Interfaces {
    public interface IDeviceFactory {
        IDevice Normal();

        IDevice Legacy();
    }
}