namespace rental_app.entity.equipment;

public class Camera : Device
{
    public int ResolutionMegapixels { get; set; }
    public int StorageCapacityGb { get; set; }

    public Camera(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate, Status status, 
        int resolutionMegapixels, int storageCapacityGb) :
        base(name, purchaseDate, warrantyExpireDate, status)
    {
        ResolutionMegapixels = resolutionMegapixels;
        StorageCapacityGb = storageCapacityGb;
    }
}