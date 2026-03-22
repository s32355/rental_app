namespace rental_app.model.equipment;

public class Laptop : Device
{
    public double ScreenSize { get; set; }
    public int HardDriveCapacity { get; set; }
    
    public Laptop(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate, Status status, double screenSize, int hardDriveCapacity) : 
        base(name, purchaseDate, warrantyExpireDate, status)
    {
        ScreenSize = screenSize;
        HardDriveCapacity = hardDriveCapacity;
    }
}