using rental_app.entity.rental;

namespace rental_app.repository;

public class RentalRepo : AbstractRepo<Rental>
{
    public RentalRepo(string finalPath) : base(finalPath) {}
    
    public int GetNumOfActiveRentalsByUserId(long userId)
    {
        return _map.Values.Where(rental => rental.UserId == userId && rental.IsReturnOnTime == null).ToList().Count;
    }

    public List<Rental> GetActiveRentalsByUserId(long userId)
    {
        return _map.Values.Where(rental => rental.UserId == userId && rental.IsReturnOnTime == null).ToList();
    }

    public List<Rental> GetActiveRentals()
    {
        return _map.Values.Where(rental => rental.IsReturnOnTime == null).ToList();
    }

    public List<Rental> GetTerminatedRentals()
    {
        var currentDayTime = DateTime.Now;
        return _map.Values.Where(rental => rental.EndDate <= currentDayTime && rental.IsReturnOnTime == null).ToList();
    }

    public double GetTotalPenaltyAmount()
    {
        var overdueRentals = GetTerminatedRentals();

        return overdueRentals.Aggregate(0.0, (sum, rental) => sum + rental.PenaltyToPay);
    }

    public int GetNumberOfActiveRentals()
    {
        return GetActiveRentals().Count;
    }

    public int GetNumberOfTerminatedRentals()
    {
        return GetTerminatedRentals().Count;
    }
}