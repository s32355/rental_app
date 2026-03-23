using rental_app.model;
using rental_app.model.equipment;
using rental_app.model.rental;
using rental_app.model.user;
using rental_app.repository;

namespace rental_app.service;

public class RentalService
{
    private const int MaxNumberOfRentalsForStudent = 2;
    private const int MaxNumberOfRentalsForEmployee = 5;
    private const double PenaltyForEachDayOfDelay = 2.5;
    private readonly RentalRepo _rentalRepo;
    private readonly UserRepo _userRepo;
    private readonly DeviceRepo _deviceRepo;

    public RentalService(RentalRepo rentalRepo, UserRepo userRepo,
        DeviceRepo deviceRepo)
    {
        _rentalRepo = rentalRepo;
        _userRepo = userRepo;
        _deviceRepo = deviceRepo;
    }

    public Rental GetRental(long id)
    {
        var rental = _rentalRepo.GetById(id);

        if (rental == null)
        {
            throw new KeyNotFoundException($"Rental with {id} not found");
        }

        return rental;
    }

    public void AddNewRental(long userId, long deviceId, DateTime startDate, DateTime endDate)
    {
        var user = _userRepo.GetById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }

        ValidateUserNumbersRental(user);

        var device = _deviceRepo.GetById(deviceId);
        if (device == null)
        {
            throw new KeyNotFoundException("Device not found");
        }

        ValidateDeviceStatus(device);

        ValidateRangeOfStartAndEndDate(startDate, endDate);

        ChangeDeviceStatus(device, Status.InUse);

        var rental = new Rental(startDate, endDate, userId, deviceId);
        _rentalRepo.AddObject(rental);
    }

    private void ValidateUserNumbersRental(User user)
    {
        int activeRentals = _rentalRepo.GetNumOfActiveRentalsByUserId(user.Id);

        if (user is Student && activeRentals >= MaxNumberOfRentalsForStudent)
        {
            throw new InvalidOperationException(
                $"User reached limit of {MaxNumberOfRentalsForStudent} active rentals for students");
        }

        if (user is Employee && activeRentals >= MaxNumberOfRentalsForEmployee)
        {
            throw new InvalidOperationException(
                $"User reached limit of {MaxNumberOfRentalsForEmployee} active rentals for employees");
        }
    }

    private void ValidateDeviceStatus(Device device)
    {
        if (device.Status != Status.Available)
        {
            throw new InvalidOperationException("Device is not available");
        }
    }

    private void ValidateRangeOfStartAndEndDate(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            throw new InvalidOperationException("Start date of rental cannot be after end date");
        }
    }

    private void ChangeDeviceStatus(Device device, Status status)
    {
        device.Status = status;
    }

    public double ReturnRentalDevice(long rentalId)
    {
        var rental = GetRental(rentalId);
        if (rental == null)
        {
            throw new KeyNotFoundException("Rental not found");
        }

        var device = _deviceRepo.GetById(rental.DeviceId);
        if (device == null)
        {
            throw new KeyNotFoundException("Device not found");
        }

        ChangeDeviceStatus(device, Status.Available);
        
        return CalculatePenaltyForTheDelayRental(rental); 
    }

    private double CalculatePenaltyForTheDelayRental(Rental rental)
    {
        var returnDate = DateTime.Now;
        if (returnDate > rental.EndDate)
        {
            var totalLateDays = (returnDate - rental.EndDate).TotalDays;
            var penalty = Math.Round(totalLateDays * PenaltyForEachDayOfDelay, 2);
            rental.IsReturnOnTime = false;
            rental.PenaltyToPay = penalty;
        }
        else
        {
            rental.IsReturnOnTime = true;
        }

        return rental.PenaltyToPay;
    }

    public List<RentalDTO> GetActiveRentalsByUserId(long userId)
    {
        if (!_userRepo.CheckIfExists(userId))
        {
            throw new KeyNotFoundException("User not found");
        }

        var user = _userRepo.GetById(userId);
        var devices = _deviceRepo.GetObjects();
        var rentals = _rentalRepo.GetActiveRentalsByUserId(userId);
        if (rentals.Count == 0)
        {
            throw new InvalidOperationException("Nothing to Show");
        }

        var rentalsDto = new List<RentalDTO>();

        foreach (var rental in rentals)
        {
            if (!devices.TryGetValue(rental.DeviceId, out var device))
            {
                throw new KeyNotFoundException("Device not found");
            }
            
            rentalsDto.Add(new RentalDTO(rental.Id, user, device));
        }
        
        return rentalsDto;
    }

    public List<RentalDTO> GetAllActiveRentalsDto()
    {
        var rentalsDto = new List<RentalDTO>();
        var activeRentals = _rentalRepo.GetActiveRentals();
        if (activeRentals.Count == 0)
        {
            throw new InvalidOperationException("Nothing to Show");
        }
        
        var users = _userRepo.GetObjects();
        var devices = _deviceRepo.GetObjects();

        foreach (var rental in activeRentals)
        {
            if (!users.TryGetValue(rental.UserId, out var user))
            {
                throw new KeyNotFoundException("User not found");
            }

            if (!devices.TryGetValue(rental.DeviceId, out var device))
            {
                throw new KeyNotFoundException("Device not found");
            }
            
            rentalsDto.Add(new RentalDTO(rental.Id, user, device));
        }
        
        return rentalsDto;
    }

    public List<RentalDTO> GetTerminatedRentals()
    {
        var rentalsDto = new List<RentalDTO>();
        var rentals = _rentalRepo.GetTerminatedRentals();
        if (rentals.Count == 0)
        {
            throw new InvalidOperationException("Nothing to Show");
        }
        
        var users = _userRepo.GetObjects();
        var devices = _deviceRepo.GetObjects();
        
        foreach (var rental in rentals)
        {
            if (!users.TryGetValue(rental.UserId, out var user))
            {
                throw new KeyNotFoundException("User not found");
            }

            if (!devices.TryGetValue(rental.DeviceId, out var device))
            {
                throw new KeyNotFoundException("Device not found");
            }
            
            rentalsDto.Add(new RentalDTO(rental.Id, user, device));
        }
        
        return rentalsDto;
    }
}