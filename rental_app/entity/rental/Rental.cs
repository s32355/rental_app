namespace rental_app.entity.rental;

public class Rental : IEntity
{
    private static long _idCounter = 1L;

    public long Id { get; init; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; private set; }
    public long UserId { get; private set; }
    public long DeviceId { get; private set; }
    public bool? IsReturnOnTime { get; set; } = null;
    public double PenaltyToPay { get; set; } = 0.0;

    public Rental(DateTime startDate, DateTime endDate, long userId, long deviceId)
    {
        Id = _idCounter++;
        StartDate = startDate;
        EndDate = endDate;
        UserId = userId;
        DeviceId = deviceId;
    }
}