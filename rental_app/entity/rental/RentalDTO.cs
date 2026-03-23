using rental_app.model.equipment;
using rental_app.model.user;

namespace rental_app.model.rental;

public class RentalDTO
{
    public long Id { get; }
    public User UserObj { get; }
    public Device DeviceObj { get; }

    public RentalDTO(long id, User userObj, Device deviceObj)
    {
        Id = id;
        UserObj = userObj;
        DeviceObj = deviceObj;
    }
}