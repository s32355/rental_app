using rental_app.service;

namespace rental_app.view.reportView;

public class ReportView
{
    private readonly ReportService _reportService;

    public ReportView(ReportService reportService)
    {
        _reportService = reportService;
    }
    
    public void ShowReport()
    {
        var report = _reportService.CreateReport();
        
        Console.WriteLine("Report:");
        Console.WriteLine($"- Total number of devices: {report.TotalNumberOfDevices}");
        Console.WriteLine($"- Total number of users: {report.TotalNumberOfUsers}");
        Console.WriteLine($"- Available devices: {report.AvailableDevices}");
        Console.WriteLine($"- Active rentals: {report.ActiveRentals}");
        Console.WriteLine($"- Overdue rentals: {report.OverdueRentals}"); 
        Console.WriteLine($"- Total penalty amount: {report.TotalPenaltyAmount}");
        Console.WriteLine();
    }
}