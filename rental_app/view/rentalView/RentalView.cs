using rental_app.model.rental;
using rental_app.service;

namespace rental_app.view.rentalView;

public class RentalView
{
    private readonly RentalService _rentalService;

    public RentalView(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public void CreateNewRentalView(Action userView, Action deviceView)
    {
        userView();
        Console.Write("Enter user id: ");
        var userIdInp = Console.ReadLine();

        if (!long.TryParse(userIdInp, out var userId))
        {
            Console.WriteLine("Wrong input");
            return;
        }

        deviceView();
        Console.Write("Enter device id: ");
        var deviceIdInp = Console.ReadLine();

        if (!long.TryParse(deviceIdInp, out var deviceId))
        {
            Console.WriteLine("Wrong input");
            return;
        }
        
        Console.Write("Enter the number of rental days: ");
        var rentalDaysInp = Console.ReadLine();

        if (!int.TryParse(rentalDaysInp, out var rentalDays))
        {
            Console.WriteLine("Wrong input");
            return;
        }

        var startDate = DateTime.Now;
        var endDate = startDate.AddDays(rentalDays);

        try
        {
            _rentalService.AddNewRental(userId, deviceId, startDate, endDate);
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        catch (InvalidOperationException e )
        {
            Console.WriteLine(e.Message);
            return;
        }
        
        Console.WriteLine("Rental added successfully");
    }

    public void ReturnDeviceAndCloseRental()
    {
        try
        {
            ShowRentalsDto(_rentalService.GetAllActiveRentalsDto(), "All active rentals:");
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.Write("Enter rental id to return: ");
        var rentalIdInp = Console.ReadLine();

        if (!long.TryParse(rentalIdInp, out var rentalId))
        {
            Console.WriteLine("Wrong input");
            return;
        }

        try
        {
            var penaltyToPay = _rentalService.ReturnRentalDevice(rentalId);
            if (penaltyToPay > 0.0)
            {
                Console.WriteLine($"Penalty for late return is {penaltyToPay}");
            }
            else
            {
                Console.WriteLine("No penalty");
            }
            
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void ShowRentalsDto(List<RentalDTO> rentals, string title)
    {
           Console.WriteLine(title);

           foreach (var rental in rentals)
           {
               Console.WriteLine($"id: {rental.Id} - user name: {rental.UserObj.Name} - user surname: {rental.UserObj.Surname} - device: {rental.DeviceObj.Name}");
           }
    }

    public void ShowAllActiveRentalsForUser(Action showUsers)
    {
        showUsers();
        
        Console.Write("Enter id of user you want to see: ");
        var userIdInp = Console.ReadLine();

        if (!long.TryParse(userIdInp, out var userId))
        {
            Console.WriteLine("Wrong input"); 
        }

        try
        {
            ShowRentalsDto(_rentalService.GetActiveRentalsByUserId(userId),
                $"All active rentals for user id {userId};");
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void ShowOverdueRentals()
    {
        try
        {
            ShowRentalsDto(_rentalService.GetTerminatedRentals(), "All overdue rentals:");
        }
        catch (KeyNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}