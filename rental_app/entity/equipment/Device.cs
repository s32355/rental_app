namespace rental_app.entity.equipment;

public abstract class Device : IEntity
{
    private static long _idCounter = 1L;

    public long Id { get; init; }
    public string Name { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public DateOnly WarrantyExpireDate { get; set; }
    public Status Status { get; set; }

    protected Device(string name, DateOnly purchaseDate, DateOnly warrantyExpireDate, Status status) 
    {
        Id = _idCounter++;
        Name = name;
        PurchaseDate = purchaseDate;
        WarrantyExpireDate = warrantyExpireDate;
        Status = status;
    }
    
    
    
    
    
}