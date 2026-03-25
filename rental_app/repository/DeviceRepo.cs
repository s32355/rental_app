using rental_app.entity.equipment;

namespace rental_app.repository;

public class DeviceRepo : AbstractRepo<Device>
{
    public DeviceRepo(string finalPath) : base(finalPath) {}

    public Dictionary<long, Device> GetAvailableDevices()
    {
        return _map.Where(pair => pair.Value.Status == Status.Available)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    public int GetTotalNumberOfDevices()
    {
        return _map.Count;
    }

    public int GetNumberOfAvailableDevices()
    {
        return GetAvailableDevices().Count;
    }
}