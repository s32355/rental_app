using rental_app.model;
using rental_app.model.equipment;

namespace rental_app.repository;

public class DeviceRepo : AbstractRepo<Device>
{
    public Dictionary<long, Device> GetAvailableDevices()
    {
        return _map.Where(pair => pair.Value.Status == Status.Available)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}