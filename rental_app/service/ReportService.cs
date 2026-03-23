using rental_app.model.report;
using rental_app.repository;

namespace rental_app.service;

public class ReportService
{
    private readonly DeviceRepo _deviceRepo;
    private readonly UserRepo _userRepo;
    private readonly RentalRepo _rentalRepo;

    public ReportService(DeviceRepo deviceRepo, UserRepo userRepo, RentalRepo rentalRepo)
    {
        _deviceRepo = deviceRepo;
        _userRepo = userRepo;
        _rentalRepo = rentalRepo;
    }

    public Report CreateReport()
    {
        var totalNumberOfDevices = _deviceRepo.GetTotalNumberOfDevices();
        var totalNumberOfUsers = _userRepo.GetTotalNumberOfUsers();
        var availableDevices = _deviceRepo.GetNumberOfAvailableDevices();
        var activeRentals = _rentalRepo.GetNumberOfActiveRentals();
        var overdueRentals = _rentalRepo.GetNumberOfTerminatedRentals();
        var totalPenaltyAmount = _rentalRepo.GetTotalPenaltyAmount();
        
        return new Report(totalNumberOfDevices, totalNumberOfUsers, availableDevices, 
            activeRentals, overdueRentals, totalPenaltyAmount);
    }
}