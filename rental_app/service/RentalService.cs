using rental_app.model;
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
        
        int activeRentals = _rentalRepo.GetNumOfActiveRentalsByUserId(userId);

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

        var device = _deviceRepo.GetById(deviceId);
        if (device == null)
        {
            throw new KeyNotFoundException("Device not found");
        }

        if (device.Status != Status.Available)
        {
            throw new InvalidOperationException("Device is not available");
        }

        if (startDate > endDate)
        {
            throw new InvalidOperationException("Start date of rental cannot be after end date");
        }

        device.Status = Status.InUse;
        
        var rental = new Rental(startDate, endDate, userId, deviceId);
        _rentalRepo.AddObject(rental);
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

        device.Status = Status.Available;

        var returnDate = new DateTime();
        if (returnDate > rental.EndDate)
        {
            var totalLateDays = (returnDate - rental.EndDate).TotalDays;
            var penalty = totalLateDays * PenaltyForEachDayOfDelay;
            rental.IsReturnOnTime = false;
            rental.PenaltyToPay = penalty;
        }
        else
        {
            rental.IsReturnOnTime = true;
        }

        return rental.PenaltyToPay;
    }

    public List<Rental> GetActiveRentalsByUserId(long userId)
    {
        return _rentalRepo.GetActiveRentalsByUserId(userId);
    }

    public List<Rental> GetTerminatedRentals()
    {
        return _rentalRepo.GetTerminatedRentals();
    }
}