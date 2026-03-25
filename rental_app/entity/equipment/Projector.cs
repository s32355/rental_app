namespace rental_app.entity.equipment;

public class Projector : Device
{
    public int ResolutionWidth { get; set; }
    public int ResolutionHeight { get; set; }

    public Projector(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate, Status status,
        int resolutionWidth, int resolutionHeight) : 
        base(name, purchaseDate, warrantyExpireDate, status)
    {
        ResolutionWidth = resolutionWidth;
        ResolutionHeight = resolutionHeight;
    }
}