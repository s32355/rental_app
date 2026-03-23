using rental_app.model;
using rental_app.model.equipment;
using rental_app.service;

namespace rental_app.view.deviceView;

public class DeviceView
{
    private readonly DeviceService _deviceService;

    public DeviceView(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    
    public void ShowTypesOfDevicesToCreate()
    {
        Console.WriteLine("1. Create new Laptop");
        Console.WriteLine("2. Create new Projector");
        Console.WriteLine("3. Create new Camera");
        Console.WriteLine("Type QQ for returning to main menu");
        
        Console.Write("Enter number (1-3) or type 'QQ': ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1" or "2" or "3":
                GetValuesFromUser(input);
                break;
            case "QQ":
                return;
            default:
                Console.WriteLine("Wrong input!");
                Console.WriteLine("################################\n\n");
                return;
        }
    }

    private void GetValuesFromUser(string input)
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        
        Console.Write("Enter purchase date (yyyy-MM-dd): ");
        string purchaseDateInp = Console.ReadLine();

        if (!DateOnly.TryParse(purchaseDateInp, out var purchaseDate))
        {
            Console.WriteLine("Invalid date format!");
            return;
        }
        
        Console.Write("Enter warranty expire date (yyyy-MM-dd): ");
        string expireDateInp = Console.ReadLine();

        if (!DateOnly.TryParse(expireDateInp, out var expireDate))
        {
            Console.WriteLine("Invalid date format!");
            return;
        }

        switch (input)
        {
            case "1":
                GetValuesToCreateLaptop(name, purchaseDate, expireDate);       
                break;
            case "2":
                GetValuesToCreateProjector(name, purchaseDate, expireDate);
                break;
            case "3":
                GetValuesToCreateCamera(name, purchaseDate, expireDate);
                break;
            default:
                return;
        }
    }

    private void GetValuesToCreateLaptop(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate)
    {
        Console.Write("Enter screen size (xx.xx): ");
        string screenSizeInp = Console.ReadLine();

        if (!Double.TryParse(screenSizeInp, out var screenSize))
        {
            Console.WriteLine("Invalid format!");
            return;
        }
        
        Console.Write("Enter hard drive capacity in GB: ");
        string hardDriveCapacityInp = Console.ReadLine();

        if (!int.TryParse(hardDriveCapacityInp, out var hardDriveCapacity))
        {
            Console.WriteLine("Invalid format!");
            return;
        }
        
        _deviceService.AddLaptop(name,purchaseDate, warrantyExpireDate, screenSize, hardDriveCapacity);
        Console.WriteLine("Laptop added successfully");
    }
    
    private void GetValuesToCreateProjector(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate)
    {
        Console.Write("Enter resolution width: ");
        string resolutionWidthInp = Console.ReadLine();

        if (!int.TryParse(resolutionWidthInp, out var resolutionWidth))
        {
            Console.WriteLine("Invalid format!");
            return;
        }
        
        Console.Write("Enter resolution height: ");
        string resolutionHeightInp = Console.ReadLine();

        if (!int.TryParse(resolutionHeightInp, out var resolutionHeight))
        {
            Console.WriteLine("Invalid format!");
            return;
        }
        
        _deviceService.AddProjector(name,purchaseDate, warrantyExpireDate, resolutionWidth, resolutionHeight);
        Console.WriteLine("Projector added successfully");
    }
    
    private void GetValuesToCreateCamera(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate)
    {
        Console.Write("Enter resolution in megapixels: ");
        string resolutionMegapixelsInp = Console.ReadLine();

        if (!int.TryParse(resolutionMegapixelsInp, out var resolutionMegapixels))
        {
            Console.WriteLine("Invalid format!");
            return;
        }
        
        Console.Write("Enter storage capacity in GB: ");
        string storageCapacityInp = Console.ReadLine();

        if (!int.TryParse(storageCapacityInp, out var storageCapacity))
        {
            Console.WriteLine("Invalid format!");
            return;
        }
        
        _deviceService.AddCamera(name,purchaseDate, warrantyExpireDate, resolutionMegapixels, storageCapacity);
        Console.WriteLine("Camera added successfully");
    }

    public void DisplayAllDevicesWithCurrentStatuses()
    {
        var devices = _deviceService.GetDevices();
        
        Console.WriteLine("All devices: ");
        
        foreach (var device in devices.Values)
        {
            Console.WriteLine($"id: {device.Id} - name: {device.Name} - status: {device.Status}");
        }
    }

    public void ShowAvailableDevices()
    {
        var availableDevices = _deviceService.GetAvailableDevices();
        
        Console.WriteLine("All available devices:");
        foreach (var device in availableDevices.Values)
        {
            Console.WriteLine($"id: {device.Id} - name: {device.Name}");
        }
    }

    public void MarkDeviceAsUnavailable()
    {
        ShowAvailableDevices();
        
        Console.Write("Enter id of device: ");
        var deviceIdInp = Console.ReadLine();

        if (!long.TryParse(deviceIdInp, out var deviceId))
        {
            throw new InvalidOperationException("Wrong input");
        }

        if (_deviceService.CheckIfDeviceExists(deviceId))
        {
            try
            {
                var statusNum = ShowStatuses();

                Status status;

                if (statusNum == 1)
                {
                    status = Status.Broken;
                }
                else
                {
                    status = Status.InService;
                }

                _deviceService.MarkDeviceAsUnavailable(deviceId, status);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            Console.WriteLine("Device not found");
        }
    }

    private int ShowStatuses()
    {
        Console.WriteLine("Statuses:");
        Console.WriteLine("1. Broken");
        Console.WriteLine("2. In service");
        
        Console.Write("Enter status number: ");
        var statusInp = Console.ReadLine();

        if (!int.TryParse(statusInp, out var status))
        {
            throw new InvalidOperationException("Wrong input");
        }

        if (status < 1 || status > 2)
        {
            throw new InvalidOperationException("Wrong input");
        }

        return status;
    }
}