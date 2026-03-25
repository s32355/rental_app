namespace rental_app.entity.report;

public class Report
{
    public int TotalNumberOfDevices { get; }
    public int TotalNumberOfUsers { get; }
    public int AvailableDevices { get; }
    public int ActiveRentals { get; }
    public int OverdueRentals { get; }
    public double TotalPenaltyAmount { get; }

    public Report(int totalNumberOfDevices, int totalNumberOfUsers, int availableDevices, int activeRentals,
        int overdueRentals, double totalPenaltyAmount)
    {
        TotalNumberOfDevices = totalNumberOfDevices;
        TotalNumberOfUsers = totalNumberOfUsers;
        AvailableDevices = availableDevices;
        ActiveRentals = activeRentals;
        OverdueRentals = overdueRentals;
        TotalPenaltyAmount = totalPenaltyAmount;
    }
}