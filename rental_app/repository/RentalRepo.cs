using rental_app.model.rental;

namespace rental_app.repository;

public class RentalRepo : IRepo<Rental>
{
    public int GetNumOfActiveRentalsByUserId(long userId)
    {
        return _map.Values.Where(rental => rental.UserId == userId && rental.IsReturnOnTime == null).Count();
    }

    public List<Rental> GetActiveRentalsByUserId(long userId)
    {
        return _map.Values.Where(rental => rental.UserId == userId && rental.IsReturnOnTime == null).ToList();
    }

    public List<Rental> GetTerminatedRentals()
    {
        var currentDayTime = new DateTime();
        return _map.Values.Where(rental => rental.EndDate <= currentDayTime && rental.IsReturnOnTime == null).ToList();
    }
}