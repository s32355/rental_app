using rental_app.model;
using rental_app.model.equipment;
using rental_app.repository;

namespace rental_app.service;

public class DeviceService
{
    private readonly DeviceRepo _deviceRepo;

    public DeviceService(DeviceRepo deviceRepo)
    {
        _deviceRepo = deviceRepo;
    }

    public void AddLaptop(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate, 
        double screenSize, int hardDriveCapacity)
    {
        Device laptop = new Laptop(name, purchaseDate, warrantyExpireDate, Status.Available, 
            screenSize, hardDriveCapacity);
        _deviceRepo.AddObject(laptop);
    }

    public void AddProjector(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate,
        int resolutionWidth, int resolutionHeight)
    {
        Device projector = new Projector(name, purchaseDate, warrantyExpireDate, Status.Available,
            resolutionWidth, resolutionHeight);
        _deviceRepo.AddObject(projector);
    }

    public void AddCamera(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate, 
        int resolutionMegapixels, int storageCapacityGb)
    {
        Device camera = new Camera(name, purchaseDate, warrantyExpireDate, Status.Available,
            resolutionMegapixels, storageCapacityGb);
        _deviceRepo.AddObject(camera);
    }

    public Device GetDevice(long id)
    {
        var device = _deviceRepo.GetById(id);

        if (device == null)
        {
            throw new KeyNotFoundException($"Device with {id} not found");
        }

        return device;
    }

    public Dictionary<long, Device> GetDevices()
    {
        return _deviceRepo.GetObjects();
    }

    public Dictionary<long, Device> GetAvailableDevices()
    {
        return _deviceRepo.GetAvailableDevices();
    }

    public void MarkDeviceAsUnavailable(long deviceId, Status status)
    {
        if (status != Status.Broken && status != Status.InService)
        {
            throw new ArgumentException("Only 'Broken' or 'InService' statuses are allowed here");
        }

        var device = GetDevice(deviceId);

        if (device.Status == Status.InUse)
        {
            throw new InvalidOperationException("Device is currently in use");
        }

        device.Status = status;
    }

    public bool CheckIfDeviceExists(long id)
    {
        return _deviceRepo.CheckIfExists(id);
    }
}