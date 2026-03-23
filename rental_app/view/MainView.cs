using rental_app.service;
using rental_app.view.deviceView;
using rental_app.view.rentalView;
using rental_app.view.userView;

namespace rental_app.view;

public class MainView
{
    private readonly UserView _userView;
    private readonly DeviceView _deviceView;
    private readonly RentalView _rentalView;
    
    public MainView(UserView userView, DeviceView deviceView, RentalView rentalView)
    {
        _userView = userView;
        _deviceView = deviceView;
        _rentalView = rentalView;
    }
    
    public void Start()
    {
        bool quitApp = true;

        while (quitApp)
        {   
            ShowMainMenu();
            Console.Write("Enter number (1-9) or type 'QQ': ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    _userView.ShowTypesOfUsersToCreate();
                    break;
                case "2":
                    _deviceView.ShowTypesOfDevicesToCreate();
                    break;
                case "3":
                    _deviceView.DisplayAllDevicesWithCurrentStatuses();
                    break;
                case "4":
                    _deviceView.ShowAvailableDevices();
                    break;
                case "5":
                    _rentalView.CreateNewRentalView(_userView.ShowAllUsers, _deviceView.ShowAvailableDevices);
                    break;
                case "6":
                    _rentalView.ReturnDeviceAndCloseRental();
                    break;
                case "7":
                    _deviceView.MarkDeviceAsUnavailable();
                    break;
                case "8":
                    _rentalView.ShowAllActiveRentalsForUser(_userView.ShowAllUsers);
                    break;
                case "9":
                    _rentalView.ShowOverdueRentals();
                    break;
                case "QQ":
                    quitApp = false;
                    break;
            }
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("1. Add a new user to the system.");
        Console.WriteLine("2. Add a new device of a given type.");
        Console.WriteLine("3. Display a list of all devices with their current status.");
        Console.WriteLine("4. Display only devices available for rental.");
        Console.WriteLine("5. Rent a device to a user.");
        Console.WriteLine("6. Return a device along with calculating any late fee.");
        Console.WriteLine("7. Mark a device as unavailable, e.g. due to damage or servicing.");
        Console.WriteLine("8. Display active rentals for a given user.");
        Console.WriteLine("9. Display a list of overdue rentals.");
        Console.WriteLine("Type QQ for close the app");
    }
}